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
        private bool _isEdit = false;

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

        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                if (_isEdit != value)
                {
                    _isEdit = value;
                    OnPropertyChanged(nameof(IsEdit));
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
