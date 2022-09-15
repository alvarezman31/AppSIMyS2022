using AppSIMyS.Models;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Nodes;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Cell = iText.Layout.Element.Cell;
using Document = iText.Layout.Document;
using Image = iText.Layout.Element.Image;
using Path = System.IO.Path;
using PdfDocument = iText.Kernel.Pdf.PdfDocument;
using Rectangle = iText.Kernel.Geom.Rectangle;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace AppSIMyS.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormatoServicio : ContentPage
    {
        public static string EmpresaActual;  
        public static byte[] LogoEmpresa;
        public static IEnumerable<ClsEmpresas> EmpresaActiva;
        public FormatoServicio(string usuario, string RutCliente)
        {
            InitializeComponent();
            LbUsuario.Text = usuario;
            LbRutCliente.Text = RutCliente;
            LLenarClientes();
            EmpresaActual = RutCliente;
            EmpresaActiva = App.SQLiteDB.GetClsEmpresasByRutAsync2(RutCliente);
            

            //insert into serv_servicios(cliente, descripcion, tecnico) values('12345678', 'prueba de inclusión', '1');
            //SELECT `AUTO_INCREMENT` FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'alvalucc_appserv' AND TABLE_NAME = 'serv_servicios';
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);

        }

        public async void LLenarClientes()
        {
            var GetLstcliente = await App.SQLiteDB.GetClientesAsync();

            if (GetLstcliente != null)
            {
                ListClientes.ItemsSource = GetLstcliente;
            }


        }
        public async void Save(object sender, EventArgs eventArgs)
        {
            /*Stream stream = await signature.GetImageStreamAsync(SignatureImageFormat.Jpeg);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            string Firma = Convert.ToBase64String(bytes);
            */

            Stream sig = await signature.GetImageStreamAsync(SignatureImageFormat.Png);
            var signatureMemoryStream = sig as MemoryStream;
            byte[] data = signatureMemoryStream.ToArray();

            sig = await signatureCliente.GetImageStreamAsync(SignatureImageFormat.Png);
            signatureMemoryStream = sig as MemoryStream;
            byte[] dataCliente = signatureMemoryStream.ToArray();

            //var strokesSignature = signatureCliente.Strokes;

            //signature2.Strokes = strokesSignature;
            //ImgFirma.Source = "data:image/jpg;base64," + Firma; // Convert.ToBase64String(data);
            ClsServicios servicios = new ClsServicios();
            servicios.Cliente = LbRutCliente.Text.Trim();
            servicios.Tecnico = LbUsuario.Text.Trim();
            servicios.Observacion = TxtObservacion.Text.Trim();
            Conectar.GuardarServicio(servicios);
            ClsDetServicios detservicios = new ClsDetServicios();
            detservicios.IdServicio = 1;
            detservicios.IdTipoServicio = Convert.ToInt32(LbTipoServicio.Text.Trim());
            Conectar.GuardarDetalleServicio(detservicios);

            GenerarPdfFormatoServicio(data, dataCliente);

            //Clientes cliente = new Clientes();
            //cliente.Rut = "26848758-1";
            //cliente.Empresa = "26848758";
            //cliente.Descripcion = "Freddy Gudiño";
            //cliente.Direccion = "Eyzaguirre 766";

            //await App.SQLiteDB.SaveCliente(cliente);

            //await DisplayAlert("Registro", "Guardado Existosamente", "OK");

            //var GetLstcliente = await App.SQLiteDB.GetClientesAsync();

            //if (GetLstcliente != null)
            //{
            //    LstCliente.ItemsSource = GetLstcliente;
            //}

            //ListClientes.ItemsSource=GetLstcliente;
        }



        protected void GenerarPdfFormatoServicio(byte[] Firma, byte[] FirmaCliente)
        {
            Random nro = new Random();
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OT" + nro.Next(200000, 999999).ToString() + ".pdf"), root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OT" + nro.Next(200000, 999999).ToString() + ".pdf");

            fileName = "OT" + nro.Next(200000, 999999).ToString() + ".pdf";

            //Save the document to the stream
            //MemoryStream stream = new MemoryStream();
            //PdfWriter writer = new PdfWriter(stream);
            //PdfDocument pdf = new PdfDocument(writer.SetSmartMode(true));
            //Document document = new Document(pdf, iText.Kernel.Geom.PageSize.LETTER);


            //Paragraph header = new Paragraph("HEADER").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16);
            //document.Add(header);
            //header = new Paragraph("HEADER").SetTextAlignment(TextAlignment.CENTER).SetFontSize(18);
            //document.Add(header);
            //header = new Paragraph("HEADER").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
            //document.Add(header);
            //header = new Paragraph("HEADER").SetTextAlignment(TextAlignment.CENTER).SetFontSize(22);
            //document.Add(header);
            //document.Close();
            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            document.SetMargins(100f, 70f, 100f, 70f);


            // encabezado 
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1());

            document.Add(new Paragraph("Lorem Ipsum ..."));
            document.Add(new Paragraph(Convert.ToBase64String(Firma)));


            float[] pointColumnWidths = { 100F, 150F, 100F, 150F, 100F };
            Table table = new Table(pointColumnWidths);
            table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("Firma Técnico").SetTextAlignment(TextAlignment.CENTER)));
            table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));
            table.AddCell(new Cell().Add(new Paragraph("Firma Cliente").SetTextAlignment(TextAlignment.CENTER)));
            table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));

            table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));

            Image img = new Image(ImageDataFactory
           .Create(Firma))
           .SetTextAlignment(TextAlignment.CENTER).SetHeight(50).SetWidth(50);
            document.Add(new Paragraph("Firma Técnico").SetTextAlignment(TextAlignment.CENTER));
            document.Add(img);

            table.AddCell(new Cell().Add(img).SetHorizontalAlignment(HorizontalAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));

            img = new Image(ImageDataFactory
           .Create(FirmaCliente))
           .SetTextAlignment(TextAlignment.CENTER).SetHeight(50).SetWidth(50);
            document.Add(new Paragraph("Cliente").SetTextAlignment(TextAlignment.CENTER));
            document.Add(img);
            table.AddCell(new Cell().Add(img).SetHorizontalAlignment(HorizontalAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));


            document.Add(table);

            /*
                        ImageData imageData = ImageDataFactory.Create("./resources/logo_dcservicios.png");

                        iText.Layout.Element.Image image = new iText.Layout.Element.Image(imageData);

                        document.Add(image);
            */
            //ImageData imageData = ImageDataFactory.Create(Firma);

            //iText.Layout.Element.Image image = new iText.Layout.Element.Image(imageData);

            //document.Add(image);


            //var image20 = new Xamarin.Forms.Image { Source = "logo.PNG" };
            //            var assembly = this.GetType().GetTypeInfo().Assembly;
            //            byte[] imgByteArray = null;

            //            using (var s = assembly.GetManifestResourceStream("logo.PNG"))
            //            {
            //                if (s != null)
            //                {
            //                    var length = s.Length;
            //                    imgByteArray = new byte[length];
            //                    s.Read(imgByteArray, 0, (int)length);


            document.Close();

            //Save the stream as a file in the device and invoke it for viewing
            DependencyService.Get<ISavePdf>().SaveAndView(fileName, "application/pdf", stream, root);

            cmSendMailCcopy(Path.Combine(root, fileName));
            //cmSendMailCcopy(Path.Combine("/AppSimys", fileName));


        }


        protected void GenerarPdfFormatoServicio2(byte[] Firma)
        {
            Random nro = new Random();

            //string root = Environment.ex .ExternalStorageDirectory.ToString();

            //root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //fileName = "OT" + nro.Next(200000, 999999).ToString() + ".pdf";
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OT" + nro.Next(200000, 999999).ToString() + ".pdf");
            //fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            // Create a new PDF document

            //PdfDocument document = new PdfDocument();

            //Add a page to the document
            //PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            //PdfGraphics graphics = page.Graphics;

            //Stream imageStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("App2.img.logo_dcservicios.PNG");

            //PdfBitmap image = new PdfBitmap(imageStream);
            ////Draw the image
            //graphics.DrawImage(image, 0, 0);


            //Set the standard font
            //PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            //Draw the text
            //graphics.DrawString(TxtObservacion.Text, font, PdfBrushes.Black, new PointF(0, 0));
            //graphics.DrawString(LbConCopia.Text, font, PdfBrushes.Black, new PointF(50, 25));
            //graphics.DrawString(LbConCopia.Text+"12121212121221", font, PdfBrushes.Black, new PointF(100, 50));


            //Save the document to the stream
            //byte[] byteArray = Encoding.UTF8.GetBytes(fileName);
            // MemoryStream stream = new MemoryStream();

            //Stream str = GenerateStreamFromString(filename);
            //  document.Save(stream);            

            //Close the document
            //  document.Close(true);


            //byte[] bytes = new byte[stream.Length];
            //stream.Read(bytes, 0, bytes.Length);
            //string Firma = Convert.ToBase64String(bytes);


            //using (FileStream fs = new FileStream(fileName, FileMode.Create))
            //{
            //    fs.Write(bytes, 0, bytes.Length);
            //}



            //string filename = Configuraciones.PathApp + "/HolaMundo";
            //var archivo = Path.Combine(filename, "Prueba.pdf");

            //byte[] byteArray = Encoding.UTF8.GetBytes(fileName);
            //stream = new MemoryStream(byteArray);
            //stream.Write(bytes, 0, bytes.Length);
            //stream.Close();


            // Conectar.GuardarFirma(Firma, 14237889);

            //Save the stream as a file in the device and invoke it for viewing
            //Random nro = new Random();

            fileName = "OT" + nro.Next(200000, 999999).ToString() + ".pdf";

            //Save the document to the stream
            MemoryStream stream = new MemoryStream();
            // document.Save(stream);

            //Close the document
            //document.Close(true);

            //Save the stream as a file in the device and invoke it for viewing
            Xamarin.Forms.DependencyService.Get<ISavePdf>().SaveAndView(fileName, "application/pdf", stream, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            cmSendMailCcopy(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName));
            //cmSendMailCcopy(Path.Combine("/AppSimys", fileName));
        }

        protected void cmSendMailCcopy(string file)
        {
            string mailTo;
            string mailCopy;
            string mailFrom;
            string mailSubject;
            string mailBody;
            string mailAuthentication;
            string mailPassword;
            string mailSmtpServer;

            List<string> mailAttachment = null;
            //string filename = Configuraciones.PathApp + "/HolaMundo";
            //string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.pdf");
            //var archivo = Path.Combine(filename, "Prueba.pdf");


            /* public void Send(string mailTo, string mailCopy, string mailFromAddress, string mailFromName,
                          string mailSubject, string mailBody, string mailAuthentication, string mailPassword,
                          string mailSmtpServer, int mailPort, List<string> mailAttachment = null)
             {*/
            try
            {
                MailMessage mail = new MailMessage();

                string mailDe = "manuelalvarezl@hotmail.com";
                string deNombre = "Manuel Alvarez";
                string mailPara = "manuelalvarez3108@gmail.com";
                string mailCopia = LbConCopia.Text.Trim();// "manuelalvarez3108@gmail.com";
                string asunto = "Prueba envio Xamarin";
                string mensaje = "Prueba de Envio";
                string mailAutenticacion = "manuelalvarezl@hotmail.com";
                string mailContra = "M@@l*2209";
                string mailSmtp = "smtp.office365.com";
                int mailPuerto = 587;
                List<string> mailAdjunto = mailAttachment;

                try
                {
                    //mailAdjunto.Add(file);
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = mailSmtp,
                        Port = mailPuerto,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(mailDe, mailContra),
                        Timeout = 3000
                    };

                    MailMessage correo = new MailMessage(mailDe, mailPara, asunto, mensaje);
                    correo.IsBodyHtml = true;
                    if (mailCopia != "")
                        correo.CC.Add(mailCopia);

                    if (mailAdjunto != null)
                    {
                        foreach (var item in mailAdjunto)
                        {
                            if (System.IO.File.Exists(item))
                            {
                                correo.Attachments.Add(new Attachment(item, MediaTypeNames.Application.Pdf));
                            }
                        }
                    }

                    if (file != "" || file != null)
                    {
                        /*   byte[] stemp = Convert.FromBase64String(tRespuesta.PDFResultado); 
                           Stream stream = new MemoryStream(stemp);
                           correo.Attachments.Add(new Attachment(stream, Path.GetFileName("prueba.pdf"), "application/pdf"));*/
                        correo.Attachments.Add(new Attachment(file, MediaTypeNames.Application.Pdf));
                    }
                    smtp.Send(correo);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            //}
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            //int selectedIndex = 
            LbTipoServicio.Text = picker.SelectedIndex.ToString();
        }

        private void ListClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            //int selectedIndex = 
            Clientes dt = picker.SelectedItem as Clientes;

            LbTipoServicio.Text = dt.Empresa;// .ItemsSource[picker.SelectedIndex].ToString(); //picker.SelectedItem.ToString();
            //LbTipoServicio.Text = picker.ItemsSource[picker.SelectedIndex].ToString(); //picker.SelectedItem.ToString();
        }

        public class HeaderEventHandler1 : IEventHandler
        {
            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent pdfEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = pdfEvent.GetDocument();
                PdfPage page = pdfEvent.GetPage();

                Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 70, page.GetPageSize().GetRight() - 70, 50);
                Canvas canvas = new Canvas(page, rootArea);
                canvas
                    .Add(getTable(pdfEvent));
                /*
                         .ShowTextAligned("Este es el Encabezado de págna", 10, 0, TextAlignment.CENTER)
                         .ShowTextAligned("Este es el pie de pagina", 10, 0, TextAlignment.CENTER)
                         .ShowTextAligned("texto agregado", 10, 0, TextAlignment.RIGHT)
                         .Close();
                */


            }

            public Table getTable(PdfDocumentEvent docEvent)
            {
                float[] pointColumnWidths = { 100F, 150F, 100F, 150F, 100F };
                Table table = new Table(pointColumnWidths);
                table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph("Firma Técnico" + EmpresaActual ).SetTextAlignment(TextAlignment.CENTER)));
                table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell().Add(new Paragraph("Firma Cliente").SetTextAlignment(TextAlignment.CENTER)));
                table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));                
                return table;
            }


        }
    }
}