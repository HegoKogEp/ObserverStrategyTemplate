using System;
using System.Collections.Generic;
using System.Text;
using ObserverStrategyTemplate.Models.Interfaces;

namespace ObserverStrategyTemplate.Models
{
    public class TextFormatStrategy : IFormatStrategy
    {
        public string Format(string message, DateTime timestamp) => $"{timestamp: yyyy-MM-dd HH:mm} {message}";
    }
}
