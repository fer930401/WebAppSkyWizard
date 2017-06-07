using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using System.Web;

namespace LogicaNegocio
{
    public class Logica
    {

        Configuracion _con = new Configuracion();
        _conecta bdd = new _conecta();

        /* variables estaticas */
        static int valor;
        public static int Valor
        {
            get { return Logica.valor; }
            set { Logica.valor = value; }
        }

        static string artTipo;
        public static string ArtTipo
        {
            get { return Logica.artTipo; }
            set { Logica.artTipo = value; }
        }

        static string _art_cve;
        public static string Art_cve
        {
            get { return Logica._art_cve; }
            set { Logica._art_cve = value; }
        }
        static string aux_parm;
        public static string Aux_parm
        {
            get { return Logica.aux_parm; }
            set { Logica.aux_parm = value; }
        }
        static string art_tip_select;
        public static string Art_tip_select
        {
            get { return Logica.art_tip_select; }
            set { Logica.art_tip_select = value; }
        }
        static string artcveFin;
        public static string ArtcveFin
        {
            get { return Logica.artcveFin; }
            set { Logica.artcveFin = value; }
        }


        /* funciones para la pantalla Default */
        public DataTable defaul(string paso_cve)
        {
            DataTable dtpasos = new DataTable();
            dtpasos = _con.defaul(paso_cve);
            return dtpasos;
        }

        public DataTable StepUser(string flujo, string user)
        {
            DataTable dtStep = new DataTable();
            dtStep = _con.StepUser(flujo, user);
            return dtStep;
        }

        /* fin de funciones para la pantalla de Default */

        /* Para la pantalla de Autobusqueda */
        DataTable tab = new DataTable();
        public DataTable llenaCombo(string prg_cve)
        {
            DataTable dt = new DataTable();
            dt = bdd.llenaCombo(prg_cve);
            return dt;
        }

        public DataTable llenaSubCombo(string art_tip, string ef_cve)
        {
            DataTable dt = new DataTable();
            dt = bdd.llenaSubCombo(art_tip,ef_cve);
            return dt;
        }

        public DataTable llenaTabla(string art_tip, string art_cve, string nom_comer, int opc)
        {
            DataTable dt = new DataTable();
            dt = bdd.llenaTabla(art_tip, art_cve, nom_comer, opc);
            return dt;

        }
        /* fin de metodos para la pantalla de Autobusqueda */


