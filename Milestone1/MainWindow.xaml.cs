using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Milestone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NpgsqlConnection sqlYelp = new NpgsqlConnection("Host=localhost; Username=postgres; Password=Office1; Database=Milestone1DB;");
        public MainWindow()
        {
            InitializeComponent();

            //load the state dropdown right away
            loadStateDropdown();
            loadData();
        }

        /*
        Description: will load state dropdown with data
        */
        private void loadStateDropdown()
        {
            string sql = "SELECT DISTINCT STATE FROM BUSINESS WHERE STATE <> 'state' ORDER BY STATE";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, sqlYelpCon());
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            comboState.ItemsSource = dt.DefaultView;
            comboState.DisplayMemberPath = "state";
        }

        /*
        Description: will update city dropdown when a state is chosen in the state dropdown
        */
        private void loadCityDropdown()
        {
            if (comboState.SelectedValue != null)
            {
                string sql = "SELECT DISTINCT CITY FROM BUSINESS WHERE STATE = '" + comboState.SelectedValue.ToString() + "'";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, sqlYelpCon());
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                
                comboCity.DisplayMemberPath = "city";
                comboCity.ItemsSource = dt.DefaultView;
            }
        }

        /*
        Description: will load data into the datagrid every time the combo dropdowns are altered 
        */
        private void loadData()
        {
            string sql = "SELECT NAME, STATE, CITY FROM BUSINESS ";

            if (comboState.SelectedValue != null)
            {
                sql += "WHERE STATE = '" + comboState.SelectedValue.ToString() + "' ";
            }

            if (comboCity.SelectedValue != null)
            {
                sql += "AND CITY = '" + comboCity.SelectedValue.ToString() + "' ";
            }

            sql += "ORDER BY NAME";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, sqlYelpCon());
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridBusiness.ItemsSource = dt.DefaultView;
        }

        //function to return the connection, open it if it is closed first
        private NpgsqlConnection sqlYelpCon()
        {
            if (sqlYelp.State == ConnectionState.Open)
                return sqlYelp;
            else
                sqlYelp.Open();

            return sqlYelp;
        }

        //events go under here
        private void comboState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadCityDropdown();
            loadData();
        }

        private void comboCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadData();
        }
    }
}
