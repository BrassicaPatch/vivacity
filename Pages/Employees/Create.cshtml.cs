using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vivacity.Data;
using Vivacity.Models;

namespace Vivacity.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly Vivacity.Data.AppDbContext _context;

        public CreateModel(Vivacity.Data.AppDbContext context){
            _context = context;
        }

        

        [BindProperty]
        public Employee Employee { get; set; } = default!;
        public SelectList Plants {get; set;} = default!;
        
        public IActionResult OnGet(){
            Plants = new SelectList(_context.Plants, "ID", "Name");

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid || _context.Employees == null || Employee == null){
                return Page();
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
