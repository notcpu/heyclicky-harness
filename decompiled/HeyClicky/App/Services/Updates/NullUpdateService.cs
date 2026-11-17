using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.Core.Identity;

namespace HeyClicky.App.Services.Updates
{
  // Token: 0x020000F0 RID: 240
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NullUpdateService : IUpdateService
  {
    // Token: 0x1400002C RID: 44
    // (add) Token: 0x06000BBF RID: 3007 RVA: 0x0055CA24 File Offset: 0x0055CA24
    // (remove) Token: 0x06000BC0 RID: 3008 RVA: 0x0055CA28 File Offset: 0x0055CA28
    [Nullable(2)]
    public event EventHandler Changed
    {
      [NullableContext(2)]
      add
      {
      }
      [NullableContext(2)]
      remove
      {
      }
    }

    // Token: 0x17000210 RID: 528
    // (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0055CA2C File Offset: 0x0055CA2C
    public string CurrentVersion
    {
      get
      {
        return ClickyClientIdentity.Current.AppVersion;
      }
    }

    // Token: 0x17000211 RID: 529
    // (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0055CA38 File Offset: 0x0055CA38
    public UpdateState State
    {
      get
      {
        return UpdateState.Unavailable;
      }
    }

    // Token: 0x17000212 RID: 530
    // (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0055CA3C File Offset: 0x0055CA3C
    [Nullable(2)]
    public string AvailableVersion
    {
      [NullableContext(2)]
      get
      {
        return null;
      }
    }

    // Token: 0x17000213 RID: 531
    // (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0055CA40 File Offset: 0x0055CA40
    public int DownloadPercent
    {
      get
      {
        return 0;
      }
    }

    // Token: 0x17000214 RID: 532
    // (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0055CA44 File Offset: 0x0055CA44
    public string RowTitle
    {
      get
      {
        return "App updates";
      }
    }

    // Token: 0x17000215 RID: 533
    // (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0055CA4C File Offset: 0x0055CA4C
    public string RowSubtitle
    {
      get
      {
        return "You're on version " + this.CurrentVersion + ".";
      }
    }

    // Token: 0x17000216 RID: 534
    // (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0055CA64 File Offset: 0x0055CA64
    public string ButtonTitle
    {
      get
      {
        return "Check for updates";
      }
    }

    // Token: 0x17000217 RID: 535
    // (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0055CA6C File Offset: 0x0055CA6C
    public bool ButtonIsPrimary
    {
      get
      {
        return false;
      }
    }

    // Token: 0x06000BC9 RID: 3017 RVA: 0x0055CA70 File Offset: 0x0055CA70
    public void PerformRowAction(string surface)
    {
    }

    // Token: 0x06000BCA RID: 3018 RVA: 0x0055CA74 File Offset: 0x0055CA74
    public Task CheckNowAsync(string surface)
    {
      return Task.CompletedTask;
    }

    // Token: 0x06000BCB RID: 3019 RVA: 0x0055CA7C File Offset: 0x0055CA7C
    public void InstallAndRelaunch(string surface)
    {
    }

    // Token: 0x04000448 RID: 1096
    public static readonly NullUpdateService Instance = new NullUpdateService();
  }
}
