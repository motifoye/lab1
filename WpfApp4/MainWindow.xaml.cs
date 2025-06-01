﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp4.Controls;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MainControl();
        }

        private void Quizzes_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new QuizListControl();
        }

        private void Questions_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new QuestionListControl();
        }
    }
}