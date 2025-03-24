using System;
using System.Collections.Generic;

namespace PostOffice.Models;

public partial class ParcelTracking
{
    public int IdTracking { get; set; }

    public int? IdParcel { get; set; }

    public int? IdBranch { get; set; }

    public int? IdPercelStatus { get; set; }

    public DateTime? TrackingDate { get; set; }

    public string? Description { get; set; }

    public virtual Branch? IdBranchNavigation { get; set; }

    public virtual Parcel? IdParcelNavigation { get; set; }

    public virtual ParcelStatus? IdPercelStatusNavigation { get; set; }
}
