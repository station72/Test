using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SelTest.Helpers;
using SelTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SelTest.Workers
{
    class FonBetWorker
    {
        private IWebDriver _browser;

        public void Start()
        {
            var options = new ChromeOptions();
            //options.AddArguments("--headless");
            _browser = new ChromeDriver(options);
            var url = @"https://www.fonbet.ru/#!/live/football";
            _browser.Navigate().GoToUrl(url);

            var wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(5));
            //поменял namespace
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("tbody")));

            var table = _browser.FindElement(By.CssSelector("table.table"));
            var tableHtml = "<table class=\"table\">" + table.GetAttribute("innerHTML") + "</table>";

            var parser = new HtmlParser();
            var document = parser.Parse(tableHtml);

            var tBodys = document.QuerySelectorAll("table.table>tbody.table__body");
            foreach (var tBody in tBodys)
            {
                Console.WriteLine("---");
                var rows = tBody.QuerySelectorAll("tr.table__row"); //line

                foreach (var row in rows.Skip(1)) //skip header
                {
                    var skipElement = row.QuerySelector("td>div._is_child ");
                    if (skipElement != null)
                    {
                        continue;
                    }

                    var tds = row.Children;
                    var sportEvent = EventGetter(tds);
                    var teams = sportEvent.Title.Split(new string[] { " — " }, StringSplitOptions.RemoveEmptyEntries);
                    if (teams.Length != 2)
                    {
                        throw new Exception("team.Length = " + teams.Length);
                    }
                    EventAggregatorContainer.Instance.AddEvent(teams[0], teams[1], sportEvent);
                    Console.WriteLine(sportEvent);
                }
            }
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
            _browser.Quit();
        }


        float? GetValue(IEnumerable<IElement> tds, int skip)
        {
            var td = tds.Skip(skip).First();
            return FloatHelper.ParseToNullable(td.TextContent);
        }

        private SportEvent EventGetter(IEnumerable<IElement> tds)
        {
            var result = new SportEvent();

            //title
            var titleTd = tds.Skip(1).First();
            var div = titleTd.QuerySelector(".table__match-title");
            result.Id = div.QuerySelector("span").TextContent;
            var titleEl = div.QuerySelector("a.table__match-title-text");
            if (titleEl == null)
            {
                Console.WriteLine("Title element is null");
            }
            result.Title = titleEl?.TextContent;

            result.Win1 = GetValue(tds, 2);
            result.Draw = GetValue(tds, 3);
            result.Win2 = GetValue(tds, 4);
            result.Win1OrDraw = GetValue(tds, 5);
            result.Win1OrWin2 = GetValue(tds, 6);
            result.Win2OrDraw = GetValue(tds, 7);
            result.ForaText1 = GetValue(tds, 8);
            result.ForaTeam1 = GetValue(tds, 9);
            result.ForaText2 = GetValue(tds, 10);
            result.ForaTeam2 = GetValue(tds, 11);
            result.TotalMoreText = GetValue(tds, 12);
            result.TotalLessText = GetValue(tds, 12);
            result.TotalMore = GetValue(tds, 13);
            result.TotalLess = GetValue(tds, 14);

            return result;
        }
    }

}
