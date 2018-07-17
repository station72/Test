using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SelTest
{
    class Program
    {
        //TODO: установить русский язык (добавить возможность выбора)
        static void Main(string[] args)
        {
            //var fonbet = new FonBetWorker();
            //fonbet.Start();

            var marathon = new Marathon();
            marathon.Start();
        }

        class Marathon
        {
            private IWebDriver _browser;

            public void Start()
            {
                ChromeOptions options = new ChromeOptions();
                //options.AddArguments("--headless");
                _browser = new ChromeDriver(options);
                var url = @"https://www.marathonbet.com/su/live/26418";
                _browser.Navigate().GoToUrl(url);

                //WebDriverWait wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(5));
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("tbody")));

                var tables = _browser.FindElements(By.CssSelector("table.foot-market"));
                var table = tables.First(); //table

                var tBodys = table.FindElements(By.CssSelector("tbody")).Take(2).ToArray();
                var titleTBody = tBodys.First();
                var infoTBody = tBodys.Skip(1).First();

                var title = infoTBody.GetAttribute("data-event-name");
                var eventId = infoTBody.GetAttribute("data-event-treeid");

                //var allTds = infoTBody.FindElements(By.CssSelector("tbody>tr:first-of-type>td"));
                var allTds = infoTBody.FindElement(By.CssSelector("tbody>tr:first-of-type"));
                var deb = allTds.GetAttribute("innerHTML");

                //var tdsList = new List<string>();
                //foreach (var td in allTds)
                //{
                //    tdsList.Add(td.GetAttribute("innerHTML"));
                //}

                //var sportEvent = GetEvent(allTds);
                //sportEvent.Title = title;

                //Console.WriteLine(sportEvent);

                _browser.Quit();
            }

            private SportEvent GetEvent(ReadOnlyCollection<IWebElement> allTds)
            {
                var sportEvent = new SportEvent();

                sportEvent.Win1 = GetText(allTds, 2);
                sportEvent.Draw = GetText(allTds, 3);
                sportEvent.Win2 = GetText(allTds, 4);
                sportEvent.Win1OrDraw = GetText(allTds, 5);
                sportEvent.Win1OrWin2 = GetText(allTds, 6);
                sportEvent.Win2OrDraw = GetText(allTds, 7);

                var foraText1 = allTds.Skip(8).First().Text;
                sportEvent.ForaText1 = FloatHelper.ParseToNullable(foraText1);

                sportEvent.ForaTeam1 = GetText(allTds, 8);

                var foraText2 = allTds.Skip(8).First().Text;
                sportEvent.ForaText2 = FloatHelper.ParseToNullable(foraText2);

                sportEvent.ForaTeam2 = GetText(allTds, 9);
                sportEvent.TotalLess = GetText(allTds, 10);
                sportEvent.TotalMore = GetText(allTds, 11);

                return sportEvent;
            }

            float? GetText(IEnumerable<IWebElement> allTds, int skip)
            {
                var td = allTds.Skip(skip).First();
                var span = td.FindElement(By.TagName("span"));
                return FloatHelper.ParseToNullable(span.Text);
            }
        }

        class FonBetWorker
        {
            private IWebDriver _browser;

            public void Start()
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--headless");
                _browser = new ChromeDriver(options);
                var url = @"https://www.fonbet.ru/#!/live/football";
                _browser.Navigate().GoToUrl(url);

                WebDriverWait wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(5));
                //поменял namespace
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("tbody")));
                var tBodys = _browser.FindElements(By.TagName("tbody"));
                var firstTBody = tBodys.Skip(0).First();
                var rows = firstTBody.FindElements(By.TagName("tr"));
                var testRow = rows.Skip(1).First();//skip headers
                var tds = testRow.FindElements(By.TagName("td"));
                try
                {
                    while (true)
                    {
                        var e = EventGetter(testRow);
                        Console.WriteLine(e.ToString());
                        Console.WriteLine("Press any key to refresh");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    _browser.Quit();
                    throw;
                }
            }


            SportEvent EventGetter(IWebElement row)
            {
                var result = new SportEvent();
                var tds = row.FindElements(By.TagName("td"));

                //title
                var titleTd = tds.Skip(1).First();
                var div = titleTd.FindElement(By.ClassName("table__match-title"));
                result.Title = div.Text;

                //1
                var win1Td = tds.Skip(2).First();
                result.Win1 = FloatHelper.ParseToNullable(win1Td.Text);

                //x
                var draw = tds.Skip(3).First();
                result.Draw = FloatHelper.ParseToNullable(draw.Text);

                //2
                var win2 = tds.Skip(4).First();
                result.Win2 = FloatHelper.ParseToNullable(win2.Text);

                //1x
                var win1OrDraw = tds.Skip(5).First();
                result.Win1OrDraw = FloatHelper.ParseToNullable(win1OrDraw.Text);

                //12
                var win1OrWin2 = tds.Skip(6).First();
                result.Win1OrWin2 = FloatHelper.ParseToNullable(win1OrWin2.Text);

                //x2
                var win2OrDraw = tds.Skip(7).First();
                result.Win2OrDraw = FloatHelper.ParseToNullable(win2OrDraw.Text);

                var foraText1 = tds.Skip(8).First();
                result.ForaText1 = FloatHelper.ParseToNullable(foraText1.Text);

                var foraTeam1 = tds.Skip(9).First();
                result.ForaTeam1 = FloatHelper.ParseToNullable(foraTeam1.Text);

                var foraText2 = tds.Skip(10).First();
                result.ForaText2 = FloatHelper.ParseToNullable(foraText2.Text);

                var foraTeam2 = tds.Skip(11).First();
                result.ForaTeam2 = FloatHelper.ParseToNullable(foraTeam2.Text);

                var total = tds.Skip(12).First();
                result.Total = FloatHelper.ParseToNullable(total.Text);

                var totalMore = tds.Skip(13).First();
                result.Total = FloatHelper.ParseToNullable(totalMore.Text);

                var totalLess = tds.Skip(14).First();
                result.TotalLess = FloatHelper.ParseToNullable(totalLess.Text);

                return result;
            }
        }

    }



    //добавить неактивность коэффициентов
    class SportEvent
    {
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

        public float? Total { get; set; }

        public float? TotalMore { get; set; }

        public float? TotalLess { get; set; }

        public override string ToString()
        {
            return $"{Title} {Environment.NewLine} {Win1} {Draw} {Win2} {Win1OrDraw} {Win1OrWin2} {Win2OrDraw} {ForaText1} {ForaTeam1} {ForaText2} {ForaTeam2} {Total} {TotalMore} {TotalLess}";
        }
    }

    public static class FloatHelper
    {
        public static float? ParseToNullable(string s)
        {
            float fRes;
            if (!float.TryParse(s, out fRes))
                return null;

            return fRes;
        }
    }
}
