namespace invoice.Services
{
    static public class FileCleanUp
    {
        static public string OutPath { get; set; } = "Services/Fileout/";

        static void DeleteOldInvoices()
        {
            var fileList = Directory.GetFiles(@"C:\File", "*.pdf");
            if (fileList != null && fileList.Length > 2 && fileList.Length < 6)
            {
                foreach (var file in fileList)
                {
                    DeleteFile(file);
                }
            }
        }
        static void DeleteFile(string filePath)
        {
            try { File.Delete(filePath); }
            catch (Exception ex)
            {
                Error.initializeError("Delete Old Invoices", "100", ex.Message);
                Error.logError();
            }
        }
    }
}
