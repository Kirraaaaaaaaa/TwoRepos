using SESSIA.Fabrics_Data_SetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SESSIA.Windows
{
    /// <summary>
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        Fabrics_Data_Set Fabrics_Data_Set = new Fabrics_Data_Set();
        ProductTableAdapter productTableAdapter = new ProductTableAdapter();

        private string ConnectionString;
        private SqlConnection Connection;
        private SqlCommand Command = new SqlCommand();
        SqlConnectionStringBuilder StringBuilder = new SqlConnectionStringBuilder();


        public Manager(string FIO)
        {
            InitializeComponent();
            productTableAdapter.Fill(Fabrics_Data_Set.Product);

            StringBuilder.ConnectionString = Properties.Settings.Default.FabricsConnectionString;
            ConnectionString = StringBuilder.ConnectionString;
            Connection = new SqlConnection(ConnectionString);
        }

        private void Back_BTN_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            this.Hide();
            authorization.Show();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Insert_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (Product_ArticleNumber.Text != "" || Product_Name.Text != "" || Product_Metric.Text != "" || Product_Cost.Text != "" || Product_MaxDiscount.Text != "" || Product_Manufacturer.Text != "" || 
                Product_Provider.Text != "" || Product_Category.Text != "" || Product_CurrentDiscount.Text != "" || Product_QuantityInStock.Text != "" || Product_Description.Text != "")
            {
                productTableAdapter.InsertQuery(Product_ArticleNumber.Text, Product_Name.Text, Product_Metric.Text,
                Convert.ToInt32(Product_Cost.Text), Convert.ToInt32(Product_MaxDiscount.Text), Product_Manufacturer.Text, Product_Provider.Text, Product_Category.Text,
                Convert.ToInt32(Product_CurrentDiscount.Text), Convert.ToInt32(Product_QuantityInStock.Text), Product_Description.Text, "");
                productTableAdapter.Fill(Fabrics_Data_Set.Product);
            }
            else
            {
                Connection.Close();
                MessageBox.Show("Введите все данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
