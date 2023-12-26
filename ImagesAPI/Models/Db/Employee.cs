using System;
using System.Collections.Generic;

namespace ImagesAPI.Models.Db;

public partial class Employee
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string SecondName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? ThirdName { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? LastEntryTime { get; set; }

    public bool? LastEntryType { get; set; }

    public byte[]? ImageFile { get; set; }

    public virtual Role Role { get; set; } = null!;
}
