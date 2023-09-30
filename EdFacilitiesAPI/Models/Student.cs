using System;
using System.Collections.Generic;

namespace EdFacilitiesAPI.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
