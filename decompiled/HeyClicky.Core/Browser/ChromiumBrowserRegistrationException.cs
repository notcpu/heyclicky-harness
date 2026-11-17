using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001E5 RID: 485
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ChromiumBrowserRegistrationException : IOException
  {
    // Token: 0x06001BE9 RID: 7145 RVA: 0x003DEB6C File Offset: 0x003DEB6C
    public ChromiumBrowserRegistrationException(string browserName, string keyPath, Exception inner)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(100, 2);
      defaultInterpolatedStringHandler.AppendLiteral("HeyClicky couldn't write to ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral("'s registry key ");
      defaultInterpolatedStringHandler.AppendFormatted(keyPath);
      defaultInterpolatedStringHandler.AppendLiteral(". Check that your account can change it, then try again.");
      base..ctor(defaultInterpolatedStringHandler.ToStringAndClear(), inner);
    }

    // Token: 0x1700062B RID: 1579
    // (get) Token: 0x06001BEA RID: 7146 RVA: 0x003DEBD4 File Offset: 0x003DEBD4
    public string BrowserName { get; } = browserName;

    // Token: 0x1700062C RID: 1580
    // (get) Token: 0x06001BEB RID: 7147 RVA: 0x003DEBDC File Offset: 0x003DEBDC
    public string KeyPath { get; } = keyPath;
  }
}
