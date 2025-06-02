using APBD.Models;
using APBD.Repositories;

namespace APBD.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ICarRepository _carRepository;
    private readonly ICarRentalRepository _carRentalRepository;

    public ClientService(IClientRepository clientRepository, 
                ICarRepository carRepository, 
                ICarRentalRepository carRentalRepository)
    {
        _clientRepository = clientRepository;
        _carRepository = carRepository;
        _carRentalRepository = carRentalRepository;
    }

    public async Task<ClientWithRentals> GetClientWithRentalsAsync(int clientId)
    {
        return await _clientRepository.GetClientWithRentals(clientId);
    }

    public async Task AddClientWithRentals(NewClientWithRentalRequest request)
    {
        var carExists = await _carRepository.CarExist(request.CarId);
        if (!carExists)
        {
            throw new ArgumentException("Car does not exist");
        }
        
        var days = (request.DateTo - request.DateFrom).Days;
        if (days <= 0)
        {
            throw new ArgumentException("Days must be greater than 0");
        }
        
        var car = await _carRepository.GetCarById(request.CarId);
        var totalPrice = days * car.PricePerDay;

        var clientId = await _clientRepository.AddClient(request.Client);

        var rental = new CarRental
        {
            ClientId = clientId,
            CarId = request.CarId,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
            TotalPrice = totalPrice,
            Discount = null
        };
        await _carRentalRepository.AddRental(rental);
    }
}