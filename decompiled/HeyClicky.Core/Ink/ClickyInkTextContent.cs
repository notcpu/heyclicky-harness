using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000184 RID: 388
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkTextContent : IEquatable<ClickyInkTextContent>
  {
    // Token: 0x17000549 RID: 1353
    // (get) Token: 0x06001743 RID: 5955 RVA: 0x003C2B58 File Offset: 0x003C2B58
    public List<ClickyInkRun> Runs { get; } = new List<ClickyInkRun>();

    // Token: 0x1700054A RID: 1354
    // (get) Token: 0x06001744 RID: 5956 RVA: 0x003C2B60 File Offset: 0x003C2B60
    // (set) Token: 0x06001745 RID: 5957 RVA: 0x003C2B68 File Offset: 0x003C2B68
    public int PenRun { get; set; } = -1;

    // Token: 0x1700054B RID: 1355
    // (get) Token: 0x06001746 RID: 5958 RVA: 0x003C2B74 File Offset: 0x003C2B74
    // (set) Token: 0x06001747 RID: 5959 RVA: 0x003C2B7C File Offset: 0x003C2B7C
    public ClickyInkPenAttribute? Pen { get; set; }

    // Token: 0x1700054C RID: 1356
    // (get) Token: 0x06001748 RID: 5960 RVA: 0x003C2B88 File Offset: 0x003C2B88
    // (set) Token: 0x06001749 RID: 5961 RVA: 0x003C2B90 File Offset: 0x003C2B90
    public double PenClock { get; set; }

    // Token: 0x0600174A RID: 5962 RVA: 0x003C2B9C File Offset: 0x003C2B9C
    public ClickyInkTextContent()
    {
    }

    // Token: 0x0600174B RID: 5963 RVA: 0x003C2BB8 File Offset: 0x003C2BB8
    public ClickyInkTextContent(IEnumerable<ClickyInkRun> runs)
    {
      this.Runs.AddRange(runs);
    }

    // Token: 0x0600174C RID: 5964 RVA: 0x003C2BE0 File Offset: 0x003C2BE0
    public static ClickyInkTextContent Plain(string text, Color? color = null)
    {
      return new ClickyInkTextContent(new <>z__ReadOnlySingleElementList<ClickyInkRun>(new ClickyInkRun(text, color, false, null, null, false, null, null)));
    }

    // Token: 0x0600174D RID: 5965 RVA: 0x003C2C1C File Offset: 0x003C2C1C
    public static ClickyInkTextContent Symbol(string name, Color? color = null)
    {
      return new ClickyInkTextContent(new <>z__ReadOnlySingleElementList<ClickyInkRun>(new ClickyInkRun("", color, false, null, null, false, null, name)));
    }

    // Token: 0x0600174E RID: 5966 RVA: 0x003C2C60 File Offset: 0x003C2C60
    public ClickyInkTextContent Append(ClickyInkTextContent other)
    {
      ClickyInkTextContent clickyInkTextContent = new ClickyInkTextContent(this.Runs)
      {
        PenRun = this.PenRun,
        Pen = this.Pen,
        PenClock = this.PenClock
      };
      if (other.PenRun >= 0 && this.PenRun < 0)
      {
        clickyInkTextContent.PenRun = this.Runs.Count + other.PenRun;
        clickyInkTextContent.Pen = other.Pen;
        clickyInkTextContent.PenClock = other.PenClock;
      }
      clickyInkTextContent.Runs.AddRange(other.Runs);
      return clickyInkTextContent;
    }

    // Token: 0x1700054D RID: 1357
    // (get) Token: 0x0600174F RID: 5967 RVA: 0x003C2CF4 File Offset: 0x003C2CF4
    public string PlainText
    {
      get
      {
        return string.Concat(this.Runs.Select<ClickyInkRun, string>((ClickyInkRun r) => r.Text));
      }
    }

    // Token: 0x06001750 RID: 5968 RVA: 0x003C2D28 File Offset: 0x003C2D28
    [NullableContext(2)]
    public bool Equals(ClickyInkTextContent other)
    {
      return other != null && this.Runs.SequenceEqual<ClickyInkRun>(other.Runs) && this.PenRun == other.PenRun && this.Pen == other.Pen;
    }

    // Token: 0x06001751 RID: 5969 RVA: 0x003C2D9C File Offset: 0x003C2D9C
    [NullableContext(2)]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkTextContent);
    }

    // Token: 0x06001752 RID: 5970 RVA: 0x003C2DAC File Offset: 0x003C2DAC
    public override int GetHashCode()
    {
      return HashCode.Combine<int, int, ClickyInkPenAttribute?>(this.Runs.Count, this.PenRun, this.Pen);
    }
  }
}
