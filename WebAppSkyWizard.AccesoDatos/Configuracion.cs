using System;
using System.Web;
using System.Data;
using System.Linq;
//using System.Data.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;


namespace AccesoDatos
{
    public class Configuracion
    {
        /* variables estaticas */
        /* cadena de conexion, cambiarla depende a donde va a apuntar la aplicacion */
        //static string CadenaConecta = @"Data Source=SQL;Initial Catalog=skytex;User ID=soludin_develop;Password=dinamico20";
        static string CadenaConecta = @"Data Source=skyhdev3;Initial Catalog=develop;User ID=soludin_develop;Password=dinamico20";
        public static string CadenaConecta1
        {
            get { return Configuracion.CadenaConecta; }
            set { Configuracion.CadenaConecta = value; }
        }

        static string prg_cve_act;
        public static string Prg_cve_act
        {
            get { return Configuracion.prg_cve_act; }
            set { Configuracion.prg_cve_act = value; }
        }

        int ban;
        public int Ban
        {
            get { return ban; }
            set { ban = value; }
        }
        static string per_hor;
        public static string Per_hor
        {
            get { return Configuracion.per_hor; }
            set { Configuracion.per_hor = value; }
        }
        static string per_ver;
        public static string Per_ver
        {
            get { return Configuracion.per_ver; }
            set { Configuracion.per_ver = value; }
        }
        static string error;
        public static string Error
        {
            get { return Configuracion.error; }
            set { Configuracion.error = value; }
        }
        string[,] varSave = new string[100, 2];
        public string[,] VarSave
        {
            get { return varSave; }
            set { varSave = value; }
        }

        static string cadXuseldyn;
        public static string CadXuseldyn
        {
            get { return Configuracion.cadXuseldyn; }
            set { Configuracion.cadXuseldyn = value; }
        }
        static string[,] auxDatos = new string[100, 2];
        public static string[,] AuxDatos
        {
            get { return Configuracion.auxDatos; }
            set { Configuracion.auxDatos = value; }
        }

        static string[,] camposDatos = new string[100, 2];
        public static string[,] CamposDatos
        {
            get { return Configuracion.camposDatos; }
            set { Configuracion.camposDatos = value; }
        }

        static int tamDatos;
        public static int TamDatos
        {
            get { return Configuracion.tamDatos; }
            set { Configuracion.tamDatos = value; }
        }

        static string cUser;
        public static string CUser
        {
            get { return Configuracion.cUser; }
            set { Configuracion.cUser = value; }
        }

        static string art_tip;
        public static string Art_tip
        {
            get { return Configuracion.art_tip; }
            set { Configuracion.art_tip = value; }
        }


        static string[,] camposTabla = new string[100, 3];
        public static string[,] CamposTabla
        {
            get { return Configuracion.camposTabla; }
            set { Configuracion.camposTabla = value; }
        }

        static string wflujo;
        public static string Wflujo
        {
            get { return Configuracion.wflujo; }
            set { Configuracion.wflujo = value; }
        }

        static string art_cve;
        public static string Art_cve
        {
            get { return Configuracion.art_cve; }
            set { Configuracion.art_cve = value; }
        }

        static DataTable cls_valor;
        public static DataTable Cls_valor
        {
            get { return Configuracion.cls_valor; }
            set { Configuracion.cls_valor = value; }
        }

        static string ef_cve;
        public static string Ef_cve
        {
            get { return Configuracion.ef_cve; }
            set { Configuracion.ef_cve = value; }
        }
        static string id_ultactDB;

        public static string Id_ultactDB
        {
            get { return Configuracion.id_ultactDB; }
            set { Configuracion.id_ultactDB = value; }
        }

        static string artCve_aux;
        public static string ArtCve_aux
        {
            get { return Configuracion.artCve_aux; }
            set { Configuracion.artCve_aux = value; }
        }

        static string monedas;
        public static string Monedas
        {
            get { return Configuracion.monedas; }
            set { Configuracion.monedas = value; }
        }
        static string nom_Art;
        public static string Nom_Art
        {
            get { return Configuracion.nom_Art; }
            set { Configuracion.nom_Art = value; }
        }

        static string rfc;
        public static string Rfc
        {
            get { return Configuracion.rfc; }
            set { Configuracion.rfc = value; }
        }

        string nom_tabla = "";

        /* funciones para la pantalla Default */
        public DataTable StepUser(string flujo, string user)
        {
            DataTable dtStep = new DataTable();
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandTimeout = 3600;
                _cmd.CommandText = String.Format("exec InsertUserWizard '{0}','{1}' ", user, flujo);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dtStep);
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return dtStep;
        }

        public DataTable defaul(string paso_cve)
        {
            DataTable TablaPasos = new DataTable();
            DataColumn workCol = TablaPasos.Columns.Add("paso_cve", typeof(String));
            TablaPasos.Columns.Add("orden", typeof(String));
            TablaPasos.Columns.Add("prg_cve", typeof(String));
            TablaPasos.Columns.Add("tipo_cc", typeof(String));
            TablaPasos.Columns.Add("status", typeof(String));
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select paso_cve,orden,prg_cve,tipo_cc,status from WizardPasos where paso_cve = '{0}' ", paso_cve);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(TablaPasos);
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return TablaPasos;
        }

        /* fin de funciones de la pantalla Default */


