using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B1 RID: 689
  [NullableContext(1)]
  [Nullable(0)]
  internal static class DemoFiles
  {
    // Token: 0x06001C61 RID: 7265 RVA: 0x005F9B24 File Offset: 0x005F9B24
    public unsafe static void WriteChartPng(string path)
    {
      if (File.Exists(path) && new FileInfo(path).Length > 100L)
      {
        return;
      }
      byte[] array = new byte[115360];
      int[] array2 = new int[] { 70, 95, 82, 120, 134 };
      for (int i = 0; i < 160; i++)
      {
        int num = i * 721;
        for (int j = 0; j < 240; j++)
        {
          int num2 = (j - 20) / 44;
          int num3 = (j - 20) % 44;
          bool flag = j >= 20 && num2 < array2.Length && num3 < 30 && 144 - i < array2[num2] && i < 144;
          bool flag2 = i == 144 && j >= 14 && j < 230;
          byte b;
          byte b2;
          byte b3;
          if (!flag)
          {
            if (!flag2)
            {
              b = 250;
              b2 = 250;
              b3 = 247;
            }
            else
            {
              b = 96;
              b2 = 96;
              b3 = 96;
            }
          }
          else
          {
            b = 79;
            b2 = 140;
            b3 = byte.MaxValue;
          }
          array[num + 1 + j * 3] = b;
          array[num + 2 + j * 3] = b2;
          array[num + 3 + j * 3] = b3;
        }
      }
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (ZLibStream zlibStream = new ZLibStream(memoryStream, CompressionLevel.Optimal, true))
        {
          zlibStream.Write(array);
        }
        using (MemoryStream memoryStream2 = new MemoryStream())
        {
          memoryStream2.Write(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.4C4B6A3BE1314AB86138BEF4314DDE022E600960D8689A2C8F8631802D20DAB6), 8));
          byte[] array3 = new byte[13];
          BinaryPrimitives.WriteInt32BigEndian(array3, 240);
          BinaryPrimitives.WriteInt32BigEndian(array3.AsSpan<byte>(4), 160);
          array3[8] = 8;
          array3[9] = 2;
          DemoFiles.Chunk(memoryStream2, "IHDR", array3);
          DemoFiles.Chunk(memoryStream2, "IDAT", memoryStream.ToArray());
          DemoFiles.Chunk(memoryStream2, "IEND", Array.Empty<byte>());
          File.WriteAllBytes(path, memoryStream2.ToArray());
        }
      }
    }

    // Token: 0x06001C62 RID: 7266 RVA: 0x005F9D58 File Offset: 0x005F9D58
    private static void Chunk(Stream png, string type, byte[] data)
    {
      byte[] array = new byte[4];
      BinaryPrimitives.WriteInt32BigEndian(array, data.Length);
      png.Write(array);
      byte[] array2 = Encoding.ASCII.GetBytes(type).Concat<byte>(data).ToArray<byte>();
      png.Write(array2);
      uint num = uint.MaxValue;
      foreach (byte b in array2)
      {
        num ^= (uint)b;
        for (int j = 0; j < 8; j++)
        {
          num = (((num & 1U) != 0U) ? (3988292384U ^ (num >> 1)) : (num >> 1));
        }
      }
      byte[] array4 = new byte[4];
      BinaryPrimitives.WriteUInt32BigEndian(array4, ~num);
      png.Write(array4);
    }

    // Token: 0x06001C63 RID: 7267 RVA: 0x005F9E14 File Offset: 0x005F9E14
    public static void WritePdf(string path, string title, string line)
    {
      if (File.Exists(path) && new FileInfo(path).Length > 100L)
      {
        return;
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(137, 2);
      defaultInterpolatedStringHandler.AppendLiteral("BT /F1 22 Tf 48 520 Td (");
      defaultInterpolatedStringHandler.AppendFormatted(title);
      defaultInterpolatedStringHandler.AppendLiteral(") Tj ET BT /F1 12 Tf 48 490 Td (");
      defaultInterpolatedStringHandler.AppendFormatted(line);
      defaultInterpolatedStringHandler.AppendLiteral(") Tj ET 0.31 0.55 1 rg 48 300 80 150 re f 148 300 80 110 re f 248 300 80 180 re f");
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      string[] array = new string[5];
      array[0] = "<< /Type /Catalog /Pages 2 0 R >>";
      array[1] = "<< /Type /Pages /Kids [3 0 R] /Count 1 >>";
      array[2] = "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 420 595] /Resources << /Font << /F1 5 0 R >> >> /Contents 4 0 R >>";
      int num = 3;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 2);
      defaultInterpolatedStringHandler.AppendLiteral("<< /Length ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
      defaultInterpolatedStringHandler.AppendLiteral(" >>\nstream\n");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\nendstream");
      array[num] = defaultInterpolatedStringHandler.ToStringAndClear();
      array[4] = "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>";
      string[] array2 = array;
      StringBuilder stringBuilder = new StringBuilder("%PDF-1.4\n");
      List<int> list = new List<int>();
      StringBuilder stringBuilder2;
      StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler;
      for (int i = 0; i < array2.Length; i++)
      {
        list.Add(stringBuilder.Length);
        stringBuilder2 = stringBuilder;
        StringBuilder stringBuilder3 = stringBuilder2;
        appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(15, 2, stringBuilder2);
        appendInterpolatedStringHandler.AppendFormatted<int>(i + 1);
        appendInterpolatedStringHandler.AppendLiteral(" 0 obj\n");
        appendInterpolatedStringHandler.AppendFormatted(array2[i]);
        appendInterpolatedStringHandler.AppendLiteral("\nendobj\n");
        stringBuilder3.Append(ref appendInterpolatedStringHandler);
      }
      int length = stringBuilder.Length;
      stringBuilder2 = stringBuilder;
      StringBuilder stringBuilder4 = stringBuilder2;
      appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(28, 1, stringBuilder2);
      appendInterpolatedStringHandler.AppendLiteral("xref\n0 ");
      appendInterpolatedStringHandler.AppendFormatted<int>(array2.Length + 1);
      appendInterpolatedStringHandler.AppendLiteral("\n0000000000 65535 f \n");
      stringBuilder4.Append(ref appendInterpolatedStringHandler);
      foreach (int num2 in list)
      {
        stringBuilder2 = stringBuilder;
        StringBuilder stringBuilder5 = stringBuilder2;
        appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(10, 1, stringBuilder2);
        appendInterpolatedStringHandler.AppendFormatted<int>(num2, "D10");
        appendInterpolatedStringHandler.AppendLiteral(" 00000 n \n");
        stringBuilder5.Append(ref appendInterpolatedStringHandler);
      }
      stringBuilder2 = stringBuilder;
      StringBuilder stringBuilder6 = stringBuilder2;
      appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(50, 2, stringBuilder2);
      appendInterpolatedStringHandler.AppendLiteral("trailer\n<< /Size ");
      appendInterpolatedStringHandler.AppendFormatted<int>(array2.Length + 1);
      appendInterpolatedStringHandler.AppendLiteral(" /Root 1 0 R >>\nstartxref\n");
      appendInterpolatedStringHandler.AppendFormatted<int>(length);
      appendInterpolatedStringHandler.AppendLiteral("\n%%EOF\n");
      stringBuilder6.Append(ref appendInterpolatedStringHandler);
      File.WriteAllText(path, stringBuilder.ToString(), Encoding.ASCII);
    }
  }
}
