using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace invoice.Models;

public class Invoice
{
    [Key]
    [DisplayName("Id")]
    public ulong InvoiceId { get; set; }
    [DisplayName("Customer")]
    [ForeignKey("CustomerId")]
    public ulong CustomerId { get; set; }
    [DisplayName("Customer Id")]
    [ForeignKey("UserId")]
    public ulong UserId { get; set; }
    public List<ItemSold>? Items = new();
    public List<Fee>? Fees = new();
    [DisplayName("Due Date")]
    [Required]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }
    [Required]
    [DisplayName("Issued Date")]
    [DataType(DataType.Date)]
    public DateTime? IssuedDate { get; set; } = DateTime.Now;
    public string? ItemsJson { get; set; }
    public string? FeesJson { get; set; }
    [Required]
    [DisplayName("Invoice Status")]
    public bool IsPaid { get; set; } = false;
    [NotMapped]
    [Precision(18, 2)]
    public decimal? Total = 0.0M;
    [NotMapped]
    [Precision(18, 2)]
    public decimal? Subtotal = 0.0M;
    [NotMapped]
    [Precision(18, 2)]
    public decimal? Tax = 0.07M;
    [NotMapped]
    [Precision(18, 2)]
    public decimal? Shipping = 0.0M;
    [NotMapped]
    [Precision(18, 2)]
    public decimal? TotalFees = 0.0M;
    [NotMapped]
    public bool HasFee { get; set; }
    [NotMapped]
    public Company? Issuer { get; set; }
    [NotMapped]
    public Company? Customer { get; set; }
    [NotMapped]
    public string? FormatedFee
    {
        get
        {
            if (Fees != null)
            {
                foreach (var item in this.Fees)
                {
                    if (item.IsFlat == 1) TotalFees += item.Amount;
                    else
                    {
                        TotalFees += item.Amount * Subtotal;
                    }
                };
                return string.Format("{0:C}", this.TotalFees);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    [NotMapped]
    public string FormatedTotal
    {
        get
        {
            this.Total = this.Subtotal + (this.Tax * this.Subtotal) + this.TotalFees;
            return string.Format("{0:C}", this.Total);
        }
    }
    [NotMapped]
    public string FormatedTax
    {
        get { return string.Format("{0:C}", (this.Tax * this.Subtotal)); }
    }
    [NotMapped]
    public string FormatedSubtotal
    {
        get { return string.Format("{0:C}", this.Subtotal); }
    }
    public void SaveItemList()
    {
        try
        {
            this.ItemsJson = System.Text.Json.JsonSerializer.Serialize(this.Items);
        }
        catch (Exception ex)
        {
            Error.initializeError("Invoice Json Serialize", "100", ex.Message);
            Error.logError();
        }
    }
    public void SaveFeeList()
    {
        try
        {
            this.FeesJson = System.Text.Json.JsonSerializer.Serialize(this.Fees);
        }
        catch (Exception ex)
        {
            Error.initializeError("Invoice Json Fee Serialize", "100", ex.Message);
            Error.logError();
        }
    }
    public async Task<bool> ParseItemList()
    {
        try
        {
            Items = JsonConvert.DeserializeObject<List<ItemSold>>(ItemsJson);
            return true;
        }
        catch (Exception ex)
        {
            Error.initializeError("ParseItemList", "100", ex.Message);
            Error.logError();
            return false;
        }
    }
    public async Task<bool> ParseFeeList()
    {
        try
        {
            Fees = JsonConvert.DeserializeObject<List<Fee>>(FeesJson);
            return true;
        }
        catch (Exception ex)
        {
            Error.initializeError("ParseItemList", "100", ex.Message);
            Error.logError();
            return false;
        }
    }
}
