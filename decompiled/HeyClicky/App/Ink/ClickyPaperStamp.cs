using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001CB RID: 459
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyPaperStamp : Grid
  {
    // Token: 0x060017CD RID: 6093 RVA: 0x005C60B0 File Offset: 0x005C60B0
    public ClickyPaperStamp(ClickyThingStatus status, string word = null, double fontSize = 9.5, double rotationDegrees = -6.0)
    {
      ClickyPaperTextures.StampImage stampImage = ClickyPaperTextures.Stamp((word ?? status.StampWord()).ToUpperInvariant(), status.StampInk(), fontSize);
      this.StampSize = stampImage.Size;
      base.Width = stampImage.Size.Width;
      base.Height = stampImage.Size.Height;
      base.IsHitTestVisible = false;
      base.Children.Add(new Image
      {
        Source = stampImage.Bitmap,
        Width = stampImage.Size.Width,
        Height = stampImage.Size.Height,
        Stretch = Stretch.Fill,
        Opacity = 0.9
      });
      base.RenderTransformOrigin = new Point(0.5, 0.5);
      base.RenderTransform = new RotateTransform
      {
        Angle = rotationDegrees
      };
      string text = word ?? status.StampWord();
      AutomationProperties.SetName(this, word ?? CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text.ToLowerInvariant()));
    }

    // Token: 0x1700043F RID: 1087
    // (get) Token: 0x060017CE RID: 6094 RVA: 0x005C61D4 File Offset: 0x005C61D4
    public Size StampSize { get; }

    // Token: 0x060017CF RID: 6095 RVA: 0x005C61DC File Offset: 0x005C61DC
    public static Size SizeOf(ClickyThingStatus status, string word = null, double fontSize = 9.5)
    {
      return ClickyPaperTextures.Stamp((word ?? status.StampWord()).ToUpperInvariant(), status.StampInk(), fontSize).Size;
    }
  }
}
