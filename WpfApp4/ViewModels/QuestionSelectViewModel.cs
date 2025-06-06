using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
    internal class QuestionSelectViewModel : INotifyPropertyChanged
    {
        #region Privat
        private ICollectionView questionCollection = CollectionViewSource.GetDefaultView(Data.Questions);
        private ICommand? selectCommand;
        #endregion

        #region Props
        public ICollectionView QuestionCollection
        {
            get => questionCollection;
        }
        #endregion

        #region Events
        public event Action<IList>? QuestionSelected;
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Commands
        public ICommand SelectCommand => selectCommand ??= new RelayCommand(obj =>
        {
            var lb = obj as ListBox;
            var items = lb!.SelectedItems;
            QuestionSelected?.Invoke(items);
        });
        #endregion

        #region Methods
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public List<Question> GetSelectedQuestions()
        {
            return [];
        }
        #endregion
    }
}
