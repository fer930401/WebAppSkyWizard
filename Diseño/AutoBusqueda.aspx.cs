using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using LogicaNegocio;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;

namespace Diseño
{
    public partial class AutoBusqueda : System.Web.UI.Page
    {
        Logica obj = new Logica();
        string step;
        string flujo;
        string user;
        string ef_cve;
        DataTable _item = new DataTable();

        /* variables estaticas utilizadas en esta clase */
        static object senDer;
        public static object SenDer
        {
            get { return AutoBusqueda.senDer; }
            set { AutoBusqueda.senDer = value; }
        }

        static Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs evento;
        public static Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs Evento
        {
            get { return AutoBusqueda.evento; }
            set { AutoBusqueda.evento = value; }
        }

        static string artTipo;
        public static string ArtTipo
        {
            get { return AutoBusqueda.artTipo; }
            set { AutoBusqueda.artTipo = value; }
        }

        static string artClave;
        public static string ArtClave
        {
            get { return AutoBusqueda.artClave; }
            set { AutoBusqueda.artClave = value; }
        }

        static string artNombre;
        public static string ArtNombre
        {
            get { return AutoBusqueda.artNombre; }
            set { AutoBusqueda.artNombre = value; }
        }
        /* fin de las variables estaticas */

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 3600;
            txtName.TabIndex = 0;
            txtName.Attributes.Add("autocomplete", "off");
            txtName.Attributes.Add("onblur", "return tab_btn2();");
            txtName.Attributes.Add("autopostback", "return tab_btn2();");

