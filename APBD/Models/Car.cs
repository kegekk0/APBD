namespace APBD.Models;

public class Car
{
    public int CarId { get; set; }
    public string Vin  { get; set; }
    public string Name { get; set; }
    public int Seats { get; set; }
    public int PricePerDay { get; set; }
    public int ModelId { get; set; }
    public int ColorId { get; set; }
}