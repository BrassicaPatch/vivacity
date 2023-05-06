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

namespace Vivacity.Pages.Machines
{
    public class EditModel : PageModel{
        private readonly Vivacity.Data.AppDbContext _context;

        public EditModel(Vivacity.Data.AppDbContext context){
            _context = context;
        }

        [BindProperty]
        public Machine Machine { get; set; } = default!;
        public SelectList Plants {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id){
            if (id == null || _context.Machines == null){
                return NotFound();
            }

            var machine =  await _context.Machines.FirstOrDefaultAsync(m => m.ID == id);
            if (machine == null){
                return NotFound();
            }
            Machine = machine;
            Plants = new SelectList(_context.Plants, "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(){
            if (!ModelState.IsValid){
                return Page();
            }

            _context.Attach(Machine).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!MachineExists(Machine.ID)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MachineExists(int id){
          return (_context.Machines?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
