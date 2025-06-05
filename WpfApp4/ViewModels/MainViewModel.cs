using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Controls;

namespace WpfApp4.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        #region Pirvats
        private static MainViewModel? _instance;
        private ContentControl _activeControl = new MainControl();
        private ICommand? _goHome;
        private ICommand? _goQuestions;
        private ICommand? _goQuizs; 
        #endregion

        private MainViewModel() { }

        #region Properties
        public static MainViewModel Instance => _instance ??= new MainViewModel();
        public ContentControl ActiveControl
        {
            get => _activeControl;
            set
            {
                if (value != _activeControl)
                {
                    _activeControl = value;
                    OnPropertyChanged(nameof(ActiveControl));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand GoHomeCommand => _goHome ??= new RelayCommand(_ =>
        {
            ActiveControl = new MainControl();
        });
        public ICommand GoQuestionsCommand => _goQuestions ??= new RelayCommand(_ =>
        {
            ActiveControl = new QuestionListControl();
        });public ICommand GoQuizsCommand => _goQuizs ??= new RelayCommand(_ =>
        {
            ActiveControl = new QuizListControl();
        });
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
