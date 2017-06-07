<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddArt.aspx.cs" Inherits="Diseño.AddArt" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
    <link rel="shortcut icon" href="Imagenes/skytex.ico" />
    <title>SkyWizard | Agregar Registro</title>
    <link type="text/css" rel="stylesheet" href="Content/bootstrap.css" />
    <!-- carga de archivos JS -->
    <script type="text/javascript" src="Scripts/modal.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.js"></script>   
    <%--<script language="Javascript">
        function confirmar() {
            confirmar = confirm("¿Te gusta nuestra web?");
            if (confirmar)
                // si pulsamos en aceptar
                alert('Has dicho que si');
            else
                // si pulsamos en cancelar
                alert('Has dicho que no');
        }
    </script>--%>
    <script type="text/javascript">
        var datefield = document.createElement("input")
        datefield.setAttribute("type", "date")
        if (datefield.type != "date") { //if browser doesn't support input type="date", load files for jQuery UI Date Picker
            document.write('<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />\n')
            document.write('<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"><\/script>\n')
            document.write('<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"><\/script>\n')
        }
    </script>

    <script>
        if (datefield.type != "date") { //if browser doesn't support input type="date", initialize date picker widget:
            jQuery(function ($) { //on document.ready
                $('#tb1fecha').datepicker();
                $('#tb1fecha').datepicker('setDate', new Date());
                $('#tb1fecha').datepicker("option", "dateFormat", 'dd/mm/yy');
            })
        }
    </script>
</head>
<body>
    <div class="container-fluid">
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server"  Skin="Bootstrap" />
        <form id="form1" runat="server">

            <div style="font-style: italic;">
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                <telerik:RadWizard ID="RadWizard1" Runat="server" OnFinishButtonClick="RadWizard1_FinishButtonClick" OnInit="RadWizard1_Init" OnNextButtonClick="RadWizard1_NextButtonClick1" OnPreviousButtonClick="RadWizard1_PreviousButtonClick" DisplayNavigationButtons="False">
                </telerik:RadWizard>
                <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" CssClass="btn btn-success" OnClick="btnFinalizar_Click"/>
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnFinalizar_Click"/>
                <br />
                <asp:ImageButton ID="btnAtras" runat="server" CssClass="btn btn-danger" CausesValidation="False" CommandName="MovePrevious" ImageUrl="~/Imagenes/flechaAtras.png" OnClick="btnAtras_Click1"/>
                <asp:ImageButton ID="btnAdelante" runat="server" CssClass="btn btn-primary" CausesValidation="False" CommandName="MoveNext"  ImageUrl="~/Imagenes/flechaAdelante.png" OnClick="btnAdelante_Click"/>
                <br />
                <telerik:RadTicker ID="RadTicker1" runat="server" Visible="False">
                </telerik:RadTicker>
                <asp:Label ID="Label1" runat="server" style="font-family: Andalus; font-size: medium; color: #000000; text-align: center; font-weight: 700; background-color: #996600" Text="Label" Visible="False"></asp:Label>
                <br />
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" ViewStateMode="Enabled" EnablePageMethods="true"></asp:ScriptManager>
        </form>        
    </div>
</body>
</html>
