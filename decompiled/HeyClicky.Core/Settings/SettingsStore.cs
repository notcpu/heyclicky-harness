using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Configuration;

namespace HeyClicky.Core.Settings
{
  // Token: 0x020000B4 RID: 180
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SettingsStore : IDisposable
  {
    // Token: 0x14000016 RID: 22
    // (add) Token: 0x06000BD7 RID: 3031 RVA: 0x0037B21C File Offset: 0x0037B21C
    // (remove) Token: 0x06000BD8 RID: 3032 RVA: 0x0037B254 File Offset: 0x0037B254
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler Changed;

    // Token: 0x06000BD9 RID: 3033 RVA: 0x0037B28C File Offset: 0x0037B28C
    [NullableContext(2)]
    public SettingsStore(string filePath = null)
    {
      this.filePath = filePath ?? Path.Combine(AppBundleConfiguration.AppDataDirectory, "settings.json");
      this.current = SettingsStore.Load(this.filePath);
      this.saveTimer = new Timer(delegate(object _)
      {
        this.Flush();
      }, null, -1, -1);
    }

    // Token: 0x1700028E RID: 654
    // (get) Token: 0x06000BDA RID: 3034 RVA: 0x0037B2F0 File Offset: 0x0037B2F0
    public string FilePath
    {
      get
      {
        return this.filePath;
      }
    }

    // Token: 0x1700028F RID: 655
    // (get) Token: 0x06000BDB RID: 3035 RVA: 0x0037B2F8 File Offset: 0x0037B2F8
    public AppSettings Current
    {
      get
      {
        object obj = this.gate;
        AppSettings appSettings;
        lock (obj)
        {
          appSettings = SettingsStore.Clone(this.current);
        }
        return appSettings;
      }
    }

    // Token: 0x06000BDC RID: 3036 RVA: 0x0037B340 File Offset: 0x0037B340
    public T Read<[Nullable(2)] T>(Func<AppSettings, T> read)
    {
      object obj = this.gate;
      T t;
      lock (obj)
      {
        t = read(this.current);
      }
      return t;
    }

    // Token: 0x06000BDD RID: 3037 RVA: 0x0037B388 File Offset: 0x0037B388
    public void Update(Action<AppSettings> change)
    {
      object obj = this.gate;
      lock (obj)
      {
        change(this.current);
        this.dirty = true;
        this.saveTimer.Change(250, -1);
      }
      try
      {
        EventHandler changed = this.Changed;
        if (changed != null)
        {
          changed(this, EventArgs.Empty);
        }
      }
      catch (Exception ex)
      {
        Log.Error("Settings", "Changed handler threw", ex);
      }
    }

    // Token: 0x06000BDE RID: 3038 RVA: 0x0037B420 File Offset: 0x0037B420
    public void Flush()
    {
      object obj = this.gate;
      byte[] array;
      lock (obj)
      {
        if (!this.dirty)
        {
          return;
        }
        this.dirty = false;
        array = JsonSerializer.SerializeToUtf8Bytes<AppSettings>(this.current, CoreJsonContext.Default.AppSettings);
      }
      try
      {
        AtomicFile.WriteAllBytes(this.filePath, array);
      }
      catch (Exception ex)
      {
        Log.Error("Settings", "settings.json write failed", ex);
        obj = this.gate;
        lock (obj)
        {
          this.dirty = true;
        }
      }
    }

    // Token: 0x06000BDF RID: 3039 RVA: 0x0037B4E4 File Offset: 0x0037B4E4
    private static AppSettings Load(string path)
    {
      try
      {
        if (File.Exists(path))
        {
          return JsonSerializer.Deserialize<AppSettings>(AtomicFile.ReadJson(path).Span, CoreJsonContext.Default.AppSettings) ?? new AppSettings();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Settings", "settings.json unreadable (" + ex.Message + "); using defaults (the old file is kept as settings.json.unreadable)");
        AtomicFile.KeepUnreadable(path);
      }
      return new AppSettings();
    }

    // Token: 0x06000BE0 RID: 3040 RVA: 0x0037B564 File Offset: 0x0037B564
    private static AppSettings Clone(AppSettings settings)
    {
      return JsonSerializer.Deserialize<AppSettings>(JsonSerializer.SerializeToUtf8Bytes<AppSettings>(settings, CoreJsonContext.Default.AppSettings), CoreJsonContext.Default.AppSettings);
    }

    // Token: 0x06000BE1 RID: 3041 RVA: 0x0037B58C File Offset: 0x0037B58C
    public void Dispose()
    {
      this.saveTimer.Dispose();
      this.Flush();
    }

    // Token: 0x0400051F RID: 1311
    private readonly string filePath;

    // Token: 0x04000520 RID: 1312
    private readonly object gate = new object();

    // Token: 0x04000521 RID: 1313
    private readonly Timer saveTimer;

    // Token: 0x04000522 RID: 1314
    private AppSettings current;

    // Token: 0x04000523 RID: 1315
    private bool dirty;
  }
}
