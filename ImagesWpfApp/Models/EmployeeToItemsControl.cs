using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesWpfApp.Models
{
    public class EmployeeToItemsControl
    {
        public EmployeeToItemsControl(Employees employee)
        {
            Role = employee.Roles != null ? employee.Roles.Name : string.Empty;
            Id = employee.Id;
            FirstName = employee.FirstName;
            SecondName = employee.SecondName;
            ThirdName = employee.ThirdName;
            Login = employee.Login;
            Password = employee.Password;
            LastEntryTime = employee.LastEntryTime;
            LastEntryType = employee.LastEntryType != null ? (bool)employee.LastEntryType ? "Успешно" : "Не успешно" : string.Empty;
            ImagePath = "/Resources/" + SecondName + ".jpeg";
        }
        public int Id { get; set; }

        public string SecondName { get; set; } = null;

        public string FirstName { get; set; } = null;

        public string ThirdName { get; set; }

        public string Login { get; set; } = null;

        public string Password { get; set; } = null;

        public DateTime? LastEntryTime { get; set; }

        public string LastEntryType { get; set; }

        public string ImagePath { get; set; }

        public string Role { get; set; } = null;
    }
}
