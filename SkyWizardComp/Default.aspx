<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Diseño.Default" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"> 
    <link rel="shortcut icon" href="Imagenes/skytex.ico" />
    <title>SkyWizard</title>
    <link href="/estilos/wiz.css" rel="stylesheet" type="text/css" />
    <script src="modernizr.min.js"></script>
    <link type="text/css" rel="stylesheet" href="Content/bootstrap.css" />
</head>
<body>
   
    <form id="form1" runat="server"  >
        <div class="container">
            <br />
            <div class="well">
                <div class="form-inline">
                    <div class="form-group">
                        <img src="Imagenes/skytex.png" width="100" height="30"/>
                    </div>
                    <div class="form-group">
                        <h4>Sky Wizard <small>Skytex México S.A de C.V.</small></h4>    
                    </div>
                </div>
            </div>
            <telerik:RadSkinManager ID="RadSkinManager1" runat="server"  Skin="WebBlue" />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-md-4 ">
                    .
                </div>
                <div class="col-md-4 ">
                    <img src="Imagenes/Warning.png" width="250" height="200" class="img-responsive"/>
                </div>
                <div class="col-md-4 ">
                    .
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-1">
                    .
                </div>
                <div class="col-md-10">
                    <telerik:RadAjaxPanel ID="rPanel1" runat="server" height="169px" HorizontalAlign="NotSet" LoadingPanelID="rPanel1" RenderMode="Inline" width="946px" ForeColor="#0000CC">
                        <div class="alert alert-danger" role="alert">
                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                            <span class="sr-only">Error:</span>
                                <telerik:RadTicker ID="RadTicker1" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="Large" >
                                    <Items>
                                        <telerik:RadTickerItem>Ups!! Hubo un problema de comunicación con el servidor de datos, Actualice la Página (F5),  si el problema continua consulte a su administrador</telerik:RadTickerItem>
                                    </Items>
                                </telerik:RadTicker>
                        </div>
                    </telerik:RadAjaxPanel>
                </div>
            </div>
        </div>        
    </form>
</body>
</html>
