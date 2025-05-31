using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp2.Models;
using WpfApp3.Models;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private readonly EnemyManager enemyManager;
        private Enemy? currentEnemyTemplate;
        private CEnemy? currentEnemy;
        private Player player;
        private bool isBattleActive = true;

        public MainWindow()
        {
            enemyManager = new EnemyManager();
            if (DataLoader.DataFilePath != null)
            {
                enemyManager.LoadEnemiesFromJson(DataLoader.DataFilePath);
                enemyManager.NormalizeChances();
            }
            player = new Player(new BigNumber("0"), new BigNumber("2"), 1.1, new BigNumber("10"), 1.5);

            InitializeComponent();
            CostTb.Text = player.UpgradeCost().ToString();
            SelectRandomEnemy();
            UpdateUI();
        }

        private void SelectRandomEnemy()
        {
            Random rnd = new Random();
            double chance = rnd.NextDouble();

            currentEnemyTemplate = enemyManager.FindByChance(chance);
            if (currentEnemyTemplate != null)
            {
                currentEnemy = new CEnemy(currentEnemyTemplate, player.GetLevel());
                isBattleActive = true;
                NextBtn.IsEnabled = false;
                RepeatBtn.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Не удалось выбрать противника.");
            }
        }


        private void UpdateUI()
        {
            if (currentEnemy != null)
            {
                EnemyNameTbx.Text = currentEnemy.Name;
                EnemyHitsTb.Text = currentEnemy.HitPoints.ToString();
                GoldEnemyTb.Text = currentEnemy.Gold.ToString();

                // Загрузка изображения противника
                string iconFile = currentEnemy.IconName;
                string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", iconFile);

                if (!string.IsNullOrWhiteSpace(iconFile) && File.Exists(fullPath))
                {
                    EnemyImage.Source = new BitmapImage(new Uri(fullPath));
                }
                else
                {
                    // Показываем встроенную картинку из ресурсов
                    EnemyImage.Source = new BitmapImage(new Uri("pack://application:,,,/empty.png"));
                }
            }

            PlayersGoldTb.Text = player.Gold().ToString();
            PlayersDamageTb.Text = player.Damage().ToString();
            LevelTb.Text = player.GetLevel().ToString();

            UpdateUpgradeButton();
        }

        private void EnemyImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentEnemy == null || !isBattleActive) return;

            currentEnemy.TakeDamage(player.Damage());
            EnemyHitsTb.Text = currentEnemy.HitPoints.ToString();

            if (currentEnemy.HitPoints.ToString() == "0" || currentEnemy.HitPoints.IsNegative)
            {
                player.gainGold(new(currentEnemy.Gold.ToString()));
                PlayersGoldTb.Text = player.Gold().ToString();

                isBattleActive = false;
                NextBtn.IsEnabled = true;
                RepeatBtn.IsEnabled = true;
                UpdateUpgradeButton();
            }
        }

        private void UpgradeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (player.Gold().CompareTo(player.UpgradeCost()) < 0)
            {
                MessageBox.Show("Недостаточно золота для улучшения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool upgraded = player.upgrade();
            if (upgraded)
            {
                PlayersGoldTb.Text = player.Gold().ToString();
                PlayersDamageTb.Text = player.Damage().ToString();
                LevelTb.Text = player.GetLevel().ToString();
                CostTb.Text = player.UpgradeCost().ToString();
            }

            UpdateUpgradeButton();
        }

        private void UpdateUpgradeButton()
        {
            UpgradeBtn.IsEnabled = player.Gold().CompareTo(player.UpgradeCost()) >= 0;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectRandomEnemy();
            UpdateUI();
        }

        private void RepeatBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentEnemyTemplate != null)
            {
                currentEnemy = new CEnemy(currentEnemyTemplate, player.GetLevel());
                isBattleActive = true;
                NextBtn.IsEnabled = false;
                RepeatBtn.IsEnabled = false;
                UpdateUI();
            }
        }

    }
}
