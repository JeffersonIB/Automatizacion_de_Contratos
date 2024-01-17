<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Genera_Contrato.aspx.cs" Inherits="RRHH5.Pages.Documentos.Genera_Contrato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body1" runat="server">

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Exportar a Word</title>
        <script type="text/javascript">
            var empleados = [];

            function filterEmployees() {
                var searchText = document.getElementById('txtSearch').value.toLowerCase();
                var checkboxes = document.getElementById('<%= CheckBoxListEmpleados.ClientID %>').getElementsByTagName('input');

                for (var i = 0; i < checkboxes.length; i++) {
                    var checkbox = checkboxes[i];
                    var employeeName = checkbox.parentNode.innerText.toLowerCase();
                    if (employeeName.indexOf(searchText) > -1) {
                        checkbox.parentNode.style.display = 'block';
                    } else {
                        checkbox.parentNode.style.display = 'none';
                    }
                }
            }
        </script>
        <style type="text/css">
            .scroll_checkboxes {
                height: 120px;
                width: 100%;
                padding: 5px;
                overflow: auto;
                border: 1px solid #ccc;
                display: block;
                padding: .375rem .75rem;
                font-size: 1rem;
                line-height: 1.5;
                color: #495057;
                background-color: #fff;
                background-clip: padding-box;
                border: 1px solid #ced4da;
                border-radius: .25rem;
                transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out
            }

            .scroll_checkboxess {
                height: 120px;
                width: 200px;
                padding: 5px;
                overflow: auto;
                border: 1px solid #ccc;
            }

            .FormText {
                FONT-SIZE: 11px;
                FONT-FAMILY: tahoma,sans-serif
            }
        </style>
    </head>
    <body>
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Generar contrato
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table align="center">
                        <%--<tr>
                            <td>
                                <br />
                                Sociedad:
                                <br />
                            </td>
                            <td>
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="DDLSociedad" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLSociedad_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <br />
                                Representante legal:
                                <br />
                            </td>
                            <td>
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="DDLRepresentante" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLRepresentante_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                Empleado :
                                <br />
                            </td>
                            <td>
                                <div class="scroll_checkboxes" cssclass="form-control">
                                    <input type="text" id="txtSearch" oninput="filterEmployees()" placeholder="Buscar por código o nombre" style="width: 100%;" />
                                    <asp:CheckBoxList ID="CheckBoxListEmpleados" runat="server" CssClass="FormText" DataTextField="Nom_Ape" DataValueField="Id_Empleado"></asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>Inico de relación laboral : 
                            </td>
                            <td>
                                <div>
                                    <input id="frelacion" runat="server" class="date-picker form-control" placeholder="dd-mm-yyyy" type="text" required="required" onfocus="this.type='date'" onmouseover="this.type='date'" onclick="this.type='date'" onblur="this.type='text'" onmouseout="timeFunctionLong(this)">
                                    <script>
                                        function timeFunctionLong(input) {
                                            setTimeout(function () {
                                                input.type = 'text';
                                            }, 60000);
                                        }
                                    </script>
                                </div>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td align="center">Proceso :
                            </td>
                            <td colspan="3">
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="DDLProceso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLProceso_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>--%>
                       <%-- <tr>
                            <td align="center">Puesto :
                            </td>
                            <td colspan="3">
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="DDLPuesto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLPuesto_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="center">Horario de trabajo :
                            </td>
                            <td colspan="3">
                                <div class="control">
                                    <div class="select">
                                        <asp:DropDownList ID="DDLHorario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLHorario_SelectedIndexChanged" CssClass="form-control" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>Fecha del contrato : 
                            </td>
                            <td>
                                <div>
                                    <input id="fcontrato" runat="server" class="date-picker form-control" placeholder="dd-mm-yyyy" type="text" required="required" onfocus="this.type='date'" onmouseover="this.type='date'" onclick="this.type='date'" onblur="this.type='text'" onmouseout="timeFunctionLong(this)">
                                    <script>
                                        function timeFunctionLong(input) {
                                            setTimeout(function () {
                                                input.type = 'text';
                                            }, 60000);
                                        }
                                    </script>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                Nombre del documento :
                                <br />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NombreDocumento" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <div>
                                    <asp:Button ID="btnGenerate" runat="server" class="btn btn-round btn-success" Text="Generar documento Word" OnClick="btnGenerate_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
    </body>
    </html>


</asp:Content>
