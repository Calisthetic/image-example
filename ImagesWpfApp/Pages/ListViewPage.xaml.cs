using ImagesWpfApp.Utils;
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
using System.Text.Json;
using ImagesWpfApp.Models;

namespace ImagesWpfApp.Pages
{
    /// <summary>
    /// Interaction logic for ListViewPage.xaml
    /// </summary>
    public partial class ListViewPage : Page
    {
        public ListViewPage()
        {
            InitializeComponent();
        }

        private async void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                string response = await APIContext.GetRequest("Employees");
                if (response != null)
                {
                    try
                    {
                        var employees = JsonSerializer.Deserialize<List<EmployeeResponse>>(response);
                        LVEmployees.ItemsSource = employees.ConvertAll(x => new EmployeeToListView(x));
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось получить данные");
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось получить данные!");
                }
            }
        }
    }
}
