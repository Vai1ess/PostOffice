using System;
using System.Collections.Generic;

namespace PostOffice.Models;

public partial class Parcel
{
    public int IdParcel { get; set; }

    public int? IdSender { get; set; }

    public int? IdRecipient { get; set; }

    public int? IdDepartureBranch { get; set; }

    public int? IdDestinationBranch { get; set; }

    public int? IdParcelType { get; set; }

    public int? IdParcelStatus { get; set; }

    public decimal? Weight { get; set; }

    public string? Dimensions { get; set; }

    public DateOnly? ShippingDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public decimal? ShippingCost { get; set; }

    public DateOnly? ActualDeliveryDate { get; set; }

    public virtual Branch? IdDepartureBranchNavigation { get; set; }

    public virtual Branch? IdDestinationBranchNavigation { get; set; }

    public virtual ParcelStatus? IdParcelStatusNavigation { get; set; }

    public virtual ParcelType? IdParcelTypeNavigation { get; set; }

    public virtual Client? IdRecipientNavigation { get; set; }

    public virtual Client? IdSenderNavigation { get; set; }

    public virtual ICollection<ParcelTracking> ParcelTrackings { get; set; } = new List<ParcelTracking>();
}
