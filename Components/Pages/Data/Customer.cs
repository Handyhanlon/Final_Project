using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace Final_Project.Components.Pages.Data
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long? Phone { get; set; }

        public Customer(int customerID, string firstName, string lastName, long? phone, string email)
        {
            CustomerID = customerID;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
        }

        public static MySqlConnectionStringBuilder builderString = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            UserID = "root",
            Password = "password",
            Database = "cpsy200_final"
        };

        public static async Task<List<Customer>> GetCustomersAsync()
        {
            List<Customer> customers = new List<Customer>();
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM customer";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            customers.Add(new Customer(
                                reader.GetInt32("CustomerID"),
                                reader.GetString("FirstName"),
                                reader.GetString("LastName"),
                                reader.IsDBNull("Phone") ? null : reader.GetInt64("Phone"),
                                reader.GetString("Email")
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return customers;
        }

        public static async Task AddCustomerAsync(Customer customer)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = @"INSERT INTO customer 
                                    (FirstName, LastName, Phone, Email) 
                                    VALUES (@FirstName, @LastName, @Phone, @Email)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static async Task<Customer> FindCustomerByIDAsync(int customerID)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM customer WHERE CustomerID = @CustomerID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Customer(
                                customerID,
                                reader.GetString("FirstName"),
                                reader.GetString("LastName"),
                                reader.IsDBNull("Phone") ? null : reader.GetInt64("Phone"),
                                reader.GetString("Email")
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

        public static async Task UpdateCustomerAsync(Customer customer)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = @"UPDATE customer SET 
                                    FirstName = @FirstName, 
                                    LastName = @LastName, 
                                    Phone = @Phone, 
                                    Email = @Email 
                                    WHERE CustomerID = @CustomerID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static async Task DeleteCustomerAsync(int customerID)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "DELETE FROM customer WHERE CustomerID = @CustomerID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
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