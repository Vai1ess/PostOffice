using System;
using System.Collections.Generic;

namespace PostOffice.Models;

public partial class ParcelStatus
{
    public int IdParcelStatuses { get; set; }

    public string? StatusName { get; set; }

    public string? DescriptionStatus { get; set; }

    public virtual ICollection<ParcelTracking> ParcelTrackings { get; set; } = new List<ParcelTracking>();

    public virtual ICollection<Parcel> Parcels { get; set; } = new List<Parcel>();
}
