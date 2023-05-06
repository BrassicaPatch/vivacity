using Microsoft.EntityFrameworkCore;
using Vivacity.Models;

namespace Vivacity.Data;

public class SeedData {

    public static void Initialize(IServiceProvider serviceProvider) {
        using(var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {
            if(context.Employees.Any()) return;

            var random = new Random();

            var usedPlants = new HashSet<Plant>();
            for(int i=0; i<8;){ //random.Next(3, 20);){
                var plant = PlantData.Plants[random.Next(0, PlantData.Plants.Count()-1)];
                if(usedPlants.Contains(plant)) continue;

                usedPlants.Add(plant);
                context.Plants.Add(plant);
                context.SaveChanges();
                i++;
            }

            //Theres probably a much better way to do than then randomly checking, either iterating or otherwise, but I started this way first and its good enough :)
            var usedMachines = new HashSet<Machine>();
            var usedEmployees = new HashSet<Employee>();
            foreach(var p in context.Plants) {

                var numOfMachines = random.Next(6, 11);
                for(int i=0; i<numOfMachines;){
                    var machine = MachineData.Machines[random.Next(0, MachineData.Machines.Count()-1)];
                    if(usedMachines.Contains(machine)) continue;

                    machine.PlantID=p.ID;
                    usedMachines.Add(machine);
                    context.Machines.Add(machine);
                    context.SaveChanges();
                    i++;
                }

                var numOfEmployees = (random.Next(2,5)*numOfMachines);
                for(int i=0; i<numOfEmployees;){
                    var employee = EmployeeData.Employees[random.Next(0, EmployeeData.Employees.Count()-1)];
                    if(usedEmployees.Contains(employee)) continue;

                    employee.PlantID=p.ID;
                    usedEmployees.Add(employee);
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    i++;
                }
            }
        }
    }
}