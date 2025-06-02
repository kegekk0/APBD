using APBD.Models;
using Dapper;
using Microsoft.Data.SqlClient;


namespace APBD.Repositories;

public class CarRepository : ICarRepository
{
    
    private readonly string _connectionString;

    public CarRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("RentalDB") 
                            ?? throw new ArgumentNullException("Connection string not found");    
    }

    public async Task<bool> CarExist(int carId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        var query = "SELECT 1 FROM Cars WHERE CarId = @CarId";
        var result = await connection.QueryFirstOrDefaultAsync<int?>(query, new { CarId = carId });
        return result.HasValue();
    }

    public async Task<Car> GetCarById(int carId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        var query = "SELECT ID, VIN, Name, Seats, PricePerDay, ModelID, ColorID FROM cars WHERE CarId = @CarId";
        return await connection.QueryFirstOrDefaultAsync<Car>(query, new { CarId = carId });
    }
}