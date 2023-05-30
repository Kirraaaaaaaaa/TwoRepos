using SESSIA.Fabrics_Data_SetTableAdapters;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Klient.xaml
    /// </summary>
    public partial class Klient : Window
    {
        Fabrics_Data_Set Fabrics_Data_Set = new Fabrics_Data_Set();
        ProductTableAdapter productTableAdapter = new ProductTableAdapter();

        public Klient(string FIO)
        {
            InitializeComponent();
            productTableAdapter.Fill(Fabrics_Data_Set.Product);
            Product_List.ItemsSource = Fabrics_Data_Set.Product.DefaultView;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Back_BTN_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            this.Hide();
            authorization.Show();
        }

    }
}
