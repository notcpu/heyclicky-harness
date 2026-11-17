using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000128 RID: 296
  public sealed class InkActionsSnapshot
  {
    // Token: 0x17000483 RID: 1155
    // (get) Token: 0x06001354 RID: 4948 RVA: 0x003A66FC File Offset: 0x003A66FC
    // (set) Token: 0x06001355 RID: 4949 RVA: 0x003A6704 File Offset: 0x003A6704
    [JsonPropertyName("version")]
    public int Version { get; set; } = 1;

    // Token: 0x17000484 RID: 1156
    // (get) Token: 0x06001356 RID: 4950 RVA: 0x003A6710 File Offset: 0x003A6710
    // (set) Token: 0x06001357 RID: 4951 RVA: 0x003A6718 File Offset: 0x003A6718
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    [JsonPropertyName("actionsByThingID")]
    public Dictionary<string, List<InkSentActionFile>> ActionsByThingId
    {
      [return: Nullable(new byte[] { 2, 1, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1, 1 })]
      set;
    }
  }
}
