# ObserverStrategyTemplate - Документация по проекту

## Содержание
1. [ObserverStrategyTemplate.csproj](#observerstrategytemplatecsproj)
2. [Program.cs](#programcs)
3. [Models](#models)
   1. [ConsoleHandler.cs](#consolehandlercs)
   2. [FileHandler.cs](#filehandlercs)
   3. [HtmlFormatStrategy.cs](#htmlformatstrategycs)
   4. [JsonFormatStrategy.cs](#jsonformatstrategycs)
   5. [TextFormatStrategy.cs](#textformatstrategycs)
4. [Models/Base](#models-base)
   1. [EventHandlerBase.cs](#eventhandlerbasecs)
5. [Models/Interfaces](#models-interfaces)
   1. [IFormatStrategy.cs](#iformatstrategycs)
6. [Observerdata](#observerdata)
   1. [EventMonitor.cs](#eventmonitorcs)
   2. [MetricData.cs](#metricdatacs)
   3. [MetricEventArgs.cs](#metriceventargscs)

## FILE 1: EventHandlerBase.cs

<a id='eventhandlerbasecs'></a>

```csharp
﻿using ObserverStrategyTemplate.Models.Interfaces;
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

        protected abstract string FormatMessage(string type, object data);

        protected abstract void SendMessage(string message);

        protected abstract void LogResult();
    }
}
```

---

## FILE 2: ConsoleHandler.cs

<a id='consolehandlercs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;
using ObserverStrategyTemplate.Models.Base;
using ObserverStrategyTemplate.Models.Interfaces;

namespace ObserverStrategyTemplate.Models
{
    public class ConsoleHandler : EventHandlerBase
    {
        public ConsoleHandler(IFormatStrategy strategy) : base(strategy) { }

        protected override string FormatMessage(string type, object data)
        {
            return _formStrategy.Format($"[ALERT] {type}: {data}", DateTime.Now);
        }

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
```

---

## FILE 3: FileHandler.cs

<a id='filehandlercs'></a>

```csharp
﻿using System;
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
```

---

## FILE 4: HtmlFormatStrategy.cs

<a id='htmlformatstrategycs'></a>

```csharp
﻿using System;
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
```

---

## FILE 5: IFormatStrategy.cs

<a id='iformatstrategycs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverStrategyTemplate.Models.Interfaces
{
    public interface IFormatStrategy
    {
        string Format(string message, DateTime timestamp);
    }
}
```

---

## FILE 6: JsonFormatStrategy.cs

<a id='jsonformatstrategycs'></a>

```csharp
﻿using System;
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
```

---

## FILE 7: TextFormatStrategy.cs

<a id='textformatstrategycs'></a>

```csharp
﻿using System;
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
```

---

## FILE 8: EventMonitor.cs

<a id='eventmonitorcs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverStrategyTemplate.ObserverData
{
    public delegate void MetricEventHandler(MetricEventArgs e);

    public class EventMonitor
    {
        public event MetricEventHandler? OnMetricExceeded;

        public void CheckMetric(string metricName, double value, double threshold)
        {
            Console.WriteLine($"[Monitor]: Checking {metricName} ({value} vs {threshold})");
            if (value > threshold)
            {
                var eventData = new MetricData(metricName, value, threshold, DateTime.Now);
                OnMetricExceeded?.Invoke(new MetricEventArgs(eventType: metricName + "_Exceeded", data: eventData));
            }
        }
    }
}
```

---

## FILE 9: MetricData.cs

<a id='metricdatacs'></a>

```csharp
﻿
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
```

---

## FILE 10: MetricEventArgs.cs

<a id='metriceventargscs'></a>

```csharp
﻿using System;
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
```

---

## FILE 11: ObserverStrategyTemplate.csproj

<a id='observerstrategytemplatecsproj'></a>

```xml
﻿<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

---

## FILE 12: Program.cs

<a id='programcs'></a>

```csharp
﻿using ObserverStrategyTemplate.Models;
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
```

---

