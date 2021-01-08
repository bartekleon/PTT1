using PT_Task2_Presentation_Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PT_Task2_Presentation
{
    public class MyemViewModel : INotifyPropertyChanged
    {

        public MyemViewModel()
        {
            FetchDataCommand = new RelayCommand(FetchData);
            SaveDataCommand = new RelayCommand(SendDataBack);
            IncreaseBookCountCommand = new RelayCommand(IncreaseBookCount,
                () => !(HighlightedEntry == null));
            DecreaseBookCountCommand = new RelayCommand(DecreaseBookCount,
                () => (!(HighlightedEntry == null)) && HighlightedEntry.BookCount > 0);
            AddEntryCommand = new RelayCommand(AddEntry, () => !(Model == null));
            DeleteEntryCommand = new RelayCommand(DeleteEntry, () => !(Entries == null));
            ConsoleTraceCommand = new RelayCommand(ListEntries);
        }

        #region command methods
        private void DeleteEntry()
        {
            string remember = HighlightedEntry.Title;
            DataSalvator.DeleteEntry(HighlightedEntry);
            Entries.Remove(HighlightedEntry);
            RaisePropertyChanged("Entries");
            RaisePropertyChanged("HighlightedEntry");
            ProvideFeedback("Deleted " + remember);

        }
        private void AddEntry()
        {
            DataSalvator.switchedOn = false;
            Entries.Add(DataSalvator.NewEntry());
            HighlightedEntry = Entries[Entries.Count - 1];

            RaisePropertyChanged("Entries");
            RaisePropertyChanged("HighlightedEntry");
            DecreaseBookCountCommand.RaiseCanExecuteChanged();
            DeleteEntryCommand.RaiseCanExecuteChanged();
            ProvideFeedback("An entry added");
            DataSalvator.switchedOn = true;
        }
        private void SendDataBack()
        {
            DataSalvator.SaveToDatabase();
            ProvideFeedback("Changes saved.");
        }
        private void FetchData()
        {
            DataSalvator.switchedOn = false;
            DataSalvator.FlushChanges();
            Model = new DataModel();
            AddEntryCommand.RaiseCanExecuteChanged();

            if (Entries.Count > 0)
            {
                HighlightedEntry = Entries[0];
                IncreaseBookCountCommand.RaiseCanExecuteChanged();
                DecreaseBookCountCommand.RaiseCanExecuteChanged();
                DeleteEntryCommand.RaiseCanExecuteChanged();
            };
            DataSalvator.switchedOn = true;
        }
        private void IncreaseBookCount()
        {
            HighlightedEntry.BookCount++;
            HighlightedEntry.AddSuchBook();
            RaisePropertyChanged("HighlightedEntry");
            DecreaseBookCountCommand.RaiseCanExecuteChanged();
        }
        private void DecreaseBookCount()
        {
            HighlightedEntry.BookCount--;
            HighlightedEntry.RemoveSuchBook();
            RaisePropertyChanged("HighlightedEntry");
            DecreaseBookCountCommand.RaiseCanExecuteChanged();
        }
        public void ListEntries()
        {
            string list = String.Empty;
            foreach (Entry entry in Entries)
            {
                list += entry.ToString();
                list += ";   ";
            }
            ProvideFeedback(list);
        }
        #endregion

        #region api
        public DataModel Model
        {
            get => m_Model;
            set
            {
                m_Model = value;
                Entries = new ObservableCollection<Entry>(value.Data);
            }
        }
        public ObservableCollection<Entry> Entries
        {
            get => m_Entries;
            set
            {
                m_Entries = value;
                RaisePropertyChanged();
                ProvideFeedback("Data loaded");
            }
        }
        public string FeedbackMessage
        {
            get => m_FeedbackMessage;
            set { }
        }
        public Entry HighlightedEntry
        {
            get => m_HighlightedEntry;
            set
            {
                m_HighlightedEntry = value;
                RaisePropertyChanged();
                DecreaseBookCountCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region command api
        public RelayCommand FetchDataCommand
        {
            get; private set;
        }
        public RelayCommand SaveDataCommand
        {
            get; private set;
        }
        public RelayCommand IncreaseBookCountCommand
        {
            get; private set;
        }
        public RelayCommand DecreaseBookCountCommand
        {
            get; private set;
        }
        public RelayCommand AddEntryCommand
        {
            get; private set;
        }
        public RelayCommand DeleteEntryCommand
        {
            get; private set;
        }
        public RelayCommand ConsoleTraceCommand
        {
            get; private set;
        }
        #endregion

        #region private fields
        private DataModel m_Model;
        private Entry m_HighlightedEntry;
        private ObservableCollection<Entry> m_Entries;
        private string m_FeedbackMessage = "Working!";
        #endregion

        #region inotify
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private void ProvideFeedback(string message)
        {
            m_FeedbackMessage = message;
            RaisePropertyChanged("FeedbackMessage");
        }
    }
}
