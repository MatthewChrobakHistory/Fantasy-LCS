using LoL.Data.Models.Roster;
using System.Collections.Generic;

namespace LoL.Data.Models
{
    public class Account
    {
        public List<TeamMember> Roster;

        public Account() {
            this.Roster = new List<TeamMember>();
        }

        public void Generate() {
            var member = new TeamMember("Random Guy");
            member.Generate();
            this.Roster.Add(member);
        }
    }
}