        /* funciones  para la pantalla de AddArt */
        public int no_pasos(string paso_cve)
        {
            int r = 0;
            r = _con.no_pasos(paso_cve);
            return r;
        }
        public DataTable parametroInicial(string orden, string flujo)
        {
            DataTable dt = new DataTable();
            dt = _con.parametroInicial(orden, flujo);
            return dt;
        }
        public string Art_TipPant(string _flujo, string prg_cvePant)
        {
            string clave = _con.art_tipPant(_flujo, prg_cvePant);
            return clave;
        }
        public string tituloStep(string prg_cve)
        {
            string r = "";
            r = _con.tituloStep(prg_cve);
            return r;
        }
        public DataTable xcdic(string prg_cve, string user, string tab, string ef_cve)
        {
            DataTable r = new DataTable();
            r = _con.xcdic(prg_cve, user, tab, ef_cve);
            return r;
        }
        public DataTable xuarrays(string prg_cve)
        {
            DataTable T_xuarrays = new DataTable();
            T_xuarrays = _con.xuarrays(prg_cve);
            return T_xuarrays;
        }
        public int CuentaXcdicB(string tipo_cc, string tab, string secciones)
        {
            int r = 0;
            r = _con.CuentaXcdicB(tipo_cc, tab, secciones);
            return r;
        }
        public DataTable TablaXuarraysBotones(string prg_cve)
        {
            DataTable T_xuarrays2 = new DataTable();
            T_xuarrays2 = _con.TablaXuarraysBotones(prg_cve);
            return T_xuarrays2;
        }
        public DataTable tablaTotal(string prg_cve, string seccion, string tab)
        {
            DataTable r = new DataTable();
            r = _con.totalTablas(prg_cve, seccion, tab);
            return r;
        }
        public DataTable qtiparta(string tipoCC, string tabla)
        {
            DataTable dt = new DataTable();
            dt = _con.qtiparta(tipoCC, tabla);
            return dt;
        }
        public DataTable temp_xuarrays(string prg, int ban, int pant)
        {
            DataTable tempGLqry = _con.temp_GLquery(prg, pant);//Obtiene el GL_qrycombo
            return tempGLqry;
        }
        public string nombreLabel(string campo, string prg_cve, string tipo_cc, int secc)
        {
            string r = "";
            r = _con.nombreLabel(campo, prg_cve, tipo_cc, secc);
            return r;
        }
        public int _validacion(string campo, string prg_cve, string tipo_cc, int secc)
        {
            int r = 0;
            r = _con._validacion(campo, prg_cve, tipo_cc, secc);
            return r;
        }
        public void cargaFlujo(string flujo)
        {
            _con.cargaFlujo(flujo);
        }
        public string guardarBloque(string prg_cve, int secc)
        {
            string art_cveFin = "";
            try
            {
                Configuracion.AuxDatos = variablesGuardar.VDatos;
                Configuracion.TamDatos = variablesGuardar.NDatos;
                Configuracion.Ef_cve = variablesGuardar.Ef_cve;
                Configuracion.CUser = variablesGuardar.User;
                int camposArreglo = Configuracion.AuxDatos.Length / 2;
                for (int i = 0; i < camposArreglo; i++)
                {
                    if (Configuracion.AuxDatos[i, 0] != null)
                    {
                        if (Configuracion.AuxDatos[i, 0].ToString().Equals("art_cve") == true)
                        {
                            Configuracion.ArtCve_aux = Configuracion.AuxDatos[i, 1].ToString();
                        }
                        else if (Configuracion.AuxDatos[i, 0].ToString().Equals("rfc") == true)
                        {
                            variablesGuardar.Rfc = Configuracion.AuxDatos[i, 1].ToString();
                            Configuracion.Rfc = Configuracion.AuxDatos[i, 1].ToString();
                        }
                        else if (Configuracion.AuxDatos[i, 0].ToString().Equals("nom1") == true)
                        {
                            Configuracion.Nom_Art = Configuracion.AuxDatos[i, 1].ToString();
                            variablesGuardar.Nom_Art = Configuracion.AuxDatos[i, 1].ToString();
                        }
                        else if (Configuracion.AuxDatos[i, 0].ToString().Equals("nombre") == true && prg_cve.Contains("cccat") == false)
                        {
                            Configuracion.Nom_Art = Configuracion.AuxDatos[i, 1].ToString();
                            variablesGuardar.Nom_Art = Configuracion.AuxDatos[i, 1].ToString();
                        }
                        else if (Configuracion.AuxDatos[i, 0].ToString().Equals("art_tip") == true)
                        {
                            Configuracion.Art_tip = Configuracion.AuxDatos[i, 1].ToString();
                            variablesGuardar.Art_tipFinal = Configuracion.AuxDatos[i, 1].ToString();
                            if (secc == 2)
                            {
                                for (int y = 0; y < camposArreglo; y++)
                                {
                                    if (Configuracion.AuxDatos[y, 0].ToString().Equals("sku_cve") == true)
                                    {
                                        variablesGuardar.Sku_cveFin = Configuracion.AuxDatos[y, 1].ToString();
                                    }
                                }                                
                            }
                        }
                    }
                } 
                /* Xuseldyn crea la cadena del insert */
                art_cveFin = _con.guardaBloque(prg_cve, secc);
                //art_cveFin = "ARTCVE";
            }
            catch (Exception e)
            {
                art_cveFin = e.Message.ToString() + "logica";
            }
            for (int h = 0; h < 100; h++)
            {
                variablesGuardar.VDatos[h, 0] = "";
                variablesGuardar.VDatos[h, 1] = "";
            }
            return art_cveFin;
        }
        //vacia los arreglos 
        public void borrar()
        {
            _con.borrar();
        }
        public void finalizaFlujo()
        {
            _con.finalizaFlujo();
        }
        public string ultimoArt_Tip(string _user, string _flujo)
        {
            string clave = _con.ultimoArt_Tip(_user, _flujo);
            return clave;
        }
        public string insertClasifica(string[,] vDatos, string tab, int filas)
        {
            string r = "";
            r = _con.insertClasifica(vDatos, tab, filas);
            return r;
        }
        public string buscaCls(string art_cve, string user, string artTip, int pant)
        {
            Configuracion.Prg_cve_act = variablesGuardar.Prg_cves[pant,0];
            string _cls = _con.cls_busca(art_cve, user, artTip);
            return _cls;
        }
        public string obtieneCls(string prg_cve)
        {
            string _cls = _con.obtieneCls(prg_cve);
            return _cls;
        }
        public int insert(string ssql)
        {
            int r = 0;
            r = _con.Insert(ssql);
            return r;
        }
        public DataTable _CargaCombo(DataTable _Xuarrays, int pos, DataTable _Xcdic, string prg_cve, int pant, string art_tipSession, string prg_cve2, string[] pasos, int z, string[,] qry, int secc)
        {
            DataTable _dt = new DataTable();
            string cad = "";
            string pa = "";
            string consul = "";
            string[,] xua = new string[100, 5];
            int con = 0;
            string campo = _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
            string tipo_cc = _Xcdic.Rows[pos][13].ToString().TrimEnd(' ');
            string sys_var = _Xcdic.Rows[pos][12].ToString().TrimEnd(' ');
            string balin4 = _Xcdic.Rows[pos][21].ToString().TrimEnd(' ');
            string balin5 = _Xcdic.Rows[pos][22].ToString().TrimEnd(' ');
            string balin6 = _Xcdic.Rows[pos][23].ToString().TrimEnd(' ');
            string balin7 = _Xcdic.Rows[pos][24].ToString().TrimEnd(' ');
            int orden = Convert.ToInt32(_Xcdic.Rows[pos][5].ToString().TrimEnd(' '));
            string tipo_ccc = _Xcdic.Rows[pos][16].ToString().TrimEnd(' ');
            int lon_xuarrays = _Xuarrays.Rows.Count;
            string array_nom;
            string col = "";
            for (int i = 0; i < lon_xuarrays; i++)
            {
                array_nom = _Xuarrays.Rows[i][1].ToString().TrimEnd(' ');
                col = _Xuarrays.Rows[i][6].ToString().TrimEnd(' ');
                if ((array_nom == "GL_qrycom") && (col == "0"))
                {
                    xua[con, 0] = _Xuarrays.Rows[i][2].ToString();//orden
                    xua[con, 1] = _Xuarrays.Rows[i][3].ToString();//stored combo
                    //xua[con, 3] = _xuarrays.Rows[i][12].ToString();//sysvar
                    con += 1;
                }
            }
            for (int i = 0; i < (con); i++)
            {
                string or = xua[i, 0].ToString();
                xua[i, 2] = obtieneCampo(or, _Xcdic);//orden 
                xua[i, 3] = obtieneSysVar(or, _Xcdic);
                xua[i, 4] = obtSysVarXuarrays(xua[i, 3].ToString(), _Xuarrays);

            }
            switch (campo.TrimEnd(' '))
            {
                case "art_tip":
                    consul = "qcomartic11";//qcomartic11 'icartsc'
                    if (pant != 2)
                    {
                        if (prg_cve == "ieexdalm")
                        {
                            cad = "mtot";
                        }
                        else
                        {
                            cad = "'"+prg_cve+"'";
                        }
                    }
                    if (pant == 2)
                    {
                        consul = "qcomartip91";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cc_cve":
                    if (pant == 2)
                    {//exec dbo.qcomsubp21 '012', 'GAS0014' 
                        consul = "qcomsubp21";
                        cad = "'" + variablesGuardar.Art_tipBDD + "','" + variablesGuardar.ArticuloClave + "'";
                    }
                    else if (variablesGuardar.Prg_cves[pant, 0] == "icartmta")
                    {
                        consul = "qcomcc1";
                        cad = "'P'";
                    }
                    else if (variablesGuardar.Prg_cves[pant, 0] == "ccinfad")
                    {
                        consul = "qcomcc1";
                        cad = "'K'";
                    }
                    else
                    {
                        consul = "qcomsku51";
                        cad = "'" + art_tipSession + "'";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "forent_cve":
                    consul = "qcomcve61";
                    if (balin5 != "")
                    {
                        pa = "'" + balin5 + "'";
                        cad = _con.parametroBase(prg_cve, pa);
                        cad = "'" + cad + "'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "cmp_tip":
                    consul = "qcomartip71";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cmp_cve":
                    consul = "qcomart111";
                    cad = "M03";  //"M03";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "origen_cve": //qcompais1 ''
                    consul = "qcompais1";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "frac_aran": /* se agrego la instruccion rtrim a este sp */
                    consul = "qcomfra1";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "uni_uso"://qcomumed81 '012', '0'  se agrego la instruccion rtrim a este sp 
                    consul = "qcomumed81";
                    if (art_tipSession.Equals("h") == true || art_tipSession.Equals("H") == true)
                    {
                        art_tipSession = "C";
                    }
                    cad = "'" + art_tipSession + "','" + sys_var + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "uni_alt1"://qcomumed81 '012', '0'
                    consul = "qcomumed81";
                    if (art_tipSession.Equals("h") == true || art_tipSession.Equals("H") == true)
                    {
                        art_tipSession = "C";
                        sys_var = "1";
                    }
                    cad = "'" + art_tipSession + "','" + sys_var + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "uni_alt2":
                    consul = "qcomumed81";
                    if (art_tipSession.Equals("h") == true || art_tipSession.Equals("H") == true)
                    {
                        art_tipSession = "C";
                        sys_var = "1";
                    }
                    cad = "'" + art_tipSession + "','" + sys_var + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cls_num"://qcomcls1 '012'
                    consul = "qcomcls1";
                    if (variablesGuardar.Prg_cves[pant, 0] == "icartst")
                    {
                        variablesGuardar.Prm1 = "T";
                    }
                    else if (variablesGuardar.Prg_cves[pant, 0] == "icartsh" || variablesGuardar.Prg_cves[pant, 0] == "icartsb" || variablesGuardar.Prg_cves[pant, 0] == "icartsu")
                    {
                        variablesGuardar.Prm1 = "A";
                    }
                    else if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        variablesGuardar.Prm1 = "C";
                    }
                    /*if (art_tipSession.Equals("h") == true || art_tipSession.Equals("H") == true)
                    {
                        art_tipSession = "A";
                    }*/
                    cad = "'" + variablesGuardar.Prm1 + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "subcls_cve": //qcomcld1 '012', '2'este campo se obtiene de qcomcls1 
                    consul = "qcomcld1";
                    cad = "'" + variablesGuardar.Prm1 + "','" + variablesGuardar.Dedicado + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "serv_cve":
                    consul = "qcomser11";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "tm_lim_cre":
                    consul = "qcomtm1a";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "sku_cve":
                    consul = "qcomartcve11";//qcomartcve11 '012'---> exec qcomartcve13  '012', 'Gasolina @ GAS0006'
                    if (pant != 2)
                    {
                        cad = "'" + art_tipSession + "'";
                        if (pant == 3)//exec qcomsubp1 '012' ,'ACE0123'
                        {
                            consul = "qcomsubp1";
                            cad = "'" + art_tipSession + "',''";
                        }
                        else
                        {
                            if (prg_cve == "ieexdalm")
                            {
                                consul = "qcomarts1";
                                cad = "'" + art_tipSession + "'";
                            }
                            else
                            {
                                consul = "qcomartcve13";
                                cad = "'" + art_tipSession + "',''";
                            }
                        }
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    if (pant == 2)
                    {
                        consul = "qcomsku2";
                        cad = "'" + variablesGuardar.Art_tipBDD + "',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "prm7":
                    consul = "qcomdiv1";/* se agrego la instruccion rtrim a este sp */
                    cad = "'" + variablesGuardar.Ef_cve + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "sw_ant":
                    consul = "qcomstsub1";// qcomstsub1 'lcartsub', '012', 'GAS0006', 'V'
                    DataTable dt = _con._tipoAnteriorBDD(variablesGuardar.User);
                    string tipo = dt.Rows[0][0].ToString();
                    string cve = dt.Rows[0][1].ToString();
                    cad = "'" + variablesGuardar.Prg_cves[pant, 0] + "','" + tipo + "','" + variablesGuardar.Art_cveFin + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "sw_act":  //exec dbo.qcomstsub3  'lcartsub', '012', 'GAS0006', 'V', 'Inactivo' --> //exec dbo.qcomstmat1 'lcartsub', '1  ' ---> //qcomstmat1 'lcartsub', '1  '
                    /*if (pant != 2)
                    {
                        consul = "qcomstsub3";
                        cad = prg_cve + ",'" + art_tipSession + "','" + pasos[z - 2].ToString() + "','V',''" + pasos[z - 1].ToString();
                        _dt = _con.ExecQryTabla(consul, cad);

                        consul = "qcomstmat1";
                        cad = prg_cve + ",'" + _dt + "'";
                        _dt = _con.ExecQryTabla(consul, cad);

                    }
                    if (pant == 2)
                    {
                        consul = "qcomstsub3";
                        cad = prg_cve2 + ",'" + art_tipSession + "','" + pasos[z - 2].ToString() + "','V',''" + "'" + pasos[z - 1].ToString() + "'";
                        _dt = _con.ExecQryTabla(consul, cad);

                        consul = "qcomstmat1";
                        cad = prg_cve2 + ",'" + _dt + "'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }*/
                    consul = "qcomstmat1";
                    cad = "'" + variablesGuardar.Prg_cves[pant, 0] + "', '0'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "prm1":
                    if (variablesGuardar.Prg_cves[pant, 0] == "icartmta")
                    {
                        consul = "qcomsku81";
                        cad = "'" + Configuracion.Art_tip + "'";
                    }
                    else
                    {
                        consul = "qcomcve1";
                        cad = "'" + tipo_ccc + "'";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "art_cve":
                    consul = "qcomart13";
                    cad = "'" + art_tipSession + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "per_hor":
                    consul = "qcomprf11";
                    cad = "C";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "prm4":
                    consul = "qcomtm1a";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;

                case "status":
                    if (variablesGuardar.Prg_cves[pant, 0] == "icartss" || variablesGuardar.Prg_cves[pant, 0] == "icartst")
                    {
                        consul = "qcomstmat11";
                        cad = "'" + tipo_cc + "', 'FOLIO'";
                    }
                    else if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomswc1";
                        cad = "'C'";
                    }
                    else
                    {
                        consul = "qcomswc1";
                        cad = "'" + tipo_cc + "'";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "edo_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomedon1";
                    }
                    else
                    {
                        consul = "qcomcd1";
                    }
                    cad = "''";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "pais_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcompaisn1";
                    }
                    else
                    {
                        consul = "qcompais1";
                    }
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cd_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcdn1";
                        cad = "'',''";
                    }
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatcck" || variablesGuardar.Prg_cves[pant, 0] == "cccatccm" || variablesGuardar.Prg_cves[pant, 0] == "cccatcce" || variablesGuardar.Prg_cves[pant, 0] == "cccatccp")
                    {
                        consul = "qcomcd1";

                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "mpo_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcommpion1";
                        cad = "'','',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "cp":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcpn1";
                        cad = "'','','',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "art_tip_dest1":
                    consul = "qcomrpt1";
                    cad = "'40'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "art_tip_fte":
                    consul = "qcomartip11";
                    cad = "'" + variablesGuardar.Prg_cves[pant, 0] + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "puesto3":
                    consul = "qcomcve1";
                    cad = "'076'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "atencion3":
                    consul = "qcomcve1";
                    cad = "'077'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "if_cve":
                    consul = "qcomif1";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "rev_dias":
                    consul = "qcomcve1";
                    cad = "'082'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "comp1_cve":
                    consul = "qcomcve1";
                    cad = "'018'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "comp2_cve":
                    consul = "qcomcve1";
                    cad = "'018'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "comp3_cve":
                    consul = "qcomcve1";
                    cad = "'018'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "edo_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomedon1";
                        cad = "''";
                    }
                    else
                    {
                        consul = "qcomcd1";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "pais_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcompaisn1";
                        cad = "";
                    }
                    else
                    {
                        consul = "qcompais1";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cd_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcdn1";
                        cad = "'',''";
                    }
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccp")
                    {
                        consul = "qcomcd1";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "munic_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcommpion1";
                        cad = "'','',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "cp_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcpn1";
                        cad = "'','','',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "suc_aten3":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcve1";
                        cad = "'092'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "polvta_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcompol1";
                        cad = "'001'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "foremb_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcc1";
                        cad = "'E'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccp")
                    {
                        consul = "qcomcc1";
                        cad = "'E'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "ven_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcc1";
                        cad = "'V'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccp")
                    {
                        consul = "qcomcc1";
                        cad = "'M'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "dia_entrega":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomdia1";
                        cad = "";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "linea44":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomif1";
                        cad = "";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "forpag_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomfp21";
                        cad = "";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "linea42":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomef1";
                        cad = "";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "linea43":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomdivgrp21";
                        cad = "''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "ieps_cve":
                    consul = "qcomiep1";

                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
            }
            return _dt;
        }
        public string tipo_CC(string orden, string prg_cve)
        {
            string tipo_CC = "";
            tipo_CC = _con.tipoCC(orden, prg_cve);
            return tipo_CC;
        }
        public string obtieneCampo(string orden, DataTable _Xcdic)
        {
            string camp = "";
            int total = _Xcdic.Rows.Count;
            for (int j = 0; j < total; j++)
            {
                string ord = _Xcdic.Rows[j][5].ToString();
                if (ord == orden)
                {
                    camp = _Xcdic.Rows[j][1].ToString();
                    j = total;
                }
            }
            return camp;
        }
        public string obtieneSysVar(string orden, DataTable _Xcdic)
        {
            string camp = "";
            int total = _Xcdic.Rows.Count;
            for (int j = 0; j < total; j++)
            {
                string ord = _Xcdic.Rows[j][5].ToString();
                if (ord == orden)
                {
                    camp = _Xcdic.Rows[j][12].ToString();
                    j = total;
                }
            }
            return camp;
        }
        public string obtSysVarXuarrays(string orden, DataTable Xuarrays)
        {
            string camp = "";
            int total = Xuarrays.Rows.Count;
            for (int j = 0; j < total; j++)
            {
                string ord = Xuarrays.Rows[j][5].ToString();
                if (ord == orden)
                {
                    camp = Xuarrays.Rows[j][3].ToString();
                    j = total;
                }
            }
            return camp;
        }
        public string obtiene_cve()
        {
            string art_cve = Art_cve;
            return art_cve;
        }
        public string aux_ExecQryTabla(string qry, string parametros)
        {
            string parm = "";
            try
            {
                if (parametros.Contains(',') == true)
                {
                    int nPrm = _con.nParametros(qry);
                    int c = 0;
                    string[] prm = new string[nPrm];//verificar porque se inicia en este valor
                    for (int i = 0; i < parametros.Length; i++)
                    {
                        if ((parametros[i].Equals(',') == true) && (parametros[i + 1].ToString() != " "))
                        {
                            c += 1;
                        }
                        else
                        {
                            prm[c] = prm[c] + parametros[i].ToString();
                        }
                    }
                    c += 1;
                    for (int x = 0; x < nPrm; x++)
                    {
                        if (string.IsNullOrEmpty(prm[x]) == true)
                        {
                            prm[x] = "1";
                        }
                    }
                    string art_cve = obtiene_cve();
                    if (c == 1)
                    {
                        parm = _con._auxExecQry2(qry, prm[0], prm[1]);
                    }
                    if (c == 2)
                    {
                        parm = _con._auxExecQry3(qry, prm[0], prm[1], prm[2]);
                    }
                    if (c == 3)
                    {
                        parm = _con._auxExecQry4(qry, prm[1], art_cve, prm[2], prm[3]);

                        Aux_parm = prm[0] + "," + prm[1] + "," + art_cve + ",'" + parm + "'";
                    }
                    if (c == 4)
                    {
                        parm = _con._auxExecQry5(qry, prm[0], prm[1], prm[2], prm[3], prm[4]);
                    }
                    if (c == 5)
                    {
                        parm = _con._auxExecQry6(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5]);
                    }
                    if (c == 6)
                    {
                        parm = _con._auxExecQry7(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5], prm[6]);
                    }
                    if (c == 7)
                    {
                        parm = _con._auxExecQry8(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5], prm[6], prm[7]);
                    }
                    if (c == 8)
                    {
                        parm = _con._auxExecQry9(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5], prm[6], prm[7], prm[8]);
                    }
                    if (c == 9)
                    {
                        parm = _con._auxExecQry10(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5], prm[6], prm[7], prm[8], prm[9]);
                    }
                }
                else
                {
                    parm = _con._auxExecQry(qry, parametros);
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return parm;
        }
        public DataTable ExecQryTabla(string qry, string parametros)
        {
            DataTable dt = new DataTable();
            try
            {
                if (qry.ToString().Equals("qcomstsub1"))
                {
                    string qry_aux = qry;
                    qry = "qcomsubp3";

                    string val = aux_ExecQryTabla(qry, parametros);
                    qry = qry_aux;
                    parametros = Logica.Aux_parm;

                }
                if (parametros.Contains(',') == true)
                {
                    int nPrm = _con.nParametros(qry);
                    int c = 0;
                    string[] prm = new string[nPrm];//verificar porque se inicia en este valor
                    for (int i = 0; i < parametros.Length; i++)
                    {

                        if ((parametros[i].Equals(',') == true) && (parametros[i + 1].ToString() != " "))
                        {
                            c += 1;
                            //i += 1;
                        }
                        else
                        {
                            prm[c] = prm[c] + parametros[i].ToString();
                        }
                    }
                    if (c == 1)
                    {
                        dt = _con.ExecQryTabla2(qry, prm[0], prm[1]);
                    }
                    if (c == 2)
                    {
                        dt = _con.ExecQryTabla3(qry, prm[0], prm[1], prm[2]);
                    }
                    if (c == 3)
                    {
                        dt = _con.ExecQryTabla4(qry, prm[0], prm[1], prm[2], prm[3]);
                    }
                    if (c == 4)
                    {
                        dt = _con.ExecQryTabla5(qry, prm[0], prm[1], prm[2], prm[3], prm[4]);
                    }
                    if (c == 5)
                    {
                        dt = _con.ExecQryTabla6(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5]);
                    }
                    if (c == 6)
                    {
                        dt = _con.ExecQryTabla7(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5], prm[6]);
                    }
                    if (c == 7)
                    {
                        dt = _con.ExecQryTabla8(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5], prm[6], prm[7]);
                    }
                    if (c == 8)
                    {
                        dt = _con.ExecQryTabla9(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5], prm[6], prm[7], prm[8]);
                    }
                    if (c == 9)
                    {
                        dt = _con.ExecQryTabla10(qry, prm[0], prm[1], prm[2], prm[3], prm[4], prm[5], prm[6], prm[7], prm[8], prm[9]);
                    }
                }
                else
                {
                    dt = _con.ExecQryTabla(qry, parametros);
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return dt;
        }
        

        /* fin de los metodos para la pantalla AddArt */

        /* metodos para la pantalla Acciones */
        public string actualizaBloque(string prg_cve, int secc, int accion)
        {
            string art_cveFin = "";
            int z = 0;
            try
            {
                Configuracion.AuxDatos = variablesGuardar.VDatos;
                Configuracion.TamDatos = variablesGuardar.NDatos;
                Configuracion.Ef_cve = variablesGuardar.Ef_cve;
                Configuracion.CUser = variablesGuardar.User;
                int camposArreglo = Configuracion.AuxDatos.Length / 2;
                for (int i = 0; i < camposArreglo; i++)
                {
                    if (Configuracion.AuxDatos[i, 0] != null)
                    {
                        if (Configuracion.AuxDatos[i, 0].ToString().Equals("art_cve") == true)
                        {
                            Configuracion.ArtCve_aux = Configuracion.AuxDatos[i, 1].ToString();
                        }
                        else if (Configuracion.AuxDatos[i, 0].ToString().Equals("art_tip") == true)
                        {
                            Configuracion.Art_tip = Configuracion.AuxDatos[i, 1].ToString();
                        }
                        else if (Configuracion.AuxDatos[i, 0].ToString().Equals("cc_cve") == true && prg_cve.Equals("icartmta") == false)
                        {
                            Configuracion.ArtCve_aux = Configuracion.AuxDatos[1, 1].ToString();
                        }
                    }
                }
                /* Xuseldyn crea la cadena del insert */
                art_cveFin = _con.actualizaBloque(prg_cve, secc, accion);
                //art_cveFin = "ARTCVE";
            }
            catch (Exception e)
            {
                art_cveFin = e.Message.ToString() + "logica";
            }
            return art_cveFin;
        }
        public string consultaDatosArt(string artCve, string artTip, string campo, string tabla)
        {
            string datos = "";
            datos = _con.TablaDatosArt(artCve, artTip, campo, tabla);
            variablesGuardar.Art_cveFin = Configuracion.Art_cve;
            variablesGuardar.UserBD = Configuracion.Id_ultactDB;
            
            return datos.TrimEnd(' ');
        }
        public DataTable _CargaComboUp(DataTable _xuarrays, int pos, DataTable _Xcdic, string prg_cve, int pant, string art_tipSession, string prg_cve2, string[] pasos, int z, string[,] qry, string dato)
        {
            DataTable _dt = new DataTable();
            string cad = "";
            string consul = "";
            string[,] xua = new string[100, 5];
            int con = 0;
            string campo = _Xcdic.Rows[pos][1].ToString().TrimEnd(' ');
            string tipo_cc = _Xcdic.Rows[pos][13].ToString().TrimEnd(' ');
            string sys_var = _Xcdic.Rows[pos][12].ToString().TrimEnd(' ');
            string balin4 = _Xcdic.Rows[pos][21].ToString().TrimEnd(' ');
            string balin5 = _Xcdic.Rows[pos][22].ToString().TrimEnd(' ');
            string balin6 = _Xcdic.Rows[pos][23].ToString().TrimEnd(' ');
            string balin7 = _Xcdic.Rows[pos][24].ToString().TrimEnd(' ');
            int orden = Convert.ToInt32(_Xcdic.Rows[pos][5].ToString().TrimEnd(' '));
            string tipo_ccc = _Xcdic.Rows[pos][16].ToString().TrimEnd(' ');
            int lon_xuarrays = _xuarrays.Rows.Count;
            int lon_xcdic = _Xcdic.Rows.Count;
            string array_nom;
            string col = "";
            for (int i = 0; i < lon_xuarrays; i++)
            {
                array_nom = _xuarrays.Rows[i][1].ToString().TrimEnd(' ');
                col = _xuarrays.Rows[i][6].ToString().TrimEnd(' ');
                if ((array_nom == "GL_qrycom") && (col == "0"))
                {
                    xua[con, 0] = _xuarrays.Rows[i][2].ToString();//orden
                    xua[con, 1] = _xuarrays.Rows[i][3].ToString();//stored combo
                    //xua[con, 3] = _xuarrays.Rows[i][12].ToString();//sysvar
                    con += 1;
                }
            }
            for (int i = 0; i < (con); i++)
            {
                string or = xua[i, 0].ToString();
                xua[i, 2] = obtieneCampo(or, _Xcdic);//orden 
                xua[i, 3] = obtieneSysVar(or, _Xcdic);
                xua[i, 4] = obtSysVarXuarrays(xua[i, 3].ToString(), _xuarrays);

            }
            switch (campo.TrimEnd(' '))
            {
                case "art_tip": /* se agrego la instruccion rtrim a este sp */
                    consul = "qcomartic11";//qcomartic11 'icartsc'
                    if (prg_cve == "ieexdalm")
                    {
                        cad = "mtot";
                    }
                    else
                    {
                        cad = "'"+prg_cve+"'";                        
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cc_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "icartmta")
                    {
                        consul = "qcomcc1";
                        cad = "'P'";
                    }
                    else if (variablesGuardar.Prg_cves[pant, 0] == "ccinfad")
                    {
                        consul = "qcomcc1";
                        cad = "'K'";
                    }
                    else
                    {
                        consul = "qcomsku51";
                        cad = "'" + art_tipSession + "'";                        
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "forent_cve":/* se agrego la instruccion rtrim a este sp */
                    consul = "qcomcve62";
                    if (balin5 != "")
                    {
                        cad = "'" + tipo_ccc + "','" + dato + "'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;

                case "cmp_tip":
                    consul = "qcomartip71";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cmp_cve":
                    consul = "qcomart111";
                    cad = variablesGuardar.Prm1;  //"M03";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "origen_cve": //qcompais1 ''
                    consul = "qcompais1";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "frac_aran": /* se agrego la instruccion rtrim a este sp */
                    consul = "qcomfra1";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "uni_uso"://qcomumed81 '012', '0' /* se agrego la instruccion rtrim a este sp */
                    consul = "qcomumed81";                    
                    if (art_tipSession.Equals("h") == true || art_tipSession.Equals("H") == true)
                    {
                        art_tipSession = "C";
                    }
                    cad = "'" + art_tipSession + "','" + sys_var + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "uni_alt1"://qcomumed81 '012', '0'
                    consul = "qcomumed81";
                    if (art_tipSession.Equals("h") == true || art_tipSession.Equals("H") == true)
                    {
                        art_tipSession = "C";
                        sys_var = "1";
                    }
                    cad = "'" + art_tipSession + "','" + sys_var + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "uni_alt2":
                    consul = "qcomumed81";
                    if (art_tipSession.Equals("h") == true || art_tipSession.Equals("H") == true)
                    {
                        art_tipSession = "C";
                        sys_var = "1";
                    }
                    cad = "'" + art_tipSession + "','" + sys_var + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cls_num"://qcomcls1 '012'
                    consul = "qcomcls1";
                    if (art_tipSession.Equals("h") == true || art_tipSession.Equals("H") == true)
                    {
                        art_tipSession = "A";
                    }
                    cad = "'" + art_tipSession + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "subcls_cve": //qcomcld1 '012', '2'este campo se obtiene de qcomcls1 
                    consul = "qcomcld1";
                    if (art_tipSession.Equals("h") == true || art_tipSession.Equals("H") == true)
                    {
                        art_tipSession = "A";
                    }
                    cad = "'" + art_tipSession + "','" + variablesGuardar.Dedicado + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "sku_cve":
                    consul = "qcomartcve11";//qcomartcve11 '012'---> exec qcomartcve13  '012', 'Gasolina @ GAS0006'
                    if (pant != 2)
                    {
                        cad = "'" + art_tipSession + "'";
                        _dt = _con.ExecQryTabla(consul, cad);
                        if (pant == 3)//exec qcomsubp1 '012' ,'ACE0123'
                        {
                            consul = "qcomsubp1";
                            cad = "'" + art_tipSession + "','" + pasos[z - 1].ToString() + "'";
                            _dt = _con.ExecQryTabla(consul, cad);
                        }
                        else
                        {
                            if (prg_cve == "ieexdalm")
                            {
                                consul = "qcomarts1";
                                cad = "'" + art_tipSession + "'";
                                _dt = _con.ExecQryTabla(consul, cad);
                            }
                            else
                            {
                                consul = "qcomartcve13";
                                cad = "'" + art_tipSession + "','" + pasos[z - 1].ToString() + "'";
                                _dt = _con.ExecQryTabla(consul, cad);
                            }
                        }
                    }
                    if (pant == 2)
                    {
                        consul = "qcomsku2";
                        cad = "'" + variablesGuardar.Art_tipBDD + "','" + pasos[z - 1].ToString() + "'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "prm7":
                    consul = "qcomdiv1";
                    cad = "'" + variablesGuardar.Ef_cve + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "sw_ant":
                    consul = "qcomstsub1";// qcomstsub1 'lcartsub', '012', 'GAS0006', 'V'
                    DataTable dt = _con._tipoAnteriorBDD(variablesGuardar.User);
                    string tipo = dt.Rows[0][0].ToString();
                    string cve = dt.Rows[0][1].ToString();
                    cad = "'" + variablesGuardar.Prg_cves[pant, 0] + "','" + tipo + "','" + variablesGuardar.Art_cveFin + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "sw_act":  //exec dbo.qcomstsub3  'lcartsub', '012', 'GAS0006', 'V', 'Inactivo' --> //exec dbo.qcomstmat1 'lcartsub', '1  ' ---> //qcomstmat1 'lcartsub', '1  '
                    if (pant != 2)
                    {
                        consul = "qcomstsub3";
                        cad = prg_cve + ",'" + art_tipSession + "','" + pasos[z - 2].ToString() + "','V',''" + pasos[z - 1].ToString();
                        _dt = _con.ExecQryTabla(consul, cad);

                        consul = "qcomstmat1";
                        cad = prg_cve + ",'" + _dt + "'";
                        _dt = _con.ExecQryTabla(consul, cad);

                    }
                    if (pant == 2)
                    {
                        consul = "qcomstsub3";
                        cad = prg_cve2 + ",'" + art_tipSession + "','" + pasos[z - 2].ToString() + "','V',''" + "'" + pasos[z - 1].ToString() + "'";
                        _dt = _con.ExecQryTabla(consul, cad);

                        consul = "qcomstmat1";
                        cad = prg_cve2 + ",'" + _dt + "'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "prm1":
                    if (variablesGuardar.Prg_cves[pant, 0] == "icartmta")
                    {/* se agrego rtrim(acconcom.sku_cve) y 'NDES' en el sp */
                        consul = "qcomsku81";
                        cad = "'" + variablesGuardar.Art_tip + "'";
                    }
                    else
                    {
                        consul = "qcomcve1";
                        cad = "'" + tipo_ccc + "'";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "ieps_cve":
                    consul = "qcomiep1";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "art_cve":/* se agrego la columna art_cve al sp */
                    consul = "qcomart13";
                    cad = "'" + art_tipSession + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "per_hor":
                    consul = "qcomprf11";
                    cad = "'" + variablesGuardar.Art_tip + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "prm4":/* se modifico el sp para que regresara dos columnas */
                    consul = "qcomtm1a";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;

                case "status":
                    if (variablesGuardar.Prg_cves[pant, 0] == "icartss" || variablesGuardar.Prg_cves[pant, 0] == "icartst")
                    {
                        consul = "qcomstmat11";
                        cad = "'" + tipo_cc + "', 'FOLIO'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    else if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomswc1";
                        cad = "'C'";
                    }
                    else
                    {
                        consul = "qcomswc1";
                        cad = "'" + tipo_cc + "'";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "edo_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomedon1";
                        cad = "''";
                    }
                    else
                    {
                        consul = "qcomcd1";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "pais_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcompaisn1";
                        cad = "";
                    }
                    else
                    {
                        consul = "qcompais1";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cd_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcdn1";
                        cad = "'',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatcck" || variablesGuardar.Prg_cves[pant, 0] == "cccatccm" || variablesGuardar.Prg_cves[pant, 0] == "cccatcce" || variablesGuardar.Prg_cves[pant, 0] == "cccatccp")
                    {
                        consul = "qcomcd1";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "mpo_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcommpion1";
                        cad = "'','',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "cp":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcpn1";
                        cad = "'','','',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "art_tip_dest1":
                    consul = "qcomrpt1";
                    cad = "'40'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "art_tip_fte":
                    consul = "qcomartip11";
                    cad = "'" + variablesGuardar.Prg_cves[pant, 0] + "'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "puesto3":
                    consul = "qcomcve1";
                    cad = "'076'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "atencion3":
                    consul = "qcomcve1";
                    cad = "'077'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "if_cve":
                    consul = "qcomif1";
                    cad = "";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "rev_dias":
                    consul = "qcomcve1";
                    cad = "'082'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "comp1_cve":
                    consul = "qcomcve1";
                    cad = "'018'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "comp2_cve":
                    consul = "qcomcve1";
                    cad = "'018'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "comp3_cve":
                    consul = "qcomcve1";
                    cad = "'018'";
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "edo_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomedon1";
                        cad = "''";
                    }
                    else
                    {
                        consul = "qcomcd1";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "pais_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcompaisn1";
                        cad = "";
                    }
                    else
                    {
                        consul = "qcompais1";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "cd_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcdn1";
                        cad = "'',''";
                    }
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccp")
                    {
                        consul = "qcomcd1";
                        cad = "";
                    }
                    _dt = _con.ExecQryTabla(consul, cad);
                    break;
                case "munic_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcommpion1";
                        cad = "'','',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "cp_consi":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcpn1";
                        cad = "'','','',''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "suc_aten3":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcve1";
                        cad = "'092'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "polvta_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcompol1";
                        cad = "'001'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "foremb_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcc1";
                        cad = "'E'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccp")
                    {
                        consul = "qcomcc1";
                        cad = "'E'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "ven_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomcc1";
                        cad = "'V'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccp")
                    {
                        consul = "qcomcc1";
                        cad = "'M'";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "dia_entrega":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomdia1";
                        cad = "";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "linea44":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomif1";
                        cad = "";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "forpag_cve":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomfp21";
                        cad = "";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "linea42":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomef1";
                        cad = "";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
                case "linea43":
                    if (variablesGuardar.Prg_cves[pant, 0] == "cccatccam1")
                    {
                        consul = "qcomdivgrp21";
                        cad = "''";
                        _dt = _con.ExecQryTabla(consul, cad);
                    }
                    break;
            }
            return _dt;
        }
        public string deleteClasifica(string[,] vDatos, string tab, int filas)
        {
            string r = "";
            r = _con.deleteClasifica(vDatos, tab, filas);
            return r;
        }
        /* fin de los metodos de la pantalla Acciones*/
    }
}
