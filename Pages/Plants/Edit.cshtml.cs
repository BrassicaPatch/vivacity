using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vivacity.Data;
using Vivacity.Models;

namespace Vivacity.Pages.Plants
{
    public class EditModel : PageModel
    {
        private readonly Vivacity.Data.AppDbContext _context;

        public EditModel(Vivacity.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Plant Plant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Plants == null)
            {
                return NotFound();
            }

            var plant =  await _context.Plants.FirstOrDefaultAsync(m => m.ID == id);
            if (plant == null)
            {
                return NotFound();
            }
            Plant = plant;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Plant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(Plant.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlantExists(int id)
        {
          return (_context.Plants?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
