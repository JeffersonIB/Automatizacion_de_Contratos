<%@ Page Title="" Language="C#" MasterPageFile="~/MP1.Master" AutoEventWireup="true" CodeBehind="Horarios.aspx.cs" Inherits="RRHH5.Pages.Admin.WebForm1" %>

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
        </script>
        <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
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
        <%--<p>
            <a class="btn btn-primary" data-toggle="collapse" href="#multiCollapseExample1" role="button" aria-expanded="false" aria-controls="multiCollapseExample1">Crear</a>
            <button class="btn btn-primary" type="button" data-toggle="collapse" data-target="#multiCollapseExample2" aria-expanded="false" aria-controls="multiCollapseExample2">Modificar</button>
            <button class="btn btn-primary" type="button" data-toggle="collapse" data-target=".multi-collapse" aria-expanded="false" aria-controls="multiCollapseExample1 multiCollapseExample2">Ambos</button>
        </p>--%>
        <div class="row">
            <div class="col">
                <div>
                    <div class="card card-body">
                        <%--Modal--%>
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Agregar horario
                                    </h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <table align="center">
                                        <tr>
                                            <td align="center">Horario :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="AgHorario" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">Descripción :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="AgDescHorario" CssClass="form-control" TextMode="MultiLine" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">
                                                <asp:Button runat="server" ID="Agregar" class="btn btn-round btn-primary" OnClick="Agregar_Click" Text="Agregar" />
                                                <asp:Button runat="server" ID="CancelarAg" class="btn btn-round btn-danger" OnClick="CancelarAg_Click" Text="Cancelar" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <%--Modal--%>
                    </div>
                </div>
            </div>

            <%--Ventana izquierda--%>
            <div class="col">
                <div>
                    <div class="card card-body">
                        <%--Modal--%>

                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Modificar horarios
                                    </h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <table align="center">
                                        <tr>
                                            <td align="center">Seleccionar :
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
                                            <td align="center">Horario :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ModHorario" CssClass="form-control" MaxLength="200" Style="width: 100%;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">Descripción :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ModDesHorario" CssClass="form-control" TextMode="MultiLine" Style="width: 100%;"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">
                                                <asp:Button runat="server" ID="Modificar" class="btn btn-round btn-primary" OnClick="Modificar_Click" Text="Modificar" />
                                                <asp:Button runat="server" ID="CancelarMod" class="btn btn-round btn-danger" OnClick="CancelarMod_Click" Text="Cancelar" />
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </div>
                        </div>
                        <%--Modal--%>
                    </div>
                </div>
            </div>
            <%--Ventana izquierda--%>
        </div>
    </body>
    </html>
</asp:Content>
