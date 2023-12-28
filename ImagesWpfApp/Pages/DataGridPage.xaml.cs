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
    /// Interaction logic for DataGridPage.xaml
    /// </summary>
    public partial class DataGridPage : Page
    {
        public DataGridPage()
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new DataGridCreateUpdatePage());
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<Employees> selectedYemploees = DGEmployees.SelectedItems.Cast<Employees>().ToList();
            DBContext.db.Employees.RemoveRange(selectedYemploees);
            try
            {
                await DBContext.db.SaveChangesAsync();
                await RefreshData();
                MessageBox.Show("Let's gooo");
            }
            catch
            {
                MessageBox.Show("Не удалось удалить сотрудника");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            List<Employees> selectedYemploees = DGEmployees.SelectedItems.Cast<Employees>().ToList();
            if (selectedYemploees.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника");
            }
            else if (selectedYemploees.Count > 1)
            {
                MessageBox.Show("Выберите только одного сотрудника");
            }
            else
            {
                PageManager.MainFrame.Navigate(new DataGridCreateUpdatePage(selectedYemploees[0]));
            }
        }

        private async Task RefreshData()
        {
            DGEmployees.ItemsSource = await DBContext.db.Employees.ToListAsync();
        }
    }
}
