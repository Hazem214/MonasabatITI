using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Monasapat.Models;

namespace ITI.Monasabat.Control.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminReservationsController : Controller
    {
        private readonly MonasabatContext _context;

        public AdminReservationsController(MonasabatContext context)
        {
            _context = context;
        }

        // GET: AdminReservations
        public async Task<IActionResult> Index()
        {
            var monasabatContext = _context.Reservations.Include(r => r.Place).Include(r => r.User);
            return View(await monasabatContext.ToListAsync());
        }

        // GET: AdminReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Place)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: AdminReservations/Create
        public IActionResult Create()
        {
            ViewData["placeID"] = new SelectList(_context.Places, "ID", "Name");
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Status,ReservationTime,PaidMoney,remainingMoney,TotalMoney,placeID,UserID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["placeID"] = new SelectList(_context.Places, "ID", "Name", reservation.placeID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", reservation.UserID);
            return View(reservation);
        }

        // GET: AdminReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["placeID"] = new SelectList(_context.Places, "ID", "Name", reservation.placeID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", reservation.UserID);
            return View(reservation);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Status,ReservationTime,PaidMoney,remainingMoney,TotalMoney,placeID,UserID")] Reservation reservation)
        {
            if (id != reservation.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    if (reservation.PaidMoney + reservation.remainingMoney != reservation.TotalMoney||reservation.PaidMoney<0||reservation.remainingMoney<0)
                    {
                        ModelState.AddModelError("", "total money should equal paid money +reamining money");
                        ViewData["placeID"] = new SelectList(_context.Places, "ID", "Name", reservation.placeID);
                        ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", reservation.UserID);
                        return View();
                    }
                    if ( reservation.ReservationTime.Day <= DateTime.Now.Day)
                    {
                        ModelState.AddModelError("", "Enter Valid Date");
                        ViewData["placeID"] = new SelectList(_context.Places, "ID", "Name", reservation.placeID);
                        ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", reservation.UserID);
                        return View();
                    }
                    if (reservation.Status == null || reservation.TotalMoney == 0)
                    {
                      
                        ViewData["placeID"] = new SelectList(_context.Places, "ID", "Name", reservation.placeID);
                        ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", reservation.UserID);
                        ModelState.AddModelError("", "you should enter valid data");
                       
                        return View();
                    }
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["placeID"] = new SelectList(_context.Places, "ID", "Name", reservation.placeID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", reservation.UserID);
            return View(reservation);
        }

        // GET: AdminReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Place)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: AdminReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'MonasabatContext.Reservations'  is null.");
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
          return _context.Reservations.Any(e => e.ID == id);
        }
    }
}