            if (Request.Params["step"] != null)
            {
                step = Request.Params["step"].ToString();
                flujo = Request.Params["flujo"].ToString();
                user = Request.Params["u"].ToString();
                ef_cve = Request.Params["ef_cve"].ToString();
                if (flujo.Equals(variablesGuardar.Nom_Flujo) == false)
                {
                    AutoBusqueda.ArtClave = null;
                    AutoBusqueda.ArtTipo = null;
                    AutoBusqueda.ArtNombre = null;
                    variablesGuardar.ArtCveAuto = null;
                    variablesGuardar.CveAuto = null;
                    variablesGuardar.NomArtAuto = null;
                    variablesGuardar.ArtTipAuto = null;
                }
                /* toda pantalla que apunte a algun catalogo de clientes o por el estilo se le deshabilitara el combo de tipo */
                if (flujo.Contains("cccat") == true)
                {
                    cmbTipo1.Enabled = false;
                }
                variablesGuardar.Nom_Flujo = flujo;
            }
            if (!IsPostBack)
            {
                _item = obj.llenaCombo(flujo);
                cmbArtTip1.DataSource = _item;
                cmbArtTip1.EmptyMessage = "Seleccionar una opción";
                cmbArtTip1.DataTextField = _item.Columns[0].ColumnName.ToString();
                cmbArtTip1.DataValueField = _item.Columns[1].ColumnName.ToString();
                cmbArtTip1.DataBind();
                DataTable dt;
                if (variablesGuardar.ArtCveAuto != null)
                {
                    cmbArtTip1.SelectedValue = variablesGuardar.ArtCveAuto;
                    DataTable _item2 = new DataTable();
                    _item2 = obj.llenaSubCombo(variablesGuardar.ArtCveAuto, ef_cve);
                    cmbTipo1.Items.Clear();
                    cmbTipo1.DataSource = _item2;
                    cmbTipo1.EmptyMessage = "Seleccionar una opción";
                    cmbTipo1.DataTextField = _item2.Columns[0].ColumnName.ToString();
                    cmbTipo1.DataValueField = _item2.Columns[1].ColumnName.ToString();
                    cmbTipo1.DataBind();
                    if (variablesGuardar.NomArtAuto != null)
                    {
                        cmbTipo1.SelectedValue = variablesGuardar.ArtTipAuto;
                        if(variablesGuardar.CveAuto != null ){
                            if(variablesGuardar.NomArtAuto.Equals("") == false ){
                                txtName.Text = variablesGuardar.NomArtAuto;
                                dt = obj.llenaTabla(variablesGuardar.ArtCveAuto + "%", variablesGuardar.CveAuto + "%", "%" + variablesGuardar.NomArtAuto + "%", 1);
                                RadGrid1.DataSource = dt;
                                RadGrid1.DataBind();
                            }
                        }
                        else
                        {
                            string nomComer = "";
                            dt = obj.llenaTabla(variablesGuardar.ArtCveAuto + "%", variablesGuardar.NomArtAuto, "%" + nomComer + "%", 2);
                            RadGrid1.DataSource = dt;
                            RadGrid1.DataBind();
                        }
                    }
                }
            }
        }
        
        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string art_cve = "";
                string nomComer = txtName.Text;
                string resultado = "";
                /* cuando exista un valor seleccionado en el combo tipo tomamos el valor del mismo */
                if (cmbTipo1.SelectedValue != "")
                {
                    art_cve = cmbTipo1.SelectedValue.ToString().TrimStart(' ').TrimEnd(' ');
                    ArtClave = cmbTipo1.SelectedValue.ToString().TrimStart(' ').TrimEnd(' ');
                }
                /* si esta vacio mandamos vacio el valor */
                else
                {
                    art_cve = "";
                }
                /* si el valor de la variable nomComer contiene un @ recortamos la cadena para obtener el art_cve del articulo */
                if (nomComer.Contains("@") == true)
                {
                    resultado = nomComer.Substring(0, nomComer.IndexOf("@"));
                    if (art_cve.Equals("") == true)
                    {
                        /* obtenemos la ultima parte de la cadena */
                        art_cve = nomComer.Remove(0, nomComer.IndexOf("@") + 1);
                    }
                    variablesGuardar.NomArtAuto = resultado;
                }
                else
                {
                    resultado = nomComer.TrimStart(' ').TrimEnd(' ');
                    variablesGuardar.NomArtAuto = resultado;
                }
                DataTable dt;
                if (cmbTipo1.SelectedValue == "")
                {
                    if (art_cve.Equals("") == true)
                    {
                        dt = obj.llenaTabla(ArtTipo + "%", art_cve.TrimStart(' ').TrimEnd(' '), "%" + resultado + "%", 3);
                    }
                    else
                    {
                        if (resultado.Contains("_") == true)
                        {
                            dt = obj.llenaTabla(ArtTipo, art_cve.TrimStart(' ').TrimEnd(' '), "%" + resultado.Substring(0,resultado.IndexOf("_")) + "%", 4);
                        }
                        else
                        {
                            dt = obj.llenaTabla(ArtTipo, art_cve.TrimStart(' ').TrimEnd(' '), "%" + resultado + "%", 4);
                        }
                    }
                }
                else
                {
                    /* configuracion especial para los catalogos de clientes */
                    if (flujo.Contains("cccat") == true)
                    {
                        dt = obj.llenaTabla(ArtTipo + "%", ArtClave.TrimStart(' ').TrimEnd(' '), "%" + resultado + "%", 6);
                    }
                    else
                    {
                        if (nomComer.Equals("") == false)
                        {
                            art_cve = nomComer.Remove(0, nomComer.IndexOf("@") + 1);
                            variablesGuardar.CveAuto = art_cve;
                        }
                        dt = obj.llenaTabla(ArtTipo + "%", art_cve + "%", "%" + resultado + "%", 1);
                    }
                }
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ArtTipo == "" || ArtTipo == null)
                {
                    ArtTipo = "012";
                }
                string artNom = cmbArtTip1.Text;
                Response.Redirect("AddArt.aspx?step=" + step.TrimStart(' ').TrimEnd(' ') + "&flujo=" + flujo.TrimStart(' ').TrimEnd(' ') + "&u=" + user.TrimStart(' ').TrimEnd(' ') + "&artTipo=" + ArtTipo + "&ef_cve=" + ef_cve.TrimStart(' ').TrimEnd(' '));
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
        }
        int ban = 0;     
        protected void cmbArtTip1_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Evento = e;
            SenDer = sender;
            RadComboBox ddl = (RadComboBox)sender;
            string valorArtTipo = "";
            ArtTipo = cmbArtTip1.SelectedValue.ToString();
            valorArtTipo = ArtTipo;
            variablesGuardar.ArtCveAuto = ArtTipo;
            DataTable _item2 = new DataTable();
            if (flujo.Contains("cccat") == true)
            {
                string art_tip = ArtTipo;
                string art_cve = "";
                string nomComer = "";
                DataTable dt = obj.llenaTabla(art_tip + "%", art_cve, "%" + nomComer + "%", 5);
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();
            }
            _item2 = obj.llenaSubCombo(valorArtTipo, ef_cve);
            cmbTipo1.Items.Clear();
            cmbTipo1.DataSource = _item2;
            cmbTipo1.EmptyMessage = "Seleccionar una opción";
            cmbTipo1.DataTextField = _item2.Columns[0].ColumnName.ToString();
            cmbTipo1.DataValueField = _item2.Columns[1].ColumnName.ToString();
            cmbTipo1.DataBind();
            cmbTipo1.Focus();
        }
        protected void cmbTipo1_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string art_cve = cmbTipo1.Text.ToString().TrimStart(' ').TrimEnd(' ');
                variablesGuardar.NomArtAuto = cmbTipo1.Text.ToString().TrimStart(' ').TrimEnd(' ');
                variablesGuardar.ArtTipAuto = cmbTipo1.SelectedValue.ToString().TrimStart(' ').TrimEnd(' ');
                string nomComer = "";
                DataTable dt = obj.llenaTabla(ArtTipo + "%", art_cve, "%" + nomComer + "%", 2);
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();
                ban = 1;
            }
            catch (Exception t)
            { 
                t.Message.ToString(); 
            }
        }
        protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = "";
            string clave = "";
            int accion = 0;
            foreach (GridDataItem item in RadGrid1.SelectedItems)
            {
                tipo = item["Tipo"].Text.TrimStart(' ').TrimEnd(' ');
                clave = item["Clave"].Text.TrimStart(' ').TrimEnd(' ');
            }
            LinkButton clickedButton = (LinkButton)sender;
            if (clickedButton.ID.ToString().Equals("btnEditar"))
            {
                accion = 1;
            }
            else
            {
                accion = 2;
            }
            string flujo = Request.Params["flujo"].ToString();
            string user = Request.Params["u"].ToString();
            string ef_cve = Request.Params["ef_cve"].ToString();
            Response.Redirect("Acciones.aspx?tipo=" + tipo + "&clave=" + clave + "&user=" + user + "&ef_cve=" + ef_cve + "&flujo=" + flujo + "&step=" + step + "&accion=" + accion);
            //Response.Redirect(accion.ToString());
        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "smClick")
            {
                // un click
                GridDataItem item = RadGrid1.MasterTableView.Items[Convert.ToInt32(e.CommandArgument)]; // newly added
            }
            else if (e.CommandName == "dbClick")
            {
                // doble click
                //GridDataItem item = RadGrid1.MasterTableView.Items[Convert.ToInt32(e.CommandArgument)]; // newly added
                string flujo = Request.Params["flujo"].ToString();
                string user = Request.Params["u"].ToString();
                string ef_cve = Request.Params["ef_cve"].ToString();
                string datos = e.CommandArgument.ToString(); // newly added
                string clave = datos.Substring(datos.IndexOf("&") + 1, datos.Length - datos.IndexOf("&") - 1);
                string tipo = datos.Substring(0, datos.IndexOf("&"));
                int accion = 1;
                Response.Redirect("Acciones.aspx?tipo=" + tipo + "&clave=" + clave + "&user=" + user + "&ef_cve=" + ef_cve + "&flujo=" + flujo + "&step=" + step + "&accion=" + accion);
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {

        }
    }
}