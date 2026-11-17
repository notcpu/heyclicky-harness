using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200016C RID: 364
  public sealed class InkNoteSnapshot
  {
    // Token: 0x17000510 RID: 1296
    // (get) Token: 0x0600162F RID: 5679 RVA: 0x003BC514 File Offset: 0x003BC514
    // (set) Token: 0x06001630 RID: 5680 RVA: 0x003BC51C File Offset: 0x003BC51C
    [JsonPropertyName("version")]
    public int Version { get; set; } = 1;

    // Token: 0x17000511 RID: 1297
    // (get) Token: 0x06001631 RID: 5681 RVA: 0x003BC528 File Offset: 0x003BC528
    // (set) Token: 0x06001632 RID: 5682 RVA: 0x003BC530 File Offset: 0x003BC530
    [Nullable(new byte[] { 2, 1 })]
    [JsonPropertyName("versions")]
    public List<InkNoteVersionFile> Versions
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }
  }
}
