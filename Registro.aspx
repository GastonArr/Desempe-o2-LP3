<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Consultoría_Legal.Registro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="tarjeta formulario-registro">
        <h2>Registro de usuarios/clientes</h2>
        <p>Complete los datos solicitados para acceder a la gestión de documentos legales.</p>

        <%-- ValidationSummary agrupa los errores producidos por los controles de validación vistos en clase. --%>
        <asp:ValidationSummary ID="ValidationSummaryRegistro" runat="server" CssClass="resumen-validacion" HeaderText="Revise los siguientes datos:" />

        <div class="campo-formulario">
            <asp:Label ID="LabelCorreo" runat="server" AssociatedControlID="txtCorreo" Text="Correo electrónico" />
            <asp:TextBox ID="txtCorreo" runat="server" />
            <%-- RequiredFieldValidator se usa para que el correo sea obligatorio. --%>
            <asp:RequiredFieldValidator ID="RequiredCorreo" runat="server" ControlToValidate="txtCorreo" CssClass="mensaje-error" ErrorMessage="El correo electrónico es obligatorio." Display="Dynamic" Text="*" />
            <%-- RegularExpressionValidator valida el formato básico del correo electrónico. --%>
            <asp:RegularExpressionValidator ID="RegularCorreo" runat="server" ControlToValidate="txtCorreo" CssClass="mensaje-error" ErrorMessage="Ingrese un correo electrónico válido." Display="Dynamic" Text="*" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
        </div>

        <div class="campo-formulario">
            <asp:Label ID="LabelUsuario" runat="server" AssociatedControlID="txtUsuario" Text="Nombre de usuario" />
            <asp:TextBox ID="txtUsuario" runat="server" />
            <%-- RequiredFieldValidator se usa para que el nombre de usuario sea obligatorio. --%>
            <asp:RequiredFieldValidator ID="RequiredUsuario" runat="server" ControlToValidate="txtUsuario" CssClass="mensaje-error" ErrorMessage="El nombre de usuario es obligatorio." Display="Dynamic" Text="*" />
        </div>

        <div class="campo-formulario">
            <asp:Label ID="LabelEdad" runat="server" AssociatedControlID="txtEdad" Text="Edad" />
            <asp:TextBox ID="txtEdad" runat="server" />
            <%-- RequiredFieldValidator se usa para que la edad sea obligatoria. --%>
            <asp:RequiredFieldValidator ID="RequiredEdad" runat="server" ControlToValidate="txtEdad" CssClass="mensaje-error" ErrorMessage="La edad es obligatoria." Display="Dynamic" Text="*" />
            <%-- RangeValidator controla que la edad sea un número entero entre 16 y 120. El máximo 120 se usa como límite superior razonable porque RangeValidator necesita mínimo y máximo. --%>
            <asp:RangeValidator ID="RangeEdad" runat="server" ControlToValidate="txtEdad" CssClass="mensaje-error" ErrorMessage="La edad debe ser un número entre 16 y 120." Display="Dynamic" Text="*" Type="Integer" MinimumValue="16" MaximumValue="120" />
        </div>

        <div class="campo-formulario">
            <asp:Label ID="LabelClave" runat="server" AssociatedControlID="txtClave" Text="Contraseña" />
            <asp:TextBox ID="txtClave" runat="server" TextMode="Password" />
            <%-- RequiredFieldValidator se usa para que la contraseña sea obligatoria. --%>
            <asp:RequiredFieldValidator ID="RequiredClave" runat="server" ControlToValidate="txtClave" CssClass="mensaje-error" ErrorMessage="La contraseña es obligatoria." Display="Dynamic" Text="*" />
        </div>

        <div class="campo-formulario">
            <asp:Label ID="LabelConfirmarClave" runat="server" AssociatedControlID="txtConfirmarClave" Text="Confirmación de contraseña" />
            <asp:TextBox ID="txtConfirmarClave" runat="server" TextMode="Password" />
            <%-- RequiredFieldValidator se usa para que la confirmación de contraseña sea obligatoria. --%>
            <asp:RequiredFieldValidator ID="RequiredConfirmarClave" runat="server" ControlToValidate="txtConfirmarClave" CssClass="mensaje-error" ErrorMessage="La confirmación de contraseña es obligatoria." Display="Dynamic" Text="*" />
            <%-- CompareValidator compara la contraseña con su confirmación. --%>
            <asp:CompareValidator ID="CompareClaves" runat="server" ControlToValidate="txtConfirmarClave" ControlToCompare="txtClave" CssClass="mensaje-error" ErrorMessage="Las contraseñas deben coincidir." Display="Dynamic" Text="*" />
        </div>

        <div class="campo-formulario">
            <asp:Label ID="LabelNombreApellido" runat="server" AssociatedControlID="txtNombreApellido" Text="Nombre y Apellido" />
            <asp:TextBox ID="txtNombreApellido" runat="server" />
            <%-- RequiredFieldValidator se usa para que el nombre y apellido sean obligatorios. --%>
            <asp:RequiredFieldValidator ID="RequiredNombreApellido" runat="server" ControlToValidate="txtNombreApellido" CssClass="mensaje-error" ErrorMessage="El nombre y apellido son obligatorios." Display="Dynamic" Text="*" />
        </div>

        <asp:Button ID="ButtonRegistrar" runat="server" CssClass="boton" Text="Registrar" OnClick="ButtonRegistrar_Click" />
        <asp:Label ID="LabelMensaje" runat="server" CssClass="mensaje-informativo" />
    </section>
</asp:Content>
