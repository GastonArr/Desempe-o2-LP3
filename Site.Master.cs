using System;
using System.Web;
using System.Web.UI;

namespace Consultoría_Legal
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EliminarCookieAjenaALaConsigna();
        }

        private void EliminarCookieAjenaALaConsigna()
        {
            // Se elimina la cookie ajs_anonymous_id porque no pertenece a la consigna; puede quedar en el navegador por herramientas externas o pruebas anteriores en localhost.
            // Para este desempeño solo deben verse la cookie "clave" y la cookie automática ASP.NET_SessionId que ASP.NET usa para mantener la Session.
            if (this.Request.Cookies["ajs_anonymous_id"] != null)
            {
                HttpCookie cookieAjena = new HttpCookie("ajs_anonymous_id");
                cookieAjena.Expires = DateTime.Now.AddDays(-1);
                cookieAjena.Path = "/";
                this.Response.Cookies.Add(cookieAjena);
            }
        }
    }
}
