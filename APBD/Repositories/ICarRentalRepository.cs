using APBD.Models;

namespace APBD.Repositories;

public interface ICarRentalRepository
{
    Task AddRental(CarRental rental);
}