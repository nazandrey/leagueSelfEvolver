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
