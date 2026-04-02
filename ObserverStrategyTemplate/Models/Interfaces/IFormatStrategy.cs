using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverStrategyTemplate.Models.Interfaces
{
    public interface IFormatStrategy
    {
        string Format(string message, DateTime timestamp);
    }
}
