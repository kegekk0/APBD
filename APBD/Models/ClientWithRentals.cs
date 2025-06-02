namespace APBD.Models;

public class ClientWithRentals
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public List<RentalInfo> Rentals { get; set; }
}