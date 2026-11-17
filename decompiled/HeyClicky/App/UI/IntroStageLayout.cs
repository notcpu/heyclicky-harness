using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B2 RID: 178
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class IntroStageLayout : Panel
  {
    // Token: 0x1700012B RID: 299
    // (get) Token: 0x06000749 RID: 1865 RVA: 0x0053700C File Offset: 0x0053700C
    // (set) Token: 0x0600074A RID: 1866 RVA: 0x00537014 File Offset: 0x00537014
    public UIElement Header { get; set; }

    // Token: 0x1700012C RID: 300
    // (get) Token: 0x0600074B RID: 1867 RVA: 0x00537020 File Offset: 0x00537020
    // (set) Token: 0x0600074C RID: 1868 RVA: 0x00537028 File Offset: 0x00537028
    public UIElement Body { get; set; }

    // Token: 0x1700012D RID: 301
    // (get) Token: 0x0600074D RID: 1869 RVA: 0x00537034 File Offset: 0x00537034
    // (set) Token: 0x0600074E RID: 1870 RVA: 0x0053703C File Offset: 0x0053703C
    public UIElement Footer { get; set; }

    // Token: 0x1700012E RID: 302
    // (get) Token: 0x0600074F RID: 1871 RVA: 0x00537048 File Offset: 0x00537048
    // (set) Token: 0x06000750 RID: 1872 RVA: 0x00537050 File Offset: 0x00537050
    public bool HasFooter { get; set; } = true;

    // Token: 0x1700012F RID: 303
    // (get) Token: 0x06000751 RID: 1873 RVA: 0x0053705C File Offset: 0x0053705C
    // (set) Token: 0x06000752 RID: 1874 RVA: 0x00537064 File Offset: 0x00537064
    public double MinimumHeight { get; set; }

    // Token: 0x17000130 RID: 304
    // (get) Token: 0x06000753 RID: 1875 RVA: 0x00537070 File Offset: 0x00537070
    private double ContentOffset
    {
      get
      {
        return (double)(this.HasFooter ? 0 : 24);
      }
    }

    // Token: 0x06000754 RID: 1876 RVA: 0x00537080 File Offset: 0x00537080
    [NullableContext(1)]
    public void SetParts(UIElement header, UIElement body, [Nullable(2)] UIElement footer, bool hasFooter)
    {
      base.Children.Clear();
      this.Header = header;
      this.Body = body;
      this.Footer = footer;
      this.HasFooter = hasFooter;
      base.Children.Add(header);
      base.Children.Add(body);
      if (footer != null)
      {
        base.Children.Add(footer);
      }
      base.InvalidateMeasure();
    }

    // Token: 0x06000755 RID: 1877 RVA: 0x005370E4 File Offset: 0x005370E4
    protected override Size MeasureOverride(Size available)
    {
      double num = (double.IsInfinity(available.Width) ? 392.0 : available.Width);
      Size size = new Size(num, double.PositiveInfinity);
      UIElement header = this.Header;
      if (header != null)
      {
        header.Measure(size);
      }
      UIElement body = this.Body;
      if (body != null)
      {
        body.Measure(size);
      }
      UIElement footer = this.Footer;
      if (footer != null)
      {
        footer.Measure(size);
      }
      UIElement header2 = this.Header;
      double num2 = ((header2 != null) ? header2.DesiredSize.Height : 0.0);
      double num3 = ((this.HasFooter && this.Footer != null) ? (this.Footer.DesiredSize.Height + 16.0) : 0.0);
      double num4 = num2 - this.ContentOffset;
      double num5 = num3 + this.ContentOffset;
      UIElement body2 = this.Body;
      double num6 = ((body2 != null) ? body2.DesiredSize.Height : 0.0);
      double num7 = Math.Max(this.MinimumHeight, num6 + 2.0 * Math.Max(num4, num5));
      return new Size(num, num7);
    }

    // Token: 0x06000756 RID: 1878 RVA: 0x00537218 File Offset: 0x00537218
    protected override Size ArrangeOverride(Size final)
    {
      UIElement header = this.Header;
      if (header != null)
      {
        header.Arrange(new Rect(0.0, 0.0, final.Width, this.Header.DesiredSize.Height));
      }
      if (this.Body != null)
      {
        Size desiredSize = this.Body.DesiredSize;
        double num = final.Height / 2.0 + this.ContentOffset;
        this.Body.Arrange(new Rect(0.0, num - desiredSize.Height / 2.0, final.Width, desiredSize.Height));
      }
      if (this.Footer != null)
      {
        Size desiredSize2 = this.Footer.DesiredSize;
        this.Footer.Arrange(new Rect(0.0, final.Height - 16.0 - desiredSize2.Height, final.Width, desiredSize2.Height));
      }
      return final;
    }

    // Token: 0x04000232 RID: 562
    private const double FooterBottomInset = 16.0;
  }
}
