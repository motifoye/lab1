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
        private ContentControl _activeControl;
        private ICommand? _goHome;
        private ICommand? _goQuestions;
        private ICommand? _goQuizs; 
        #endregion

        private MainViewModel()
        {
            _activeControl = new MainControl();
        }

        #region Properties
        public static MainViewModel Instance => _instance ??= new MainViewModel();
        public ContentControl ActiveControl
        {
            get => _activeControl;
            private set
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
        public ICommand GoHomeCommand => _goHome ??= 
            new RelayCommand(_ => SetActiveControl(new MainControl()));

        public ICommand GoQuizsCommand => _goQuizs ??= 
            new RelayCommand(_ => SetActiveControl(new QuizListControl()));

        public ICommand GoQuestionsCommand => _goQuestions ??= 
            new RelayCommand(_ => SetActiveControl(new QuestionListControl()));
        #endregion

        public void SetActiveControl(ContentControl contentControl)
        {
            ActiveControl = contentControl;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
