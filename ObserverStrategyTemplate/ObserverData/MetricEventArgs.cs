using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverStrategyTemplate.ObserverData
{
    public class MetricEventArgs(string eventType, MetricData data) : EventArgs
    {
        public string EventType { get; } = eventType ?? throw new ArgumentNullException(nameof(eventType));
        public MetricData Data { get; } = data ?? throw new ArgumentNullException(nameof(data));
    }
}
