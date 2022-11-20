using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Monasapat.Models;
using System;

namespace ITI.Monasabat.Control.Controllers
{
    public class UserProfileController : Controller
    {
        MonasabatContext monasabatContext = new MonasabatContext();
        private readonly UserManager<User>? _userManager;
        public UserProfileController(UserManager<User>? userManager)
        {
            _userManager = userManager;
        }
        public IActionResult GetUserReservations()
        {

            var UserId = _userManager.GetUserId(HttpContext.User);

            User user = _userManager.FindByIdAsync(UserId).Result;


            List<Reservation> reservations = monasabatContext.Reservations.Where(x => x.UserID == UserId).ToList();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.ReservationTime.Day - DateTime.Now.Day <= 3 && reservation.PaidMoney < reservation.TotalMoney / 4)
                {

                    Reservation reservation1 = monasabatContext.Reservations.Where(r => r.ID == reservation.ID).FirstOrDefault();
                    reservation1.Status = "Canceled";
                    if (reservation1 != null)
                    {
                        monasabatContext.Reservations.Add(reservation1);
                        //monasabatContext.SaveChanges();

                    }


                }

            }


            ViewBag.LoginUser = user;
            ViewBag.ReservationsList = reservations;

            return View();

        }

        public IActionResult PayPall()
        {
            return View();
        }

        public IActionResult Remove(int id)
        {

            Reservation reservation = monasabatContext.Reservations.Where(r => r.ID == id).FirstOrDefault();
            if (reservation != null)
            {
                monasabatContext.Remove(reservation);
                monasabatContext.SaveChanges();
            }


            return RedirectToAction("GetUserReservations");

        }
    }


}