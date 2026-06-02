using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Consultoría_Legal
{
    public partial class Registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                MostrarDatosGuardados();
            }
        }

        private void MostrarDatosGuardados()
        {
            // Se recupera el usuario desde Session para mostrarlo en el TextBox Nombre de usuario durante el tiempo que dure la sesión.
            if (this.Session["usuario"] != null)
            {
                this.txtUsuario.Text = this.Session["usuario"].ToString();
            }

            // Se recupera la contraseña desde la cookie para mostrarla en el TextBox Contraseña solamente como demostración académica.
            if (this.Request.Cookies["clave"] != null)
            {
                // Se cambia temporalmente el TextMode a SingleLine para que el profesor pueda ver el valor recuperado desde la cookie.
                // En un sistema real no se debe mostrar una contraseña en pantalla.
                this.txtClave.TextMode = TextBoxMode.SingleLine;
                this.txtClave.Text = this.Request.Cookies["clave"].Value;
            }
        }

        protected void ButtonRegistrar_Click(object sender, EventArgs e)
        {
            // this.IsValid verifica que todos los validadores ASP.NET del formulario se hayan cumplido antes de ejecutar la lógica.
            if (this.IsValid)
            {
                // La sesión se configura con duración de 1 minuto para cumplir la consigna de mostrar el usuario por un lapso temporal breve.
                this.Session.Timeout = 1;

                // Se guarda el nombre de usuario en Session para mantenerlo temporalmente durante la navegación del usuario.
                this.Session["usuario"] = this.txtUsuario.Text;

                // Se guarda la contraseña en una cookie solamente porque la consigna lo solicita. En un sistema real no se debe guardar una contraseña en una cookie sin protección.
                HttpCookie cookieClave = new HttpCookie("clave", this.txtClave.Text);
                // No se configura Expires para que la cookie quede sin tiempo determinado en el código, según lo pedido para la demostración de la clase.
                this.Response.Cookies.Add(cookieClave);

                // Después del clic se muestra el usuario recuperado desde Session en el TextBox Nombre de usuario.
                this.txtUsuario.Text = this.Session["usuario"].ToString();

                // Después del clic se muestra la contraseña recuperada desde la cookie creada en el TextBox Contraseña.
                // Se cambia a SingleLine solo para que el valor sea visible en la demostración solicitada por la consigna.
                this.txtClave.TextMode = TextBoxMode.SingleLine;
                this.txtClave.Text = cookieClave.Value;

                this.LabelMensaje.Text = "Registro correcto. Usuario mostrado desde Session por 1 minuto y contraseña mostrada desde Cookie sin tiempo determinado configurado.";
            }
        }
    }
}
