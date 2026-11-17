using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Configuration;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000DE RID: 222
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveStore
  {
    // Token: 0x06000E7B RID: 3707 RVA: 0x0038B27C File Offset: 0x0038B27C
    [NullableContext(2)]
    public ProactiveStore(string path = null)
    {
      this.path = path ?? Path.Combine(AppBundleConfiguration.AppDataDirectory, "proactive.json");
      this.data = ProactiveStore.Load(this.path);
    }

    // Token: 0x06000E7C RID: 3708 RVA: 0x0038B2BC File Offset: 0x0038B2BC
    private static ProactiveStoreData Load(string path)
    {
      try
      {
        if (File.Exists(path))
        {
          return JsonSerializer.Deserialize<ProactiveStoreData>(AtomicFile.ReadJson(path).Span, ProactiveJsonContext.Default.ProactiveStoreData) ?? new ProactiveStoreData();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Proactive", "proactive.json unreadable (" + ex.Message + "); starting fresh");
        AtomicFile.KeepUnreadable(path);
      }
      return new ProactiveStoreData();
    }

    // Token: 0x06000E7D RID: 3709 RVA: 0x0038B33C File Offset: 0x0038B33C
    public T Read<[Nullable(2)] T>(Func<ProactiveStoreData, T> read)
    {
      object obj = this.gate;
      T t;
      lock (obj)
      {
        t = read(this.data);
      }
      return t;
    }

    // Token: 0x06000E7E RID: 3710 RVA: 0x0038B384 File Offset: 0x0038B384
    public T ReadAccount<[Nullable(2)] T>(string accountKey, Func<ProactiveAccountState, T> read)
    {
      object obj = this.gate;
      T t;
      lock (obj)
      {
        ProactiveAccountState proactiveAccountState;
        t = read(this.data.Accounts.TryGetValue(accountKey, out proactiveAccountState) ? proactiveAccountState : new ProactiveAccountState());
      }
      return t;
    }

    // Token: 0x06000E7F RID: 3711 RVA: 0x0038B3E4 File Offset: 0x0038B3E4
    public void Update(Action<ProactiveStoreData> change)
    {
      object obj = this.gate;
      byte[] array;
      lock (obj)
      {
        change(this.data);
        array = JsonSerializer.SerializeToUtf8Bytes<ProactiveStoreData>(this.data, ProactiveJsonContext.Default.ProactiveStoreData);
      }
      try
      {
        AtomicFile.WriteAllBytes(this.path, array);
      }
      catch (Exception ex)
      {
        Log.Warn("Proactive", "proactive.json write failed: " + ex.Message);
      }
    }

    // Token: 0x06000E80 RID: 3712 RVA: 0x0038B480 File Offset: 0x0038B480
    public void UpdateAccount(string accountKey, Action<ProactiveAccountState> change)
    {
      this.Update(delegate(ProactiveStoreData d)
      {
        ProactiveAccountState proactiveAccountState;
        if (!d.Accounts.TryGetValue(accountKey, out proactiveAccountState))
        {
          proactiveAccountState = (d.Accounts[accountKey] = new ProactiveAccountState());
        }
        change(proactiveAccountState);
      });
    }

    // Token: 0x0400063E RID: 1598
    private readonly string path;

    // Token: 0x0400063F RID: 1599
    private readonly object gate = new object();

    // Token: 0x04000640 RID: 1600
    private ProactiveStoreData data;
  }
}
