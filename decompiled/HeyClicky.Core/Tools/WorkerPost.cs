using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Tools
{
  // Token: 0x020000A6 RID: 166
  // (Invoke) Token: 0x06000A47 RID: 2631
  [return: TupleElementNames(new string[] { "Status", "Body" })]
  [return: Nullable(new byte[] { 1, 0, 1 })]
  public delegate Task<ValueTuple<int, string>?> WorkerPost(string path, string jsonBody, CancellationToken ct);
}
