using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200018F RID: 399
  public static class NotchGreeting
  {
    // Token: 0x060015B0 RID: 5552 RVA: 0x005B6C84 File Offset: 0x005B6C84
    [NullableContext(2)]
    public static string FirstName(UserProfile user)
    {
      if (user == null)
      {
        return null;
      }
      string displayName = user.DisplayName;
      string text = ((displayName != null) ? displayName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault<string>() : null);
      if (text != null && text.Length > 0)
      {
        return text;
      }
      string text2 = user.Email.Split('@', StringSplitOptions.None).FirstOrDefault<string>();
      if (string.IsNullOrEmpty(text2))
      {
        return null;
      }
      string text3 = text2.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.73BF42C4F90EAC2D219F322C2FF3DD31C26B0124325CAED15C602CCAB325D1272).FieldHandle)).FirstOrDefault<string>((string s) => s.Length > 0) ?? text2;
      char c = char.ToUpperInvariant(text3[0]);
      ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
      string text4 = text3;
      return readOnlySpan + text4.Substring(1, text4.Length - 1);
    }

    // Token: 0x060015B1 RID: 5553 RVA: 0x005B6D50 File Offset: 0x005B6D50
    [NullableContext(1)]
    public static string Greeting([Nullable(2)] UserProfile user)
    {
      int hour = DateTime.Now.Hour;
      string text = ((hour < 12) ? "Morning" : ((hour < 17) ? "Afternoon" : "Evening"));
      string text2 = NotchGreeting.FirstName(user);
      if (text2 == null)
      {
        return text + ".";
      }
      return text + ", " + text2 + ".";
    }
  }
}
