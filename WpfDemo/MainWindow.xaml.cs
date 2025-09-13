using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDemo
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=LAB411-014\\SQLEXPRESS;Database=Tecsup2023DB;Integrated Security=true;TrustServerCertificate=True;";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string searchName = txtSearch.Text.Trim();
            GetProductsByName(searchName);
        }

        // Función que obtiene los productos filtrados por nombre usando DataReader
        private void GetProductsByName(string searchName)
        {
            try
            {
                // Limpiar los datos previos del DataGrid
                dataGridProducts.ItemsSource = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ProductId, Name, Price FROM Products WHERE Name LIKE @Name";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", "%" + searchName + "%");

                    SqlDataReader reader = command.ExecuteReader();

                    var productsList = new List<Product>();

                    // Leer los datos y llenar la lista de productos
                    while (reader.Read())
                    {
                        productsList.Add(new Product
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2)
                        });
                    }

                    // Asignar la lista de productos al DataGrid
                    dataGridProducts.ItemsSource = productsList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
            }
        }
    }
}

    // Clase para representar un estudiante
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
