using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000143 RID: 323
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyInkQuietReading : IEquatable<ClickyInkQuietReading>
  {
    // Token: 0x170004C7 RID: 1223
    // (get) Token: 0x0600144D RID: 5197 RVA: 0x003AC7B4 File Offset: 0x003AC7B4
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkQuietReading);
      }
    }

    // Token: 0x170004C8 RID: 1224
    // (get) Token: 0x0600144E RID: 5198 RVA: 0x003AC7C0 File Offset: 0x003AC7C0
    public string CheckedLine { get; }

    // Token: 0x170004C9 RID: 1225
    // (get) Token: 0x0600144F RID: 5199 RVA: 0x003AC7C8 File Offset: 0x003AC7C8
    public long Runs { get; }

    // Token: 0x06001450 RID: 5200 RVA: 0x003AC7D0 File Offset: 0x003AC7D0
    [NullableContext(1)]
    public ClickyInkQuietReading(ClickyInkDocument.Block block)
    {
      string text = block["checked"] ?? block["sources"];
      string text2 = ((text != null) ? text.TrimWs() : null);
      string text3 = block["at"] ?? block["when"];
      string text4 = ((text3 != null) ? text3.TrimWs() : null);
      List<string> list = new List<string>();
      if (!string.IsNullOrEmpty(text2))
      {
        List<string> list2 = list;
        string text5 = "checked ";
        IEnumerable<string> enumerable = text2.Characters();
        Func<string, bool> func;
        if ((func = ClickyInkQuietReading.<>O.<0>__IsNumber) == null)
        {
          func = (ClickyInkQuietReading.<>O.<0>__IsNumber = new Func<string, bool>(InkText.IsNumber));
        }
        list2.Add(text5 + (enumerable.All<string>(func) ? (text2 + " sources") : text2));
      }
      if (!string.IsNullOrEmpty(text4))
      {
        if (list.Count == 0)
        {
          list.Add("checked " + text4);
        }
        else
        {
          List<string> list3 = list;
          list3[0] = list3[0] + " at " + text4;
        }
      }
      string text6 = block["since"];
      if (text6 != null && text6.Length > 0)
      {
        list.Add("nothing new since " + text6);
      }
      this.CheckedLine = ((list.Count == 0) ? null : string.Join(" · ", list));
      ClickyInkNumber? clickyInkNumber;
      this.Runs = Math.Max(1L, InkText.InkClamping((ClickyInkNumber.Parse(block["runs"] ?? block["streak"]) != null) ? clickyInkNumber.GetValueOrDefault().Value : 1.0));
    }

    // Token: 0x06001451 RID: 5201 RVA: 0x003AC964 File Offset: 0x003AC964
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkQuietReading");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001452 RID: 5202 RVA: 0x003AC9B0 File Offset: 0x003AC9B0
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("CheckedLine = ");
      builder.Append(this.CheckedLine);
      builder.Append(", Runs = ");
      builder.Append(this.Runs.ToString());
      return true;
    }

    // Token: 0x06001453 RID: 5203 RVA: 0x003ACA04 File Offset: 0x003ACA04
    [CompilerGenerated]
    public static bool operator !=(ClickyInkQuietReading left, ClickyInkQuietReading right)
    {
      return !(left == right);
    }

    // Token: 0x06001454 RID: 5204 RVA: 0x003ACA10 File Offset: 0x003ACA10
    [CompilerGenerated]
    public static bool operator ==(ClickyInkQuietReading left, ClickyInkQuietReading right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001455 RID: 5205 RVA: 0x003ACA24 File Offset: 0x003ACA24
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CheckedLine>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<Runs>k__BackingField);
    }

    // Token: 0x06001456 RID: 5206 RVA: 0x003ACA64 File Offset: 0x003ACA64
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkQuietReading);
    }

    // Token: 0x06001457 RID: 5207 RVA: 0x003ACA74 File Offset: 0x003ACA74
    [CompilerGenerated]
    public bool Equals(ClickyInkQuietReading other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<CheckedLine>k__BackingField, other.<CheckedLine>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<Runs>k__BackingField, other.<Runs>k__BackingField));
    }

    // Token: 0x06001459 RID: 5209 RVA: 0x003ACAD8 File Offset: 0x003ACAD8
    [CompilerGenerated]
    private ClickyInkQuietReading([Nullable(1)] ClickyInkQuietReading original)
    {
      this.CheckedLine = original.<CheckedLine>k__BackingField;
      this.Runs = original.<Runs>k__BackingField;
    }

    // Token: 0x020004C8 RID: 1224
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001DE8 RID: 7656
      [Nullable(0)]
      public static Func<string, bool> <0>__IsNumber;
    }
  }
}
