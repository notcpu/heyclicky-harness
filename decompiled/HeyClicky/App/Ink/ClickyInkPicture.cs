using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200027F RID: 639
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkPicture : IEquatable<ClickyInkPicture>
  {
    // Token: 0x06001AEA RID: 6890 RVA: 0x005E9A18 File Offset: 0x005E9A18
    public ClickyInkPicture(ImageSource Color, ImageSource Gray, int PixelWidth, int PixelHeight)
    {
      this.Color = Color;
      this.Gray = Gray;
      this.PixelWidth = PixelWidth;
      this.PixelHeight = PixelHeight;
      base..ctor();
    }

    // Token: 0x1700049D RID: 1181
    // (get) Token: 0x06001AEB RID: 6891 RVA: 0x005E9A40 File Offset: 0x005E9A40
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkPicture);
      }
    }

    // Token: 0x1700049E RID: 1182
    // (get) Token: 0x06001AEC RID: 6892 RVA: 0x005E9A4C File Offset: 0x005E9A4C
    // (set) Token: 0x06001AED RID: 6893 RVA: 0x005E9A54 File Offset: 0x005E9A54
    public ImageSource Color { get; set; }

    // Token: 0x1700049F RID: 1183
    // (get) Token: 0x06001AEE RID: 6894 RVA: 0x005E9A60 File Offset: 0x005E9A60
    // (set) Token: 0x06001AEF RID: 6895 RVA: 0x005E9A68 File Offset: 0x005E9A68
    public ImageSource Gray { get; set; }

    // Token: 0x170004A0 RID: 1184
    // (get) Token: 0x06001AF0 RID: 6896 RVA: 0x005E9A74 File Offset: 0x005E9A74
    // (set) Token: 0x06001AF1 RID: 6897 RVA: 0x005E9A7C File Offset: 0x005E9A7C
    public int PixelWidth { get; set; }

    // Token: 0x170004A1 RID: 1185
    // (get) Token: 0x06001AF2 RID: 6898 RVA: 0x005E9A88 File Offset: 0x005E9A88
    // (set) Token: 0x06001AF3 RID: 6899 RVA: 0x005E9A90 File Offset: 0x005E9A90
    public int PixelHeight { get; set; }

    // Token: 0x06001AF4 RID: 6900 RVA: 0x005E9A9C File Offset: 0x005E9A9C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkPicture");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001AF5 RID: 6901 RVA: 0x005E9AE8 File Offset: 0x005E9AE8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Color = ");
      builder.Append(this.Color);
      builder.Append(", Gray = ");
      builder.Append(this.Gray);
      builder.Append(", PixelWidth = ");
      builder.Append(this.PixelWidth.ToString());
      builder.Append(", PixelHeight = ");
      builder.Append(this.PixelHeight.ToString());
      return true;
    }

    // Token: 0x06001AF6 RID: 6902 RVA: 0x005E9B7C File Offset: 0x005E9B7C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkPicture left, ClickyInkPicture right)
    {
      return !(left == right);
    }

    // Token: 0x06001AF7 RID: 6903 RVA: 0x005E9B88 File Offset: 0x005E9B88
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkPicture left, ClickyInkPicture right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001AF8 RID: 6904 RVA: 0x005E9B9C File Offset: 0x005E9B9C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ImageSource>.Default.GetHashCode(this.<Color>k__BackingField)) * -1521134295 + EqualityComparer<ImageSource>.Default.GetHashCode(this.<Gray>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PixelWidth>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PixelHeight>k__BackingField);
    }

    // Token: 0x06001AF9 RID: 6905 RVA: 0x005E9C18 File Offset: 0x005E9C18
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkPicture);
    }

    // Token: 0x06001AFA RID: 6906 RVA: 0x005E9C28 File Offset: 0x005E9C28
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkPicture other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ImageSource>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField) && EqualityComparer<ImageSource>.Default.Equals(this.<Gray>k__BackingField, other.<Gray>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PixelWidth>k__BackingField, other.<PixelWidth>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PixelHeight>k__BackingField, other.<PixelHeight>k__BackingField));
    }

    // Token: 0x06001AFC RID: 6908 RVA: 0x005E9CBC File Offset: 0x005E9CBC
    [CompilerGenerated]
    private ClickyInkPicture(ClickyInkPicture original)
    {
      this.Color = original.<Color>k__BackingField;
      this.Gray = original.<Gray>k__BackingField;
      this.PixelWidth = original.<PixelWidth>k__BackingField;
      this.PixelHeight = original.<PixelHeight>k__BackingField;
    }

    // Token: 0x06001AFD RID: 6909 RVA: 0x005E9CF4 File Offset: 0x005E9CF4
    [CompilerGenerated]
    public void Deconstruct(out ImageSource Color, out ImageSource Gray, out int PixelWidth, out int PixelHeight)
    {
      Color = this.Color;
      Gray = this.Gray;
      PixelWidth = this.PixelWidth;
      PixelHeight = this.PixelHeight;
    }
  }
}
