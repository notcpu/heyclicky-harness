using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.Core.Notch;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200018B RID: 395
  [NullableContext(2)]
  public interface INotchUpdateContentSource
  {
    // Token: 0x06001584 RID: 5508
    [NullableContext(1)]
    [return: Nullable(2)]
    NotchUpdateContent Content(string railKey);

    // Token: 0x1400005E RID: 94
    // (add) Token: 0x06001585 RID: 5509
    // (remove) Token: 0x06001586 RID: 5510
    event Action Changed;

    // Token: 0x06001587 RID: 5511 RVA: 0x005B5364 File Offset: 0x005B5364
    [NullableContext(1)]
    [return: TupleElementNames(new string[] { "Subject", "Properties" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    ValueTuple<string, JsonObject>? ShownAnalytics(string railKey, NotchUpdateKind kind)
    {
      return null;
    }
  }
}
