using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EdFacilitiesAPI.Models;

public partial class History
{
    public int Id { get; set; }

    public int FacilityId { get; set; }

    public int StudentId { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateStart { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateEnd { get; set; }

    public virtual Facility Facility { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
