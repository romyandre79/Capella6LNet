using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capella6LNet
{
  public class User
  {
    public string UserAccessId;
    public string UserName;
    public string Password;
    public string RealName;
    public string Email;
    public string PhoneNo;
    public string LanguageId;
    public string ThemeId;
    public string IsOnline;
    public string AuthKey;
    public string Wallpaper;
    public string LanguageName;
    public string ThemeName;
    public string RecordStatus;
    public IList<MenuAccess> MenuItems;
    public IList<MenuAccess> UserFavs;
  }
}
