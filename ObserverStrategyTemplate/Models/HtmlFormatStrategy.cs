using System;
using System.Collections.Generic;
using System.Text;
using ObserverStrategyTemplate.Models.Interfaces;

namespace ObserverStrategyTemplate.Models
{
    public class HtmlFormatStrategy : IFormatStrategy
    {
        public string Format(string message, DateTime timestamp) => $"<div class=\"alert\"><time>{timestamp: yyyy:MM:dd HH:mm}</time><p>{message}</p></div>";
    }
}
