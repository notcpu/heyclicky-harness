using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using Windows.UI;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002D3 RID: 723
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownViewerPalette : IEquatable<MarkdownViewerPalette>
  {
    // Token: 0x06001E65 RID: 7781 RVA: 0x00606FB8 File Offset: 0x00606FB8
    public MarkdownViewerPalette(bool IsDark, Color PageBackground, Color SecondaryTextColor, Color RaisedChipBackgroundColor, Color HeadingText, Color BodyText, Color SecondaryText, Color LinkText, Color InlineCodeBackground, Color CodeBlockBackground, Color Hairline, Color QuotePlateFill, Color QuotePlateStroke, Color QuotePlateBar, Color RaisedChipBackground)
    {
      this.IsDark = IsDark;
      this.PageBackground = PageBackground;
      this.SecondaryTextColor = SecondaryTextColor;
      this.RaisedChipBackgroundColor = RaisedChipBackgroundColor;
      this.HeadingText = HeadingText;
      this.BodyText = BodyText;
      this.SecondaryText = SecondaryText;
      this.LinkText = LinkText;
      this.InlineCodeBackground = InlineCodeBackground;
      this.CodeBlockBackground = CodeBlockBackground;
      this.Hairline = Hairline;
      this.QuotePlateFill = QuotePlateFill;
      this.QuotePlateStroke = QuotePlateStroke;
      this.QuotePlateBar = QuotePlateBar;
      this.RaisedChipBackground = RaisedChipBackground;
      base..ctor();
    }

    // Token: 0x1700056D RID: 1389
    // (get) Token: 0x06001E66 RID: 7782 RVA: 0x00607040 File Offset: 0x00607040
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MarkdownViewerPalette);
      }
    }

    // Token: 0x1700056E RID: 1390
    // (get) Token: 0x06001E67 RID: 7783 RVA: 0x0060704C File Offset: 0x0060704C
    // (set) Token: 0x06001E68 RID: 7784 RVA: 0x00607054 File Offset: 0x00607054
    public bool IsDark { get; set; }

    // Token: 0x1700056F RID: 1391
    // (get) Token: 0x06001E69 RID: 7785 RVA: 0x00607060 File Offset: 0x00607060
    // (set) Token: 0x06001E6A RID: 7786 RVA: 0x00607068 File Offset: 0x00607068
    public Color PageBackground { get; set; }

    // Token: 0x17000570 RID: 1392
    // (get) Token: 0x06001E6B RID: 7787 RVA: 0x00607074 File Offset: 0x00607074
    // (set) Token: 0x06001E6C RID: 7788 RVA: 0x0060707C File Offset: 0x0060707C
    public Color SecondaryTextColor { get; set; }

    // Token: 0x17000571 RID: 1393
    // (get) Token: 0x06001E6D RID: 7789 RVA: 0x00607088 File Offset: 0x00607088
    // (set) Token: 0x06001E6E RID: 7790 RVA: 0x00607090 File Offset: 0x00607090
    public Color RaisedChipBackgroundColor { get; set; }

    // Token: 0x17000572 RID: 1394
    // (get) Token: 0x06001E6F RID: 7791 RVA: 0x0060709C File Offset: 0x0060709C
    // (set) Token: 0x06001E70 RID: 7792 RVA: 0x006070A4 File Offset: 0x006070A4
    public Color HeadingText { get; set; }

    // Token: 0x17000573 RID: 1395
    // (get) Token: 0x06001E71 RID: 7793 RVA: 0x006070B0 File Offset: 0x006070B0
    // (set) Token: 0x06001E72 RID: 7794 RVA: 0x006070B8 File Offset: 0x006070B8
    public Color BodyText { get; set; }

    // Token: 0x17000574 RID: 1396
    // (get) Token: 0x06001E73 RID: 7795 RVA: 0x006070C4 File Offset: 0x006070C4
    // (set) Token: 0x06001E74 RID: 7796 RVA: 0x006070CC File Offset: 0x006070CC
    public Color SecondaryText { get; set; }

    // Token: 0x17000575 RID: 1397
    // (get) Token: 0x06001E75 RID: 7797 RVA: 0x006070D8 File Offset: 0x006070D8
    // (set) Token: 0x06001E76 RID: 7798 RVA: 0x006070E0 File Offset: 0x006070E0
    public Color LinkText { get; set; }

    // Token: 0x17000576 RID: 1398
    // (get) Token: 0x06001E77 RID: 7799 RVA: 0x006070EC File Offset: 0x006070EC
    // (set) Token: 0x06001E78 RID: 7800 RVA: 0x006070F4 File Offset: 0x006070F4
    public Color InlineCodeBackground { get; set; }

    // Token: 0x17000577 RID: 1399
    // (get) Token: 0x06001E79 RID: 7801 RVA: 0x00607100 File Offset: 0x00607100
    // (set) Token: 0x06001E7A RID: 7802 RVA: 0x00607108 File Offset: 0x00607108
    public Color CodeBlockBackground { get; set; }

    // Token: 0x17000578 RID: 1400
    // (get) Token: 0x06001E7B RID: 7803 RVA: 0x00607114 File Offset: 0x00607114
    // (set) Token: 0x06001E7C RID: 7804 RVA: 0x0060711C File Offset: 0x0060711C
    public Color Hairline { get; set; }

    // Token: 0x17000579 RID: 1401
    // (get) Token: 0x06001E7D RID: 7805 RVA: 0x00607128 File Offset: 0x00607128
    // (set) Token: 0x06001E7E RID: 7806 RVA: 0x00607130 File Offset: 0x00607130
    public Color QuotePlateFill { get; set; }

    // Token: 0x1700057A RID: 1402
    // (get) Token: 0x06001E7F RID: 7807 RVA: 0x0060713C File Offset: 0x0060713C
    // (set) Token: 0x06001E80 RID: 7808 RVA: 0x00607144 File Offset: 0x00607144
    public Color QuotePlateStroke { get; set; }

    // Token: 0x1700057B RID: 1403
    // (get) Token: 0x06001E81 RID: 7809 RVA: 0x00607150 File Offset: 0x00607150
    // (set) Token: 0x06001E82 RID: 7810 RVA: 0x00607158 File Offset: 0x00607158
    public Color QuotePlateBar { get; set; }

    // Token: 0x1700057C RID: 1404
    // (get) Token: 0x06001E83 RID: 7811 RVA: 0x00607164 File Offset: 0x00607164
    // (set) Token: 0x06001E84 RID: 7812 RVA: 0x0060716C File Offset: 0x0060716C
    public Color RaisedChipBackground { get; set; }

    // Token: 0x06001E85 RID: 7813 RVA: 0x00607178 File Offset: 0x00607178
    private static Color Grey(double white)
    {
      return DS.Rgb(white, white, white, 1.0);
    }

    // Token: 0x06001E86 RID: 7814 RVA: 0x0060718C File Offset: 0x0060718C
    public static MarkdownViewerPalette For(MessagesPalette look)
    {
      if (!look.IsDark)
      {
        return MarkdownViewerPalette.Light;
      }
      return MarkdownViewerPalette.Dark;
    }

    // Token: 0x1700057D RID: 1405
    // (get) Token: 0x06001E87 RID: 7815 RVA: 0x006071A4 File Offset: 0x006071A4
    public static MarkdownViewerPalette Current
    {
      get
      {
        return MarkdownViewerPalette.For(MessagesPalette.Current);
      }
    }

    // Token: 0x06001E88 RID: 7816 RVA: 0x006071B0 File Offset: 0x006071B0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MarkdownViewerPalette");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001E89 RID: 7817 RVA: 0x006071FC File Offset: 0x006071FC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("IsDark = ");
      builder.Append(this.IsDark.ToString());
      builder.Append(", PageBackground = ");
      builder.Append(this.PageBackground.ToString());
      builder.Append(", SecondaryTextColor = ");
      builder.Append(this.SecondaryTextColor.ToString());
      builder.Append(", RaisedChipBackgroundColor = ");
      builder.Append(this.RaisedChipBackgroundColor.ToString());
      builder.Append(", HeadingText = ");
      builder.Append(this.HeadingText.ToString());
      builder.Append(", BodyText = ");
      builder.Append(this.BodyText.ToString());
      builder.Append(", SecondaryText = ");
      builder.Append(this.SecondaryText.ToString());
      builder.Append(", LinkText = ");
      builder.Append(this.LinkText.ToString());
      builder.Append(", InlineCodeBackground = ");
      builder.Append(this.InlineCodeBackground.ToString());
      builder.Append(", CodeBlockBackground = ");
      builder.Append(this.CodeBlockBackground.ToString());
      builder.Append(", Hairline = ");
      builder.Append(this.Hairline.ToString());
      builder.Append(", QuotePlateFill = ");
      builder.Append(this.QuotePlateFill.ToString());
      builder.Append(", QuotePlateStroke = ");
      builder.Append(this.QuotePlateStroke.ToString());
      builder.Append(", QuotePlateBar = ");
      builder.Append(this.QuotePlateBar.ToString());
      builder.Append(", RaisedChipBackground = ");
      builder.Append(this.RaisedChipBackground.ToString());
      return true;
    }

    // Token: 0x06001E8A RID: 7818 RVA: 0x00607458 File Offset: 0x00607458
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MarkdownViewerPalette left, MarkdownViewerPalette right)
    {
      return !(left == right);
    }

    // Token: 0x06001E8B RID: 7819 RVA: 0x00607464 File Offset: 0x00607464
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MarkdownViewerPalette left, MarkdownViewerPalette right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001E8C RID: 7820 RVA: 0x00607478 File Offset: 0x00607478
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDark>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PageBackground>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SecondaryTextColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<RaisedChipBackgroundColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<HeadingText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<BodyText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SecondaryText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<LinkText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<InlineCodeBackground>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<CodeBlockBackground>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Hairline>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<QuotePlateFill>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<QuotePlateStroke>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<QuotePlateBar>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<RaisedChipBackground>k__BackingField);
    }

    // Token: 0x06001E8D RID: 7821 RVA: 0x006075F0 File Offset: 0x006075F0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MarkdownViewerPalette);
    }

    // Token: 0x06001E8E RID: 7822 RVA: 0x00607600 File Offset: 0x00607600
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(MarkdownViewerPalette other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<IsDark>k__BackingField, other.<IsDark>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<PageBackground>k__BackingField, other.<PageBackground>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SecondaryTextColor>k__BackingField, other.<SecondaryTextColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<RaisedChipBackgroundColor>k__BackingField, other.<RaisedChipBackgroundColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<HeadingText>k__BackingField, other.<HeadingText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<BodyText>k__BackingField, other.<BodyText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SecondaryText>k__BackingField, other.<SecondaryText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<LinkText>k__BackingField, other.<LinkText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<InlineCodeBackground>k__BackingField, other.<InlineCodeBackground>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<CodeBlockBackground>k__BackingField, other.<CodeBlockBackground>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Hairline>k__BackingField, other.<Hairline>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<QuotePlateFill>k__BackingField, other.<QuotePlateFill>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<QuotePlateStroke>k__BackingField, other.<QuotePlateStroke>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<QuotePlateBar>k__BackingField, other.<QuotePlateBar>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<RaisedChipBackground>k__BackingField, other.<RaisedChipBackground>k__BackingField));
    }

    // Token: 0x06001E90 RID: 7824 RVA: 0x006077C0 File Offset: 0x006077C0
    [CompilerGenerated]
    private MarkdownViewerPalette(MarkdownViewerPalette original)
    {
      this.IsDark = original.<IsDark>k__BackingField;
      this.PageBackground = original.<PageBackground>k__BackingField;
      this.SecondaryTextColor = original.<SecondaryTextColor>k__BackingField;
      this.RaisedChipBackgroundColor = original.<RaisedChipBackgroundColor>k__BackingField;
      this.HeadingText = original.<HeadingText>k__BackingField;
      this.BodyText = original.<BodyText>k__BackingField;
      this.SecondaryText = original.<SecondaryText>k__BackingField;
      this.LinkText = original.<LinkText>k__BackingField;
      this.InlineCodeBackground = original.<InlineCodeBackground>k__BackingField;
      this.CodeBlockBackground = original.<CodeBlockBackground>k__BackingField;
      this.Hairline = original.<Hairline>k__BackingField;
      this.QuotePlateFill = original.<QuotePlateFill>k__BackingField;
      this.QuotePlateStroke = original.<QuotePlateStroke>k__BackingField;
      this.QuotePlateBar = original.<QuotePlateBar>k__BackingField;
      this.RaisedChipBackground = original.<RaisedChipBackground>k__BackingField;
    }

    // Token: 0x06001E91 RID: 7825 RVA: 0x00607888 File Offset: 0x00607888
    [CompilerGenerated]
    public void Deconstruct(out bool IsDark, out Color PageBackground, out Color SecondaryTextColor, out Color RaisedChipBackgroundColor, out Color HeadingText, out Color BodyText, out Color SecondaryText, out Color LinkText, out Color InlineCodeBackground, out Color CodeBlockBackground, out Color Hairline, out Color QuotePlateFill, out Color QuotePlateStroke, out Color QuotePlateBar, out Color RaisedChipBackground)
    {
      IsDark = this.IsDark;
      PageBackground = this.PageBackground;
      SecondaryTextColor = this.SecondaryTextColor;
      RaisedChipBackgroundColor = this.RaisedChipBackgroundColor;
      HeadingText = this.HeadingText;
      BodyText = this.BodyText;
      SecondaryText = this.SecondaryText;
      LinkText = this.LinkText;
      InlineCodeBackground = this.InlineCodeBackground;
      CodeBlockBackground = this.CodeBlockBackground;
      Hairline = this.Hairline;
      QuotePlateFill = this.QuotePlateFill;
      QuotePlateStroke = this.QuotePlateStroke;
      QuotePlateBar = this.QuotePlateBar;
      RaisedChipBackground = this.RaisedChipBackground;
    }

    // Token: 0x04000EB3 RID: 3763
    public static readonly MarkdownViewerPalette Dark = new MarkdownViewerPalette(true, MarkdownViewerPalette.Grey(0.105), MarkdownViewerPalette.Grey(0.56), MarkdownViewerPalette.Grey(0.17), MarkdownViewerPalette.Grey(0.97), MarkdownViewerPalette.Grey(0.88), MarkdownViewerPalette.Grey(0.56), DS.Rgb(0.42, 0.63, 0.98, 1.0), MarkdownViewerPalette.Grey(0.2), MarkdownViewerPalette.Grey(0.155), MarkdownViewerPalette.Grey(0.24), MarkdownViewerPalette.Grey(0.13725490196078433), MarkdownViewerPalette.Grey(0.2549019607843137), MarkdownViewerPalette.Grey(0.6), MarkdownViewerPalette.Grey(0.17));

    // Token: 0x04000EB4 RID: 3764
    public static readonly MarkdownViewerPalette Light = new MarkdownViewerPalette(false, MarkdownViewerPalette.Grey(0.995), MarkdownViewerPalette.Grey(0.45), MarkdownViewerPalette.Grey(0.93), MarkdownViewerPalette.Grey(0.08), MarkdownViewerPalette.Grey(0.16), MarkdownViewerPalette.Grey(0.45), DS.Rgb(0.0, 0.4, 0.86, 1.0), MarkdownViewerPalette.Grey(0.925), MarkdownViewerPalette.Grey(0.955), MarkdownViewerPalette.Grey(0.86), MarkdownViewerPalette.Grey(0.965), MarkdownViewerPalette.Grey(0.86), MarkdownViewerPalette.Grey(0.55), MarkdownViewerPalette.Grey(0.93));
  }
}
