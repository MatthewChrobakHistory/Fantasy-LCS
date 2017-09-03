using LoL.Data.Models;
using System.IO;
using LoL.IO;
using LoL.Data.Models.Roster;

namespace LoL.Data
{
    public static class DataManager
    {
        public static readonly string AccountPath = LoL.DataPath + "accounts\\";
        public static string AccountFile { private set; get; }

        public static Account Account { private set; get; }

        public static void Load() {
            // Inlcude all data-loading logic here
        }

        public static void Save() {
            // Include all data-saving logic here.
        }

        public static void SaveAccount() {
            var buffer = new DataBuffer();

            buffer.Write(Account.Roster.Count);
            foreach (var member in Account.Roster) {
                buffer.Write(member.Name);

                buffer.Write(member.Attributes.Count);
                foreach (var attribute in member.Attributes) {
                    buffer.Write((int)attribute);
                }

                buffer.Write(member.Champion.Count);
                foreach (var key in member.Champion.Keys) {
                    buffer.Write(key);
                    buffer.Write(member.Champion[key].MaxSkillLevel);
                    buffer.Write(member.Champion[key].SkillLevel);
                }
            }

            buffer.Save(AccountFile + "profile.dat", true);
        }

        public static void LoadAccount(string accountName) {
            Account = new Account();
            DataManager.AccountFile = DataManager.AccountPath + accountName + "\\";

            if (Directory.Exists(DataManager.AccountPath + accountName + "\\")) {
                var buffer = new DataBuffer(AccountFile + "profile.dat");

                int memCount = buffer.ReadInt();
                for (int i = 0; i < memCount; i++) {
                    Account.Roster.Add(new TeamMember(buffer.ReadString()));

                    int attCount = buffer.ReadInt();
                    for (int att = 0; att < attCount; att++) {
                        Account.Roster[i].Attributes.Add((MemberAttribute)buffer.ReadInt());
                    }

                    int champCount = buffer.ReadInt();
                    for (int c = 0; c < champCount; c++) {
                        string key = buffer.ReadString();
                        var champ = new Champion();
                        champ.MaxSkillLevel = buffer.ReadByte();
                        champ.SkillLevel = buffer.ReadByte();
                        Account.Roster[i].Champion.Add(key, champ);
                    }
                }
            } else {
                Account.Generate();
                Directory.CreateDirectory(DataManager.AccountPath + accountName + "\\");
            }
        }
    }
}
