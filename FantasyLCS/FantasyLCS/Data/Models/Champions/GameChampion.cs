using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyLCS.Data.Models.Champions
{
    public enum Roles
    {
        Top,
        Mid,
        Bot,
        Support,
        Jungle
    }

    public enum DamageType
    {
        True,
        AD,
        AP
    }

    public static class TeamColor
    {
        public const string Red = "Red";
        public const string Blue = "Blue";
    }

    public class GameChampion : BaseChampion
    {
        public Properties.GameChampionStats PlayerStats = new Properties.GameChampionStats();
        public Properties.GameLocation Location = new Properties.GameLocation();
        public Properties.Stats ChampionStats = new Properties.Stats();
        
        public void AddXP(int amount) {
            // Add the xp to their player.
            this.PlayerStats.Experience += amount;

            // Check to see if they leveled up.
            int NextLevel = (this.PlayerStats.Level + 1) * 100 + 80;
            if (this.PlayerStats.Experience >= NextLevel) {
                this.PlayerStats.Experience -= NextLevel;
                this.PlayerStats.Level++;
            }
        }

        public void HeadBack() {
            if (this.PlayerStats.TeamColor == TeamColor.Red) {
                this.SetCurrentLocation(y: Point.RedShopYLocation);
            } else {
                this.SetCurrentLocation(y: Point.BlueShopYLocation);
            }
            this.SetCurrentLocation(x: Point.ShopXLocation);
            this.SetFocusLocation(Point.FocusDNE, Point.FocusDNE);
        }

        public void TryBuyItems() {
            // At home and no focus point.
            if (this.GetCurrentLocation().X == Point.ShopXLocation && this.GetFocusLocation().Y == Point.FocusDNE && this.GetFocusLocation().Y == Point.FocusDNE) {

            }
        }



        public bool EnemiesInMyArea() {
            return DataManager.Game.EnemiesInArea(this.PlayerStats.TeamColor, this.Location.Current.X, this.Location.Current.Y);
        }
        public GameChampion[] GetEnemiesInMyArea() {
            return DataManager.Game.GetEnemies(this);
        }

        public virtual void Q(ref GameChampion Victim) {

        }
        public virtual void W(ref GameChampion Victim) {

        }
        public virtual void E(ref GameChampion Victim) {

        }
        public virtual void R(ref GameChampion Victim) {

        }
        public virtual void TakeDamage(ref GameChampion Attacker, int Amount, DamageType DamageType) {

        }
        public virtual void ProcessCombat() {
            var area = GetMyArea();
            if (this.PlayerStats.TeamColor == TeamColor.Blue) {
                // Focus on enemy minions. If they're all dead, check for towers.
                // If enemy tower, check for minions and enemies. If enemies, don't engage.
                if (area.RedMinions > 0) {
                    int slain = Utilities.RNG.Get(1, 4);
                    if (area.RedMinions < slain) {
                        slain = area.RedMinions;
                    }
                    area.RedMinions -= slain;
                    this.PlayerStats.Gold += slain * 25;
                    this.PlayerStats.Creeps += slain;
                } else {
                    if (area.RedTowerHealth > 0) {
                        if (area.BlueMinions > 0) {
                            if (!this.EnemiesInMyArea()) {
                                this.DamageTower();
                            }
                        }
                    }
                }
            } else {
                if (area.BlueMinions > 0) {
                    int slain = Utilities.RNG.Get(1, 4);
                    if (area.BlueMinions < slain) {
                        slain = area.BlueMinions;
                    }
                    area.BlueMinions -= slain;
                    this.PlayerStats.Gold += slain * 25;
                    this.PlayerStats.Creeps += slain;
                } else {
                    if (area.BlueTowerHealth > 0) {
                        if (area.RedMinions > 0) {
                            if (!this.EnemiesInMyArea()) {
                                this.DamageTower();
                            }
                        }
                    }
                }
            }
        }
        public virtual void DamageTower() {
            var area = this.GetMyArea();
            area.DamageTower(this);
        }
        public virtual void TryCreateWayPoint() {
            var focus = this.GetFocusLocation();
            var current = this.GetCurrentLocation();

            // Make sure that we haven't had a waypoint already made.
            if (focus.X == Point.FocusDNE && focus.Y == Point.FocusDNE) {

                // By default, focus on our current area.
                this.SetFocusLocation();

                // Are we currently located at spawn?
                if (current.X == Point.ShopXLocation) {
                    // Create a waypoint to the nexus area.
                    if (this.PlayerStats.TeamColor == TeamColor.Red) {
                        this.SetFocusLocation(4, 0);
                    } else {
                        this.SetFocusLocation(0, 4);
                    }
                    return;
                }

                // We're not at spawn. Get our current area.
                var area = this.GetMyArea();

                // Are we on the red team?
                if ()
            }
        }

        #region Location Methods
        public Point GetCurrentLocation() {
            return this.Location.Current;
        }
        public void SetCurrentLocation(int x = Point.PointParameterBypass, int y = Point.PointParameterBypass) {
            if (x == Point.PointParameterBypass) {
                x = GetCurrentLocation().X;
            }
            if (y == Point.PointParameterBypass) {
                y = GetCurrentLocation().Y;
            }
            this.Location.Current = new Point(x, y);
        }
        public Point GetFocusLocation() {
            return this.Location.Focus;
        }
        public void SetFocusLocation(int x = Point.PointParameterBypass, int y = Point.PointParameterBypass) {
            if (x == Point.PointParameterBypass) {
                x = this.Location.Focus.X;
            }
            if (y == Point.PointParameterBypass) {
                y = this.Location.Focus.Y;
            }
            this.Location.Focus = new Point(x, y);
        }
        public SummonersRift.Area GetMyArea() {
            return DataManager.Game.Map.Area[this.Location.Current.X, this.Location.Current.Y];
        }
        #endregion

        public string GetTeamColor() {
            return this.PlayerStats.TeamColor;
        }
        public int GetRole() {
            return this.PlayerStats.Role;
        }

        internal void ProcessMovement() {
            throw new NotImplementedException();
        }
    }
}
