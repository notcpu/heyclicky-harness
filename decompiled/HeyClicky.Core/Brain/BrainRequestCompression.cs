using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F6 RID: 502
  [NullableContext(1)]
  [Nullable(0)]
  public static class BrainRequestCompression
  {
    // Token: 0x17000668 RID: 1640
    // (get) Token: 0x06001D03 RID: 7427 RVA: 0x003E591C File Offset: 0x003E591C
    public static bool WorkerAcceptsGzip
    {
      get
      {
        return BrainRequestCompression.workerAcceptsGzip;
      }
    }

    // Token: 0x06001D04 RID: 7428 RVA: 0x003E5928 File Offset: 0x003E5928
    public static void NoteResponse([Nullable(2)] HttpResponseMessage response, string path)
    {
      if (response == null || !BrainRequestCompression.GzipPaths.Contains(path))
      {
        return;
      }
      IEnumerable<string> enumerable;
      BrainRequestCompression.workerAcceptsGzip = response.Headers.TryGetValues("x-clicky-accepts-gzip", out enumerable) && enumerable.FirstOrDefault<string>() == "1";
    }

    // Token: 0x06001D05 RID: 7429 RVA: 0x003E5974 File Offset: 0x003E5974
    internal static void SetWorkerAcceptsGzipForTest(bool accepts)
    {
      BrainRequestCompression.workerAcceptsGzip = accepts;
    }

    // Token: 0x06001D06 RID: 7430 RVA: 0x003E5980 File Offset: 0x003E5980
    public static bool ShouldCompress(string path, long length)
    {
      return BrainRequestCompression.workerAcceptsGzip && BrainRequestCompression.GzipPaths.Contains(path) && length >= 8192L;
    }

    // Token: 0x06001D07 RID: 7431 RVA: 0x003E59A8 File Offset: 0x003E59A8
    [return: TupleElementNames(new string[] { "Content", "Bytes", "IsGzip" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public static ValueTuple<HttpContent, int, bool> Content(byte[] json, int length, string path)
    {
      if (BrainRequestCompression.ShouldCompress(path, (long)length))
      {
        byte[] array = BrainRequestCompression.Gzip(json.AsSpan<byte>(0, length));
        if (array != null && array.Length < length)
        {
          return new ValueTuple<HttpContent, int, bool>(new ByteArrayContent(array)
          {
            Headers = 
            {
              ContentType = new MediaTypeHeaderValue("application/json"),
              ContentEncoding = { "gzip" }
            }
          }, array.Length, true);
        }
      }
      return new ValueTuple<HttpContent, int, bool>(new ByteArrayContent(json, 0, length)
      {
        Headers = 
        {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      }, length, false);
    }

    // Token: 0x06001D08 RID: 7432 RVA: 0x003E5A3C File Offset: 0x003E5A3C
    [NullableContext(0)]
    [return: Nullable(2)]
    public static byte[] Gzip(ReadOnlySpan<byte> data)
    {
      if (data.IsEmpty)
      {
        return null;
      }
      byte[] array;
      try
      {
        using (MemoryStream memoryStream = new MemoryStream(data.Length / 3 + 64))
        {
          using (GZipStream gzipStream = new GZipStream(memoryStream, new ZLibCompressionOptions
          {
            CompressionLevel = 4
          }, true))
          {
            gzipStream.Write(data);
          }
          array = memoryStream.ToArray();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Brain", "gzip failed: " + ex.Message);
        array = null;
      }
      return array;
    }

    // Token: 0x04000C74 RID: 3188
    public const string AcceptsGzipHeader = "x-clicky-accepts-gzip";

    // Token: 0x04000C75 RID: 3189
    public static readonly IReadOnlySet<string> GzipPaths = new HashSet<string> { "/brain/turn", "/brain/compact", "/brain/session-end", "/brain/prewarm" };

    // Token: 0x04000C76 RID: 3190
    public const int MinimumBytesToCompress = 8192;

    // Token: 0x04000C77 RID: 3191
    private static volatile bool workerAcceptsGzip;
  }
}
