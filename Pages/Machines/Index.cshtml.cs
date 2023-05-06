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
    public class IndexModel : PageModel
    {
        private readonly Vivacity.Data.AppDbContext _context;

        public IndexModel(Vivacity.Data.AppDbContext context){
            _context = context;
        }

        public IList<Machine> Machine { get;set; } = default!;

        [BindProperty(SupportsGet=true)]
        public string CurrentSort {get; set;}

        public async Task OnGetAsync(){
            if (_context.Machines != null){
                var query = _context.Machines.Include(m=>m.Plant).Select(m=>m);
                
                switch(CurrentSort){
                    case "type_asc":
                        query=query.OrderBy(m=>m.MachineType);
                        break;
                    case "type_dsc":
                        query=query.OrderByDescending(m=>m.MachineType);
                        break;
                    case "model_asc":
                        query=query.OrderBy(m=>m.ModelNumber);
                        break;
                    case "model_dsc":
                        query=query.OrderByDescending(m=>m.ModelNumber);
                        break;
                    case "date_asc":
                        query=query.OrderBy(m=>m.PurchaseDate);
                        break;
                    case "date_dsc":
                        query=query.OrderByDescending(m=>m.PurchaseDate);
                        break;
                    case "cost_asc":
                        query=query.OrderBy(m=>m.PurchaseCost);
                        break;
                    case "cost_dsc":
                        query=query.OrderByDescending(m=>m.PurchaseCost);
                        break;
                    case "plant_asc":
                        query=query.OrderBy(m=>m.Plant.Name);
                        break;
                    case "plant_dsc":
                        query=query.OrderByDescending(m=>m.Plant.Name);
                        break;
                }

                Machine = await query.ToListAsync();
            }
        }
    }
}
