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
    public class AdminCommentsController : Controller
    {
        private readonly MonasabatContext _context;

        public AdminCommentsController(MonasabatContext context)
        {
            _context = context;
        }

        // GET: AdminComments
        public async Task<IActionResult> Index()
        {
            var monasabatContext = _context.Comments.Include(c => c.Place).Include(c => c.User);
            return View(await monasabatContext.ToListAsync());
        }

        // GET: AdminComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(c => c.Place)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

   



        

        // GET: AdminComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(c => c.Place)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // POST: AdminComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'MonasabatContext.Comments'  is null.");
            }
            var comments = await _context.Comments.FindAsync(id);
            if (comments != null)
            {
                _context.Comments.Remove(comments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentsExists(int id)
        {
          return _context.Comments.Any(e => e.ID == id);
        }
    }
}
