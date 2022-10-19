using AppSIMyS.Models;
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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading;
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
    public partial class FormatoServicio : TabbedPage
    {
        public static string? EmpresaActual;        
        public static ClsEmpresas? EmpresaActiva;
        public static string? nroOrden;
        public static string TipoInforme = "Servicio";


        public FormatoServicio(string usuario, string RutCliente)
        {
            InitializeComponent();
            BindingContext = new FormatoServicioViewModel();
            LbUsuario.Text = usuario;
            //LbRutCliente.Text = RutCliente;
            LLenarClientes();
            LLenarServicios();
            EmpresaActual = RutCliente;            
            EmpresaActiva = new ClsEmpresas();
            //LblNroServicio.Text =
            LblNroServicio.Text = App.SQLiteDB.ProximoClsServicios().ToString();
            //foreach (var item in Servicos)
            //{
                //LblNroServicio.Text = (item.NroServicio+1).ToString();
            //}
            
            var EmpresaActiva1 = App.SQLiteDB.GetClsEmpresasByRutAsync2(RutCliente);
            foreach (var item in EmpresaActiva1)
            {                
                EmpresaActiva.Logo = item.Logo;
                EmpresaActiva.PiePagina = item.PiePagina;
                EmpresaActiva.Rut = item.Rut;
                EmpresaActiva.Descripcion = item.Descripcion;
                EmpresaActiva.Direccion = item.Direccion;
                EmpresaActiva.Telefono = item.Telefono;
                EmpresaActiva.Email = item.Email;
            }
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
        public async void LLenarServicios()
        {
            var GetLstServicios = await App.SQLiteDB.GetServiciosAsync();
            if (GetLstServicios != null)
            {
                ListServicios.ItemsSource = GetLstServicios;
            }
        }
        public async void Save(object sender, EventArgs eventArgs)
        {
            if (LbDescripcionCliente.Text.Trim()=="")
            {
                await DisplayAlert("Advertencia", "Debe seleccionar los datos del cliente", "OK");
                ListClientes.Focus();
                return;
            }
            if (LbEmalCliente.Text.Trim()=="")
            {
                await DisplayAlert("Advertencia", "Debe colocar Email del cliente", "OK");
                LbEmalCliente.Focus();
                return;
            }
            Stream sig = await signature.GetImageStreamAsync(SignatureImageFormat.Png);
            if (sig == null)
            {
                await DisplayAlert("Advertencia", "Falta firma del técnico", "OK");
                return;
            }
            var signatureMemoryStream = sig as MemoryStream;
            byte[]? data = signatureMemoryStream.ToArray();


            sig = await signatureCliente.GetImageStreamAsync(SignatureImageFormat.Png);
            if (sig == null)
            {
                await DisplayAlert("Advertencia", "Falta firma del Cliente", "OK");
                return;
            }
            signatureMemoryStream = sig as MemoryStream;
            byte[]? dataCliente = signatureMemoryStream.ToArray();

            ClsServicios servicios = new ClsServicios();
            servicios.NroServicio = Convert.ToInt32(LblNroServicio.Text);
            servicios.Cliente = LbRutCliente.Text.Trim();
            servicios.Tecnico = LbUsuario.Text.Trim();
            servicios.Observacion = TxtObservacion.Text.Trim();
            servicios.Fecha = DateTime.Now;
            App.SQLiteDB.AgregarClsServicios(servicios);
            //Conectar.GuardarServicio(servicios);
            
            
            //ClsDetServicios detservicios = new ClsDetServicios();
            //detservicios.IdServicio = 1;
            //detservicios.IdTipoServicio = Convert.ToInt32(LbTipoServicio.Text.Trim());
            //Conectar.GuardarDetalleServicio(detservicios);

            GenerarPdfFormatoServicio(data, dataCliente);
        }



        protected void GenerarPdfFormatoServicio(byte[] Firma, byte[] FirmaCliente)
        {
            Random nro = new Random();
            //string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OT" + nro.Next(200000, 999999).ToString() + ".pdf"),
            string root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OT" + nro.Next(200000, 999999).ToString() + ".pdf");

            nroOrden = nro.Next(20000000, 99999999).ToString();
            nroOrden = LblNroServicio.Text.ToString().PadLeft(8,'0');
            fileName = "OT" + nroOrden + ".pdf";

            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            document.SetMargins(120f, 50f, 100f, 50f);

            // encabezado 
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1());

            string tecnico = "";
            var UsuariosActual = App.SQLiteDB.GetUsuariosByRutAsync2(LbUsuario.Text.Trim());
            if (UsuariosActual.Count() > 0)
            {
                foreach (var item in UsuariosActual)
                {
                    tecnico = (item.nombres == null ? "" : item.nombres);
                }
            }
            //var ClienteActual = App.SQLiteDB.GetClientesByRutAsync2(LbRutCliente.Text.Trim());
            //if (ClienteActual.Count() > 0)
            //{
                float[] ClientesWith = { 70F, 330F, 70F, 300F };
                Table tablaCliente = new Table(ClientesWith);                
                //foreach (var item in ClienteActual)
                //{
                    
                    tablaCliente.AddCell(new Cell().Add(new Paragraph("Empresa:").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph(LbDescripcionCliente.Text).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    //tablaCliente.AddCell(new Cell().Add(new Paragraph(item.Descripcion).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph("Fecha:").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph(DateTime.Now.ToString("dd/MM/yyyy")).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));

                    tablaCliente.AddCell(new Cell().Add(new Paragraph("Dirección:").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph(LbDireccionCliente.Text).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    //tablaCliente.AddCell(new Cell().Add(new Paragraph(item.Direccion).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph("Teléfono:").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph(LbTelefonoCliente.Text).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    //tablaCliente.AddCell(new Cell().Add(new Paragraph(item.Telefono).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));

                    tablaCliente.AddCell(new Cell().Add(new Paragraph("Rut:").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph(LbRutCliente.Text).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    //tablaCliente.AddCell(new Cell().Add(new Paragraph(item.Rut).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph("Ciudad:").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph("Santiago").SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    
                    tablaCliente.AddCell(new Cell().Add(new Paragraph("Atención Sr:").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10)).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph(LbConctacoCliente.Text).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    //tablaCliente.AddCell(new Cell().Add(new Paragraph((item.Persona1 == null ? "" : item.Persona1)).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph("Técnico:").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10)).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    tablaCliente.AddCell(new Cell().Add(new Paragraph(tecnico).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));


                //}
                document.Add(tablaCliente);
                document.Add(new Paragraph("").SetFontSize(5));
            //}
            var ServiciosActuales = App.SQLiteDB.GetDetServiciosByRutAsync2();
            if (ServiciosActuales.Count() > 0)
            {
                float[] ServiciosWith = { 70F, 600F};
                Table tablaServicios  = new Table(ServiciosWith);
                tablaServicios.AddCell(new Cell().Add(new Paragraph("Cant.").SetFontSize(10)).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER));
                tablaServicios.AddCell(new Cell().Add(new Paragraph("Descripción").SetFontSize(10)).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER));
                
                foreach (var item in ServiciosActuales)
                {

                    tablaServicios.AddCell(new Cell().Add(new Paragraph(item.Cantidad.ToString()).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                    tablaServicios.AddCell(new Cell().Add(new Paragraph(item.Descripcion).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                    
                }
                tablaServicios.AddCell(new Cell().Add(new Paragraph("").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10)).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetHeight(1));
                tablaServicios.AddCell(new Cell().Add(new Paragraph("").SetTextAlignment(TextAlignment.LEFT).SetFontSize(10)).SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetHeight(1));

                document.Add(tablaServicios);
                document.Add(new Paragraph("").SetFontSize(5));
            }


            var ImagenesServicios = App.SQLiteDB.GetTblImagenesByAsync2();
            if (ImagenesServicios.Count() > 0)
            {
                document.Add(new Paragraph("Area de Imagenes").SetFontSize(10));
                float[] tablaImagenesWith = { 300F, 200F };
                Table tablaImagenes = new Table(tablaImagenesWith);
                tablaImagenes.AddCell(new Cell().Add(new Paragraph("Imagen").SetTextAlignment(TextAlignment.CENTER)));
                tablaImagenes.AddCell(new Cell().Add(new Paragraph("Comentario").SetTextAlignment(TextAlignment.CENTER)));
                Image img2;
                foreach (var item in ImagenesServicios)
                {
                    img2 = new Image(ImageDataFactory
           .Create(item.Imagen))
           .SetTextAlignment(TextAlignment.CENTER).SetHeight(150).SetWidth(150);
                    tablaImagenes.AddCell(new Cell().Add(img2).SetHorizontalAlignment(HorizontalAlignment.CENTER));
                    tablaImagenes.AddCell(new Cell().Add(new Paragraph(item.Comentario)).SetVerticalAlignment(VerticalAlignment.MIDDLE));
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
            //document.Add(new Paragraph("Firma Técnico").SetTextAlignment(TextAlignment.CENTER));
            //document.Add(img);

            table.AddCell(new Cell().Add(img).SetHorizontalAlignment(HorizontalAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));

            img = new Image(ImageDataFactory
           .Create(FirmaCliente))
           .SetTextAlignment(TextAlignment.CENTER).SetHeight(50).SetWidth(50);
           // document.Add(new Paragraph("Cliente").SetTextAlignment(TextAlignment.CENTER));
            //document.Add(img);
            table.AddCell(new Cell().Add(img).SetHorizontalAlignment(HorizontalAlignment.CENTER));
            table.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));

            document.Add(table);
            document.Close();

            //Save the stream as a file in the device and invoke it for viewing
            DependencyService.Get<ISavePdf>().SaveAndView(fileName, "application/pdf", stream, root);

            TblArchivoServicios tblArchivoServicios = new TblArchivoServicios();
            tblArchivoServicios.Extension = ".pdf";
            tblArchivoServicios.Mime = "application/pdf";
            tblArchivoServicios.Nombre = fileName;
            tblArchivoServicios.Archivo2 = stream.ToArray();
            tblArchivoServicios.NroServicio = Convert.ToInt32(LblNroServicio.Text);
            tblArchivoServicios.Archivo = "/img/iconopdf.jpg";
            App.SQLiteDB.AgregarTblArchivoServicios(tblArchivoServicios);
            Conectar.GuardarArchivoServicio(tblArchivoServicios);

            cmSendMailCcopy(Path.Combine(root, fileName));
            //cmSendMailCcopy(Path.Combine("/AppSimys", fileName));
        }

        protected void cmSendMailCcopy(string file)
        {
            //string mailTo;
            //string mailCopy;
            //string mailFrom;
            //string mailSubject;
            //string mailBody;
            //string mailAuthentication;
            //string mailPassword;
            //string mailSmtpServer;

            List<string>? mailAttachment = null;
            
            try
            {
                MailMessage mail = new MailMessage();

                string mailDe = "manuelalvarezl@hotmail.com";
                //string deNombre = "Manuel Alvarez";
                string mailPara = LbEmalCliente.Text.Trim();
                string mailCopia = LbConCopia.Text.Trim();// "manuelalvarez3108@gmail.com";
                string asunto = "Prueba envio Xamarin";
                string mensaje = "Prueba de Envio";
                string mailAutenticacion = "manuelalvarezl@hotmail.com";
                string mailContra = "M@@l*2209";
                string mailSmtp = "smtp.office365.com";
                int mailPuerto = 587;
                List<string>? mailAdjunto = mailAttachment;

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
            ClsClientes? dt = picker.SelectedItem as ClsClientes;
            if (dt != null)
            {
                LbTipoServicio.Text = dt.Empresa;// .ItemsSource[picker.SelectedIndex].ToString(); //picker.SelectedItem.ToString();
                LbRutCliente.Text = dt.Rut;
                LbDescripcionCliente.Text = (dt.Descripcion == null ? "" : dt.Descripcion);
                LbDireccionCliente.Text = (dt.Direccion == null ? "" : dt.Direccion);
                LbTelefonoCliente.Text = (dt.Telefono == null ? "" : dt.Telefono);
                LbConctacoCliente.Text = (dt.Persona1 == null ? "" : dt.Persona1);
                LbEmalCliente.Text = (dt.Email == null ? "" : dt.Email);
            }     
        }

        public class HeaderEventHandler1 : IEventHandler
        {

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent pdfEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = pdfEvent.GetDocument();
                PdfPage page = pdfEvent.GetPage();
                var document = new Document(pdfDoc);
                iText.Layout.Element.Image img = new Image(ImageDataFactory.Create(EmpresaActiva.Logo)).SetTextAlignment(TextAlignment.CENTER).SetHeight(100).SetWidth(150);


                Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 120, page.GetPageSize().GetRight() - 70, 100);
                Canvas canvas = new Canvas(page, rootArea);
                canvas
                    .Add(getTable(pdfEvent));
         
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
                
                float[] pointColumnWidths = { 180F, 350F, 70F, 110F, 110F };

                Table table = new Table(pointColumnWidths);
                iText.Layout.Element.Image img;
                img = new Image(ImageDataFactory.Create(EmpresaActiva.Logo)).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetHeight(80).SetWidth(150);
                table.AddCell(new Cell(4, 1).Add(img).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER));
                table.AddCell(new Cell(2, 2).Add(new Paragraph(EmpresaActiva.Descripcion)).SetBorder(Border.NO_BORDER).SetFontSize(12).SetVerticalAlignment(VerticalAlignment.BOTTOM).SetBold());
                table.AddCell(new Cell(1, 2).Add(new Paragraph("Informe de " + TipoInforme)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(12).SetBorderBottom(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.BOTTOM));
                table.AddCell(new Cell(1, 2).Add(new Paragraph(" Nro: OT-" + nroOrden)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(12).SetBorderTop(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.TOP));

                table.AddCell(new Cell(1, 4).Add(new Paragraph(EmpresaActiva.Direccion)).SetBorder(Border.NO_BORDER).SetFontSize(10));

                table.AddCell(new Cell(1, 4).Add(new Paragraph("Telefono: " + EmpresaActiva.Telefono + " Email: " + EmpresaActiva.Email)).SetBorder(Border.NO_BORDER).SetFontSize(10));


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

      
        async void SeleccionarImagen_Clicked(object sender, EventArgs e)
        {
            if (TxtComentario.Text.Trim() == "")
            {
                await DisplayAlert("Advertencia", "Debe Agregar Comentario para la Imagen", "OK");
                return;
            }
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

            ((Button)sender).IsEnabled = true;
        }

      

        private async void BtnCamara_Clicked(object sender, EventArgs e)
        {
            if (TxtComentario.Text=="")
            {
                await DisplayAlert("Advertencia", "Debe Agregar Comentario para la Imagen", "OK");
                return;
            }
            var cameraOptions = new StoreCameraMediaOptions();
            cameraOptions.PhotoSize = PhotoSize.Small;
            cameraOptions.SaveToAlbum = true;
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(cameraOptions); // opciones personalizadas
            if (photo != null)
            {
                image.Source = ImageSource.FromStream(() =>
                {
                    return photo.GetStream();
                });
                agregarImagen();
      
            }
        }
      
        public async void agregarImagen()
        {
            
            Stream stream1 = await ((StreamImageSource)image.Source).Stream(CancellationToken.None);
            byte[] bytesAvailable = new byte[stream1.Length];
            stream1.Read(bytesAvailable, 0, bytesAvailable.Length);
            TblImagenServicio ImgSer = new TblImagenServicio();
            ImgSer.NroServicio = -9999;
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
            var Data = (ImagenServicio)button1.BindingContext;
            string commandParameter = button1.CommandParameter.ToString();
            switch (commandParameter)
            {
                case "A":                    
                    App.SQLiteDB.ActualizarTblImagenServicio(Data.Id,Data.Comentario);
                    await DisplayAlert("Advertencia", "Comentario Actualizado ", "OK");
                    BindingContext = new FormatoServicioViewModel();
                    break;
                case "E":
                    var action = await DisplayAlert("Advertencia!!!", "Seguro de Eliminar la Imagen Nro. " + Data.Id, "Yes", "No");
                    if (action)
                    {
                        App.SQLiteDB.EliminarTblImagenServicio(Data.Id);
                        BindingContext = new FormatoServicioViewModel();
                    }
                    break;
                default:
                    break;
            }            
        }
        private void ListClientes_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void ListServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            TblServicios? dt = picker.SelectedItem as TblServicios;
            if (dt != null)
            {
                LblCodServicio.Text = dt.Codigo;
                TxtDescServicio.Text = dt.Descripcion;
            }
        }

        private void BtnAgregarServicio_Clicked(object sender, EventArgs e)
        {
            if (LblCodServicio.Text.Trim()=="")
                {
                DisplayAlert("Advertencia", "Debe seleccionar un servicio", "OK");
                return;
            }
            ClsDetServicios ImgSer = new ClsDetServicios();
            ImgSer.NroServicio = -9999;
            ImgSer.Codigo= LblCodServicio.Text;
            ImgSer.Comentario = TxtComentarioServicio.Text.Trim();// "Prueba de Galeria";
            ImgSer.Cantidad = Convert.ToInt32(NroCantidad.Text);
            ImgSer.Descripcion = TxtDescServicio.Text;
            //App.SQLiteDB.AgregarTblImagenServicio(ImgSer);
            App.SQLiteDB.AgregarClsDetServicios(ImgSer);
            BindingContext = new FormatoServicioViewModel();
            LblCodServicio.Text="";
        }

        private async void ImageButtonServicios_Clicked(object sender, EventArgs e)
        {
            ImageButton button1 = (ImageButton)sender;
            var Data = (ClsDetServicios)button1.BindingContext;
            string commandParameter = button1.CommandParameter.ToString();
            switch (commandParameter)
            {
                case "A":
                    App.SQLiteDB.ActualizarClsDetServicios(Data.Id, Data.Comentario, Data.Cantidad);
                    await DisplayAlert("Advertencia", "Registro Actualizado ", "OK");
                    BindingContext = new FormatoServicioViewModel();
                    break;
                case "E":
                    var action = await DisplayAlert("Advertencia!!!", "Seguro de eliminar " + Data.Descripcion, "Yes", "No");
                    if (action)
                    {
                        App.SQLiteDB.EliminarClsDetServicios(Data.Id);
                        BindingContext = new FormatoServicioViewModel();
                    }
                    break;
                default:
                    break;
            }
        }

        private void BtnNuevo_Clicked(object sender, EventArgs e)
        {
            App.SQLiteDB.EliminarClsDetServicios();
            App.SQLiteDB.EliminarTblImagenServicio();
            BindingContext = new FormatoServicioViewModel();

        }
    }
}