namespace HolaBase.Models;

public partial class Account
{
    public string UserName { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public int Type { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
