using System.Collections.Generic;

namespace LoL.Data.Models.Roster
{
    public class TeamMember
    {
        public Dictionary<string, Champion> Champion;
        public List<MemberAttribute> Attributes;

        public string Name;
        
        public TeamMember(string name) {
            this.Name = name;
            this.Champion = new Dictionary<string, Roster.Champion>();
            this.Attributes = new List<MemberAttribute>();
        }

        public void Generate() {
            foreach (string name in ChampionMeta.Names) {
                var champion = new Champion();
                champion.Generate();
                this.Champion.Add(name, champion);
            }

            this.Attributes.Add(MemberAttribute.TeamLeader);
        }

        public bool HasAttribute(MemberAttribute attribute) {
            foreach (var value in this.Attributes) {
                if (value == attribute) {
                    return true;
                }
            }

            return false;
        }

        public void AddAttribute(MemberAttribute attribute) {
            if (!this.HasAttribute(attribute)) {
                this.Attributes.Add(attribute);
            }
        }
    }
}
