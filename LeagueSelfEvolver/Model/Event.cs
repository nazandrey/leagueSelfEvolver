using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Conclusion> _goalConclusions;
        public ObservableCollection<Conclusion> _generalConclusions;

        public Event()
        {
            goalComment = "";
            generalComment = "";
            _goalConclusions = new ObservableCollection<Conclusion>();
            _generalConclusions = new ObservableCollection<Conclusion>();
        }

        public Event(XElement eventEl)
        {
            goalComment = eventEl.Element("GoalComment").Value;
            generalComment = eventEl.Element("GeneralComment").Value;
            _goalConclusions = ConclusionDictExtensions.InitConclusions(eventEl, "GoalConclusions");
            _generalConclusions = ConclusionDictExtensions.InitConclusions(eventEl, "GeneralConclusions");
        }        

        public ObservableCollection<Conclusion> GoalConclusions {
            get { return _goalConclusions; }
            set { _goalConclusions = value; }
        }

        public ObservableCollection<Conclusion> GeneralConclusions
        {
            get { return _generalConclusions; }
            set { _generalConclusions = value; }
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
                new XElement("GoalConclusions", _goalConclusions.ToXml()),
                new XElement("GeneralConclusions", _generalConclusions.ToXml())
            );
        }
    }
}
