using System;
using System.Collections.ObjectModel;
using System.Windows;
using WpfApp4.Entities;

namespace ProductApp
{
    public partial class ProductListWindow : Window
    {
        public ObservableCollection<Product> Products { get; set; }

        public ProductListWindow()
        {
            InitializeComponent();
            DataContext = this;

            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            AppDbContext dbContext = new AppDbContext();
            Products = dbContext.GetProducts();
        }
    }
}
