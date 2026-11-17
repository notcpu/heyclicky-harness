using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001EB RID: 491
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class JsonWriterContent : HttpContent
  {
    // Token: 0x06001CB9 RID: 7353 RVA: 0x003E48DC File Offset: 0x003E48DC
    public JsonWriterContent(Action<Utf8JsonWriter> write)
    {
      this.write = write;
      base.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    }

    // Token: 0x06001CBA RID: 7354 RVA: 0x003E4900 File Offset: 0x003E4900
    protected override async Task SerializeToStreamAsync(Stream stream, [Nullable(2)] TransportContext context)
    {
      await this.SerializeToStreamAsync(stream, context, CancellationToken.None).ConfigureAwait(false);
    }

    // Token: 0x06001CBB RID: 7355 RVA: 0x003E4954 File Offset: 0x003E4954
    protected override async Task SerializeToStreamAsync(Stream stream, [Nullable(2)] TransportContext context, CancellationToken cancellationToken)
    {
      Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions
      {
        SkipValidation = true
      });
      object obj = null;
      int num = 0;
      try
      {
        this.write(writer);
        await writer.FlushAsync(cancellationToken).ConfigureAwait(false);
        num = 1;
      }
      catch (object obj)
      {
      }
      if (writer != null)
      {
        await writer.DisposeAsync();
      }
      object obj2 = obj;
      if (obj2 != null)
      {
        Exception ex = obj2 as Exception;
        if (ex == null)
        {
          throw obj2;
        }
        ExceptionDispatchInfo.Capture(ex).Throw();
      }
      if (num != 1)
      {
        obj = null;
        writer = null;
      }
    }

    // Token: 0x06001CBC RID: 7356 RVA: 0x003E49A8 File Offset: 0x003E49A8
    protected override bool TryComputeLength(out long length)
    {
      length = 0L;
      return false;
    }

    // Token: 0x04000C58 RID: 3160
    private readonly Action<Utf8JsonWriter> write;
  }
}
