namespace Domain.Aggregates
{
    public class Statistics
    {
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

        public Statistics Update(double elapsedMs)
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

            return new Statistics(
                TotalRequests,
                TotalResponseTimeMs,
                FastCount,
                AverageCount,
                SlowCount,
                AverageResponseTime);
        }
    }
}
