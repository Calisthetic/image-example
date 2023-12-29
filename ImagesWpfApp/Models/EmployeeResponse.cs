using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImagesWpfApp.Models
{
    public class EmployeeResponse
    {
        public EmployeeResponse() { }

        public EmployeeResponse(EmployeeToListView employee)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            SecondName = employee.SecondName;
            ThirdName = employee.ThirdName;
            Login = employee.Login;
            Password = employee.Password;
            LastEntryTime = employee.LastEntryTime;
            LastEntryType = employee.LastEntryType;
            ImageFile = Convert.ToBase64String(employee.ImageFile);
            Role = employee.Role;
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("secondName")]
        public string SecondName { get; set; } = null;

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null;

        [JsonPropertyName("thirdName")]
        public string ThirdName { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; } = null;

        [JsonPropertyName("password")]
        public string Password { get; set; } = null;

        [JsonPropertyName("lastEntryTime")]
        public DateTime? LastEntryTime { get; set; }

        [JsonPropertyName("lastEntryType")]
        public string LastEntryType { get; set; }

        [JsonPropertyName("imageFile")]
        public string ImageFile { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; } = null;
    }
}
