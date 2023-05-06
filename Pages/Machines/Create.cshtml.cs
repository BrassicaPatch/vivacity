using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vivacity.Data;
using Vivacity.Models;

namespace Vivacity.Pages.Machines
{
    public class CreateModel : PageModel
    {
        private readonly Vivacity.Data.AppDbContext _context;

        public CreateModel(Vivacity.Data.AppDbContext context){
            _context = context;
        }

        [BindProperty]
        public Machine Machine { get; set; } = default!;
        public SelectList Plants {get; set;} = default!;
        
        public IActionResult OnGet(){
            Plants = new SelectList(_context.Plants, "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(){
            if (!ModelState.IsValid || _context.Machines == null || Machine == null){
                return Page();
            }

            _context.Machines.Add(Machine);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
