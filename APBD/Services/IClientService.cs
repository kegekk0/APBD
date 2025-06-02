using APBD.Models;

namespace APBD.Services;

public interface IClientService
{
    Task<ClientWithRentals> GetClientWithRentalsAsync(int clientId);
    Task AddClientWithRentals (NewClientWithRentalRequest request);
}