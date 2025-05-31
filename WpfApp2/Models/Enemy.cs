using System.Text.Json.Serialization;

namespace WpfApp2.Models
{
    public class Enemy
    {
        [JsonInclude]
        string name;
        [JsonInclude]
        string iconName;
        [JsonInclude]
        int baseLife;
        [JsonInclude]
        double lifeModifier;
        [JsonInclude]
        int baseGold;
        [JsonInclude]
        double goldModifier;
        [JsonInclude]
        double spawnChance;

        public Enemy(string name, string iconName, int baseLife, double lifeModifier, int baseGold, double goldModifier, double spawnChance)
        {
            this.name = name;
            this.iconName = iconName;
            this.baseLife = baseLife;
            this.lifeModifier = lifeModifier;
            this.baseGold = baseGold;
            this.goldModifier = goldModifier;
            this.spawnChance = spawnChance;
        }

        public string Name() => name;
        public string IconName() => iconName;
        public int BaseLife() => baseLife;
        public double LifeModifier() => lifeModifier;
        public int BaseGold() => baseGold;
        public double GoldModifier() => goldModifier;
        public double SpawnChance() => spawnChance;

    }
}
