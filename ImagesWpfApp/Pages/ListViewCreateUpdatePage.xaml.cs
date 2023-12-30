using ImagesWpfApp.Models;
using ImagesWpfApp.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
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

        private async void btnSave_Click(object sender, RoutedEventArgs e)
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
                    string result = await APIContext.Put("Employees/" + _employee.Id, JsonSerializer.Serialize(new EmployeeResponse(_employee)));
                    if (result == "Success")
                    {
                        MessageBox.Show("Данные сохранены");
                    }
                    else
                    {
                        MessageBox.Show("Не получилось");
                    }
                }
                else
                {
                    if (imgAva.Source != null)
                    {
                        BitmapSource bitmapSource = (BitmapSource)imgAva.Source;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                            encoder.Save(stream);

                            var t = new EmployeeResponse()
                            {
                                FirstName = txbFirstName.Text,
                                SecondName = txbSecondName.Text,
                                ThirdName = txbThirdName.Text,
                                ImageFile = Convert.ToBase64String(stream.ToArray()),
                                Login = "1",
                                Password = "2",
                                Role = "Продавец"
                            };
                            string result = await APIContext.Post("Employees", JsonSerializer.Serialize(t));
                            if (await CreateEmployee(t))
                            {
                                MessageBox.Show("Добавлено");
                                PageManager.MainFrame.GoBack();
                            }
                            else
                            {
                                MessageBox.Show("Не получилось");
                            }
                        }
                    }
                    else
                    {
                        var t = new EmployeeResponse()
                        {
                            FirstName = txbFirstName.Text,
                            SecondName = txbSecondName.Text,
                            ThirdName = txbThirdName.Text,
                            Login = "1",
                            Password = "2",
                            Role = "Продавец"
                        };
                        string result = await APIContext.Post("Employees", JsonSerializer.Serialize(t));
                        if (await CreateEmployee(t))
                        {
                            MessageBox.Show("Добавлено");
                            PageManager.MainFrame.GoBack();
                        }
                        else
                        {
                            MessageBox.Show("Не получилось");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<bool> CreateEmployee(EmployeeResponse employee)
        {
            return !string.IsNullOrEmpty(await APIContext.Post("Employees", JsonSerializer.Serialize(employee)));
        }
    }
}
