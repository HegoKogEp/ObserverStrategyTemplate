using System;
using System.Collections.Generic;
using System.Text;
using ObserverStrategyTemplate.Models.Base;
using ObserverStrategyTemplate.Models.Interfaces;

namespace ObserverStrategyTemplate.Models
{
    public class ConsoleHandler : EventHandlerBase
    {
        public ConsoleHandler(IFormatStrategy strategy) : base(strategy) { }


        protected override void SendMessage(string message)
        {
            Console.WriteLine(message);
        }

        protected override void LogResult()
        {
            Console.WriteLine("[Console Log]: Notification sent");
        }
    }
}
