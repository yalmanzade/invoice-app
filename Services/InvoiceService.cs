using invoice.Models;
using iText.Html2pdf;

namespace invoice.Services;

public class InvoiceService
{
    private string notes = "";
    private string outPath = "Services/Fileout/";
    private string htmlTemplate = "Services/Templates/template.html";
    private string htmlOutput = "Services/Templates/Filein/output.txt";
    private string htmlTable = "Services/Templates/tabletop.html";
    private string htmlFooter = "Services/Templates/footer.html";
    private Invoice invoice;

    public InvoiceService(Invoice invoice)
    {
        this.invoice = invoice;
        this.invoice.DueDate = invoice.DueDate;
    }

    public bool GenerateInvoice()
    {
        try
        {
            ReadyHTMLfile();
            string filename = outPath + invoice.InvoiceId + ".pdf";
            FileStream template = new(htmlOutput, FileMode.Open, FileAccess.Read, FileShare.Read);
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
            StreamReader reader = new(htmlTable);
            using (reader)
            {
                table = reader.ReadToEnd();
            }
            foreach (var item in invoice.Items)
            {
                invoice.Subtotal += item.Amount * item.Quantity;
                table += @"<tr> 
                           <td>" + item.Name + @"</td>
                           <td>" + item.Quantity + @"</td>
                           <td>" + item.Amount + @"</td>
                           <td>" + item.FormatedPrice + @"</td>
                       </tr>";
            }
            table += @"<tr>
                <td></td>
                <td></td>
                <td>Subtotal</td>
                <td>" + invoice.FormatedSubtotal + @"</td>
            </tr>";
            table += @"<tr>
                <td></td>
                <td></td>
                <td>Fees</td>
                <td>" + invoice.FormatedFee + @"</td>
            </tr>";
            table += @"<tr>
                <td></td>
                <td></td>
                <td>Total</td>
                <td>" + invoice.FormatedTotal + @"</td>
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
            StreamReader reader = new(htmlTemplate);
            using (reader)
            {
                inFile = reader.ReadToEnd();
            }
            reader = new(htmlFooter);
            string footer;
            using (reader)
            {
                footer = reader.ReadToEnd();
                footer = string.Format(footer, this.notes);
            }
            StreamWriter writer = new(htmlOutput, false);
            using (writer)
            {
                writer.WriteLine(inFile, DateTime.Now, invoice.DueDate, invoice.Issuer.Name, invoice.Issuer.FormatedPhone, invoice.Issuer.Address,
                               invoice.Customer.Name, invoice.Customer.FormatedPhone, invoice.Customer.Address, table, footer);
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
        return outPath + fileName;
    }
}
