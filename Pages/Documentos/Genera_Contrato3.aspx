<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Genera_Contrato3.aspx.cs" Inherits="IT_Contratos.Pages.Documentos.Genera_Contrato3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Genera Contrato</title>
        <!-- Agrega aquí tus referencias a hojas de estilo, scripts o cualquier otro recurso necesario -->
    </head>
    <body>
        <div>
            <!-- Agrega aquí tus controles ASP.NET, como botones, listas desplegables, etc. -->
            <asp:DropDownList ID="DDLRepresentante" runat="server"></asp:DropDownList>
            <asp:CheckBoxList ID="CheckBoxListEmpleados" runat="server"></asp:CheckBoxList>
            <input type="text" id="frelacion" runat="server" />
            <input type="text" id="fcontrato" runat="server" />
            <input type="text" id="NombreDocumento" runat="server" />
            <asp:DropDownList ID="DDLHorario" runat="server"></asp:DropDownList>
            <asp:Button ID="btnGenerate" runat="server" Text="Generar Contrato" OnClick="btnGenerate_Click" />
        </div>
    </body>
    </html>

</asp:Content>
