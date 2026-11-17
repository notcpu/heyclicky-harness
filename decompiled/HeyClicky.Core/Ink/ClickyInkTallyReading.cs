using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000140 RID: 320
  public readonly struct ClickyInkTallyReading : IEquatable<ClickyInkTallyReading>
  {
    // Token: 0x170004B8 RID: 1208
    // (get) Token: 0x06001422 RID: 5154 RVA: 0x003ABB54 File Offset: 0x003ABB54
    [Nullable(1)]
    public string Key
    {
      [NullableContext(1)]
      get;
    }

    // Token: 0x170004B9 RID: 1209
    // (get) Token: 0x06001423 RID: 5155 RVA: 0x003ABB5C File Offset: 0x003ABB5C
    public long Count { get; }

    // Token: 0x170004BA RID: 1210
    // (get) Token: 0x06001424 RID: 5156 RVA: 0x003ABB64 File Offset: 0x003ABB64
    public long Fresh { get; }

    // Token: 0x06001425 RID: 5157 RVA: 0x003ABB6C File Offset: 0x003ABB6C
    [NullableContext(1)]
    public ClickyInkTallyReading(ClickyInkDocument.Block block, int? latestEdit = null)
    {
      this.Key = new string[] { "value", "count", "n" }.FirstOrDefault<string>((string k) => block[k] != null) ?? "value";
      long num = 0L;
      ClickyInkNumber? clickyInkNumber = ClickyInkNumber.Parse(block[this.Key]);
      this.Count = Math.Max(num, InkText.InkClamping(Math.Round((clickyInkNumber != null) ? clickyInkNumber.GetValueOrDefault().Value : 0.0, MidpointRounding.AwayFromZero)));
      string text = block["new"];
      if (text != null)
      {
        clickyInkNumber = ClickyInkNumber.Parse(text);
        double? num2 = ((clickyInkNumber != null) ? new double?(clickyInkNumber.GetValueOrDefault().Value) : null);
        if (num2 != null)
        {
          double valueOrDefault = num2.GetValueOrDefault();
          this.Fresh = Math.Max(0L, InkText.InkClamping(valueOrDefault));
          return;
        }
      }
      if (ClickyInkChange.IsLatest(block, latestEdit))
      {
        string text2 = ClickyInkChange.Previous(block, this.Key);
        if (text2 != null)
        {
          clickyInkNumber = ClickyInkNumber.Parse(text2);
          double? num2 = ((clickyInkNumber != null) ? new double?(clickyInkNumber.GetValueOrDefault().Value) : null);
          if (num2 != null)
          {
            double valueOrDefault2 = num2.GetValueOrDefault();
            this.Fresh = Math.Max(0L, this.Count - InkText.InkClamping(Math.Round(valueOrDefault2, MidpointRounding.AwayFromZero)));
            return;
          }
        }
      }
      this.Fresh = 0L;
    }

    // Token: 0x06001426 RID: 5158 RVA: 0x003ABD18 File Offset: 0x003ABD18
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkTallyReading");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001427 RID: 5159 RVA: 0x003ABD64 File Offset: 0x003ABD64
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Key = ");
      builder.Append(this.Key);
      builder.Append(", Count = ");
      builder.Append(this.Count.ToString());
      builder.Append(", Fresh = ");
      builder.Append(this.Fresh.ToString());
      return true;
    }

    // Token: 0x06001428 RID: 5160 RVA: 0x003ABDDC File Offset: 0x003ABDDC
    [CompilerGenerated]
    public static bool operator !=(ClickyInkTallyReading left, ClickyInkTallyReading right)
    {
      return !(left == right);
    }

    // Token: 0x06001429 RID: 5161 RVA: 0x003ABDE8 File Offset: 0x003ABDE8
    [CompilerGenerated]
    public static bool operator ==(ClickyInkTallyReading left, ClickyInkTallyReading right)
    {
      return left.Equals(right);
    }

    // Token: 0x0600142A RID: 5162 RVA: 0x003ABDF4 File Offset: 0x003ABDF4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<Count>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<Fresh>k__BackingField);
    }

    // Token: 0x0600142B RID: 5163 RVA: 0x003ABE34 File Offset: 0x003ABE34
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ClickyInkTallyReading && this.Equals((ClickyInkTallyReading)obj);
    }

    // Token: 0x0600142C RID: 5164 RVA: 0x003ABE4C File Offset: 0x003ABE4C
    [CompilerGenerated]
    public bool Equals(ClickyInkTallyReading other)
    {
      return EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<Count>k__BackingField, other.<Count>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<Fresh>k__BackingField, other.<Fresh>k__BackingField);
    }
  }
}
