using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.ViewModels
{
    internal class QuizViewModel : INotifyPropertyChanged
    {
        #region Privat
        private string? title;
        #endregion

        #region Props
        public string Title
        {
            get => title ?? "Title empty";
            set
            {
                if (value == title) return;
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
