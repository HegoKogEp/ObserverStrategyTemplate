
using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverStrategyTemplate.ObserverData
{
    public class MetricData(string metricName, double value, double threshold, DateTime timestamp)
    {
        public string MetricName { get; } = metricName ?? throw new ArgumentNullException(nameof(metricName));
        public double Value { get; } = value;
        public double Threshold { get; } = threshold;
        public DateTime Timestamp { get; } = timestamp;

        public override string ToString()
        {
            return $"Metric: {MetricName}, Value: {Value} (Threshold: {Threshold})";
        }
    }
}
