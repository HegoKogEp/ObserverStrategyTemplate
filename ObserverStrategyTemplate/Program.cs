using ObserverStrategyTemplate.Models;
using ObserverStrategyTemplate.ObserverData;

namespace ObserverStrategyTemplate
{
    public class Program
    {
        static void Main(string[] args)
        {
            var monitor = new EventMonitor();

            var consoleTextHandler = new ConsoleHandler(new TextFormatStrategy());
            var consoleJsonHandler = new ConsoleHandler(new JsonFormatStrategy());
            var fileHtmlHandler = new FileHandler("alerts.log", new HtmlFormatStrategy());

            monitor.OnMetricExceeded += consoleTextHandler.ProcessEvent;
            monitor.OnMetricExceeded += consoleJsonHandler.ProcessEvent;
            monitor.OnMetricExceeded += fileHtmlHandler.ProcessEvent;

            Console.WriteLine("Запуск системы мониторинга:\n");

            monitor.CheckMetric("CPU", 45.0, 80.0);
            monitor.CheckMetric("CPU", 92.5, 80.0);
            monitor.CheckMetric("Memory", 78.0, 60.0);
            monitor.CheckMetric("Network", 10.0, 50.0);

            Console.WriteLine("Смена стратегии:\n");

            fileHtmlHandler.SetStrategy(new JsonFormatStrategy());
            monitor.CheckMetric("Memory", 85.0, 60.0);

        }
    }
}
