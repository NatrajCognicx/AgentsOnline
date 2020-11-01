using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

using OneLogin.Saml;

public partial class LoginOkta : System.Web.UI.Page
{
    BLLLogin objBLLLogin = new BLLLogin();
    clsUtilities objclsUtilities = new clsUtilities();
    BLLAgentRegistration objBLLAgentRegistration = new BLLAgentRegistration();

    string strCompany = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        AccountSettings accountSettings = new AccountSettings();
        if (Request.Form["SAMLResponse"] == null)
        {
            string IDPTargetURL;
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                IDPTargetURL = ConfigurationManager.AppSettings["idp_sso_target_logouturl"].ToString();
                //OneLogin.Saml.AuthRequest req = new AuthRequest(new AppSettings(), accountSettings);
                Response.Redirect(IDPTargetURL); //+ "?SAMLRequest=" + Server.UrlEncode(req.GetRequest(AuthRequest.AuthRequestFormat.Base64))
                return;
            }
            else
            {
                IDPTargetURL = ConfigurationManager.AppSettings["idp_sso_target_url"].ToString();
                //OneLogin.Saml.AuthRequest req = new AuthRequest(new AppSettings(), accountSettings);
                Response.Redirect(IDPTargetURL); //+ "?SAMLRequest=" + Server.UrlEncode(req.GetRequest(AuthRequest.AuthRequestFormat.Base64))
                return;
            }

