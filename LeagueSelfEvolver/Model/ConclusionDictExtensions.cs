using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeagueSelfEvolver.Model
{
    static class ConclusionDictExtensions
    {
        public static ObservableCollection<Conclusion> InitConclusions(XElement eventEl, string conclusionTypeStr)
        {
            return new ObservableCollection<Conclusion>(eventEl.Element(conclusionTypeStr).Elements("Conclusion").Select(conclusion => { return new Conclusion(conclusion); }));
        }

        public static IEnumerable<XElement> ToXml(this ObservableCollection<Conclusion> conclusionDict)
        {
            return conclusionDict.Select((conclusion) =>
            {
                return conclusion.ToXml();
            });
        }
    }
}
