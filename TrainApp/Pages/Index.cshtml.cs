using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainApp.Data;
using TrainApp.Models;

namespace TrainApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TrainRepository _trainRepository;
        public IndexModel(ILogger<IndexModel> logger, TrainRepository trainRepository)
        {
            _logger = logger;
            _trainRepository = trainRepository;
        }

        public IEnumerable<TrainVM>? Trains { get; set; }

        public void OnGet()
        {
            try
            {
                Trains = _trainRepository.GetAll()
                    .Select(t => new TrainVM { Destination = t.Destination, DepartureTime = t.DepartureTime });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An unexpected error occurred");
                throw;
            }
            
        }

        public PartialViewResult OnGetTrain(string destination)
        {
            try
            {
                var train = _trainRepository.Get(destination);
                var model = new TrainSeatsVM { Destination = destination, AvailableAisleSeats = train.AisleSeatsAvailable, AvailableWindowSeats = train.WindowSeatsAvailable };
                return Partial("_BookingSelectionPartial", model);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "An unexpected error occurred.");
                return Partial("_ErrorPartial", "An unexpected error occurred while trying to fetch available tickets. Please try again later.");
            }
            
        }

        public JsonResult OnPostWindowBooking(string destination)
        {
            try
            {
                var train = _trainRepository.Get(destination);
                var bookingSuccessful = train.BookWindowSeat();
                if (bookingSuccessful)
                {
                    _trainRepository.Update(train);
                }
                return new JsonResult(new { Success = bookingSuccessful });
            }
            catch(Exception e)
            {
                _logger.LogError(e, "An unexpected error ocurred while trying to book a window seat");
                return new JsonResult(new { Error = true });
            }
            
        }

        public JsonResult OnPostAisleBooking(string destination)
        {
            try
            {
                var train = _trainRepository.Get(destination);
                var bookingSuccessful = train.BookAisleSeat();
                if (bookingSuccessful)
                {
                    _trainRepository.Update(train);
                }
                return new JsonResult(new { Success = bookingSuccessful });
            }
            catch(Exception e)
            {
                _logger.LogError(e, "An unexpected error ocurred while trying to book a window seat");
                return new JsonResult(new { Error = true });
            }
            
        }
    }
}
