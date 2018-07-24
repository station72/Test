using SelTest.Model;
using System;
using System.Collections.Generic;

namespace SelTest
{
    class EventNameHelper
    {
        //TODO: если не удается сопоставить команды, то нужно смотреть счет и время матча

        //FONBET

        //до 17
        //Калуга U17 — Ротор-Волгоград U17
        //АФ Тамбов U17 — Динамо Брянск U17

        //резерв
        //Робина Сити (р) — Ранавей Бэй (р)

        //до 21
        //Сабах U21 — Малакка Юн U21

        //юнайтед
        //Селангор Юн U21 — Селангор U21

        //Челтенхэм Т — Бирмингем С


        //MARATHON

        //до 17
        //Калуга до 17 - Ротор до 17
        //Тамбов-АФ до 17 - Динамо Брянск до 17

        //резерв
        //Робина Сити-резерв - Раневей Бэй-резерв

        //до 21
        //Сабах до 21 - Мелака Юнайтед до 21

        //юнайтед
        //Селангор Юнайтед до 21 - Селангор до 21

        //Челтнэм Таун - Бирмингем Сити

        public static double DiceCoefficient(string strA, string strB)
        {
            var setA = new HashSet<string>();
            var setB = new HashSet<string>();

            for (int i = 0; i < strA.Length - 1; ++i)
                setA.Add(strA.Substring(i, 2));

            for (int i = 0; i < strB.Length - 1; ++i)
                setB.Add(strB.Substring(i, 2));

            var intersection = new HashSet<string>(setA);
            intersection.IntersectWith(setB);

            return (2.0 * intersection.Count) / (setA.Count + setB.Count);
        }

        public static bool HaveSameTokens(HashSet<string> a, HashSet<string> b)
        {
            if (a.Count == 0 && b.Count == 0)
                return true;

            if ((a.Count - b.Count) != 0)
                return false;

            return a.SetEquals(b);
        }

        public static int Levenshtein(string a, string b)
        {
            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }

            int cost;
            int[,] d = new int[a.Length + 1, b.Length + 1];
            int min1;
            int min2;
            int min3;

            for (var i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (var i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (var i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (var j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = Convert.ToInt32(!(a[i - 1] == b[j - 1]));

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];
        }

        public static string GetEventId(SportEvent sportEvent)
        {
            if (string.IsNullOrWhiteSpace(sportEvent.Id))
            {
                throw new ArgumentException(nameof(sportEvent));
            }

            var prefix = sportEvent.Bookmaker.ToString();
            return $"{prefix}_{sportEvent.Id}";
        }

    }


}
