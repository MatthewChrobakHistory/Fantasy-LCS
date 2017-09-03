namespace FantasyLCS.Data.Models
{
    public class PerLevelStat
    {
        public int Base { get; set; }
        public int LevelModifier { get; set; }

        public PerLevelStat() {
            this.Base = 0;
            this.LevelModifier = 0;
        }
        public PerLevelStat(int BaseVal, int LevelVal) {
            this.Base = BaseVal;
            this.LevelModifier = LevelVal;
        }
    }
}
