using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading;
using System.Text;
public static class HttpHelper
{
	const string UserAgent="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3521.2 Safari/537.36";
	private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
	{
	return true;
	}
    public static string HttpGet(string Url)
    {
    	if(Url.ToLower().StartsWith("https"))
    	{
    		ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
    		ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
    	}
        HttpWebRequest request =(HttpWebRequest)WebRequest.Create(Url);
        request.Method = "GET";
        request.Timeout=30*1000;
        request.UserAgent=UserAgent;
        request.ContentType = "text/html;charset=UTF-8";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream myResponseStream = response.GetResponseStream();
        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        string retString = myStreamReader.ReadToEnd();
        myStreamReader.Close();
        myResponseStream.Close();
        return retString;
    }
}