            //Response.Write("Invalid Request");
            //return;
        }
        // replace with an instance of the users account.

        OneLogin.Saml.Response samlResponse = new Response(accountSettings);
        samlResponse.LoadXmlFromBase64(Request.Form["SAMLResponse"]);

        if (samlResponse.IsValid())
        {
            //Response.Write("OK!");
            //Response.Write(samlResponse.GetNameID());
            fnROLogin(samlResponse.GetNameID());
        }
        else
        {
            Response.Write("Failed");
        }
    }

    private string GetStyleName(string strClientID = "")
    {
        // *** Danny 12/08/2018 Get Client Style or get Application Default style
        try
        {
            if (strClientID == null)
                strClientID = "924065660726315";
            string strStyle = "";
            strStyle = objclsUtilities.ExecuteQueryReturnSingleValue("select StylesheetName from TB_Clientstyles where RandomNumber=" + strClientID).ToString();
            if (strStyle != null)
            {
                if (strStyle.Trim().Length == 0 | strStyle.Trim() == "0")
                    strStyle = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=58").ToString();
            }
            else
                strStyle = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=58").ToString();
            return strStyle;
        }
        catch
        {
            return "style-style1.css";
        }
    }


    private void fnROLogin(string lUserEmail)
    {
        Session.Clear();
        Session["sRequestId"] = null;
        Session["sEditRequestId"] = null;
        //CheckUnderConstruction();
        string strAbsoluteUri = Page.Request.Url.AbsoluteUri.ToString().Replace("loginokta.aspx", "login.aspx") + "?ro=1";
        strAbsoluteUri = ConfigurationManager.AppSettings["idp_sso_target_logouturl"];

        Session["sAbsoluteUrl"] = strAbsoluteUri;
        Session["strTheme"] = "";
        string strTheme = "";

        if (Request.QueryString["comp"] != null)
        {
            strCompany = Request.QueryString["comp"];
            Session["sAgentCompany"] = strCompany;
            if (strCompany == "924065660726315")
            {
                Session["sDivCode"] = "01";
                strTheme = "css/" + GetStyleName(strCompany);
            }
            else if (strCompany == "675558760549078")
            {
                Session["sDivCode"] = "02";
                strTheme = "css/style-style3.css";
            }
            else
            {
                Session["sDivCode"] = "0";
                strTheme = "css/" + GetStyleName(strCompany);
            }
        }
        else
        {
            strCompany = "924065660726315"; // Default company 
            Session["sAgentCompany"] = strCompany;
            Session["sDivCode"] = "01";
            strTheme = "css/" + GetStyleName(strCompany);
        }
        Session["strTheme"] = strTheme;


        if (Session["sobjResParam"] == null)
        {
            ReservationParameters objResParam = new ReservationParameters();
            objResParam.AbsoluteUrl = strAbsoluteUri;
            objResParam.AgentCompany = strCompany;
            objResParam.DivCode = (string) Session["sDivCode"];
            objResParam.CssTheme = strTheme;
            Session["sobjResParam"] = objResParam;
        }
        else
        {
            ReservationParameters objResParam = new ReservationParameters();
            objResParam = (ReservationParameters) Session["sobjResParam"];
            objResParam.AbsoluteUrl = strAbsoluteUri;
            objResParam.AgentCompany = strCompany;
            objResParam.DivCode = (string) Session["sDivCode"];
            objResParam.CssTheme = strTheme;
            Session["sobjResParam"] = objResParam;
        }

        //LoadLoginPageFields();
        string lUserName;
        lUserName = objclsUtilities.ExecuteQueryReturnStringValue("select top 1 UserCode from usermaster where usemail='" + lUserEmail + "'");
        if (lUserName == null)
        {
            Response.Write("The account is not authorized to login");
            return;
        }
        if (lUserName.Trim() == "")
        {
            Response.Write("The account is not authorized to login");
            return;
        }
        //string lPassword;
        //lPassword = objclsUtilities.ExecuteQueryReturnStringValue("select dbo.pwddecript(userpwd) from usermaster where usercode='" + lUserName + "'");
        //if (1==2) { //(lPassword == null) {
        //    Response.Write("The username is incorrect.");
        //}
        //else
        //{
            if (objBLLLogin.ValidateROUser(lUserName.Trim()) == true)
            {

                Session.Add("sLoginType", "RO");
                ReservationParameters objResParam = new ReservationParameters();
                if (Session["sobjResParam"] != null)
                    objResParam = (ReservationParameters)Session["sobjResParam"];
                objResParam.LoginType = "RO";
                Session["sLang"] = "en-us";
                DataTable objDataTable;
                objDataTable = objBLLLogin.LoadLoginPageSessionFields((string)Session["sAgentCompany"]);
                if (objDataTable.Rows.Count > 0)
                {
                    Session.Add("sAgentCode", objDataTable.Rows[0]["agentcode"].ToString());
                    Session.Add("sCurrencyCode", objDataTable.Rows[0]["currcode"].ToString());
                    Session.Add("sCountryCode", objDataTable.Rows[0]["ctrycode"].ToString());
                    Session.Add("GlobalUserName", lUserName.Trim());
                    objResParam.AgentCode = objDataTable.Rows[0]["agentcode"].ToString();
                    objResParam.GlobalUserName = lUserName.Trim();
                }
                else
                {
                }

                GetReservationParamValues();

                string LoginIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                objResParam.LoginIp = LoginIp;
                string strIpLocation = "";
                objResParam.LoginIpLocationName = strIpLocation;
                string strAgentCode = (string)Session["sAgentCode"].ToString();
                BLLHotelSearch objBLLHotelSearch = new BLLHotelSearch();
                objResParam.Cumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode).ToString();
                objResParam.WhiteLabel = objBLLHotelSearch.FindWhiteLabel(strAgentCode).ToString();
                Session["sobjResParam"] = objResParam;

                // objclsUtilities.WriteErrorLog("Redirect Before " & Date.Now)

                Response.Redirect("Home.aspx", false);
            }
            else
            {
                Response.Write("The account is not authorized to login");
            }
        //}
    }

    private void GetReservationParamValues()
    {
        string strParams = "514,2009";
        DataTable objReservationParm;
        objReservationParm = objBLLLogin.GetReservationParamValues(strParams);
        if ( objReservationParm != null)
        {
            if (objReservationParm.Rows.Count > 0)
            {
                ReservationParameters objResParam = new ReservationParameters();
                if (Session["sobjResParam"] != null)
                    objResParam = (ReservationParameters)Session["sobjResParam"];


                for (int i = 0; i <= objReservationParm.Rows.Count - 1; i++)
                {
                    if (objReservationParm.Rows[i]["param_id"].ToString() == "514")
                        objResParam.NoOfNightLimit =   objReservationParm.Rows[i]["option_selected"].ToString();
                    if (objReservationParm.Rows[i]["param_id"].ToString() == "2009")
                        objResParam.ChildAgeLimit = objReservationParm.Rows[i]["option_selected"].ToString();
                }
                Session["sobjResParam"] = objResParam;
            }
        }
    }
}