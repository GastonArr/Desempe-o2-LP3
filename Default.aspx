<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Consultoría_Legal._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="tarjeta hero-inicio">
        <div class="hero-texto">
            <h2>Bienvenido/a a Asesoría Legal Global</h2>
            <p>
                Somos una empresa dedicada a brindar asesoramiento legal integral para clientes que necesitan
                orientación confiable, ordenada y profesional en la gestión de sus trámites y documentos.
            </p>
            <p>
                Desde este sitio podrá registrarse como usuario y acceder a un espacio simple para subir sus
                documentos legales de forma temporal, siguiendo los contenidos trabajados en clase.
            </p>
            <asp:HyperLink ID="HyperLinkRegistro" runat="server" CssClass="boton" NavigateUrl="~/Registro.aspx" Text="Registrarme" />
        </div>
        <div class="hero-imagen">
            <asp:Image ID="ImageInicio" runat="server" ImageUrl="~/imagenes/logo 3.png" AlternateText="Imagen institucional de Asesoría Legal Global" />
        </div>
    </section>
</asp:Content>
