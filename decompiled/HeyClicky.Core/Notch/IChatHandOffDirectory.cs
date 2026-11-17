using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Notch
{
  // Token: 0x020000FE RID: 254
  [NullableContext(1)]
  public interface IChatHandOffDirectory
  {
    // Token: 0x06001081 RID: 4225
    [return: TupleElementNames(new string[] { "Id", "Title" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    ValueTuple<string, string>? JobForThread(string threadId);

    // Token: 0x06001082 RID: 4226
    [return: Nullable(2)]
    string JobTitle(string jobId);

    // Token: 0x06001083 RID: 4227
    [return: Nullable(2)]
    string ClickySlugForThread(string threadId);

    // Token: 0x06001084 RID: 4228
    [return: Nullable(2)]
    string ClickyName(string slug);
  }
}
