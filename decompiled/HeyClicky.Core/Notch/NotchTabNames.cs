using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000117 RID: 279
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotchTabNames
  {
    // Token: 0x0600124B RID: 4683 RVA: 0x003A0FE0 File Offset: 0x003A0FE0
    public static string RawValue(this NotchTab tab)
    {
      string text;
      switch (tab)
      {
      case NotchTab.Home:
        text = "home";
        break;
      case NotchTab.Chat:
        text = "chat";
        break;
      case NotchTab.Settings:
        text = "settings";
        break;
      default:
        text = "document";
        break;
      }
      return text;
    }

    // Token: 0x0600124C RID: 4684 RVA: 0x003A1020 File Offset: 0x003A1020
    public static string Title(this NotchTab tab)
    {
      string text;
      switch (tab)
      {
      case NotchTab.Home:
        text = "Home";
        break;
      case NotchTab.Chat:
        text = "Chat";
        break;
      case NotchTab.Settings:
        text = "Settings";
        break;
      default:
        text = "Document";
        break;
      }
      return text;
    }

    // Token: 0x0600124D RID: 4685 RVA: 0x003A1060 File Offset: 0x003A1060
    public static string RawValue(this NotchSettingsSection section)
    {
      string text;
      if (section == NotchSettingsSection.Referral)
      {
        text = "referral";
      }
      else
      {
        text = section.ToString().ToLowerInvariant();
      }
      return text;
    }
  }
}
