using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky
{
  // Token: 0x0200001B RID: 27
  [NullableContext(2)]
  public interface IAuthService
  {
    // Token: 0x14000006 RID: 6
    // (add) Token: 0x06000125 RID: 293
    // (remove) Token: 0x06000126 RID: 294
    event EventHandler Changed;

    // Token: 0x17000052 RID: 82
    // (get) Token: 0x06000127 RID: 295
    bool IsSignedIn { get; }

    // Token: 0x17000053 RID: 83
    // (get) Token: 0x06000128 RID: 296
    UserProfile User { get; }

    // Token: 0x06000129 RID: 297
    [return: Nullable(new byte[] { 1, 2 })]
    Task<string> GetValidAccessTokenAsync(CancellationToken ct = default(CancellationToken));

    // Token: 0x0600012A RID: 298
    [NullableContext(1)]
    Task<bool> SignInWithGoogleAsync(CancellationToken ct = default(CancellationToken));

    // Token: 0x0600012B RID: 299
    [NullableContext(1)]
    Task<bool> HandleDeepLinkAsync(Uri uri);

    // Token: 0x0600012C RID: 300
    [NullableContext(1)]
    Task SignOutAsync();

    // Token: 0x17000054 RID: 84
    // (get) Token: 0x0600012D RID: 301 RVA: 0x0001E9E0 File Offset: 0x0001E9E0
    string LastSignInErrorMessage
    {
      get
      {
        return null;
      }
    }

    // Token: 0x14000007 RID: 7
    // (add) Token: 0x0600012E RID: 302 RVA: 0x0001E9E4 File Offset: 0x0001E9E4
    // (remove) Token: 0x0600012F RID: 303 RVA: 0x0001E9E8 File Offset: 0x0001E9E8
    public virtual event EventHandler SignInErrorChanged
    {
      add
      {
      }
      remove
      {
      }
    }
  }
}
