using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vivacity.Data;
using Vivacity.Models;

namespace Vivacity.Pages.Plants;

public class DetailsModel : PageModel{
    private readonly Vivacity.Data.AppDbContext _context;

    public DetailsModel(Vivacity.Data.AppDbContext context){
        _context = context;
    }

    [BindProperty(SupportsGet=true)]
    public int PageNum {get; set;} = 1;
    public int PageSize {get; set;} = 10;

    public Plant Plant { get; set; } = default!; 
    public List<Employee> Employees {get; set;} = default!;

    [BindProperty(SupportsGet=true)]
    public string CurrentSort {get; set;}

    public async Task<IActionResult> OnGetAsync(int? id){
        if (id == null || _context.Plants == null){
            return NotFound();
        }

        var plant = await _context.Plants.Include(p=>p.Employees).Include(p=>p.Machines).FirstOrDefaultAsync(p => p.ID == id);
        if (plant == null){
            return NotFound();
        }
        else {
            Plant = plant;
            var employees = Plant.Employees.Select(e=>e);
            switch(CurrentSort){
                case "id_asc":
                    employees = employees.OrderBy(e=>e.ID);
                    break;
                case "id_dsc":
                    employees = employees.OrderByDescending(e=>e.ID);
                    break;
                case "first_asc":
                    employees = employees.OrderBy(e=>e.FirstName);
                    break;
                case "first_dsc":
                    employees = employees.OrderByDescending(e=>e.FirstName);
                    break;
                case "last_asc":
                    employees = employees.OrderBy(e=>e.LastName);
                    break;
                case "last_dsc":
                    employees = employees.OrderByDescending(e=>e.LastName);
                    break;
                case "email_asc":
                    employees = employees.OrderBy(e=>e.Email);
                    break;
                case "email_dsc":
                    employees = employees.OrderByDescending(e=>e.Email);
                    break;
            }

            Employees = employees.Skip((PageNum-1)*PageSize).Take(PageSize).ToList();
        }
        return Page();
    }
}

