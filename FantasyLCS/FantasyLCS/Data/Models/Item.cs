using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyLCS.Data.Models
{
    class Item
    {
        // Offensive Stats
        public int ArmorPenetration { get; set; }
        public int AttackDamage { get; set; }
        public decimal AttackSpeed { get; set; }
        public decimal CriticalStrikeChance { get; set; }
        public decimal CriticalStrikeDamage { get; set; }
        public decimal LifeStea { get; set; }

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
