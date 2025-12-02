using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace WCFServiceApplication
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GeneratePDF(long appointmentId)
        {
            try
            {
                List<UserInfo> users = new List<UserInfo>{
                    new UserInfo
            {
                Id = 1,
                Username = "kamran",
                Email = "kamran@example.com",
                Name = "Kamran",
                Family = "TajerBashi",
                Phone = "123456789",
                SignImagePath = @"C:\Users\Tajer\Desktop\GitHub\Repo_Tajerbashi\SourceApps\WCF\WcfServiceWebApi\wwwroot\Signs\1.jpg"
            },
            new UserInfo
            {
                Id = 2,
                Username = "ali",
                Email = "ali@example.com",
                Name = "Ali",
                Family = "Ahmadi",
                Phone = "987654321",
                SignImagePath = @"C:\Users\Tajer\Desktop\GitHub\Repo_Tajerbashi\SourceApps\WCF\WcfServiceWebApi\wwwroot\Signs\2.jpg"
            }
        };
                string directoryPath = @"C:\Users\Tajer\Desktop\GitHub\Repo_Tajerbashi\SourceApps\WCF\WcfServiceWebApi\wwwroot\pdf";
                Directory.CreateDirectory(directoryPath);

                string filePath = Path.Combine(directoryPath, "UserInfo.pdf");

                using (var writer = new PdfWriter(filePath))
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    document.Add(new Paragraph("User Information").SetFontSize(18).SetMarginBottom(20).SimulateBold());

                    // Create table with 7 columns
                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 5, 10, 15, 10, 10, 10, 15 })).UseAllAvailableWidth();

                    // Add table header
                    table.AddHeaderCell("ID");
                    table.AddHeaderCell("Username");
                    table.AddHeaderCell("Email");
                    table.AddHeaderCell("Name");
                    table.AddHeaderCell("Family");
                    table.AddHeaderCell("Phone");
                    table.AddHeaderCell("Signature");

                    foreach (var user in users)
                    {
                        table.AddCell(user.Id.ToString());
                        table.AddCell(user.Username);
                        table.AddCell(user.Email);
                        table.AddCell(user.Name);
                        table.AddCell(user.Family);
                        table.AddCell(user.Phone);

                        // Load signature image if exists
                        if (File.Exists(user.SignImagePath))
                        {
                            ImageData imageData = ImageDataFactory.Create(user.SignImagePath);
                            iText.Layout.Element.Image img = new iText.Layout.Element.Image(imageData)
                                .ScaleToFit(50, 50);
                            table.AddCell(img);
                        }
                        else
                        {
                            table.AddCell("No Image");
                        }
                    }

                    document.Add(table);
                    document.Close();
                }


                Console.WriteLine($"PDF saved to: {filePath}");
                return filePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
