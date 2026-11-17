using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x02000025 RID: 37
  public static class ComposioTurnErrors
  {
    // Token: 0x06000404 RID: 1028 RVA: 0x000E9F30 File Offset: 0x000E9F30
    [NullableContext(2)]
    public static bool IsListingFailure(string error)
    {
      string text = ((error != null) ? error.ToLowerInvariant() : null);
      return text != null && text.Contains("composio") && (text.Contains("tools/list failed") || text.Contains("initialize failed"));
    }
  }
}
