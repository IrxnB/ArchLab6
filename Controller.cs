using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab6
{
    internal class Controller
    {
        private readonly ConsoleView _view;
        private readonly Lab6DbContext _db;
        private readonly Scraper _scraper;
        private static readonly List<Command> _commands = Enum
            .GetValues(typeof(Command))
            .Cast<Command>()
            .ToList();


        public Controller(ConsoleView view, Lab6DbContext db, Scraper scraper)
        {
            _view = view;
            _db = db;
            _scraper = scraper;
        }

        internal void ProcessCommand(Command? command)
        {
            if(command == null)
            {
                _view.PrintError("Wrong command");
                return;
            }

            switch (command)
            {
                case Command.ScrapeUrl:
                    {
                        var url = ReadUrl();

                        if(url == null)
                        {
                            _view.PrintError("Wrong URL");
                            return;
                        }

                        var fromDb = _db.Topics.Find(url);

                        if(fromDb != null)
                        {
                            _view.PrintMessage("Allready been scraped");
                            _view.PrintNewsTopic(fromDb);
                            return;
                        }

                        NewsTopic scrapedTopic;

                        try
                        {
                            scrapedTopic = _scraper.ScrapeNews(url);
                            _view.PrintNewsTopic(scrapedTopic);
                            scrapedTopic.Id = url;
                            _db.Topics.Add(scrapedTopic);
                            _db.SaveChanges();
                        }
                        catch(Exception ex)
                        {
                            _view.PrintError($"Failed to scrape {url}");
                        }




                        

                        break;
                    }
                case Command.GetScrapeList:
                    {
                        var urls = _db.Topics.Select(topic => topic.Id).ToList();
                        _view.PrintLines(urls);
                        break;
                    }
            }
        }

        internal Command? ReadCommand()
        {
            _commands.ForEach(command => { _view.PrintCommand(command); });
            _view.AskForValue("command Id");
            var fromConsole = Console.ReadLine();

            Command result;
            if(Enum.TryParse<Command>(fromConsole, out result))
                return result;

            return null;
        }

        internal string? ReadUrl()
        {
            _view.AskForValue("URL of a Topic to scrape");
            var fromConsole = Console.ReadLine();

            if(Uri.IsWellFormedUriString(fromConsole, UriKind.Absolute))
                return fromConsole;

            return null;

        }
        
    }
}
