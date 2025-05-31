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
        public long Gold { get; private set; }
        public string IconName { get; private set; }

        public CEnemy(Enemy template, int lvl = 1)
        {
            Name = template.Name();

            int baseLife = template.BaseLife();
            double lifeModifier = template.LifeModifier();
            HitPoints = new BigNumber((baseLife * (1 + (lvl - 1) * lifeModifier)).ToString());

            int baseGold = template.BaseGold();
            double goldModifier = template.GoldModifier();
            Gold = (long)Math.Ceiling(baseGold * (1 + lvl * goldModifier));

            IconName = template.IconName();
        }

        public void TakeDamage(BigNumber damage)
        {
            HitPoints.Subtract(damage);
        }
    }

}
