using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Web.Services;
using AccesoDatos;

namespace Diseño
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        Configuracion _con = new Configuracion();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSearch.Attributes.Add("autocomplete", "off");
        }

        [WebMethod]
        public static string[] GetCustomers(string prefix, string art_tipo, string tipo)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Configuracion.CadenaConecta1;

                using (SqlCommand cmd = new SqlCommand())
                {
                    DataTable dt = new DataTable();
                    SqlCommand _cmd = new SqlCommand();
                    _cmd.Connection = conn;
                    _cmd.CommandType = CommandType.Text;
                    if (/*tipo == null && tipo == "" && */tipo.Equals("Seleccionar una opción") == true)
                    {
                        _cmd.CommandText = String.Format("SELECT art_tip,inf_cve FROM icartinf WHERE inf_nom is not null and art_tip = '{0}'", art_tipo.Replace("(","").TrimStart(' ').TrimEnd(' '));
                    }
                    else
                    {
                        _cmd.CommandText = String.Format("SELECT art_tip,inf_cve FROM icartinf WHERE inf_nom is not null and art_tip = '{0}' and inf_nom like '{1}'", art_tipo.Replace("(", "").TrimStart(' ').TrimEnd(' '), tipo.TrimStart(' ').TrimEnd(' '));
                    }
                    //_cmd.CommandText = String.Format("SELECT art_tip,inf_cve FROM icartinf WHERE inf_nom is not null and art_tip like '{0}' and inf_nom like '{1}'", art_tipo, tipo);
                    SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                    conn.Open();
                    //conn = Convert.ToString(_cmd.ExecuteScalar());
                    _cmd.ExecuteNonQuery();
                    _da.Fill(dt);
                    conn.Close();

                    if (tipo.Equals("Seleccionar una opción") == true)
                    {
                        string art_tip2 = "";
                        cmd.CommandText = "select nombre as nom_comer, art_cve from icartesp where nombre like '%' + @SearchText + '%' and art_tip like '%' + @art_tip + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefix.TrimStart(' ').TrimEnd(' '));
                        if (dt.Rows.Count == 0)
                        {
                            art_tip2 = art_tipo;
                        }
                        else
                        {
                            art_tip2 = dt.Rows[0][0].ToString().TrimStart(' ').TrimEnd(' ');
                        }
                        
                        cmd.Parameters.AddWithValue("@art_tip", art_tip2);
                    }
                    else
                    {
                        cmd.CommandText = "select nombre as nom_comer, art_cve from icartesp where nombre like '%' + @SearchText + '%' and art_cve like '%' + @tipo + '%' and art_tip like '%' + @art_tip + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefix.TrimEnd(' '));
                        string art_tip2 = dt.Rows[0][0].ToString().TrimEnd(' ');
                        cmd.Parameters.AddWithValue("@art_tip", art_tip2);
                        string tipo2 = dt.Rows[0][1].ToString().TrimEnd(' ');
                        cmd.Parameters.AddWithValue("@tipo", tipo2);
                    }
                    
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}", sdr["nom_comer"].ToString().Replace("-","_"), sdr["art_cve"]));
                        }
                    }
                    conn.Close();
                }
            }
            return customers.ToArray();
        }
    }
}