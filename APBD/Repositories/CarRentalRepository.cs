using APBD.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace APBD.Repositories;

public class CarRentalRepository : ICarRentalRepository
{
    
    private readonly string _connectionString;

    public CarRentalRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("RentalDB") 
                            ?? throw new ArgumentNullException("Connection string not found");    }

    public async Task AddRental(CarRental rental)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        var query = @"INSERT INTO car_rentals (ClientID, CarID, DateFrom, DateTo, TotalPrice, Discount)
                        VALUES (@ClientId, @CarId, @DateFrom, @DateTo, @TotalPrice, @Discount)";
        
        await connection.ExecuteAsync(query, rental);
    }
}