using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab6
{
    internal class ConsoleView
    {
        internal void PrintCommand(Command command) =>
            Console.WriteLine($"{((int)command)}. {command}");

        internal void PrintNewsTopic(NewsTopic topic)
        {
            Console.WriteLine($"Topic: {topic.Title}, Created at: {topic.CreatedAt}, Views: {topic.ViewsCount}, ImageSrc: {topic.ImageSource}");
            Console.WriteLine($"Tags: {String.Join(", ", topic.Tags)}");
        }

        internal void AskForValue(string valueName)
        {
            Console.WriteLine($"Enter {valueName}");
        }

        internal void PrintLines(List<string> lines) => lines.ForEach(line => Console.WriteLine(line));

        internal void PrintError(string msg) => Console.WriteLine(msg);

        internal void PrintMessage(string msg) => Console.WriteLine(msg);
    }
}
