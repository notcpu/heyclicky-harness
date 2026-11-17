using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200008C RID: 140
  [NullableContext(1)]
  public interface IPageReader
  {
    // Token: 0x06000888 RID: 2184
    [return: Nullable(new byte[] { 1, 2 })]
    Task<PageReference> CaptureReferenceAsync(CancellationToken ct = default(CancellationToken));

    // Token: 0x06000889 RID: 2185
    Task<PageContext> ExtractAsync(PageReference reference, CancellationToken ct = default(CancellationToken));
  }
}
