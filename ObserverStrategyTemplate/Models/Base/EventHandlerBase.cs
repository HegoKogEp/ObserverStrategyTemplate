using ObserverStrategyTemplate.Models.Interfaces;
using ObserverStrategyTemplate.ObserverData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverStrategyTemplate.Models.Base
{
    public abstract class EventHandlerBase
    {
        protected IFormatStrategy _formStrategy;

        protected EventHandlerBase(IFormatStrategy strategy)
        {
            _formStrategy = strategy;
        }

        public void SetStrategy(IFormatStrategy strategy)
        {
            _formStrategy = strategy;
        }

        public void ProcessEvent(MetricEventArgs e)
        {
            var message = FormatMessage(e.EventType, e.Data);
            SendMessage(message);
            LogResult();
        }

        protected string FormatMessage(string type, object data)
        {
            return _formStrategy.Format($"[ALERT] {type}: {data}", DateTime.Now);
        }

        protected abstract void SendMessage(string message);

        protected abstract void LogResult();
    }
}
