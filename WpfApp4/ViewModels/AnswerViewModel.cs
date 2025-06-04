using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Controls;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
    internal class AnswerViewModel : INotifyPropertyChanged
    {
        private ICommand? remove;
        private string _text = "";
        private bool _isCorrect = false;

        #region Props
        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public bool IsCorrect
        {
            get => _isCorrect;
            set
            {
                if (_isCorrect != value)
                {
                    _isCorrect = value;
                    OnPropertyChanged(nameof(IsCorrect));
                }
            }
        }

        public bool IsEdit { get; set; } = false; 
        #endregion

        public ICommand RemoveCommand => remove ??= new RelayCommand(obj =>
        {
            if (obj is AnswerControl control)
                Delete?.Invoke(control);
        });

        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action<AnswerControl>? Delete;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
