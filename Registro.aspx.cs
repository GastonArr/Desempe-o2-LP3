using System;
using System.Web;
using System.Web.UI;

namespace Consultoría_Legal
{
    public partial class Registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonRegistrar_Click(object sender, EventArgs e)
        {
            // this.IsValid verifica que todos los validadores ASP.NET del formulario se hayan cumplido antes de ejecutar la lógica.
            if (this.IsValid)
            {
                // Se guarda el nombre de usuario en Session para mantenerlo temporalmente durante la navegación del usuario.
                this.Session["usuario"] = this.txtUsuario.Text;

                // Se guarda la contraseña en una cookie solamente porque la consigna lo solicita. En un sistema real no se debe guardar una contraseña en una cookie sin protección.
                HttpCookie cookieClave = new HttpCookie("clave", this.txtClave.Text);
                // La cookie se agrega a la respuesta para que el navegador la almacene temporalmente según lo visto en la clase de Cookies.
                this.Response.Cookies.Add(cookieClave);

                // Response.Redirect envía al usuario a la página de gestión de documentos después del registro correcto.
                this.Response.Redirect("GestionDocumentos.aspx");
            }
        }
    }
}
