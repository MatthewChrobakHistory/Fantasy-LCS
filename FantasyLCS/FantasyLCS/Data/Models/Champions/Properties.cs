namespace FantasyLCS.Data.Models.Champions
{
    public class Properties
    {
        public class GameLocation
        {
            public Point Current { get; set; }
            public Point Focus { get; set; }
        }

        public class BaseChampionStats
        {
            public string ChampionName { get; set; }
            public string Primary { get; set; }
            public string Secondary { get; set; }
            public PerLevelStat Health { get; set; }
            public PerLevelStat HealthRegeneration { get; set; }
            public PerLevelStat Mana { get; set; }
            public PerLevelStat ManaRegeneration { get; set; }
            public PerLevelStat Energy { get; set; }
            public PerLevelStat AttackDamage { get; set; }
            public PerLevelStat AttackSpeed { get; set; }
            public PerLevelStat Armor { get; set; }
            public PerLevelStat MagicResist { get; set; }
            public int MovementSpeed { get; set; }
        }

        public class GameChampionStats
        {
            public string PlayerName { get; set; }
            public string TeamColor { get; set; }
            public int MaxHealth { get; set; }
            public int MaxMana { get; set; }
            public int MaxEnergy { get; set; }
            public int Level { get; set; }
            public int Creeps { get; set; }
            public int Gold { get; set; }
            public int Kills { get; set; }
            public int Deaths { get; set; }
            public int Assists { get; set; }
            public int Experience { get; set; }
            public int Role { get; set; }
        }

        public class Stats
        {
            // Offensive Stats
            public int ArmorPenetration { get; set; }
            public int AttackDamage { get; set; }
            public decimal AttackSpeed { get; set; }
            public decimal CriticalStrikeChance { get; set; }
            public decimal CriticalStrikeDamage { get; set; }
            public decimal LifeSteal { get; set; }

            // Defensive Stats
            public int Armor { get; set; }
            public int Health { get; set; }
            public int HealthRegeneration { get; set; }
            public int MagicResistance { get; set; }

            // Ability
            public int AbilityPower { get; set; }
            public decimal CooldownReduction { get; set; }
            public int MagicPenetration { get; set; }
            public int Mana { get; set; }
            public int ManaRegeneration { get; set; }
            public int SpellVamp { get; set; }

            // Utility
            public int MovementSpeed { get; set; }
        }
    }
}
