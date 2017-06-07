using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using Diseño.Clases;
using System.Windows;
using LogicaNegocio;
//using System.Windows.Forms;



namespace Diseño
{
    
    public partial class Default : System.Web.UI.Page
    {
        Logica _con = new Logica();

        string _step;
        string _flujo;
        string _flujo2;
        string _user;
        string ef_cve;
        string ss_cve;
        string wprg_cve;
        string user_cve;
        string prg_cve;
        string nombre;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Response.Write(conexion.servidor());
                Server.ScriptTimeout = 3600;
                /*
                    cadena que e debe de generar en la aplicacion publicada
                    http://skyhdev3/WizardDev/default.aspx?ef_cve=001&ss_cve=L&user_cve=FGV&nombre=mnuicartsc&prg_cve=mnuicartsc
                */
                //////////////////////////////////////////////////
                //Comentar para publicar y subir al server
                
                Session["usr"] = "FGV";
                Session["ef_cve"] = "001";
                _user = Session["usr"].ToString();
                ef_cve = Session["ef_cve"].ToString();
                //nombre = "";
                //nombre = "Cat";//*NuevoWeb_obj
                //nombre = "Mta";//*NuevoWeb_obj
                //nombre = "Qui";//*NuevoWeb_obj
                //nombre = "Col";//*NuevoWeb_obj
                //nombre = "Con";//*NuevoWeb_obj
                //nombre = "Fib";//*NuevoWeb_obj
                //nombre = "icartsh";//*NuevoWeb_obj
                //nombre = "cccatcck";//*NuevoWeb_obj
                //nombre = "cccatccm"; //*NuevoWeb_obj
                //nombre = "cccatcce"; //*NuevoWeb_obj
                //nombre = "CatAct"; //*NuevoWeb_obj
                //nombre = "cccatccam1"; // falta obtener los datos de la ventana emergente

                //nombre = "cccatccp";// no puedo dar de alta un registro en soludin 
                nombre = "MntRef";//me falta llenar el combo de Se usa en: (el segundo) 
                //nombre = "ccinfad";
                //nombre = "CorMue";

                DataTable dtStep = _con.StepUser("mnu" + nombre, _user);
                string url = HttpContext.Current.Request.Url.PathAndQuery;
                variablesGuardar.UrlInicial = url;
                //Session["urlInicial" + _user] = url;
                Response.Write("<br/> " + HttpContext.Current.Request.Url.Host);
                
                /////////////////////////////////////////////////

                
                /////////////////////////////////////////////////
                /*Descomentar estas lineas publicar y subir al servidor*/
                /*
                ef_cve = Request.Params["ef_cve"].ToString();
                ss_cve = Request.Params["ss_cve"].ToString();
                user_cve = Request.Params["user_cve"].ToString();
                nombre = Request.Params["nombre"].ToString();
                prg_cve = Request.Params["prg_cve"].ToString();
                
                Session["ef_cve"] = ef_cve;
                Session["ss_cve"] = ss_cve;
                Session["usr"] = user_cve;
                Session["flujo"] = nombre;
                Session["wprg_cve"] = prg_cve;

                string url = HttpContext.Current.Request.Url.PathAndQuery.TrimStart(' ').TrimEnd(' ').Replace("'","").Replace(" ","");
                variablesGuardar.UrlInicial = url;
                //Session["urlInicial" + _user] = url;

                _user = user_cve.TrimStart(' ').TrimEnd(' ').Replace(" ", "");
                //_flujo = nombre;
                _flujo = prg_cve.TrimStart(' ').TrimEnd(' ').Replace(" ", "");
                
                //_flujo = Session["flujo"].ToString();
                DataTable dtStep = _con.StepUser(_flujo, _user);//obtiene las pantallas de acuerdo al flujo paramentro
                */
                /////////////////////////////////////////////////

                _flujo2 = dtStep.Rows[0][1].ToString().TrimStart(' ').TrimEnd(' ').Replace(" ", "");
                _step = dtStep.Rows[0][2].ToString().TrimStart(' ').TrimEnd(' ').Replace(" ", "");
                
                if (_flujo2.Equals(variablesGuardar.Nom_Flujo) == false)
                {
                    AutoBusqueda.ArtClave = null;
                    AutoBusqueda.ArtTipo = null;
                    AutoBusqueda.ArtNombre = null;
                    variablesGuardar.ArtCveAuto = null;
                    variablesGuardar.CveAuto = null;
                    variablesGuardar.NomArtAuto = null;
                    variablesGuardar.ArtTipAuto = null;
                }
                DataTable dtpasos = _con.defaul(_flujo2);//obtiene el paso en el cual se quedo
                string busca = "";

                for (int i = 0; i < dtpasos.Rows.Count; i++)
                {
                    if (dtpasos.Rows[i][1].ToString().Equals("0") == true)
                    {
                        busca = dtpasos.Rows[i][2].ToString().TrimStart(' ').TrimEnd(' ').Replace(" ", "");
                    }
                }
                //pregunta si el usuario tiene habilitada la opcion de busqueda de articulos
                if ((busca != "") && (busca.Substring(busca.Length - 5, 5) == ".aspx"))
                {
                    //envia  el paso en el que se quedo el usuario y el flujo en el que interactuo por ultima ves.
                    Response.Redirect(busca.TrimStart(' ').TrimEnd(' ') + "?step=" + _step.TrimStart(' ').TrimEnd(' ') + "&flujo=" + _flujo2.TrimStart(' ').TrimEnd(' ') + "&u=" + _user.TrimStart(' ').TrimEnd(' ') + "&ef_cve=" + ef_cve.TrimStart(' ').TrimEnd(' '),false);
                }
                else
                {
                    //En caso de no tener acceso a la pantalla de busqueda, es dirigido a la seccion de alta de articulo, si es que lo tiene habilitado en el flujo o mediante configuracion en tablas.
                    Response.Redirect("AddArt.aspx?step=" + _step + "&flujo=" + _flujo2 + "&u=" + _user + "&ef_cve=" + ef_cve,false);
                }
            }
            catch (Exception t)
            {
                RadTicker1.AutoStart = true;
                t.Message.ToString();
                Response.Write(t.ToString());
            }
        }
    }
}