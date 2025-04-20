using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace Final_Project.Components.Pages.Data
{
    public class Equipment
    {
        public int EquipmentID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DailyRentalCost { get; set; }
        public bool IsSelected { get; set; }
        public Equipment(int equipmentID, int categoryID, string categoryName, string name, string description, double dailyRentalCost)
        {
            EquipmentID = equipmentID;
            CategoryID = categoryID;
            CategoryName = categoryName;
            Name = name;
            Description = description;
            DailyRentalCost = dailyRentalCost;
        }

        public static MySqlConnectionStringBuilder builderString = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            UserID = "root",
            Password = "password",
            Database = "cpsy200_final"
        };

        public static async Task<List<Equipment>> GetEquipmentAsync()
        {
            List<Equipment> equipmentList = new List<Equipment>();
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM equipment";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            equipmentList.Add(new Equipment(
                                reader.GetInt32("EquipmentID"),
                                reader.GetInt32("CategoryID"),
                                reader.GetString("CategoryName"),
                                reader.GetString("Name"),
                                reader.GetString("Description"),
                                reader.GetDouble("DailyRentalCost")
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return equipmentList;
        }

        public static async Task AddEquipmentAsync(Equipment equipment)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = @"INSERT INTO equipment 
                                    (CategoryID, CategoryName, Name, Description, DailyRentalCost) 
                                    VALUES (@CategoryID, @CategoryName, @Name, @Description, @DailyRentalCost)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@CategoryID", equipment.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", equipment.CategoryName);
                    cmd.Parameters.AddWithValue("@Name", equipment.Name);
                    cmd.Parameters.AddWithValue("@Description", equipment.Description);
                    cmd.Parameters.AddWithValue("@DailyRentalCost", equipment.DailyRentalCost);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static async Task<Equipment> FindEquipmentByIDAsync(int equipmentID)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM equipment WHERE EquipmentID = @EquipmentID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@EquipmentID", equipmentID);

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Equipment(
                                equipmentID,
                                reader.GetInt32("CategoryID"),
                                reader.GetString("CategoryName"),
                                reader.GetString("Name"),
                                reader.GetString("Description"),
                                reader.GetDouble("DailyRentalCost")
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                return null;
            }
        }

        public static async Task UpdateEquipmentAsync(Equipment equipment)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = @"UPDATE equipment SET 
                                    CategoryID = @CategoryID, 
                                    CategoryName = @CategoryName, 
                                    Name = @Name, 
                                    Description = @Description, 
                                    DailyRentalCost = @DailyRentalCost 
                                    WHERE EquipmentID = @EquipmentID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@EquipmentID", equipment.EquipmentID);
                    cmd.Parameters.AddWithValue("@CategoryID", equipment.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", equipment.CategoryName);
                    cmd.Parameters.AddWithValue("@Name", equipment.Name);
                    cmd.Parameters.AddWithValue("@Description", equipment.Description);
                    cmd.Parameters.AddWithValue("@DailyRentalCost", equipment.DailyRentalCost);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static async Task DeleteEquipmentAsync(int equipmentID)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "DELETE FROM equipment WHERE EquipmentID = @EquipmentID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@EquipmentID", equipmentID);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}