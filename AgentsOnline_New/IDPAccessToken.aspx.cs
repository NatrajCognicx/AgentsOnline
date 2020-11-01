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

public partial class IDPAccessToken : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void PostDataForToken()
    {
        try
        {
            var request = (HttpWebRequest)WebRequest.Create("http://43.241.62.103:8050/openid/token");
            var postData = "client_id=" + Uri.EscapeDataString("800977");
            postData += "&client_secret=" + Uri.EscapeDataString("2adf0d0c620e6da2191c4f83db02a2a583a6d1185bf300330c65f386");
            postData += "&code=" + Uri.EscapeDataString("");
            postData += "&redirect_uri=" + Uri.EscapeDataString("http://localhost:62717/AgentsOnline_New/IDPLogin.aspx");
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
            Response.Write(responseString);

            //http s://www.getpostman.com/oauth2/callback
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}