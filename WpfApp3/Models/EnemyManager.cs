using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Models;

namespace WpfApp3.Models
{
    public class EnemyManager
    {
        private EnemyList enemyList;
        private Dictionary<Enemy, double> normalizedChances;


        public EnemyManager()
        {
            enemyList = new EnemyList();
            normalizedChances = [];
        }

        // Добавление шаблона в список
        public void AddEnemy(Enemy enemy)
        {
            enemyList.AddEnemy(enemy);
        }

        // Нормализация шансов появления
        public void NormalizeChances()
        {
            var enemies = enemyList.GetListOfEnemies();
            double sum = enemies.Sum(e => e.SpawnChance());
            if (sum == 0) return;

            normalizedChances.Clear();
            foreach (var e in enemies)
                normalizedChances[e] = e.SpawnChance() / sum;

        }

        // Поиск шаблона по вероятности
        public Enemy? FindByChance(double chance)
        {
            double sum = 0;
            foreach (var kvp in normalizedChances)
            {
                sum += kvp.Value;
                if (sum >= chance)
                    return kvp.Key;
            }
            return null;
        }

        // Создание готового противника из шаблона по шансу
        public CEnemy? CreateEnemyByChance(double chance)
        {
            Enemy template = FindByChance(chance);
            if (template != null)
                return new CEnemy(template);

            return null;
        }

        public void LoadEnemiesFromJson(string path)
        {
            enemyList.LoadToJson(path);
        }

        public void SaveEnemiesToJson(string path)
        {
            enemyList.SaveToJson(path);
        }
    }
}
