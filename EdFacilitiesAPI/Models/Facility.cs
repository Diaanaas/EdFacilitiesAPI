using System;
using System.Collections.Generic;

namespace EdFacilitiesAPI.Models;

public partial class Facility
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
