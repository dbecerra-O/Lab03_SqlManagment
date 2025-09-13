using System;
using System.Data;
using System.Data.SqlClient;

namespace Lab03C
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=LAB411-014\\SQLEXPRESS;Database=Tecsup2023DB;Integrated Security=true;";
            string query = "SELECT TOP 5 * FROM Products";

            // Llamando a las dos funciones
            GetDataTable(connectionString, query);
            Console.WriteLine();
            GetDataReader(connectionString, query);
            Console.Read();
        }

        // Función que usa DataTable
        static void GetDataTable(string connectionString, string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("✅ Conexión exitosa a la base de datos (DataTable).");

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    Console.WriteLine("📋 Datos cargados en DataTable:");
                    Console.WriteLine($"{"ProductId",-10}{"Name",-20}{"Price",-10}");
                    Console.WriteLine(new string('-', 40));

                    foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine($"{row["ProductId"],-10}{row["Name"],-20}{row["Price"],-10}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al conectar o consultar (DataTable): " + ex.Message);
            }
        }

        // Función que usa DataReader
        static void GetDataReader(string connectionString, string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("✅ Conexión exitosa a la base de datos (DataReader).");

                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("📋 Datos cargados con DataReader:");
                        Console.WriteLine($"{"ProductId",-10}{"Name",-20}{"Price",-10}");
                        Console.WriteLine(new string('-', 40));

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ProductId"],-10}{reader["Name"],-20}{reader["Price"],-10}");
                        }
                    }
                }
                }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al conectar o consultar (DataReader): " + ex.Message);
            }
        }
    }
}
