using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicaNegocio
{
   public class variablesGuardar
    {
        static int index;
        // se activa si apreto el boton agregar clasificacion. el boton next estara bloqueado mientras no se active esta variable
        public static int Index
        {
            get { return variablesGuardar.index; }
            set { variablesGuardar.index = value; }
        }
        static string ban_clasifica;
// se activa si apreto el boton agregar clasificacion. el boton next estara bloqueado mientras no se active esta variable
        public static string Ban_clasifica
        {
            get { return variablesGuardar.ban_clasifica; }
            set { variablesGuardar.ban_clasifica = value; }
        }

        static string ef_cve;

        public static string Ef_cve
        {
            get { return variablesGuardar.ef_cve; }
            set { variablesGuardar.ef_cve = value; }
        }

        static string urlInicial;

        public static string UrlInicial
        {
            get { return variablesGuardar.urlInicial; }
            set { variablesGuardar.urlInicial = value; }
        }

        static string art_tipBDD;

        public static string Art_tipBDD
        {
            get { return variablesGuardar.art_tipBDD; }
            set { variablesGuardar.art_tipBDD = value; }
        }

        static string art_tipVal;
        public static string Art_tipVal
        {
            get { return variablesGuardar.art_tipVal; }
            set { variablesGuardar.art_tipVal = value; }
        }


        static string articuloClave;
        public static string ArticuloClave
        {
            get { return variablesGuardar.articuloClave; }
            set { variablesGuardar.articuloClave = value; }
        }

        static int ban_borra;
        public static int Ban_borra
        {
            get { return variablesGuardar.ban_borra; }
            set { variablesGuardar.ban_borra = value; }
        }

        static int posicionControl;
        public static int PosicionControl
        {
            get { return variablesGuardar.posicionControl; }
            set { variablesGuardar.posicionControl = value; }
        }

        static int posicionLabel;
        public static int PosicionLabel
        {
            get { return variablesGuardar.posicionLabel; }
            set { variablesGuardar.posicionLabel = value; }
        }
       
        static string aux_art_tip;
        public static string Aux_art_tip
        {
            get { return variablesGuardar.aux_art_tip; }
            set { variablesGuardar.aux_art_tip = value; }
        }

       static string aux_art_nom;
       public static string Aux_art_nom
       {
           get { return variablesGuardar.aux_art_nom; }
           set { variablesGuardar.aux_art_nom = value; }
       }

       static int nDatos;
       public static int NDatos
       {
           get { return variablesGuardar.nDatos; }
           set { variablesGuardar.nDatos = value; }
       }

        static string[,] vDatos = new string[100,2];
        public static string[,] VDatos
        {
            get { return variablesGuardar.vDatos; }
            set { variablesGuardar.vDatos = value; }
        }

        static string[,] vCls = new string[100, 3];
        public static string[,] VCls
        {
            get { return variablesGuardar.vCls; }
            set { variablesGuardar.vCls = value; }
        }

        static DataTable clsValor;
        public static DataTable ClsValor
        {
            get { return variablesGuardar.clsValor; }
            set { variablesGuardar.clsValor = value; }
        }

        static int j;
        public static int J
        {
            get { return variablesGuardar.j; }
            set { variablesGuardar.j = value; }
        }

        static string art_nom;
        public static string Art_nom
        {
            get { return variablesGuardar.art_nom; }
            set { variablesGuardar.art_nom = value; }
        }

        static string art_tip;
        public static string Art_tip
        {
            get { return variablesGuardar.art_tip; }
            set { variablesGuardar.art_tip = value; }
        }

        static string art_cve;
        public static string Art_cve
        {
            get { return variablesGuardar.art_cve; }
            set { variablesGuardar.art_cve = value; }
        }

        static string prg_cveInicial;
        public static string Prg_cveInicial
        {
            get { return variablesGuardar.prg_cveInicial; }
            set { variablesGuardar.prg_cveInicial = value; }
        }

        static string prg_cve;
        public static string Prg_cve
        {
            get { return variablesGuardar.prg_cve; }
            set { variablesGuardar.prg_cve = value; }
        }

        static string tip_cc;
        public static string Tip_cc
        {
            get { return variablesGuardar.tip_cc; }
            set { variablesGuardar.tip_cc = value; }
        }

        static string user;
        public static string User
        {
            get { return variablesGuardar.user; }
            set { variablesGuardar.user = value; }
        }

        static string userBD;
        public static string UserBD
        {
            get { return variablesGuardar.userBD; }
            set { variablesGuardar.userBD = value; }
        }

        static string balin;
        public static string Balin
        {
            get { return variablesGuardar.balin; }
            set { variablesGuardar.balin = value; }
        }

        static string artCve_aux;
        public static string ArtCve_aux
        {
            get { return variablesGuardar.artCve_aux; }
            set { variablesGuardar.artCve_aux = value; }
        }

        static int comboActual;
        public static int ComboActual
        {
            get { return variablesGuardar.comboActual; }
            set { variablesGuardar.comboActual = value; }
        }

        static string art_cveFin;
        public static string Art_cveFin
        {
            get { return variablesGuardar.art_cveFin; }
            set { variablesGuardar.art_cveFin = value; }
        }
        static string sku_cveFin;
        public static string Sku_cveFin
        {
            get { return variablesGuardar.sku_cveFin; }
            set { variablesGuardar.sku_cveFin = value; }
        }

        static string id_ultact;
        public static string Id_ultact
        {
            get { return variablesGuardar.id_ultact; }
            set { variablesGuardar.id_ultact = value; }
        }
        

        static string _flujo;
        public static string Flujo
        {
            get { return variablesGuardar._flujo; }
            set { variablesGuardar._flujo = value; }
        }

        static string subcls_cve;
        public static string Subcls_cve
        {
            get { return variablesGuardar.subcls_cve; }
            set { variablesGuardar.subcls_cve = value; }
        }

        static string cls_num;
        public static string Cls_num
        {
            get { return variablesGuardar.cls_num; }
            set { variablesGuardar.cls_num = value; }
        }

        static string art_tipFinal;
        public static string Art_tipFinal
        {
            get { return variablesGuardar.art_tipFinal; }
            set { variablesGuardar.art_tipFinal = value; }
        }

        static string artClaveBDD;
        public static string ArtClaveBDD
        {
            get { return variablesGuardar.artClaveBDD; }
            set { variablesGuardar.artClaveBDD = value; }
        }

        static int alto;
        public static int Alto
        {
            get { return variablesGuardar.alto; }
            set { variablesGuardar.alto = value; }
        }

        static int ancho;
        public static int Ancho
        {
            get { return variablesGuardar.ancho; }
            set { variablesGuardar.ancho = value; }
        }

        static string[,] vecPrg_cve = new string[10,1];
        public static string[,] VecPrg_cve
        {
            get { return variablesGuardar.vecPrg_cve; }
            set { variablesGuardar.vecPrg_cve = value; }
        }

        static int num_btn;
        public static int Num_btn
        {
            get { return variablesGuardar.num_btn; }
            set { variablesGuardar.num_btn = value; }
        }
        static string dedicado;
        public static string Dedicado
        {
            get { return variablesGuardar.dedicado; }
            set { variablesGuardar.dedicado = value; }
        }

        static string tipo_moneda;
        public static string Tipo_moneda
        {
            get { return variablesGuardar.tipo_moneda; }
            set { variablesGuardar.tipo_moneda = value; }
        }

        static string edo_cve;
        public static string Edo_cve
        {
            get { return variablesGuardar.edo_cve; }
            set { variablesGuardar.edo_cve = value; }
        }

        static string cd_cve;
        public static string Cd_cve
        {
            get { return variablesGuardar.cd_cve; }
            set { variablesGuardar.cd_cve = value; }
        }

        static string mpio_cve;
        public static string Mpio_cve
        {
            get { return variablesGuardar.mpio_cve; }
            set { variablesGuardar.mpio_cve = value; }
        }

        static string cp_cve;
        public static string Cp_cve
        {
            get { return variablesGuardar.cp_cve; }
            set { variablesGuardar.cp_cve = value; }
        }

        static string artCveAuto;
        public static string ArtCveAuto
        {
            get { return variablesGuardar.artCveAuto; }
            set { variablesGuardar.artCveAuto = value; }
        }
        static string artTipAuto;

        public static string ArtTipAuto
        {
            get { return variablesGuardar.artTipAuto; }
            set { variablesGuardar.artTipAuto = value; }
        }

        static string nomArtAuto;
        public static string NomArtAuto
        {
            get { return variablesGuardar.nomArtAuto; }
            set { variablesGuardar.nomArtAuto = value; }
        }

        static string cveAuto;
        public static string CveAuto
        {
            get { return variablesGuardar.cveAuto; }
            set { variablesGuardar.cveAuto = value; }
        }

        static string seecionVisible;
        //se cambiara el valor de esta variable conforme el usuario vaya avanzando en la captura de informacion 
        public static string SeccionVisible
        {
            get { return variablesGuardar.seecionVisible; }
            set { variablesGuardar.seecionVisible = value; }
        }
        static int errorArttip;
        // almacenara un bandera con si se genera un error al guardar la informacion
        public static int ErrorArttip
        {
            get { return variablesGuardar.errorArttip; }
            set { variablesGuardar.errorArttip = value; }
        }
        static int numBloque;
        public static int NumBloque
        {
            get { return variablesGuardar.numBloque; }
            set { variablesGuardar.numBloque = value; }
        }

        static int cambioCombo;
        public static int CambioCombo
        {
            get { return variablesGuardar.cambioCombo; }
            set { variablesGuardar.cambioCombo = value; }
        }
        static int errorInsert;
        public static int ErrorInsert
        {
            get { return variablesGuardar.errorInsert; }
            set { variablesGuardar.errorInsert = value; }
        }
        static string newCombos;
        public static string NewCombos
        {
            get { return variablesGuardar.newCombos; }
            set { variablesGuardar.newCombos = value; }
        }
        static int numSecciones;
        public static int NumSecciones
        {
            get { return variablesGuardar.numSecciones; }
            set { variablesGuardar.numSecciones = value; }
        }
        static string sp_qtiparta;
        public static string Sp_qtiparta
        {
            get { return variablesGuardar.sp_qtiparta; }
            set { variablesGuardar.sp_qtiparta = value; }
        }
        static string qtipartaTip;
        public static string QtipartaTip
        {
            get { return variablesGuardar.qtipartaTip; }
            set { variablesGuardar.qtipartaTip = value; }
        }

        static string[,] prg_cves = new string[100, 1];
        public static string[,] Prg_cves
        {
            get { return variablesGuardar.prg_cves; }
            set { variablesGuardar.prg_cves = value; }
        }
        
       static string[,] tipo_ccs = new string[100, 1];
        public static string[,] Tipo_ccs
        {
            get { return variablesGuardar.tipo_ccs; }
            set { variablesGuardar.tipo_ccs = value; }
        }

        static string nom_Art;
        public static string Nom_Art
        {
            get { return variablesGuardar.nom_Art; }
            set { variablesGuardar.nom_Art = value; }
        }
        static string nom_Flujo;
        public static string Nom_Flujo
        {
            get { return variablesGuardar.nom_Flujo; }
            set { variablesGuardar.nom_Flujo = value; }
        }

        /* utilizado en los combos de cls */
        static string prm1;
        public static string Prm1
        {
            get { return variablesGuardar.prm1; }
            set { variablesGuardar.prm1 = value; }
        }
        static string rfc;
        public static string Rfc
        {
            get { return variablesGuardar.rfc; }
            set { variablesGuardar.rfc = value; }
        }
    }
}
