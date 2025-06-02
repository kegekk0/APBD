using APBD.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace APBD.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly string _connectionString;

    public ClientRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("RentalDB")
            ?? throw new ArgumentNullException("Connection string not found");
    }

    public async Task<ClientWithRentals> GetClientWithRentals(int clientId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        var clientQuery = "SELECT ID, FirstName, LastName, Address FROM clients WHERE ID = @ClientId";
        var client = await connection.QueryFirstOrDefaultAsync<ClientWithRentals>(clientQuery, new { ClientId = clientId });
        
        if (client == null) return null;
        
        const string rentalsQuery = @"
            SELECT 
                c.VIN, col.Name AS Color, m.Name AS Model, cr.DateFrom,
                cr.DateTo, cr.TotalPrice FROM car_rentals cr 
                JOIN cars c ON cr.CarId = c.CarId
                JOIN colors col ON c.ColorID = col.ID
                Join models m ON c.ModelId = m.ID
                WHERE cr.ClientId = @ClientId";
        
        var rentals = await connection.QueryAsync<RentalInfo>(rentalsQuery, new {CliendId = clientId});
        client.Rentals = rentals.ToList();

        return client;

    }

    public async Task<int> AddClient(Client client)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        var query = @"INSERT INTO clients (FirstName, LastName, Address)
                        OUTPUT INSERTED.ID
                        VALUES (@FirstName, @LastName, @Address)";
        
        return await connection.ExecuteScalarAsync<int>(query, client);
    }
}