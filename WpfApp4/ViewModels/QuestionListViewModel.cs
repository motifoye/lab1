using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Controls;

namespace WpfApp4.ViewModels
{
    internal class QuestionListViewModel : INotifyPropertyChanged
    {
        #region Privats
        private ICommand? _goQuestion;
        #endregion

        public QuestionListViewModel()
        {

        }

        #region Properties
        #endregion

        #region Commands
        public ICommand GoQuestionCommand => _goQuestion ??= new RelayCommand(_ =>
        {
            MainViewModel.Instance.ActiveControl = new QuestionControl();
        });
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
