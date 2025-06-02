using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Models;

namespace WpfApp4.Controls
{
    /// <summary>
    /// Логика взаимодействия для QuestionControl.xaml
    /// </summary>
    public partial class QuestionControl : UserControl, INotifyPropertyChanged
    {
        private string _questionText = "";
        private ICommand? _addAnswerCommand;
        private ICommand? _removeAnswerCommand;
        private MainWindow _main;

        public QuestionControl()
        {
            InitializeComponent();
            Answers = new ObservableCollection<AnswerViewModel>();
            // Добавим два пустых варианта по умолчанию
            Answers.Add(new AnswerViewModel { Text = "", IsCorrect = true });
            Answers.Add(new AnswerViewModel { Text = "", IsCorrect = false });
            DataContext = this;
        }
        
        public QuestionControl(MainWindow main) : this()
        {
            _main = main;
        }

        public ObservableCollection<AnswerViewModel> Answers { get; private set; }

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

        public ICommand AddAnswerCommand => _addAnswerCommand ??= new RelayCommand(_ =>
            Answers.Add(new AnswerViewModel { Text = "", IsCorrect = false }));

        public ICommand RemoveAnswerCommand => _removeAnswerCommand ??= new RelayCommand(param =>
        {
            if (param is AnswerViewModel answer)
            {
                Answers.Remove(answer);
            }
        });

        public void LoadQuestion(Question question)
        {
            ArgumentNullException.ThrowIfNull(question);
            QuestionText = question.Text;
            Answers.Clear();
            foreach (var a in question.Answers)
            {
                Answers.Add(new AnswerViewModel { Text = a.Text, IsCorrect = a.IsCorrect });
            }
        }

        public Question? GetQuestionOrNull()
        {
            if (string.IsNullOrWhiteSpace(QuestionText))
            {
                MessageBox.Show("Введите текст вопроса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            if (Answers.Count < 2)
            {
                MessageBox.Show("Должно быть не менее двух вариантов ответов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            if (!Answers.Any(a => a.IsCorrect))
            {
                MessageBox.Show("Должен быть выбран правильный ответ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            if (Answers.Any(a => string.IsNullOrWhiteSpace(a.Text)))
            {
                MessageBox.Show("Все варианты ответов должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            var answers = Answers.Select(a => new Answer(a.Text, a.IsCorrect)).ToList();
            return new Question(QuestionText, answers);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
