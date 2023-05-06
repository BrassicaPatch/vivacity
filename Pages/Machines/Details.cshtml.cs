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
    public class DetailsModel : PageModel
    {
        private readonly Vivacity.Data.AppDbContext _context;

        public DetailsModel(Vivacity.Data.AppDbContext context)
        {
            _context = context;
        }

        public Machine Machine { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id){
            if (id == null || _context.Machines == null){
                return NotFound();
            }

            var machine = await _context.Machines.Include(m=>m.Plant).FirstOrDefaultAsync(m => m.ID == id);
            if (machine == null){
                return NotFound();
            }
            else {
                Machine = machine;
            }
            return Page();
        }
    }
}
