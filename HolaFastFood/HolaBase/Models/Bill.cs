﻿namespace HolaBase.Models;

public partial class Bill
{
    public int Id { get; set; }

    public DateTime DateCheckIn { get; set; }

    public DateTime? DateCheckOut { get; set; }

    public int IdTable { get; set; }

    public int Status { get; set; }

    public string? UserName { get; set; }

    public int? Discount { get; set; }

    public double? TotalPrice { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<BillInfo> BillInfos { get; set; } = new List<BillInfo>();

    public virtual TableFood IdTableNavigation { get; set; } = null!;
}
