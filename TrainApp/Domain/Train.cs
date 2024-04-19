namespace TrainApp.Domain
{
    public class Train
    {
        public Train(string destination, TimeOnly departureTime, int totalSeatsAvailable)
        {
            Destination = destination;
            DepartureTime = departureTime;

            var remainder = totalSeatsAvailable % 2;
            _windowSeatsAvailable = totalSeatsAvailable / 2;
            _aisleSeatsAvailable = totalSeatsAvailable / 2 + remainder;
        }
        private int _windowSeatsAvailable;
        private int _aisleSeatsAvailable;

        public TimeOnly DepartureTime { get; set; }
        public string Destination { get; set; }
        public int WindowSeatsAvailable => _windowSeatsAvailable;
        public int AisleSeatsAvailable => _aisleSeatsAvailable;

        public bool BookWindowSeat()
        {
            if (WindowSeatsAvailable <= 0)
                return false;
            _windowSeatsAvailable--;
            return true;
        }

        public bool BookAisleSeat()
        {
            if (AisleSeatsAvailable <= 0)
                return false;
            _aisleSeatsAvailable--;
            return true;
        }
    }
}
