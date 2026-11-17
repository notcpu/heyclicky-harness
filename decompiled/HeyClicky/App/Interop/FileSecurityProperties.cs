using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.App.Interop
{
  // Token: 0x02000197 RID: 407
  [NullableContext(1)]
  [Nullable(0)]
  public static class FileSecurityProperties
  {
    // Token: 0x06001604 RID: 5636
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SHObjectProperties(IntPtr hwnd, uint shopObjectType, string pszObjectName, [Nullable(2)] string pszPropertyPage);

    // Token: 0x06001605 RID: 5637 RVA: 0x005B9E18 File Offset: 0x005B9E18
    public static bool Show(string path)
    {
      bool flag;
      try
      {
        if (!File.Exists(path) && !Directory.Exists(path))
        {
          flag = false;
        }
        else
        {
          bool flag2 = FileSecurityProperties.SHObjectProperties((IntPtr)0, 2U, path, "Security");
          Log.Info("PageRead", flag2 ? "opened the file's Security properties" : "Security properties didn't open");
          flag = flag2;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("PageRead", "Security properties: " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x04000AB0 RID: 2736
    private const uint SHOP_FILEPATH = 2U;
  }
}
