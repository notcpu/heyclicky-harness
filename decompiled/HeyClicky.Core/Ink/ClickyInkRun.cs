using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000183 RID: 387
  [NullableContext(1)]
  [Nullable(0)]
  public readonly struct ClickyInkRun : IEquatable<ClickyInkRun>
  {
    // Token: 0x06001728 RID: 5928 RVA: 0x003C266C File Offset: 0x003C266C
    public ClickyInkRun(string Text, Color? Foreground = null, bool IsBold = false, Color? Background = null, Color? Strike = null, bool IsLink = false, Color? LinkTint = null, [Nullable(2)] string Symbol = null)
    {
      this.Text = Text;
      this.Foreground = Foreground;
      this.IsBold = IsBold;
      this.Background = Background;
      this.Strike = Strike;
      this.IsLink = IsLink;
      this.LinkTint = LinkTint;
      this.Symbol = Symbol;
    }

    // Token: 0x17000540 RID: 1344
    // (get) Token: 0x06001729 RID: 5929 RVA: 0x003C26AC File Offset: 0x003C26AC
    // (set) Token: 0x0600172A RID: 5930 RVA: 0x003C26B4 File Offset: 0x003C26B4
    public string Text { get; set; }

    // Token: 0x17000541 RID: 1345
    // (get) Token: 0x0600172B RID: 5931 RVA: 0x003C26C0 File Offset: 0x003C26C0
    // (set) Token: 0x0600172C RID: 5932 RVA: 0x003C26C8 File Offset: 0x003C26C8
    public Color? Foreground { get; set; }

    // Token: 0x17000542 RID: 1346
    // (get) Token: 0x0600172D RID: 5933 RVA: 0x003C26D4 File Offset: 0x003C26D4
    // (set) Token: 0x0600172E RID: 5934 RVA: 0x003C26DC File Offset: 0x003C26DC
    public bool IsBold { get; set; }

    // Token: 0x17000543 RID: 1347
    // (get) Token: 0x0600172F RID: 5935 RVA: 0x003C26E8 File Offset: 0x003C26E8
    // (set) Token: 0x06001730 RID: 5936 RVA: 0x003C26F0 File Offset: 0x003C26F0
    public Color? Background { get; set; }

    // Token: 0x17000544 RID: 1348
    // (get) Token: 0x06001731 RID: 5937 RVA: 0x003C26FC File Offset: 0x003C26FC
    // (set) Token: 0x06001732 RID: 5938 RVA: 0x003C2704 File Offset: 0x003C2704
    public Color? Strike { get; set; }

    // Token: 0x17000545 RID: 1349
    // (get) Token: 0x06001733 RID: 5939 RVA: 0x003C2710 File Offset: 0x003C2710
    // (set) Token: 0x06001734 RID: 5940 RVA: 0x003C2718 File Offset: 0x003C2718
    public bool IsLink { get; set; }

    // Token: 0x17000546 RID: 1350
    // (get) Token: 0x06001735 RID: 5941 RVA: 0x003C2724 File Offset: 0x003C2724
    // (set) Token: 0x06001736 RID: 5942 RVA: 0x003C272C File Offset: 0x003C272C
    public Color? LinkTint { get; set; }

    // Token: 0x17000547 RID: 1351
    // (get) Token: 0x06001737 RID: 5943 RVA: 0x003C2738 File Offset: 0x003C2738
    // (set) Token: 0x06001738 RID: 5944 RVA: 0x003C2740 File Offset: 0x003C2740
    [Nullable(2)]
    public string Symbol
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000548 RID: 1352
    // (get) Token: 0x06001739 RID: 5945 RVA: 0x003C274C File Offset: 0x003C274C
    public bool IsClear
    {
      get
      {
        Color? foreground = this.Foreground;
        return foreground != null && foreground.GetValueOrDefault().A == 0;
      }
    }

    // Token: 0x0600173A RID: 5946 RVA: 0x003C2780 File Offset: 0x003C2780
    [NullableContext(0)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkRun");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600173B RID: 5947 RVA: 0x003C27CC File Offset: 0x003C27CC
    [NullableContext(0)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Text = ");
      builder.Append(this.Text);
      builder.Append(", Foreground = ");
      builder.Append(this.Foreground.ToString());
      builder.Append(", IsBold = ");
      builder.Append(this.IsBold.ToString());
      builder.Append(", Background = ");
      builder.Append(this.Background.ToString());
      builder.Append(", Strike = ");
      builder.Append(this.Strike.ToString());
      builder.Append(", IsLink = ");
      builder.Append(this.IsLink.ToString());
      builder.Append(", LinkTint = ");
      builder.Append(this.LinkTint.ToString());
      builder.Append(", Symbol = ");
      builder.Append(this.Symbol);
      builder.Append(", IsClear = ");
      builder.Append(this.IsClear.ToString());
      return true;
    }

    // Token: 0x0600173C RID: 5948 RVA: 0x003C2920 File Offset: 0x003C2920
    [CompilerGenerated]
    public static bool operator !=(ClickyInkRun left, ClickyInkRun right)
    {
      return !(left == right);
    }

    // Token: 0x0600173D RID: 5949 RVA: 0x003C292C File Offset: 0x003C292C
    [CompilerGenerated]
    public static bool operator ==(ClickyInkRun left, ClickyInkRun right)
    {
      return left.Equals(right);
    }

    // Token: 0x0600173E RID: 5950 RVA: 0x003C2938 File Offset: 0x003C2938
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField) * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(this.<Foreground>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsBold>k__BackingField)) * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(this.<Background>k__BackingField)) * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(this.<Strike>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsLink>k__BackingField)) * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(this.<LinkTint>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Symbol>k__BackingField);
    }

    // Token: 0x0600173F RID: 5951 RVA: 0x003C29F8 File Offset: 0x003C29F8
    [NullableContext(0)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ClickyInkRun && this.Equals((ClickyInkRun)obj);
    }

    // Token: 0x06001740 RID: 5952 RVA: 0x003C2A10 File Offset: 0x003C2A10
    [CompilerGenerated]
    public bool Equals(ClickyInkRun other)
    {
      return EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<Color?>.Default.Equals(this.<Foreground>k__BackingField, other.<Foreground>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsBold>k__BackingField, other.<IsBold>k__BackingField) && EqualityComparer<Color?>.Default.Equals(this.<Background>k__BackingField, other.<Background>k__BackingField) && EqualityComparer<Color?>.Default.Equals(this.<Strike>k__BackingField, other.<Strike>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsLink>k__BackingField, other.<IsLink>k__BackingField) && EqualityComparer<Color?>.Default.Equals(this.<LinkTint>k__BackingField, other.<LinkTint>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Symbol>k__BackingField, other.<Symbol>k__BackingField);
    }

    // Token: 0x06001741 RID: 5953 RVA: 0x003C2AE4 File Offset: 0x003C2AE4
    [CompilerGenerated]
    public void Deconstruct(out string Text, out Color? Foreground, out bool IsBold, out Color? Background, out Color? Strike, out bool IsLink, out Color? LinkTint, [Nullable(2)] out string Symbol)
    {
      Text = this.Text;
      Foreground = this.Foreground;
      IsBold = this.IsBold;
      Background = this.Background;
      Strike = this.Strike;
      IsLink = this.IsLink;
      LinkTint = this.LinkTint;
      Symbol = this.Symbol;
    }

    // Token: 0x04000A0D RID: 2573
    public static readonly Color Clear = Color.FromArgb(0, 0, 0, 0);
  }
}
