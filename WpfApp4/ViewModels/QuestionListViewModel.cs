using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Controls;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
    internal class QuestionListViewModel : INotifyPropertyChanged
    {
        #region Privats
        private QuestionListControl? _QuestionListControlInstance;
        private QuestionControl? _questionControl;
        private ICommand? _goQuestion;
        #endregion

        public QuestionListViewModel()
        {
            Questions = new ObservableCollection<Question>();
            QuestionsVM = new();
        }

        #region Properties
        public ObservableCollection<Question> Questions { get;  set; }
        public ObservableCollection<QuestionViewModel> QuestionsVM { get;  set; }
        #endregion

        #region Commands
        public ICommand GoQuestionCommand => _goQuestion ??= new RelayCommand(p =>
        {
            _QuestionListControlInstance = p as QuestionListControl;
            _questionControl = new QuestionControl();
            var dc = _questionControl.DataContext as QuestionViewModel;
            dc.Save += AddQuestion;
            MainViewModel.Instance.ActiveControl = _questionControl;
            QuestionsVM.Add(dc);
        });
        #endregion

        #region Methods
        private void AddQuestion(Question question)
        {
            Questions.Add(question);
            MainViewModel.Instance.ActiveControl = _QuestionListControlInstance;
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
