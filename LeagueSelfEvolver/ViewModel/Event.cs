using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeagueSelfEvolver.ViewModel
{
    class Event : INotifyPropertyChanged
    {
        private string goalComment;
        private string generalComment;
        private string resolution;

        public Event(XElement eventEl)
        {
            goalComment = eventEl.Element("GoalComment").Value;
            generalComment = eventEl.Element("GeneralComment").Value;
            resolution = eventEl.Element("Resolution").Value;
        }

        public string GoalComment
        {
            get { return goalComment; }
            set
            {
                goalComment = value;
                OnPropertyChanged("GoalComment");
            }
        }
        public string GeneralComment
        {
            get { return generalComment; }
            set
            {
                generalComment = value;
                OnPropertyChanged("GeneralComment");
            }
        }
        public string Resolution
        {
            get { return resolution; }
            set
            {
                resolution = value;
                OnPropertyChanged("Resolution");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public XElement ToXml()
        {
            return new XElement("Event",
                new XElement("GoalComment", GoalComment),
                new XElement("GeneralComment", GeneralComment),
                new XElement("Resolution", Resolution)
            );
        }
    }
}
