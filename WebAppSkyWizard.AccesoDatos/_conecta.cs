using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Data.OleDb;
using System.Data.Odbc;
using AccesoDatos;
//using System.Web.Script.Serialization;
using System.Reflection;
using System.Configuration;

namespace AccesoDatos
{
    public class _conecta
    {
        static string CadenaConecta = Configuracion.CadenaConecta1; //@"Data Source=skyhdev3;Initial Catalog=develop;User ID=soludin_develop;Password=dinamico20";
        DataTable TablaMaster = new DataTable();
        DataTable tabOrden = new DataTable();

        public DataTable llenaCombo(string var)
        {
            string prg_cve = "";
            SqlConnection _conn = new SqlConnection(CadenaConecta);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select prg_cve from WizardPasos where orden = '1' and paso_cve = '{0}'", var);
            _conn.Open();
            prg_cve = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            _conn.Close();

            DataTable orderTable = new DataTable();
            SqlConnection _conn2 = new SqlConnection(CadenaConecta);
            SqlCommand _cmd2 = new SqlCommand();
            _cmd2.Connection = _conn2;
            _cmd2.CommandType = CommandType.Text;
            if (prg_cve.Contains("cccat") == true)
            {
                _cmd2.CommandText = String.Format("exec qcomtipcc1");
            }
            else
            {
                _cmd2.CommandText = String.Format("exec qcomartip11 '{0}'", prg_cve);
            }

            SqlDataAdapter _da = new SqlDataAdapter(_cmd2);
            _conn2.Open();
            //artCveFin = Convert.ToString(_cmd.ExecuteScalar());
            _cmd2.ExecuteNonQuery();
            _da.Fill(orderTable);
            _conn2.Close();
            return orderTable;
        }

        public DataTable llenaSubCombo(string art_tip, string ef_cve)
        {

            DataTable orderTable = new DataTable();
            SqlConnection _conn2 = new SqlConnection(CadenaConecta);
            SqlCommand _cmd2 = new SqlCommand();
            _cmd2.Connection = _conn2;
            _cmd2.CommandType = CommandType.Text;
            _cmd2.CommandText = String.Format("select inf_nom, rtrim(inf_cve) as inf_cve from icartinf where art_tip = '{0}' and catalogo_cve = '{1}' and charindex ('NO USAR',inf_nom ) = 0 and isnull(swinf_vigente,0) = 0", art_tip, ef_cve);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd2);
            _conn2.Open();
            //artCveFin = Convert.ToString(_cmd.ExecuteScalar());
            _cmd2.ExecuteNonQuery();
            _da.Fill(orderTable);
            _conn2.Close();
            return orderTable;
        }

        public DataTable llenaTabla(string art_tip, string art_cve, string nom_comer, int opc)
        {

            DataTable orderTable = new DataTable();
            DataColumn workCol =
                orderTable.Columns.Add("Tipo", typeof(String));
            orderTable.Columns.Add("Nombre", typeof(String));
            orderTable.Columns.Add("Clave", typeof(String));
            orderTable.Columns.Add("Catalogo", typeof(String));
            orderTable.Columns.Add("Origen", typeof(String));
            orderTable.Columns.Add("Frac_Arancelaria", typeof(String));
            orderTable.Columns.Add("Forma_Entrega", typeof(String));
            orderTable.Columns.Add("UM_uso", typeof(String));
            orderTable.Columns.Add("UM_Alternativa", typeof(String));
            orderTable.Columns.Add("FactorConversion", typeof(String));
            try
            {
                SqlConnection _conn = new SqlConnection(CadenaConecta);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("exec qwiztablas '{0}','{1}','{2}','{3}'", art_tip.TrimEnd(' '), art_cve.TrimEnd(' '), nom_comer, opc);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(orderTable);
                _conn.Close();
            }
            catch (Exception t)
            {
                t.Message.ToString();
            }
            return orderTable;
        }

        int ban = 0;
    }
  
}
 


