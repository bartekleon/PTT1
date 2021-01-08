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
            ConsoleTraceCommand = new RelayCommand(DoConsoleTrace, () => !(Entries == null));
            SaveDataCommand = new RelayCommand(SendDataBack);
            IncreaseBookCountCommand = new RelayCommand(IncreaseBookCount,
                () => !(HighlightedEntry == null));
            DecreaseBookCountCommand = new RelayCommand(DecreaseBookCount,
                () => ((!(HighlightedEntry == null)) && HighlightedEntry.BookCount > 0));
        }

        #region command methods
        private void DoConsoleTrace()
        {
            foreach (Entry entry in Entries)
            {
                Console.WriteLine(entry.Title);
                Console.WriteLine();
            }
        }
        private void SendDataBack()
        {
            DataSalvator.SaveToDatabase();
            m_FeedbackMessage = "Changes saved.";
            RaisePropertyChanged("FeedbackMessage");
        }
        private void FetchData()
        {
            DataSalvator.switchedOn = false;
            Model = new DataModel();
            ConsoleTraceCommand.RaiseCanExecuteChanged();

            if (Entries.Count > 0)
            {
                HighlightedEntry = Entries[0];
                IncreaseBookCountCommand.RaiseCanExecuteChanged();
                DecreaseBookCountCommand.RaiseCanExecuteChanged();
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
                m_FeedbackMessage = "Loaded!";
                RaisePropertyChanged();
                RaisePropertyChanged("FeedbackMessage");
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
        public RelayCommand ConsoleTraceCommand
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
    }
}
