using System.Diagnostics;
using ITI.Monasabat.Control.Models;
using Microsoft.AspNetCore.Mvc;
using Monasapat.Models;

namespace ITI.Monasabat.Control.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public MonasabatContext Context = new MonasabatContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int totalMoney = 0;
            var Money = Context.Reservations?.Select(i => i.PaidMoney).ToList();
            var NumberOFUser = Context.Users.ToList().Count();
            var NumberOFReservations = Context.Reservations.Count();
            var numberOFPlaces = Context.Places.Count();
            if (Money != null)
            {
                foreach (int money in Money)
                {
                    totalMoney += money;
                }
            }
            ViewBag.TotalMoney = totalMoney;
            ViewBag.NumberOFUser = NumberOFUser;
            ViewBag.NumberOfOFReservations = NumberOFReservations;
            ViewBag.NumberOfPlaces = numberOFPlaces;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Contact()
        {
            return View ();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult OurTeam()
        {
            return View();
        }
    }
}