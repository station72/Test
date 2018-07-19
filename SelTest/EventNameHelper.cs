using SelTest.Model;
using SelTest.Tokenizers;
using System;
using System.Text.RegularExpressions;

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


        object Tokenize(string eventTitle, Bookmaker bookMaker)
        {
            switch (bookMaker)
            {
                case Bookmaker.Fonbet:
                    return new FonbetTokenizer().Tokenize(eventTitle);
                case Bookmaker.Marathon:
                    return TokenizeMarathon(eventTitle);
                default:
                    throw new NotImplementedException("bookmaker is not implemented " + bookMaker.ToString());
            }
        }



        object TokenizeMarathon(string eventTitle)
        {
            throw new NotImplementedException();
        }
    }


}
