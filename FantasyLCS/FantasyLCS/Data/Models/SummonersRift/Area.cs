using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyLCS.Data.Models.SummonersRift
{
    public class Area
    {
        public string Name { get; set; }

        public int BlueTowerHealth { get; set; }
        public int BlueInhibiterHealth { get; set; }
        public int BlueInhibTimer { get; set; }
        public int BlueNexusHealth { get; set; }
        public int BlueMinions { get; set; }

        public int RedTowerHealth { get; set; }
        public int RedInhibiterHealth { get; set; }
        public int RedInhibTimer { get; set; }
        public int RedNexusHealth { get; set; }
        public int RedMinions { get; set; }

        public void DamageTower(ref Models.Champions.GameChampion champ) {
            if (champ.PlayerStats.TeamColor == Champions.TeamColor.Red) {

            } else {

            }
        }
    }
}
