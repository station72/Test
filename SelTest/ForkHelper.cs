using System;
using System.Collections.Generic;
using System.Linq;
using SelTest.Model;

namespace SelTest
{
    //http://gidpostavkam.com/bukmekerskaya-vilka-i-vsyo-o-nej/
    internal class ForkHelper
    {
        //В = 1/К1 + 1/К2 + 1/К3
        //TODO: use IndexSportEvent
        public static List<Fork> GetFork(SportEvent sportEvent1, SportEvent sportEvent2)
        {
            if (sportEvent1 == null)
                throw new ArgumentNullException(nameof(sportEvent1));

            if (sportEvent2 == null)
                throw new ArgumentNullException(nameof(sportEvent2));

            //12 vs x
            //x vs 12+

            var res = new List<Fork>();

            //1 vs x2
            res.Add(Win1VSwin2OrDraw(sportEvent1, sportEvent2));

            //2 vs 1x
            res.Add(Win2VSWin1OrDraw(sportEvent1, sportEvent2));

            //1x vs 2
            res.Add(Win1OrDrawVSwin2(sportEvent1, sportEvent2));

            //2x vs 1
            res.Add(Win2OrDrawVSwin1(sportEvent1, sportEvent2));

            res = res.Where(u => u != null).ToList();

            return res;
        }


        static Fork Win1VSwin2OrDraw(SportEvent sportEvent1, SportEvent sportEvent2)
        {
            if (sportEvent1.Win1.HasValue && sportEvent1.Win2OrDraw.HasValue)
            {
                var win1VSwin2OrDraw = 1f / sportEvent1.Win1 + 1f / sportEvent2.Win2OrDraw;
                if (win1VSwin2OrDraw < 1)
                {
                    var res = new Fork
                    {
                        SportEvent1 = sportEvent1,
                        SportEvent2 = sportEvent2,
                        SportEvent1Field = nameof(SportEvent.Win1),
                        SportEvent2Field = nameof(SportEvent.Win2OrDraw)
                    };

                    return res;
                }
            }
            return null;
        }

        static Fork Win2VSWin1OrDraw(SportEvent sportEvent1, SportEvent sportEvent2)
        {
            if (sportEvent1.Win2.HasValue && sportEvent2.Win1OrDraw.HasValue)
            {
                var win2VSwin1OrDraw = 1f / sportEvent1.Win2 + 1f / sportEvent2.Win1OrDraw;
                if (win2VSwin1OrDraw < 1)
                {
                    return new Fork
                    {
                        SportEvent1 = sportEvent1,
                        SportEvent2 = sportEvent2,
                        SportEvent1Field = nameof(SportEvent.Win2),
                        SportEvent2Field = nameof(sportEvent2.Win1OrDraw)
                    };
                }
            }

            return null;
        }

        static Fork Win1OrDrawVSwin2(SportEvent sportEvent1, SportEvent sportEvent2)
        {
            if (sportEvent1.Win1OrDraw.HasValue && sportEvent2.Win2.HasValue)
            {
                var win1OrDrawVSwin2 = 1f / sportEvent1.Win1OrDraw + 1f / sportEvent2.Win2;
                if (win1OrDrawVSwin2 < 1)
                {
                    return new Fork
                    {
                        SportEvent1 = sportEvent1,
                        SportEvent2 = sportEvent2,
                        SportEvent1Field = nameof(SportEvent.Win1OrDraw),
                        SportEvent2Field = nameof(SportEvent.Win2)
                    };
                }
            }

            return null;
        }

        static Fork Win2OrDrawVSwin1(SportEvent sportEvent1, SportEvent sportEvent2)
        {
            if (sportEvent1.Win2OrDraw.HasValue && sportEvent2.Win1.HasValue)
            {
                var win2OrDrawVSwin1 = 1f / sportEvent1.Win2OrDraw + 1f / sportEvent2.Win2;
                if (win2OrDrawVSwin1 < 1)
                {
                    return new Fork
                    {
                        SportEvent1 = sportEvent1,
                        SportEvent2 = sportEvent2,
                        SportEvent1Field = nameof(SportEvent.Win2OrDraw),
                        SportEvent2Field = nameof(SportEvent.Win1)
                    };
                }
            }

            return null;
        }
    }
}
