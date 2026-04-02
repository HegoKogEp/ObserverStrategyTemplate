using System;
using System.Collections.Generic;
using System.Text;
using ObserverStrategyTemplate.Models.Base;
using ObserverStrategyTemplate.Models.Interfaces;

namespace ObserverStrategyTemplate.Models
{
    public class FileHandler : EventHandlerBase
    {
        private string _filePath { get; }
        public FileHandler(string filePath, IFormatStrategy strategy) : base(strategy)
        {
            _filePath = filePath;
        }
        protected override string FormatMessage(string type, object data)
        {
            return _formStrategy.Format($"[ALERT] {type}: {data}", DateTime.Now);
        }

        protected override void SendMessage(string message)
        {
            File.AppendAllText(_filePath, message + Environment.NewLine);
        }

        protected override void LogResult()
        {
            File.AppendAllText(_filePath, "[File Log]: notification sent" + Environment.NewLine);
        }
    }
}
