using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Design
{
  // Token: 0x0200034F RID: 847
  public static class DS
  {
    // Token: 0x06002401 RID: 9217 RVA: 0x00630940 File Offset: 0x00630940
    public static Color Hex(uint rgb, double alpha = 1.0)
    {
      return Color.FromArgb((byte)Math.Round(alpha * 255.0), (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
    }

    // Token: 0x06002402 RID: 9218 RVA: 0x00630964 File Offset: 0x00630964
    public static Color White(double alpha)
    {
      return Color.FromArgb((byte)Math.Round(alpha * 255.0), byte.MaxValue, byte.MaxValue, byte.MaxValue);
    }

    // Token: 0x06002403 RID: 9219 RVA: 0x0063098C File Offset: 0x0063098C
    public static Color Black(double alpha)
    {
      return Color.FromArgb((byte)Math.Round(alpha * 255.0), 0, 0, 0);
    }

    // Token: 0x06002404 RID: 9220 RVA: 0x006309A8 File Offset: 0x006309A8
    public static Color Rgb(double r, double g, double b, double a = 1.0)
    {
      return Color.FromArgb((byte)Math.Round(a * 255.0), (byte)Math.Round(r * 255.0), (byte)Math.Round(g * 255.0), (byte)Math.Round(b * 255.0));
    }

    // Token: 0x06002405 RID: 9221 RVA: 0x00630A00 File Offset: 0x00630A00
    public static Color WithAlpha(this Color c, double alpha)
    {
      return Color.FromArgb((byte)Math.Round(alpha * (double)c.A), c.R, c.G, c.B);
    }

    // Token: 0x06002406 RID: 9222 RVA: 0x00630A2C File Offset: 0x00630A2C
    [NullableContext(2)]
    public static Color CursorColor(string hexOrName)
    {
      if (string.IsNullOrWhiteSpace(hexOrName))
      {
        return DS.Colors.CursorBlue;
      }
      string text = hexOrName.Trim().TrimStart('#');
      string text2 = text.ToLowerInvariant();
      Color color;
      if (!(text2 == "red"))
      {
        if (!(text2 == "blue"))
        {
          if (!(text2 == "yellow"))
          {
            if (!(text2 == "green"))
            {
              uint num;
              color = ((text.Length == 6 && uint.TryParse(text, NumberStyles.HexNumber, null, out num)) ? DS.Hex(num, 1.0) : DS.Colors.CursorBlue);
            }
            else
            {
              color = DS.Colors.CursorGreen;
            }
          }
          else
          {
            color = DS.Colors.CursorYellow;
          }
        }
        else
        {
          color = DS.Colors.CursorBlue;
        }
      }
      else
      {
        color = DS.Colors.CursorRed;
      }
      return color;
    }

    // Token: 0x06002407 RID: 9223 RVA: 0x00630AE4 File Offset: 0x00630AE4
    [NullableContext(1)]
    public static string ToHex(Color c)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 3);
      defaultInterpolatedStringHandler.AppendLiteral("#");
      defaultInterpolatedStringHandler.AppendFormatted<byte>(c.R, "X2");
      defaultInterpolatedStringHandler.AppendFormatted<byte>(c.G, "X2");
      defaultInterpolatedStringHandler.AppendFormatted<byte>(c.B, "X2");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x020009A2 RID: 2466
    public static class Colors
    {
      // Token: 0x04002832 RID: 10290
      public static readonly Color Background = DS.Hex(1645087U, 1.0);

      // Token: 0x04002833 RID: 10291
      public static readonly Color Surface1 = DS.Hex(2039845U, 1.0);

      // Token: 0x04002834 RID: 10292
      public static readonly Color Surface2 = DS.Hex(2500397U, 1.0);

      // Token: 0x04002835 RID: 10293
      public static readonly Color NotchCardSurface = DS.Hex(1710618U, 1.0);

      // Token: 0x04002836 RID: 10294
      public static readonly Color TextPrimary = DS.Hex(15527151U, 1.0);

      // Token: 0x04002837 RID: 10295
      public static readonly Color TextSecondary = DS.Hex(9474976U, 1.0);

      // Token: 0x04002838 RID: 10296
      public static readonly Color TextTertiary = DS.Hex(6053482U, 1.0);

      // Token: 0x04002839 RID: 10297
      public static readonly Color Success = DS.Hex(366185U, 1.0);

      // Token: 0x0400283A RID: 10298
      public static readonly Color Destructive = DS.Hex(15026253U, 1.0);

      // Token: 0x0400283B RID: 10299
      public static readonly Color Warning = DS.Hex(16757284U, 1.0);

      // Token: 0x0400283C RID: 10300
      public static readonly Color Blue300 = DS.Hex(9684477U, 1.0);

      // Token: 0x0400283D RID: 10301
      public static readonly Color Blue400 = DS.Hex(6333946U, 1.0);

      // Token: 0x0400283E RID: 10302
      public static readonly Color BorderStrong = DS.White(0.14);

      // Token: 0x0400283F RID: 10303
      public static readonly Color BorderSubtle = DS.White(0.08);

      // Token: 0x04002840 RID: 10304
      public static readonly Color Black = DS.Hex(0U, 1.0);

      // Token: 0x04002841 RID: 10305
      public static readonly Color Secondary = DS.White(0.55);

      // Token: 0x04002842 RID: 10306
      public static readonly Color CursorRed = DS.Hex(16731212U, 1.0);

      // Token: 0x04002843 RID: 10307
      public static readonly Color CursorBlue = DS.Hex(3375359U, 1.0);

      // Token: 0x04002844 RID: 10308
      public static readonly Color CursorYellow = DS.Hex(13934615U, 1.0);

      // Token: 0x04002845 RID: 10309
      public static readonly Color CursorGreen = DS.Hex(2540161U, 1.0);

      // Token: 0x04002846 RID: 10310
      public static readonly Color ListeningAccent = DS.Rgb(0.4, 0.95, 1.0, 1.0);

      // Token: 0x04002847 RID: 10311
      public static readonly Color ThinkingAccent = DS.Rgb(1.0, 0.5, 0.9, 1.0);

      // Token: 0x04002848 RID: 10312
      public static readonly Color SpeakingAccent = DS.Rgb(1.0, 0.65, 0.35, 1.0);

      // Token: 0x04002849 RID: 10313
      public static readonly Color DictationAccent = DS.Rgb(0.129, 0.773, 0.369, 1.0);

      // Token: 0x0400284A RID: 10314
      public static readonly Color GelDisabledContent = DS.Hex(3425626U, 1.0);
    }

    // Token: 0x020009A3 RID: 2467
    public static class Spacing
    {
      // Token: 0x0400284B RID: 10315
      public const double Xxs = 2.0;

      // Token: 0x0400284C RID: 10316
      public const double Xs = 4.0;

      // Token: 0x0400284D RID: 10317
      public const double Sm = 8.0;

      // Token: 0x0400284E RID: 10318
      public const double Md = 12.0;

      // Token: 0x0400284F RID: 10319
      public const double Lg = 16.0;

      // Token: 0x04002850 RID: 10320
      public const double Xl = 20.0;

      // Token: 0x04002851 RID: 10321
      public const double Xxl = 24.0;

      // Token: 0x04002852 RID: 10322
      public const double Xxxl = 32.0;
    }

    // Token: 0x020009A4 RID: 2468
    [NullableContext(1)]
    [Nullable(0)]
    public static class Fonts
    {
      // Token: 0x06003DE0 RID: 15840 RVA: 0x0069BB84 File Offset: 0x0069BB84
      public static FontFamily For(double size)
      {
        if (size < 12.0)
        {
          return DS.Fonts.Small;
        }
        if (size < 20.0)
        {
          return DS.Fonts.Text;
        }
        return DS.Fonts.Display;
      }

      // Token: 0x04002853 RID: 10323
      public static readonly FontFamily Small = new FontFamily("Segoe UI Variable Small");

      // Token: 0x04002854 RID: 10324
      public static readonly FontFamily Text = new FontFamily("Segoe UI Variable Text");

      // Token: 0x04002855 RID: 10325
      public static readonly FontFamily Display = new FontFamily("Segoe UI Variable Display");

      // Token: 0x04002856 RID: 10326
      public static readonly FontFamily Mono = new FontFamily("Cascadia Mono, Consolas");

      // Token: 0x04002857 RID: 10327
      public static readonly FontFamily Symbols = new FontFamily("Segoe Fluent Icons, Segoe MDL2 Assets");
    }

    // Token: 0x020009A5 RID: 2469
    public static class Weights
    {
      // Token: 0x04002858 RID: 10328
      public static readonly FontWeight Regular = FontWeights.Normal;

      // Token: 0x04002859 RID: 10329
      public static readonly FontWeight Medium = FontWeights.Medium;

      // Token: 0x0400285A RID: 10330
      public static readonly FontWeight Semibold = FontWeights.SemiBold;

      // Token: 0x0400285B RID: 10331
      public static readonly FontWeight Bold = FontWeights.Bold;
    }
  }
}
