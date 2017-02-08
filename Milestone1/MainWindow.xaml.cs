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

            //load all data to start
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
            
            //fill the datatable with the adapter
            adapter.Fill(dt);

            //set the source of data for the combobox to the filled datatable, avoids any loops
            comboState.ItemsSource = dt.DefaultView;
            comboState.DisplayMemberPath = "state";
        }

        /*
        Description: will update city dropdown when a state is chosen in the state dropdown
        */
        private void loadCityDropdown()
        {
            //make sure a value is selected
            if (comboState.SelectedValue != null)
            {
                string sql = "SELECT DISTINCT CITY FROM BUSINESS WHERE STATE = '" + comboState.SelectedValue.ToString() + "'";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, sqlYelpCon());
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                //fill combobox with data that is in the data table
                comboCity.DisplayMemberPath = "city";
                comboCity.ItemsSource = dt.DefaultView;
            }
        }

        /*
        Description: will load data into the datagrid every time the combo dropdowns are altered 
        */
        private void loadData()
        {
            //base case, show all data
            string sql = "SELECT NAME, STATE, CITY FROM BUSINESS ";

            //if state selected, only show data for that state
            if (comboState.SelectedValue != null)
            {
                sql += "WHERE STATE = '" + comboState.SelectedValue.ToString() + "' ";
            }

            //if city selected (state must be selected so keep 'AND') show data for that city
            if (comboCity.SelectedValue != null)
            {
                sql += "AND CITY = '" + comboCity.SelectedValue.ToString() + "' ";
            }

            //order alphabetical by name
            sql += "ORDER BY NAME";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, sqlYelpCon());
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            //set the data source for the grid as the filled data table, avoids any loops
            dataGridBusiness.ItemsSource = dt.DefaultView;
        }

        //function to return the connection, open it if it is closed first
        private NpgsqlConnection sqlYelpCon()
        {
            //if connection is already open - return it, else open it and return it
            if (sqlYelp.State == ConnectionState.Open)
                return sqlYelp;
            else
                sqlYelp.Open();

            return sqlYelp;
        }

        //events go under here
        private void comboState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if state is selected, load the cities
            loadCityDropdown();
            //now that new state is picked, load the data into the grid for that state
            loadData();

            dataGridBusiness.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void comboCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if city gets chosen, state has already been selected so just load data into datagrid
            loadData();

            dataGridBusiness.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void dataGridBusiness_Loaded(object sender, RoutedEventArgs e)
        {
            //eliminates extra columna put at end
            dataGridBusiness.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    }
}
