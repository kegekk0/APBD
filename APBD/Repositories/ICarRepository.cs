using APBD.Models;

namespace APBD.Repositories;

public interface ICarRepository
{
    Task<bool> CarExist(int carId);
    Task<Car> GetCarById(int carId);
}