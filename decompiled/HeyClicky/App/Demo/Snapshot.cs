using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace HeyClicky.App.Demo
{
  // Token: 0x0200035A RID: 858
  [NullableContext(1)]
  [Nullable(0)]
  public static class Snapshot
  {
    // Token: 0x170006A3 RID: 1699
    // (get) Token: 0x06002553 RID: 9555 RVA: 0x0063AF04 File Offset: 0x0063AF04
    [Nullable(2)]
    public static string Directory
    {
      [NullableContext(2)]
      get
      {
        return Environment.GetEnvironmentVariable("HEYCLICKY_SNAPSHOT_DIR");
      }
    }

    // Token: 0x06002554 RID: 9556 RVA: 0x0063AF10 File Offset: 0x0063AF10
    public static async Task SaveAsync(UIElement root, string name)
    {
      string dir = Snapshot.Directory;
      if (!string.IsNullOrEmpty(dir))
      {
        try
        {
          global::System.IO.Directory.CreateDirectory(dir);
          RenderTargetBitmap rtb = new RenderTargetBitmap();
          await rtb.RenderAsync(root);
          IBuffer pixels = await rtb.GetPixelsAsync();
          string path = Path.Combine(dir, name + ".png");
          using (IRandomAccessStream stream = await (await StorageFile.GetFileFromPathAsync(await Snapshot.EnsureFile(path))).OpenAsync(FileAccessMode.ReadWrite))
          {
            object obj = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
            XamlRoot xamlRoot = root.XamlRoot;
            double num = ((xamlRoot != null) ? xamlRoot.RasterizationScale : 1.0);
            obj.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied, (uint)rtb.PixelWidth, (uint)rtb.PixelHeight, 96.0 * num, 96.0 * num, pixels.ToArray());
            await obj.FlushAsync();
            string text = "snapshot";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
            defaultInterpolatedStringHandler.AppendLiteral("saved ");
            defaultInterpolatedStringHandler.AppendFormatted(path);
            defaultInterpolatedStringHandler.AppendLiteral(" ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(rtb.PixelWidth);
            defaultInterpolatedStringHandler.AppendLiteral("x");
            defaultInterpolatedStringHandler.AppendFormatted<int>(rtb.PixelHeight);
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          }
          rtb = null;
          pixels = null;
          path = null;
          IRandomAccessStream stream = null;
        }
        catch (Exception ex)
        {
          Log.Error("snapshot", name, ex);
        }
      }
    }

    // Token: 0x06002555 RID: 9557
    [DllImport("user32.dll")]
    private static extern bool PrintWindow(IntPtr hwnd, IntPtr dc, uint flags);

    // Token: 0x06002556 RID: 9558
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, out Snapshot.WinRect r);

    // Token: 0x06002557 RID: 9559
    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hwnd);

    // Token: 0x06002558 RID: 9560
    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

    // Token: 0x06002559 RID: 9561
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr dc);

    // Token: 0x0600255A RID: 9562
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr dc, int w, int h);

    // Token: 0x0600255B RID: 9563
    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr dc, IntPtr obj);

    // Token: 0x0600255C RID: 9564
    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr obj);

    // Token: 0x0600255D RID: 9565
    [DllImport("gdi32.dll")]
    private static extern bool DeleteDC(IntPtr dc);

    // Token: 0x0600255E RID: 9566
    [DllImport("gdi32.dll")]
    private static extern int GetDIBits(IntPtr dc, IntPtr bmp, uint start, uint lines, byte[] bits, ref Snapshot.BitmapInfoHeader info, uint usage);

    // Token: 0x0600255F RID: 9567 RVA: 0x0063AF5C File Offset: 0x0063AF5C
    public static async Task SaveWindowAsync(IntPtr hwnd, string name)
    {
      string directory = Snapshot.Directory;
      if (!string.IsNullOrEmpty(directory) && hwnd != 0)
      {
        try
        {
          global::System.IO.Directory.CreateDirectory(directory);
          Snapshot.WinRect winRect;
          Snapshot.GetWindowRect(hwnd, out winRect);
          int w = winRect.Right - winRect.Left;
          int h = winRect.Bottom - winRect.Top;
          if (w > 0 && h > 0)
          {
            byte[] pixels = new byte[w * h * 4];
            IntPtr dc = Snapshot.GetDC((IntPtr)0);
            IntPtr intPtr = Snapshot.CreateCompatibleDC(dc);
            IntPtr intPtr2 = Snapshot.CreateCompatibleBitmap(dc, w, h);
            try
            {
              IntPtr intPtr3 = Snapshot.SelectObject(intPtr, intPtr2);
              Snapshot.PrintWindow(hwnd, intPtr, 2U);
              Snapshot.SelectObject(intPtr, intPtr3);
              Snapshot.BitmapInfoHeader bitmapInfoHeader = new Snapshot.BitmapInfoHeader
              {
                biSize = (uint)Marshal.SizeOf<Snapshot.BitmapInfoHeader>(),
                biWidth = w,
                biHeight = -h,
                biPlanes = 1,
                biBitCount = 32
              };
              Snapshot.GetDIBits(intPtr, intPtr2, 0U, (uint)h, pixels, ref bitmapInfoHeader, 0U);
            }
            finally
            {
              Snapshot.DeleteObject(intPtr2);
              Snapshot.DeleteDC(intPtr);
              Snapshot.ReleaseDC((IntPtr)0, dc);
            }
            for (int i = 3; i < pixels.Length; i += 4)
            {
              pixels[i] = byte.MaxValue;
            }
            string path = Path.Combine(directory, name + ".png");
            using (IRandomAccessStream stream = await (await StorageFile.GetFileFromPathAsync(await Snapshot.EnsureFile(path))).OpenAsync(FileAccessMode.ReadWrite))
            {
              object obj = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
              obj.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)w, (uint)h, 96.0, 96.0, pixels);
              await obj.FlushAsync();
              string text = "snapshot";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 3);
              defaultInterpolatedStringHandler.AppendLiteral("saved ");
              defaultInterpolatedStringHandler.AppendFormatted(path);
              defaultInterpolatedStringHandler.AppendLiteral(" ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(w);
              defaultInterpolatedStringHandler.AppendLiteral("x");
              defaultInterpolatedStringHandler.AppendFormatted<int>(h);
              defaultInterpolatedStringHandler.AppendLiteral(" (window)");
              Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            }
            pixels = null;
            path = null;
            IRandomAccessStream stream = null;
          }
        }
        catch (Exception ex)
        {
          Log.Error("snapshot", name, ex);
        }
      }
    }

    // Token: 0x06002560 RID: 9568 RVA: 0x0063AFA8 File Offset: 0x0063AFA8
    private static async Task<string> EnsureFile(string path)
    {
      if (File.Exists(path))
      {
        File.Delete(path);
      }
      await File.WriteAllBytesAsync(path, Array.Empty<byte>(), default(CancellationToken));
      return path;
    }

    // Token: 0x02000A66 RID: 2662
    [NullableContext(0)]
    private struct WinRect
    {
      // Token: 0x04002CFA RID: 11514
      public int Left;

      // Token: 0x04002CFB RID: 11515
      public int Top;

      // Token: 0x04002CFC RID: 11516
      public int Right;

      // Token: 0x04002CFD RID: 11517
      public int Bottom;
    }

    // Token: 0x02000A67 RID: 2663
    [NullableContext(0)]
    private struct BitmapInfoHeader
    {
      // Token: 0x04002CFE RID: 11518
      public uint biSize;

      // Token: 0x04002CFF RID: 11519
      public int biWidth;

      // Token: 0x04002D00 RID: 11520
      public int biHeight;

      // Token: 0x04002D01 RID: 11521
      public ushort biPlanes;

      // Token: 0x04002D02 RID: 11522
      public ushort biBitCount;

      // Token: 0x04002D03 RID: 11523
      public uint biCompression;

      // Token: 0x04002D04 RID: 11524
      public uint biSizeImage;

      // Token: 0x04002D05 RID: 11525
      public int biXPelsPerMeter;

      // Token: 0x04002D06 RID: 11526
      public int biYPelsPerMeter;

      // Token: 0x04002D07 RID: 11527
      public uint biClrUsed;

      // Token: 0x04002D08 RID: 11528
      public uint biClrImportant;
    }
  }
}
