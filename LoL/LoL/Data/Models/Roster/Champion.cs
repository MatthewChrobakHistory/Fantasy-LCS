namespace LoL.Data.Models.Roster
{
    public class Champion
    {
        // How good a player is at a champion
        public byte SkillLevel;
        public byte MaxSkillLevel;

        public void Generate() {
            this.MaxSkillLevel = (byte)Rng.Next(60, 255);
            this.SkillLevel = 0;
        }
    }
}
