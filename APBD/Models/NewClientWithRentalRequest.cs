namespace APBD.Models;

public class NewClientWithRentalRequest
{
    public Client Client { get; set; }
    public int CarId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}