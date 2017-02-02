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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Milestone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
        
        }

        /*
        Description: will load data into the datagrid every time the combo dropdowns are altered 
        */
        private void loadData()
        {

        }

        //events go under here
        private void comboState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateCityDropdown();
        }
    }
}
