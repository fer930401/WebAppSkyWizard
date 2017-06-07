
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoBusqueda.aspx.cs" Inherits="Diseño.AutoBusqueda" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%--<%@ OutputCache Duration="16000" VaryByParam="none" %>--%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"> 
    <link rel="shortcut icon" href="Imagenes/skytex.ico" />
    <title>SkyWizard | Inicio</title>
    <link type="text/css" rel="stylesheet" href="Content/bootstrap.css" /> 
    
    <style type="text/css">
        .btnPrimary {
          color: white !important;
          background-color: #337ab7 !important;
          border-color: #2e6da4 !important;
        }
 
        .btnHover {
          color: white !important;
          background-color: #286090 !important;
          border-color: #204d74 !important;
        }
         
        .RadButton {
            font-size: 14px !important;
        }
         
        span.rbPrimaryIcon,
        span.rbSecondaryIcon {
            width: 1.33em;
            height: 1.33em;
        }
    </style>
</head>
<body>
    <script type="text/javascript" >
        var isDoubleClick = false;

        var clickHandler = null;

        var ClickedIndex = null; // newly added

        function RowClick(sender, args) {
            ClickedIndex = args._itemIndexHierarchical; // newly added
            isDoubleClick = false;
            if (clickHandler) {
                window.clearTimeout(clickHandler);
                clickHandler = null;
            }
            //clickHandler = window.setTimeout(ActualClick, 200);
        }

        function RowDblClick(sender, args) {
            ClickedIndex = args._itemIndexHierarchical; // newly added
            isDoubleClick = true;
            if (clickHandler) {
                window.clearTimeout(clickHandler);
                clickHandler = null;
            }
            clickHandler = window.setTimeout(ActualClick, 200);
        }
        function ActualClick() {
            if (isDoubleClick) {
                var grid = $find("<%=RadGrid1.ClientID %>");
                  if (grid) {
                      var MasterTable = grid.get_masterTableView();
                      var Rows = MasterTable.get_dataItems();
                      for (var i = 0; i < Rows.length; i++) {
                          var row = Rows[i];
                          if (ClickedIndex != null && ClickedIndex == i) { // newly added
                              var artTipo;
                              var artClave;
                              var fila = parseInt(ClickedIndex) + 1;

                              $("#<%=RadGrid1.ClientID %>").find('tr:eq(' + fila + ')').find('td:eq(0)').each(function () {
                                  valor = $(this).html();
                                  artTipo = valor;
                              })
                              $("#<%=RadGrid1.ClientID %>").find('tr:eq(' + fila + ')').find('td:eq(2)').each(function () {
                                  valor2 = $(this).html();
                                  artClave = valor2;
                              })
                              var datos = artTipo + "&" + artClave;
                              MasterTable.fireCommand("dbClick", datos); // newly added
                          } // newly added
                      }
                  }
              }
              else {
                  var grid = $find("<%=RadGrid1.ClientID %>");
                  if (grid) {
                      var MasterTable = grid.get_masterTableView();
                      var Rows = MasterTable.get_dataItems();
                      for (var i = 0; i < Rows.length; i++) {
                          var row = Rows[i];
                          if (ClickedIndex != null && ClickedIndex == i) { // newly added
                              MasterTable.fireCommand("smClick", ClickedIndex); // newly added
                          } // newly added
                      }
                  }

              }
          }
    </script>
    <div class="container-fluid">
    <!--
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
    -->
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" Skin="Bootstrap"/>
    <form id="form1" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <telerik:RadWizard ID="RadWizard1" Runat="server" DisplayNavigationButtons="false" OnPreviousButtonClick="RadWizard1_PreviousButtonClick" Width="98%">
            <WizardSteps>
                <telerik:RadWizardStep ID="RadWizardStep1" runat="server" Title="Busqueda de Registros">
                    <asp:Panel ID="Panel1" runat="server" OnInit="Panel1_Init" Height="400px" Width="95%">
                        <div class="row">
                                <div class="col-md-3">
                                    <div class="input-group"> 
                                        <label>Tipo de artículo:</label><br />
                                        <telerik:RadComboBox ID="cmbArtTip1" Runat="server" AllowCustomText="False" AutoPostBack="True" DropDownAutoWidth="Enabled" MarkFirstMatch="True" OnSelectedIndexChanged="cmbArtTip1_SelectedIndexChanged" Width="100%" ></telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-group"> 
                                        <label><asp:Label ID="lblTipo1" runat="server" Text="Tipo:"></asp:Label></label>
                                        <telerik:RadComboBox ID="cmbTipo1" Runat="server" AppendDataBoundItems="True" AllowCustomText="False" AutoPostBack="True" DropDownAutoWidth="Enabled" MarkFirstMatch="True" OnSelectedIndexChanged="cmbTipo1_SelectedIndexChanged" Width="100%"></telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="input-group"> 
                                        <label>Nombre del artículo:</label>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtName_TextChanged" onKeyDown="return tab_btn(event);" Width="145%"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                                    <br />
                                    <telerik:RadButton ID="RadButton1" ButtonType="SkinnedButton" runat="server" OnClick="RadButton1_Click"  Text="Agregar Art." CssClass="btn btnPrimary" Icon-PrimaryIconCssClass="glyphicon glyphicon-plus"></telerik:RadButton>
                                </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <telerik:RadGrid ID="RadGrid1" runat="server" Culture="es-ES" Height="300px" Skin="Silk" OnItemCommand="RadGrid1_ItemCommand" width="100%">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True"/>
                                        <ClientEvents OnRowDblClick="RowDblClick" OnRowClick="RowClick"  />
                                    </ClientSettings>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" /><clientsettings>
                                    <Scrolling AllowScroll="True" /></clientsettings>
                                    <MasterTableView>
                                        <HeaderStyle BorderStyle="None" Height="48px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <NoRecordsTemplate>
                                            <h4 style="color:Black">No se encontraron resultados, por favor verifica los datos ingresados</h4>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <br />
                                <%-- <asp:LinkButton ID="btnEditar" CssClass="btn btn-warning" runat="server" OnClick="RadGrid1_SelectedIndexChanged"><span class="glyphicon glyphicon-pencil"></span> Modificar</asp:LinkButton> --%>
                                <%-- <asp:LinkButton ID="btnEliminar" CssClass="btn btn-danger" runat="server" OnClick="RadGrid1_SelectedIndexChanged"><span class="glyphicon glyphicon-remove"></span> Eliminar</asp:LinkButton> --%>
                            </div>
                        </div>
                    </asp:Panel>
                </telerik:RadWizardStep>
            </WizardSteps>
        </telerik:RadWizard>
        
    </form>
    </div>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
    rel="Stylesheet" type="text/css" />  
     <script type="text/javascript">
         var art_tipoT = $('input:text[name=<%= cmbArtTip1.ClientID %>]').val();
         var res = art_tipoT.trim().slice(-4);
         var art_tipo = res.slice(0, 3);
         var tipo = $('input:text[name=<%= cmbTipo1.ClientID %>]').val();
         //alert(art_tipo +", "+ tipo);
         $(function () {
             $("[id$=txtName]").autocomplete({
                 source: function (request, response) {
                     $.ajax({
                         //url: '=ResolveUrl("~/Diseño/WebForm2.aspx/GetCustomers")',
                         url: '<%=ResolveUrl("WebForm2.aspx/GetCustomers")%>',
                         data: "{ 'prefix': '" + request.term + "', 'art_tipo': '" + art_tipo + "', 'tipo': '" + tipo + "'}",
                         dataType: "json",
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         success: function (data) {
                             response($.map(data.d, function (item) {
                                 return {
                                     label: item.split('-')[0] +"@"+ item.split('-')[1],
                                     val: item.split('-')[1]
                                 }
                             }))
                         },
                         error: function (response) {
                             alert(response.responseText);
                         },
                         failure: function (response) {
                             alert(response.responseText);
                         }
                     });
                 },
                 select: function (e, i) {
                     $("[id$=art_cve]").val(i.item.val);
                 },
                 minLength: 1
             });
         });

         function tab_btn(event) {
             var t = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
             if ((t == 9) || (t == 13)) {
                 document.getElementById('txtName').focus();
                 __doPostBack("txtName", "TextChanged");
                 return false;
             }
             return true;
         }
         function tab_btn2() {
             /*var t = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
             if ((t == 9) || (t == 13)) {*/
                 document.getElementById('txtName').focus();
                 __doPostBack("txtName", "TextChanged");
                 return false;
             /*}
             return true;*/
         }
</script>
</body>
</html>