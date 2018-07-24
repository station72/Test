namespace SelTest.Model
{
    internal class SportEventMatch
    {
        public int DistanceOfTeam1 { get; set; }

        public int DistanceOfTeam2 { get; internal set; }

        public RecognizedSportEvent RecognizedSportEvent { get; set; }
    }
}
