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
    class Conclusion : INotifyPropertyChanged
    {
        private string _tag;
        private string _description;

        public Conclusion()
        {
            _tag = "";
            _description = "";
        }

        public Conclusion(XElement eventEl)
        {
            _tag = eventEl.Element("Tag").Value;
            _description = eventEl.Element("Description").Value;
        }

        public Conclusion(string tag, string description)
        {
            _tag = tag;
            _description = description;
        }

        public string Tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                OnPropertyChanged("Tag");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public XElement ToXml()
        {
            return new XElement("Conclusion",
                new XElement("Tag", _tag),
                new XElement("Description", _description)
            );
        }
    }
}
