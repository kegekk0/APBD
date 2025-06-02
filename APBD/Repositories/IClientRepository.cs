using APBD.Models;

namespace APBD.Repositories;

public interface IClientRepository
{
    Task<ClientWithRentals> GetClientWithRentals(int clientId);
    Task<int> AddClient(Client client);
}