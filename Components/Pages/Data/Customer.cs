using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using MySqlConnector;
using System.Reflection.PortableExecutable;

namespace Final_Project.Components.Pages.Data
{
    public class Customer
    {
        public int customerID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public long? phone { get; set; }
        
        public Customer (int customerID, string firstName, string lastName,  long? phone, string email)
        {
            this.customerID = customerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.email = email;
        }

        public static MySqlConnectionStringBuilder builderString = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            UserID = "root",
            Password = "password",
            Database = "cpsy200_final"
        };


        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM customer";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        int customerID = reader.GetInt32(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);

                        // Handle phone number carefully
                        long? phone = null;
                        if (!reader.IsDBNull(3))
                        {
                            try
                            {
                                phone = reader.GetInt64(3); // Using GetInt64 for long type
                            }
                            catch
                            {
                                Console.WriteLine($"Error parsing phone number for customer {customerID}");
                            }
                        }

                        string email = reader.GetString(4);

                        Customer customer = new Customer(customerID, firstName, lastName, phone, email);
                        customers.Add(customer);
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                }
                return customers;
            }
        }
        public static void AddCustomer(string firstName, string lastName, long? phone, string email)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"INSERT INTO customer (firstName, lastName, phone, email) VALUES ('{firstName}', '{lastName}', {phone}, '{email}')";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static Customer FindCustomerByID(int customerID)
        {
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM customer WHERE customerID = '{customerID}'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader.GetString(1);
                            string lastName = reader.GetString(2);
                            long? phone = null;
                            if (!reader.IsDBNull(3))
                            {
                                try
                                {
                                    phone = reader.GetInt64(3); // Using GetInt64 for long type
                                }
                                catch
                                {
                                    Console.WriteLine($"Error parsing phone number for customer {customerID}");
                                }
                            }

                            string email = reader.GetString(4);
                            Customer customer = new Customer(customerID, firstName, lastName, phone, email);
                            connection.Close();
                            return customer;
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
        public string CustomerToString()
        {
            return $"{customerID} {firstName} {lastName} {phone} {email}";
        }
    }
}
