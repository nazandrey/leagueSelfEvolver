﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeagueSelfEvolver.ViewModel
{
    class GoalViewModel : INotifyPropertyChanged
    {
        private const string DataPath = @"/DataModel/GoalData.xml";

        public GoalViewModel()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            XDocument xDoc = XDocument.Load(appPath + DataPath);
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
        public string Title { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
