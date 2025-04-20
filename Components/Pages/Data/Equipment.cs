using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using MySqlConnector;

namespace Final_Project.Components.Pages.Data
{
    public class Equipment
    {
        public int equipmentID { get; set; }
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double dailyRentalCost { get; set; }
        public Equipment(int equipmentID, int categoryID, string categoryName, string name, string description, double dailyRentalCost)
        {
            this.equipmentID = equipmentID;
            this.categoryID = categoryID;
            this.categoryName = categoryName;
            this.name = name;
            this.description = description;
            this.dailyRentalCost = dailyRentalCost;
        }

        public static MySqlConnectionStringBuilder builderString = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            UserID = "root",
            Password = "password",
            Database = "cpsy200_final"
        };
        public static List<Equipment> GetEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM equipment";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int equipmentID = reader.GetInt32(0);
                        int categoryID = reader.GetInt32(1);
                        string categoryName = reader.GetString(2);
                        string name = reader.GetString(3);
                        string description = reader.GetString(4);
                        double dailyRentalCost = reader.GetDouble(5);
                        Equipment equipment = new Equipment(equipmentID, categoryID, categoryName, name, description, dailyRentalCost);
                        equipmentList.Add(equipment);
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return equipmentList;
        }
        public static void AddEquipment(Equipment equipment)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"INSERT INTO equipment (categoryID, categoryName, name, description, dailyRentalCost) VALUES (@categoryID, @categoryName, @name, @description, @dailyRentalCost)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@categoryID", equipment.categoryID);
                    cmd.Parameters.AddWithValue("@categoryName", equipment.categoryName);
                    cmd.Parameters.AddWithValue("@name", equipment.name);
                    cmd.Parameters.AddWithValue("@description", equipment.description);
                    cmd.Parameters.AddWithValue("@dailyRentalCost", equipment.dailyRentalCost);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        public static Equipment FindEquipmentByID(int equipmentID)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM equipment WHERE equipmentID = '{equipmentID}'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int categoryID = reader.GetInt32(1);
                            string categoryName = reader.GetString(2);
                            string name = reader.GetString(3);
                            string description = reader.GetString(4);
                            double dailyRentalCost = reader.GetDouble(5);
                            connection.Close();
                            Equipment equipment = new Equipment(equipmentID, categoryID, categoryName, name, description, dailyRentalCost);
                            return equipment;
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
    }
}
