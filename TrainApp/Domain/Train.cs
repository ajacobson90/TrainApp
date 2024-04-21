namespace TrainApp.Domain
{
    public class Train
    {
        public Train(string destination, TimeOnly departureTime, int totalSeatsAvailable)
        {
            if (totalSeatsAvailable < 0)
                throw new ArgumentException("The number of seats available cannot be less than 0");

            Destination = destination;
            DepartureTime = departureTime;

            var remainder = totalSeatsAvailable % 2;
            WindowSeatsAvailable = totalSeatsAvailable / 2;
            AisleSeatsAvailable = totalSeatsAvailable / 2 + remainder;
        }

        public TimeOnly DepartureTime { get; set; }
        public string Destination { get; set; }
        public int WindowSeatsAvailable { get; private set; }
        public int AisleSeatsAvailable { get; private set; }

        public bool BookWindowSeat()
        {
            if (WindowSeatsAvailable <= 0)
                return false;
            WindowSeatsAvailable--;
            return true;
        }

        public bool BookAisleSeat()
        {
            if (AisleSeatsAvailable <= 0)
                return false;
            AisleSeatsAvailable--;
            return true;
        }
    }
}
