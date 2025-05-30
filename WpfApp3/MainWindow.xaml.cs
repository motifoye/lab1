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
        private CEnemy currentEnemy;
        private Player player;

        public MainWindow()
        {
            enemyManager = new EnemyManager();
            if (DataLoader.DataFilePath != null)
            {
                enemyManager.LoadEnemiesFromJson(DataLoader.DataFilePath);
                enemyManager.NormalizeChances();
            }

            InitializeComponent();

            InitializePlayer();
            SelectRandomEnemy();
            UpdateUI();
        }

        private void InitializePlayer()
        {
            player = new Player(
                new BigNumber("0"),       // золото
                new BigNumber("1"),       // урон
                1.1,                     // множитель урона
                new BigNumber("10"),      // стоимость улучшения
                1.5                      // множитель стоимости улучшения
            );
        }

        private void SelectRandomEnemy()
        {
            // Выбираем случайное число от 0 до 1
            Random rnd = new Random();
            double chance = rnd.NextDouble();

            currentEnemy = enemyManager.CreateEnemyByChance(chance);

            if (currentEnemy == null)
            {
                MessageBox.Show("Не удалось выбрать противника.");
                return;
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
            if (currentEnemy == null) return;
            currentEnemy.TakeDamage(player.Damage());
            EnemyHitsTb.Text = currentEnemy.HitPoints.ToString();

            if (currentEnemy.HitPoints.ToString() == "0")
            {
                player.gainGold(currentEnemy.Gold);
                PlayersGoldTb.Text = player.Gold().ToString();
                SelectRandomEnemy();
                UpdateUI();
            }
        }

        private void UpgradeBtn_Click(object sender, RoutedEventArgs e)
        {
            bool upgraded = player.upgrade();
            if (upgraded)
            {
                // Обновляем отображение игрока
                PlayersGoldTb.Text = player.Gold().ToString();
                PlayersDamageTb.Text = player.Damage().ToString();
                LevelTb.Text = player.GetLevel().ToString();
            }
            else
            {
                MessageBox.Show("Недостаточно золота для улучшения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            UpdateUpgradeButton();
        }

        private void UpdateUpgradeButton()
        {
            // Пробуем вычесть стоимость апгрейда из золота временно, чтобы проверить, хватает ли игроку золота
            BigNumber goldCopy = player.Gold().getBigNumber();
            BigNumber upgradeCost = player.UpgradeCost();

            try
            {
                goldCopy.Substruct(upgradeCost);
                UpgradeBtn.IsEnabled = true;
            }
            catch
            {
                UpgradeBtn.IsEnabled = false;
            }
        }


    }
}
