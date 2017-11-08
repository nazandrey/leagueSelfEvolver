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

        public GoalModel()
        {
            XDocument xDoc = XDocument.Load(DataPath);
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
        }

        public ObservableCollection<Event> EventList { get; set; }
        public string Title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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

            StorageFile file = await StorageFile.GetFileFromPathAsync(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, DataPath));
            using (Stream fileStream = await file.OpenStreamForWriteAsync())
            {
                xDoc.Save(fileStream);
            }
        }
    }
}
