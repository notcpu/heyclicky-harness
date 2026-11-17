using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000358 RID: 856
  [NullableContext(1)]
  [Nullable(0)]
  internal static class ScreenGrab
  {
    // Token: 0x0600253A RID: 9530
    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hwnd);

    // Token: 0x0600253B RID: 9531
    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

    // Token: 0x0600253C RID: 9532
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr dc);

    // Token: 0x0600253D RID: 9533
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr dc, int w, int h);

    // Token: 0x0600253E RID: 9534
    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr dc, IntPtr obj);

    // Token: 0x0600253F RID: 9535
    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(IntPtr dst, int x, int y, int w, int h, IntPtr src, int sx, int sy, uint rop);

    // Token: 0x06002540 RID: 9536
    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr obj);

    // Token: 0x06002541 RID: 9537
    [DllImport("gdi32.dll")]
    private static extern bool DeleteDC(IntPtr dc);

    // Token: 0x06002542 RID: 9538
    [DllImport("gdi32.dll")]
    private static extern int GetDIBits(IntPtr dc, IntPtr bmp, uint start, uint lines, byte[] bits, ref ScreenGrab.BITMAPINFOHEADER info, uint usage);

    // Token: 0x06002543 RID: 9539 RVA: 0x0063AA0C File Offset: 0x0063AA0C
    public static async Task SaveAsync(string path, int x, int y, int w, int h)
    {
      if (w > 0 && h > 0)
      {
        await ScreenGrab.SavePixelsAsync(path, ScreenGrab.Grab(x, y, w, h), w, h);
      }
    }

    // Token: 0x06002544 RID: 9540 RVA: 0x0063AA70 File Offset: 0x0063AA70
    public static byte[] Grab(int x, int y, int w, int h)
    {
      byte[] array = new byte[w * h * 4];
      IntPtr dc = ScreenGrab.GetDC((IntPtr)0);
      IntPtr intPtr = ScreenGrab.CreateCompatibleDC(dc);
      IntPtr intPtr2 = ScreenGrab.CreateCompatibleBitmap(dc, w, h);
      try
      {
        IntPtr intPtr3 = ScreenGrab.SelectObject(intPtr, intPtr2);
        ScreenGrab.BitBlt(intPtr, 0, 0, w, h, dc, x, y, 1087111200U);
        ScreenGrab.SelectObject(intPtr, intPtr3);
        ScreenGrab.BITMAPINFOHEADER bitmapinfoheader = new ScreenGrab.BITMAPINFOHEADER
        {
          biSize = (uint)Marshal.SizeOf<ScreenGrab.BITMAPINFOHEADER>(),
          biWidth = w,
          biHeight = -h,
          biPlanes = 1,
          biBitCount = 32
        };
        ScreenGrab.GetDIBits(intPtr, intPtr2, 0U, (uint)h, array, ref bitmapinfoheader, 0U);
      }
      finally
      {
        ScreenGrab.DeleteObject(intPtr2);
        ScreenGrab.DeleteDC(intPtr);
        ScreenGrab.ReleaseDC((IntPtr)0, dc);
      }
      for (int i = 3; i < array.Length; i += 4)
      {
        array[i] = byte.MaxValue;
      }
      return array;
    }

    // Token: 0x06002545 RID: 9541 RVA: 0x0063AB54 File Offset: 0x0063AB54
    public static async Task SavePixelsAsync(string path, byte[] pixels, int w, int h)
    {
      using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
      {
        object obj = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
        obj.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)w, (uint)h, 96.0, 96.0, pixels);
        await obj.FlushAsync();
        byte[] bytes = new byte[stream.Size];
        stream.Seek(0UL);
        using (DataReader reader = new DataReader(stream.GetInputStreamAt(0UL)))
        {
          await reader.LoadAsync((uint)stream.Size);
          reader.ReadBytes(bytes);
        }
        DataReader reader = null;
        await File.WriteAllBytesAsync(path, bytes, default(CancellationToken));
      }
    }

    // Token: 0x02000A5A RID: 2650
    [NullableContext(0)]
    private struct BITMAPINFOHEADER
    {
      // Token: 0x04002CAD RID: 11437
      public uint biSize;

      // Token: 0x04002CAE RID: 11438
      public int biWidth;

      // Token: 0x04002CAF RID: 11439
      public int biHeight;

      // Token: 0x04002CB0 RID: 11440
      public ushort biPlanes;

      // Token: 0x04002CB1 RID: 11441
      public ushort biBitCount;

      // Token: 0x04002CB2 RID: 11442
      public uint biCompression;

      // Token: 0x04002CB3 RID: 11443
      public uint biSizeImage;

      // Token: 0x04002CB4 RID: 11444
      public int biXPelsPerMeter;

      // Token: 0x04002CB5 RID: 11445
      public int biYPelsPerMeter;

      // Token: 0x04002CB6 RID: 11446
      public uint biClrUsed;

      // Token: 0x04002CB7 RID: 11447
      public uint biClrImportant;
    }
  }
}
