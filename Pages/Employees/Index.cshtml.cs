using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vivacity.Data;
using Vivacity.Models;

namespace Vivacity.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly Vivacity.Data.AppDbContext _context;

        public IndexModel(Vivacity.Data.AppDbContext context){
            _context = context;
        }

        public IList<Employee> Employees { get;set; } = default!;

        [BindProperty(SupportsGet=true)]
        public string CurrentSort {get; set;}

        [BindProperty(SupportsGet=true)]
        public string Search {get; set;}

        public async Task OnGetAsync() {
            if (_context.Employees != null){
                var query = _context.Employees.Include(e => e.Plant).Select(e=>e);

                //if(!String.IsNullOrEmpty(Search))
                    //query=query.Where(e=> Contains(e.ID.ToString(), Search));   //|| Contains(e.FirstName, Search) || Contains(e.LastName, Search));

                switch(CurrentSort){
                    case "id_asc":
                        query=query.OrderBy(e=>e.ID);
                        break;
                    case "id_dsc":
                        query=query.OrderByDescending(e=>e.ID);
                        break;
                    case "first_asc":
                        query=query.OrderBy(e=>e.FirstName);
                        break;
                    case "first_dsc":
                        query=query.OrderByDescending(e=>e.FirstName);
                        break;
                    case "last_asc":
                        query=query.OrderBy(e=>e.LastName);
                        break;
                    case "last_dsc":
                        query=query.OrderByDescending(e=>e.LastName);
                        break;
                    case "date_asc":
                        query=query.OrderBy(e=>e.HireDate);
                        break;
                    case "date_dsc":
                        query=query.OrderByDescending(e=>e.HireDate);
                        break;
                    case "sal_asc":
                        query=query.OrderBy(e=>e.Salary);
                        break;
                    case "sal_dsc":
                        query=query.OrderByDescending(e=>e.Salary);
                        break;
                    case "plant_asc":
                        query=query.OrderBy(e=>e.Plant.Name);
                        break;
                    case "plant_dsc":
                        query=query.OrderByDescending(e=>e.Plant.Name);
                        break;
                }

                if(!String.IsNullOrEmpty(Search)){
                    //Had to do this in a weird way since it wouldn't let me run Contains() on the query directly, since I guess certain LINQ things can't be
                    //run on queries for some reason. Didn't really understand why when searching it, but something I'll probably look into more in the future. For now this works.
                    Employees = query.AsEnumerable().Where(e=> Contains(e.ID.ToString(), Search) || Contains(e.FirstName, Search) || Contains(e.LastName, Search)).ToList();
                }
                else Employees = query.ToList();
            }
        }

        bool Contains(string source, string toCheck){
            return source.IndexOf(toCheck, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}
