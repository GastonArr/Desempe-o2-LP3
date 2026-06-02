using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace Consultoría_Legal
{
    public partial class GestionDocumentos : Page
    {
        private const int TamanoMaximoDocumento = 5 * 1024 * 1024;
        private static readonly string[] ExtensionesPermitidas = { ".pdf", ".doc", ".docx", ".txt" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                MostrarEstadoUsuario();
                CargarDocumentosUsuario();
            }
        }

        private void MostrarEstadoUsuario()
        {
            // Se verifica Session["usuario"] para confirmar que el usuario inició sesión antes de gestionar documentos.
            if (this.Session["usuario"] == null)
            {
                this.LabelBienvenida.Text = "Primero debe iniciar sesión o registrarse para acceder a la gestión de documentos legales.";
                this.LabelMensaje.Text = "Use la opción Registro para identificarse; luego será dirigido a su espacio privado de archivos.";
                this.PanelGestion.Visible = false;
                this.ButtonSubirDocumento.Enabled = false;
            }
            else
            {
                string usuario = this.Session["usuario"].ToString();
                this.LabelBienvenida.Text = "Bienvenido/a " + usuario + " a su espacio privado de gestión de documentos legales.";
                this.LabelMensaje.Text = string.Empty;
                this.PanelGestion.Visible = true;
                this.ButtonSubirDocumento.Enabled = true;
            }
        }

        protected void ButtonSubirDocumento_Click(object sender, EventArgs e)
        {
            // Antes de usar FileUpload se vuelve a verificar Session["usuario"] para asegurar que exista un usuario autenticado.
            if (this.Session["usuario"] == null)
            {
                this.LabelMensaje.Text = "Primero debe iniciar sesión o registrarse para subir documentos.";
                this.PanelGestion.Visible = false;
                this.ButtonSubirDocumento.Enabled = false;
                return;
            }

            // FileUpload.HasFile permite saber si el usuario seleccionó un archivo para subir.
            if (!this.FileUploadDocumento.HasFile)
            {
                this.LabelMensaje.Text = "Debe seleccionar un archivo antes de subirlo.";
                LimpiarDatosArchivo();
                CargarDocumentosUsuario();
                return;
            }

            string nombreOriginal = Path.GetFileName(this.FileUploadDocumento.FileName);
            string extension = Path.GetExtension(nombreOriginal).ToLowerInvariant();
            int tamanoArchivo = this.FileUploadDocumento.PostedFile.ContentLength;

            if (!ExtensionesPermitidas.Contains(extension))
            {
                this.LabelMensaje.Text = "El formato del documento no está permitido. Use PDF, DOC, DOCX o TXT.";
                LimpiarDatosArchivo();
                CargarDocumentosUsuario();
                return;
            }

            if (tamanoArchivo <= 0 || tamanoArchivo > TamanoMaximoDocumento)
            {
                this.LabelMensaje.Text = "El documento debe tener contenido y no superar los 5 MB.";
                LimpiarDatosArchivo();
                CargarDocumentosUsuario();
                return;
            }

            string rutaUsuario = ObtenerRutaUsuario();

            // Se utiliza Directory.CreateDirectory solamente para crear el espacio propio del usuario si todavía no existe.
            Directory.CreateDirectory(rutaUsuario);

            string nombreSeguro = CrearNombreArchivoSeguro(nombreOriginal);
            string rutaArchivo = Path.Combine(rutaUsuario, nombreSeguro);

            // SaveAs guarda físicamente el archivo seleccionado en la carpeta privada del usuario.
            this.FileUploadDocumento.SaveAs(rutaArchivo);

            this.LabelMensaje.Text = "Archivo subido correctamente en su espacio privado.";
            this.LabelNombreArchivo.Text = nombreSeguro;
            this.LabelTamanoArchivo.Text = tamanoArchivo.ToString();
            this.LabelTipoArchivo.Text = this.FileUploadDocumento.PostedFile.ContentType;
            CargarDocumentosUsuario();
        }

        private string ObtenerRutaUsuario()
        {
            string usuario = this.Session["usuario"].ToString();
            string usuarioSeguro = SanitizarNombre(usuario);

            // Server.MapPath convierte la ruta virtual App_Data/DocumentosLegales en una ruta física no pública dentro del sitio web.
            string rutaDocumentos = this.Server.MapPath("~/App_Data/DocumentosLegales/");
            return Path.Combine(rutaDocumentos, usuarioSeguro);
        }

        private static string CrearNombreArchivoSeguro(string nombreOriginal)
        {
            string nombreSinExtension = Path.GetFileNameWithoutExtension(nombreOriginal);
            string extension = Path.GetExtension(nombreOriginal).ToLowerInvariant();
            string nombreSeguro = SanitizarNombre(nombreSinExtension);
            string marcaTiempo = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            return marcaTiempo + "_" + nombreSeguro + extension;
        }

        private static string SanitizarNombre(string valor)
        {
            string limpio = new string(valor
                .Select(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' ? c : '_')
                .ToArray())
                .Trim('_');

            if (string.IsNullOrWhiteSpace(limpio))
            {
                return "documento";
            }

            return limpio.Length > 80 ? limpio.Substring(0, 80) : limpio;
        }

        private void CargarDocumentosUsuario()
        {
            if (this.Session["usuario"] == null || this.GridViewDocumentos == null)
            {
                return;
            }

            string rutaUsuario = ObtenerRutaUsuario();
            List<DocumentoUsuario> documentos = new List<DocumentoUsuario>();

            if (Directory.Exists(rutaUsuario))
            {
                documentos = Directory.GetFiles(rutaUsuario)
                    .Select(ruta => new FileInfo(ruta))
                    .OrderByDescending(archivo => archivo.CreationTimeUtc)
                    .Select(archivo => new DocumentoUsuario
                    {
                        Nombre = archivo.Name,
                        Tamano = archivo.Length + " bytes",
                        FechaCarga = archivo.CreationTime.ToString("dd/MM/yyyy HH:mm")
                    })
                    .ToList();
            }

            this.GridViewDocumentos.DataSource = documentos;
            this.GridViewDocumentos.DataBind();
        }

        private void LimpiarDatosArchivo()
        {
            this.LabelNombreArchivo.Text = "-";
            this.LabelTamanoArchivo.Text = "-";
            this.LabelTipoArchivo.Text = "-";
        }

        private class DocumentoUsuario
        {
            public string Nombre { get; set; }

            public string Tamano { get; set; }

            public string FechaCarga { get; set; }
        }
    }
}
