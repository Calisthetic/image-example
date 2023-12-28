using ImagesWpfApp.Models;
using ImagesWpfApp.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace ImagesWpfApp.Pages
{
    /// <summary>
    /// Interaction logic for ItemsControlPage.xaml
    /// </summary>
    public partial class ItemsControlPage : Page
    {
        public ItemsControlPage()
        {
            InitializeComponent();
        }

        private async void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                try
                {
                    var employees = await DBContext.db.Employees.Include(x => x.Roles).ToListAsync();
                    icEmployees.ItemsSource = employees.ConvertAll(x => new EmployeeToItemsControl(x));
                }
                catch
                {
                    MessageBox.Show("Ошибочка");
                }
            }
        }
    }
}
