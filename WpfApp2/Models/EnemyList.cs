using System.IO;
using System.Text.Json;

namespace WpfApp2.Models
{
    public class EnemyList
    {
        private List<Enemy> enemies;

        public EnemyList()
        {
            enemies = [];
        }

        public void AddEnemy(Enemy enemy)
        {
            if (enemies.Any(e => e.Name() == enemy.Name()))
                throw new InvalidOperationException($"Враг с именем '{enemy.Name()}' уже существует.");
            enemies.Add(enemy);
        }

        public void AddEnemy(string name, string iconName, int baseLife, double lifeModifier, int baseGold, double goldModifier, double spawnChance)
        {
            AddEnemy(new Enemy(name, iconName, baseLife, lifeModifier, baseGold, goldModifier, spawnChance));
        }

        public Enemy? GetEnemyByIndex(int id)
        {
            if (enemies.Count == 0 || enemies.Count <= id)
                return null;
            return enemies[id];
        }

        public Enemy? GetEnemyByName(string name)
        {
            return enemies.FirstOrDefault(enemy => enemy.Name().Equals(name));
        }

        public void DeleteEnemyByIndex(int id)
        {
            if (enemies.Count == 0 || enemies.Count <= id)
                return;
            enemies.RemoveAt(id);
        }

        public void DeleteEnemyByName(string name)
        {
            var d = GetEnemyByName(name);
            if (d != null)
                enemies.Remove(d);
        }

        public List<string> GetListOfEnemyNames()
        {
            return [.. enemies.Select(enemy => enemy.Name())];
        }

        public void SaveToJson(string path)
        {
            string jsonString = JsonSerializer.Serialize(enemies);
            File.WriteAllText(path, jsonString);
        }

        public void LoadToJson(string path)
        {
            var enemies = new List<Enemy>();
            string jsonFromFile = File.ReadAllText(path);
            JsonDocument doc = JsonDocument.Parse(jsonFromFile);

            foreach (JsonElement element in doc.RootElement.EnumerateArray())
            {
                string name = element.GetProperty("name").GetString() ?? string.Empty;
                string iconName = element.GetProperty("iconName").GetString() ?? string.Empty;
                int baseLife = element.GetProperty("baseLife").GetInt32();
                double lifeModifier = element.GetProperty("lifeModifier").GetDouble();
                int baseGold = element.GetProperty("baseGold").GetInt32();
                double goldModifier = element.GetProperty("goldModifier").GetDouble();
                double spawnChance = element.GetProperty("spawnChance").GetDouble();
                var enemy = new Enemy(name, iconName, baseLife, lifeModifier, baseGold, goldModifier, spawnChance);
                enemies.Add(enemy);
            }

            this.enemies = enemies;
        }
    }
}
