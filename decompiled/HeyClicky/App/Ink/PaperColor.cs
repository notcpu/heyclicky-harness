using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001D9 RID: 473
  public static class PaperColor
  {
    // Token: 0x060017E8 RID: 6120 RVA: 0x005C7D18 File Offset: 0x005C7D18
    [NullableContext(1)]
    public static Color Hex(string hex)
    {
      string text = hex.Trim('#');
      ulong num;
      ulong.TryParse(text, NumberStyles.HexNumber, null, out num);
      bool flag = text.Length == 8;
      byte b = (byte)((num >> ((flag ? 24 : 16) & 63)) & 255UL);
      byte b2 = (byte)((num >> ((flag ? 16 : 8) & 63)) & 255UL);
      byte b3 = (byte)((num >> ((flag ? 8 : 0) & 63)) & 255UL);
      return Color.FromArgb(flag ? ((byte)(num & 255UL)) : byte.MaxValue, b, b2, b3);
    }

    // Token: 0x060017E9 RID: 6121 RVA: 0x005C7DA4 File Offset: 0x005C7DA4
    public static Color Opacity(this Color c, double opacity)
    {
      return Color.FromArgb((byte)Math.Round(Math.Clamp(opacity, 0.0, 1.0) * (double)c.A), c.R, c.G, c.B);
    }

    // Token: 0x060017EA RID: 6122 RVA: 0x005C7DF4 File Offset: 0x005C7DF4
    public static Color Brightness(this Color c, double amount)
    {
      return Color.FromArgb(c.A, PaperColor.<Brightness>g__Add|5_0(c.R, amount), PaperColor.<Brightness>g__Add|5_0(c.G, amount), PaperColor.<Brightness>g__Add|5_0(c.B, amount));
    }

    // Token: 0x060017EB RID: 6123 RVA: 0x005C7E2C File Offset: 0x005C7E2C
    public static Color Saturation(this Color c, double amount)
    {
      double num = 0.2126 * (double)c.R + 0.7152 * (double)c.G + 0.0722 * (double)c.B;
      return Color.FromArgb(c.A, PaperColor.<Saturation>g__Mix|6_0((double)c.R, num, amount), PaperColor.<Saturation>g__Mix|6_0((double)c.G, num, amount), PaperColor.<Saturation>g__Mix|6_0((double)c.B, num, amount));
    }

    // Token: 0x060017ED RID: 6125 RVA: 0x005C7EEC File Offset: 0x005C7EEC
    [CompilerGenerated]
    internal static byte <Brightness>g__Add|5_0(byte v, double a)
    {
      return (byte)Math.Clamp(Math.Round((double)v + a * 255.0), 0.0, 255.0);
    }

    // Token: 0x060017EE RID: 6126 RVA: 0x005C7F1C File Offset: 0x005C7F1C
    [CompilerGenerated]
    internal static byte <Saturation>g__Mix|6_0(double v, double l, double a)
    {
      return (byte)Math.Clamp(Math.Round(l + (v - l) * a), 0.0, 255.0);
    }

    // Token: 0x04000BB5 RID: 2997
    public static readonly Color White = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

    // Token: 0x04000BB6 RID: 2998
    public static readonly Color Black = Color.FromArgb(byte.MaxValue, 0, 0, 0);

    // Token: 0x04000BB7 RID: 2999
    public static readonly Color Clear = Color.FromArgb(0, 0, 0, 0);
  }
}
