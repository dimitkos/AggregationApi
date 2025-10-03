namespace Domain.Aggregates
{
    public class Weather
    {
        public long Id { get; }
        public string Name { get; }
        public double Temp { get; }
        public double Humidity { get; }

        public Weather(long id, string name, double temp, double humidity)
        {
            Id = id;
            Name = name;
            Temp = temp;
            Humidity = humidity;
        }
    }
}
