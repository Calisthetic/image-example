using ImagesAPI.Models.Db;

namespace ImagesAPI.Models.DTOs
{
    public class EmployeesResponse
    {
        public EmployeesResponse() { }

        public EmployeesResponse(Employee employee) {
            Role = employee.Role != null ? employee.Role.Name : string.Empty;
            Id = employee.Id;
            FirstName = employee.FirstName;
            SecondName = employee.SecondName;
            ThirdName = employee.ThirdName;
            Login = employee.Login;
            Password = employee.Password;
            LastEntryTime = employee.LastEntryTime;
            LastEntryType = employee.LastEntryType != null ? (bool)employee.LastEntryType ? "Успешно" : "Не успешно" : null;
            ImageFile = employee.ImageFile != null ? Convert.ToBase64String(employee.ImageFile) : null;
        }

        public int Id { get; set; }

        public string SecondName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? ThirdName { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime? LastEntryTime { get; set; }

        public string? LastEntryType { get; set; }

        public string? ImageFile { get; set; }

        public string Role { get; set; } = null!;
    }
}