        /* Metodos para la pantalla de AddArt */
        public int no_pasos(string paso_cve)
        {
            int r = 0;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("SELECT count(*) FROM WizardPasos WHERE paso_cve = '{0}' and orden != 0", paso_cve);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            r = Convert.ToInt32(_cmd.ExecuteScalar());
            _conn.Close();
            return r;
        }
        //paso en el que se quedo el usuario
        public DataTable parametroInicial(string orden, string flujo)
        {
            DataTable r = new DataTable();
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select prg_cve, tipo_cc from WizardPasos  where orden = '{0}' and paso_cve = '{1}' ", orden, flujo);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            _cmd.ExecuteNonQuery();
            _da.Fill(r);
            _conn.Close();

            return r;
        }
        public string art_tipPant(string _flujo, string prg_cvePant)
        {
            string dt = "";
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select tipo_cc from WizardPasos where paso_cve = '{0}' and prg_cve = '{1}'", _flujo, prg_cvePant);
                _conn.Open();
                dt = Convert.ToString(_cmd.ExecuteScalar());
                _cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return dt;
        }
        public string tituloStep(string prg_cve)
        {
            string r = "";
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select nombre from xuprogs where prg_cve ='{0}' ", prg_cve);
            _conn.Open();
            r = Convert.ToString(_cmd.ExecuteScalar());
            //_cmd.ExecuteNonQuery();
            _conn.Close();
            return r;
        }
        public DataTable xcdic(string prg_cve, string user, string tab, string ef_cve)
        {
            DataTable tblXuarrays = new DataTable();
            DataTable tblXcdic = new DataTable();

            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("SELECT pant, stuff((SELECT rtrim(nombre) + ' ' FROM xuarrays b where b.prg_cve = '{0}' and b.array_nom like 'GL_Tablas%' and b.col = 0 and b.pant = a.pant for xml path('') ) ,1,0,'' ) as nombre FROM xuarrays a where prg_cve = '{0}' and array_nom like 'GL_Tablas%' and col = 0 group by pant", prg_cve);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            _cmd.ExecuteNonQuery();
            _da.Fill(tblXuarrays);
            _conn.Close();

            DataTable xuarraysTemp = new DataTable();
            SqlConnection _connX = new SqlConnection(CadenaConecta1);
            SqlCommand _cmdX = new SqlCommand();
            _cmdX.Connection = _connX;
            _cmdX.CommandType = CommandType.Text;
            _cmdX.CommandText = String.Format("select nombre from xuarrays where prg_cve = '{0}' and array_nom like 'GL_Tablas%' and col = 0", prg_cve);
            SqlDataAdapter _daX = new SqlDataAdapter(_cmdX);
            _connX.Open();
            _cmdX.ExecuteNonQuery();
            _daX.Fill(xuarraysTemp);
            _connX.Close();

            for (int i = 0; i < tblXuarrays.Rows.Count; i++)
            {
                string nomTabla1 = "";
                string nomTabla2 = "";
                string nomTabla3 = "";
                nomTabla1 = xuarraysTemp.Rows[i][0].ToString().TrimEnd(' ');
                if (i == 0 && tblXuarrays.Rows.Count == 2 && prg_cve.Contains("cccatcc") == false)
                {
                    if(xuarraysTemp.Rows[i + 1][0] != null)
                    {
                        nomTabla2 = xuarraysTemp.Rows[i + 1][0].ToString().TrimEnd(' ');
                    }
                }
                if (i == 1 && tblXuarrays.Rows.Count == 2 && prg_cve.Contains("cccatcc") == false)
                {
                    if(xuarraysTemp.Rows[i + 1][0] != null)
                    {
                        nomTabla1 = xuarraysTemp.Rows[i + 1][0].ToString().TrimEnd(' ');
                    }
                }

                DataTable xcdicTem = new DataTable();
                SqlConnection _connF = new SqlConnection(CadenaConecta1);
                SqlCommand _cmdF = new SqlCommand();
                _cmdF.Connection = _connF;
                _cmdF.CommandType = CommandType.Text;
                _cmdF.CommandText = String.Format("exec dbo.sp_qopmenu '{1}', '{2}', '{3}', '{0}', '{4}', '{5}'", tab, nomTabla1, nomTabla2, nomTabla3, user, ef_cve);
                SqlDataAdapter _daF = new SqlDataAdapter(_cmdF);
                _connF.Open();
                _cmdF.ExecuteNonQuery();
                _daF.Fill(xcdicTem);
                _connF.Close();
                xcdicTem.Columns.Add("pant", typeof(Int32));
                int numFilas = xcdicTem.Rows.Count;
                for (int y = 0; y < numFilas; y++)
                {
                    xcdicTem.Rows[y]["pant"] = i;
                }

                tblXcdic.Merge(xcdicTem);
            }
            return tblXcdic;
        }
        public DataTable xuarrays(string prg_cve)// se envia a una tabla el xuarrays, para saber que tabla va en la primer pantalla 
        {
            DataTable T_xuarrays = new DataTable();
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select * from xuarrays where prg_cve = '{0}' and array_nom like 'GL%' ", prg_cve);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            _cmd.ExecuteNonQuery();
            _da.Fill(T_xuarrays);
            _conn.Close();
            return T_xuarrays;
        }
        public int CuentaXcdicB(string tipo_cc, string tab, string secciones)
        {
            int r = 0;
            try
            {
                if (secciones == "C")
                {
                    SqlConnection _conn = new SqlConnection(CadenaConecta1);
                    SqlCommand _cmd = new SqlCommand();
                    _cmd.Connection = _conn;
                    _cmd.CommandType = CommandType.Text;
                    _cmd.CommandText = String.Format("select count(*) from xcdic where tipo_cc = '{0}' and tabla in ({1}) ", tipo_cc, tab);
                    _conn.Open();
                    r = Convert.ToInt32(_cmd.ExecuteScalar());
                    _conn.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar la tabla xcdic: ", e);
            }
            return r;
        }
        public DataTable TablaXuarraysBotones(string prg_cve)// se envia a una tabla el xuarrays, para saber que tabla va en la primer pantalla 
        {
            DataTable T_xuarrays = new DataTable();
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            //_cmd.CommandText = String.Format("select * from xuarrays where prg_cve = '{0}' and array_nom like 'GL%' ", prg_cve);
            _cmd.CommandText = String.Format("select * from xuarrays where prg_cve = '{0}' and array_nom like 'GL_botones%' and col = 0 and nombre not like '%egresar%' order by orden", prg_cve);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            _cmd.ExecuteNonQuery();
            _da.Fill(T_xuarrays);
            _conn.Close();
            return T_xuarrays;
        }
        public DataTable totalTablas(string prg_cve, string seccion, string tab)
        {
            DataTable r = new DataTable();
            if ((seccion == "C") || (seccion == "c"))
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select distinct xuarrays.pant from xuarrays join xcdic on xcdic.tabla = xuarrays.nombre and xcdic.tipo_cc = '{0}' and tipo_act > 0 where xuarrays.prg_cve = '{1}' AND xuarrays.array_nom = 'GL_tablas' and xuarrays.col = 0 order by xuarrays.pant", tab, prg_cve);
                //_cmd.CommandText = String.Format("select distinct xcdic.tabla,xuarrays.orden from xuarrays join xcdic on xcdic.tabla = xuarrays.nombre and xcdic.tipo_cc = '{0}' and tipo_act > 0 where xuarrays.prg_cve = '{1}' AND xuarrays.array_nom = 'GL_tablas' and xuarrays.col = 0 order by xuarrays.orden", tab, prg_cve);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(r);
                _conn.Close();
            }
            return r;
        }
        public DataTable temp_GL_tablas(string prg_cve)//funcion que regresa el nombre de las tablas que utuliza una pantalla y que estan configuradas en el xuarrays
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                //_cmd.CommandText = String.Format("select distinct (nombre)   from xuarrays  where prg_cve = '{0}' and array_nom = 'GL_tablas' and nombre <> '{0}' ", prg_cve);
                if (prg_cve.Equals("mcact") == true)
                {
                    _cmd.CommandText = String.Format("select distinct nombre from xuarrays  where prg_cve = '{0}' and array_nom = 'GL_tablas'", prg_cve);
                }
                else
                {
                    _cmd.CommandText = String.Format("select distinct nombre,orden from xuarrays  where prg_cve = '{0}' and array_nom = 'GL_tablas' and nombre <> '{0}'  order by orden", prg_cve);
                }

                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();// throw e;
            }
            return dt;
        }
        public DataTable temp_GLquery(string prg, int pant)//Obtiene el GL_qrycombo de los combo de la pantalla
        {
            int c = 0;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select nombre from xuarrays where prg_cve = '{0}' and array_nom like 'GL_qrycom%' and pant = {1} and col = 0", prg, pant);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return dt;
        }
        public DataTable qtiparta(string tipoCC, string tabla)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                if (tipoCC == "HTP")
                {
                    _cmd.CommandText = String.Format("select tabla, campo, ctop, cleft, csize, orden, label, ltop, lleft, lsize,tipo_act, tipo_val, sys_var, tipo_cc, tipo_for, sw_busqueda= Case When sw_busqueda=1 Then -1 Else sw_busqueda End, tipo_ccc, tipo_combo, size_text, balin2, balin3, balin4, balin5, balin6, '' as formato, '' as rangoini, '' as rangofin,web_obj from xcdic where tipo_cc = '{0}' and tabla = '{1}' and campo not like '%2' order by orden desc ", tipoCC, tabla);
                }
                else
                {
                    _cmd.CommandText = String.Format("select tabla, campo, ctop, cleft, csize, orden, label, ltop, lleft, lsize,tipo_act, tipo_val, sys_var, tipo_cc, tipo_for, sw_busqueda= Case When sw_busqueda=1 Then -1 Else sw_busqueda End, tipo_ccc, tipo_combo, size_text, balin2, balin3, balin4, balin5, balin6, '' as formato, '' as rangoini, '' as rangofin,web_obj from xcdic where tipo_cc = '{0}' and tabla = '{1}' order by orden desc ", tipoCC, tabla);
                }
                //_cmd.CommandText = String.Format("select tabla, campo, ctop, cleft, csize, orden, label, ltop, lleft, lsize,tipo_act, tipo_val, sys_var, tipo_cc, tipo_for, sw_busqueda= Case When sw_busqueda=1 Then -1 Else sw_busqueda End, tipo_ccc, tipo_combo, size_text, balin2, balin3, balin4, balin5, balin6, '' as formato, '' as rangoini, '' as rangofin,web_obj from xcdic where tipo_cc = '{0}' and tabla = '{1}' order by orden desc ", tipoCC, tabla);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
            } return dt;
        }
        public string nombreLabel(string campo, string prg_cve, string tipo_cc, int secc)
        {
            string r = "";
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select xcdic.label from xuarrays join xcdic on xcdic.tabla = xuarrays.nombre and xcdic.tipo_cc = '{0}' and tipo_act > 0 where xuarrays.prg_cve = '{2}' AND xuarrays.array_nom = 'GL_tablas' and xuarrays.col = '{3}' and xcdic.campo = '{1}'  order by xuarrays.pant, xcdic.orden", tipo_cc, campo, prg_cve, secc);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            r = Convert.ToString(_cmd.ExecuteScalar());
            _conn.Close();
            return r.TrimEnd(' ');
        }
        public int _validacion(string campo, string prg_cve, string tipo_cc, int secc)
        {
            int r = 0;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select  xcdic.tipo_val  from xuarrays join xcdic on xcdic.tabla = xuarrays.nombre and xcdic.tipo_cc = '{0}' and tipo_act >0 where campo='{1}'AND xuarrays.prg_cve = '{2}' AND xuarrays.array_nom = 'GL_tablas' and xuarrays.col = 0 and xuarrays.pant = '{3}' and campo not in ('cc_cve') order by xuarrays.orden, xcdic.orden", tipo_cc, campo, prg_cve, secc);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            r = Convert.ToInt32(_cmd.ExecuteScalar());
            _conn.Close();
            return r;
        }
        public string tipoCCWizard()
        {
            //select * from cccatcc where cc_cve = (select cc_cve from xusubmat where art_tip = '012' and tipo_prg = 'ccstsm'  AND cc_tipo = 'P' and sku_cve = ' ADI0028' ) and cc_tipo = 'P'
            string tipo_cc;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format(" select tipo_cc from WizardPasos where prg_cve = '{0}'", prg_cve_act);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            tipo_cc = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return tipo_cc;
        }
        public DataTable temp_sysvar(string prg_cve, string tipo_cc)//Obtiene el GL_qrycombo de los combo de la pantalla
        {
            int c = 0;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select campo,tipo_act,sys_var from xcdic where tipo_cc = '{0}' and tabla in ('{1}') and sys_var != 0 and tipo_act = 2", tipo_cc, prg_cve);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return dt;
        }
        public string temp_dic_var(string ren)
        {
            string r = "";
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select nombre from xuarrays where prg_cve = '{0}' and array_nom like 'GL_dicvar%' and ren = '{1}'", prg_cve_act, ren);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            r = Convert.ToString(_cmd.ExecuteScalar());
            _conn.Close();
            return r.TrimEnd(' ').Replace("-", "");
        }
        public string parametroBase(string prg_cve, string sys_var)
        {
            string parametro = "";
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select nombre from xuarrays where prg_cve='{0}' and ren={1} and array_nom='GL_dicvar'", prg_cve, sys_var);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            parametro = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return parametro;
        }
        //obtiene el sys_var de acuerdo al campo que se le envia de la tabla
        public string obtieneSysVar(string campo, string prg_actual, string tipo_cc, string tabla)
        {
            string dt;
            SqlConnection conn = new SqlConnection(CadenaConecta1);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = String.Format("select sys_var from xcdic where tipo_cc = '{1}' and campo='{0}' and web_obj <> 'null'", campo, tcc);
            cmd.CommandText = String.Format("select sys_var from xcdic where tipo_cc = '{1}' and campo='{0}' and tipo_act = 2 and sys_var != 0 and tabla in ('{2}')", campo, tipo_cc, tabla); //and web_obj is not null
            conn.Open();
            dt = Convert.ToString(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            conn.Close();
            dt = dt.Trim(' ');
            return dt;
        }
        public void cargaFlujo(string flujo)
        {
            wflujo = flujo;
        }
        //separa la cadena xuseldyn por campos y los mete en una matriz para llenarla con el valor que valla a traer de la regla de negocio
        public string guardaBloque(string prg_cve, int secc)
        {
            string tabla1 = "";// "icartphv";//elaborar una funcion que obtenga las tablas que  utiliza una pantalla mediante el xuarrays
            DataTable dt_tablas1 = temp_GL_tablas2(prg_cve, secc);
            tabla1 = dt_tablas1.Rows[0][0].ToString();
            tabla1 = tabla1.TrimEnd(' ');
            string auxprg_cve = "";

            string artClave = "";
            try
            {
                string tabla = "";// "icartphv";//elaborar una funcion que obtenga las tablas que  utiliza una pantalla mediante el xuarrays
                int no_tablas = dt_tablas1.Rows.Count;
                int v = 0;
                string sql0 = "";
                string sql1 = "";
                string sql2 = "";
                string sql3 = "";
                string sql4 = "";
                string sql5 = "";
                string sqlFinal = "";
                for (int y = 0; y < no_tablas; y++)
                {
                    tabla = dt_tablas1.Rows[y][0].ToString();
                    tabla = tabla.TrimEnd(' ');
                    prg_cve_act = prg_cve;
                    if (prg_cve.Equals("cccatccam1") == true && tabla.Equals("cccatccamar1") == true)
                    {
                        tabla = "cccatcc";
                    }
                    if (secc == 1 && prg_cve.Equals("icartsb") == true)
                    {
                        tabla1 = "icartphh";
                    }
                    else if (secc == 1 && prg_cve.Equals("icartsh") == true)
                    {
                        tabla1 = "icartphh";
                    }
                    else if (secc == 1 && prg_cve.Equals("cccatccam1") == true)
                    {
                        tabla1 = "ccdetcc";
                    }
                    else
                    {
                        //tabla1 = prg_cve;
                    }

                    if (tabla != "icartcls")
                    {
                        string ssql = columnasTablas(tabla);
                        string nomVar = string.Format("sql{0}", y.ToString());
                        if (nomVar.Equals("sql0"))
                        {
                            sql0 = ssql;
                        }
                        else if (nomVar.Equals("sql1"))
                        {
                            sql1 = ssql;
                        }
                        else if (nomVar.Equals("sql2"))
                        {
                            sql2 = ssql;
                        }
                        else if (nomVar.Equals("sql3"))
                        {
                            sql3 = ssql;
                        }
                        else if (nomVar.Equals("sql4"))
                        {
                            sql4 = ssql;
                        }
                        else if (nomVar.Equals("sql5"))
                        {
                            sql5 = ssql;
                        }
                    }
                }
                sqlFinal = sql0 + sql1 + sql2 + sql3 + sql4 + sql5;
                if (sqlFinal != "")
                {
                    //v = Insert(sqlFinal);//guardar en la base de datos
                    v = 1;
                }
                if (v >= 1)
                {
                    v = v + 1;
                }
                if (v >= 2)
                {
                    artClave = Configuracion.ArtCve_aux;
                    string clave = art_cveBDD();
                    if (artClave == null)
                    {
                        Art_cve = clave;
                    }
                    else
                    {
                        if (artClave.Substring(0, 1) == ">")
                        {
                            Art_cve = artClave;
                        }
                        else
                        {
                            Art_cve = clave;
                        }
                    }
                    //Art_cve = Configuracion.ArtCve_aux;
                    notificarPaso(ordenFlujo(prg_cve, wflujo), CUser, wflujo, Nom_Art); //Guarda el art_cve en la tabla wStepUser para notificar que ya cumplio con este paso correctamente
                }
                else
                {
                    Art_cve = error;
                    if (Convert.ToInt32(ordenFlujo(prg_cve, wflujo)) > 1)
                    {
                        notificarPaso(ordenFlujo(prg_cve, wflujo), CUser, wflujo, Nom_Art);//graba el error en la base de datos
                    }
                }
                for (int h = 0; h < 100; h++)
                {
                    CamposTabla[h, 0] = "";
                    CamposTabla[h, 1] = "";
                    CamposTabla[h, 2] = "";

                    varSave[h, 0] = "";
                    varSave[h, 1] = "";
                }
            }
            catch (Exception t)
            {
                Art_cve = t.Message.ToString() + "configuracion";
            }
            return Art_cve;
        }
        public void borrar()
        {
            int d = 0;
            for (int x = 0; x < 100; x++)
            {
                varSave[x, 0] = "";
                varSave[x, 1] = "";
            }
            for (int u = 0; u < 100; u++)
            {
                auxDatos[u, 0] = "";
                auxDatos[u, 1] = "";
            }
        }
        public void finalizaFlujo()
        {
            int r = 0;
            try
            {
                string ssql = String.Format("update wStepUser set orden ='{0}', art_cve = '{3}',nombre = '{4}' where usuario = '{1}' and flujo = '{2}'", "0", cUser, wflujo, "0","");
                string ConnectionString = CadenaConecta1;
                SqlConnection cnn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("sp_InsertWizard", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ssql", ssql);
                cnn.Open();
                cmd.ExecuteNonQuery();
                r = 1;
                cnn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
        }
        public string ultimoArt_Tip(string _user, string _flujo)
        {
            string dt = "";
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select art_tip from wStepUser where usuario = '{0}' and flujo = '{1}'", _user, _flujo);
                _conn.Open();
                dt = Convert.ToString(_cmd.ExecuteScalar());
                //_cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return dt;
        }
        public string insertClasifica(string[,] vDatos, string tab, int filas)
        {
            string r = "";
            string ssql = "";
            string columnas = "";
            string valores = "";
            DataTable dt = esquemaTabla(tab);
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int q = 0; q < filas; q++)
                    {
                        if (vDatos[q, 0].ToString().Equals(dt.Rows[i][0].ToString()) == true && vDatos[q, 0] != null && dt.Rows[i][0] != null)
                        {
                            vDatos[q, 2] = dt.Rows[i][0].ToString(); // son las columnas de la tabla de la base de datos
                        }
                    }
                }
                for (int y = 0; y <= dt.Rows.Count + 1; y++)
                {
                    if (tab.Equals("ccclscc") == true)
                    {
                        if (vDatos[y, 2] != null)
                        {
                            columnas = columnas + vDatos[y, 2].ToString() + ",";
                            valores = valores + "'" + vDatos[y, 1].ToString() + "',";
                        }
                    }
                    if (tab.Equals("icartcls") == true)
                    {
                        if (vDatos[y, 2] != null)
                        {
                            columnas = columnas + vDatos[y, 2].ToString() + ",";
                            if (vDatos[y, 1] == "")
                            {
                                string tipo_cc = tipoCCWizard();
                                string sysvar = obtieneSysVar(vDatos[y, 0], Prg_cve_act, tipo_cc, tab);
                                string value = temp_dic_var(sysvar);
                                if (vDatos[y, 2].ToString().Equals("art_tip") == true)
                                {
                                    vDatos[y, 1] = Art_tip;
                                }
                                else
                                {
                                    vDatos[y, 1] = value;
                                }
                                valores = valores + "'" + vDatos[y, 1].ToString() + "',";

                                if (vDatos[y, 2].ToString().Equals("per_hor") == true)
                                {
                                    Configuracion.Per_hor = value;
                                }
                                else if (vDatos[y, 2].ToString().Equals("per_ver") == true)
                                {
                                    Configuracion.Per_ver = value;
                                }
                            }
                            else
                            {
                                valores = valores + "'" + vDatos[y, 1].ToString() + "',";
                            }

                        }
                    }
                }
                valores = valores.Substring(0, valores.Length - 1);
                columnas = columnas.Substring(0, columnas.Length - 1);

                ssql = "insert into " + tab + " (" + columnas + ") values (" + valores + ")";
                //int w = Insert(ssql);
                int w = 1;
                if (w == 1)
                {
                    r = "Artículo Clasificado";
                }
                else
                {
                    r = error;
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return r;
        }
        public string cls_busca(string artClave, string user, string artTip)
        {
            string r = "";
            DataTable dt = new DataTable();
            DataTable dtcls = new DataTable();
            try
            {
                if (prg_cve_act.Equals("cccatccam1") == true || prg_cve_act.Equals("cccatcck") == true || prg_cve_act.Equals("cccatccm") == true || prg_cve_act.Equals("cccatcce") == true || prg_cve_act.Equals("cccatccp") == true)
                {
                    SqlConnection conn = new SqlConnection(CadenaConecta1);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = String.Format("exec dbo.qlisclscc1 '{0}','{1}'", Art_tip, Art_cve);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    da.Fill(dtcls);
                    r = Convert.ToString(dtcls.Rows[0][0].ToString() + " - " + dtcls.Rows[0][1].ToString());
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(CadenaConecta1);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = String.Format("exec dbo.qliscls1 '{0}','{1}','{2}','{3}'", artClave, Per_hor, Per_ver, Art_tip);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    da.Fill(dtcls);
                    r = Convert.ToString(dtcls.Rows[0][0].ToString() + " - " + dtcls.Rows[0][1].ToString());
                    conn.Close();
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
                string[,] _error = new string[1, 1];
                _error[0, 0] = t.Message.ToString();
            }
            return r;
        }
        public string obtieneCls(string prg_cve)
        {
            string cls;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select distinct nombre from xuarrays where prg_cve = '{0}' and array_nom like 'GL_tablas%'  and nombre like '%cls%'", prg_cve);
            _conn.Open();
            cls = Convert.ToString(_cmd.ExecuteScalar());
            //_cmd.ExecuteNonQuery();
            _conn.Close();
            return cls;
        }
        public string tipoCC(string orden, string prg_cve)
        {
            string tipoCC = "";
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("SELECT tipo_cc from WizardPasos where orden = '{0}' and prg_cve = '{1}'", orden, prg_cve);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                //_cmd.ExecuteNonQuery();
                //_da.Fill(dtStep);
                tipoCC = Convert.ToString(_cmd.ExecuteScalar());
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return tipoCC;
        }
        public int nParametros(string store)
        {
            DataTable dt = new DataTable();
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select * from information_schema.parameters where specific_name='{0}'", store);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            _cmd.ExecuteNonQuery();
            _da.Fill(dt);
            _conn.Close();
            return dt.Rows.Count;
        }
        public DataTable temp_GL_tablas2(string prg_cve, int secc)//funcion que regresa el nombre de las tablas que utuliza una pantalla y que estan configuradas en el xuarrays
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                if (prg_cve.Equals("mcact") == true || prg_cve.Equals("ccinfad") == true)
                {
                    _cmd.CommandText = String.Format("select distinct nombre from xuarrays where prg_cve = '{0}' and array_nom like 'GL_tablas%' and pant = {1} and col = 0", prg_cve, secc);  //and nombre <> '{0}'
                }
                else
                {
                    _cmd.CommandText = String.Format("select distinct nombre from xuarrays where prg_cve = '{0}' and array_nom like 'GL_tablas%' and nombre <> '{0}' and pant = {1} and col = 0", prg_cve, secc);  //and nombre <> '{0}'
                    //_cmd.CommandText = String.Format("select distinct nombre,orden from xuarrays where prg_cve = '{0}' and array_nom like 'GL_tablas%' and nombre <> '{0}' and pant = {1} order by orden", prg_cve, secc);  //and nombre <> '{0}'
                }
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();
            }
            catch (Exception e)
            {
                e.Message.ToString();// throw e;
            }
            return dt;
        }
        public string columnasTablas(string tabla)//obtiene los nombres de las columnas de la tabla y se las envia a un vector para adjuntarle su valor
        {
            tabla = tabla.TrimEnd(' ');
            nom_tabla = tabla;
            DataTable camposTabla = new DataTable();
            string cl = "";
            string ssql = "";
            string valor = "";
            try
            {
                string tipo_cc = tipoCCWizard();
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select COLUMN_NAME, DATA_TYPE, IS_NULLABLE,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '{0}'", tabla);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(camposTabla);
                _conn.Close();

                DataTable temp_sys_var = temp_sysvar(tabla, tipo_cc);

                int camposArreglo = Configuracion.AuxDatos.Length / 2;
                int encontrado = 0;
                for (int i = 0; i < camposTabla.Rows.Count; i++)
                {
                    for (int y = 0; y < camposArreglo; y++)
                    {
                        if (camposTabla.Rows[i][0].ToString().ToLower().Equals("col_id") == false)
                        {
                            if (Configuracion.AuxDatos[y, 0] != null)
                            {
                                if (Configuracion.AuxDatos[y, 0] != "")
                                {
                                    if (camposTabla.Rows[i][0].ToString().ToLower().Equals(Configuracion.AuxDatos[y, 0].ToString()) == true)
                                    {
                                        if (encontrado == 0)
                                        {
                                            if (camposTabla.Rows[i][0].ToString().ToLower().Equals("fecha") == true && encontrado == 0 && tabla.Equals("xusubmat") == true && tabla.Equals("cccatcc") == true)
                                            {
                                                string fecha = Configuracion.AuxDatos[y, 1].ToString();
                                                cl = cl + Configuracion.AuxDatos[y, 0].ToString() + " , ";
                                                valor = valor + "'" + Convert.ToDateTime(fecha).ToString("MM/dd/yyyy") + "',";
                                                encontrado = 1;
                                            }
                                            else
                                            {
                                                cl = cl + Configuracion.AuxDatos[y, 0].ToString() + " , ";
                                                valor = valor + "'" + Configuracion.AuxDatos[y, 1].ToString() + "',";
                                                encontrado = 1;
                                            }
                                        }
                                    }
                                    else if (camposTabla.Rows[i][0].ToString().ToLower().Equals("id_ultact") == true && encontrado == 0)
                                    {
                                        cl = cl + camposTabla.Rows[i][0].ToString().ToLower() + " , ";
                                        valor = valor + "'" + Configuracion.CUser + "',";
                                        encontrado = 1;
                                    }
                                    else if (camposTabla.Rows[i][0].ToString().ToLower().Equals("ef_cve") == true && encontrado == 0)
                                    {
                                        cl = cl + camposTabla.Rows[i][0].ToString().ToLower() + " , ";
                                        valor = valor + "'" + Configuracion.Ef_cve + "',";
                                        encontrado = 1;
                                    }
                                    else if (camposTabla.Rows[i][0].ToString().ToLower().Equals("sku_cve") == true && encontrado == 0 && tabla.Equals("xusubmat") == true)
                                    {
                                        cl = cl + camposTabla.Rows[i][0].ToString().ToLower() + " , ";
                                        valor = valor + "'" + Configuracion.ArtCve_aux + "',";
                                        encontrado = 1;
                                    }
                                    else if (camposTabla.Rows[i][0].ToString().ToLower().Equals("rfc_fin") == true && encontrado == 0 && tabla.Equals("cccatcc") == true)
                                    {
                                        if (tipo_cc.Equals("M") == false)
                                        {
                                            cl = cl + camposTabla.Rows[i][0].ToString().ToLower() + " , ";
                                            valor = valor + "'" + Configuracion.Rfc + "',";
                                            encontrado = 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (encontrado == 0)
                    {
                        for (int x = 0; x < temp_sys_var.Rows.Count; x++)
                        {
                            if (camposTabla.Rows[i][0].ToString().ToLower().Equals(temp_sys_var.Rows[x][0].ToString().TrimEnd(' ')) == true)
                            {
                                string dicvar = temp_dic_var(temp_sys_var.Rows[x][2].ToString());
                                cl = cl + camposTabla.Rows[i][0].ToString().ToLower() + " , ";
                                valor = valor + "'" + dicvar + "',";
                                if (camposTabla.Rows[i][0].ToString().ToLower().Equals("cc_tipo") == true && tabla.Equals("cccatcc") == true)
                                {
                                    Configuracion.Art_tip = dicvar;
                                }
                                encontrado = 1;
                            }
                        }
                        if (encontrado == 0)
                        {
                            if (camposTabla.Rows[i][1].ToString().ToLower().Equals("nvarchar") == true || camposTabla.Rows[i][1].ToString().Equals("nchar") == true)
                            {
                                if (camposTabla.Rows[i][0].ToString().ToLower().Equals("rfc_fin") == true)
                                {
                                    if (tipo_cc.Equals("M") == false)
                                    {
                                        cl = cl + camposTabla.Rows[i][0].ToString().ToLower() + " , ";
                                        valor = valor + "'',";
                                    }
                                }
                                else
                                {
                                    cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                    valor = valor + "'',";
                                }
                            }
                            else if (camposTabla.Rows[i][1].ToString().Equals("smallint") == true)
                            {
                                cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                valor = valor + 1 + ",";
                            }
                            else if (camposTabla.Rows[i][1].ToString().Equals("bit") == true)
                            {
                                cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                valor = valor + 0 + ",";
                            }
                            else if (camposTabla.Rows[i][1].ToString().Equals("datetime") == true || camposTabla.Rows[i][0].ToString().Equals("fec_ultact") == true)
                            {
                                cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                valor = valor + " getdate(),";
                            }
                        }
                    }
                    encontrado = 0;
                }
                cl = cl.Substring(0, cl.Length - 2);
                string coma = valor.Substring(valor.Trim().Length - 1, 1);
                if (coma == ",")
                {
                    valor = valor.Substring(0, valor.Trim().Length - 1);
                }
                if (tabla == "xusubmat")
                {
                    ssql = "insert into " + tabla + " (" + cl + ") values ( " + valor + ") SELECT SCOPE_IDENTITY()";
                }
                else
                {
                    ssql = "insert into " + tabla + " (" + cl + ") values (" + valor + ")";
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();// throw e;
            }
            return ssql;
        }
        public int Insert(string ssql)
        {
            int r = 0;
            try
            {
                string ConnectionString = CadenaConecta1;
                SqlConnection cnn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("sp_InsertWizard", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("@ssql", ssql);
                cnn.Open();
                /* ejecucion de la insercion en la base de datos */
                cmd.ExecuteNonQuery();
                r = 1;
                cnn.Close();
            }
            catch (Exception e2)
            {
                r = 0;
                e2.Message.ToString();
                error = ">" + e2.Message.ToString();
            }
            return r;
        }
        int lon = 0;
        public string art_cveBDD()
        {
            string prm1 = "";
            string prm2 = "";
            string prm3 = "";
            string artCveIni = "";
            string artCveFin = "";

            for (int x = 0; x < lon; x++)
            {
                if (camposTabla[x, 0].ToString().Equals("art_cve") == true)
                {
                    artCveIni = camposTabla[x, 1].ToString();
                    artCveIni = artCveIni + "%";
                }
                if (camposTabla[x, 0].ToString().Equals("sku_cve") == true && prg_cve_act.Equals("icartsh") == false)
                {
                    artCveIni = camposTabla[x, 1].ToString();
                    artCveIni = artCveIni + "%";
                }
                if (camposTabla[x, 0].ToString().Equals("nom_comer") == true)
                {
                    prm1 = camposTabla[x, 1].ToString();
                }
                if (camposTabla[x, 0].ToString().Equals("art_tip") == true)
                {
                    prm2 = camposTabla[x, 1].ToString();
                }
                if (camposTabla[x, 0].ToString().Equals("id_ultact") == true)
                {
                    prm3 = camposTabla[x, 1].ToString();
                }
            }
            try
            {
                if (lon == 0)
                {
                    prm3 = Configuracion.CUser;
                    prm2 = Configuracion.Art_tip;
                    if (prg_cve_act.Contains("cccat") == false)
                    {
                        artCveIni = Configuracion.ArtCve_aux.ToString().Trim() + "%";
                    }
                }
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                if (prg_cve_act == "mcact")
                {
                    _cmd.CommandText = String.Format("select MAX(cve_activ) from mcact where art_tip ='{0}' and art_cve like '{1}' ", prm2, artCveIni);
                }
                else if (prg_cve_act.Contains("cccat") == true)
                {
                    string cc_tipo = "";
                    if (prg_cve_act == "cccatccam1")
                    {
                        cc_tipo = "C";
                    }
                    else
                    {
                        cc_tipo = tipoCCWizard();
                    }
                    _cmd.CommandText = String.Format("select MAX(cc_cve) from cccatcc where id_ultact ='{0}' and cc_tipo = '{1}'", prm3, cc_tipo);
                }
                else
                {
                    _cmd.CommandText = String.Format("select art_cve from icartesp where art_tip ='{0}' and art_cve like '{1}' order by fec_ultact desc", prm2, artCveIni);
                    //_cmd.CommandText = String.Format("select MAX(art_cve) from icartphv where art_tip ='{0}' and art_cve like '{1}' ", prm2, artCveIni);
                }
                _conn.Open();
                artCveFin = Convert.ToString(_cmd.ExecuteScalar());
                _cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return artCveFin;
        }
        public string ordenFlujo(string prg_cve, string flujo)
        {
            string dt = "";
            int o = 0;
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select orden from WizardPasos where prg_cve = '{0}' and paso_cve='{1}'", prg_cve, flujo);
                _conn.Open();
                dt = Convert.ToString(_cmd.ExecuteScalar());
                _cmd.ExecuteNonQuery();
                _conn.Close();
                o = Convert.ToInt32(dt) + 1;
                dt = o.ToString();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return dt;
        }
        public void notificarPaso(string orden, string usuario, string flujo, string nombre)
        {
            /* cuando un registro se inserta bien, se actualiza la tabla wStepUser */
            int r = 0;
            try
            {
                string ssql = String.Format("update wStepUser set orden ='{0}', art_cve = '{3}', art_tip = '{4}', nombre = '{5}' where usuario = '{1}' and flujo = '{2}'", orden, usuario, flujo, Art_cve, Art_tip, Nom_Art);
                string ConnectionString = CadenaConecta1;
                SqlConnection cnn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("sp_InsertWizard", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ssql", ssql);
                cnn.Open();
                cmd.ExecuteNonQuery();
                r = 1;   // MessageBox.Show("Los datos fueron insertados correctamente");
                cnn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
        }
        public DataTable esquemaTabla(string tab)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select column_name  from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '{0}'", tab);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();
            }
            catch (Exception e)
            {
                e.Message.ToString();// throw e;
            }
            return dt;
        }
        public DataTable _tipoAnteriorBDD(string user)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select art_tip, art_cve from wStepUser where usuario = '{0}'", user);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();
            }
            catch (Exception e)
            {
                e.Message.ToString();// throw e;
            }
            return dt;
        }


        /* fin de los metodos para la pantalla de AddArt */

        /* metodos para la pantalla de Acciones  */
        public string TablaDatosArt(string artCve, string artTip, string campo, string tabla)// se envia a una tabla el xuarrays, para saber que tabla va en la primer pantalla 
        {
            string datosSelect = "";
            DataTable datosRow = new DataTable();
            DataTable datosColumns = new DataTable();

            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec sp_readWizard '{0}','{1}','{2}'", tabla, artTip.TrimStart(' ').TrimEnd(' '), artCve.TrimStart(' ').TrimEnd(' '));
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            _cmd.ExecuteNonQuery();
            _da.Fill(datosRow);
            _conn.Close();

            SqlConnection conn = new SqlConnection(CadenaConecta1);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("select column_name from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME in ('{0}')", tabla);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            cmd.ExecuteNonQuery();
            da.Fill(datosColumns);
            try
            {
                for (int i = 0; i < datosColumns.Rows.Count; i++)
                {
                    if (datosColumns.Rows[i] != null)
                    {
                        camposDatos[i, 0] = datosColumns.Rows[i][0].ToString();
                        camposDatos[i, 1] = datosRow.Rows[0][i].ToString();
                        if (datosColumns.Rows[i][0].ToString().TrimEnd(' ').Equals("art_cve") == true)
                        {
                            Configuracion.art_cve = datosRow.Rows[0][i].ToString().TrimEnd(' ');
                        }
                        else if (datosColumns.Rows[i][0].ToString().TrimEnd(' ').Equals("id_ultact") == true)
                        {
                            Configuracion.Id_ultactDB = datosRow.Rows[0][i].ToString().TrimEnd(' ');
                        }
                        else if (datosColumns.Rows[i][0].ToString().TrimEnd(' ').Equals("art_tip") == true)
                        {
                            Configuracion.Art_tip = datosRow.Rows[0][i].ToString().TrimEnd(' ');
                        }
                        else if (datosColumns.Rows[i][0].ToString().TrimEnd(' ').Equals("per_hor") == true)
                        {
                            Configuracion.Per_hor = datosRow.Rows[0][i].ToString().TrimEnd(' ');
                        }
                        else if (datosColumns.Rows[i][0].ToString().TrimEnd(' ').Equals("per_ver") == true)
                        {
                            Configuracion.Per_ver = datosRow.Rows[0][i].ToString().TrimEnd(' ');
                        }
                    }
                }
                for (int y = 0; y < datosColumns.Rows.Count; y++)
                {
                    if (campo.Equals(camposDatos[y, 0].ToString()) == true)
                    {
                        datosSelect = camposDatos[y, 1].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            conn.Close();
            return datosSelect;
        }
        public string actualizaBloque(string prg_cve, int secc, int accion)
        {
            string artClave = "";
            try
            {
                prg_cve_act = prg_cve;
                string tabla = "";// "icartphv";//elaborar una funcion que obtenga las tablas que  utiliza una pantalla mediante el xuarrays
                //DataTable dt_tablas = temp_GL_tablas(prg_cve);
                DataTable dt_tablas = temp_GL_tablas2(prg_cve, secc);
                int no_tablas = dt_tablas.Rows.Count;
                int v = 0;
                string sql0 = "";
                string sql1 = "";
                string sql2 = "";
                string sql3 = "";
                string sql4 = "";
                string sql5 = "";
                string sqlFinal = "";
                for (int y = 0; y < no_tablas; y++)
                {
                    tabla = dt_tablas.Rows[y][0].ToString();
                    tabla = tabla.TrimEnd(' ');

                    if (prg_cve_act.Equals("cccatccam1") == true && tabla.Equals("cccatccamar1") == true)
                    {
                        tabla = "cccatcc";
                    }
                    if (tabla != "icartcls")
                    {
                        string ssql = columnasTablasActualiza(tabla, accion);

                        string nomVar = string.Format("sql{0}", y.ToString());

                        if (nomVar.Equals("sql0"))
                        {
                            sql0 = ssql;
                        }
                        else if (nomVar.Equals("sql1"))
                        {
                            sql1 = ssql;
                        }
                        else if (nomVar.Equals("sql2"))
                        {
                            sql2 = ssql;
                        }
                        else if (nomVar.Equals("sql3"))
                        {
                            sql3 = ssql;
                        }
                        else if (nomVar.Equals("sql4"))
                        {
                            sql4 = ssql;
                        }
                        else if (nomVar.Equals("sql5"))
                        {
                            sql5 = ssql;
                        }
                    }
                }

                sqlFinal = sql0 + sql1 + sql2 + sql3 + sql4 + sql5;
                if (sqlFinal != "")
                {
                    v = Insert(sqlFinal);//guardar en la base de datos
                    //v = 1;
                }
                if (v >= 1)
                {
                    v = v + 1;
                }
                if (v >= 2)
                {
                    artClave = Configuracion.ArtCve_aux;
                    string clave = art_cveBDD();
                    if (artClave == null)
                    {
                        Art_cve = clave;
                    }
                    else
                    {
                        if (artClave.Substring(0, 1) == ">")
                        {
                            Art_cve = clave;
                        }
                        else
                        {
                            Art_cve = artClave;
                        }
                    }
                    notificarPaso(ordenFlujo(prg_cve, wflujo), CUser, wflujo, Nom_Art);
                    //Guarda el art_cve en la tabla wStepUser para notificar que ya cumplio con este paso correctamente
                }
                else
                {
                    Art_cve = error;
                    if (Convert.ToInt32(ordenFlujo(prg_cve, wflujo)) > 1)
                    {
                        notificarPaso(ordenFlujo(prg_cve, wflujo), CUser, wflujo,Nom_Art);//graba el error en la base de datos
                    }
                }


                for (int h = 0; h < 100; h++)
                {
                    CamposTabla[h, 0] = "";
                    CamposTabla[h, 1] = "";
                    CamposTabla[h, 2] = "";

                    varSave[h, 0] = "";
                    varSave[h, 1] = "";
                }

            }
            catch (Exception t)
            {
                Art_cve = t.Message.ToString() + "configuracion";
            }
            return Art_cve;
        }
        public string columnasTablasActualiza(string tabla, int accion)//obtiene los nombres de las columnas de la tabla y se las envia a un vector para adjuntarle su valor
        {
            tabla = tabla.TrimEnd(' ');
            nom_tabla = tabla;
            DataTable camposTabla = new DataTable();
            string cl = "";
            string ssql = "";
            string valor = "";
            string update = "";
            string art_tipUp = "";
            string art_cveUp = "";
            string sku_cveUp = "";
            string wSku_cve = "";
            string wArt_cve = "";
            string wArt_tip = "";
            try
            {
                string tipo_cc = tipoCCWizard();
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select COLUMN_NAME, DATA_TYPE, IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '{0}'", tabla);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(camposTabla);
                _conn.Close();

                DataTable temp_sys_var = temp_sysvar(tabla, tipo_cc);

                int camposArreglo = Configuracion.AuxDatos.Length / 2;
                int encontrado = 0;
                for (int i = 0; i < camposTabla.Rows.Count; i++)
                {
                    if (accion == 2)
                    {
                        for (int y = 0; y < camposArreglo; y++)
                        {
                            if (camposTabla.Rows[i][0].ToString().Equals("col_id") == false)
                            {
                                if (Configuracion.AuxDatos[y, 0] != null)
                                {
                                    if (Configuracion.AuxDatos[y, 0] != "")
                                    {
                                        /* asignacion para las variables que se usaran en el update del registro seleccionado */
                                        if (camposTabla.Rows[i][0].ToString().Equals("art_cve") == true && encontrado == 0)
                                        {
                                            wArt_cve = " art_cve = '" + Configuracion.ArtCve_aux + "'";
                                        }
                                        else if (camposTabla.Rows[i][0].ToString().Equals("sku_cve") == true && encontrado == 0)
                                        {
                                            sku_cveUp = Configuracion.ArtCve_aux;
                                            wArt_cve = " sku_cve = '" + sku_cveUp + "'";
                                        }
                                        else if (camposTabla.Rows[i][0].ToString().Equals("art_tip") == true && encontrado == 0)
                                        {
                                            wArt_tip = " art_tip = '" + Configuracion.Art_tip + "'";
                                        }
                                        /*else if (camposTabla.Rows[i][0].ToString().Equals("cc_cve") == true && encontrado == 0 && tabla.Equals("xusubmat") == true)
                                        {
                                            wArt_cve = " cc_cve = '" + Configuracion.ArtCve_aux + "'";
                                        }
                                        else if (camposTabla.Rows[i][0].ToString().Equals("cc_tipo") == true && encontrado == 0 && tabla.Equals("xusubmat") == true)
                                        {
                                            wArt_tip = " cc_tipo = '" + Configuracion.Art_tip + "'";
                                        }*/
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int y = 0; y < camposArreglo; y++)
                        {
                            if (camposTabla.Rows[i][0].ToString().Equals("col_id") == false)
                            {
                                if (Configuracion.AuxDatos[y, 0] != null)
                                {
                                    if (Configuracion.AuxDatos[y, 0] != "")
                                    {
                                        if (camposTabla.Rows[i][0].ToString().Equals(Configuracion.AuxDatos[y, 0].ToString()) == true)
                                        {
                                            if (encontrado == 0)
                                            {
                                            if (camposTabla.Rows[i][0].ToString().Equals("fecha") == true && tabla.Equals("xusubmat") == true)
                                            {
                                                cl = cl + Configuracion.AuxDatos[y, 0].ToString() + " , ";
                                                valor = valor + "'" + Convert.ToDateTime(Configuracion.AuxDatos[y, 1].ToString()).ToString("MM/dd/yyyy") + "',";
                                                update = update + Configuracion.AuxDatos[y, 0].ToString() + " = '" + Convert.ToDateTime(Configuracion.AuxDatos[y, 1].ToString()).ToString("MM/dd/yyyy") + "',";
                                            }
                                            else
                                            {
                                                cl = cl + Configuracion.AuxDatos[y, 0].ToString() + " , ";
                                                valor = valor + "'" + Configuracion.AuxDatos[y, 1].ToString() + "',";
                                                update = update + Configuracion.AuxDatos[y, 0].ToString() + " = '" + Configuracion.AuxDatos[y, 1].ToString() + "',";
                                            }
                                            
                                            
                                            if (camposTabla.Rows[i][0].ToString().Equals("art_cve") == true)
                                            {
                                                wArt_cve = " art_cve = '" + Configuracion.ArtCve_aux + "'";
                                            }
                                            else if (camposTabla.Rows[i][0].ToString().Equals("sku_cve") == true)
                                            {
                                                sku_cveUp = Configuracion.ArtCve_aux;
                                                wArt_cve = " sku_cve = '" + sku_cveUp + "'";
                                            }
                                            else if (camposTabla.Rows[i][0].ToString().Equals("art_tip") == true)
                                            {
                                                wArt_tip = " art_tip = '" + Configuracion.Art_tip + "'";
                                            }
                                            /*else if (camposTabla.Rows[i][0].ToString().Equals("cc_cve") == true && tabla.Equals("xusubmat") == true)
                                            {
                                                wArt_cve = " cc_cve = '" + Configuracion.ArtCve_aux + "'";
                                            }
                                            else if (camposTabla.Rows[i][0].ToString().Equals("cc_tipo") == true && tabla.Equals("xusubmat") == true)
                                            {
                                                wArt_tip = " cc_tipo = '" + Configuracion.Art_tip + "'";
                                            }*/
                                            encontrado = 1;
                                            }
                                        }
                                        else if (camposTabla.Rows[i][0].ToString().Equals("id_ultact") == true && encontrado == 0)
                                        {
                                            cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                            valor = valor + "'" + Configuracion.CUser + "',";
                                            update = update + camposTabla.Rows[i][0].ToString() + " = '" + Configuracion.CUser + "',";
                                            encontrado = 1;
                                        }
                                        else if (camposTabla.Rows[i][0].ToString().Equals("ef_cve") == true && encontrado == 0)
                                        {
                                            cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                            valor = valor + "'" + Configuracion.Ef_cve + "',";
                                            update = update + camposTabla.Rows[i][0].ToString() + " = '" + Configuracion.Ef_cve + "',";
                                            encontrado = 1;
                                        }
                                        else if (camposTabla.Rows[i][0].ToString().Equals("sku_cve") == true && encontrado == 0 && tabla.Equals("xusubmat") == true)
                                        {
                                            cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                            valor = valor + "'" + Configuracion.ArtCve_aux + "',";
                                            update = update + camposTabla.Rows[i][0].ToString() + " = '" + Configuracion.ArtCve_aux + "',";
                                            encontrado = 1;
                                            sku_cveUp = Configuracion.ArtCve_aux;
                                            wArt_cve = " sku_cve = '" + sku_cveUp + "'";
                                        }
                                    }
                                }
                            }
                        }
                        if (encontrado == 0)
                        {
                            for (int x = 0; x < temp_sys_var.Rows.Count; x++)
                            {
                                if (camposTabla.Rows[i][0].ToString().Equals(temp_sys_var.Rows[x][0].ToString().TrimEnd(' ')) == true)
                                {
                                    string dicvar = temp_dic_var(temp_sys_var.Rows[x][2].ToString());
                                    cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                    valor = valor + "'" + dicvar + "',";
                                    encontrado = 1;
                                }
                            }
                            if (encontrado == 0)
                            {
                                if (camposTabla.Rows[i][1].ToString().Equals("nvarchar") == true || camposTabla.Rows[i][1].ToString().Equals("nchar") == true)
                                {
                                    cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                    valor = valor + "'',";
                                }
                                else if (camposTabla.Rows[i][1].ToString().Equals("smallint") == true)
                                {
                                    cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                    valor = valor + 1 + ",";
                                }
                                else if (camposTabla.Rows[i][1].ToString().Equals("bit") == true)
                                {
                                    cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                    valor = valor + 0 + ",";
                                }
                                else if (camposTabla.Rows[i][1].ToString().Equals("datetime") == true || camposTabla.Rows[i][0].ToString().Equals("fec_ultact") == true)
                                {
                                    cl = cl + camposTabla.Rows[i][0].ToString() + " , ";
                                    valor = valor + " getdate(),";
                                    update = update + camposTabla.Rows[i][0].ToString() + " = getdate(),";
                                }
                            }
                        }
                        encontrado = 0;
                    }
                }
                int existe = existeRegistro(Configuracion.ArtCve_aux, tabla, wArt_tip, wArt_cve);
                if (accion == 2)
                {
                    if (tabla == "xusubmat")
                    {
                        ssql = "delete from " + tabla + " where " + wArt_tip + " and " + wArt_cve + " and tipo_prg = 'icartmta'";
                    }
                    else
                    {
                        ssql = "delete from " + tabla + " where " + wArt_tip + " and " + wArt_cve;
                    }
                }
                else
                {
                    if (existe == 0)
                    {
                        cl = cl.Substring(0, cl.Length - 2);
                        string coma = valor.Substring(valor.Trim().Length - 1, 1);
                        if (coma == ",")
                        {
                            valor = valor.Substring(0, valor.Trim().Length - 1);
                        }
                        if (tabla == "xusubmat")
                        {
                            ssql = "insert into " + tabla + " (" + cl + ") values ( " + valor + ") SELECT SCOPE_IDENTITY()";
                        }
                        else
                        {
                            ssql = "insert into " + tabla + " (" + cl + ") values (" + valor + ")";
                        }
                    }
                    else
                    {
                        cl = cl.Substring(0, cl.Length - 2);
                        string coma = update.Substring(update.Trim().Length - 1, 1);
                        if (coma == ",")
                        {
                            update = update.Substring(0, update.Trim().Length - 1);
                        }
                        if (tabla == "xusubmat")
                        {
                            //ssql = "update " + tabla + " set (" + cl + ") values ( " + valor + ") SELECT SCOPE_IDENTITY()";
                            ssql = "update " + tabla + " set " + update + " where " + wArt_tip + " and " + wArt_cve + " and tipo_prg = 'icartmta'";
                        }
                        else
                        {
                            ssql = "update " + tabla + " set " + update + " where " + wArt_tip + " and " + wArt_cve;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();// throw e;
            }
            return ssql;
        }
        public int existeRegistro(string art_cve, string tabla, string tipo, string clave)
        {
            int r = 0;
            try
            {
                string ConnectionString = CadenaConecta1;
                SqlConnection cnn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                if (tabla.Equals("xusubmat") == true)
                {
                    cmd.CommandText = String.Format("select count(*) from {0} where {1} and {2} and tipo_prg = 'icartmta'", tabla, clave, tipo);
                }
                else
                {
                    cmd.CommandText = String.Format("select count(*) from {0} where {1} and {2}", tabla, clave, tipo);
                }
                cnn.Open();
                r = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception e2)
            {
                r = 0;
                e2.Message.ToString();
                error = ">" + e2.Message.ToString();
            }
            return r;
        }
        public string deleteClasifica(string[,] vDatos, string tab, int filas)
        {
            string r = "";
            string ssql = "";
            string columnas = "";
            string valores = "";
            string delete = "";
            /*if (prg_cve_act.Equals("cccatccam1") == true || prg_cve_act.Equals("cccatcck") == true)
            {
                tab = "ccclscc";
            }*/
            DataTable dt = esquemaTabla(tab);
            try
            {
                //for (int i = 0; i < filas; i++)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int q = 0; q < filas; q++)
                    {
                        if (vDatos[q, 0].ToString().Equals(dt.Rows[i][0].ToString()) == true && vDatos[q, 0] != null && dt.Rows[i][0] != null)
                        {
                            vDatos[q, 2] = dt.Rows[i][0].ToString(); // son las columnas de la tabla de la base de datos
                        }
                    }
                }

                for (int y = 0; y <= dt.Rows.Count + 1; y++)
                {
                    if (tab.Equals("ccclscc") == true)
                    {
                        if (vDatos[y, 2] != null)
                        {
                            columnas = columnas + vDatos[y, 2].ToString() + ",";
                            valores = valores + "'" + vDatos[y, 1].ToString() + "',";
                        }
                    }

                    if (tab.Equals("icartcls") == true)
                    {
                        if (vDatos[y, 2] != null)
                        {
                            delete = delete + vDatos[y, 0].ToString() + " = ";
                            if (vDatos[y, 1] == "")
                            {
                                /* emvanezado de la columna */
                                string sysvar = obtieneSysVar(vDatos[y, 0], Prg_cve_act, "", "");
                                string value = obtieneGL_Dic_Var(sysvar, Prg_cve_act);
                                vDatos[y, 1] = value;
                                valores = valores + "'" + vDatos[y, 1].ToString() + "',";
                                delete = delete + "'" + vDatos[y, 1].ToString() + "' and ";
                            }
                            else
                            {
                                /* valor de la columna */
                                valores = valores + "'" + vDatos[y, 1].ToString() + "',";
                                delete = delete + "'" + vDatos[y, 1].ToString() + "' and ";
                            }

                        }
                    }
                }
                delete = delete.Substring(0, delete.Length - 4);

                ssql = "delete from " + tab + " where " + delete;
                int w = Insert(ssql);
                //int w = 1;

                if (w == 1)
                {
                    r = "Artículo Clasificado";
                }
                else
                {
                    r = error;
                }
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return r;
        }

        /* fin de los metodos para la pantalla de Acciones */






        

        public string _auxExecQry(string qry, string prm1)
        {
            //DataTable dt = new DataTable();
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec {0} '{1} ", qry, prm1);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
        public string _auxExecQry2(string qry, string prm1, string prm2)
        {
            //DataTable dt = new DataTable();
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec {0} {1},{2} ", qry, prm1, prm2);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
        public string _auxExecQry3(string qry, string prm1, string prm2, string prm3)
        {
            //DataTable dt = new DataTable();
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec {0} {1},{2},{3} ", qry, prm1, prm2, prm3);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
        public string _auxExecQry4(string qry, string prm1, string prm2, string prm3, string prm4)
        {
            string dt = "";
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4} ", qry, prm1, prm2, prm3, prm4);
                _conn.Open();
                dt = Convert.ToString(_cmd.ExecuteScalar());
                _cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return dt;
        }
        public string _auxExecQry5(string qry, string prm1, string prm2, string prm3, string prm4, string prm5)
        {
            //DataTable dt = new DataTable();
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4}, {5} ", qry, prm1, prm2, prm3, prm4, prm5);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
        public string _auxExecQry6(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6)
        {
            //DataTable dt = new DataTable();
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4}, {5}, {6} ", qry, prm1, prm2, prm3, prm4, prm5, prm6);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
        public string _auxExecQry7(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6, string prm7)
        {
            //DataTable dt = new DataTable();
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4}, {5}, {6}, {7} ", qry, prm1, prm2, prm3, prm4, prm5, prm6, prm7);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
        public string _auxExecQry8(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6, string prm7, string prm8)
        {
            //DataTable dt = new DataTable();
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4}, {5}, {6}, {7},{8} ", qry, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
        public string _auxExecQry9(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6, string prm7, string prm8, string prm9)
        {
            //DataTable dt = new DataTable();
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4}, {5}, {6}, {7},{8},{9} ", qry, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
        public string _auxExecQry10(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6, string prm7, string prm8, string prm9, string prm10)
        {
            //DataTable dt = new DataTable();
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4}, {5}, {6}, {7},{8},{9},{10} ", qry, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9, prm10);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }


        /* llena los combos de la pantalla, a este metodo tengo que pasar los parametros  */
        public DataTable ExecQryTabla(string qry, string parametros)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1} ", qry, parametros);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }
        public DataTable ExecQryTabla2(string qry, string prm1, string prm2)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2} ", qry, prm1, prm2);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }
        public DataTable ExecQryTabla3(string qry, string prm1, string prm2, string prm3)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2},{3} ", qry, prm1, prm2, prm3);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }
        public DataTable ExecQryTabla4(string qry, string prm1, string prm2, string prm3, string prm4)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4} ", qry, prm1, prm2, prm3, prm4);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }
        public DataTable ExecQryTabla5(string qry, string prm1, string prm2, string prm3, string prm4, string prm5)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4},{5} ", qry, prm1, prm2, prm3, prm4, prm5);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }
        public DataTable ExecQryTabla6(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4},{5} ,{6}", qry, prm1, prm2, prm3, prm4, prm5, prm6);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }
        public DataTable ExecQryTabla7(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6, string prm7)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4},{5} ,{6},{7}", qry, prm1, prm2, prm3, prm4, prm5, prm6, prm7);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }
        public DataTable ExecQryTabla8(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6, string prm7, string prm8)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4},{5} ,{6},{7}, {8}", qry, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }
        public DataTable ExecQryTabla9(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6, string prm7, string prm8, string prm9)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4},{5} ,{6},{7}, {8}, {9}", qry, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }
        public DataTable ExecQryTabla10(string qry, string prm1, string prm2, string prm3, string prm4, string prm5, string prm6, string prm7, string prm8, string prm9, string prm10)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec {0} {1},{2},{3}, {4},{5} ,{6},{7}, {8}, {9},{10}", qry, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9, prm10);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
                // throw e;
            } return dt;
        }



        public string nuevoCCCve(string sys_var)
        {
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select max(cc_cve) from cccatcc where cc_tipo = '{0}'", sys_var);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
        public string obtieneGL_Dic_Var(string sysVar, string prg_temp)
        {
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format(" select nombre from xuarrays where prg_cve = '{0}' and array_nom = 'GL_dicvar' and  ren='{1}'", prg_temp, sysVar);
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            dt = dt.TrimStart(' ');
            dt = dt.TrimEnd(' ');
            return dt;
        }
        public string sysvarTempo(string campo, string prg)
        {
            string dt = "";
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta1);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select sys_var from xcdic where tipo_cc = '{0}' and  campo='{1}' and tabla in ({2})", _sysvar(prg), campo, _tabla(prg));
                _conn.Open();
                dt = Convert.ToString(_cmd.ExecuteScalar());
                _cmd.ExecuteNonQuery();
                _conn.Close();


                SqlConnection conn = new SqlConnection(CadenaConecta1);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                if (prg == "icartsu" || prg == "cccatcck" || prg == "cccatccm" || prg == "cccatcce" || prg == "cccatccam1" || prg == "cccatccp")
                {
                    cmd.CommandText = String.Format("select nombre from xuarrays where prg_cve = '{0}' and  array_nom like 'GL_dicvar%' and ren ='{1}'", prg, dt);
                }
                else
                {
                    cmd.CommandText = String.Format("select nombre from xuarrays where prg_cve = '{0}' and  array_nom like 'GL_dicvar' and ren ='{1}'", prg, dt);
                }
                conn.Open();
                dt = Convert.ToString(cmd.ExecuteScalar());
                cmd.ExecuteNonQuery();
                dt.Trim(' ');

                conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return dt;
        }
        public string _sysvar(string prg)
        {
            string tipo_cc;
            SqlConnection conn = new SqlConnection(CadenaConecta1);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("select nombre from xuarrays where array_nom = 'GL_sysvar' and prg_cve  = '{0}'", prg);
            conn.Open();
            tipo_cc = Convert.ToString(cmd.ExecuteScalar());
            cmd.ExecuteNonQuery();
            conn.Close();
            tipo_cc = tipo_cc.Trim(' ');
            return tipo_cc;
        }
        public string _tabla(string prg)
        {
            DataTable dt = new DataTable();
            string tablas = "";
            try
            {
                SqlConnection xconn = new SqlConnection(CadenaConecta1);
                SqlCommand xcmd = new SqlCommand();
                xcmd.Connection = xconn;
                xcmd.CommandType = CommandType.Text;
                xcmd.CommandText = String.Format(" select distinct nombre from xuarrays where array_nom = 'GL_tablas' and prg_cve  = '{0}' and pant = '0' and nombre != '{0}'", prg);
                //xcmd.CommandText = String.Format(" select nombre from xuarrays where array_nom = 'GL_tablas' and prg_cve  = '{0}' and orden = '1'", prg);      
                SqlDataAdapter _da = new SqlDataAdapter(xcmd);
                xconn.Open();
                //ta = Convert.ToString(xcmd.ExecuteScalar());
                xcmd.ExecuteNonQuery();
                _da.Fill(dt);
                xconn.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tablas = tablas + "'" + dt.Rows[i][0].ToString().TrimEnd(' ') + "',";
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();// throw e;
            }
            string coma = tablas.Substring(tablas.Trim().Length - 1, 1);
            if (coma == ",")
            {
                tablas = tablas.Substring(0, tablas.Trim().Length - 1);
            }
            return tablas;
        }
        public string obtieneDato(string campo) // pasa el dato segun el campo y la posicion para el llenado 
        {
            string datos = "";
            string valor = "";
            for (int r = 0; r < tamDatos; r++)
            {
                if (campo.Contains('.') == true)
                {
                    int pos = campo.IndexOf('.');
                    valor = campo.Substring(pos, campo.Length - pos);
                    valor = valor.TrimStart('.');
                }
                else
                {
                    valor = campo;
                }
                int auxDatosQ = auxDatos.Length / auxDatos.Rank;
                for (int q = 0; q < auxDatosQ; q++)
                {
                    if (AuxDatos[q, 0] != null)
                    {
                        if (AuxDatos[q, 0].ToString().Contains(valor) == true && AuxDatos[q, 0].ToString().Equals("") == false)
                        {
                            if ((campo == "art_tip") || (valor == "art_tip"))
                            {
                                datos = ArtTipo(AuxDatos[q, 1].ToString()).TrimStart(' ').TrimEnd(' ');
                                Art_tip = ArtTipo(AuxDatos[q, 1].ToString());
                                r = TamDatos;
                            }
                            else
                            {
                                datos = AuxDatos[q, 1].ToString();
                                if (datos == "Activo")
                                {
                                    datos = "1";
                                    r = TamDatos;
                                }
                                else
                                {
                                    r = TamDatos;
                                }
                            }
                        }
                        else
                        {
                            //datos = "";

                        }
                    }

                }
            }
            return datos;
        }
        public string ArtTipo(string nombre)
        {
            string dt;
            SqlConnection _conn = new SqlConnection(CadenaConecta1);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select art_tip from icarttip where art_tip = '{0}'", nombre.TrimStart(' ').TrimEnd(' '));
            //SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            dt = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            //_da.Fill(dt);
            _conn.Close();
            return dt;
        }
    }
}