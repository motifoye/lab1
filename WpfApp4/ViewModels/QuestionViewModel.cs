using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Controls;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
    internal class QuestionViewModel : INotifyPropertyChanged
    {
        private Question? _question;
        private string _text = "";
        private ICommand? deleteCommand;
        private ICommand? lookCommand;

        #region Props
        public Question Question
        {
            get
            {
                ArgumentNullException.ThrowIfNull(_question);
                return _question;
            }
            set
            {
                if (_question == value) return;
                _question = value;
                OnPropertyChanged(nameof(Question));
                Text = value.Text!;
            }
        }
        public string Text
        {
            get => _text;
            private set
            {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        #endregion

        #region Commands
        public ICommand DeleteCommand => deleteCommand ??= new RelayCommand(obj =>
        {
            if (obj is not QuestionControl q)
                return;

            if (Question.Answers != null)
            {
                foreach (var a in Question.Answers)
                {
                    Data.Answers.Remove(a);
                }
            }
            Data.Questions.Remove(Question);

            Deleted?.Invoke(q);
        });
        public ICommand LookCommand => lookCommand ??= new RelayCommand(obj =>
        {
            var qc = new QuestionEditControl();
            var vm = (QuestionEditViewModel)qc.DataContext;
            vm.IsReadOnly = true;
            vm.QuestionText = Text;
            var lac = new ObservableCollection<AnswerControl>();
            //foreach (var a in Question.Answers!)
            for (int i = 0; i < Question.Answers!.Count; ++i)
            {
                var a = Question.Answers![i];
                var ac = new AnswerControl();
                var acvm = (AnswerViewModel)ac.DataContext;
                acvm.Text = a.Text!;
                acvm.IsCorrect = a.IsCorrect;
                acvm.IsReadOnly = true;
                lac.Add(ac);
            }
            vm.Answers = lac;
            MainViewModel.Instance.ActiveControl = qc;
        });
        #endregion

        public event Action<QuestionControl>? Deleted;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
