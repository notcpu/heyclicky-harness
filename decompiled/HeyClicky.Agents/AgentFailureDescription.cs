using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Authentication;

namespace HeyClicky.Agents
{
  // Token: 0x02000005 RID: 5
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentFailureDescription
  {
    // Token: 0x06000011 RID: 17 RVA: 0x000D6160 File Offset: 0x000D6160
    public static string Diagnostic(Exception error)
    {
      List<string> list = new List<string>();
      Exception ex = error;
      while (ex != null && list.Count < 4)
      {
        list.Add(ex.GetType().Name + ": " + ((ex.Message.Length <= 300) ? ex.Message : ex.Message.Substring(0, 300)));
        ex = ex.InnerException;
      }
      list.Reverse();
      return string.Join(" < ", list);
    }

    // Token: 0x06000012 RID: 18 RVA: 0x000D61E4 File Offset: 0x000D61E4
    public static string UserMessage(Exception error)
    {
      if (!AgentFailureDescription.IsSecureConnectionFailure(error))
      {
        return error.Message;
      }
      return "HeyClicky couldn't make a secure connection to its server. A firewall, VPN or antivirus on this network may be blocking it. Retry, or try another network.";
    }

    // Token: 0x06000013 RID: 19 RVA: 0x000D61FC File Offset: 0x000D61FC
    private static bool IsSecureConnectionFailure(Exception error)
    {
      Exception ex = error;
      while (ex != null)
      {
        if (ex is AuthenticationException)
        {
          goto IL_001F;
        }
        HttpRequestException ex2 = ex as HttpRequestException;
        if (ex2 != null && ex2.HttpRequestError == HttpRequestError.SecureConnectionError)
        {
          goto IL_001F;
        }
        bool flag = false;
        IL_0025:
        if (flag)
        {
          return true;
        }
        ex = ex.InnerException;
        continue;
        IL_001F:
        flag = true;
        goto IL_0025;
      }
      return false;
    }

    // Token: 0x04000009 RID: 9
    public const string SecureConnectionFailedMessage = "HeyClicky couldn't make a secure connection to its server. A firewall, VPN or antivirus on this network may be blocking it. Retry, or try another network.";
  }
}
