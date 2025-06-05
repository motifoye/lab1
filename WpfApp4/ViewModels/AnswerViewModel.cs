using System.ComponentModel;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Controls;

namespace WpfApp4.ViewModels
{
    internal class AnswerViewModel : INotifyPropertyChanged
    {
        private ICommand? remove;
        private string _text = "";
        private bool _isCorrect = false;
        private bool _isReadOnly = false;
        private bool _isAttempt = false;

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

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }
        
        public bool IsAttempt
        {
            get => _isAttempt;
            set
            {
                if (_isAttempt != value)
                {
                    _isAttempt = value;
                    OnPropertyChanged(nameof(IsAttempt));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand RemoveCommand => remove ??= new RelayCommand(obj =>
        {
            if (obj is AnswerControl control)
                Deleted?.Invoke(control);
        });
        #endregion

        public event Action<AnswerControl>? Deleted;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
