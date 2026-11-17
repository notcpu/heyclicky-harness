using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x02000340 RID: 832
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class FakeAuthService : IAuthService
  {
    // Token: 0x14000085 RID: 133
    // (add) Token: 0x0600237B RID: 9083 RVA: 0x0062ECC8 File Offset: 0x0062ECC8
    // (remove) Token: 0x0600237C RID: 9084 RVA: 0x0062ED00 File Offset: 0x0062ED00
    public event EventHandler Changed;

    // Token: 0x1700067F RID: 1663
    // (get) Token: 0x0600237D RID: 9085 RVA: 0x0062ED38 File Offset: 0x0062ED38
    // (set) Token: 0x0600237E RID: 9086 RVA: 0x0062ED40 File Offset: 0x0062ED40
    public bool IsSignedIn { get; private set; }

    // Token: 0x17000680 RID: 1664
    // (get) Token: 0x0600237F RID: 9087 RVA: 0x0062ED4C File Offset: 0x0062ED4C
    // (set) Token: 0x06002380 RID: 9088 RVA: 0x0062ED54 File Offset: 0x0062ED54
    public UserProfile User { get; private set; }

    // Token: 0x06002381 RID: 9089 RVA: 0x0062ED60 File Offset: 0x0062ED60
    public FakeAuthService(bool signedIn)
    {
      if (signedIn)
      {
        this.SignInAs("test@heyclicky.local", "Test User");
      }
    }

    // Token: 0x06002382 RID: 9090 RVA: 0x0062ED7C File Offset: 0x0062ED7C
    [NullableContext(1)]
    private void SignInAs(string email, string name)
    {
      this.IsSignedIn = true;
      this.User = new UserProfile("dev-user", email, name, null);
      EventHandler changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed(this, EventArgs.Empty);
    }

    // Token: 0x06002383 RID: 9091 RVA: 0x0062EDB0 File Offset: 0x0062EDB0
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<string> GetValidAccessTokenAsync(CancellationToken ct = default(CancellationToken))
    {
      return Task.FromResult<string>(this.IsSignedIn ? "dev-token" : null);
    }

    // Token: 0x06002384 RID: 9092 RVA: 0x0062EDC8 File Offset: 0x0062EDC8
    [NullableContext(1)]
    public async Task<bool> SignInWithGoogleAsync(CancellationToken ct = default(CancellationToken))
    {
      await Task.Delay(400, ct);
      this.SignInAs("test@heyclicky.local", "Test User");
      return true;
    }

    // Token: 0x06002385 RID: 9093 RVA: 0x0062EE14 File Offset: 0x0062EE14
    [NullableContext(1)]
    public Task<bool> HandleDeepLinkAsync(Uri uri)
    {
      Log.Info("auth", "fake deep link " + uri.GetLeftPart(UriPartial.Path));
      return Task.FromResult<bool>(true);
    }

    // Token: 0x06002386 RID: 9094 RVA: 0x0062EE38 File Offset: 0x0062EE38
    [NullableContext(1)]
    public Task SignOutAsync()
    {
      this.IsSignedIn = false;
      this.User = null;
      EventHandler changed = this.Changed;
      if (changed != null)
      {
        changed(this, EventArgs.Empty);
      }
      return Task.CompletedTask;
    }
  }
}
