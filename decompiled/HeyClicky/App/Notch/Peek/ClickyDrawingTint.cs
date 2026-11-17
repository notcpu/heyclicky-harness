using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200016B RID: 363
  public static class ClickyDrawingTint
  {
    // Token: 0x060013FD RID: 5117 RVA: 0x005A7558 File Offset: 0x005A7558
    [return: TupleElementNames(new string[] { "Hue", "Saturation", "Brightness" })]
    public static ValueTuple<double, double, double>? DominantColor([Nullable(1)] byte[] rgba, int side = 32)
    {
      double[] array = new double[24];
      double[] array2 = new double[24];
      double[] array3 = new double[24];
      double[] array4 = new double[24];
      double[] array5 = new double[24];
      int num = 0;
      while (num + 3 < Math.Min(rgba.Length, side * side * 4))
      {
        double num2 = (double)rgba[num] / 255.0;
        double num3 = (double)rgba[num + 1] / 255.0;
        double num4 = (double)rgba[num + 2] / 255.0;
        double num5 = Math.Max(num2, Math.Max(num3, num4));
        double num6 = Math.Min(num2, Math.Min(num3, num4));
        double num7 = num5 - num6;
        if (num5 > 0.12 && num7 > 0.08)
        {
          double num8;
          if (num5 == num2)
          {
            num8 = (num3 - num4) / num7 % 6.0;
          }
          else if (num5 == num3)
          {
            num8 = (num4 - num2) / num7 + 2.0;
          }
          else
          {
            num8 = (num2 - num3) / num7 + 4.0;
          }
          num8 /= 6.0;
          if (num8 < 0.0)
          {
            num8 += 1.0;
          }
          double num9 = num7 / num5;
          double num10 = num9 * num9 * num5;
          int num11 = Math.Min(23, (int)(num8 * 24.0));
          array[num11] += num10;
          array2[num11] += num9 * num10;
          array3[num11] += num5 * num10;
          array4[num11] += Math.Sin(num8 * 2.0 * 3.141592653589793) * num10;
          array5[num11] += Math.Cos(num8 * 2.0 * 3.141592653589793) * num10;
        }
        num += 4;
      }
      int num12 = -1;
      double num13 = 0.0;
      for (int i = 0; i < 24; i++)
      {
        double num14 = array[i] + 0.5 * (array[(i + 1) % 24] + array[(i + 24 - 1) % 24]);
        if (num14 > num13)
        {
          num13 = num14;
          num12 = i;
        }
      }
      if (num12 < 0 || num13 <= (double)(side * side) * 0.012 || array[num12] <= 0.0)
      {
        return null;
      }
      double num15 = Math.Atan2(array4[num12], array5[num12]) / 6.283185307179586;
      if (num15 < 0.0)
      {
        num15 += 1.0;
      }
      return new ValueTuple<double, double, double>?(new ValueTuple<double, double, double>(num15, array2[num12] / array[num12], array3[num12] / array[num12]));
    }

    // Token: 0x060013FE RID: 5118 RVA: 0x005A7830 File Offset: 0x005A7830
    [return: TupleElementNames(new string[] { "R", "G", "B" })]
    public static ValueTuple<byte, byte, byte> Pastel([TupleElementNames(new string[] { "Hue", "Saturation", "Brightness" })] ValueTuple<double, double, double> color)
    {
      double num = Math.Min(0.42, Math.Max(0.18, color.Item2 * 0.55));
      return ClickyDrawingTint.FromHsb(color.Item1, num, 0.97);
    }

    // Token: 0x060013FF RID: 5119 RVA: 0x005A7880 File Offset: 0x005A7880
    [return: TupleElementNames(new string[] { "R", "G", "B" })]
    public static ValueTuple<byte, byte, byte> FromHsb(double hue, double saturation, double brightness)
    {
      double num = (hue - Math.Floor(hue)) * 6.0;
      int num2 = (int)Math.Floor(num) % 6;
      double num3 = num - Math.Floor(num);
      double num4 = brightness * (1.0 - saturation);
      double num5 = brightness * (1.0 - saturation * num3);
      double num6 = brightness * (1.0 - saturation * (1.0 - num3));
      ValueTuple<double, double, double> valueTuple;
      switch (num2)
      {
      case 0:
        valueTuple = new ValueTuple<double, double, double>(brightness, num6, num4);
        break;
      case 1:
        valueTuple = new ValueTuple<double, double, double>(num5, brightness, num4);
        break;
      case 2:
        valueTuple = new ValueTuple<double, double, double>(num4, brightness, num6);
        break;
      case 3:
        valueTuple = new ValueTuple<double, double, double>(num4, num5, brightness);
        break;
      case 4:
        valueTuple = new ValueTuple<double, double, double>(num6, num4, brightness);
        break;
      default:
        valueTuple = new ValueTuple<double, double, double>(brightness, num4, num5);
        break;
      }
      ValueTuple<double, double, double> valueTuple2 = valueTuple;
      double item = valueTuple2.Item1;
      double item2 = valueTuple2.Item2;
      double item3 = valueTuple2.Item3;
      return new ValueTuple<byte, byte, byte>(ClickyDrawingTint.<FromHsb>g__Byte|3_0(item), ClickyDrawingTint.<FromHsb>g__Byte|3_0(item2), ClickyDrawingTint.<FromHsb>g__Byte|3_0(item3));
    }

    // Token: 0x06001400 RID: 5120 RVA: 0x005A7984 File Offset: 0x005A7984
    [CompilerGenerated]
    internal static byte <FromHsb>g__Byte|3_0(double v)
    {
      return (byte)Math.Round(Math.Clamp(v, 0.0, 1.0) * 255.0);
    }

    // Token: 0x04000965 RID: 2405
    public const int SampleSide = 32;
  }
}
