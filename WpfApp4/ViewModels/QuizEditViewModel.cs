using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Controls;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
    internal class QuizEditViewModel : INotifyPropertyChanged
    {
        #region Privat
        private string? title;
        private ObservableCollection<QuestionControl> questionControls;
        private ICommand? selectQuestions;
        private ICommand? saveCommand;
        #endregion

        public QuizEditViewModel() 
        {
            questionControls = [];
        }

        #region Props
        public string? Title
        {
            get => title;
            set
            {
                if (title == value) return;
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public ObservableCollection<QuestionControl> QuestionControls
        {
            get => questionControls!;
            set
            {
                if (questionControls == null) return;
                questionControls = value;
                OnPropertyChanged(nameof(QuestionControls));
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action? QuizDeleted;
        public event Action? QuizSaved;
        #endregion

        #region Commands
        public ICommand SelectQuestions => selectQuestions ??= new RelayCommand(obj =>
        {
            if (obj is not UserControl uc) //QuizEditControl (this)
                return;
            var qsc = new QuestionSelectControl();
            var qsvm = (QuestionSelectViewModel)qsc.DataContext;
            qsvm.QuestionSelected += (res) =>
            {
                if (res != null)
                    foreach (var i in res)
                    {
                        var qc = new QuestionControl();
                        var qcvm = (QuestionViewModel)qc.DataContext;
                        qcvm.Question = (Question)i;
                        qcvm.Deleted += OnDeleted;
                        QuestionControls.Add(qc);
                    }
                MainViewModel.Instance.SetActiveControl(uc);
            };
            MainViewModel.Instance.SetActiveControl(qsc);
        });
        public ICommand SaveCommand => saveCommand ??= new RelayCommand(_ =>
        {
            if(Save())
                QuizSaved?.Invoke();
        });
        #endregion

        #region Methods
        private void OnDeleted(QuestionControl qc)
        {
            QuestionControls.Remove(qc);
        }
        private bool Save()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                MessageBox.Show("Введите название викторины.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (QuestionControls.Count < 1)
            {
                MessageBox.Show("Должен быть хотя бы один вопрос.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            var qstns = QuestionControls.Select(q => ((QuestionViewModel)q.DataContext).Question).ToList();

            Data.Quizzes.Add(new Quiz() { Title = Title, Questions = qstns, });

            return true;
        }
        protected void OnPropertyChanged(string name) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
