using System;
using System.Collections.Generic;
using System.Text;
using ObserverStrategyTemplate.Models.Interfaces;

namespace ObserverStrategyTemplate.Models
{
    public class JsonFormatStrategy : IFormatStrategy
    {
        public string Format(string message, DateTime timestamp) => $"{{\"timestamp\":\"{timestamp:O}\",\"message\":\"{message}\"}}";
    }
}
