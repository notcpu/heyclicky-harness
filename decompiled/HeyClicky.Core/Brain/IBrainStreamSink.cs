using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001FB RID: 507
  public interface IBrainStreamSink
  {
    // Token: 0x06001D76 RID: 7542
    void OnText(ReadOnlySpan<char> text);

    // Token: 0x06001D77 RID: 7543
    [NullableContext(1)]
    void OnEvent(string type, JsonElement root);
  }
}
