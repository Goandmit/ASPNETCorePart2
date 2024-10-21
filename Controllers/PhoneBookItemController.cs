using ASPNETCorePart2.Data;
using ASPNETCorePart2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCorePart2.Controllers
{
    public class PhoneBookItemController : Controller
    {
        private readonly ASPNETCorePart2Context _context;

        public PhoneBookItemController(ASPNETCorePart2Context context)
        {
            _context = context;
        }

        // GET: PhoneBookItem
        public async Task<IActionResult> Index()
        {
            return _context.PhoneBook != null ?
                        View(await _context.PhoneBook.ToListAsync()) :
                        Problem("Entity set 'ASPNETCorePart2Context.PhoneBookItem'  is null.");
        }

        // GET: PhoneBookItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhoneBook == null)
            {
                return NotFound();
            }

            var phoneBookItem = await _context.PhoneBook.FirstOrDefaultAsync(m => m.Id == id);

            if (phoneBookItem == null)
            {
                return NotFound();
            }

            return View(phoneBookItem);
        }

        // GET: PhoneBookItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhoneBookItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Surname,Name,Patronymic,PhoneNumber,Address,Description")] PhoneBookItem phoneBookItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneBookItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(phoneBookItem);
        }

        // GET: PhoneBookItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhoneBook == null)
            {
                return NotFound();
            }

            var phoneBookItem = await _context.PhoneBook.FindAsync(id);

            if (phoneBookItem == null)
            {
                return NotFound();
            }

            return View(phoneBookItem);
        }

        // POST: PhoneBookItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,Surname,Name,Patronymic,PhoneNumber,Address,Description")] PhoneBookItem phoneBookItem)
        {
            if (id != phoneBookItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneBookItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneBookItemExists(phoneBookItem.Id))
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

            return View(phoneBookItem);
        }

        // GET: PhoneBookItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhoneBook == null)
            {
                return NotFound();
            }

            var phoneBookItem = await _context.PhoneBook.FirstOrDefaultAsync(m => m.Id == id);

            if (phoneBookItem == null)
            {
                return NotFound();
            }

            return View(phoneBookItem);
        }

        // POST: PhoneBookItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhoneBook == null)
            {
                return Problem("Entity set 'ASPNETCorePart2Context.PhoneBookItem'  is null.");
            }

            var phoneBookItem = await _context.PhoneBook.FindAsync(id);

            if (phoneBookItem != null)
            {
                _context.PhoneBook.Remove(phoneBookItem);
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool PhoneBookItemExists(int id)
        {            
          return (_context.PhoneBook?.Any(e => e.Id == id)).GetValueOrDefault();
        }        
    }
}
