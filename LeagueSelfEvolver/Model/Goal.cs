using LeagueSelfEvolver.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace LeagueSelfEvolver.Model
{
    class GoalModel : INotifyPropertyChanged
    {
        private const string DataPath = @"Data\GoalData.xml";
        private readonly StorageFolder RoamingFolder = ApplicationData.Current.RoamingFolder;
        private ObservableCollection<TagCountStat> _tagCountStat;

        public GoalModel()
        {
            XDocument xDoc;
            try
            {
                xDoc = XDocument.Load(Path.Combine(RoamingFolder.Path, DataPath));
            }
            catch (IOException ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
            {
                xDoc = XDocument.Load(DataPath);
            }
            XNode goalNode = xDoc.Nodes().FirstOrDefault();
            if (goalNode != null)
            {
                XElement goalEl = goalNode as XElement;
                if (goalEl != null)
                {
                    Title = goalEl.Element("Title").Value;
                    XElement xmlEventList = goalEl.Element("EventList");
                    EventList = new ObservableCollection<Event>();
                    if (xmlEventList != null)
                    {
                        foreach (XElement eventEl in goalEl.Element("EventList").Elements("Event"))
                        {
                            EventList.Add(new Event(eventEl));
                        }
                    }
                }
            }
            AddEventCommand = new RelayCommand(() => AddEvent());
            DeleteEventCommand = new RelayCommand<Event>((eventItem) => DeleteEvent(eventItem));
            SaveToXmlCommand = new RelayCommand(() => SaveToXml());
        }

        public string Title { get; set; }
        public ObservableCollection<Event> EventList { get; set; }
        public ObservableCollection<TagCountStat> TagCountStat
        {
            get { return InitTagCountStat(); }
            set { _tagCountStat = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public RelayCommand SaveToXmlCommand { get; private set; }
        public RelayCommand AddEventCommand { get; private set; }
        public RelayCommand<Event> DeleteEventCommand { get; private set; }


        private void AddEvent()
        {
            EventList.Add(new Event());
        }

        private void DeleteEvent(Event item)
        {
            if (item != null)
            {
                EventList.Remove(item);
            }            
        }

        public async void SaveToXml()
        {
            XDocument xDoc = new XDocument(
                new XElement("Goal",
                    new XElement("Title", Title),
                    new XElement("EventList", EventList.Select<Event, XElement>((eventItem) => {
                        return eventItem.ToXml();
                    }))
                )
            );

            using (Stream fileStream = await RoamingFolder.OpenStreamForWriteAsync(DataPath, CreationCollisionOption.ReplaceExisting))
            {
                xDoc.Save(fileStream);
            }
        }

        public void AddRow()
        {
            EventList.Add(new Event());
        }

        public void RemoveRow(Event eventItem)
        {
            if (eventItem != null)
            {
                EventList.Remove(eventItem);
            }
        }

        private ObservableCollection<TagCountStat> InitTagCountStat()
        {
            var tagCountStat = new List<TagCountStat>();
            foreach (var eventItem in EventList)
            {
                foreach (var conclusion in eventItem.GoalConclusions.Concat(eventItem.GeneralConclusions))
                {
                    if (conclusion.Tag == "") continue;
                    var tagCount = tagCountStat.FirstOrDefault((t) => t.Tag == conclusion.Tag);
                    if (tagCount == null)
                    {
                        tagCountStat.Add(new TagCountStat(conclusion.Tag, 1));
                    }
                    else
                    {
                        tagCount.Count += 1;
                    }
                }
            }
            var tagCountStatAsObserbableCollection = new ObservableCollection<TagCountStat>(tagCountStat.OrderByDescending(tagCount => tagCount.Count));
            return tagCountStatAsObserbableCollection;
        }
    }
}
