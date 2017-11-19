using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeagueSelfEvolver.Model
{
    class Event : INotifyPropertyChanged
    {
        private string goalComment;
        private string generalComment;
        private string goalConclusions;
        private string generalConclusions;

        public Event()
        {
            goalComment = "";
            generalComment = "";
            goalConclusions = "";
            generalConclusions = "";
        }
        public Event(XElement eventEl)
        {
            goalComment = eventEl.Element("GoalComment").Value;
            generalComment = eventEl.Element("GeneralComment").Value;
            goalConclusions = eventEl.Element("GoalConclusions").Value;
            generalConclusions = eventEl.Element("GeneralConclusions").Value;
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
        public string GoalConclusions
        {
            get { return goalConclusions; }
            set
            {
                goalConclusions = value;
                OnPropertyChanged("GoalConclusions");
            }
        }
        public string GeneralConclusions
        {
            get { return generalConclusions; }
            set
            {
                generalConclusions = value;
                OnPropertyChanged("GeneralConclusions");
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
                new XElement("GoalConclusions", GoalConclusions),
                new XElement("GeneralConclusions", GeneralConclusions)
            );
        }
    }
}
