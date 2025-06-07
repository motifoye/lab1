using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Models;
using WpfApp4.Controls;
using System.Diagnostics;

namespace WpfApp4.ViewModels
{
    internal class PassingViewModel : INotifyPropertyChanged
    {
        #region Privat
        private Quiz? quiz;
        private ObservableCollection<Question> questions = [];
        private int currentIndex = 0;
        private Answer? selectedAnswer;
        private bool isChecking;
        private ICommand? nextQuestionCommand;
        private ICommand? checkCommand;
        private ICommand? endCommand;
        #endregion

        public PassingViewModel()
        {
            
        }

        #region Props
        public Quiz? Quiz
        {
            get
            {
                return quiz;
            }
            set
            {
                if (value == null)
                    ArgumentNullException.ThrowIfNull(value, nameof(value));
                if (quiz == value)
                    return;
                quiz = value;
                OnPropertyChanged(nameof(Quiz));
                Questions = new(Quiz!.Questions);
                OnPropertyChanged(nameof(IsLastQuestion));
            }
        }
        public ObservableCollection<Question> Questions
        {
            get => questions;
            private set
            {
                if (questions == value) return;
                questions = value;
                OnPropertyChanged(nameof(Questions));
                OnPropertyChanged(nameof(CurrentQuestion));
            }
        }
        public int CurrentIndex
        {
            get => currentIndex + 1;
        }
        public Question? CurrentQuestion
        {
            get => Questions.ElementAtOrDefault(currentIndex);
        }
        public Answer? SelectedAnswer
        {
            get => selectedAnswer;
            set
            {
                if (value == selectedAnswer) return;
                selectedAnswer = value;
                OnPropertyChanged(nameof(SelectedAnswer));
            }
        }
        public bool IsChecking
        {
            get => isChecking;
            set
            {
                if (value == isChecking) return;
                isChecking = value;
                OnPropertyChanged(nameof(IsChecking));
            }
        }
        public bool IsLastQuestion
        {
            get => CurrentIndex >= Questions.Count;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Commands
        public ICommand NextQuestionCommand => nextQuestionCommand ??= new RelayCommand(_ =>
        {
            ++currentIndex;
            OnPropertyChanged(nameof(CurrentIndex));
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(IsLastQuestion));
            IsChecking = false;
            Debug.WriteLine($"\n\nNext: {IsLastQuestion}\n\n");

        }, (obj) => {
            if (IsLastQuestion)
                return false;
            if (!IsChecking)
                return false;
            return true; 
        });
        public ICommand CheckCommand => checkCommand ??= new RelayCommand(_ =>
        {
            Debug.WriteLine($"\n\nCheck: {IsLastQuestion}\n\n");
            IsChecking = true;
        }, (obj) => {
            if (SelectedAnswer == null)
                return false;
            if (IsChecking)
                return false;
            return true; 
        });
        public ICommand EndCommand => endCommand ??= new RelayCommand(_ =>
        {
            MainViewModel.Instance.SetActiveControl(new QuizListControl());
        }, (obj) => {
            if (!IsLastQuestion)
                return false;
            if (!IsChecking)
                return false;
            return true; 
        });
        #endregion

        #region Methods
        private void Init()
        {

        }
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
        
        /************************************************************************/
        internal class AnswerAttempt(string text, bool isCorrect)
        {
            private readonly string text = text;
            private readonly bool isCorrect = isCorrect;

            internal string Text { get => text; }
            internal bool UserAnswer { get; set; }

            internal bool Result => UserAnswer == isCorrect;

        }
    }
}
