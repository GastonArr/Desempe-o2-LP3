<%@ Page Title="Gestión de Documentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionDocumentos.aspx.cs" Inherits="Consultoría_Legal.GestionDocumentos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="tarjeta gestion-documentos">
        <h2>Gestión de Documentos Legales</h2>
        <asp:Label ID="LabelBienvenida" runat="server" CssClass="mensaje-bienvenida" />
        <asp:Label ID="LabelMensaje" runat="server" CssClass="mensaje-informativo" />

        <asp:Panel ID="PanelGestion" runat="server" CssClass="espacio-documentos">
            <div class="resumen-espacio">
                <h3>Espacio privado de archivos</h3>
                <p>
                    Los documentos se guardan en una carpeta exclusiva del usuario dentro de App_Data,
                    por lo que no quedan publicados directamente desde el navegador.
                </p>
            </div>

            <div class="campo-formulario campo-upload">
                <asp:Label ID="LabelSeleccion" runat="server" AssociatedControlID="FileUploadDocumento" Text="Seleccione un documento legal" />
                <asp:FileUpload ID="FileUploadDocumento" runat="server" />
                <small class="ayuda-campo">Formatos permitidos: PDF, DOC, DOCX y TXT. Tamaño máximo: 5 MB.</small>
            </div>

            <asp:Button ID="ButtonSubirDocumento" runat="server" CssClass="boton" Text="Subir documento" OnClick="ButtonSubirDocumento_Click" />

            <div class="datos-archivo">
                <h3>Datos del último documento cargado</h3>
                <p><strong>Nombre del archivo:</strong> <asp:Label ID="LabelNombreArchivo" runat="server" Text="-" /></p>
                <p><strong>Tamaño en bytes:</strong> <asp:Label ID="LabelTamanoArchivo" runat="server" Text="-" /></p>
                <p><strong>Tipo de archivo:</strong> <asp:Label ID="LabelTipoArchivo" runat="server" Text="-" /></p>
            </div>

            <div class="documentos-usuario">
                <h3>Mis documentos cargados</h3>
                <asp:GridView ID="GridViewDocumentos" runat="server" AutoGenerateColumns="False" CssClass="tabla-documentos" EmptyDataText="Aún no tiene documentos cargados.">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Archivo" />
                        <asp:BoundField DataField="Tamano" HeaderText="Tamaño" />
                        <asp:BoundField DataField="FechaCarga" HeaderText="Fecha de carga" />
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
    </section>
</asp:Content>
