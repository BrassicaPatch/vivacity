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

public class IndexModel : PageModel{
    private readonly Vivacity.Data.AppDbContext _context;

    public IndexModel(Vivacity.Data.AppDbContext context){
        _context = context;
    }

    public IList<Plant> Plants { get;set; } = default!;

    public async Task OnGetAsync(){
        if (_context.Plants != null){
            Plants = await _context.Plants.ToListAsync();
        }
    }
}

