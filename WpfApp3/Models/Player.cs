using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp3.Models
{
    public class Player
    {
        private int lvl;
        private BigNumber gold;
        private BigNumber damage;
        private double damageModifier;
        private BigNumber upgradeCost;
        private double upgradeModifier;

        public Player(BigNumber gold, BigNumber damage, double damageModifier,
                       BigNumber upgradeCost, double upgradeModifier)
        {
            this.lvl = 1;
            this.gold = gold.getBigNumber();
            this.damage = damage.getBigNumber();
            this.damageModifier = damageModifier;
            this.upgradeCost = upgradeCost.getBigNumber();
            this.upgradeModifier = upgradeModifier;
        }

        public int GetLevel()
        {
            return lvl;
        }

        public BigNumber Gold()
        {
            return gold.getBigNumber();
        }

        public BigNumber Damage()
        {
            return damage.getBigNumber();
        }

        public double DamageModifier()
        {
            return damageModifier;
        }

        public BigNumber UpgradeCost()
        {
            return upgradeCost.getBigNumber();
        }

        public bool gainGold(BigNumber additionalGold)
        {
            gold.Add(additionalGold.getBigNumber());
            return true;
        }

        public bool upgrade()
        {
            BigNumber goldCopy = gold.getBigNumber();
            BigNumber costCopy = upgradeCost.getBigNumber();

            goldCopy.Substruct(costCopy); // если gold < cost, число будет некорректным

            if (IsNegative(goldCopy))
                return false;

            gold.Substruct(upgradeCost);
            lvl++;

            double scale = upgradeModifier * lvl * 1.05;
            upgradeCost.Multiply(scale);

            double dmgScale = damageModifier * (1.0 + lvl / 10.0);
            damage.Multiply(dmgScale);

            return true;
        }

        private bool IsNegative(BigNumber bnum)
        {
            string str = bnum.getStringNumber();
            return str.StartsWith("-");
        }
    }

}
