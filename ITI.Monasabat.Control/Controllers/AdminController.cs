using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monasapat.Models;

namespace ITI.Monasabat.Control.Controllers
{
    public class AdminController : Controller
    {
        MonasabatContext Context;
        public AdminController(MonasabatContext _context)
        {
            Context=_context;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            int totalMoney=0;
            var Money= Context.Reservations?.Select(i=>i.PaidMoney).ToList();
            var NumberOFUser = Context.Users.ToList().Count();
            var NumberOFReservations=Context.Reservations.Count();
            var numberOFPlaces=Context.Places.Count();
            var NumberOfComments = Context.Comments.Count();
            var numberOfplaceOwner = Context.PlaceOwners.Count();
            if (Money != null)
            {
                foreach (int money in Money)
                {
                    totalMoney += money;
                }
            }
            ViewBag.TotalMoney=totalMoney;
            ViewBag.NumberOFUser=NumberOFUser;
            ViewBag.NumberOfOFReservations=NumberOFReservations;
            ViewBag.NumberOfPlaces = numberOFPlaces;
            ViewBag.Comments = NumberOfComments;
            ViewBag.PlaceOwner = numberOfplaceOwner;


            return View();
        }
    }
}
