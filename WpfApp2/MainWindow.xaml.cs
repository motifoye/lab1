using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Models;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EnemyList enemyList = new();
        private IconList iconList = new();
        public MainWindow()
        {
            InitializeComponent();

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.json");
            if (File.Exists(path))
            {
                enemyList.LoadToJson(path);
                lb1.ItemsSource = enemyList.GetListOfEnemyNames();
            }

            ShowIcons();

        }

        private void ShowIcons()
        {
            foreach (var icon in iconList.GetIcons())
            {
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(icon.FullPath(), UriKind.Absolute)),
                    Width = 64,
                    Height = 64,
                    Margin = new Thickness(5)
                };
                lb2.Items.Add(image);
            }

        }

        private void ELoad(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new()
            {
                FileName = $"data",
                DefaultExt = ".json",
                Filter = "JSON|*.json",
            };
            if (dlg.ShowDialog(this) == true)
            {
                enemyList.LoadToJson(dlg.FileName);
                lb1.ItemsSource = enemyList.GetListOfEnemyNames();
            }
        }

        private void ESave(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new()
            {
                FileName = $"data",
                DefaultExt = ".json",
                Filter = "JSON|*.json"
            };
            if (dlg.ShowDialog(this) == true)
                enemyList.SaveToJson(dlg.FileName);
        }

        private void ESelectEnemy(object sender, SelectionChangedEventArgs e)
        {
            var s = sender as ListBox;
            var name = s?.SelectedValue?.ToString() ?? string.Empty;
            var enemy = enemyList.GetEnemyByName(name);
            if (enemy == null)
            {
                return;
            }
            tbxEnemyName.Text = enemy.Name();
            tbxIconName.Text = enemy.IconName();
            tbxBaseLife.Text = enemy.BaseLife().ToString();
            tbxLifeModifier.Text = enemy.LifeModifier().ToString();
            tbxBaseGold.Text = enemy.BaseGold().ToString();
            tbxGoldModifier.Text = enemy.GoldModifier().ToString();
            tbxSpawnChance.Text = enemy.SpawnChance().ToString();

            string iconFile = enemy.IconName();
            string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", iconFile);

            if (!string.IsNullOrWhiteSpace(iconFile) && File.Exists(fullPath))
            {
                iconImg.Source = new BitmapImage(new Uri(fullPath));
            }
            else
            {
                // Показываем встроенную картинку из ресурсов
                iconImg.Source = new BitmapImage(new Uri("pack://application:,,,/empty.png"));
            }
        }

        private void ESelectIcon(object sender, SelectionChangedEventArgs e)
        {
            if (lb2.SelectedItem is Image selectedImage &&
                selectedImage.Source is BitmapImage bmp)
            {
                string iconFileName = System.IO.Path.GetFileName(bmp.UriSource.LocalPath);
                tbxIconName.Text = iconFileName;
                iconImg.Source = selectedImage.Source;
            }
        }

        private void EAdd(object sender, RoutedEventArgs e)
        {
            string name = tbxEnemyName.Text.Trim();
            string icon = tbxIconName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите имя врага.");
                return;
            }

            if (string.IsNullOrWhiteSpace(icon))
            {
                MessageBox.Show("Введите имя иконки.");
                return;
            }

            if (!int.TryParse(tbxBaseLife.Text, out int blife))
            {
                MessageBox.Show("Некорректное значение для 'Base Life'.");
                return;
            }

            if (!double.TryParse(tbxLifeModifier.Text, out double mlife))
            {
                MessageBox.Show("Некорректное значение для 'Life Modifier'.");
                return;
            }

            if (!int.TryParse(tbxBaseGold.Text, out int bgold))
            {
                MessageBox.Show("Некорректное значение для 'Base Gold'.");
                return;
            }

            if (!double.TryParse(tbxGoldModifier.Text, out double mgold))
            {
                MessageBox.Show("Некорректное значение для 'Gold Modifier'.");
                return;
            }

            if (!double.TryParse(tbxSpawnChance.Text, out double chance))
            {
                MessageBox.Show("Некорректное значение для 'Spawn Chance'.");
                return;
            }

            if (chance < 1 || chance > 100)
            {
                MessageBox.Show("'Spawn Chance' должно быть от 1 до 100.");
                return;
            }

            if (enemyList.GetEnemyByName(name) != null)
            {
                MessageBox.Show("Враг с таким именем уже существует.");
                return;
            }

            Enemy newEnemy = new(name, icon, blife, mlife, bgold, mgold, chance);
            enemyList.AddEnemy(newEnemy);

            lb1.ItemsSource = null;
            lb1.ItemsSource = enemyList.GetListOfEnemyNames();
        }

        private void ERemove(object sender, RoutedEventArgs e)
        {
            string? selectedName = lb1.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(selectedName))
            {
                MessageBox.Show("Выберите врага для удаления.");
                return;
            }


            lb1.SelectedItem = null;
            lb1.ItemsSource = null;
            enemyList.DeleteEnemyByName(selectedName);
            lb1.ItemsSource = enemyList.GetListOfEnemyNames();
        }
    }
}