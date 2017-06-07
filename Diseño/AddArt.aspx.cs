using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using Telerik.Web.UI;
using Telerik;
using System.Web.Script.Serialization;
using LogicaNegocio;

namespace Diseño
{
    public partial class AddArt : System.Web.UI.Page
    {
        static object senDer;

        public static object SenDer
        {
            get { return AddArt.senDer; }
            set { AddArt.senDer = value; }
        }

        static Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs evento;

        public static Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs Evento
        {
            get { return AddArt.evento; }
            set { AddArt.evento = value; }
        }

        Logica _con = new Logica();
        int z = 0;//para el vector de pasos de _combos()
        int _pant = 0;
        DataTable Xuarrays = new DataTable();
        DataTable Xcdic = new DataTable();
        DataTable prm = new DataTable();
        string[,] qry = new string[100, 3];
        //string[] pasos = new string[100];
        string _step;
        string _flujo;
        string _user;
        string _artTipo;
        string _artNom;
        string _ef_cve;
        int numExtras = 1;
        string urlInicial;
        string nombreUser = "";
        int numPantalla = 0;
        int cont = 1;
        int bloqueado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usr"] != null)
            {
                nombreUser = Session["usr"].ToString();
            }
            else
            {
                nombreUser = "";
            }
            urlInicial = variablesGuardar.UrlInicial;
            btnAdelante.Visible = false;
            btnAtras.Visible = false;
            _flujo = Request.Params["flujo"].ToString();
            _step = Request.Params["step"].ToString();
            if (variablesGuardar.Art_cveFin != null)
            {
                /* un art_cve no debe de exceder los 13 caracteres */
                if (variablesGuardar.Art_cveFin.Length <= 13)
                {
                    btnFinalizar.Visible = true;
                    btnCancelar.Visible = false;
                }
                else
                {
                    btnFinalizar.Visible = false;
                    btnCancelar.Visible = true;
                }
            }
            else
            {
                btnFinalizar.Visible = false;
            }
        }
        /*Esta funcion consulta a la base de datos para verificar el paso inicial y los status de los flujos de acuerdo al usuario*/
        protected void RadWizard1_Init(object sender, EventArgs e)
        {
            if (Session["usr"] != null)
            {
                nombreUser = Session["usr"].ToString();
            }
            else
            {
                nombreUser = "";
            }
            /* ver en que lo puedo usar */
            Label1.Visible = false;
            variablesGuardar.Ban_borra = 0;
            if (!this.IsPostBack)
            {
                if (Request.Params["step"] != null)
                {
                    _step = Request.Params["step"].ToString();
                    _flujo = Request.Params["flujo"].ToString();
                    _user = Request.Params["u"].ToString();
                    _ef_cve = Request.Params["ef_cve"].ToString();
                    _artTipo = "";// Request.Params["artTipo"].ToString();
                    _artNom = "";// Request.Params["nom"].ToString();
                    variablesGuardar.Art_tip = _artTipo;
                    variablesGuardar.Art_nom = _artNom;
                    variablesGuardar.User = _user;
                    variablesGuardar.Flujo = _flujo;
                    variablesGuardar.Ef_cve = _ef_cve;
                    if (RadWizard1.ActiveStepIndex >= 1)
                    {
                        btnAtras.Visible = true;
                    }
                    else
                    {
                        btnAtras.Visible = false;
                    }
                }
                int nPaso = 0;
                int nPasoIni;
                string step = "";
                int no_pantalla = 0;
                try
                {
                    nPasoIni = Convert.ToInt32(_step);
                    nPasoIni = nPasoIni + 1;
                    if (variablesGuardar.SeccionVisible == null)
                    {
                        variablesGuardar.SeccionVisible = "0_" + nPasoIni.ToString();
                    }
                    variablesGuardar.Art_tipBDD = Request.Params["artTipo"].ToString(); //_con.ultimoArt_Tip(_user, _flujo);
                    nPaso = _con.no_pasos(_flujo);
                    if (nPaso >= 2)
                    {
                        if (variablesGuardar.Art_cveFin != null)
                        {
                            if (variablesGuardar.Art_cveFin.Length <= 13)
                            {
                                /* adelantar el paso automaticamente */
                                //RadWizard1.ActiveStepIndex += 1;
                            }
                        }
                    }
                    for (int p = nPasoIni; p <= nPaso; p++)
                    {
                        RadWizardStep temp = new RadWizardStep();
                        System.Web.UI.WebControls.Panel _pan = new System.Web.UI.WebControls.Panel();
                        prm = _con.parametroInicial(p.ToString(), _flujo);
                        variablesGuardar.Tipo_ccs[p, 0] = prm.Rows[0][1].ToString();
                        variablesGuardar.Prg_cves[p, 0] = prm.Rows[0][0].ToString();
                        if (variablesGuardar.Prg_cves[p, 0].Contains("cccatcc") == true)
                        {
                            variablesGuardar.Prm1 = variablesGuardar.Tipo_ccs[p, 0];
                        }
                        variablesGuardar.Art_tipVal = _con.Art_TipPant(_flujo, variablesGuardar.Prg_cves[p, 0]);
                        temp.ID = "WizardStep" + (p).ToString();
                        step = temp.ID.ToString();
                        temp.Title = _con.tituloStep(variablesGuardar.Prg_cves[p, 0]);
                        _pan.ID = "Panel" + p;
                        no_pantalla = p;
                        Xcdic = _con.xcdic(variablesGuardar.Prg_cves[p, 0], variablesGuardar.User, variablesGuardar.Tipo_ccs[p, 0], variablesGuardar.Ef_cve);// xcdic.*
                        Xuarrays = _con.xuarrays(variablesGuardar.Prg_cves[p, 0]);
                        RadWizard1.WizardSteps.Add(temp);
                        temp.Controls.Add(_pan);
                        genControles(Xcdic, Xuarrays, no_pantalla, variablesGuardar.Tipo_ccs[p, 0], step, 0, p.ToString());
                    }
                }
                catch (Exception t)
                {
                    t.Message.ToString();
                    RadTickerItem x = new RadTickerItem(t.Message.ToString());
                    RadTicker1.Items.Add(x);
                }
            }
            else //postback
            {
                _step = Request.Params["step"].ToString();
                _flujo = Request.Params["flujo"].ToString();
                _user = Request.Params["u"].ToString();
                _ef_cve = Request.Params["ef_cve"].ToString();
                _artTipo = variablesGuardar.Art_tip;
                _artNom = variablesGuardar.Art_nom;
                int nPaso = 0;
                string step = "";
                int no_pantalla = 0;
                int nPasoIni;
                if (RadWizard1.ActiveStepIndex >= 1)
                {
                    btnAtras.Visible = true;
                }
                else
                {
                    btnAtras.Visible = false;
                }
                try
                {
                    variablesGuardar.Art_tipBDD = Request.Params["artTipo"].ToString(); //_con.ultimoArt_Tip(_user, _flujo);
                    nPaso = _con.no_pasos(_flujo);
                    nPasoIni = Convert.ToInt32(_step);
                    nPasoIni = nPasoIni + 1;

                    for (int p = nPasoIni; p <= nPaso; p++)
                    {
                        RadWizardStep _temp = new RadWizardStep();
                        System.Web.UI.WebControls.Panel _pan = new System.Web.UI.WebControls.Panel();
                        prm = _con.parametroInicial(p.ToString(), _flujo);
                        variablesGuardar.Tipo_ccs[p, 0] = prm.Rows[0][1].ToString();
                        variablesGuardar.Prg_cves[p, 0] = prm.Rows[0][0].ToString();
                        variablesGuardar.Art_tipVal = _con.Art_TipPant(_flujo, variablesGuardar.Prg_cves[p, 0]);
                        _temp.ID = "WizardStep" + (p).ToString();
                        step = _temp.ID.ToString();
                        _temp.Title = _con.tituloStep(variablesGuardar.Prg_cves[p, 0]);
                        _pan.ID = "Panel" + p;
                        no_pantalla = p;
                        Xcdic = _con.xcdic(variablesGuardar.Prg_cves[p, 0], variablesGuardar.User, variablesGuardar.Tipo_ccs[p, 0], variablesGuardar.Ef_cve);// xcdic.*
                        Xuarrays = _con.xuarrays(variablesGuardar.Prg_cves[p, 0]);
                        RadWizard1.WizardSteps.Add(_temp);
                        _temp.Controls.Add(_pan);
                        genControles(Xcdic, Xuarrays, no_pantalla, variablesGuardar.Tipo_ccs[p, 0], step, 1, p.ToString());
                        if (Evento != null)
                        {
                            combo_SelectedIndexChanged(SenDer, Evento, numPantalla);
                            Evento = null;
                        }
                    }
                }
                catch (Exception t)
                {
                    t.Message.ToString();
                    RadTickerItem x = new RadTickerItem(t.Message.ToString());
                    RadTicker1.Items.Add(x);
                }
            }
            //RadWizard1.mov = "WizardStep2";
        }
        //crea e inserta paneles en en cada paso creando anteriormente, al mismo tiempo selecciona el tipo de contro a dibujar y manda a llamar al metodo que dibuja el control
        public void genControles(DataTable _Xcdic, DataTable _Xuarrays, int pant, string tip, string step, int postbk, string postArt)
        {
            int total = 0;// _con.CuentaXcdicB(tip, tab, seccion);
            DataTable prg_cveTablas;
            int totalTablas = 0;
            string nomPanel = "Panel" + pant.ToString();
            string prg_cvePantalla = "";
            DataTable XuarraysBotones;
            int cccatccam1 = 1;

            prg_cvePantalla = variablesGuardar.Prg_cves[pant, 0];
            total = _Xcdic.Rows.Count;
            XuarraysBotones = _con.TablaXuarraysBotones(prg_cvePantalla); //xuarrays.*
            try
            {
                prg_cveTablas = _con.tablaTotal(prg_cvePantalla, "C", variablesGuardar.Tipo_ccs[pant, 0]);
                totalTablas = _con.tablaTotal(prg_cvePantalla, "C", variablesGuardar.Tipo_ccs[pant, 0]).Rows.Count;
                string nomBoton = "";
                string aux1 = "";
                int rowXuarrays = 0;
                for (int y = 0; y < totalTablas; y++)
                {
                    if (y > 0)
                    {
                        if (prg_cvePantalla.Equals("cccatccam1") == true || prg_cvePantalla.Equals("cccatccp") == true)
                        {
                            if (y == 1)
                            {
                                cccatccam1 = 0;
                            }
                            else if (y == 2)
                            {
                                cccatccam1 = 2;
                            }
                            else
                            {
                                cccatccam1 = 1;
                            }
                        }
                        nomBoton = XuarraysBotones.Rows[y - cccatccam1][3].ToString().Replace("&", "").Replace("¬", "").Replace("#", "").Replace(" ", "").Replace("'", "");
                    }
                    if (nomBoton.Equals("") == true)
                    {
                        nomBoton = "Principal";
                    }
                    addSeccion(nomPanel, pant, step, y, nomBoton);
                    for (int i = 0; i < total; i++)
                    {
                        if (_Xuarrays.Rows[i][1].ToString().Equals("GL_botones     ") == true && _Xuarrays.Rows[i][4].ToString().Equals(y.ToString()) && _Xuarrays.Rows[i][5].ToString().Equals("0") && _Xuarrays.Rows[i][6].ToString().Equals("0"))
                        {
                            rowXuarrays = i;
                        }
                        int pantXcdic = Int32.Parse(_Xcdic.Rows[i][28].ToString());
                        int pantXuarrays = Int32.Parse(prg_cveTablas.Rows[y][0].ToString());
                        if (pantXcdic == pantXuarrays)
                        {
                            /* columna 14 tipo_for soludin */
                            string tipoFor = _Xcdic.Rows[i][14].ToString();

                            /* columna 10 tipo_act soludin */
                            string tipo_act = _Xcdic.Rows[i][10].ToString().TrimEnd(' ');

                            /* columna 0 tabla soludin */
                            string tabla = _Xcdic.Rows[i][0].ToString().TrimEnd(' ');

                            if ((tipoFor != "7") && (tipoFor != "") && (tipo_act.Equals("2") == false) && (tipo_act.Equals("0") == false))
                            {
                                /* le asignamos el valor del tipo for a una variabel auxiliar para despues generar un boton de guardar */
                                aux1 = tipoFor;
                                /* obtenemos el renglon en el que se van a dibujar los controles */
                                int reng = 0;

                                string web_obj = _Xcdic.Rows[i][27].ToString().TrimEnd(' ');
                                if (web_obj.Contains(",") == true)
                                {
                                    reng = Int32.Parse(web_obj.Substring(0, 1));
                                    tipoFor = web_obj.Substring(1, 3).Replace(',', ' ').TrimStart(' ');
                                    //tipoFor = tipoFor.TrimStart(' ');
                                    bloqueado = 1;
                                }
                                else
                                {
                                    reng = Int32.Parse(_Xcdic.Rows[i][27].ToString().TrimEnd(' '));
                                    bloqueado = 0;
                                }


                                int tipo_combo = Convert.ToInt32(_Xcdic.Rows[i][17].ToString());

                                /* genera los labels de cada campo */
                                genLabel(_Xcdic, i, pant, step);

                                /* genera un textbox */
                                if ((tipoFor.Equals("94") == true) || (tipoFor.Equals("0") == true) || (tipoFor.Equals("93") == true))
                                {
                                    genTextbox(_Xcdic, i, pant, 0, "", pantXcdic);
                                    addPanelHTML(pant, reng);
                                }
                                /* genera un textbox selector de fechas */
                                if ((tipoFor.Equals("95") == true))
                                {
                                    genDatePicker(_Xcdic, i, pant, 0, "", pantXcdic);
                                    addPanelHTML(pant, reng);
                                }
                                /* genera un textbox selector de horas */
                                if ((tipoFor.Equals("97") == true))
                                {
                                    genDateTimePicker(_Xcdic, i, pant, 0, "", pantXcdic);
                                    addPanelHTML(pant, reng);
                                }
                                /* genera un textbox numerico */
                                else if ((tipoFor.Equals("11") == true) || (tipoFor.Equals("4") == true) || (tipoFor.Equals("5") == true) || (tipoFor.Equals("13") == true) || (tipoFor.Equals("24") == true) || (tipoFor.Equals("3") == true) || (tipoFor.Equals("12") == true) || (tipoFor.Equals("2") == true))
                                {
                                    if (web_obj.Equals("2") == false)
                                    {
                                        genTextboxNum(_Xcdic, i, pant, 0, "", pantXcdic);
                                        addPanelHTML(pant, reng);
                                    }
                                }
                                /* genera un combobox */
                                else if (tipoFor.Equals("91") == true || (tipoFor.Equals("80") == true))
                                {
                                    genComboBox(_Xcdic, _Xuarrays, i, pant, prg_cvePantalla, step, pantXcdic, postArt, y);
                                    addPanelHTML(pant, reng);
                                }
                                /* genera un textbox area */
                                else if (tipoFor.Equals("92") == true)
                                {
                                    if (tabla.Equals("icartcls") == true || tabla.Equals("ccclscc") == true)
                                    {
                                        genTextArea(_Xcdic, i, pant);
                                        //genBtnCls(_Xcdic, i, pant, step);
                                        addPanelHTML(pant, reng);
                                    }
                                    else
                                    {
                                        genTextbox(_Xcdic, i, pant, 0, "", pantXcdic);
                                        addPanelHTML(pant, reng);
                                    }
                                }
                                /* genera un checkbox */
                                else if (tipoFor.Equals("90") == true)
                                {
                                    genCheckBox(_Xcdic, i, pant, 0, "", cont);
                                    addPanelHTML(pant, reng);
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                    /* genera los botones de guardar para los bloques que simulan las pantallas de soludin */
                    if (aux1 != "92")
                    {
                        genBtnGuardar(nomPanel, pant, y, nomBoton);
                    }
                    /* genera el boton para la pantalla de clasificaciones de soludin */
                    else
                    {
                        genBtnCls(_Xcdic, y, pant, step);
                    }
                    aux1 = "";
                    addCierreSeccion(nomPanel, pant, step, y);
                }
                if (variablesGuardar.Art_cveFin != null)
                {
                    if (variablesGuardar.Art_cveFin.ToString().Length <= 13 && variablesGuardar.NumBloque != null)
                    {
                        int bloque = 0;
                        string textoEmergente = "";
                        string sku_cve = "";
                        if (Session["tb1nombre" + nombreUser] != null)
                        {
                            textoEmergente = variablesGuardar.Nom_Art;
                            //textoEmergente = Session["tb1nombre" + nombreUser].ToString();
                        }
                        if (Session["tb1dscon_activ" + nombreUser] != null)
                        {
                            textoEmergente = Session["tb1dscon_activ" + nombreUser].ToString();
                        }
                        if (Session["tb1rfc" + nombreUser] != null)
                        {
                            textoEmergente = variablesGuardar.Nom_Art;
                            //textoEmergente = Session["tb1rfc" + nombreUser].ToString();
                        }
                        if (Session["tb1nom1" + nombreUser] != null)
                        {
                            textoEmergente = variablesGuardar.Nom_Art;
                            //textoEmergente = Session["tb1rfc" + nombreUser].ToString();
                        }
                        if (variablesGuardar.Sku_cveFin != null)
                        {
                            sku_cve = "<br/><strong>sku_cve: </strong>" + variablesGuardar.Sku_cveFin;
                        }
                        bloque = Int32.Parse(variablesGuardar.NumBloque.ToString()) + 1;
                        Response.Write("<div class='flotante'>" +
                            "<span class='glyphicon glyphicon-check' aria-hidden='true'></span>" +
                                "Paso <strong>" + bloque + "</strong> procesado correctamente.  <br />" +
                                "<strong>Clave: </strong>" +
                                variablesGuardar.Art_cveFin.ToString() +
                                "<br /> <strong>Nombre/Descripcion: </strong>" +
                                textoEmergente +
                                sku_cve +
                        "</div>");
                    }
                }
                try
                {
                    if (postArt.Equals("1") == true)
                    {
                        foreach (Object obj in RadWizard1.Controls)
                        {
                            if (obj is RadWizardStep)
                            {
                                RadWizardStep temp = (RadWizardStep)obj;
                                if (temp.ID.ToString().Equals(step) == true)
                                {
                                    foreach (Object ob in temp.Controls)
                                    {
                                        if (ob is System.Web.UI.WebControls.Panel)
                                        {
                                            System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                            if (temp2.ID.ToString().Equals(nomPanel))
                                            {
                                                if (variablesGuardar.NewCombos != null)
                                                {
                                                    if (variablesGuardar.NumSecciones != 0)
                                                    {

                                                        for (int i = 1; i <= Int32.Parse(variablesGuardar.NumSecciones.ToString()); i++)
                                                        {
                                                            System.Web.UI.WebControls.Label lb = new System.Web.UI.WebControls.Label();
                                                            System.Web.UI.WebControls.TextBox tbx = new System.Web.UI.WebControls.TextBox();
                                                            RadComboBox cbx = new RadComboBox();
                                                            string nombreLabel = "labelDinamico" + nombreUser + i.ToString();
                                                            string nombreControl = "controlDinamico" + nombreUser + i.ToString();
                                                            string segundonombre = "nom" + nombreUser + Session[nombreControl].ToString();
                                                            string nombreExtra = "ex" + nombreUser + Session[nombreControl].ToString();
                                                            if (Session[nombreControl].ToString().Equals(Session[segundonombre].ToString()) && Session[segundonombre].ToString().Contains("cls_num") == false && Session[segundonombre].ToString().Contains("subcls_cve") == false)
                                                            {
                                                                if (variablesGuardar.ErrorInsert != null)
                                                                {
                                                                    if (variablesGuardar.ErrorInsert.ToString() == "0")//&& nombreExtra.StartsWith("exex") == false
                                                                    {
                                                                        if (variablesGuardar.CambioCombo != null)
                                                                        {
                                                                            if (variablesGuardar.CambioCombo.ToString() == "0")
                                                                            {
                                                                                if (nombreExtra.StartsWith("ex" + nombreUser + "excb") == true)
                                                                                {
                                                                                    string balin_4 = Session["balin4" + nombreUser + nombreExtra.Remove(0, 5)].ToString();
                                                                                    string tem_2 = Session["tem" + nombreUser + nombreExtra.Remove(0, 5)].ToString();
                                                                                    string balin2 = Session["balin2" + nombreUser + nombreExtra.Remove(0, 5)].ToString();
                                                                                    DataTable dato = _con.ExecQryTabla(balin_4, tem_2);
                                                                                    cbx.Items.Clear();
                                                                                    cbx.DataSource = dato;
                                                                                    cbx.DataTextField = dato.Columns[0].ColumnName.ToString();
                                                                                    cbx.DataValueField = dato.Columns[1].ColumnName.ToString();
                                                                                    cbx.DataBind();
                                                                                    cbx.AllowCustomText = false;
                                                                                    cbx.MarkFirstMatch = true;
                                                                                    cbx.ID = nombreExtra.Remove(0, 5);
                                                                                    cbx.Width = Int32.Parse(Session["Width" + nombreUser + nombreExtra.Remove(0, 5)].ToString());
                                                                                    cbx.ToolTip = nombreExtra.Remove(0, 5);
                                                                                    cbx.SelectedIndex = Int32.Parse(Session[nombreExtra.Remove(0, 5) + nombreUser].ToString());
                                                                                    lb.Text = Session[nombreLabel].ToString();
                                                                                    if (_flujo == "mnuCatAct")
                                                                                    {
                                                                                        temp2.Controls.AddAt(10, new LiteralControl("<br />"));
                                                                                        temp2.Controls.AddAt(11, lb);
                                                                                        temp2.Controls.AddAt(12, cbx);
                                                                                        temp2.Controls.AddAt(13, new LiteralControl("<br />"));
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (balin2.Equals("1") == true)
                                                                                        {
                                                                                            temp2.Controls.AddAt(variablesGuardar.Index + 1, lb);
                                                                                            temp2.Controls.AddAt(variablesGuardar.Index + 2, cbx);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            temp2.Controls.AddAt(3, lb);
                                                                                            temp2.Controls.AddAt(4, cbx);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    tbx.Width = 150;
                                                                                    tbx.CssClass = "form-control2";
                                                                                    tbx.Text = Session[nombreExtra].ToString();
                                                                                    tbx.ID = nombreExtra.Remove(0, 5);
                                                                                    tbx.ToolTip = nombreExtra;
                                                                                    lb.Text = Session[nombreLabel].ToString();
                                                                                    temp2.Controls.AddAt(3, lb);
                                                                                    temp2.Controls.AddAt(4, tbx);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception t)
                {
                    t.Message.ToString();
                    RadTickerItem x = new RadTickerItem(t.Message.ToString());
                    RadTicker1.Items.Add(x);
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //crea un label 
        public void genLabel(DataTable _Xcdic, int pos, int pant, string step)
        {
            try
            {
                string nomPanel = "";
                System.Web.UI.WebControls.Label _label = new System.Web.UI.WebControls.Label();
                _label.ID = "lb" + pant + "_" + pos + _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
                _label.Text = _Xcdic.Rows[pos][6].ToString().TrimEnd(' ');
                nomPanel = "Panel" + pant.ToString();
                if (_Xcdic.Rows[pos][6].ToString().TrimEnd(' ') != "x" && _Xcdic.Rows[pos][14].ToString().Equals("90") == false && _Xcdic.Rows[pos][10].ToString().Equals("2") == false)
                {
                    if (_label.ID.ToString().Equals("sw_retencion") == true)
                    {
                        _label.Text = "";
                    }
                    if (_label.ID.ToString().Equals("fecha") == true)
                    {
                        _label.Text = _Xcdic.Rows[pos][6].ToString() + "<strong> dd/MM/yyyy</strong>";
                    }
                    addPanelLabel(_label, nomPanel, pant, 0, step);
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //crea un textbox normal
        public void genTextbox(DataTable _Xcdic, int pos, int pant, int var, string step, int cont)
        {
            try
            {
                string nomPanel = "Panel" + pant.ToString();
                step = "WizardStep" + pant.ToString();
                RadTextBox TextBoxB = new RadTextBox();
                TextBoxB.ID = "tb" + pant + "_" + cont + _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
                TextBoxB.ToolTip = TextBoxB.ID.ToString();
                decimal largo = Convert.ToDecimal(_Xcdic.Rows[pos][4].ToString().TrimEnd(' '));
                largo = (largo * 0.09m);
                int largo2 = Convert.ToInt32(largo);
                TextBoxB.Width = largo2;
                if (TextBoxB.ID.Contains("tb1puesto") == true || TextBoxB.ID.Contains("tb1atencion") == true || TextBoxB.ID.Contains("tb1telef") == true || TextBoxB.ID.Contains("tb1fax") == true)
                {
                    TextBoxB.EmptyMessage = _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
                }
                if (TextBoxB.ID.Equals("tb1sku_cve") == true)
                {
                    TextBoxB.ReadOnly = true;
                    if (variablesGuardar.Sku_cveFin != null)
                    {
                        if (variablesGuardar.Sku_cveFin.Length >= 13)
                        {
                            TextBoxB.Text = variablesGuardar.Sku_cveFin.ToString();
                        }
                        else
                        {
                            TextBoxB.Text = "N/A";
                        }
                    }
                    else
                    {
                        TextBoxB.Text = "N/A";
                    }
                }
                TextBoxB.CssClass = "form-control";
                if (_Xcdic.Rows[pos][18] != null || _Xcdic.Rows[pos][18].ToString() != "")
                {
                    int maxLen = Convert.ToInt32(_Xcdic.Rows[pos][18].ToString().TrimEnd(' '));
                    if (maxLen > 0)
                    {
                        TextBoxB.MaxLength = Convert.ToInt32(_Xcdic.Rows[pos][18].ToString().TrimEnd(' '));
                    }
                }
                if (_Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("fecha"))
                {
                    TextBoxB.Width = 150;
                    TextBoxB.EmptyMessage = "01/01/1999";
                    TextBoxB.InputType = Html5InputType.Date;
                }
                if (_Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("fec_limrev"))
                {
                    TextBoxB.Width = 150;
                    TextBoxB.EmptyMessage = "01/01/1999";
                    TextBoxB.InputType = Html5InputType.Date;
                }
                if (_Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("horaent_ini"))
                {
                    TextBoxB.Width = 250;
                    TextBoxB.EmptyMessage = "01/01/1999";
                    TextBoxB.InputType = Html5InputType.DateTimeLocal;
                }
                if (_Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("horaent_fin"))
                {
                    TextBoxB.Width = 250;
                    TextBoxB.EmptyMessage = "01/01/1999";
                    TextBoxB.InputType = Html5InputType.DateTimeLocal;
                }

                if (_Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("cc_cve") == true)
                {
                    TextBoxB.Enabled = false;
                    if (variablesGuardar.Art_cveFin != null)
                    {
                        if (variablesGuardar.Art_cveFin.ToString().Length <= 13)
                        {
                            TextBoxB.Text = variablesGuardar.Art_cveFin.ToString();
                        }
                        else
                        {
                            TextBoxB.Text = "N/A";
                        }
                    }
                    else
                    {
                        TextBoxB.Text = "N/A";
                    }
                }
                else if (_Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("art_cve") || _Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("cve_activ"))
                {
                    TextBoxB.Enabled = false;
                    if (variablesGuardar.Art_cveFin != null)
                    {
                        if (variablesGuardar.Art_cveFin.ToString().Length <= 13)
                        {
                            TextBoxB.Text = variablesGuardar.Art_cveFin.ToString();
                        }
                        else
                        {
                            TextBoxB.Text = "N/A";
                        }
                    }
                    else
                    {
                        TextBoxB.Text = "N/A";
                    }
                }
                else if (_Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("id_ultact"))
                {
                    TextBoxB.Enabled = false;
                    string id_user = "ID USER";
                    if (variablesGuardar.User != null)
                    {
                        id_user = variablesGuardar.User.ToString();
                    }
                    else
                    {
                        id_user = "S/U";
                    }
                    TextBoxB.Text = id_user;
                }
                else
                {
                    TextBoxB.Enabled = true;
                }
                if (Session[TextBoxB.ID.ToString() + nombreUser] != null)
                {
                    if (TextBoxB.ID.ToString().Equals(Session["nom" + nombreUser + TextBoxB.ID.ToString()].ToString()) == true && _Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("art_cve") == false && _Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("cve_activ") == false && _Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("cc_cve") == false && _Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("sku_cve") == false)
                    {
                        TextBoxB.Text = Session[TextBoxB.ID.ToString() + nombreUser].ToString();
                    }
                }
                if (variablesGuardar.SeccionVisible.Equals(cont + "_" + pant) == true)
                {
                    TextBoxB.Attributes["required"] = "required";
                }
                if (bloqueado == 1)
                {
                    TextBoxB.Enabled = false;
                    TextBoxB.Text = variablesGuardar.Art_cveFin + " @ " + variablesGuardar.Nom_Art;
                }
                addPanelTextBox(TextBoxB, nomPanel, pant, 1, step, cont);
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //crea y carga un combobox segun las variables pasadas por defecto
        public void genComboBox(DataTable _Xcdic, DataTable _Xuarrays, int pos, int pant, string prg_cve, string step, int cont, string postArt, int secc)
        {
            RadComboBox combo = new RadComboBox();
            string Cart_tip = variablesGuardar.Art_tipBDD;
            try
            {
                combo.ID = "cb" + pant + "_" + cont + _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
                if (combo.ID.ToString().Equals("cb1_0art_tip") == true)
                {
                    combo.Items.Insert(0, "Selecciona una opcion");
                }
                combo.EmptyMessage = "Seleccionar una opción";
                combo.ToolTip = combo.ID.ToString();
                combo.AllowCustomText = false;
                combo.EnableTextSelection = true;
                decimal largo = Convert.ToDecimal(_Xcdic.Rows[pos][4].ToString().TrimEnd(' '));
                largo = (largo * 0.09m);
                int largo2 = Convert.ToInt32(largo);
                combo.Width = largo2;
                combo.MarkFirstMatch = true;

                int tipo_combo = Convert.ToInt32(_Xcdic.Rows[pos][17].ToString());
                int sys_var = Int32.Parse(_Xcdic.Rows[pos][12].ToString());
                string balin5 = "";
                if (_Xcdic.Rows[pos][22] != null)
                {
                    balin5 = _Xcdic.Rows[pos][22].ToString();
                }
                DataTable _item = new DataTable();
                _item.Clear();
                _item = _con._CargaCombo(_Xuarrays, pos, _Xcdic, prg_cve, pant, Cart_tip, "", null, z, qry, secc);
                int colum = _item.Columns.Count;
                if (colum >= 2)
                {
                    combo.Items.Clear();
                    combo.DataSource = _item;
                    combo.DataTextField = _item.Columns[0].ColumnName.ToString();
                    combo.DataValueField = _item.Columns[1].ColumnName.ToString();
                    if (tipo_combo == 1 && sys_var != 0)// si el combo es tipo 1 entonces filtra al siguiente combo
                    {
                        combo.AutoPostBack = true;
                        combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler((sender1, e1) => combo_SelectedIndexChanged(sender1, e1, secc));
                    }

                    if (tipo_combo == 2)
                    {
                        if (balin5.Equals("") == false && sys_var != 0)
                        {
                            combo.AutoPostBack = true;
                            combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler((sender1, e1) => combo_SelectedIndexChanged(sender1, e1, secc));
                        }
                        else
                        {
                            combo.AutoPostBack = false;
                            //combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler((sender1, e1) => combo_SelectedIndexChanged(sender1, e1, secc));
                        }
                    }
                }
                else
                {
                    combo.Items.Clear();
                    combo.DataSource = _item;
                    combo.DataTextField = _item.Columns[0].ColumnName.ToString();
                    combo.DataValueField = _item.Columns[0].ColumnName.ToString();
                    if (tipo_combo == 1 && sys_var != 0)
                    {
                        combo.AutoPostBack = true;
                        combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler((sender1, e1) => combo_SelectedIndexChanged(sender1, e1, secc));
                    }
                    if (tipo_combo == 2)
                    {
                        if (balin5 != null)
                        {
                            if (balin5.Equals("") == false && sys_var != 0)
                            {
                                combo.AutoPostBack = true;
                                combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler((sender1, e1) => combo_SelectedIndexChanged(sender1, e1, secc));
                            }
                            else
                            {
                                combo.AutoPostBack = false;
                                //combo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler((sender1, e1) => combo_SelectedIndexChanged(sender1, e1, secc));
                            }
                        }
                        
                    }
                }
                combo.DataBind();
                if (combo.ID.ToString().Contains("cls_num") == true)
                {
                    if (Session["indexCombo" + nombreUser] != null)
                    {
                        combo.SelectedIndex = Int32.Parse(Session["indexCombo" + nombreUser].ToString());
                        Session["indexCombo" + nombreUser] = null;
                    }
                }
                if (Session[combo.ID.ToString() + nombreUser] != null)
                {
                    if (combo.ID.ToString().Equals(Session["nom" + nombreUser + combo.ID.ToString()].ToString()) == true /*&& combo.ID.ToString().Equals("cb1cls_num") == false*/ && combo.ID.ToString().Contains("subcls_cve") == false)
                    {
                        if (combo.ID.ToString().Equals("cb1_0art_tip") == true)
                        {
                            combo.Items.Insert(0, "Selecciona una opcion");
                            combo.SelectedIndex = Int32.Parse(Session[combo.ID.ToString() + nombreUser].ToString());
                        }
                        else
                        {
                            combo.SelectedIndex = Int32.Parse(Session[combo.ID.ToString() + nombreUser].ToString());
                        }
                    }
                }
                else
                {
                    if (combo.ID.ToString().Equals("cb1_0art_tip") == true)
                    {
                        combo.Items.Insert(0, "Selecciona una opcion");
                        combo.SelectedIndex = 0;
                    }
                }
                if (combo.ID.ToString().Equals("cb1_0art_tip") == true)
                {
                    if (variablesGuardar.ErrorArttip == 1)
                    {
                        if (variablesGuardar.Art_cveFin != null)
                        {
                            if (variablesGuardar.Art_cveFin.ToString().Length <= 13)
                            {
                                combo.SelectedIndex = Int32.Parse(Session[combo.ID.ToString() + nombreUser].ToString());
                            }
                        }
                        else
                        {
                            combo.SelectedIndex = 0;
                        }
                    }
                }
                if (pant > 1)
                {
                    if (_Xcdic.Rows[pos][1].ToString().TrimEnd(' ').Equals("art_tip") == true)
                    {
                        combo.SelectedValue = variablesGuardar.Art_tipFinal;
                    }
                }
                string nomPanel = "Panel" + pant.ToString();
                addPanelComboBox(combo, nomPanel, pant, 1, step);
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //crea texbox donde el usuario va a redactar la información
        public void genTextArea(DataTable _Xcdic, int pos, int pant)
        {
            try
            {
                string nomPanel = "Panel" + pant.ToString();
                string step = "WizardStep" + pant.ToString();
                System.Web.UI.WebControls.TextBox TextBoxB = new System.Web.UI.WebControls.TextBox();
                TextBoxB.ID = "tb" + pant + "_" + pos + _Xcdic.Rows[pos][1].ToString().TrimEnd(' ') + "cls";
                TextBoxB.TextMode = TextBoxMode.MultiLine;
                TextBoxB.Height = 123;
                TextBoxB.Width = 423;
                TextBoxB.Enabled = false;
                if (variablesGuardar.User != null)
                {
                    if (variablesGuardar.Art_cveFin != null)
                    {
                        if (variablesGuardar.Art_cveFin.Length <= 13)
                        {
                            string infoCls = "";
                            infoCls = _con.buscaCls(variablesGuardar.Art_cveFin.ToString(), variablesGuardar.User.ToString(), "", pant);
                            if (infoCls.Equals("") == false)
                            {
                                TextBoxB.Text = infoCls;
                            }
                            else
                            {
                                TextBoxB.Text = "";
                            }
                        }
                        else
                        {
                            TextBoxB.Text = "";
                        }
                    }
                    else
                    {
                        TextBoxB.Text = "";
                    }
                }
                else
                {
                    TextBoxB.Text = "";
                }
                addPanelTextArea(TextBoxB, nomPanel, pant, 2, step);
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //crea un textbox numerico
        public void genTextboxNum(DataTable _Xcdic, int pos, int pant, int var, string step, int cont)
        {
            try
            {
                string nomPanel = "Panel" + pant.ToString();
                step = "WizardStep" + pant.ToString();
                RadNumericTextBox TextBoxB = new RadNumericTextBox();
                TextBoxB.ID = "tb" + pant + "_" + cont + _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
                TextBoxB.ToolTip = TextBoxB.ID.ToString();
                decimal largo = Convert.ToDecimal(_Xcdic.Rows[pos][4].ToString().TrimEnd(' '));
                largo = (largo * 0.09m);
                int largo2 = Convert.ToInt32(largo);
                TextBoxB.Width = largo2;

                TextBoxB.Style["Position"] = "top";
                TextBoxB.CssClass = "form-control";
                if (_Xcdic.Rows[pos][18] != null || _Xcdic.Rows[pos][18].ToString() != "")
                {
                    int maxLen = Convert.ToInt32(_Xcdic.Rows[pos][18].ToString().TrimEnd(' '));
                    if (maxLen > 0)
                    {
                        TextBoxB.MaxLength = Convert.ToInt32(_Xcdic.Rows[pos][18].ToString().TrimEnd(' '));
                    }
                }
                TextBoxB.MinValue = 0;
                TextBoxB.MaxValue = 150;
                TextBoxB.AllowOutOfRangeAutoCorrect = true;
                TextBoxB.Text = "0";
                if (Session[TextBoxB.ID.ToString() + nombreUser] != null)
                {
                    if (TextBoxB.ID.ToString().Equals(Session["nom" + nombreUser + TextBoxB.ID.ToString()].ToString()) == true)
                    {
                        TextBoxB.Text = Session[TextBoxB.ID.ToString() + nombreUser].ToString();
                    }
                }
                addPanelTextBoxNum(TextBoxB, nomPanel, pant, 1, step, cont);
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //crea un checkbox
        public void genCheckBox(DataTable _Xcdic, int pos, int pant, int var, string step, int cont)
        {
            string nomPanel = "Panel" + pant.ToString();
            step = "WizardStep" + pant.ToString();
            System.Web.UI.WebControls.CheckBox chkbox = new System.Web.UI.WebControls.CheckBox();
            chkbox.ID = "cx" + pant + "_" + cont + _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
            chkbox.ToolTip = chkbox.ID.ToString();
            chkbox.Text = _Xcdic.Rows[pos][6].ToString().TrimEnd(' ').Replace(":", "");
            addPanelCheckBox(chkbox, nomPanel, pant, 1, step, cont);
        }
        public void genDatePicker(DataTable _Xcdic, int pos, int pant, int var, string step, int cont)
        {
            try
            {
                string nomPanel = "Panel" + pant.ToString();
                step = "WizardStep" + pant.ToString();
                RadDatePicker TextBoxD = new RadDatePicker();
                TextBoxD.DateInput.DateFormat = "dd/MM/yyyy";
                TextBoxD.ID = "tb" + pant + "_" + cont + _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
                TextBoxD.ToolTip = TextBoxD.ID.ToString();
                decimal largo = Convert.ToDecimal(_Xcdic.Rows[pos][4].ToString().TrimEnd(' '));
                largo = (largo * 0.20m);
                int largo2 = Convert.ToInt32(largo);
                TextBoxD.Width = largo2;
                addPanelDatePicker(TextBoxD, nomPanel, pant, 1, step, cont);
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        public void genDateTimePicker(DataTable _Xcdic, int pos, int pant, int var, string step, int cont)
        {
            try
            {
                string nomPanel = "Panel" + pant.ToString();
                step = "WizardStep" + pant.ToString();
                RadDateTimePicker TextBoxD = new RadDateTimePicker();
                TextBoxD.DateInput.DateFormat = "HH:mm";
                TextBoxD.ID = "tb" + pant + _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
                TextBoxD.ToolTip = TextBoxD.ID.ToString();
                decimal largo = Convert.ToDecimal(_Xcdic.Rows[pos][4].ToString().TrimEnd(' '));
                largo = (largo * 0.40m);
                int largo2 = Convert.ToInt32(largo);
                TextBoxD.Width = largo2;
                addPanelDatePicker(TextBoxD, nomPanel, pant, 1, step, cont);
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //crea boton de clasificaciones
        public void genBtnCls(DataTable _Xcdic, int pos, int pant, string step)
        {
            try
            {
                RadButton _boton = new RadButton();
                _boton.ID = "_b" + pant + _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
                _boton.Text = "Guardar Clasificaciones";
                _boton.Click += new EventHandler(onClickBtnCls);
                _boton.Skin = "Outlook";
                string nomPanel = "Panel" + pant.ToString();
                addPanelBotonCls(_boton, nomPanel, pant, step);
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //crea el boton guardar para cada seccion
        public void genBtnGuardar(string nomPanel, int pant, int numBoton, string nom_boton)
        {
            foreach (Object obj in RadWizard1.Controls)
            {
                string paso = "WizardStep" + pant.ToString();
                if (obj is RadWizardStep)
                {
                    RadWizardStep temp = (RadWizardStep)obj;
                    if (temp.ID.ToString().Equals(paso) == true)
                    {
                        foreach (Object ob in temp.Controls)
                        {
                            if (ob is System.Web.UI.WebControls.Panel)
                            {
                                System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                if (temp2.ID.ToString().Equals(nomPanel) == true)
                                {
                                    RadButton btnGuarda = new RadButton();
                                    btnGuarda.ID = "btn_guarda_" + nomPanel + numBoton;
                                    btnGuarda.Click += new EventHandler((sender1, e1) => onClickBtnGuardar(sender1, e1, numBoton));
                                    btnGuarda.Text = "Guardar " + nom_boton;
                                    btnGuarda.ToolTip = btnGuarda.ID.ToString();
                                    btnGuarda.Skin = "Outlook";
                                    btnGuarda.AutoPostBack = true;
                                    temp2.Controls.Add(btnGuarda);
                                }
                            }
                        }
                    }
                }
            }
        }
        //Agrega el combobox en el panel actual y lo serializa.
        public void addPanelComboBox(RadComboBox cmb, string nomPanel, int pant, int var, string step)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel) == true)
                                    {
                                        cmb.ID.ToString();
                                        temp2.Controls.Add(cmb);
                                        if (cmb.ID.ToString().Contains("art_tip") == true)
                                        {
                                            //agrego los saltos de linea abajo del combo de tipo de articulo
                                            //temp2.Controls.Add(new LiteralControl("<br />"));
                                            //temp2.Controls.Add(new LiteralControl("<br />"));
                                        }
                                    }//cierre del if temp2 con el panel expuesto
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //Agrega el boton clasificaciones en el panel actual , lo serializa
        public void addPanelBotonCls(RadButton btn, string nomPanel, int pant, string step)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel))
                                    {
                                        temp2.Controls.Add(btn);
                                        temp2.Controls.Add(new LiteralControl("<br />"));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //Agrega los labels en panel y los serializa
        public void addPanelLabel(System.Web.UI.WebControls.Label rlb, string nomPanel, int pant, int var, string step)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel))
                                    {
                                        temp2.Controls.Add(rlb);
                                    }//cierre del if temp2 con el panel expuesto
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //Agrega el textarea al panel y lo serializa
        public void addPanelTextArea(System.Web.UI.WebControls.TextBox tbx, string nomPanel, int pant, int var, string step)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel) == true)
                                    {
                                        temp2.Controls.Add(tbx);
                                    }//cierre del if temp2 con el panel expuesto
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }

        }
        //Agrega el textbox al panel y lo serializa     
        public void addPanelTextBox(RadTextBox tbx, string nomPanel, int pant, int var, string step, int cont)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel) == true)
                                    {
                                        temp2.Controls.Add(tbx);
                                        if (tbx.ID.Equals("tb1sku_cve") == true)
                                        {
                                            variablesGuardar.Index = temp2.Controls.IndexOf(tbx);
                                            //temp2.Controls.Add(new LiteralControl("<strong>" + variablesGuardar.Index + "</strong>"));
                                        }
                                    }//cierre del if temp2 con el panel expuesto
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //agrega el textbox numerico al panel y lo serializa   
        public void addPanelTextBoxNum(RadNumericTextBox tbx, string nomPanel, int pant, int var, string step, int cont)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel) == true)
                                    {
                                        temp2.Controls.Add(tbx);
                                    }//cierre del if temp2 con el panel expuesto
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //agrega el checkbox al panel y lo serializa  
        public void addPanelCheckBox(System.Web.UI.WebControls.CheckBox cbx, string nomPanel, int pant, int var, string step, int cont)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel) == true)
                                    {
                                        temp2.Controls.Add(cbx);
                                    }//cierre del if temp2 con el panel expuesto
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //agrega el datepicker al panel y lo serializa  
        public void addPanelDatePicker(RadDatePicker tbx, string nomPanel, int pant, int var, string step, int cont)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel) == true)
                                    {
                                        temp2.Controls.Add(tbx);
                                    }//cierre del if temp2 con el panel expuesto
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //evento click del boton clasificaciones (para agregar una clasificacion)
        protected void onClickBtnCls(object sender, EventArgs e)
        {
            /////Sacar la informacion del  cb1subcls_cve y mandarlo al tb1balin 
            string step = "WizardStep" + (RadWizard1.ActiveStep.Index + 1).ToString();
            variablesGuardar.NewCombos = "1";
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    foreach (Object _temp2 in temp2.Controls)
                                    {
                                        if (_temp2 is RadComboBox)
                                        {
                                            RadComboBox rcb = (RadComboBox)_temp2;
                                            if (rcb.ID.ToString().Contains("cls_num") == true)
                                            {
                                                variablesGuardar.Cls_num = rcb.SelectedValue.ToString();
                                                Session["nom" + nombreUser + rcb.ID.ToString()] = rcb.ID.ToString();
                                                Session[rcb.ID.ToString() + nombreUser] = rcb.SelectedIndex;
                                            }
                                            else if (rcb.ID.ToString().Contains("subcls_cve") == true)
                                            {
                                                variablesGuardar.Subcls_cve = rcb.SelectedValue.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                variablesGuardar.VCls[0, 0] = "art_cve";
                variablesGuardar.VCls[0, 1] = variablesGuardar.Art_cveFin;
                variablesGuardar.VCls[1, 0] = "cc_cve";
                variablesGuardar.VCls[1, 1] = variablesGuardar.Art_cveFin; //variablesGuardar.Art_cveFin
                variablesGuardar.VCls[2, 0] = "cc_tipo";
                variablesGuardar.VCls[2, 1] = variablesGuardar.Prm1; //variablesGuardar.Art_tipBDD
                variablesGuardar.VCls[3, 0] = "cls_num";
                variablesGuardar.VCls[3, 1] = variablesGuardar.Cls_num; //variablesGuardar.Cls_num
                variablesGuardar.VCls[4, 0] = "subcls_cve";
                variablesGuardar.VCls[4, 1] = variablesGuardar.Subcls_cve; //variablesGuardar.Subcls_cve
                variablesGuardar.VCls[5, 0] = "per_hor";
                variablesGuardar.VCls[5, 1] = "";
                variablesGuardar.VCls[6, 0] = "per_ver";
                variablesGuardar.VCls[6, 1] = "";
                variablesGuardar.VCls[7, 0] = "art_tip";
                variablesGuardar.VCls[7, 1] = "";
                int index = Int32.Parse(RadWizard1.ActiveStep.ID.ToString().Substring(RadWizard1.ActiveStep.ID.Length - 1, 1));// Convert.ToInt32(RadWizard1.ActiveStepIndex.ToString());
                string tab = _con.obtieneCls(variablesGuardar.Prg_cves[index, 0]);
                string clasifica = _con.insertClasifica(variablesGuardar.VCls, tab.TrimEnd(' '), 8);


                if (clasifica == "Artículo Clasificado")
                {
                    btnFinalizar.Visible = true;
                    Session["indexCombo" + nombreUser] = -1;
                    /*int seccionNueva = Int32.Parse(variablesGuardar.SeccionVisible.Substring(0, 1));
                    seccionNueva = seccionNueva + 1;
                    int stepActual = Int32.Parse(step.Substring(step.Length - 1, 1))+1;*/
                    int seccionNueva = Int32.Parse(variablesGuardar.SeccionVisible.Substring(0, 1));
                    seccionNueva = seccionNueva + 1;
                    int stepActual = Int32.Parse(step.Substring(step.Length - 1, 1));
                    variablesGuardar.NumBloque = Int32.Parse(variablesGuardar.SeccionVisible.Substring(0, 1));
                    //variablesGuardar.SeccionVisible = "0_"+stepActual;
                    variablesGuardar.SeccionVisible = seccionNueva + "_" + stepActual;
                    Response.Write("<script type=\"text/javascript\">alert('" + clasifica + "'); window.location.href =  window.location.href; </script>"); //window.location.href =  window.location.href;
                }
                else
                {
                    if (clasifica.Equals("") == true)
                    {
                        clasifica = "Hubo un error al clasificar, Intente de nuevo";
                    }
                    Response.Write("<script type=\"text/javascript\">alert('" + clasifica + "'); window.location.href =  window.location.href; </script>"); //window.location.href =  window.location.href;
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //evento click del boton guardar (para guardar el bloque actual)
        protected void onClickBtnGuardar(object sender, EventArgs e, int secc)
        {
            int index = Int32.Parse(RadWizard1.ActiveStep.ID.ToString().Substring(RadWizard1.ActiveStep.ID.Length - 1, 1));// Convert.ToInt32(RadWizard1.ActiveStepIndex.ToString());
            string paso = RadWizard1.ActiveStep.ID.ToString();
            string nomPanel = "Panel" + index.ToString();
            try
            {
                ValidaDatosBloque(paso, secc);
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
                RadTicker1.Visible = true;
            }
        }
        //inserta secciones directamente al codigo html para hacer saltos de lineas o barras para el acomodo de los controles
        public void addPanelHTML(int pant, int reng)
        {
            try
            {
                string _step = "WizardStep" + pant.ToString();
                string pnl = "Panel" + pant.ToString();
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(_step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(pnl) == true)
                                    {
                                        /* si quiero que los controles se dibujen sin los saltos de linea, comentar estew bloque*/
                                        if (reng == 1)
                                        {
                                            temp2.Controls.Add(new LiteralControl("<br />"));
                                            temp2.Controls.Add(new LiteralControl("<br />"));
                                        }
                                    }//cierre del if temp2 con el panel expuesto
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //crea la secciones en las que se divide una pantalla
        public void addSeccion(string nomPanel, int pant, string step, int numSecc, string nomBoton)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel))
                                    {
                                        string seccionActiva = numSecc + "_" + pant;
                                        if (variablesGuardar.SeccionVisible.Equals(seccionActiva) == true)
                                        {
                                            temp2.Controls.Add(
                                                    new LiteralControl("<button class='btn btn-primary' type='button'" +
                                                    "data-toggle='collapse' data-target='#collapseExample" + seccionActiva + "' aria-expanded='false'" +
                                                    "aria-controls='collapseExample'>" + nomBoton + "</button><br />" +
                                                    "<div class='collapse.in' id='collapseExample" + seccionActiva + "'>" +
                                                    "<div class='well' id='divid'>"));
                                        }
                                        else
                                        {
                                            temp2.Controls.Add(
                                                    new LiteralControl("<button class='btn btn-primary' type='button' " + //disabled type='button'
                                                    "data-toggle='collapse' data-target='#collapseExample" + seccionActiva + "' aria-expanded='false'" +
                                                    "aria-controls='collapseExample'>" + nomBoton + "</button><br />" +
                                                    "<div class='collapse'  id='collapseExample" + seccionActiva + "'>" + //style='pointer-events:none;'
                                                    "<div class='well' id='divid'>"));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //cierra cada seccion en la cual se divido la pantalla
        public void addCierreSeccion(string nomPanel, int pant, string step, int numSecc)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel))
                                    {
                                        temp2.Controls.Add(new LiteralControl("</div></div><br />"));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //metodo que se utiliza por los combos para generar el evento de seleccion de item's y actualiza los combos consecutivos
        protected void combo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e, int secc)
        {
            Evento = e;
            SenDer = sender;
            numPantalla = secc;
            RadComboBox ddl = (RadComboBox)sender;
            DataTable aux_dt;
            numExtras = 1;
            string step = RadWizard1.ActiveStep.ID.ToString();
            string nom = "";
            string _balin4 = "";
            try
            {
                _pant = Int32.Parse(step.Substring(step.Length - 1, 1));
                nom = ddl.ID.ToString();
                string wpaso = nom.Substring(2, nom.Length - 2);
                wpaso = wpaso.Substring(0, 1);//me indica el orden en la tabla WizardPasos de donde se toma el prg_cve para obtener 
                string campo = nom.Substring(5, nom.Length - 5);
                string prg_cve = "";
                string tipo_cc = "";
                tipo_cc = variablesGuardar.Tipo_ccs[_pant, 0];
                prg_cve = variablesGuardar.Prg_cves[_pant, 0];
                DataTable temp_xcdic = _con.xcdic(prg_cve, variablesGuardar.User, tipo_cc, variablesGuardar.Ef_cve);
                

                for (int x = 0; x < temp_xcdic.Rows.Count; x++)
                //recorre el xcdic en busca del valor de campo y orden para sincronizar con la tabla temp_GLqry 
                {
                    string tem_campo = temp_xcdic.Rows[x][1].ToString().TrimEnd(' ');
                    tem_campo = tem_campo.Trim(' ');
                    if ((tem_campo.Equals(campo) == true))
                    {
                        /* balin4 trae el sp_qtiparta */
                        if (temp_xcdic.Rows[x][21].ToString().Trim(' ').Equals("") == false)
                        {
                            int bust = buscaEX(1);
                            if (bust >= 1)
                            {
                                DeleteControl(_pant, nom);
                            }
                            variablesGuardar.Art_nom = e.Text;
                            variablesGuardar.Art_tip = e.Value.ToString().Trim(' ');
                            variablesGuardar.Prm1 = e.Value.ToString().Trim(' ');
                            _balin4 = temp_xcdic.Rows[x][21].ToString().TrimEnd(' ');
                            if (_balin4.Equals("sp_qtiparta") == false)
                            {
                                string p = "'" + variablesGuardar.Art_nom.Trim(' ') + "','" + secc + "','0'";
                                string cve = variablesGuardar.Art_tip.Trim(' ');
                                aux_dt = _con.ExecQryTabla(_balin4, p);//verifica si hay subCombos 
                                aux_dt.DefaultView.Sort = "orden desc";
                            }
                            else
                            {
                                aux_dt = _con.qtiparta(variablesGuardar.Art_tip, _balin4);//verifica si hay subCombos 
                                aux_dt.DefaultView.Sort = "orden desc";
                            }
                            if (variablesGuardar.Art_tip == "HE")
                            {
                                if (variablesGuardar.Ban_borra > 0)
                                {
                                    variablesGuardar.Index = 96;
                                }
                                else
                                {
                                    variablesGuardar.Index = 86;
                                }
                            }
                            else if (variablesGuardar.Art_tip == "HTT")
                            {
                                if (variablesGuardar.Ban_borra > 0)
                                {
                                    variablesGuardar.Index = 90;
                                }
                                else
                                {
                                    variablesGuardar.Index = 86;
                                }
                            }
                            else if (variablesGuardar.Art_tip == "C")
                            {
                                if (variablesGuardar.Ban_borra > 0)
                                {
                                    variablesGuardar.Index = 98;
                                }
                                else
                                {
                                    variablesGuardar.Index = 86;
                                }
                            }
                            else if (variablesGuardar.Art_tip == "PO1")
                            {
                                if (variablesGuardar.Ban_borra > 0)
                                {
                                    variablesGuardar.Index = 90 + (variablesGuardar.Ban_borra / 3);
                                }
                                else
                                {
                                    variablesGuardar.Index = 86;
                                }
                            }
                            else
                            {
                                /*if (variablesGuardar.Ban_borra > 0)
                                {
                                    variablesGuardar.Index = 96;
                                }
                                else
                                {
                                    variablesGuardar.Index = 86;
                                }*/
                            }
                            aux_dt = aux_dt.DefaultView.ToTable();
                            int fila = aux_dt.Rows.Count;//indica cuantos subCombos se encontraron
                            for (int g = 0; g < fila; g++)
                            {
                                if (g >= 1) //si g es mayor igual que 1 entonces el balin4 regreso mas controles a dibujar
                                {
                                    string uno = aux_dt.Rows[g][1].ToString();
                                    string tipo_for = aux_dt.Rows[g][27].ToString();//tenia 14
                                    int _rows = aux_dt.Rows.Count;
                                    if (tipo_for == "94")// si tipo for es 0 entonces inserta un textbox 
                                    {
                                        string lb = aux_dt.Rows[g][6].ToString() + "  ";
                                        int bus = buscaEX(1);
                                        if ((bus >= _rows) && (variablesGuardar.Ban_borra > 0))
                                        {
                                            DeleteControl(_pant, nom);
                                        }
                                        numExtras++;
                                        genTextboxNew(_pant, g, aux_dt, variablesGuardar.Art_tip, step, nom, lb);
                                    }// si tipo for es mayor o igual que 1 inserta un combo
                                    else
                                    {
                                        if (tipo_for != "0")
                                        {
                                            nom = "cb" + g.ToString() + aux_dt.Rows[g][1].ToString();
                                            int bus = buscaEX(1);
                                            int _rows_ = aux_dt.Rows.Count;
                                            if (bus >= _rows_)
                                            {
                                                DeleteControl(_pant, nom);
                                            }
                                            numExtras++;
                                            genComboBoxNew(_balin4, _pant, 0, aux_dt, variablesGuardar.Art_tip, step, nom, g, 1);
                                        }
                                    }
                                }
                                else
                                {
                                    string uno1 = aux_dt.Rows[g][1].ToString();
                                    string tipo_for1 = aux_dt.Rows[g][27].ToString();
                                    if ((tipo_for1 == "91") || (tipo_for1 == "80"))
                                    {
                                        int _rows_ = aux_dt.Rows.Count;
                                        int bus = buscaEX(1);
                                        if (bus >= _rows_)
                                        {
                                            DeleteControl(_pant, nom);
                                        }
                                        genComboBoxNew(_balin4, _pant, 0, aux_dt, variablesGuardar.Art_tip, step, nom, g, 1);
                                    }
                                    else
                                    {
                                        string lb = aux_dt.Rows[g][6].ToString() + "  ";
                                        int bus = buscaEX(1);
                                        int _rows_ = aux_dt.Rows.Count;
                                        if (bus >= _rows_)
                                        {
                                            DeleteControl(_pant, nom);
                                        }
                                        genTextboxNew(_pant, g, aux_dt, variablesGuardar.Art_tip, step, nom, lb);
                                    }
                                }
                            }//cierre del ciclo 
                            variablesGuardar.Art_nom = "";
                            variablesGuardar.Art_tip = "";
                            variablesGuardar.NumSecciones = numExtras;
                        }
                        else if (temp_xcdic.Rows[x + 1][23].ToString().Trim(' ') != "" && temp_xcdic.Rows[x][12].ToString().Equals("0") == false && temp_xcdic.Rows[x][1].ToString().Equals("cls_num") == true)
                        {
                            string datoAux = e.Value.ToString();
                            string sp_aux = "";
                            string camp_aux = "cb" + wpaso.ToString() + temp_xcdic.Rows[x + 1][1].ToString().TrimEnd(' ');
                            string nom_aux = temp_xcdic.Rows[x + 1][1].ToString().TrimEnd(' ');
                            string tbl_aux = temp_xcdic.Rows[x + 1][0].ToString().TrimEnd(' ');
                            int orden = Int32.Parse(temp_xcdic.Rows[x+1][5].ToString().TrimEnd(' '));

                            DataTable temp_qrycom = _con.temp_xuarrays(prg_cve, 1, numPantalla);//obtiene el GL_qrycom
                            if (nom_aux.Equals("subcls_cve") == true)
                            {
                                datoAux = "'" + variablesGuardar.Prm1 + "'," + datoAux;
                            }
                            for (int i = 0; i < temp_qrycom.Rows.Count; i++)
                            {
                                if (i == orden - 1)
                                {
                                    if (temp_qrycom.Rows[i][0] != null)
                                    {
                                        if (temp_qrycom.Rows[i][0].ToString().Equals("") == false)
                                        {
                                            if (prg_cve.Equals("cccatcce") == true)
                                            {
                                                sp_aux = temp_qrycom.Rows[i - 1][0].ToString();
                                            }
                                            else
                                            {
                                                sp_aux = temp_qrycom.Rows[i][0].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            DataTable dato = _con.ExecQryTabla(sp_aux.TrimEnd(' '), datoAux);
                            _SubCombo(dato, camp_aux, step);
                        }
                        else
                        {
                            string datoAux = e.Value.ToString();
                            string sp_aux = "";
                            string camp_aux = "cb" + wpaso.ToString() + "_" + numPantalla + temp_xcdic.Rows[x + 1][1].ToString().TrimEnd(' ');
                            string nom_aux = temp_xcdic.Rows[x + 1][1].ToString().TrimEnd(' ');
                            string tbl_aux = temp_xcdic.Rows[x + 1][0].ToString().TrimEnd(' ');
                            int orden = Int32.Parse(temp_xcdic.Rows[x + 1][5].ToString().TrimEnd(' '));

                            if (campo.Equals("art_tip") == false && campo.Equals("cmp_cve") == false)
                            {
                                DataTable temp_qrycom = _con.temp_xuarrays(prg_cve, 1, numPantalla);//obtiene el GL_qrycom
                                if (nom_aux.Contains("edo_c") == true)
                                {
                                    variablesGuardar.Edo_cve = datoAux;
                                    if (numPantalla == 1)
                                    {
                                        orden = 1;
                                    }
                                    else
                                    {
                                        orden = 2;
                                    }
                                    
                                    //datoAux = "'" + variablesGuardar.Edo_cve + "'";
                                }
                                else if (nom_aux.Contains("cd_c") == true)
                                {
                                    variablesGuardar.Cd_cve = datoAux;
                                    datoAux = "'" + variablesGuardar.Edo_cve + "'," + "'" + variablesGuardar.Cd_cve + "'";
                                    if (numPantalla == 1)
                                    {
                                        orden = 2;
                                    }
                                    else
                                    {
                                        orden = 3;
                                    }
                                }
                                else if (nom_aux.Contains("mpo_c") == true)
                                {
                                    variablesGuardar.Mpio_cve = datoAux;
                                    datoAux = "'" + variablesGuardar.Edo_cve + "','" + variablesGuardar.Cd_cve + "','" + variablesGuardar.Mpio_cve + "'";
                                    if (numPantalla == 1)
                                    {
                                        orden = 3;
                                    }
                                    else
                                    {
                                        orden = 4;
                                    }
                                }
                                else if (nom_aux.Contains("cp") == true)
                                {
                                    variablesGuardar.Cp_cve = datoAux;
                                    datoAux = "'" + variablesGuardar.Edo_cve + "','" + variablesGuardar.Cd_cve + "','" + variablesGuardar.Mpio_cve + "','" + variablesGuardar.Cp_cve + "'";
                                    if (numPantalla == 1)
                                    {
                                        orden = 5;
                                    }
                                    else
                                    {
                                        orden = 5;
                                    }
                                }
                                else if (nom_aux.Equals("cmp_cve") == true)
                                {
                                    orden = 4;
                                }
                                for (int i = 0; i < temp_qrycom.Rows.Count; i++)
                                {
                                    if (i == orden)
                                    {
                                        if (temp_qrycom.Rows[i][0] != null)
                                        {
                                            if (temp_qrycom.Rows[i][0].ToString().Equals("") == false)
                                            {
                                                sp_aux = temp_qrycom.Rows[i][0].ToString();
                                            }
                                        }
                                    }
                                }
                                DataTable dato = _con.ExecQryTabla(sp_aux.TrimEnd(' '), datoAux);
                                _SubCombo(dato, camp_aux, step);
                            }
                            if (prg_cve.Equals("mcact") == true)
                            {
                                DataTable temp_qrycom = _con.temp_xuarrays(prg_cve, 1, numPantalla);//obtiene el GL_qrycom
                                sp_aux = temp_qrycom.Rows[1][0].ToString();
                                DataTable dato = _con.ExecQryTabla(sp_aux.TrimEnd(' '), datoAux);
                                _SubCombo(dato, camp_aux, step);
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                Response.Write("<script type=\"text/javascript\">alert('Inconsistencia en los datos seleccionados. Se activo el try catch en |=| combo_SelectedIndexChanged (" + t.Message.ToString() + ")'); window.location.href = window.location.href;</script>");
                t.Message.ToString();
                variablesGuardar.ArticuloClave = null;
            }
        }
        //se posiciona en el combo actual para refrescar la informacion
        public void genComboBoxNew(string _balin4, int pant, int pos, DataTable dt_aux, string cve, string step, string nombre, int fila, int f)
        {
            string sys_var;
            string balin2;
            string balin3;
            string balin4;
            string balin5;
            string balin6;
            string balin7;
            string tem;
            int m = 0;
            int n = 0;
            variablesGuardar.ComboActual = 0;

            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    foreach (Object _temp2 in temp2.Controls)
                                    {
                                        n++;
                                        if (_temp2 is RadComboBox)
                                        {
                                            RadComboBox rcb = (RadComboBox)_temp2;
                                            if (rcb.ID.ToString().Equals("cb1_0art_tip") == true)
                                            {
                                                /*if (f == 1)
                                                {*/
                                                    RadComboBox extra = new RadComboBox();
                                                    extra.AllowCustomText = false;
                                                    extra.Focus();
                                                    extra.MarkFirstMatch = true;
                                                    string id = "excb" + variablesGuardar.Prg_cves[pant, 0] + dt_aux.Rows[fila][1].ToString().TrimEnd(' ');
                                                    if (id.Contains(" ") == true)
                                                    {
                                                        id = id.Replace(" ", "");
                                                        extra.ID = id;
                                                    }
                                                    else
                                                    {
                                                        extra.ID = id;
                                                    }
                                                    extra.ToolTip = extra.ID.ToString();
                                                    extra.ToolTip = id;
                                                    decimal largo = Convert.ToDecimal(dt_aux.Rows[fila][4].ToString().TrimEnd(' '));
                                                    largo = (largo * 0.09m);
                                                    int largo2 = Convert.ToInt32(largo);
                                                    extra.Width = largo2;
                                                    Session["Width" + nombreUser + id] = largo2;
                                                    variablesGuardar.ComboActual = variablesGuardar.ComboActual + 1;
                                                    n = variablesGuardar.ComboActual;
                                                    variablesGuardar.Ban_borra = variablesGuardar.Ban_borra + 1;
                                                    string cmp = dt_aux.Rows[fila][1].ToString();
                                                    System.Web.UI.WebControls.Label lb = new System.Web.UI.WebControls.Label();
                                                    lb.ID = "exLB" + n.ToString() + dt_aux.Rows[fila][1].ToString().Trim(' ');
                                                    sys_var = dt_aux.Rows[fila][12].ToString().TrimEnd(' ');
                                                    balin2 = dt_aux.Rows[fila][19].ToString().TrimEnd(' ');
                                                    balin3 = dt_aux.Rows[fila][20].ToString().TrimEnd(' ');
                                                    balin4 = dt_aux.Rows[fila][21].ToString().TrimEnd(' ');
                                                    balin5 = dt_aux.Rows[fila][22].ToString().TrimEnd(' ');
                                                    balin6 = dt_aux.Rows[fila][23].ToString().TrimEnd(' ');
                                                    balin7 = dt_aux.Rows[fila][24].ToString().TrimEnd(' ');
                                                    lb.Text = dt_aux.Rows[fila][6].ToString().TrimEnd(' ');
                                                    tem = "'" + cve.Trim(' ') + "','" + balin6 + "'";
                                                    if (balin4.TrimEnd(' ') == "qcomartinf")
                                                    {
                                                        balin4 = balin4.TrimEnd(' ') + "1";
                                                    }
                                                    else if (balin4.TrimEnd(' ').Equals("qcomcc") == true)
                                                    {
                                                        balin4 = balin4.TrimEnd(' ') + "1";
                                                        tem = "'o'";
                                                    }
                                                    else
                                                    {
                                                        balin4 = balin4.TrimEnd(' ');
                                                    }
                                                    Session["balin2" + nombreUser + id] = balin2;
                                                    Session["balin4" + nombreUser + id] = balin4;
                                                    Session["tem" + nombreUser + id] = tem;
                                                    DataTable dato = _con.ExecQryTabla(balin4, tem);

                                                    /*if (dato.Columns.Count > 1)
                                                    {*/
                                                        extra.Items.Clear();
                                                        extra.DataSource = dato;
                                                        extra.DataTextField = dato.Columns[0].ColumnName.ToString();
                                                        extra.DataValueField = dato.Columns[1].ColumnName.ToString();
                                                        extra.DataBind();
                                                        if (_flujo == "mnuCatAct")
                                                        {

                                                            temp2.Controls.AddAt(10, new LiteralControl("<br />"));
                                                            temp2.Controls.AddAt(11, lb);
                                                            temp2.Controls.AddAt(12, extra);
                                                            temp2.Controls.AddAt(13, new LiteralControl("<br />"));
                                                        }
                                                        else
                                                        {
                                                            if (balin2.Equals("1") == true)
                                                            {
                                                                temp2.Controls.AddAt(variablesGuardar.Index + 1, lb);
                                                                temp2.Controls.AddAt(variablesGuardar.Index + 2, extra);
                                                            }
                                                            else
                                                            {
                                                                temp2.Controls.AddAt(3, lb);
                                                                temp2.Controls.AddAt(4, extra);
                                                            }
                                                        }
                                                        Session["labelDinamico" + nombreUser + numExtras] = lb.Text;
                                                        Session["controlDinamico" + nombreUser + numExtras] = extra.ID.ToString();
                                                    /*}
                                                    else
                                                    {
                                                        if (extra.ID.ToString().Equals("excbicartstDetalle") == true)
                                                        {
                                                            RadTextBox tbExtra = new RadTextBox();
                                                            tbExtra.ID = extra.ID.ToString();
                                                            tbExtra.CssClass = "form-control2";
                                                            temp2.Controls.AddAt(6, lb);
                                                            temp2.Controls.AddAt(7, tbExtra);
                                                            Session["labelDinamico" + nombreUser + numExtras] = lb.Text;
                                                            Session["controlDinamico" + nombreUser + numExtras] = tbExtra.ID.ToString();
                                                        }
                                                        else
                                                        {
                                                            extra.Items.Clear();
                                                            DataTable dato2 = null;
                                                            if (extra.ID.ToString() == "excbicartsufabricante")
                                                            {
                                                                Session["balin4" + nombreUser + extra.ID.ToString()] = "qcomcc1";
                                                                Session["tem" + nombreUser + extra.ID.ToString()] = "o";
                                                                dato2 = _con.ExecQryTabla("qcomcc1", "o");
                                                            }
                                                            else
                                                            {
                                                                Session["balin4" + nombreUser + extra.ID.ToString()] = "qcomart51";
                                                                Session["tem" + nombreUser + extra.ID.ToString()] = "'m01','0'";
                                                                dato2 = _con.ExecQryTabla("qcomart51", "'m01','0'");
                                                            }
                                                            extra.DataSource = dato2;
                                                            extra.DataTextField = dato2.Columns[0].ColumnName.ToString();
                                                            extra.DataValueField = dato2.Columns[0].ColumnName.ToString();
                                                            extra.DataBind();
                                                            temp2.Controls.AddAt(6, lb);
                                                            temp2.Controls.AddAt(7, extra);
                                                            Session["labelDinamico" + nombreUser + numExtras] = lb.Text;
                                                            Session["controlDinamico" + nombreUser + numExtras] = extra.ID.ToString();
                                                            if (cont == 3)
                                                            {
                                                                temp2.Controls.Add(new LiteralControl("<br /><br />")); cont = 1;
                                                            }
                                                            temp2.Controls.Add(new LiteralControl("<br />"));
                                                        }
                                                    }*/
                                                    break;
                                                /*}
                                                else
                                                {
                                                    sys_var = dt_aux.Rows[fila][12].ToString();
                                                    balin4 = dt_aux.Rows[fila][21].ToString();
                                                    balin5 = dt_aux.Rows[fila][22].ToString();
                                                    balin6 = dt_aux.Rows[fila][23].ToString();
                                                    balin7 = dt_aux.Rows[fila][24].ToString();
                                                    tem = "'" + cve + "','" + balin6 + "'";
                                                    if (balin4.TrimEnd(' ') == "qcomartinf")
                                                    {
                                                        balin4 = balin4.TrimEnd(' ') + "1";
                                                    }
                                                    else
                                                    {
                                                        balin4 = balin4.TrimEnd(' ');
                                                    }
                                                    DataTable dato = _con.ExecQryTabla(balin4, tem);
                                                    if (dato.Rows.Count > 1)
                                                    {
                                                        rcb.Items.Clear();
                                                        rcb.DataSource = dato;
                                                        rcb.DataTextField = dato.Columns[0].ColumnName.ToString();
                                                        rcb.DataValueField = dato.Columns[1].ColumnName.ToString();
                                                        rcb.DataBind();
                                                    }
                                                    if (dato.Rows.Count == 1)
                                                    {
                                                        rcb.Items.Clear();
                                                        rcb.DataSource = dato;
                                                        rcb.DataTextField = dato.Columns[0].ColumnName.ToString();
                                                        rcb.DataValueField = dato.Columns[0].ColumnName.ToString();
                                                        rcb.DataBind();
                                                    }
                                                    break;
                                                }*/
                                            }
                                            addPanelHTML(pant, 0);
                                        }
                                        m++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //existen casos especiales de combos y se agregaron en esta funcion
        public void _SubCombo(DataTable datos, string nomCombo, string step)
        {
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    foreach (Object _temp2 in temp2.Controls)
                                    {
                                        if (_temp2 is RadComboBox)
                                        {
                                            RadComboBox rcb = (RadComboBox)_temp2;
                                            if (rcb.ID.ToString().Equals(nomCombo) == true)
                                            {
                                                DataTable dato = datos;
                                                if (dato.Rows.Count > 0)
                                                {
                                                    int col = dato.Columns.Count;
                                                    rcb.Items.Clear();
                                                    rcb.DataSource = dato;
                                                    if (col == 1)
                                                    {
                                                        rcb.DataTextField = dato.Columns[0].ColumnName.ToString();
                                                        rcb.DataValueField = dato.Columns[0].ColumnName.ToString();
                                                    }
                                                    else
                                                    {
                                                        rcb.DataTextField = dato.Columns[0].ColumnName.ToString();
                                                        rcb.DataValueField = dato.Columns[1].ColumnName.ToString();
                                                    }
                                                    rcb.DataBind();
                                                    rcb.Focus();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        //Procedimiento que cera los texbos multilineas para  los casos donde se anexan varios letrero ejemplo icartcls
        public void genTextboxNew(int _pant, int g, DataTable aux_dt, string art_tip, string step, string nom, string letrero)
        {
            System.Web.UI.WebControls.TextBox tb = new System.Web.UI.WebControls.TextBox();
            string di = "extb" + variablesGuardar.Prg_cves[_pant, 0] + aux_dt.Rows[g][1].ToString();
            if (di.Contains(' ') == true)
            {
                di = di.Replace(' ', '_');
                tb.ID = di;
            }
            else
            {
                tb.ID = di;
            }
            tb.Width = 150;
            tb.CssClass = "form-control2";
            tb.ToolTip = di;
            if (aux_dt.Rows[g][18] != null || aux_dt.Rows[g][18] != "")
            {
                int maxLen = Convert.ToInt32(aux_dt.Rows[g][18].ToString().TrimEnd(' '));
                if (maxLen > 0)
                {
                    tb.MaxLength = Convert.ToInt32(aux_dt.Rows[g][18].ToString().TrimEnd(' '));
                }
            }
            string nomPanel = "Panel" + _pant.ToString();
            System.Web.UI.WebControls.Label lb = new System.Web.UI.WebControls.Label();
            string di1 = "exlb" + aux_dt.Rows[g][2].ToString() + aux_dt.Rows[g][1].ToString();
            if (di1.Contains(' ') == true)
            {
                di1 = di1.Replace(' ', '_');
                lb.ID = di1;
            }
            else
            {
                lb.ID = di1;
            }
            lb.Text = letrero;
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(step) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel temp2 = (System.Web.UI.WebControls.Panel)ob;
                                    if (temp2.ID.ToString().Equals(nomPanel))
                                    {
                                        temp2.Controls.AddAt(3, lb);
                                        temp2.Controls.AddAt(4, tb);
                                        Session["labelDinamico" + nombreUser + numExtras] = lb.Text;
                                        Session["controlDinamico" + nombreUser + numExtras] = tb.ID.ToString();
                                    }//cierre del if temp2 con el panel expuesto
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        // valida que la informacion este correcta segun el tipeado de la tabla y los campos, asi mismo tambien y solo si estan correctos los guarda en la BDD
        public void ValidaDatosBloque(string nom_paso, int secc)
        {
            int errorCaptura = 0;
            string nomError = "";
            string[] vec = new string[] { "°", "!", "#", "$", "&", "(", ")", "=", "?", "¿", "¡", "+", "´", "{", "}", "-", ";", ":", "_", "'" };
            string[] vec_int = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "." };
            int p = Convert.ToInt32(nom_paso.Substring(nom_paso.Length - 1, 1));
            try
            {
                foreach (Object _obj in RadWizard1.Controls)
                {
                    if (_obj is RadWizardStep)
                    {
                        RadWizardStep _temp = (RadWizardStep)_obj;
                        if (_temp.ID.ToString().Equals(nom_paso) == true)
                        {
                            foreach (Object obj in _temp.Controls)
                            {
                                if (obj is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel pnl = (System.Web.UI.WebControls.Panel)obj;
                                    foreach (Object controles in pnl.Controls)
                                    {
                                        if (controles is RadTextBox)
                                        {
                                            RadTextBox temp = (RadTextBox)controles;
                                            string Variable = temp.Text;
                                            string campo_aux = temp.ID.Substring(3, temp.ID.Length - 3).ToString();
                                            string nomLabel = _con.nombreLabel(campo_aux, variablesGuardar.Prg_cves[p, 0], variablesGuardar.Art_tipVal, secc);
                                            int tipo_val = _con._validacion(campo_aux, variablesGuardar.Prg_cves[p, 0], variablesGuardar.Art_tipVal, secc);
                                            if ((tipo_val == 1) && (temp.Enabled == true))
                                            { // Clave (no en blanco, no espacios intermedios, no caracteres raros )
                                                string aux = Variable;
                                                if (aux == "")
                                                {
                                                    errorCaptura++;
                                                    nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < vec.Length; j++)
                                                    {
                                                        if (aux.ToString().Contains(vec[j].ToString()))
                                                        {
                                                            errorCaptura++;
                                                            nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                        }
                                                    }
                                                }
                                            }

                                            if ((tipo_val == 2) && (temp.Enabled == true))
                                            {//Campos que no deben ir an blanco (Nombre por ejemplo) 
                                                string aux = Variable;
                                                if (aux == "" || aux == " ")
                                                {
                                                    errorCaptura++;
                                                    nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < vec.Length; j++)
                                                    {
                                                        if (aux.ToString().Contains(vec[j].ToString()))
                                                        {
                                                            errorCaptura++;
                                                            nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                        }
                                                    }
                                                }
                                            }
                                            int _ban = 0;
                                            if ((tipo_val == 3) && (temp.Enabled == true))
                                            {//Rango para campos numericos y se puede modificar
                                                string aux = Variable;
                                                if (aux != "" || aux != " ")
                                                {
                                                    for (int i = 0; i < aux.Length; i++)
                                                    {
                                                        string aux1 = aux[i].ToString();
                                                        for (int j = 0; j < 10; j++)
                                                        {
                                                            if (aux1.Equals(vec_int[j].ToString()))
                                                            {
                                                                j = 10;
                                                                _ban = 1;
                                                            }
                                                            else
                                                            {
                                                                errorCaptura++;
                                                                nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    errorCaptura++;
                                                    nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                }
                                            }
                                            if ((tipo_val >= 4) && (temp.Enabled == true))
                                            {//Para campos numericos que no se pueden modificar 
                                                string aux = Variable;
                                                int ban1 = 0;
                                                if (aux != "")
                                                {
                                                    for (int i = 0; i < aux.Length; i++)
                                                    {
                                                        string aux1 = aux[i].ToString();
                                                        for (int j = 0; j < 10; j++)
                                                        {
                                                            if (aux1.Equals(vec_int[j].ToString()))
                                                            {
                                                                j = 10;
                                                                ban1 = 1;
                                                            }
                                                            else
                                                            {
                                                                ban1 = 2;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    errorCaptura++;
                                                    nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                }

                                                if (ban1 == 0)
                                                {
                                                    errorCaptura++;
                                                    nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                }
                                            }
                                        }
                                        if (controles is RadComboBox)
                                        {
                                            RadComboBox temp = (RadComboBox)controles;
                                            string Variable = temp.SelectedValue.ToString();
                                            string variable2 = temp.Text;
                                            string campo_aux = temp.ID.ToString().Substring(3, temp.ID.Length - 3).ToString();
                                            string nomLabel = _con.nombreLabel(campo_aux, variablesGuardar.Prg_cves[p, 0], variablesGuardar.Art_tipVal, secc);
                                            int tipo_val = _con._validacion(campo_aux, variablesGuardar.Prg_cves[p, 0], variablesGuardar.Art_tipVal, secc);
                                            if ((tipo_val == 1) && (temp.Enabled == true))
                                            { // Clave (no en blanco, no espacios intermedios, no caracteres raros )
                                                string aux = Variable.TrimEnd(' ');
                                                string aux2 = variable2.TrimEnd(' ');
                                                if (aux == "")
                                                {
                                                    if (aux2 == "")
                                                    {
                                                        errorCaptura++;
                                                        nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                    }
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < vec.Length; j++)
                                                    {
                                                        if (aux.ToString().Contains(vec[j].ToString()))
                                                        {
                                                            errorCaptura++;
                                                            nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                        }
                                                    }
                                                }
                                            }
                                            if ((tipo_val == 2) && (temp.Enabled == true))
                                            {//Campos que no deben ir an blanco (Nombre por ejemplo) 
                                                string aux = Variable.TrimEnd(' ');
                                                if (aux == "" || aux == " ")
                                                {
                                                    errorCaptura++;
                                                    nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                }
                                                else
                                                {
                                                    for (int j = 0; j < vec.Length; j++)
                                                    {
                                                        if (aux.ToString().Contains(vec[j].ToString()))
                                                        {
                                                            errorCaptura++;
                                                            nomError = nomLabel.TrimEnd(' ').Replace(":", "") + "," + nomError;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (errorCaptura > 0)
                {
                    string coma = nomError.Substring(nomError.Trim().Length - 1, 1);
                    if (coma == ",")
                    {
                        nomError = nomError.Substring(0, nomError.Trim().Length - 1);
                    }
                    int r = cargarValores(variablesGuardar.Prg_cves[p, 0], nom_paso);
                    variablesGuardar.ErrorInsert = 1;
                    variablesGuardar.NewCombos = null;
                    variablesGuardar.ErrorArttip = 1;
                    Response.Write("<script type=\"text/javascript\">alert('" + errorCaptura + " campos ( " + nomError + " ) han sido mal capturados o estan vacíos, verifique la información e intente nuevamente'); window.location.href =  window.location.href; </script>"); //window.location.href =  window.location.href;
                }
                else
                {
                    string varResult = "";
                    string pasoName = nom_paso;
                    int r = cargarValores(variablesGuardar.Prg_cves[p, 0], pasoName);
                    if (r > 0)
                    {
                        _con.cargaFlujo(_flujo);
                        varResult = _con.guardarBloque(variablesGuardar.Prg_cves[p, 0], secc);
                        //varResult = "ACC0000";
                        if (varResult == null || varResult == "")
                        {
                            varResult = ">Ocurrio un error intentelo de nuevo.\r\nThe statement has been terminated.";
                        }
                        if (varResult.Substring(0, 1).ToString().Equals(">") == true)
                        {
                            z = 0;
                        }
                        else
                        {
                            variablesGuardar.Art_cveFin = varResult;
                            z = 1;
                        }
                    }
                    else
                    {
                        z = 0;
                    }
                    if (z == 0)
                    {
                        string error = "";
                        error = varResult;
                        if ((error != "") && (error.Length > 1))
                        {
                            error = error.Substring(1, error.Length - 1);
                            int k = error.IndexOf('\r');
                            if (k >= 1)
                            {
                                error = error.Substring(0, k);
                            }
                            error = error.Replace("'", " ");
                            _con.borrar();
                            variablesGuardar.ErrorInsert = 1;
                            variablesGuardar.NewCombos = null;
                            variablesGuardar.ErrorArttip = 1;
                            string s = "<SCRIPT language='javascript'>alert('" + error.Replace("\r\n", "\\n").Replace("'", "") + "'); window.location.href =  window.location.href; </SCRIPT>"; //window.location.href =  window.location.href;
                            Type cstype = this.GetType();
                            ClientScriptManager cs = this.Page.ClientScript;
                            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
                        }
                    }
                    else
                    {
                        int newSecc = secc + 1;
                        variablesGuardar.SeccionVisible = newSecc + "_" + p;
                        variablesGuardar.NewCombos = "1";
                        variablesGuardar.ErrorInsert = 0;
                        variablesGuardar.CambioCombo = 0;
                        variablesGuardar.NumBloque = secc;
                        variablesGuardar.ErrorArttip = 0;
                        string urlRefresh = HttpContext.Current.Request.Url.PathAndQuery;
                        Response.Redirect(urlRefresh.Replace(" ", "").Replace("'", ""), false);
                        btnFinalizar.Visible = true;
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                string s = "<SCRIPT language='javascript'>alert('" + t.Message.ToString() + "'); window.location.href =  window.location.href;</SCRIPT>";
                Type cstype = this.GetType();
                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterClientScriptBlock(cstype, s, s.ToString());
            }
        }
        // vacia la informacion de los controles al vector de datos 
        public int cargarValores(string prg_cve, string pasoName)
        {
            int r = 0;
            string art_cve = "";
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(pasoName) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel o = (System.Web.UI.WebControls.Panel)ob;
                                    foreach (Object _obj in o.Controls)
                                    {
                                        string nom_aux = "";
                                        if (_obj is RadComboBox)
                                        {
                                            RadComboBox rcb = (RadComboBox)_obj;
                                            nom_aux = rcb.ID.ToString();
                                            nom_aux = nom_aux.Substring(5, nom_aux.Length - 5);
                                            Session["nom" + nombreUser + rcb.ID.ToString()] = rcb.ID.ToString();
                                            Session[rcb.ID.ToString() + nombreUser] = rcb.SelectedIndex;
                                            Session["ex" + nombreUser + rcb.ID.ToString()] = rcb.Text;
                                            string texto = rcb.SelectedValue.ToString();
                                            string nomAux = nom_aux;
                                            variablesGuardar.VDatos[r, 1] = texto;
                                            variablesGuardar.VDatos[r, 0] = nomAux;
                                            r += 1;
                                            variablesGuardar.NDatos = r;
                                            string nom = rcb.ID.ToString();
                                            nom = nom.Substring(0, 2);
                                            if (nom.Equals("ex") == true)
                                            {
                                                art_cve = art_cve + texto.ToUpper();
                                            }
                                        }

                                        if (_obj is RadTextBox)
                                        {
                                            RadTextBox rtb = (RadTextBox)_obj;
                                            nom_aux = rtb.ID.ToString();
                                            nom_aux = nom_aux.Substring(5, nom_aux.Length - 5);
                                            string nom = rtb.ID.ToString();
                                            Session["nom" + nombreUser + rtb.ID.ToString()] = rtb.ID.ToString();
                                            Session[rtb.ID.ToString() + nombreUser] = rtb.Text;
                                            Session["ex" + nombreUser + rtb.ID.ToString()] = rtb.Text;
                                            nom = nom.Substring(0, 2);

                                            if (nom.Equals("ex") == true)
                                            {
                                                art_cve = art_cve + rtb.Text.TrimEnd(' ').ToUpper();
                                            }
                                            if (nom_aux.Equals("art_cve") == true)
                                            {
                                                variablesGuardar.VDatos[r, 0] = nom_aux;
                                                if (rtb.Text.Equals("N/A") == false)
                                                {
                                                    variablesGuardar.VDatos[r, 1] = rtb.Text;
                                                }
                                                
                                                else
                                                {
                                                    variablesGuardar.VDatos[r, 1] = art_cve;
                                                }
                                            }
                                            else if (nom_aux.Equals("cve_activ") == true && prg_cve.Equals("mcact") == true)
                                            {
                                                variablesGuardar.VDatos[r, 0] = nom_aux;
                                                if (rtb.Text.Equals("N/A") == false)
                                                {
                                                    variablesGuardar.VDatos[r, 1] = rtb.Text;
                                                }
                                                else
                                                {
                                                    variablesGuardar.VDatos[r, 1] = "FOLIO";
                                                }
                                            }   
                                            else if (nom_aux.Equals("cc_cve") == true && prg_cve.Contains("cccatcc"))
                                            {
                                                variablesGuardar.VDatos[r, 0] = nom_aux;
                                                if (rtb.Text.Equals("N/A") == false)
                                                {
                                                    variablesGuardar.VDatos[r, 1] = rtb.Text;
                                                }
                                                else
                                                {
                                                    variablesGuardar.VDatos[r, 1] = "FOLIO";
                                                }
                                            }
                                            else if (nom_aux.Equals("ean_cve") == true)
                                            {
                                                int arreglo = variablesGuardar.VDatos.Length / 2;
                                                for (int i = 0; i < arreglo; i++)
                                                {
                                                    if (variablesGuardar.VDatos[i, 0] != null)
                                                    {
                                                        if (variablesGuardar.VDatos[i, 0].ToString().Equals("") == false)
                                                        {
                                                            if (variablesGuardar.VDatos[i, 0].ToString().Equals("sku_cve") == true)
                                                            {
                                                                variablesGuardar.VDatos[i, 1] = art_cve;
                                                            }
                                                        }
                                                    }
                                                }
                                                variablesGuardar.VDatos[r, 0] = nom_aux;
                                                variablesGuardar.VDatos[r, 1] = rtb.Text;
                                            }
                                            else
                                            {
                                                variablesGuardar.VDatos[r, 0] = nom_aux;
                                                variablesGuardar.VDatos[r, 1] = rtb.Text;
                                            }
                                            r += 1;
                                            variablesGuardar.NDatos = r;
                                        }
                                        if (_obj is System.Web.UI.WebControls.TextBox)
                                        {
                                            System.Web.UI.WebControls.TextBox tb = (System.Web.UI.WebControls.TextBox)_obj;
                                            if (tb.ID.ToString().StartsWith("ex" + nombreUser + "ex") == false)
                                            {
                                                string nom1 = tb.ID.ToString();
                                                Session["nom" + nombreUser + tb.ID.ToString()] = tb.ID.ToString();
                                                Session[tb.ID.ToString() + nombreUser] = tb.Text;
                                                Session["ex" + nombreUser + tb.ID.ToString()] = tb.Text;
                                                nom1 = nom1.Substring(0, 2);
                                                if (nom1.Equals("ex") == true)
                                                {
                                                    art_cve = art_cve + tb.Text.ToUpper();
                                                }
                                                nom_aux = tb.ID.ToString();
                                                nom_aux = nom_aux.Substring(5, nom_aux.Length - 5);
                                                variablesGuardar.VDatos[r, 0] = nom_aux;
                                                variablesGuardar.VDatos[r, 1] = tb.Text;
                                                r += 1;
                                                variablesGuardar.NDatos = r;
                                            }
                                        }
                                        if (_obj is System.Web.UI.WebControls.CheckBox)
                                        {
                                            System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)_obj;
                                            if (cbx.ID.ToString().StartsWith("ex" + nombreUser + "ex") == false)
                                            {
                                                string nom1 = cbx.ID.ToString();
                                                Session["nom" + nombreUser + cbx.ID.ToString()] = cbx.ID.ToString();
                                                Session[cbx.ID.ToString() + nombreUser] = cbx.Text;
                                                Session["ex" + nombreUser + cbx.ID.ToString()] = cbx.Text;
                                                nom1 = nom1.Substring(0, 3);
                                                nom_aux = cbx.ID.ToString();
                                                nom_aux = nom_aux.Substring(5, nom_aux.Length - 5);
                                                int valor = 1;
                                                if (cbx.Checked == false)
                                                {
                                                    valor = 0;
                                                }
                                                else
                                                {
                                                    valor = 1;
                                                }
                                                variablesGuardar.VDatos[r, 0] = nom_aux;
                                                variablesGuardar.VDatos[r, 1] = valor.ToString();
                                                r += 1;
                                                variablesGuardar.NDatos = r;
                                            }
                                        }
                                        if (_obj is RadNumericTextBox)
                                        {
                                            RadNumericTextBox rtb = (RadNumericTextBox)_obj;
                                            nom_aux = rtb.ID.ToString();
                                            nom_aux = nom_aux.Substring(5, nom_aux.Length - 5);
                                            string nom = rtb.ID.ToString();
                                            Session["nom" + nombreUser + rtb.ID.ToString()] = rtb.ID.ToString();
                                            Session[rtb.ID.ToString() + nombreUser] = rtb.Text;
                                            Session["ex" + nombreUser + rtb.ID.ToString()] = rtb.Text;
                                            nom = nom.Substring(0, 2);
                                            variablesGuardar.VDatos[r, 0] = nom_aux;
                                            variablesGuardar.VDatos[r, 1] = rtb.Text;
                                            r += 1;
                                            variablesGuardar.NDatos = r;
                                        }
                                        if (_obj is RadDatePicker)
                                        {
                                            RadDatePicker rdp = (RadDatePicker)_obj;
                                            nom_aux = rdp.ID.ToString();
                                            nom_aux = nom_aux.Substring(5, nom_aux.Length - 5);
                                            string nom = rdp.ID.ToString();
                                            Session["nom" + nombreUser + rdp.ID.ToString()] = rdp.ID.ToString();
                                            Session[rdp.ID.ToString() + nombreUser] = rdp.SelectedDate;
                                            Session["ex" + nombreUser + rdp.ID.ToString()] = rdp.SelectedDate;
                                            nom = nom.Substring(0, 2);
                                            variablesGuardar.VDatos[r, 0] = nom_aux;
                                            variablesGuardar.VDatos[r, 1] = rdp.SelectedDate.ToString();
                                            r += 1;
                                            variablesGuardar.NDatos = r;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                r = 0;
            }
            return r;
        }
        //elimina y enumera los controles cada ves que se regresca una pantalla con una cantidad diferente de controles, se agrego porque el viewstate mantenia activos los id de los controles para reservar la informacion en la pantalla
        public void DeleteControl(int wpaso, string nombre)
        {
            string auxStep = "WizardStep" + wpaso;
            string panel = "Panel" + wpaso;
            string objeto = nombre;
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(auxStep) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel deletPanel = (System.Web.UI.WebControls.Panel)ob;
                                    string pan = "Panel" + _pant.ToString();
                                    if (deletPanel.ID.ToString().Equals(panel) == true)
                                    {//verificar porque no borra los combos
                                        int con = wpaso;
                                        foreach (Object t in deletPanel.Controls)
                                        {
                                            if (t is RadComboBox)
                                            {
                                                RadComboBox c_borrar = (RadComboBox)t;
                                                string nomX = c_borrar.ID.ToString();
                                                nomX = nomX.Substring(0, 2);
                                                if (nomX == "ex")
                                                {
                                                    c_borrar.ID = "cb_borrado" + nombre + con.ToString();
                                                    c_borrar.Visible = false;
                                                    //deletPanel.Dispose();
                                                    variablesGuardar.Ban_borra -= 1;
                                                    con += 2;
                                                }
                                            }
                                            if (t is RadTextBox)
                                            {
                                                RadTextBox t_borrar = (RadTextBox)t;
                                                string nomY = t_borrar.ID.ToString();
                                                nomY = nomY.Substring(0, 2);
                                                if (nomY == "ex")
                                                {
                                                    t_borrar.ID = "tb_borrado" + nombre + con.ToString();
                                                    t_borrar.Visible = false;
                                                    con += 2;
                                                    deletPanel.Dispose();
                                                    variablesGuardar.Ban_borra -= 1;
                                                }
                                            }

                                            if (t is System.Web.UI.WebControls.TextBox)
                                            {
                                                System.Web.UI.WebControls.TextBox tb_borrar = (System.Web.UI.WebControls.TextBox)t;
                                                string nomY = tb_borrar.ID.ToString();
                                                nomY = nomY.Substring(0, 2);
                                                if (nomY == "ex")
                                                {
                                                    tb_borrar.ID = "tb_borrado" + nombre + con.ToString();
                                                    tb_borrar.Visible = false;
                                                    con += 2;
                                                    deletPanel.Dispose();
                                                    variablesGuardar.Ban_borra -= 1;
                                                }
                                            }

                                            if (t is System.Web.UI.WebControls.Label)
                                            {
                                                System.Web.UI.WebControls.Label L_borrar = (System.Web.UI.WebControls.Label)t;
                                                string nomY = L_borrar.ID.ToString();
                                                nomY = nomY.Substring(0, 2);
                                                if ((nomY == "ex") )
                                                {
                                                    L_borrar.ID = "L_borrado" + nombre + con.ToString();
                                                    L_borrar.Visible = false;
                                                    con += 2;
                                                    deletPanel.Dispose();
                                                    variablesGuardar.Ban_borra -= 1;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }

        }
        //busca si hay controles extra y los identifica para aliminar o refrescar
        public int buscaEX(int wpaso)
        {
            string auxStep = "WizardStep" + wpaso;
            string panel = "Panel" + wpaso;
            int x = 0;
            try
            {
                foreach (Object obj in RadWizard1.Controls)
                {
                    if (obj is RadWizardStep)
                    {
                        RadWizardStep temp = (RadWizardStep)obj;
                        if (temp.ID.ToString().Equals(auxStep) == true)
                        {
                            foreach (Object ob in temp.Controls)
                            {
                                if (ob is System.Web.UI.WebControls.Panel)
                                {
                                    System.Web.UI.WebControls.Panel deletPanel = (System.Web.UI.WebControls.Panel)ob;
                                    string pan = "Panel" + _pant.ToString();
                                    if (deletPanel.ID.ToString().Equals(panel) == true)
                                    {
                                        foreach (Object t in deletPanel.Controls)
                                        {
                                            if (t is RadComboBox)
                                            {
                                                RadComboBox c_borrar = (RadComboBox)t;
                                                string nomX = c_borrar.ID.ToString();
                                                nomX = nomX.Substring(0, 2);
                                                if (nomX == "ex")
                                                {
                                                    x += 1;
                                                    variablesGuardar.Ban_borra = variablesGuardar.Ban_borra + 1;
                                                }
                                            }
                                            if (t is RadTextBox)
                                            {
                                                RadTextBox t_borrar = (RadTextBox)t;
                                                string nomY = t_borrar.ID.ToString();
                                                nomY = nomY.Substring(0, 2);
                                                if (nomY == "ex")
                                                {
                                                    x += 1;
                                                    variablesGuardar.Ban_borra = variablesGuardar.Ban_borra + 1;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                Response.Write("<script type=\"text/javascript\">alert('Ups!!! Algo salio mal al intentar borrar el compenente agregado por el Art. Tipo ( " + t.Message.ToString() + " )');</script>");
                variablesGuardar.ArticuloClave = null;
            }
            return x;
        }
        protected void RadWizard1_PreviousButtonClick(object sender, WizardEventArgs e)
        {
        }
        protected void RadWizard1_NextButtonClick1(object sender, WizardEventArgs e)
        {
        }
        protected void RadWizard1_FinishButtonClick(object sender, WizardEventArgs e)
        {
            finalizarProg();
        }
        protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RadWizard1.ActiveStepIndex += 1;

                if (RadWizard1.ActiveStepIndex >= 1)
                {
                    btnAtras.Visible = true;
                }
                else
                {
                    btnAtras.Visible = false;
                }
            }
            catch (Exception t)
            {
                finalizarProg();
            }
        }
        protected void btnAtras_Click1(object sender, ImageClickEventArgs e)
        {
            RadWizard1.ActiveStepIndex -= 1;
            if (RadWizard1.ActiveStepIndex == 0)
            {
                this.btnAtras.Visible = false;
            }
            else
            {
                this.btnAtras.Visible = true;
            }
        }
        public void finalizarProg()
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.RemoveAll();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                _flujo = "";
                _user = "";
                _artTipo = "";
                _artNom = "";
                variablesGuardar.SeccionVisible = "0_1";
                variablesGuardar.Art_tip = _artTipo;
                variablesGuardar.Art_nom = _artNom;
                variablesGuardar.Art_cve = null;
                variablesGuardar.Art_cveFin = null;
                variablesGuardar.Sku_cveFin = null;
                variablesGuardar.ArtCve_aux = null;
                variablesGuardar.Aux_art_nom = null;
                variablesGuardar.Aux_art_tip = null;
                variablesGuardar.Balin = null;
                variablesGuardar.Ban_borra = 0;
                variablesGuardar.ComboActual = 0;
                variablesGuardar.NDatos = 0;
                variablesGuardar.Prg_cve = null;
                variablesGuardar.Tip_cc = null;
                variablesGuardar.User = null;
                variablesGuardar.ErrorArttip = 0;
                variablesGuardar.NumBloque = 0;
                variablesGuardar.CambioCombo = 0;
                variablesGuardar.ErrorInsert = 0;
                variablesGuardar.NewCombos = null;
                variablesGuardar.NumSecciones = 0;
                _con.borrar();
                for (int w = 0; w < 100; w++)
                {
                    variablesGuardar.VDatos[w, 0] = "";
                    variablesGuardar.VDatos[w, 1] = "";
                }
                _con.finalizaFlujo();
                Response.Redirect(urlInicial.TrimStart(' ').TrimEnd(' ').Replace(" ", ""), false);
            }
            catch (Exception t)
            {
                t.Message.ToString();
                RadTickerItem x = new RadTickerItem(t.Message.ToString());
                RadTicker1.Items.Add(x);
            }
        }
        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            finalizarProg();
        }
    }
}