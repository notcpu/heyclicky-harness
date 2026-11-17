using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200016D RID: 365
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InkNoteVersionFile
  {
    // Token: 0x17000512 RID: 1298
    // (get) Token: 0x06001634 RID: 5684 RVA: 0x003BC54C File Offset: 0x003BC54C
    // (set) Token: 0x06001635 RID: 5685 RVA: 0x003BC554 File Offset: 0x003BC554
    [JsonPropertyName("at")]
    public double At { get; set; }

    // Token: 0x17000513 RID: 1299
    // (get) Token: 0x06001636 RID: 5686 RVA: 0x003BC560 File Offset: 0x003BC560
    // (set) Token: 0x06001637 RID: 5687 RVA: 0x003BC568 File Offset: 0x003BC568
    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    // Token: 0x17000514 RID: 1300
    // (get) Token: 0x06001638 RID: 5688 RVA: 0x003BC574 File Offset: 0x003BC574
    // (set) Token: 0x06001639 RID: 5689 RVA: 0x003BC57C File Offset: 0x003BC57C
    [JsonPropertyName("isEdit")]
    public bool IsEdit { get; set; }

    // Token: 0x17000515 RID: 1301
    // (get) Token: 0x0600163A RID: 5690 RVA: 0x003BC588 File Offset: 0x003BC588
    // (set) Token: 0x0600163B RID: 5691 RVA: 0x003BC590 File Offset: 0x003BC590
    [JsonPropertyName("isPartial")]
    public bool? IsPartial { get; set; }

    // Token: 0x17000516 RID: 1302
    // (get) Token: 0x0600163C RID: 5692 RVA: 0x003BC59C File Offset: 0x003BC59C
    // (set) Token: 0x0600163D RID: 5693 RVA: 0x003BC5A4 File Offset: 0x003BC5A4
    [JsonPropertyName("startsNewEdit")]
    public bool? StartsNewEdit { get; set; }

    // Token: 0x0600163E RID: 5694 RVA: 0x003BC5B0 File Offset: 0x003BC5B0
    public ClickyInkNoteVersion ToVersion()
    {
      return new ClickyInkNoteVersion(InkFiles.FromSecondsSince1970(this.At), this.Text, this.IsEdit, this.IsPartial, this.StartsNewEdit);
    }

    // Token: 0x0600163F RID: 5695 RVA: 0x003BC5DC File Offset: 0x003BC5DC
    public static InkNoteVersionFile From(ClickyInkNoteVersion version)
    {
      return new InkNoteVersionFile
      {
        At = InkFiles.SecondsSince1970(version.At),
        Text = version.Text,
        IsEdit = version.IsEdit,
        IsPartial = version.IsPartial,
        StartsNewEdit = version.StartsNewEdit
      };
    }
  }
}
