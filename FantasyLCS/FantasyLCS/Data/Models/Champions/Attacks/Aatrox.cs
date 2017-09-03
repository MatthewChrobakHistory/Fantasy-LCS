namespace FantasyLCS.Data.Models.Champions.Custom
{
    public class Aatrox : GameChampion
    {
        public override void ProcessCombat() {
            if (this.EnemiesInMyArea()) {
                var enemies = this.GetEnemiesInMyArea();
            }
        }
    }
}
