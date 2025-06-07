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

namespace WpfApp4.ViewModels
{
    internal class QuizListViewModel : INotifyPropertyChanged
    {

        #region Privat
        private ObservableCollection<QuizControl> controls;
        private ICommand? addCommand;
        #endregion
        
        public QuizListViewModel()
        {
            controls = new([.. Data.Quizzes
                .Select(q =>
                {
                    var qc = new QuizControl();
                    var vm = (QuizViewModel)qc.DataContext;
                    vm.Quiz = q;
                    vm.Deleted += OnDeleted;
                    return qc;
                })]);
        }

        #region Props
        public ObservableCollection<QuizControl> Controls
        {
            get => controls;
        }
        #endregion

        #region Commands
        public ICommand AddCommand => addCommand ??= new RelayCommand(_ =>
        {
            var qec = new QuizEditControl();
            var qevm = (QuizEditViewModel)qec.DataContext;
            qevm.QuizSaved += OnQuizSaved;
            MainViewModel.Instance.SetActiveControl(qec);
        });

        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Methods
        private void OnDeleted(QuizControl quizControl)
        {
            Controls.Remove(quizControl);
        }
        private void OnQuizSaved()
        {
            MainViewModel.Instance.SetActiveControl(new QuizListControl());
        }
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

    }
}
