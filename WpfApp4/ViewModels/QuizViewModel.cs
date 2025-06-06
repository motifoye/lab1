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

namespace WpfApp4.ViewModels
{
    internal class QuizViewModel : INotifyPropertyChanged
    {
        #region Privat
        private string? title;
        private ObservableCollection<Question>? questionList;
        #endregion

        #region Props
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
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Commands
        
        #endregion

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
