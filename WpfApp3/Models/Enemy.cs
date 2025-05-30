using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Models;

namespace WpfApp3.Models
{
    public class CEnemy
    {
        public string Name { get; private set; }
        public BigNumber HitPoints { get; private set; }
        public BigNumber Gold { get; private set; }
        public string IconName { get; private set; }

        // Конструктор создания противника из шаблона
        public CEnemy(Enemy template)
        {
            Name = template.Name();

            // Рассчитываем текущее здоровье с модификатором
            int baseLife = template.BaseLife();
            double lifeModifier = template.LifeModifier();
            HitPoints = new BigNumber((baseLife * lifeModifier).ToString());

            // Рассчитываем золото, которое получит игрок после победы
            int baseGold = template.BaseGold();
            double goldModifier = template.GoldModifier();
            Gold = new BigNumber((baseGold * goldModifier).ToString());

            // Иконка - путь к изображению
            IconName = template.IconName();
        }

        // Метод для нанесения урона (пример)
        public void TakeDamage(BigNumber damage)
        {
            HitPoints.Substruct(damage);
            if (HitPoints.getStringNumber() == "0")
            {
                // Логика смерти противника, если нужно
            }
        }

        // Можно добавить другие методы, например, получение текущего здоровья или золота
    }

}
