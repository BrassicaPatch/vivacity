using System.ComponentModel.DataAnnotations;

namespace Vivacity.Models;

public class Machine {
    public int ID {get; set;}

    [Required]
    public string MachineType {get; set;} = null!;
    [Required]
    public string ModelNumber {get; set;} = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime PurchaseDate {get; set;}
    [Required]
    [DataType(DataType.Currency)]
    public float PurchaseCost {get; set;} = 0;

    public int PlantID {get; set;}
    public Plant? Plant {get; set;}

    public Machine(){}

    public Machine(string type, string model, DateTime date, float cost){
        MachineType=type;
        ModelNumber=model;
        PurchaseDate=date;
        PurchaseCost=cost;
    }
}
