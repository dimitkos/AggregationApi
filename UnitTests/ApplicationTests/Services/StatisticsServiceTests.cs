using Application.Services;

namespace UnitTests.ApplicationTests.Services
{
    public class StatisticsServiceTests
    {
        [Fact]
        public void Record_ShouldAddNewApiStats()
        {
            // Arrange
            var service = new StatisticsService();
            var apiName = "TestApi";
            double elapsedMs = 50;

            // Act
            service.Record(apiName, elapsedMs);
            var stats = service.GetStats();

            // Assert
            Assert.True(stats.ContainsKey(apiName));
            var stat = stats[apiName];
            Assert.Equal(1, stat.TotalRequests);
            Assert.Equal(elapsedMs, stat.TotalResponseTimeMs);
            Assert.Equal(1, stat.FastCount);
            Assert.Equal(0, stat.AverageCount);
            Assert.Equal(0, stat.SlowCount);
            Assert.Equal(elapsedMs, stat.AverageResponseTime);
        }

        [Fact]
        public void Record_ShouldUpdateExistingApiStats()
        {
            // Arrange
            var service = new StatisticsService();
            var apiName = "TestApi";
            service.Record(apiName, 50);
            service.Record(apiName, 150);

            // Act
            var stats = service.GetStats();
            var stat = stats[apiName];

            // Assert
            Assert.Equal(2, stat.TotalRequests);
            Assert.Equal(1, stat.FastCount);
            Assert.Equal(1, stat.AverageCount);
            Assert.Equal(0, stat.SlowCount);
            Assert.Equal(100, stat.AverageResponseTime);
        }
    }
}
