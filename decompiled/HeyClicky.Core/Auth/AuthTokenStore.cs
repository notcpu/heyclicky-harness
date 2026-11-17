using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.Json;

namespace HeyClicky.Core.Auth
{
  // Token: 0x02000229 RID: 553
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AuthTokenStore
  {
    // Token: 0x0600204C RID: 8268 RVA: 0x003F4CAC File Offset: 0x003F4CAC
    public AuthTokenStore(string directory, bool isLocalBackend)
    {
      this.filePath = Path.Combine(directory, isLocalBackend ? "auth.localdev.bin" : "auth.bin");
      this.pendingSignInFilePath = Path.Combine(directory, isLocalBackend ? "signin-pending.localdev.bin" : "signin-pending.bin");
    }

    // Token: 0x17000748 RID: 1864
    // (get) Token: 0x0600204D RID: 8269 RVA: 0x003F4CEC File Offset: 0x003F4CEC
    public string FilePath
    {
      get
      {
        return this.filePath;
      }
    }

    // Token: 0x17000749 RID: 1865
    // (get) Token: 0x0600204E RID: 8270 RVA: 0x003F4CF4 File Offset: 0x003F4CF4
    public string PendingSignInFilePath
    {
      get
      {
        return this.pendingSignInFilePath;
      }
    }

    // Token: 0x0600204F RID: 8271 RVA: 0x003F4CFC File Offset: 0x003F4CFC
    [NullableContext(2)]
    public StoredAuthSession Load()
    {
      StoredAuthSession storedAuthSession;
      try
      {
        if (!File.Exists(this.filePath))
        {
          storedAuthSession = null;
        }
        else
        {
          storedAuthSession = JsonSerializer.Deserialize<StoredAuthSession>(ProtectedData.Unprotect(File.ReadAllBytes(this.filePath), AuthTokenStore.Entropy, DataProtectionScope.CurrentUser), CoreJsonContext.Default.StoredAuthSession);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Auth", "stored session unreadable (" + ex.GetType().Name + "); starting signed out");
        storedAuthSession = null;
      }
      return storedAuthSession;
    }

    // Token: 0x06002050 RID: 8272 RVA: 0x003F4D84 File Offset: 0x003F4D84
    public bool Save(StoredAuthSession session)
    {
      bool flag;
      try
      {
        byte[] array = ProtectedData.Protect(JsonSerializer.SerializeToUtf8Bytes<StoredAuthSession>(session, CoreJsonContext.Default.StoredAuthSession), AuthTokenStore.Entropy, DataProtectionScope.CurrentUser);
        AtomicFile.WriteAllBytes(this.filePath, array);
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Error("Auth", "session save failed", ex);
        flag = false;
      }
      return flag;
    }

    // Token: 0x06002051 RID: 8273 RVA: 0x003F4DE8 File Offset: 0x003F4DE8
    public bool Delete()
    {
      bool flag;
      try
      {
        if (File.Exists(this.filePath))
        {
          File.Delete(this.filePath);
        }
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Error("Auth", "session delete failed", ex);
        flag = false;
      }
      return flag;
    }

    // Token: 0x06002052 RID: 8274 RVA: 0x003F4E38 File Offset: 0x003F4E38
    [NullableContext(2)]
    public StoredPendingSignIn LoadPendingSignIn()
    {
      StoredPendingSignIn storedPendingSignIn;
      try
      {
        if (!File.Exists(this.pendingSignInFilePath))
        {
          storedPendingSignIn = null;
        }
        else
        {
          storedPendingSignIn = JsonSerializer.Deserialize<StoredPendingSignIn>(ProtectedData.Unprotect(File.ReadAllBytes(this.pendingSignInFilePath), AuthTokenStore.PendingSignInEntropy, DataProtectionScope.CurrentUser), CoreJsonContext.Default.StoredPendingSignIn);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Auth", "pending sign-in unreadable (" + ex.GetType().Name + ")");
        storedPendingSignIn = null;
      }
      return storedPendingSignIn;
    }

    // Token: 0x06002053 RID: 8275 RVA: 0x003F4EC0 File Offset: 0x003F4EC0
    public bool SavePendingSignIn(StoredPendingSignIn pendingSignIn)
    {
      bool flag;
      try
      {
        byte[] array = ProtectedData.Protect(JsonSerializer.SerializeToUtf8Bytes<StoredPendingSignIn>(pendingSignIn, CoreJsonContext.Default.StoredPendingSignIn), AuthTokenStore.PendingSignInEntropy, DataProtectionScope.CurrentUser);
        AtomicFile.WriteAllBytes(this.pendingSignInFilePath, array);
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Error("Auth", "pending sign-in save failed; keeping it for this run only", ex);
        flag = false;
      }
      return flag;
    }

    // Token: 0x06002054 RID: 8276 RVA: 0x003F4F24 File Offset: 0x003F4F24
    public bool DeletePendingSignIn()
    {
      bool flag;
      try
      {
        if (File.Exists(this.pendingSignInFilePath))
        {
          File.Delete(this.pendingSignInFilePath);
        }
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Error("Auth", "pending sign-in delete failed", ex);
        flag = false;
      }
      return flag;
    }

    // Token: 0x04000DA6 RID: 3494
    private static readonly byte[] Entropy = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.840AD0A8B36BF6A4B9A09410476692C616E86844AB6929086CA4328E47735C74), 17).ToArray();

    // Token: 0x04000DA7 RID: 3495
    private static readonly byte[] PendingSignInEntropy = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.AABA12E60C34D199FD5610CFFDCCBACD2D14580FEF8D8508C0A4BC513E186F81), 26).ToArray();

    // Token: 0x04000DA8 RID: 3496
    private readonly string filePath;

    // Token: 0x04000DA9 RID: 3497
    private readonly string pendingSignInFilePath;
  }
}
