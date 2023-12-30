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
using System.Diagnostics;

namespace ImagesWpfApp.Pages
{
    /// <summary>
    /// Interaction logic for ListViewPage.xaml
    /// </summary>
    public partial class ListViewPage : Page
    {
        private bool _isClickable = false;
        public ICommand DeleteCommand { get; }
        public ListViewPage()
        {
            InitializeComponent();
        }

        private async void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                await RefreshData();
            }
        }

        private async Task RefreshData()
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new ListViewCreateUpdatePage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            List<EmployeeToListView> selectedEmployees = LVEmployees.SelectedItems.Cast<EmployeeToListView>().ToList();
            if (selectedEmployees.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника");
            }
            else if (selectedEmployees.Count > 1)
            {
                MessageBox.Show("Выберите только одного сотрудника");
            }
            else
            {
                PageManager.MainFrame.Navigate(new ListViewCreateUpdatePage(selectedEmployees[0]));
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<EmployeeToListView> selectedEmployees = LVEmployees.SelectedItems.Cast<EmployeeToListView>().ToList();
            if (selectedEmployees.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника");
            }
            else if (selectedEmployees.Count > 1)
            {
                MessageBox.Show("Выберите только одного сотрудника");
            }
            else
            {
                string result = await APIContext.Delete("Employees/" + selectedEmployees[0].Id);
                if (result != null)
                {
                    await RefreshData();
                    MessageBox.Show("Удалено успешно");
                }
                else
                {
                    MessageBox.Show("Не получилось");
                }
            }
        }

        private void LVEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = LVEmployees.SelectedItem;
            if (item != null) {
                var niceItem = item as EmployeeToListView;
                Debug.WriteLine((item as EmployeeToListView).Id);
                if (_isClickable)
                {
                    PageManager.MainFrame.Navigate(new ListViewCreateUpdatePage(niceItem));
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isClickable = cmbClickability.SelectedIndex == 1 ? true : cmbClickability.SelectedIndex == 2 ? false : _isClickable;
            cmbClickability.SelectedIndex = 0;
        }

        // don't work
        private async void btnItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string id)
            {
                var result = await APIContext.Delete("Employees/" + id);
                if (!string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Успешно удалено");
                }
            }
        }
    }
}
