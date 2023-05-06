using System.ComponentModel.DataAnnotations;

namespace Vivacity.Models;

public class Plant {
    public int ID {get; set;}

    [Required]
    public string Name {get; set;} = null!;
    [Required]
    public string Address {get; set;} = null!;

    public List<Employee> Employees {get; set;} = new();
    public List<Machine> Machines {get; set;} = new();

    public Plant(){}

    public Plant(string name, string address, List<Employee> employees, List<Machine> machines) {
        Name=name;
        Address=address;
        Employees=employees;
        Machines = machines;
    }
}