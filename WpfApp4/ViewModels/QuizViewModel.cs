using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Controls;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
    internal class QuizViewModel : INotifyPropertyChanged
    {
        #region Privat
        private Quiz? quiz;
        private string? title;
        private ObservableCollection<Question>? questionList;
        private ICommand? deleteCommand;
        #endregion

        #region Props
        public Quiz Quiz
        {
            get
            {
                ArgumentNullException.ThrowIfNull(quiz);
                return quiz;
            }
            set
            {
                if (quiz == value) return;
                quiz = value;
                OnPropertyChanged(nameof(Quiz));
                Title = quiz!.Title!;
                QuestionList = new(quiz!.Questions!);
            }
        }
        public string Title
        {
            get => title ?? "Title empty";
            private set
            {
                if (value == title) return;
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public ObservableCollection<Question> QuestionList
        {
            get => questionList ??= [];
            private set
            {
                if (value == questionList) return;
                questionList = value;
                OnPropertyChanged(nameof(QuestionList));
            }
        }
        #endregion

        #region Events
        public event Action<QuizControl>? Deleted;
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Commands
        public ICommand DeleteCommand => deleteCommand ??= new RelayCommand(obj =>
        {
            if (obj is not QuizControl q)
                return;

            Data.Quizzes.Remove(Quiz);

            Deleted?.Invoke(q);
        });
        #endregion

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
