using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001DF RID: 479
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaperFont : IEquatable<PaperFont>
  {
    // Token: 0x060017FD RID: 6141 RVA: 0x005C8354 File Offset: 0x005C8354
    public PaperFont(FontFamily Family, string FamilyName, double Size, FontWeight Weight)
    {
      this.Family = Family;
      this.FamilyName = FamilyName;
      this.Size = Size;
      this.Weight = Weight;
      base..ctor();
    }

    // Token: 0x17000441 RID: 1089
    // (get) Token: 0x060017FE RID: 6142 RVA: 0x005C837C File Offset: 0x005C837C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PaperFont);
      }
    }

    // Token: 0x17000442 RID: 1090
    // (get) Token: 0x060017FF RID: 6143 RVA: 0x005C8388 File Offset: 0x005C8388
    // (set) Token: 0x06001800 RID: 6144 RVA: 0x005C8390 File Offset: 0x005C8390
    public FontFamily Family { get; set; }

    // Token: 0x17000443 RID: 1091
    // (get) Token: 0x06001801 RID: 6145 RVA: 0x005C839C File Offset: 0x005C839C
    // (set) Token: 0x06001802 RID: 6146 RVA: 0x005C83A4 File Offset: 0x005C83A4
    public string FamilyName { get; set; }

    // Token: 0x17000444 RID: 1092
    // (get) Token: 0x06001803 RID: 6147 RVA: 0x005C83B0 File Offset: 0x005C83B0
    // (set) Token: 0x06001804 RID: 6148 RVA: 0x005C83B8 File Offset: 0x005C83B8
    public double Size { get; set; }

    // Token: 0x17000445 RID: 1093
    // (get) Token: 0x06001805 RID: 6149 RVA: 0x005C83C4 File Offset: 0x005C83C4
    // (set) Token: 0x06001806 RID: 6150 RVA: 0x005C83CC File Offset: 0x005C83CC
    public FontWeight Weight { get; set; }

    // Token: 0x06001807 RID: 6151 RVA: 0x005C83D8 File Offset: 0x005C83D8
    public TextBlock Apply(TextBlock text)
    {
      text.FontFamily = this.Family;
      text.FontSize = this.Size;
      text.FontWeight = this.Weight;
      text.IsTextScaleFactorEnabled = false;
      return text;
    }

    // Token: 0x06001808 RID: 6152 RVA: 0x005C8408 File Offset: 0x005C8408
    public CanvasTextFormat TextFormat()
    {
      return new CanvasTextFormat
      {
        FontFamily = this.FamilyName,
        FontSize = (float)this.Size,
        FontWeight = this.Weight,
        WordWrapping = CanvasWordWrapping.NoWrap
      };
    }

    // Token: 0x06001809 RID: 6153 RVA: 0x005C843C File Offset: 0x005C843C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PaperFont");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600180A RID: 6154 RVA: 0x005C8488 File Offset: 0x005C8488
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Family = ");
      builder.Append(this.Family);
      builder.Append(", FamilyName = ");
      builder.Append(this.FamilyName);
      builder.Append(", Size = ");
      builder.Append(this.Size.ToString());
      builder.Append(", Weight = ");
      builder.Append(this.Weight.ToString());
      return true;
    }

    // Token: 0x0600180B RID: 6155 RVA: 0x005C851C File Offset: 0x005C851C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PaperFont left, PaperFont right)
    {
      return !(left == right);
    }

    // Token: 0x0600180C RID: 6156 RVA: 0x005C8528 File Offset: 0x005C8528
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PaperFont left, PaperFont right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600180D RID: 6157 RVA: 0x005C853C File Offset: 0x005C853C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<FontFamily>.Default.GetHashCode(this.<Family>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FamilyName>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Size>k__BackingField)) * -1521134295 + EqualityComparer<FontWeight>.Default.GetHashCode(this.<Weight>k__BackingField);
    }

    // Token: 0x0600180E RID: 6158 RVA: 0x005C85B8 File Offset: 0x005C85B8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PaperFont);
    }

    // Token: 0x0600180F RID: 6159 RVA: 0x005C85C8 File Offset: 0x005C85C8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PaperFont other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<FontFamily>.Default.Equals(this.<Family>k__BackingField, other.<Family>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FamilyName>k__BackingField, other.<FamilyName>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Size>k__BackingField, other.<Size>k__BackingField) && EqualityComparer<FontWeight>.Default.Equals(this.<Weight>k__BackingField, other.<Weight>k__BackingField));
    }

    // Token: 0x06001811 RID: 6161 RVA: 0x005C865C File Offset: 0x005C865C
    [CompilerGenerated]
    private PaperFont(PaperFont original)
    {
      this.Family = original.<Family>k__BackingField;
      this.FamilyName = original.<FamilyName>k__BackingField;
      this.Size = original.<Size>k__BackingField;
      this.Weight = original.<Weight>k__BackingField;
    }

    // Token: 0x06001812 RID: 6162 RVA: 0x005C8694 File Offset: 0x005C8694
    [CompilerGenerated]
    public void Deconstruct(out FontFamily Family, out string FamilyName, out double Size, out FontWeight Weight)
    {
      Family = this.Family;
      FamilyName = this.FamilyName;
      Size = this.Size;
      Weight = this.Weight;
    }
  }
}
