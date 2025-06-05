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
        private ICommand? _goQuestionEdit;
        #endregion

        public QuestionListViewModel()
        {
            Questions = new(Data.Questions
                .Select(q =>
                {
                    var qc = new QuestionControl();
                    var vm = (QuestionViewModel)qc.DataContext;
                    vm.Question = q;
                    vm.Deleted += QuestionDeleted;
                    return qc;
                })
                .ToList());
        }

        #region Properties
        public ObservableCollection<QuestionControl> Questions { get; set; }
        #endregion

        #region Commands
        public ICommand GoQuestionEditCommand => _goQuestionEdit ??= new RelayCommand(p =>
        {
            QuestionEditControl questionEditControl = new();
            ((QuestionEditViewModel)questionEditControl.DataContext).Saved += QuestionsUpdate;
            MainViewModel.Instance.ActiveControl = questionEditControl;
        });
        #endregion

        #region Methods
        private void QuestionsUpdate()
        {
            MainViewModel.Instance.ActiveControl = new QuestionListControl();
        }

        private void QuestionDeleted(QuestionControl question)
        {
            Questions.Remove(question);
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
