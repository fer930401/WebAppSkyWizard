<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Acciones.aspx.cs" Inherits="Diseño.Acciones" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
    <link rel="shortcut icon" href="Imagenes/skytex.ico" />
    <title>SkyWizard | Acciones</title>
    <link type="text/css" rel="stylesheet" href="Content/bootstrap.css" />
    <!-- carga de archivos JS -->
    <script type="text/javascript" src="Scripts/modal.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.js"></script>   
    
    <script type="text/javascript">
        $(function () {
            $("[id*='btn_elimina']").click(function () {
                $.ajaxSetup({
                    global: false,
                    type: "GET",
                    beforeSend: function () {
                        $(".modal").show();
                    },
                    complete: function () {
                        $(".modal").hide(1000000);
                    }
                });
                $.ajax({
                    data: "{}"
                });
            });
        });
        $(function () {
            $("[id*='_bEliminar']").click(function () {
                $.ajaxSetup({
                    global: false,
                    type: "GET",
                    beforeSend: function () {
                        $(".modal").show();
                    },
                    complete: function () {
                        $(".modal").hide(1000000);
                    }
                });
                $.ajax({
                    data: "{}"
                });
            });
        });
        
    </script>
    <style type="text/css">
        .modal
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 145px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 128px;
            width: 128px;
        }
    </style>
</head>
<body>
    <div class="container-fluid">
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server"  Skin="Bootstrap" />
        <form id="form1" runat="server">
            <asp:ScriptManager runat="server" ViewStateMode="Enabled" EnablePageMethods="true"></asp:ScriptManager>
            <div style="font-style: italic;">
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                <telerik:RadWizard ID="RadWizard1" Runat="server" OnFinishButtonClick="RadWizard1_FinishButtonClick" OnInit="RadWizard1_Init" OnNextButtonClick="RadWizard1_NextButtonClick1" OnPreviousButtonClick="RadWizard1_PreviousButtonClick" DisplayNavigationButtons="False"></telerik:RadWizard>
                <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar Proceso" CssClass="btn btn-success" OnClick="btnFinalizar_Click" OnClientClick="if ( !confirm('A guardado los cambios realizados?')) return false;" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnFinalizar_Click" OnClientClick="if ( !confirm('Desea salir del proceso actual?')) return false;"  /> 
                <br />
                <asp:ImageButton ID="btnAtras" runat="server" CssClass="btn btn-danger" CausesValidation="False" CommandName="MovePrevious" ImageUrl="~/Imagenes/flechaAtras.png" OnClick="btnAtras_Click1"/>
                <asp:ImageButton ID="btnAdelante" runat="server" CssClass="btn btn-primary" CausesValidation="False" CommandName="MoveNext"  ImageUrl="~/Imagenes/flechaAdelante.png" OnClick="btnAdelante_Click"/>
                <br />
                <telerik:RadTicker ID="RadTicker1" runat="server" Visible="False">
                </telerik:RadTicker>
                <asp:Label ID="Label1" runat="server" style="font-family: Andalus; font-size: medium; color: #000000; text-align: center; font-weight: 700; background-color: #996600" Text="Label" Visible="False"></asp:Label>
                <br />
            </div>    
        </form> 
        <div class="modal" style="display: none">
            <div class="center">
                <img alt="" src="Imagenes/loading.gif" width="150" height="150" />
            </div>
        </div>
    </div>
</body>
</html>
