using SelTest.Model;
using SelTest.Workers;


namespace SelTest
{
    partial class Program
    {
        //TODO: установить русский язык (добавить возможность выбора)
        static void Main(string[] args)
        {
            var fonbet = new FonBetWorker();
            fonbet.Start();

            //var marathon = new MarathonWorker();
            //marathon.Start();

            var agr = EventAggregatorContainer.Instance.EventAggregators;
        }

    }
}
