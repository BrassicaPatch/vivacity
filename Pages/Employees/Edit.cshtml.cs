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

namespace Vivacity.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly Vivacity.Data.AppDbContext _context;

        public EditModel(Vivacity.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;
        public SelectList Plants {get; set;} = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee =  await _context.Employees.FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
            Plants = new SelectList(_context.Plants, "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(){
            if (!ModelState.IsValid){
                return Page();
            }

            _context.Attach(Employee).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!EmployeeExists(Employee.ID)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
