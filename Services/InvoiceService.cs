using invoice.Models;
using iText.Html2pdf;

namespace invoice.Services;

public class InvoiceService
{
    public string Notes = string.Empty;
    public string TermsAndCond = string.Empty;
    private string OutPath = "Services/Fileout/";
    private string HtmlTemplate = "Services/Templates/template.html";
    private string HtmlOutput = "Services/Templates/Filein/output.txt";
    private string HtmlTable = "Services/Templates/tabletop.html";
    private string HtmlFooter = "Services/Templates/footer.html";
    private Invoice Invoice;

    public InvoiceService(Invoice invoice)
    {
        this.Invoice = invoice;
        this.Invoice.DueDate = invoice.DueDate;
    }

    public bool GenerateInvoice()
    {
        try
        {
            ReadyHTMLfile();
            string filename = OutPath + Invoice.InvoiceId + ".pdf";
            FileStream template = new(HtmlOutput, FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream outFile = new(filename, FileMode.Create);
            HtmlConverter.ConvertToPdf(template, outFile);
            Console.WriteLine("Invoice Generated");
            outFile.Close();
            template.Close();
            return true;
        }
        catch (FileNotFoundException e)
        {
            Error.initializeError("GenerateInvoice", "000", e.Message);
            Error.logError();
            return false;
        }
    }
    private string ReadyHTMLTable()
    {
        string table = "";
        try
        {
            StreamReader reader = new(HtmlTable);
            using (reader)
            {
                table = reader.ReadToEnd();
            }
            foreach (var item in Invoice.Items)
            {
                Invoice.Subtotal += item.Amount * item.Quantity;
                table += @"<tr> 
                           <td>" + item.Name + @"</td>
                           <td>" + item.Quantity + @"</td>
                           <td>" + $"${item.Amount}" + @"</td>
                           <td>" + item.FormatedPrice + @"</td>
                       </tr>";
            }
            table += @"<tr>
                <td></td>
                <td></td>
                <td>Subtotal</td>
                <td>" + Invoice.FormatedSubtotal + @"</td>
            </tr>";
            table += @"<tr>
                <td></td>
                <td></td>
                <td>Taxes</td>
                <td>" + Invoice.FormatedTax + @"</td>
            </tr>";
            table += @"<tr>
                <td></td>
                <td></td>
                <td>Fees</td>
                <td>" + Invoice.FormatedFee + @"</td>
            </tr>";
            table += @"<tr>
                <td></td>
                <td></td>
                <td>Total</td>
                <td>" + Invoice.FormatedTotal + @"</td>
            </tr>";
            table += @"</tbody></table>";
            return table;
        }
        catch (DirectoryNotFoundException e)
        {
            Error.initializeError("Ready HTML File", "000", e.Message);
            Error.logError();
        }
        catch (FileNotFoundException e)
        {
            Error.initializeError("Ready HTML File", "000", e.Message);
            Error.logError();
        }
        catch (IOException e)
        {
            Error.initializeError("Ready HTML File", "000", e.Message);
            Error.logError();
        }
        return "";
    }
    private bool ReadyHTMLfile()
    {
        try
        {
            string table = ReadyHTMLTable();
            string inFile = "";
            StreamReader reader = new(HtmlTemplate);
            using (reader)
            {
                inFile = reader.ReadToEnd();
            }
            reader = new(HtmlFooter);
            string footer;
            using (reader)
            {
                footer = reader.ReadToEnd();
                footer = string.Format(footer, this.Notes, this.TermsAndCond);
            }
            StreamWriter writer = new(HtmlOutput, false);
            using (writer)
            {
                writer.WriteLine(inFile, DateTime.Now, Invoice.DueDate, Invoice.Issuer.Name, Invoice.Issuer.FormatedPhone, Invoice.Issuer.Address,
                               Invoice.Customer.Name, Invoice.Customer.FormatedPhone, Invoice.Customer.Address, table, footer);
            }
            return true;
        }
        catch (FileNotFoundException e)
        {
            Error.initializeError("Ready HTML File", "000", e.Message);
            Error.logError();
            return false;
        }
        catch (IOException e)
        {
            Error.initializeError("Ready HTML File", "000", e.Message);
            Error.logError();
            return false;
        }
    }
    public string GetFilePath(ulong Id)
    {
        string fileName = Id.ToString() + ".pdf";
        return OutPath + fileName;
    }
}
