using Crawler;
using Eventor.Integration;

namespace EventorResults.Console
{
    class Program
    {
      

        static void Main(string[] args)
        {
            var request = new EventorRequest(new AppSettings());

            var response = request.GetXml("starts/event?eventId=2007");           

            System.Console.WriteLine("Crawl starting...");
            
            var crawler = new EventorCrawler(new AppSettings());
            crawler.Crawl();

            System.Console.WriteLine("Crawl completed. Press Enter to exit...");
            System.Console.ReadLine();
        }
    }
}
