using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImagesWpfApp.Models
{
    internal class EmployeeToListView
    {
        public EmployeeToListView(EmployeeResponse employee)
        {
            Role = employee.Role;
            Id = employee.Id;
            FirstName = employee.FirstName;
            SecondName = employee.SecondName;
            ThirdName = employee.ThirdName;
            Login = employee.Login;
            Password = employee.Password;
            LastEntryTime = employee.LastEntryTime;
            LastEntryType = employee.LastEntryType;
            ImageFile = null;
            if (employee.ImageFile != null)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(Convert.FromBase64String(employee.ImageFile));
                bitmap.EndInit();
                //ImageFile = bitmap;
                ImageFile = Convert.FromBase64String(employee.ImageFile);
            }
        }
        public int Id { get; set; }

        public string SecondName { get; set; } = null;

        public string FirstName { get; set; } = null;

        public string ThirdName { get; set; }

        public string Login { get; set; } = null;

        public string Password { get; set; } = null;

        public DateTime? LastEntryTime { get; set; }

        public string LastEntryType { get; set; }

        //public BitmapImage ImageFile { get; set; }
        public byte[] ImageFile { get; set; }

        public string Role { get; set; } = null;
    }
}
