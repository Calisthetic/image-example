using ImagesWpfApp.Models;
using ImagesWpfApp.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ListViewCreateUpdatePage.xaml
    /// </summary>
    public partial class ListViewCreateUpdatePage : Page
    {
        private EmployeeToListView _employee = null;
        public ListViewCreateUpdatePage()
        {
            InitializeComponent();
            _employee = null;
        }

        public ListViewCreateUpdatePage(EmployeeToListView employee)
        {
            InitializeComponent();
            _employee = employee;
            txbFirstName.Text = employee.FirstName;
            txbSecondName.Text = employee.SecondName;
            txbThirdName.Text = employee.ThirdName;

            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(employee.ImageFile))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
            }
            imgAva.Source = bitmap;
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Name | Description
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                BitmapImage bitmap = new BitmapImage(fileUri);
                imgAva.Source = bitmap;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_employee != null)
                {
                    _employee.FirstName = txbFirstName.Text;
                    _employee.SecondName = txbSecondName.Text;
                    _employee.ThirdName = txbThirdName.Text;

                    BitmapSource bitmapSource = (BitmapSource)imgAva.Source;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                        encoder.Save(stream);
                        _employee.ImageFile = stream.ToArray();
                    }
                }
                else
                {
                    BitmapSource bitmapSource = (BitmapSource)imgAva.Source;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                        encoder.Save(stream);

                        var t = new Employees()
                        {
                            FirstName = txbFirstName.Text,
                            SecondName = txbSecondName.Text,
                            ThirdName = txbThirdName.Text,
                            ImageFile = stream.ToArray(),
                            Login = "1",
                            Password = "2",
                            RoleId = 1
                        };
                        PageManager.MainFrame.GoBack();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
