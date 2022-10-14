using AppSIMyS.Models;
using AppSIMyS.Services;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Plugin.Media.Abstractions;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Cell = iText.Layout.Element.Cell;
using Document = iText.Layout.Document;
using Image = iText.Layout.Element.Image;
using Path = System.IO.Path;
using PdfDocument = iText.Kernel.Pdf.PdfDocument;
using Rectangle = iText.Kernel.Geom.Rectangle;
using TextAlignment = iText.Layout.Properties.TextAlignment;
//using Plugin.Media.Abstractions;
//using Plugin.Media;

namespace AppSIMyS.ViewModels
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormatoServicio : ContentPage
    {
        public static string EmpresaActual;
        //public static byte[] LogoEmpresaAct;
        public static ClsEmpresas EmpresaActiva;
        public static string nroOrden;
        public static string TipoInforme;


        public FormatoServicio(string usuario, string RutCliente, ImageSource LogoEmpresa)
        {
            InitializeComponent();
            BindingContext = new FormatoServicioViewModel();
            LbUsuario.Text = usuario;
            LbRutCliente.Text = RutCliente;
            LLenarClientes();
            EmpresaActual = RutCliente;
            //LogoEmpresaAct = LogoEmpresa;
            EmpresaActiva = new ClsEmpresas();
            //LstImagenes = new ObservableCollection<ImagenServicio>();
            //ImagenServicio imagenServicio = new ImagenServicio();
            //imagenServicio.Imagen = null;
            //imagenServicio.Comentario = "111111";
            //imagenServicio.Empresa = "111111";
            ////LstImagenes.Add(imagenServicio);
            //MyCollectionView.ItemsSource = LstImagenes;

            var EmpresaActiva1 = App.SQLiteDB.GetClsEmpresasByRutAsync2(RutCliente);



            foreach (var item in EmpresaActiva1)
            {
                //Xamarin.Forms.Image image1 = new Xamarin.Forms.Image();
                //Byte[] bindata;
                //bindata = (byte[])(item.Logo);
                //LogoEmpresaAct = bindata;

                EmpresaActiva.Logo = item.Logo;
                EmpresaActiva.PiePagina = item.PiePagina;
                EmpresaActiva.Rut = item.Rut;
                EmpresaActiva.Descripcion = item.Descripcion;
                EmpresaActiva.Direccion = item.Direccion;
                EmpresaActiva.Telefono = item.Telefono;
                EmpresaActiva.Email = item.Email;

                //image1.Source = ImageSource.FromStream(() => new MemoryStream(bindata));

                //Empresas empresa = new Empresas();
                //empresa.Rut = item.Rut;
                //empresa.Descripcion = item.Descripcion;
                //empresa.Empresa = item.Empresa;
                //empresa.Telefono = "Tel. " + item.Telefono;
                //empresa.Direccion = "Dir. " + (item.Direccion.Trim() + "                                                                  ").Substring(0, 50).Trim();
                //empresa.Logo = image1.Source;
                //EmpresaActiva.Add(empresa);
            }

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

            nroOrden = nro.Next(20000000, 99999999).ToString();
            fileName = "OT" + nroOrden + ".pdf";

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
            document.SetMargins(120f, 70f, 100f, 70f);


            // encabezado 
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1());

            //document.Add(new Paragraph("Lorem Ipsum ..."));
            //document.Add(new Paragraph(Convert.ToBase64String(Firma)));

            var ImagenesServicios = App.SQLiteDB.GetTblImagenesByAsync2();
            if (ImagenesServicios.Count() > 0)
            {
                float[] tablaImagenesWith = { 300F, 200F };
                Table tablaImagenes = new Table(tablaImagenesWith);
                tablaImagenes.AddCell(new Cell().Add(new Paragraph("Imagen").SetTextAlignment(TextAlignment.CENTER)));
                tablaImagenes.AddCell(new Cell().Add(new Paragraph("Comentario").SetTextAlignment(TextAlignment.CENTER)));
                Image img2;
                foreach (var item in ImagenesServicios)
                {
                    img2 = new Image(ImageDataFactory
           .Create(item.Imagen))
           .SetTextAlignment(TextAlignment.CENTER).SetHeight(50).SetWidth(50);
                    tablaImagenes.AddCell(new Cell().Add(img2).SetHorizontalAlignment(HorizontalAlignment.CENTER));
                    tablaImagenes.AddCell(new Cell().Add(new Paragraph(item.Comentario)).SetBorder(Border.NO_BORDER));
                }
                document.Add(tablaImagenes);
            }




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


        //protected void GenerarPdfFormatoServicio2(byte[] Firma)
        //{
        //    Random nro = new Random();

        //    //string root = Environment.ex .ExternalStorageDirectory.ToString();

        //    //root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //    //fileName = "OT" + nro.Next(200000, 999999).ToString() + ".pdf";
        //    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OT" + nro.Next(200000, 999999).ToString() + ".pdf");
        //    //fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //    //root = Android.OS.Environment.ExternalStorageDirectory.ToString();
        //    // Create a new PDF document

        //    //PdfDocument document = new PdfDocument();

        //    //Add a page to the document
        //    //PdfPage page = document.Pages.Add();

        //    //Create PDF graphics for the page
        //    //PdfGraphics graphics = page.Graphics;

        //    //Stream imageStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("App2.img.logo_dcservicios.PNG");

        //    //PdfBitmap image = new PdfBitmap(imageStream);
        //    ////Draw the image
        //    //graphics.DrawImage(image, 0, 0);


        //    //Set the standard font
        //    //PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

        //    //Draw the text
        //    //graphics.DrawString(TxtObservacion.Text, font, PdfBrushes.Black, new PointF(0, 0));
        //    //graphics.DrawString(LbConCopia.Text, font, PdfBrushes.Black, new PointF(50, 25));
        //    //graphics.DrawString(LbConCopia.Text+"12121212121221", font, PdfBrushes.Black, new PointF(100, 50));


        //    //Save the document to the stream
        //    //byte[] byteArray = Encoding.UTF8.GetBytes(fileName);
        //    // MemoryStream stream = new MemoryStream();

        //    //Stream str = GenerateStreamFromString(filename);
        //    //  document.Save(stream);            

        //    //Close the document
        //    //  document.Close(true);


        //    //byte[] bytes = new byte[stream.Length];
        //    //stream.Read(bytes, 0, bytes.Length);
        //    //string Firma = Convert.ToBase64String(bytes);


        //    //using (FileStream fs = new FileStream(fileName, FileMode.Create))
        //    //{
        //    //    fs.Write(bytes, 0, bytes.Length);
        //    //}



        //    //string filename = Configuraciones.PathApp + "/HolaMundo";
        //    //var archivo = Path.Combine(filename, "Prueba.pdf");

        //    //byte[] byteArray = Encoding.UTF8.GetBytes(fileName);
        //    //stream = new MemoryStream(byteArray);
        //    //stream.Write(bytes, 0, bytes.Length);
        //    //stream.Close();


        //    // Conectar.GuardarFirma(Firma, 14237889);

        //    //Save the stream as a file in the device and invoke it for viewing
        //    //Random nro = new Random();

        //    fileName = "OT" + nroOrden + ".pdf";

        //    //Save the document to the stream
        //    MemoryStream stream = new MemoryStream();
        //    // document.Save(stream);

        //    //Close the document
        //    //document.Close(true);

        //    //Save the stream as a file in the device and invoke it for viewing
        //    Xamarin.Forms.DependencyService.Get<ISavePdf>().SaveAndView(fileName, "application/pdf", stream, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        //    cmSendMailCcopy(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName));
        //    //cmSendMailCcopy(Path.Combine("/AppSimys", fileName));
        //}

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
            TipoInforme = picker.SelectedItem.ToString();
        }

        private void ListClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            //int selectedIndex = 
            ClsClientes dt = picker.SelectedItem as ClsClientes;

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
                var document = new Document(pdfDoc);
                iText.Layout.Element.Image img;
                img = new Image(ImageDataFactory.Create(EmpresaActiva.Logo)).SetTextAlignment(TextAlignment.CENTER).SetHeight(100).SetWidth(150);
                //document.Add(new Cell(3, 1).Add(img).SetBorder(Border.NO_BORDER));


                Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 120, page.GetPageSize().GetRight() - 70, 100);
                Canvas canvas = new Canvas(page, rootArea);
                canvas
                    .Add(getTable(pdfEvent));
                /*
                         .ShowTextAligned("Este es el Encabezado de págna", 10, 0, TextAlignment.CENTER)
                         .ShowTextAligned("Este es el pie de pagina", 10, 0, TextAlignment.CENTER)
                         .ShowTextAligned("texto agregado", 10, 0, TextAlignment.RIGHT)
                         .Close();
                */

                if (EmpresaActiva.PiePagina != null)
                {
                    //img = new Image(ImageDataFactory.Create(EmpresaActiva.PiePagina)).SetTextAlignment(TextAlignment.CENTER).SetHeight(100).SetWidth(550);
                    rootArea = new Rectangle(40, page.GetPageSize().GetBottom() + 50, page.GetPageSize().GetRight() + 50, 50);
                    canvas = new Canvas(page, rootArea);
                    canvas.Add(getTableFooter(pdfEvent));
                }

            }

            public Table getTable(PdfDocumentEvent docEvent)
            {
                /* Xamarin.Forms.Image image1 = new Xamarin.Forms.Image();
                  Byte[] bindata;
                  bindata = (byte[])(EmpresaActiva.);
                  image1.Source = ImageSource.FromStream(() => new MemoryStream(bindata));
                  */
                float[] pointColumnWidths = { 180F, 350F, 70F, 110F, 110F };

                Table table = new Table(pointColumnWidths);
                iText.Layout.Element.Image img;
                img = new Image(ImageDataFactory.Create(EmpresaActiva.Logo)).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetHeight(80).SetWidth(150);
                table.AddCell(new Cell(4, 1).Add(img).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell(2, 2).Add(new Paragraph(EmpresaActiva.Descripcion)).SetBorder(Border.NO_BORDER).SetFontSize(12).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetBold());
                table.AddCell(new Cell(1, 2).Add(new Paragraph("Informe de " + TipoInforme)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(12).SetBorderBottom(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.BOTTOM));
                table.AddCell(new Cell(1, 2).Add(new Paragraph(" Nro: OT-" + nroOrden)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(12).SetBorderTop(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.TOP));
                //table.AddCell(new Cell(1, 1).Add(new Paragraph(" Nro: OT-" + nroOrden)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(12));

                //table.AddCell(new Cell(1,2).Add(new Paragraph(TipoInforme)));                

                table.AddCell(new Cell(1, 4).Add(new Paragraph(EmpresaActiva.Direccion)).SetBorder(Border.NO_BORDER).SetFontSize(10));

                table.AddCell(new Cell(1, 4).Add(new Paragraph("Telefono: " + EmpresaActiva.Telefono + " Email: " + EmpresaActiva.Email)).SetBorder(Border.NO_BORDER).SetFontSize(10));
                //table.AddCell(new Cell(1, 1).Add(new Paragraph("")).SetBorder(Border.NO_BORDER));

                //table.AddCell(new Cell().Add(new Paragraph("222")).SetBorder(Border.NO_BORDER));
                //table.AddCell(new Cell().Add(new Paragraph("144")).SetBorder(Border.NO_BORDER));
                //table.AddCell(new Cell().Add(new Paragraph("244")).SetBorder(Border.NO_BORDER));
                //table.AddCell(new Cell().Add(new Paragraph("344")).SetBorder(Border.NO_BORDER));

                return table;
            }
            public Table getTableFooter(PdfDocumentEvent docEvent)
            {
                /* Xamarin.Forms.Image image1 = new Xamarin.Forms.Image();
                  Byte[] bindata;
                  bindata = (byte[])(EmpresaActiva.);
                  image1.Source = ImageSource.FromStream(() => new MemoryStream(bindata));
                  */
                float[] pointColumnWidths = { 1 };
                Table table = new Table(pointColumnWidths);
                iText.Layout.Element.Image img;
                img = new Image(ImageDataFactory.Create(EmpresaActiva.PiePagina)).SetTextAlignment(TextAlignment.CENTER).SetHeight(80).SetWidth(500);
                table.AddCell(new Cell().Add(img).SetBorder(Border.NO_BORDER));
                return table;
            }


        }

        //private async void TomarFoto_Clicked(object sender, EventArgs e)
        //{
        //    //var opciones_almacenamiento = new StoreCameraMediaOptions()
        //    //{
        //    //    SaveToAlbum = true,
        //    //    Name = "MiFoto.jpg"

        //    //};
        //    //var foto = await CrossMedia.Current.TakePhotoAsync(opciones_almacenamiento);
        //    //MiImagen.Source = ImageSource.FromStream(() =>
        //    //{
        //    //    var strem = foto.GetStream();
        //    //    foto.Dispose();
        //    //    return strem;
        //    //    });
        //}


        async void SeleccionarImagen_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            var cameraOptions = new PickMediaOptions();
            cameraOptions.PhotoSize = PhotoSize.Small;

            var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(cameraOptions); // opciones personalizadas
            if (photo != null)
            {
                image.Source = ImageSource.FromStream(() =>
                {
                    return photo.GetStream();
                });
                agregarImagen();

            }

            //Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            //if (stream != null)
            //{
            //    image.Source = ImageSource.FromStream(() => stream);                
            //    agregarImagen();
            //}


            //byte[] bytesAvailable = new byte[stream.Length];
            //stream.Read(bytesAvailable, 0, bytesAvailable.Length);



            //LstImagenes.Add(ImgSer);
            //MyCollectionView.ItemsSource = LstImagenes;

            ((Button)sender).IsEnabled = true;
        }

        //private async void ElegirFoto_Clicked(object sender, EventArgs e)
        //{
        //    //if (CrossMedia.Current.IsTakePhotoSupported)
        //    //{
        //    //    var imagen = await CrossMedia.Current.PickPhotoAsync();
        //    //    if (imagen != null)
        //    //    {
        //    //        MiImagen.Source = ImageSource.FromStream(() =>
        //    //        {
        //    //            var strem = imagen.GetStream();
        //    //            imagen.Dispose();
        //    //            return strem;                        
        //    //        });
        //    //    }
        //    //}
        //}

        private async void BtnCamara_Clicked(object sender, EventArgs e)
        {
            var cameraOptions = new StoreCameraMediaOptions();
            cameraOptions.PhotoSize = PhotoSize.Small;
            //var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()); // opiones generales 
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(cameraOptions); // opciones personalizadas
            if (photo != null)
            {
                image.Source = ImageSource.FromStream(() =>
                {
                    return photo.GetStream();
                });
                //Image imagen = (Bitmap)((new ImageConverter()).ConvertFrom(photo));
                //Stream stream = await ((StreamImageSource)image.Source).Stream(CancellationToken.None);
                agregarImagen();
                //byte[] bytesAvailable = new byte[stream.Length];
                //stream.Read(bytesAvailable, 0, bytesAvailable.Length);
                //TblImagenServicio ImgSer = new TblImagenServicio();
                //ImgSer.Imagen = bytesAvailable;
                //ImgSer.Comentario = "Prueba de Galeria";
                //ImgSer.Empresa = "xxsxsxs";

            }
        }
        //public async Task<byte[]> ConvertImageSourceToBytesAsync(ImageSource imageSource)
        //{

        //byte[] imageArray = null;

        //using (MemoryStream memory = new MemoryStream())
        //{

        //    Stream stream = imageSource.GetStream();
        //    stream.CopyTo(memory);
        //    imageArray = memory.ToArray();
        //}

        //Stream stream = await ((StreamImageSource)imageSource).Stream(CancellationToken.None);
        //byte[] bytesAvailable = new byte[stream.Length];
        //stream.Read(bytesAvailable, 0, bytesAvailable.Length);

        //return bytesAvailable;
        //}
        //public byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        //{
        //    byte[] bytes = null;
        //    var bitmapSource = imageSource as BitmapSource;

        //    if (bitmapSource != null)
        //    {
        //        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

        //        using (var stream = new MemoryStream())
        //        {
        //            encoder.Save(stream);
        //            bytes = stream.ToArray();
        //        }
        //    }

        //    return bytes;
        //}
        public async void agregarImagen()
        {
            Stream stream1 = await ((StreamImageSource)image.Source).Stream(CancellationToken.None);
            byte[] bytesAvailable = new byte[stream1.Length];
            stream1.Read(bytesAvailable, 0, bytesAvailable.Length);


            TblImagenServicio ImgSer = new TblImagenServicio();
            ImgSer.Imagen = bytesAvailable;
            ImgSer.Comentario = TxtComentario.Text.Trim();// "Prueba de Galeria";
            ImgSer.Empresa = EmpresaActual;// "xxsxsxs";
            ImgSer.Id = (new Random().Next(1, 1000000));
            //App.SQLiteDB.AgregarTblImagenServicio(ImgSer);
            App.SQLiteDB.AgregarTblImagenServicio(ImgSer);

            //MyCollectionView.SetBinding(ItemsView.ItemsSourceProperty, "LstImagenes");
            BindingContext = new FormatoServicioViewModel();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            //var data = ImageButton.Parameter;
            ImageButton button1 = (ImageButton)sender;
            string commandParameter = button1.CommandParameter.ToString();

            //DisplayAlert("DEMO", "Has presionado un control ImageButton - " + commandParameter, "OK");
            //DisplayAlert("DEMO", "Has presionado un control ImageButton" + MyCollectionView.ItemsSource.ToString(), "OK");

            var action = await DisplayAlert("Advertencia!!!", "Seguro de Eliminar la Imagen Nro. " + commandParameter, "Yes", "No");
            if (action)
            {
                App.SQLiteDB.EliminarTblImagenServicio(commandParameter);
                BindingContext = new FormatoServicioViewModel();
            }

        }

        //private void EliminarImagen_Tapped(object sender, EventArgs e)
        //{
        //    var tappedEvgs = e as TappedEventArgs;
        //    var data = tappedEvgs.Parameter;
        //}
    }
}