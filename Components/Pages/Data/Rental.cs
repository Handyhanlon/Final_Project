using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using MySqlConnector;

namespace Final_Project.Components.Pages.Data
{
    public class Rental
    {
        public int rentalID { get; set; }
        public DateTime currentDate { get; set; }
        public int customerID { get; set; }
        public int equipmentID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Rental(int rentalID, DateTime currentDate, int customerID, int equipmentID, DateTime startDate, DateTime endDate)
        {
            this.rentalID = rentalID;
            this.customerID = customerID;
            this.equipmentID = equipmentID;
            this.startDate = startDate;
            this.endDate = endDate;
        }
        public static MySqlConnectionStringBuilder builderString = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            UserID = "root",
            Password = "password",
            Database = "cpsy200_final"
        };
        public static List<Rental> GetRentals()
        {
            List<Rental> rentals = new List<Rental>();
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM rental";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int rentalID = reader.GetInt32(0);
                        DateTime currentDate = reader.GetDateTime(1);
                        int customerID = reader.GetInt32(2);
                        int equipmentID = reader.GetInt32(3);
                        DateTime startDate = reader.GetDateTime(4);
                        DateTime endDate = reader.GetDateTime(5);
                        Rental rental = new Rental(rentalID, currentDate, customerID, equipmentID, startDate, endDate);
                        rentals.Add(rental);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return rentals;
        }
        public static void AddRental(Rental rental)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"INSERT INTO rental (currentDate, customerID, equipmentID, startDate, endDate) VALUES (@currentDate, @customerID, @equipmentID, @startDate, @endDate)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@currentDate", rental.currentDate);
                    cmd.Parameters.AddWithValue("@customerID", rental.customerID);
                    cmd.Parameters.AddWithValue("@equipmentID", rental.equipmentID);
                    cmd.Parameters.AddWithValue("@startDate", rental.startDate);
                    cmd.Parameters.AddWithValue("@endDate", rental.endDate);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void UpdateRental(Rental rental)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"UPDATE rental SET currentDate = @currentDate, customerID = @customerID, equipmentID = @equipmentID, startDate = @startDate, endDate = @endDate WHERE rentalID = @rentalID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@currentDate", rental.currentDate);
                    cmd.Parameters.AddWithValue("@customerID", rental.customerID);
                    cmd.Parameters.AddWithValue("@equipmentID", rental.equipmentID);
                    cmd.Parameters.AddWithValue("@startDate", rental.startDate);
                    cmd.Parameters.AddWithValue("@endDate", rental.endDate);
                    cmd.Parameters.AddWithValue("@rentalID", rental.rentalID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void DeleteRental(int rentalID)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"DELETE FROM rental WHERE rentalID = @rentalID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@rentalID", rentalID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static double CalculateRentalCost(int equipmentID, DateTime startDate, DateTime endDate)
        {
            TimeSpan rentalDuration = endDate - startDate;
            double totalCost = rentalDuration.TotalDays * Equipment.GetEquipment().FirstOrDefault(e => e.equipmentID == equipmentID)?.dailyRentalCost ?? 0;
            return totalCost;
        }
    }
}
