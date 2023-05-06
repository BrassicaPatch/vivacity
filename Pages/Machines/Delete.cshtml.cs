using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vivacity.Data;
using Vivacity.Models;

namespace Vivacity.Pages.Machines
{
    public class DeleteModel : PageModel
    {
        private readonly Vivacity.Data.AppDbContext _context;

        public DeleteModel(Vivacity.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Machine Machine { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Machines == null)
            {
                return NotFound();
            }

            var machine = await _context.Machines.FirstOrDefaultAsync(m => m.ID == id);

            if (machine == null)
            {
                return NotFound();
            }
            else 
            {
                Machine = machine;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Machines == null)
            {
                return NotFound();
            }
            var machine = await _context.Machines.FindAsync(id);

            if (machine != null)
            {
                Machine = machine;
                _context.Machines.Remove(Machine);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
