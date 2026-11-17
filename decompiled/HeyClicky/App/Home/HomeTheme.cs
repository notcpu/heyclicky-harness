using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B2 RID: 690
  public static class HomeTheme
  {
    // Token: 0x06001C64 RID: 7268 RVA: 0x005FA084 File Offset: 0x005FA084
    public static Color SurfaceAt(double y)
    {
      double num = Math.Clamp((y - 14.0) / 130.0, 0.0, 1.0);
      double[] array = new double[] { 0.0, 0.22, 0.42, 0.64, 0.84, 1.0 };
      double[] array2 = new double[] { 0.0, 0.25, 0.5, 0.75, 0.92, 1.0 };
      double num2 = 1.0;
      for (int i = 1; i < array.Length; i++)
      {
        if (num <= array[i])
        {
          num2 = array2[i - 1] + (array2[i] - array2[i - 1]) * (num - array[i - 1]) / (array[i] - array[i - 1]);
          break;
        }
      }
      return DS.Rgb((double)HomeTheme.PaneFill.R / 255.0 * num2, (double)HomeTheme.PaneFill.G / 255.0 * num2, (double)HomeTheme.PaneFill.B / 255.0 * num2, 1.0);
    }

    // Token: 0x06001C65 RID: 7269 RVA: 0x005FA190 File Offset: 0x005FA190
    [NullableContext(1)]
    public static LinearGradientBrush TopBandFade(double height)
    {
      List<ValueTuple<double, Color>> list = new List<ValueTuple<double, Color>>();
      foreach (double num in new int[] { 0, 14, 30, 45, 60, 80, 100, 120, 144 })
      {
        if (num < height * 0.75)
        {
          list.Add(new ValueTuple<double, Color>(num / height, HomeTheme.SurfaceAt(num)));
        }
      }
      list.Add(new ValueTuple<double, Color>(0.75, HomeTheme.SurfaceAt(height * 0.75)));
      list.Add(new ValueTuple<double, Color>(1.0, HomeTheme.SurfaceAt(height).WithAlpha(0.0)));
      return Ui.Vertical(list.ToArray());
    }

    // Token: 0x06001C66 RID: 7270 RVA: 0x005FA244 File Offset: 0x005FA244
    [return: TupleElementNames(new string[] { "W", "H" })]
    public static ValueTuple<double, double> FullSurface(double vw, double vh)
    {
      double num = Math.Max(1250.0, Math.Round(vw * 0.7));
      double num2 = Math.Max(850.0, Math.Round(vh * 0.78));
      return new ValueTuple<double, double>(Math.Max(720.0, Math.Min(num, vw - 24.0)), Math.Max(520.0, Math.Min(num2, vh - 12.0)));
    }

    // Token: 0x06001C67 RID: 7271 RVA: 0x005FA2D4 File Offset: 0x005FA2D4
    [return: TupleElementNames(new string[] { "W", "H" })]
    public static ValueTuple<double, double> AttachedSize(double vw, double vh, [TupleElementNames(new string[] { "W", "H" })] ValueTuple<double, double>? saved)
    {
      ValueTuple<double, double> valueTuple = HomeTheme.FullSurface(vw, vh);
      return HomeTheme.Clamp(saved ?? new ValueTuple<double, double>(Math.Round(valueTuple.Item1 * 0.7), Math.Round(valueTuple.Item2 * 0.7)), vw, vh);
    }

    // Token: 0x06001C68 RID: 7272 RVA: 0x005FA334 File Offset: 0x005FA334
    [return: TupleElementNames(new string[] { "W", "H" })]
    public static ValueTuple<double, double> Clamp([TupleElementNames(new string[] { "W", "H" })] ValueTuple<double, double> req, double vw, double vh)
    {
      return new ValueTuple<double, double>(Math.Max(720.0, Math.Min(req.Item1, vw - 24.0)), Math.Max(520.0, Math.Min(req.Item2, vh - 12.0)));
    }

    // Token: 0x04000D82 RID: 3458
    public static readonly Color PaneFill = DS.Hex(1973790U, 1.0);

    // Token: 0x04000D83 RID: 3459
    public static readonly Color SidebarFill = DS.Hex(1842203U, 1.0);

    // Token: 0x04000D84 RID: 3460
    public static readonly Color SearchFieldFill = DS.Hex(2236961U, 1.0);

    // Token: 0x04000D85 RID: 3461
    public static readonly Color RowDivider = DS.Hex(3881787U, 1.0);

    // Token: 0x04000D86 RID: 3462
    public static readonly Color SelectionBlue = DS.Hex(37375U, 1.0);

    // Token: 0x04000D87 RID: 3463
    public static readonly Color IncomingBubble = DS.Hex(3881789U, 1.0);

    // Token: 0x04000D88 RID: 3464
    public static readonly Color ComposerFieldFill = DS.Hex(2302755U, 1.0);

    // Token: 0x04000D89 RID: 3465
    public static readonly Color ComposerFieldStroke = DS.Hex(4276545U, 1.0);

    // Token: 0x04000D8A RID: 3466
    public static readonly Color NamePillFill = DS.Hex(2697513U, 1.0);

    // Token: 0x04000D8B RID: 3467
    public static readonly Color AvatarPlate = DS.Hex(3815996U, 1.0);

    // Token: 0x04000D8C RID: 3468
    public static readonly Color PrimaryText = DS.Hex(16053492U, 1.0);

    // Token: 0x04000D8D RID: 3469
    public static readonly Color SecondaryText = DS.Hex(10790052U, 1.0);

    // Token: 0x04000D8E RID: 3470
    public static readonly Color TertiaryText = DS.Hex(10066329U, 1.0);

    // Token: 0x04000D8F RID: 3471
    public static readonly Color FailureRed = DS.Hex(16729402U, 1.0);

    // Token: 0x04000D90 RID: 3472
    public static readonly Color ToolbarGlyph = DS.Hex(14474460U, 1.0);

    // Token: 0x04000D91 RID: 3473
    public static readonly Color WindowControlGlyph = DS.Hex(7895162U, 1.0);

    // Token: 0x04000D92 RID: 3474
    public static readonly Color LiveOrange = DS.Hex(14251863U, 1.0);

    // Token: 0x04000D93 RID: 3475
    public static readonly Color ThemeTextPrimary = DS.Rgb(0.925, 0.925, 0.937, 1.0);

    // Token: 0x04000D94 RID: 3476
    public static readonly Color ThemeTextSecondary = DS.Rgb(0.565, 0.576, 0.627, 1.0);

    // Token: 0x04000D95 RID: 3477
    public static readonly Color ThemeTextTertiary = DS.Rgb(0.361, 0.369, 0.416, 1.0);

    // Token: 0x04000D96 RID: 3478
    [Nullable(1)]
    public static readonly Color[] CardPastels = new Color[]
    {
      DS.Hex(16772808U, 1.0),
      DS.Hex(14149119U, 1.0),
      DS.Hex(16763105U, 1.0),
      DS.Hex(13890272U, 1.0),
      DS.Hex(14999039U, 1.0),
      DS.Hex(16768200U, 1.0)
    };

    // Token: 0x04000D97 RID: 3479
    public const double SheetCornerRadius = 26.0;

    // Token: 0x04000D98 RID: 3480
    public const double DetachedCornerRadius = 12.0;

    // Token: 0x04000D99 RID: 3481
    public const double TopFlareRadius = 10.0;

    // Token: 0x04000D9A RID: 3482
    public const double CanvasSide = 16.0;

    // Token: 0x04000D9B RID: 3483
    public const double CanvasBottom = 40.0;

    // Token: 0x04000D9C RID: 3484
    public const double ShadowOpacity = 0.18;

    // Token: 0x04000D9D RID: 3485
    public const double ShadowRadius = 6.0;

    // Token: 0x04000D9E RID: 3486
    public const double ShadowY = 3.0;

    // Token: 0x04000D9F RID: 3487
    public const double AttachedScale = 0.7;

    // Token: 0x04000DA0 RID: 3488
    public const double MinWidth = 720.0;

    // Token: 0x04000DA1 RID: 3489
    public const double MinHeight = 520.0;

    // Token: 0x04000DA2 RID: 3490
    public const double OpenSeconds = 0.43;

    // Token: 0x04000DA3 RID: 3491
    public const double CloseSeconds = 0.3;

    // Token: 0x04000DA4 RID: 3492
    public const double ContentFadeSeconds = 0.18;

    // Token: 0x04000DA5 RID: 3493
    public const double HoverForgiveness = 28.0;

    // Token: 0x04000DA6 RID: 3494
    public const double HoverGraceSeconds = 0.15;

    // Token: 0x04000DA7 RID: 3495
    public static readonly TimeSpan RestoreWindow = TimeSpan.FromMinutes(4L);
  }
}
