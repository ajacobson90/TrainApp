using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System.Runtime.CompilerServices;
using TrainApp.Domain;

namespace TrainApp.Data
{
    public class TrainRepository
    {
        //since there wasn't a strict requirement for persisting data, and I'm not sure about modifying the original text file,
        //I'm deciding to store updates to the data exclusively in memory. Will work fine as long as repository
        //is injected as a singleton during the time the site runs. Oviously we wouldn't do this in a production app.
        private Dictionary<string, Train> _trainData;
        public TrainRepository()
        {
            _trainData = new Dictionary<string, Train>();
        }
        public IEnumerable<Train> GetAll()
        {
            if (!_trainData.Any())
                PopulateDataStore();
            return _trainData.Select(d => d.Value).ToList();
        }

        public Train Get(string destination)
        {
            if (!_trainData.Any())
                PopulateDataStore();
            return _trainData[destination];
        }

        public void Update(Train train)
        {
            if (_trainData.ContainsKey(train.Destination))
                _trainData[train.Destination] = train;
        }

        private void PopulateDataStore()
        {
            _trainData.Clear();

            DateTimeFormatInfo timeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            timeFormat.TimeSeparator = ".";

            using (var reader = new StreamReader("./Train Data.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var data = line.Split(',');
                    var time = TimeOnly.Parse(data[0], timeFormat);
                    var destination = data[1];
                    var tickets = int.Parse(data[2]);

                    var train = new Train(destination, time, tickets);
                    _trainData.Add(destination, train);

                    line = reader.ReadLine();
                }
            }
        }
    }
}
