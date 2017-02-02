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
        SqlConnection sqlYelp = new SqlConnection();
        public MainWindow()
        {
            InitializeComponent();
        }

        /*
        Description: will load state dropdown with data
        */
        private void updateStateDropdown()
        {

        }

        /*
        Description: will update city dropdown when a state is chosen in the state dropdown
        */
        private void updateCityDropdown()
        {
            string sql = "put query here";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlYelpCon());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
        }

        /*
        Description: will load data into the datagrid every time the combo dropdowns are altered 
        */
        private void loadData()
        {

        }

        private SqlConnection sqlYelpCon()
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
            updateCityDropdown();
        }
    }
}
