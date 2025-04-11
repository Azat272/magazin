using System;
using System.Collections.ObjectModel;
using System.IO;
using MySql.Data.MySqlClient;
using WpfApp4.Entities;

namespace ProductApp
{
    public class AppDbContext
    {
        private readonly string connectionString = "user=root; password=galimov231; port=3306; server=localhost; database=magazin";

        public ObservableCollection<Product> GetProducts()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                string defaultImage = Path.Combine(imageFolder, "picture.png");

                string query = @"SELECT p.ProductId, p.Article, p.Name, p.Description, p.ImageUrl, p.StockQuantity, 
                                 pd.ManufacturerId, pd.SupplierId, pd.CategoryId, pd.Unit, pd.Price, 
                                 d.DiscountPercentage, d.StartDate, d.EndDate, d.MaxAllowedDiscount, c.Name as CategoryName
                                 FROM Products p
                                 LEFT JOIN ProductDetails pd ON p.ProductId = pd.ProductId
                                 LEFT JOIN Discounts d ON p.ProductId = d.ProductId
                                 LEFT JOIN Categories c ON pd.CategoryId = c.CategoryId";

                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();

                var productList = new ObservableCollection<Product>();

                while (reader.Read())
                {
                    string imagePath = reader["ImageUrl"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["ImageUrl"].ToString())
                        ? Path.Combine(imageFolder, reader["ImageUrl"].ToString())
                        : defaultImage;

                    imagePath = File.Exists(imagePath) ? imagePath : defaultImage;

                    var product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Article = reader["Article"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        ImageUrl = imagePath,
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        ManufacturerId = reader["ManufacturerId"] as int?,
                        SupplierId = reader["SupplierId"] as int?,
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        Unit = reader["Unit"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Category = new Category { Name = reader["CategoryName"].ToString() },
                        Discount = reader["DiscountPercentage"] != DBNull.Value ? new Discount
                        {
                            DiscountPercentage = Convert.ToDecimal(reader["DiscountPercentage"]),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            MaxAllowedDiscount = reader["MaxAllowedDiscount"] as decimal?
                        } : null
                    };

                    productList.Add(product);
                }

                reader.Close();
                return productList;
            }
        }
    }
}
