using System;
using System.Collections.Generic;

namespace PostOffice.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? ZipCode { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Parcel> ParcelIdRecipientNavigations { get; set; } = new List<Parcel>();

    public virtual ICollection<Parcel> ParcelIdSenderNavigations { get; set; } = new List<Parcel>();

    public string FullName => $"{Name} {Surname}";
}