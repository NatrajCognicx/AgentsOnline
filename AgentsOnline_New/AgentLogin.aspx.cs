using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Net;
using System.Text;
using System.IO;

using System.Net.Cache;

using System.Web.Script.Serialization;
using System.Configuration;
using System.Data;
using System.Web.Security;


public partial class AgentLogin : System.Web.UI.Page
{
    public string gscode = "", gsstate = "", gssession_state = "", gs_accesstoken = "", gs_refreshtoekn = "", gs_email = "";

    BLLLogin objBLLLogin = new BLLLogin();
    clsUtilities objclsUtilities = new clsUtilities();
    BLLAgentRegistration objBLLAgentRegistration = new BLLAgentRegistration();

    string strCompany = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (Request.QueryString["code"] == null)
            {
                Session.Clear();
                string lRedirectURL = "", lAccessURL = "", lCode = "", lURL;
                lAccessURL = ConfigurationManager.AppSettings["IDP_AuthorizeURL"].ToString();
                lRedirectURL = ConfigurationManager.AppSettings["IDP_RedirectURL"].ToString();
                lCode = ConfigurationManager.AppSettings["IDP_Code"].ToString();
                lURL = lAccessURL + "?client_id=" + Uri.EscapeDataString(lCode) + "&redirect_uri=" + Uri.EscapeDataString(lRedirectURL) + "&response_type=code&scope=" + Uri.EscapeDataString("openid profile email");
                Response.Redirect(lURL);
            }
            else
            {
                gscode = Request.QueryString["code"];
                Session["idp_accesscode"] = gscode;

                if (Request.QueryString["state"] != null)
                {
                    gsstate = Request.QueryString["state"];
                    Session["idp_state"] = gsstate;
                }
                if (Request.QueryString["session_state"] != null)
                {
                    gssession_state = Request.QueryString["session_state"];
                    Session["idp_session_state"] = gssession_state;
                }
                if (PostDataForToken() == true)
                {
                    if (PostDataForLoginUser(gs_accesstoken) == true)
                    {
                        //if (objBLLLogin.IDPPostDataForRefreshToken() == true)
                        //{
                        fnLogin(gs_email);
                        //}
                    }
                }

            }
        }
    }



    private Boolean PostDataForToken()
    {
        try
        {
            var request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["IDP_TokenURL"].ToString());
            var postData = "client_id=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["IDP_Code"].ToString());
            postData += "&client_secret=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["IDP_ClientSecret"].ToString());
            postData += "&code=" + Uri.EscapeDataString(gscode);
            postData += "&redirect_uri=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["IDP_RedirectURL"].ToString());
            postData += "&grant_type=" + Uri.EscapeDataString("authorization_code");
            postData += "&scope=" + Uri.EscapeDataString("openid profile email");
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;


            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //Response.Write(responseString);

            string responseFromServer = responseString.ToString();
            JavaScriptSerializer j = new JavaScriptSerializer();
            IDPLoginModel model = j.Deserialize<IDPLoginModel>(responseFromServer);
            if (model != null)
            {
                gs_accesstoken = model.access_token;
                gs_refreshtoekn = model.refresh_token;
                Session["accesstoken"] = model.access_token;
                Session["refreshtoken"] = model.refresh_token;

                DateTime CurrDate = new DateTime();
                CurrDate = DateTime.Now;
                CurrDate = CurrDate.AddMinutes((Convert.ToDouble(model.expires_in) / 60));
                //CurrDate = CurrDate.AddMinutes(6);
                Session["idp_refreshtoken_expiry"] = CurrDate;
                return true;
            }
            else
            {
                return false;
            }
            //http s://www.getpostman.com/oauth2/callback
        }
        catch (Exception ex)
        {
            Response.Write(ex);
            return false;
        }
    }

    //private bool PostDataForRefreshToken(string asRefreshToken)
    //{
    //    try
    //    {
    //        var request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["IDP_TokenURL"].ToString());
    //        var postData = "client_id=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["IDP_Code"].ToString());
    //        postData += "&client_secret=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["IDP_ClientSecret"].ToString());
    //        postData += "&refresh_token=" + Uri.EscapeDataString(asRefreshToken);
    //        postData += "&redirect_uri=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["IDP_RedirectURL"].ToString());
    //        postData += "&grant_type=" + Uri.EscapeDataString("refresh_token");
    //        postData += "&scope=" + Uri.EscapeDataString("openid profile email");
    //        var data = Encoding.ASCII.GetBytes(postData);
    //        request.Method = "POST";
    //        request.ContentType = "application/x-www-form-urlencoded";
    //        request.ContentLength = data.Length;

    //        using (var stream = request.GetRequestStream())
    //        {
    //            stream.Write(data, 0, data.Length);
    //        }

    //        var response = (HttpWebResponse)request.GetResponse();
    //        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

    //        string responseFromServer = responseString.ToString();
    //        JavaScriptSerializer j = new JavaScriptSerializer();
    //        IDPLoginModel model = j.Deserialize<IDPLoginModel>(responseFromServer);
    //        if (model != null)
    //        {
    //            gs_accesstoken = model.access_token;
    //            gs_refreshtoekn = model.refresh_token;
    //            return true;
    //        }
    //        else {
    //            return false;
    //        }


    //        //Response.Write(responseString);

    //        //http s://www.getpostman.com/oauth2/callback
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex);
    //        return false;
    //    }
    //}

    private bool PostDataForLoginUser(string asRefreshToken)
    {
        try
        {
            var request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["IDP_UserURL"].ToString());

            //var postData = "client_id=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["IDP_Code"].ToString());
            //postData += "&client_secret=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["IDP_ClientSecret"].ToString());
            //postData += "&refresh_token=" + Uri.EscapeDataString(asRefreshToken);
            //postData += "&redirect_uri=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["IDP_RedirectURL"].ToString());
            //postData += "&grant_type=" + Uri.EscapeDataString("refresh_token");
            //postData += "&scope=" + Uri.EscapeDataString("openid profile email");
            //var data = Encoding.ASCII.GetBytes(postData);

            request.Headers.Add("Authorization", "Bearer " + Session["accesstoken"]);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;

            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            string responseFromServer = responseString.ToString();
            JavaScriptSerializer j = new JavaScriptSerializer();
            IDPLoginModel model = j.Deserialize<IDPLoginModel>(responseFromServer);
            if (model != null)
            {
                gs_email = model.email;
                return true;
            }
            else
            {
                return false;
            }


            //Response.Write(responseString);

            //http s://www.getpostman.com/oauth2/callback
        }
        catch (Exception ex)
        {
            Response.Write(ex);
            return false;
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //PostDataForRefreshToken(txtaccesstoken.Text);
    }

    private void fnLogin(string lUserEmail)
    {
        //Session.Clear();
        Session["refreshtoken"] = gs_refreshtoekn;
        Session["accesstoken"] = gs_accesstoken;
        Session["sRequestId"] = null;
        Session["sEditRequestId"] = null;
        string strAbsoluteUri = Page.Request.Url.AbsoluteUri.ToString().Replace("loginokta.aspx", "login.aspx");
        strAbsoluteUri = ConfigurationManager.AppSettings["IDP_BaseURL"] + "/accounts/logout/";

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
            objResParam.DivCode = (string)Session["sDivCode"];
            objResParam.CssTheme = strTheme;
            Session["sobjResParam"] = objResParam;
        }
        else
        {
            ReservationParameters objResParam = new ReservationParameters();
            objResParam = (ReservationParameters)Session["sobjResParam"];
            objResParam.AbsoluteUrl = strAbsoluteUri;
            objResParam.AgentCompany = strCompany;
            objResParam.DivCode = (string)Session["sDivCode"];
            objResParam.CssTheme = strTheme;
            Session["sobjResParam"] = objResParam;
        }

        //LoadLoginPageFields();
        string lUserName;
        lUserName = objclsUtilities.ExecuteQueryReturnStringValue("select top 1 webusername from agentmast (nolock) where isnull(webapprove,0)=1 and webemail='" + lUserEmail + "'");
        string lShortName;
        lShortName = objclsUtilities.ExecuteQueryReturnStringValue("select top 1 ShortName from agentmast (nolock) where isnull(webapprove,0)=1 and webemail='" + lUserEmail + "'");
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
        BLLLogin objBLLLogin = new BLLLogin();
        objBLLLogin.UserName = lUserName;
        objBLLLogin.ShortName = lShortName;
        String LoginIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        objBLLLogin.LoginType = "MAIN";
        objBLLLogin.IpAddress = LoginIp;
        objBLLLogin.DivCode = (String)Session["sDivCode"];
        DataTable dtValidate;
        dtValidate = objBLLLogin.ValidateUserWithoutPassword();


        if (dtValidate.Rows.Count > 0)
        {
            Session.Add("GlobalUserName", lUserName);
            //Session.Add("Userpwd", txtPassword.Text.Trim);
            Session.Add("changeyear", DateTime.Now.Year.ToString());
            Session.Add("sLoginType", "Agent");

            Session.Add("sAgentCode", dtValidate.Rows[0]["agentcode"].ToString());
            Session.Add("sCurrencyCode", dtValidate.Rows[0]["currcode"].ToString());
            Session.Add("sCountryCode", dtValidate.Rows[0]["ctrycode"].ToString());
            Session["sLang"] = "en-us";

            GetReservationParamValues();


            ReservationParameters objResParam = new ReservationParameters();
            if (Session["sobjResParam"] != null)
            {
                objResParam = (ReservationParameters)Session["sobjResParam"];
            }
            objResParam.LoginIp = LoginIp;
            String strIpLocation = ""; // GeionName = strIpLocation;
            objResParam.AgentCode = dtValidate.Rows[0]["agentcode"].ToString();
            objResParam.LoginIpLocationName = strIpLocation;
            objResParam.LoginType = "Agent";


            BLLHotelSearch objBLLHotelSearch = new BLLHotelSearch();
            objResParam.Cumulative = objBLLHotelSearch.FindBookingEnginRateType(dtValidate.Rows[0]["agentcode"].ToString()).ToString();
            objResParam.WhiteLabel = objBLLHotelSearch.FindWhiteLabel(dtValidate.Rows[0]["agentcode"].ToString()).ToString();
            if (dtValidate.Rows[0]["logintype"].ToString() == "subuser")
            {
                objResParam.SubUserCode = dtValidate.Rows[0]["agentsubcode"].ToString();
                objResParam.IsSubUser = "1";
            }
            else
            {
                objResParam.SubUserCode = "";
                objResParam.IsSubUser = "0";
            }

            Session["sobjResParam"] = objResParam;
            FormsAuthentication.SetAuthCookie(lUserName, false);
            Session["IDPLOginType"] = "Agent";
            Response.Redirect("Home.aspx", false);
        }
        else
        {
            Response.Write("The account is not authorized to login");
        }
        //}
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


    private void GetReservationParamValues()
    {
        string strParams = "514,2009";
        DataTable objReservationParm;
        objReservationParm = objBLLLogin.GetReservationParamValues(strParams);
        if (objReservationParm != null)
        {
            if (objReservationParm.Rows.Count > 0)
            {
                ReservationParameters objResParam = new ReservationParameters();
                if (Session["sobjResParam"] != null)
                    objResParam = (ReservationParameters)Session["sobjResParam"];


                for (int i = 0; i <= objReservationParm.Rows.Count - 1; i++)
                {
                    if (objReservationParm.Rows[i]["param_id"].ToString() == "514")
                        objResParam.NoOfNightLimit = objReservationParm.Rows[i]["option_selected"].ToString();
                    if (objReservationParm.Rows[i]["param_id"].ToString() == "2009")
                        objResParam.ChildAgeLimit = objReservationParm.Rows[i]["option_selected"].ToString();
                }
                Session["sobjResParam"] = objResParam;
            }
        }
    }
}