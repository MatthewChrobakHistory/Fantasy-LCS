using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyLCS.Data.Models.SummonersRift
{
    public class Map
    {
        // Spawn coords are x = -1
        // Spawn type is y = 0 or y = 1
        public Area[,] Area = new Area[5, 5];
        private Area[,] _processedAreas;

        public Map() {
            #region Initialize Map Areas
            Area[0, 0] = new Area() {
                Name = "Top River"
            };
            Area[0, 1] = new Area() {
                Name = "Top Tier 1 Red",
                RedTowerHealth = Tower.BaseHealth
            };
            Area[0, 2] = new Area() {
                Name = "Top Tier 2 Red",
                RedTowerHealth = Tower.BaseHealth
            };
            Area[0, 3] = new Area() {
                Name = "Top Tier 3 Red",
                RedTowerHealth = Tower.BaseHealth,
                RedInhibiterHealth = Inhibiter.BaseHealth
            };
            Area[0, 4] = new Area() {
                Name = "Red Nexus",
                RedTowerHealth = Tower.BaseHealth * 2,
                RedNexusHealth = Nexus.BaseHealth
            };
            Area[1, 0] = new Area() {
                Name = "Top Tier 1 Blue",
                BlueTowerHealth = Tower.BaseHealth
            };
            Area[1, 1] = new Area() {
                Name = "Baron Pit"
            };
            Area[1, 2] = new Area() {
                Name = "Top Red Jungle"
            };
            Area[1, 3] = new Area() {
                Name = "Mid Tier 2-3 Red",
                RedTowerHealth = Tower.BaseHealth * 2,
                RedInhibiterHealth = Inhibiter.BaseHealth
            };
            Area[1, 4] = new Area() {
                Name = "Bot Tier 3 Red",
                RedTowerHealth = Tower.BaseHealth,
                RedInhibiterHealth = Inhibiter.BaseHealth
            };
            Area[2, 0] = new Area() {
                Name = "Top Tier 2 Blue",
                BlueTowerHealth = Tower.BaseHealth
            };
            Area[2, 1] = new Area() {
                Name = "Top Blue Jungle"
            };
            Area[2, 2] = new Area() {
                Name = "Mid Tier 1 Blue Red",
                RedTowerHealth = Tower.BaseHealth,
                BlueTowerHealth = Tower.BaseHealth
            };
            Area[2, 3] = new Area() {
                Name = "Bot Red Jungle"
            };
            Area[2, 4] = new Area() {
                Name = "Bot Tier 2 Red",
                RedTowerHealth = Tower.BaseHealth
            };
            Area[3, 0] = new Area() {
                Name = "Top Tier 3 Blue",
                BlueTowerHealth = Tower.BaseHealth,
                BlueInhibiterHealth = Inhibiter.BaseHealth
            };
            Area[3, 1] = new Area() {
                Name = "Mid 2-3 Blue",
                BlueTowerHealth = Tower.BaseHealth * 2,
                BlueInhibiterHealth = Inhibiter.BaseHealth
            };
            Area[3, 2] = new Area() {
                Name = "Bot Blue Jungle"
            };
            Area[3, 3] = new Area() {
                Name = "Dragon Pit"
            };
            Area[3, 4] = new Area() {
                Name = "Bot Tier 1 Red",
                RedTowerHealth = Tower.BaseHealth
            };
            Area[4, 0] = new Area() {
                Name = "Blue Nexus",
                BlueTowerHealth = Tower.BaseHealth * 2,
                BlueNexusHealth = Nexus.BaseHealth
            };
            Area[4, 1] = new Area() {
                Name = "Bot Tier 3 Blue",
                BlueTowerHealth = Tower.BaseHealth,
                BlueInhibiterHealth = Inhibiter.BaseHealth
            };
            Area[4, 2] = new Area() {
                Name = "Bot Tier 2 Blue",
                BlueTowerHealth = Tower.BaseHealth
            };
            Area[4, 3] = new Area() {
                Name = "Bot Tier 1 Blue",
                BlueTowerHealth = Tower.BaseHealth
            };
            Area[4, 4] = new Area() {
                Name = "Bot River"
            };
            #endregion

            // Copy over the data so we can use it for processing.
            _processedAreas = Area;
        }

        public void ProcessLogic() {
            Champions.GameChampion[] team = DataManager.Game.BlueTeam;

            for (int i = 0; i < 2; i++) {
                if (i == 1) {
                    team = DataManager.Game.RedTeam;
                }
                foreach (var champion in team) {
                    // Are we low enough to head back?
                    if (champion.ChampionStats.Health < champion.PlayerStats.MaxHealth * 0.30) {
                        champion.HeadBack();
                    } else {
                        // We're not super low. 
                        var focus = champion.GetFocusLocation();
                        var current = champion.GetCurrentLocation();

                        // Logic to check for moving at the end of the map, but it doesn't account for minions
                        // enemies or structures.

                        //if (focus.X != Point.FocusDNE && focus.Y != Point.FocusDNE) {
                        //    if (focus.X != current.X) {
                        //        if (champion.FocusX < champion.X) {
                        //            champion.X--;
                        //        } else {
                        //            champion.X++;
                        //        }
                        //    }
                        //    if (champion.FocusY != champion.Y) {
                        //        if (champion.FocusY < champion.Y) {
                        //            champion.Y--;
                        //        } else {
                        //            champion.Y++;
                        //        }
                        //    }
                        //}
                    }
                }
            }

        }
    }
}
