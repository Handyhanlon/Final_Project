using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace Final_Project.Components.Pages.Data
{
    public class Rental
    {
        public int RentalID { get; set; }
        public DateTime RentalDate { get; set; }
        public int CustomerID { get; set; }
        public int EquipmentID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Rental(int rentalID, DateTime rentalDate, int customerID, int equipmentID, DateTime startDate, DateTime returnDate)
        {
            RentalID = rentalID;
            RentalDate = rentalDate;
            CustomerID = customerID;
            EquipmentID = equipmentID;
            StartDate = startDate;
            ReturnDate = returnDate;
        }

        public static MySqlConnectionStringBuilder builderString = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            UserID = "root",
            Password = "password",
            Database = "cpsy200_final"
        };

        public static async Task<List<Rental>> GetRentalsAsync()
        {
            List<Rental> rentalList = new List<Rental>();
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM rental";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rentalList.Add(new Rental(
                                reader.GetInt32("RentalID"),
                                reader.GetDateTime("RentalDate"),
                                reader.GetInt32("CustomerID"),
                                reader.GetInt32("EquipmentID"),
                                reader.GetDateTime("StartDate"),
                                reader.GetDateTime("ReturnDate")
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetRentalsAsync: {ex.Message}");
                }
            }
            return rentalList;
        }

        public static async Task<Rental> FindRentalAsync(int rentalID)
        {
            Rental rental = null;
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM rental WHERE rentalID = '{rentalID}'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime currentDate = reader.GetDateTime(1);
                            int customerID = reader.GetInt32(2);
                            int equipmentID = reader.GetInt32(3);
                            DateTime startDate = reader.GetDateTime(4);
                            DateTime endDate = reader.GetDateTime(5);
                            rental = new Rental(rentalID, currentDate, customerID, equipmentID, startDate, endDate);
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return rental;
        }

        public static async Task AddRentalAsync(Rental rental)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = @"INSERT INTO rental (RentalID, RentalDate, CustomerID, EquipmentID, StartDate, ReturnDate)
                                   VALUES (@RentalID, @RentalDate, @CustomerID, @EquipmentID, @StartDate, @ReturnDate)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@RentalID", rental.RentalID);
                    cmd.Parameters.AddWithValue("@RentalDate", rental.RentalDate);
                    cmd.Parameters.AddWithValue("@CustomerID", rental.CustomerID);
                    cmd.Parameters.AddWithValue("@EquipmentID", rental.EquipmentID);
                    cmd.Parameters.AddWithValue("@StartDate", rental.StartDate);
                    cmd.Parameters.AddWithValue("@ReturnDate", rental.ReturnDate);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in AddRentalAsync: {ex.Message}");
                }
            }
        }

        public static async Task DeleteRentalAsync(int rentalID)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "DELETE FROM rental WHERE RentalID = @RentalID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@RentalID", rentalID);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in DeleteRentalAsync: {ex.Message}");
                }
            }
        }

        public static async Task<double> CalculateRentalCostAsync(int equipmentID, DateTime startDate, DateTime returnDate)
        {
            double totalCost = 0.0;
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT DailyRentalCost FROM equipment WHERE EquipmentID = @EquipmentID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@EquipmentID", equipmentID);
                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value && double.TryParse(result.ToString(), out double dailyCost))
                    {
                        TimeSpan rentalDuration = returnDate - startDate;
                        totalCost = dailyCost * rentalDuration.TotalDays;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in CalculateRentalCostAsync: {ex.Message}");
                }
            }
            return totalCost;
        }
    }
}