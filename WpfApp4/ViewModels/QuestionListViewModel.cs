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
                    vm.Deleted += OnDeleted;
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
            ((QuestionEditViewModel)questionEditControl.DataContext).Saved += OnUpdated;
            MainViewModel.Instance.SetActiveControl(questionEditControl);
        });
        #endregion

        #region Methods
        private void OnUpdated()
        {
            MainViewModel.Instance.SetActiveControl(new QuestionListControl());
        }

        private void OnDeleted(QuestionControl question)
        {
            Questions.Remove(question);
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
