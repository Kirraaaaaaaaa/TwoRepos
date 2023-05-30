using SESSIA.Fabrics_Data_SetTableAdapters;
using SESSIA.Windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SESSIA
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        Fabrics_Data_Set fabrics_Data_Set = new Fabrics_Data_Set();
        UserTableAdapter userTableAdapter = new UserTableAdapter();
        RoleTableAdapter roleTableAdapter = new RoleTableAdapter();

        private string ConnectionString;
        private SqlConnection Connection;
        private SqlCommand Command = new SqlCommand();
        private SqlDataReader Reader;
        SqlConnectionStringBuilder StringBuilder = new SqlConnectionStringBuilder();    


        public Authorization()
        {
            InitializeComponent();
            userTableAdapter.Fill(fabrics_Data_Set.User);
            roleTableAdapter.Fill(fabrics_Data_Set.Role);

            StringBuilder.ConnectionString = Properties.Settings.Default.FabricsConnectionString;
            ConnectionString = StringBuilder.ConnectionString;
            Connection = new SqlConnection(ConnectionString);
        }

        private void Authorization_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (Login_TB.Text !="" && Password_PB.Password !="")
            {
                try
                {
                    Command.CommandText = "SELECT * from dbo.[User] where User_Login = '" + Login_TB.Text + "' AND User_Password = '" + Password_PB.Password+"'";
                    Command.Connection = Connection;
                    Connection.Open();
                    Reader = Command.ExecuteReader();
                    int count = 0;
                    string user_role = "";
                    string user_FIO = "";
                    while (Reader.Read())
                        {
                        count++;
                        user_role = Convert.ToString(Reader["User_Role_Name"]);
                        user_FIO = Convert.ToString(Reader["User_Surname"] + " " + Convert.ToString(Reader["User_Name"] + " " + Convert.ToString(Reader["User_Middle_Name"])));
                    }
                    if (count == 1)
                    {
                        Connection.Close();
                        switch (user_role)
                        {
                            case "Администратор":
                                Admin admin = new Admin(user_FIO);
                                admin.Show();
                                this.Close();
                                break;

                            case "Менеджер":
                                Manager manager = new Manager(user_FIO);
                                manager.Show();
                                this.Close();

                                break;
                            case "Клиент":
                                Klient klient = new Klient(user_FIO);
                                klient.Show();
                                this.Close();
                                break;
                        }
                    }
                    else
                    {
                        Connection.Close();
                        MessageBox.Show("Неверный логин или пароль!\n Повторите попытку входа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка связи с сервером", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Guest_BTN_Click(object sender, RoutedEventArgs e)
        {

            Klient klient = new Klient("Гость");
            klient.Show();
            this.Close();
        }

        private void Login_Password_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex login_regex = new Regex("[^а-яА-ЯёЁ]");
            e.Handled = !login_regex.IsMatch(e.Text);
            
        }
    }
}
