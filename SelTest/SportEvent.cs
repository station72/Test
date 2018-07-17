using System;

namespace SelTest
{
    //добавить неактивность коэффициентов
    class SportEvent
    {
        public BookMaker BookMaker { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public float? Win1 { get; set; }

        public float? Draw { get; set; } //ничья

        public float? Win2 { get; set; }

        public float? Win1OrDraw { get; set; }

        public float? Win1OrWin2 { get; set; }

        public float? Win2OrDraw { get; set; }

        public float? ForaText1 { get; set; }

        public float? ForaTeam1 { get; set; }

        public float? ForaText2 { get; set; }

        public float? ForaTeam2 { get; set; }

        public float? TotalMoreText { get; set; }

        public float? TotalMore { get; set; }

        public float? TotalLessText { get; set; }

        public float? TotalLess { get; set; }

        public override string ToString()
        {
            return $"{Title} {Environment.NewLine} {Win1} {Draw} {Win2} {Win1OrDraw} {Win1OrWin2} {Win2OrDraw} {ForaText1} {ForaTeam1} {ForaText2} {ForaTeam2} {TotalMoreText} {TotalMore} {TotalLess}";
        }
    }
}
