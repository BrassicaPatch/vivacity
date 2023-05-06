using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vivacity.Data;
using Vivacity.Models;

namespace Vivacity.Pages.Plants
{
    public class DeleteModel : PageModel
    {
        private readonly Vivacity.Data.AppDbContext _context;

        public DeleteModel(Vivacity.Data.AppDbContext context)
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

            var plant = await _context.Plants.FirstOrDefaultAsync(m => m.ID == id);

            if (plant == null)
            {
                return NotFound();
            }
            else 
            {
                Plant = plant;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Plants == null)
            {
                return NotFound();
            }
            var plant = await _context.Plants.FindAsync(id);

            if (plant != null)
            {
                Plant = plant;
                _context.Plants.Remove(Plant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
