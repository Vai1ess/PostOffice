using System;
using System.Collections.Generic;

namespace PostOffice.Models;

public partial class ParcelType
{
    public int IdParcelType { get; set; }

    public string? TypeName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Parcel> Parcels { get; set; } = new List<Parcel>();
    public string FullParcelType => $"Тип посылки: {TypeName}. Описание: {Description}";
}
