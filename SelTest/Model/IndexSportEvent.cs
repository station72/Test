using System.Collections.Generic;

namespace SelTest.Model
{
    class IndexSportEvent
    {
        private Dictionary<string, float?> Dict { get; set; } = new Dictionary<string, float?>();

        private float? GetByKey(string key)
        {
            if (Dict.ContainsKey(key))
            {
                return Dict[key];
            }

            return null;
        }

        private void SetByKey(string key, float? value)
        {
            Dict[key] = value;
        }

        public BookMaker BookMaker{ get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public float? Win1
        {
            get { return GetByKey(nameof(Win1)); }
            set { SetByKey(nameof(Win1), value); }
        }

        public float? Draw
        {
            get { return GetByKey(nameof(Draw)); }
            set { SetByKey(nameof(Draw), value); }
        }

        public float? Win2
        {
            get { return GetByKey(nameof(Win2)); }
            set { SetByKey(nameof(Win2), value); }
        }

        public float? Win1OrDraw
        {
            get { return GetByKey(nameof(Win1OrDraw)); }
            set { SetByKey(nameof(Win1OrDraw), value); }
        }

        public float? Win1OrWin2
        {
            get { return GetByKey(nameof(Win1OrWin2)); }
            set { SetByKey(nameof(Win1OrWin2), value); }
        }

        public float? Win2OrDraw
        {
            get { return GetByKey(nameof(Win2OrDraw)); }
            set { SetByKey(nameof(Win2OrDraw), value); }
        }

        public float? ForaText1
        {
            get { return GetByKey(nameof(ForaText1)); }
            set { SetByKey(nameof(ForaText1), value); }
        }

        public float? ForaTeam1
        {
            get { return GetByKey(nameof(ForaTeam1)); }
            set { SetByKey(nameof(ForaTeam1), value); }
        }

        public float? ForaText2
        {
            get { return GetByKey(nameof(ForaText2)); }
            set { SetByKey(nameof(ForaText2), value); }
        }

        public float? ForaTeam2
        {
            get { return GetByKey(nameof(ForaTeam2)); }
            set { SetByKey(nameof(ForaTeam2), value); }
        }

        public float? TotalMoreText
        {
            get { return GetByKey(nameof(TotalMoreText)); }
            set { SetByKey(nameof(TotalMoreText), value); }
        }

        public float? TotalMore
        {
            get { return GetByKey(nameof(TotalMore)); }
            set { SetByKey(nameof(TotalMore), value); }
        }

        public float? TotalLessText
        {
            get { return GetByKey(nameof(TotalLessText)); }
            set { SetByKey(nameof(TotalLessText), value); }
        }

        public float? TotalLess
        {
            get { return GetByKey(nameof(TotalLess)); }
            set { SetByKey(nameof(TotalLess), value); }
        }
    }
}
