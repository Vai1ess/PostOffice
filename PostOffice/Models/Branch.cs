using System;
using System.Collections.Generic;

namespace PostOffice.Models;

public partial class Branch
{
    public int IdBranch { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? ZipCode { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Parcel> ParcelIdDepartureBranchNavigations { get; set; } = new List<Parcel>();

    public virtual ICollection<Parcel> ParcelIdDestinationBranchNavigations { get; set; } = new List<Parcel>();

    public virtual ICollection<ParcelTracking> ParcelTrackings { get; set; } = new List<ParcelTracking>();

    public string FullValue => $"Город: {City}, Адрес: {Address}, Index: {ZipCode} Номер телефона: {PhoneNumber}";
}
