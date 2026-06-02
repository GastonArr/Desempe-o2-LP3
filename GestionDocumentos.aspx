<%@ Page Title="Gestión de Documentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionDocumentos.aspx.cs" Inherits="Consultoría_Legal.GestionDocumentos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="tarjeta gestion-documentos">
        <h2>Gestión de Documentos Legales</h2>
        <asp:Label ID="LabelBienvenida" runat="server" CssClass="mensaje-bienvenida" />
        <asp:Label ID="LabelMensaje" runat="server" CssClass="mensaje-informativo" />

        <div class="campo-formulario campo-upload">
            <asp:Label ID="LabelSeleccion" runat="server" AssociatedControlID="FileUploadDocumento" Text="Seleccione un documento legal" />
            <asp:FileUpload ID="FileUploadDocumento" runat="server" />
        </div>

        <asp:Button ID="ButtonSubirDocumento" runat="server" CssClass="boton" Text="Subir documento" OnClick="ButtonSubirDocumento_Click" />

        <div class="datos-archivo">
            <h3>Datos del documento</h3>
            <p><strong>Nombre del archivo:</strong> <asp:Label ID="LabelNombreArchivo" runat="server" Text="-" /></p>
            <p><strong>Tamaño en bytes:</strong> <asp:Label ID="LabelTamanoArchivo" runat="server" Text="-" /></p>
            <p><strong>Tipo de archivo:</strong> <asp:Label ID="LabelTipoArchivo" runat="server" Text="-" /></p>
        </div>
    </section>
</asp:Content>
