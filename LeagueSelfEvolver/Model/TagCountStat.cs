using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSelfEvolver.Model
{
    class TagCountStat
    {
        public TagCountStat(string tag, int count)
        {
            Tag = tag;
            Count = count;
        }

        public string Tag { get; set; }
        public int Count { get; set; }
    }
}
