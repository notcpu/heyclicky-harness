using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002D2 RID: 722
  internal static class MarkdownViewerBufferExtensions
  {
    // Token: 0x06001E64 RID: 7780 RVA: 0x00606FB0 File Offset: 0x00606FB0
    [NullableContext(1)]
    public static IBuffer AsBuffer(this byte[] bytes)
    {
      return bytes.AsBuffer();
    }
  }
}
