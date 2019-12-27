using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net.Sockets;

namespace Capella6LNet
{

  public class Utility
  {
    public static void GetServerData(Host MyHost)
    {
      IniFile iniFile = new IniFile(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Capella6WNet.ini");
      MyHost.Title = iniFile.IniReadValue("MAIN", "title");
      MyHost.Company = iniFile.IniReadValue("MAIN", "company");
      MyHost.Serial = iniFile.IniReadValue("MAIN", "serial");
      MyHost.Icon = iniFile.IniReadValue("MAIN", "icon");
      MyHost.Wallpaper = iniFile.IniReadValue("MAIN", "wallpaper");
      MyHost.Server = iniFile.IniReadValue("API", "host");
      MyHost.User = iniFile.IniReadValue("API", "user");
      MyHost.Password = iniFile.IniReadValue("API", "password");
      MyHost.Timeout = iniFile.IniReadValue("API", "timeout");
      MyHost.HostIPExt = iniFile.IniReadValue("API", "hostipext");
      MyHost.HostIPGeo = iniFile.IniReadValue("API", "hostipgeo");
    }

    public static StringBuilder GetDataAPI(Host MyHost, string Url, StringBuilder RequestData)
    {
      StringBuilder ResponseData = new StringBuilder();
      try
      {
        WebRequest client = WebRequest.Create(MyHost.Server + Url);
        client.Timeout = Int32.Parse(MyHost.Timeout);
        client.Credentials = CredentialCache.DefaultCredentials;
        client.Method = WebRequestMethods.Http.Post;
        client.ContentType = "application/x-www-form-urlencoded";
        byte[] byteArray = Encoding.UTF8.GetBytes(RequestData.ToString());
        client.ContentLength = byteArray.Length;
        Stream dataStream = client.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        WebResponse response = client.GetResponse();
        using (dataStream = response.GetResponseStream())
        {
          StreamReader reader = new StreamReader(dataStream);
          ResponseData.Append(reader.ReadToEnd());
        }
        response.Close();
        return ResponseData;
      }
      catch (Exception e)
      {
        return ResponseData.Append("Error: "+e.Message);
      }      
    }

    public static string GetMessageAPI(Host MyHost, string Messages)
    {
      StringBuilder ResponseData = GetDataAPI(MyHost, "sysadm/getcatalog", new StringBuilder("messages=" + Messages));
      if (ResponseData.ToString().Contains("Error") == true)
      {
        return ResponseData.ToString();
      }
      else
      {
        BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(ResponseData.ToString());
        return baseResponse.Msg;
      }
    }

    public static string GetExternalIPAddress(Host MyHost)
    {
      WebRequest client = WebRequest.Create(MyHost.HostIPExt);
      client.Timeout = Int32.Parse(MyHost.Timeout);
      client.Credentials = CredentialCache.DefaultCredentials;
      client.Method = "GET";
      string s = "";
      WebResponse response = client.GetResponse();
      using (Stream dataStream = response.GetResponseStream())
      {
        StreamReader reader = new StreamReader(dataStream);
        s = reader.ReadToEnd();
      }
      response.Close();
      var match = Regex.Match(s, @"Current IP Address: (.+?)</body").Groups[1].Value;
      return match;
    }

    public static string GetIpAddrList()
    {
      var host = Dns.GetHostEntry(Dns.GetHostName());
      foreach (var ip in host.AddressList)
      {
        if (ip.AddressFamily == AddressFamily.InterNetwork)
        {
          return ip.ToString();
        }
      }
      throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public static bool GetAccess(Host MyHost, User MyUser, string MenuName, string MenuAction, out StringBuilder ResponseData)
    {
      StringBuilder requestData = new StringBuilder();
      requestData.Append("token=" + MyUser.AuthKey);
      requestData.Append("&menuname=" + MenuName);
      requestData.Append("&menuaction=" + MenuAction);
      ResponseData = GetDataAPI(MyHost, "sysadm/checkaccess", requestData);
      MenuAccessRightResponse menuAccessRightResponse = JsonConvert.DeserializeObject<MenuAccessRightResponse>(ResponseData.ToString());
      int i = menuAccessRightResponse.Rows.access;
      if (i == 0)
      {
        return false;
      } else
      {
        return true;
      }
    }

    public static bool Login(Host MyHost, string UserName, string UserPassword, User MyUser)
    {
      bool ret = false;
      StringBuilder ResponseData = Utility.GetDataAPI(MyHost,"sysadm/login", new StringBuilder("username="+UserName+"&password="+UserPassword));
      UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(ResponseData.ToString());
      if (userResponse.IsError == 0)
      {
        MyUser.AuthKey = userResponse.Rows.AuthKey;
        MyUser.Email = userResponse.Rows.Email;
        MyUser.IsOnline = userResponse.Rows.IsOnline;
        MyUser.LanguageId = userResponse.Rows.LanguageId;
        MyUser.LanguageName = userResponse.Rows.LanguageName;
        MyUser.Password = userResponse.Rows.Password;
        MyUser.PhoneNo = userResponse.Rows.PhoneNo;
        MyUser.ThemeId = userResponse.Rows.ThemeId;
        MyUser.ThemeName = userResponse.Rows.ThemeName;
        MyUser.UserName = userResponse.Rows.UserName;
        MyUser.RealName = userResponse.Rows.RealName;
        MyUser.UserAccessId = userResponse.Rows.UserAccessId;
        ret = true;
      }
      return ret;
    }

    public static string GetDataUser(Host MyHost, User MyUser)
    {
      string s = MyHost.HostIPGeo + GetExternalIPAddress(MyHost);
      StringBuilder ResponseData = GetDataAPI(MyHost, s, new StringBuilder());
      IPGeoResponse response = JsonConvert.DeserializeObject<IPGeoResponse>(ResponseData.ToString());
      return MyUser.UserName + "," + GetExternalIPAddress(MyHost) + "," + GetIpAddrList() + "," + response.lat + "," + response.lon;
    }

    public static void GetAllMenus()
    {

    }

  }
}
