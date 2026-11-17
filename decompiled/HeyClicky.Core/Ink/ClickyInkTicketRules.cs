using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000173 RID: 371
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkTicketRules
  {
    // Token: 0x0600168B RID: 5771 RVA: 0x003BF308 File Offset: 0x003BF308
    public static string Code(string text)
    {
      int num = text.IndexOf('(');
      int num2 = text.IndexOf(')');
      if (num >= 0 && num2 >= 0 && num < num2)
      {
        int num3 = num + 1;
        return text.Substring(num3, num2 - num3).ToUpperInvariant();
      }
      if (text.CharacterCount() <= 4)
      {
        return text.ToUpperInvariant();
      }
      return text.PrefixCharacters(3).ToUpperInvariant();
    }

    // Token: 0x0600168C RID: 5772 RVA: 0x003BF364 File Offset: 0x003BF364
    public static string Place(string text)
    {
      int num = text.IndexOf('(');
      if (num >= 0)
      {
        return text.Substring(0, num).TrimWs();
      }
      if (text.CharacterCount() > 4)
      {
        return text;
      }
      return "";
    }

    // Token: 0x0600168D RID: 5773 RVA: 0x003BF39C File Offset: 0x003BF39C
    public static string Icon([Nullable(2)] string kind, bool hasRoute)
    {
      string text = ((kind != null) ? kind.ToLowerInvariant() : null);
      string text2;
      if (!(text == "train"))
      {
        if (!(text == "event") && !(text == "show") && !(text == "concert") && !(text == "game"))
        {
          if (!(text == "bus"))
          {
            text2 = (hasRoute ? "airplane" : "ticket.fill");
          }
          else
          {
            text2 = "bus.fill";
          }
        }
        else
        {
          text2 = "ticket.fill";
        }
      }
      else
      {
        text2 = "tram.fill";
      }
      return text2;
    }
  }
}
