using FantasyLCS.Data.Models.Champions;
using FantasyLCS.Data.Models.SummonersRift;
using System.Collections.Generic;

namespace FantasyLCS.Data.Models
{
    public class Game
    {
        public GameChampion[] RedTeam;
        public GameChampion[] BlueTeam;
        public Map Map = new Map();
        public bool Running;
        public int ElapsedTime = 0;

        public Game(BaseChampion[] redTeam, BaseChampion[] blueTeam, string[] redNames, string[] blueNames) {
            
            // Initialize the arrays.
            this.RedTeam = new GameChampion[redTeam.Length];
            this.BlueTeam = new GameChampion[blueTeam.Length];
            
            // Transfer over data.
            for (int n = 0; n < 2; n++) {
                BaseChampion[] baseArray;
                GameChampion[] gameArray;
                string[] names;

                if (n == 0) {
                    baseArray = redTeam;
                    gameArray = this.RedTeam;
                    names = redNames;
                } else {
                    baseArray = blueTeam;
                    gameArray = this.BlueTeam;
                    names = blueNames;
                }

                for (int i = 0; i < baseArray.Length; i++) {
                    var bchamp = baseArray[i];
                    var champ = gameArray[i];

                    // Transfer all the values, and set the defaults.
                    champ.BaseChampionStats = bchamp.BaseChampionStats;

                    champ.PlayerStats.Gold = 475;
                    if (i < names.Length) {
                        champ.PlayerStats.PlayerName = names[i];
                    } else {
                        champ.PlayerStats.PlayerName = "null";
                    }
                    champ.PlayerStats.Role = i;
                    champ.SetCurrentLocation(Point.ShopXLocation, n);
                    champ.SetFocusLocation(Point.FocusDNE, Point.FocusDNE);
                }
            }
        }

        public void BeginGame() {
            int tick = 0;
            // Purchase items.
            // Sort into lanes.
            // Separate the map into a 5x5 grid
            // Make them move at increments of 15 seconds from point to point.
            // Spawn creeps at 1:30.
            
            // Begin laning.
            // 1-6 creeps per wave.
            // 2 creep waves per minute.
            // Use attack speed to factor in how fast we cleared through minions and
            //     How much poke we can apply to the enemy.
            // If champ has less than 10% of his health or so, teleport back home
            // After every half minute, check to see if we backed. If we backed, progress our items
            // If jungler touches our section, increase posibility of being ganked by % / 2 because the jungler touches 2 lanes.

            while (Running) {

                // Buy items and crap.
                // Process movement.
                foreach (var champion in RedTeam) {
                    champion.TryBuyItems();
                    champion.ProcessMovement();
                }
                foreach (var champion in BlueTeam) {
                    champion.TryBuyItems();
                    champion.ProcessMovement();
                }

                // Attacks
                for (int turn = 0; turn < 4; turn++) {
                    foreach (var champion in RedTeam) {
                        champion.ProcessCombat();
                    }
                    foreach (var champion in BlueTeam) {
                        champion.ProcessCombat();
                    }
                }

                ElapsedTime += 15;
                // Set Waypoints, and move creeps.
                Map.ProcessLogic();
            }
        }

        public GameChampion GetChamp(string color, int role) {
            if (color.ToLower() == TeamColor.Red) {
                foreach (var champ in RedTeam) {
                    if (champ.PlayerStats.Role == role) {
                        return champ;
                    }
                }
            } else {
                foreach (var champ in BlueTeam) {
                    if (champ.PlayerStats.Role == role) {
                        return champ;
                    }
                }
            }
            return default(GameChampion);
        }

        public bool EnemiesInArea(string color, int x, int y) {
            switch (color.ToLower()) {
                case TeamColor.Red:
                    foreach (var champion in BlueTeam) {
                        if (champion.GetCurrentLocation().X == x && champion.GetCurrentLocation().Y == y) {
                            return true;
                        }
                    }
                    break;
                case TeamColor.Blue:
                    foreach (var champion in RedTeam) {
                        if (champion.GetCurrentLocation().X == x && champion.GetCurrentLocation().Y == y) {
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }

        public GameChampion[] GetEnemies(GameChampion player) {
            var enemies = BlueTeam;
            var enemylist = new List<GameChampion>();

            if (player.PlayerStats.TeamColor.ToLower() != TeamColor.Blue) {
                enemies = RedTeam;
            }

            foreach (var enemy in enemies) {
                if (enemy.Location.Current == player.Location.Current) {
                    enemylist.Add(enemy);
                }
            }

            return enemylist.ToArray();
        }
    }
}
