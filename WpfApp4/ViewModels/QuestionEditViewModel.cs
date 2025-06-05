using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Controls;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
    internal class QuestionEditViewModel : INotifyPropertyChanged
    {
        private string _questionText = "";
        private ICommand? _addAnswerCommand;
        private ICommand? _removeAnswerCommand;
        private ICommand? _saveCommand;

        public QuestionEditViewModel()
        {
            Answers = [NewAnswerControl(true), NewAnswerControl(false)];
        }

        #region Props
        public ObservableCollection<AnswerControl> Answers { get; set; } = [];

        public string QuestionText
        {
            get => _questionText;
            set
            {
                if (_questionText != value)
                {
                    _questionText = value;
                    OnPropertyChanged(nameof(QuestionText));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand AddAnswerCommand => _addAnswerCommand ??= new RelayCommand(_ =>
            Answers.Add(NewAnswerControl()));

        public ICommand SaveCommand => _saveCommand ??= new RelayCommand(_ =>
        {
            if (Save())
                Saved?.Invoke();
        });
        #endregion

        #region Methods
        private AnswerControl NewAnswerControl(bool isCorrect = false)
        {
            var ac = new AnswerControl();
            var vm = (AnswerViewModel)ac.DataContext;
            vm.IsCorrect = isCorrect;
            vm.IsEdit = true;
            vm.Delete += (obj) =>
            {
                if (obj is AnswerControl control)
                    Answers.Remove(control);
            };

            return ac;
        }

        private bool Save()
        {
            if (string.IsNullOrWhiteSpace(QuestionText))
            {
                MessageBox.Show("Введите текст вопроса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (Answers.Count < 2)
            {
                MessageBox.Show("Должно быть не менее двух вариантов ответов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!Answers.Any(a => ((AnswerViewModel)a.DataContext).IsCorrect))
            {
                MessageBox.Show("Должен быть выбран правильный ответ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (Answers.Any(a => string.IsNullOrWhiteSpace(((AnswerViewModel)a.DataContext).Text)))
            {
                MessageBox.Show("Все варианты ответов должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            var answers = Answers.Select(a => 
            {
                var vm = a.DataContext as AnswerViewModel;
                var answer = new Answer() { Text = vm!.Text, IsCorrect = vm!.IsCorrect };
                Data.Answers.Add(answer);
                return answer;
            }).ToList();
            var question =  new Question() { Text = QuestionText, Answers = answers };
            Data.Questions.Add(question);
            return true;
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        public event Action? Saved;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
