using SelTest.Workers;
using System;
using System.Threading.Tasks;

namespace SelTest
{
    partial class Program
    {
        //TODO: установить русский язык (добавить возможность выбора)
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

        }

        static async Task MainAsync(string[] args)
        {
            var fonbet = new FonBetWorker();
            await fonbet.Start();

            //var marathon = new MarathonWorker();
            //marathon.Start();

            ////TODO: make thread safe
            //var agr = EventAggregatorContainer.Instance.EventAggregators;

            Console.WriteLine("Press any key to close app");
            Console.ReadKey();
        }
    }
}
