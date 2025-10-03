namespace Domain.Aggregates
{
    public class Statistics
    {
        private readonly List<(DateTime Timestamp, double ElapsedMs)> _recentRequests = new();

        public int TotalRequests { get; private set; }
        public double TotalResponseTimeMs { get; private set; }
        public int FastCount { get; private set; }
        public int AverageCount { get; private set; }
        public int SlowCount { get; private set; }
        public double AverageResponseTime { get; private set; }

        public Statistics(int totalRequests, double totalResponseTimeMs, int fastCount, int averageCount, int slowCount, double averageResponseTime)
        {
            TotalRequests = totalRequests;
            TotalResponseTimeMs = totalResponseTimeMs;
            FastCount = fastCount;
            AverageCount = averageCount;
            SlowCount = slowCount;
            AverageResponseTime = averageResponseTime;
        }

        public static Statistics Initialize()
        {
            return new Statistics(0, 0, 0, 0, 0, 0);
        }

        public void Update(double elapsedMs)
        {
            TotalRequests++;
            TotalResponseTimeMs += elapsedMs;

            if (elapsedMs < 100)
                FastCount++;
            else if (elapsedMs < 200)
                AverageCount++;
            else
                SlowCount++;

            AverageResponseTime = TotalRequests == 0 ? 0 : TotalResponseTimeMs / TotalRequests;

            _recentRequests.Add((DateTime.UtcNow, elapsedMs));
            _recentRequests.RemoveAll(x => x.Timestamp < DateTime.UtcNow.AddMinutes(-15));
        }

        public double GetLastMinutesAverage(int minutes)
        {
            var time = DateTime.UtcNow.AddMinutes(-minutes);
            var recent = _recentRequests.Where(x => x.Timestamp >= time).ToList();

            if (!recent.Any()) 
                return 0;

            return recent.Average(x => x.ElapsedMs);
        }
    }
}
