using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Data.SqlClient;

namespace ADO.NET_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Authorr
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public override string ToString()
            {
                
                return $"{Id}  {FirstName}  {LastName}";
            }
        }

        public class Categoryy
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {

                return $"{Id}  {Name}  ";
            }
        }

        public ObservableCollection<Authorr> Authorrs { get; set; }
        public ObservableCollection<Categoryy> Categoryys { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Authorrs = new ObservableCollection<Authorr>();
            Categoryys = new ObservableCollection<Categoryy>();
            LoadData();
            DataContext = this;
        }

        private void LoadData()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-SDREM5J\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Trust Server Certificate=True;");
            try
            {
                connection.Open();
                string query = "SELECT * FROM Authors";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Authorrs.Add(new Authorr { Id = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2) });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            { connection.Close(); }


            try
            {
                connection.Open();
                string query_category = "SELECT * FROM Categories";
                SqlCommand command1 = new SqlCommand(query_category, connection);
                SqlDataReader reader1 = command1.ExecuteReader();

                while (reader1.Read())
                {                  
                    Categoryys.Add(new Categoryy { Id = reader1.GetInt32(0), Name = reader1.GetString(1) });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            { connection.Close(); }

        }

        private void aut_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            cat.IsEnabled = true;
        }
    }
}
