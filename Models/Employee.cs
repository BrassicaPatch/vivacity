using System.ComponentModel.DataAnnotations;

namespace Vivacity.Models;

public class Employee {
    [Required]
    public int ID {get; set;}

    [Required]
    public string FirstName {get; set;} = null!;
    [Required]
    public string LastName {get; set;} = null!;

    [Required]
    public string Address {get; set;} = null!;

    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber {get; set;} = null!;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email {get; set;} = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime HireDate {get; set;}

    [Required]
    [DataType(DataType.Currency)]
    public float Salary {get; set;} = 0;

    public int PlantID {get; set;}
    public Plant? Plant {get; set;}

    public Employee(){}

    public Employee(int id, string fname, string lname, string address, string pnumber, string email, DateTime hired, float salary){
        ID=id;
        FirstName=fname;
        LastName=lname;
        Address=address;
        PhoneNumber=pnumber;
        Email=email;
        HireDate=hired;
        Salary=salary;
    }
}