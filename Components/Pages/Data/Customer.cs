using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using MySqlConnector;

namespace Final_Project.Components.Pages.Data
{
    internal class Customer
    {
        public int customerID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int? phone { get; set; }
        
        public Customer (int customerID, string firstName, string lastName,  int? phone, string email)
        {
            this.customerID = customerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.email = email;
        }

        static MySqlConnectionStringBuilder builderString = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Database = "cpsy200_final",
            UserID = "root",
            Password = "password"
        };


        public static List<Customer> GetCustomers(string search)
        {
            List<Customer> customers = new List<Customer>();
            using (MySqlConnection connection = new MySqlConnection(builderString.ConnectionString))
            {
                
                try
                {
                    connection.Open();

                    if (search != "")
                    {
                        string query = $"SELECT * FROM customer WHERE LOWER(firstName) LIKE '%{search}%' OR LOWER(lastName) LIKE '%{search}%'";

                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            int customerID = reader.GetInt32(0);
                            string firstName = reader.GetString(1);
                            string lastName = reader.GetString(2);
                            int? phone = reader.GetInt32(3);
                            string email = reader.GetString(4);

                            Customer customer = new Customer(customerID, firstName, lastName, phone, email);
                            customers.Add(customer);
                        }
                    }
                    else
                    {
                        string query = $"SELECT * FROM customer";

                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            int customerID = reader.GetInt32(0);
                            string firstName = reader.GetString(1);
                            string lastName = reader.GetString(2);
                            int? phone = reader.GetInt32(3);
                            string email = reader.GetString(4);

                            Customer customer = new Customer(customerID, firstName, lastName, phone, email);
                            customers.Add(customer);
                        }
                    }


                        connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                return customers;
            }

        }
    }
}
