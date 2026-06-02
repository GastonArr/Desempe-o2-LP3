using System;
using System.IO;
using System.Web.UI;

namespace Consultoría_Legal
{
    public partial class GestionDocumentos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                MostrarEstadoUsuario();
                MostrarDatosTemporales();
            }
        }

        private void MostrarEstadoUsuario()
        {
            // Se verifica Session["usuario"] para confirmar que el usuario se registró antes de gestionar documentos.
            if (this.Session["usuario"] == null)
            {
                this.LabelBienvenida.Text = "Primero debe registrarse para acceder a la gestión de documentos legales.";
                this.ButtonSubirDocumento.Enabled = false;
            }
            else
            {
                string usuario = this.Session["usuario"].ToString();
                this.LabelBienvenida.Text = "Bienvenido/a " + usuario + " a su espacio de gestión de documentos legales.";
                this.ButtonSubirDocumento.Enabled = true;
            }
        }

        private void MostrarDatosTemporales()
        {
            // Se muestra el valor guardado en Session para que se vea claramente la gestión de sesiones solicitada en la consigna.
            if (this.Session["usuario"] != null)
            {
                this.LabelUsuarioSession.Text = this.Session["usuario"].ToString();
            }
            else
            {
                this.LabelUsuarioSession.Text = "No hay usuario guardado en Session.";
            }

            // Se lee la cookie desde Request.Cookies para demostrar que fue creada y recuperada según la clase de Cookies.
            // Se muestra la contraseña en pantalla solamente con finalidad académica para evidenciar la implementación; en un sistema real no debe mostrarse ni guardarse así.
            if (this.Request.Cookies["clave"] != null)
            {
                this.LabelClaveCookie.Text = this.Request.Cookies["clave"].Value;
            }
            else
            {
                this.LabelClaveCookie.Text = "No hay clave guardada en Cookie.";
            }
        }

        protected void ButtonSubirDocumento_Click(object sender, EventArgs e)
        {
            // Antes de usar FileUpload se vuelve a verificar Session["usuario"] para asegurar que exista un usuario registrado.
            if (this.Session["usuario"] == null)
            {
                this.LabelMensaje.Text = "Primero debe registrarse para subir documentos.";
                this.ButtonSubirDocumento.Enabled = false;
                return;
            }

            // FileUpload.HasFile permite saber si el usuario seleccionó un archivo para subir.
            if (!this.FileUploadDocumento.HasFile)
            {
                this.LabelMensaje.Text = "Debe seleccionar un archivo antes de subirlo.";
                LimpiarDatosArchivo();
                return;
            }

            string usuario = this.Session["usuario"].ToString();

            // Server.MapPath convierte la ruta virtual Documentos en una ruta física dentro del sitio web.
            string rutaDocumentos = this.Server.MapPath("~/Documentos/");
            string rutaUsuario = Path.Combine(rutaDocumentos, usuario);

            // Se utiliza Directory.CreateDirectory solamente para crear el espacio propio del usuario si todavía no existe, porque la consigna pide que cada usuario tenga su propio espacio de gestión.
            Directory.CreateDirectory(rutaUsuario);

            // Se utiliza Path.GetFileName para obtener solamente el nombre del archivo y evitar rutas completas del cliente.
            string nombreArchivo = Path.GetFileName(this.FileUploadDocumento.FileName);
            string rutaArchivo = Path.Combine(rutaUsuario, nombreArchivo);

            // File.Exists valida si ya hay un documento con el mismo nombre en la carpeta del usuario.
            if (File.Exists(rutaArchivo))
            {
                this.LabelMensaje.Text = "Ya existe un archivo con ese nombre.";
                LimpiarDatosArchivo();
                return;
            }

            // SaveAs guarda físicamente el archivo seleccionado con el control FileUpload.
            this.FileUploadDocumento.SaveAs(rutaArchivo);

            this.LabelMensaje.Text = "Archivo subido correctamente.";
            this.LabelNombreArchivo.Text = nombreArchivo;
            this.LabelTamanoArchivo.Text = this.FileUploadDocumento.PostedFile.ContentLength.ToString();
            this.LabelTipoArchivo.Text = this.FileUploadDocumento.PostedFile.ContentType;
        }

        private void LimpiarDatosArchivo()
        {
            this.LabelNombreArchivo.Text = "-";
            this.LabelTamanoArchivo.Text = "-";
            this.LabelTipoArchivo.Text = "-";
        }
    }
}
