using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SelTest.Helpers;
using SelTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SelTest.Workers
{
    internal class MarathonWorker
    {
        private IWebDriver _browser;

        public void Start()
        {
            var options = new ChromeOptions();
            //options.AddArguments("--headless");
            _browser = new ChromeDriver(options);
            var url = @"https://www.marathonbet.com/su/live/26418";
            _browser.Navigate().GoToUrl(url);

            var content = _browser.FindElement(By.CssSelector(".sport-category-content"));
            var contentHTML = content.GetAttribute("innerHTML");

            var parser = new HtmlParser();
            var document = parser.Parse(contentHTML);

            EventFromDocument(document);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            _browser.Quit();
        }

        private void EventFromDocument(IHtmlDocument document)
        {
            var categoryContainers = document.QuerySelectorAll(".category-container");
            Console.WriteLine("Category containers = " + categoryContainers.Length);
            foreach (var categoryContainer in categoryContainers)
            {
                Console.WriteLine("---");
                var categoryContent = categoryContainer.QuerySelector("div.category-content");

                var tBodys = categoryContent.QuerySelectorAll("div>table.foot-market>tbody");
                var tBodyContents = tBodys.Skip(1).ToArray();

                foreach (var tBodyContent in tBodyContents)
                {
                    ParseTBodyContent(tBodyContent);
                }
            }
        }

        private void ParseTBodyContent(IElement tBodyContent)
        {
            var eventTitle = tBodyContent.GetAttribute("data-event-name");
            var eventId = tBodyContent.GetAttribute("data-event-treeid");

            var tr = tBodyContent.QuerySelectorAll("tr").First();
            var tds = tr.Children.ToArray();

            var sportEvent = GetEvent(tds, eventTitle, eventId);

            var teams = sportEvent.Title.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
            if (teams.Length != 2)
            {
                throw new Exception("teams.Length = " + teams.Length + " INFO:" + sportEvent.ToString());
            }
            EventAggregatorContainer.Instance.AddEvent(teams[0], teams[1], sportEvent);
            Console.WriteLine(sportEvent);
        }

        private SportEvent GetEvent(IEnumerable<IElement> allTds, string eventTitle, string eventId)
        {
            var sportEvent = new SportEvent()
            {
                Title = eventTitle,
                Id = eventId
            };

            sportEvent.Win1 = GetText(allTds, 2);
            sportEvent.Draw = GetText(allTds, 3);
            sportEvent.Win2 = GetText(allTds, 4);
            sportEvent.Win1OrDraw = GetText(allTds, 5);
            sportEvent.Win1OrWin2 = GetText(allTds, 6);
            sportEvent.Win2OrDraw = GetText(allTds, 7);

            sportEvent.ForaText1 = GetForaText(allTds, 8);
            sportEvent.ForaTeam1 = GetText(allTds, 8);

            sportEvent.ForaText2 = GetForaText(allTds, 9);
            sportEvent.ForaTeam2 = GetText(allTds, 9);

            sportEvent.TotalLessText = GetToalText(allTds, 10);
            sportEvent.TotalLess = GetText(allTds, 10);

            sportEvent.TotalMoreText = GetToalText(allTds, 11);
            sportEvent.TotalMore = GetText(allTds, 11);

            return sportEvent;
        }

        float? GetToalText(IEnumerable<IElement> allTds, int skip)
        {
            var rawTotalLessText = allTds.Skip(skip).First().TextContent;
            if (rawTotalLessText.Contains("—"))
            {
                return null;
            }

            var startIndex = rawTotalLessText.IndexOf('(');
            if (startIndex == -1)
            {
                throw new Exception("startIndex = -1; skip = " + skip);
            }
            startIndex += 1;

            var stopIndex = rawTotalLessText.IndexOf(')');
            if (stopIndex == -1)
            {
                throw new Exception("stopIndex = -1; skip = " + skip);
            }

            var totalLessText = rawTotalLessText.Substring(startIndex, stopIndex - startIndex);
            return FloatHelper.ParseToNullable(totalLessText);
        }

        float? GetForaText(IEnumerable<IElement> allTds, int skip)
        {
            var foraRawText = allTds.Skip(skip).First().TextContent;
            if (foraRawText.Contains("—")) //т.е. ставки нет
            {
                return null;
            }

            var startIndex = foraRawText.IndexOf('(');
            if (startIndex == -1)
            {
                //log
                throw new Exception("startIndex = -1; skip = " + skip);
            }
            startIndex += 1;
            var endIndex = foraRawText.IndexOf(')');
            //не отображает минус в дебаге
            var foraText = foraRawText.Substring(startIndex, endIndex - startIndex);
            return FloatHelper.ParseToNullable(foraText);
        }

        float? GetText(IEnumerable<IElement> allTds, int skip)
        {
            var td = allTds.Skip(skip).First();
            var span = td.QuerySelector("span");
            return FloatHelper.ParseToNullable(span.TextContent);
        }
    }

}
