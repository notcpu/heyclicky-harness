using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.App.Services.Updates
{
  // Token: 0x020000EE RID: 238
  [NullableContext(1)]
  public interface IUpdateService
  {
    // Token: 0x1400002A RID: 42
    // (add) Token: 0x06000B85 RID: 2949
    // (remove) Token: 0x06000B86 RID: 2950
    [Nullable(2)]
    event EventHandler Changed;

    // Token: 0x170001FD RID: 509
    // (get) Token: 0x06000B87 RID: 2951
    string CurrentVersion { get; }

    // Token: 0x170001FE RID: 510
    // (get) Token: 0x06000B88 RID: 2952
    UpdateState State { get; }

    // Token: 0x170001FF RID: 511
    // (get) Token: 0x06000B89 RID: 2953
    [Nullable(2)]
    string AvailableVersion
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x17000200 RID: 512
    // (get) Token: 0x06000B8A RID: 2954
    int DownloadPercent { get; }

    // Token: 0x17000201 RID: 513
    // (get) Token: 0x06000B8B RID: 2955
    string RowTitle { get; }

    // Token: 0x17000202 RID: 514
    // (get) Token: 0x06000B8C RID: 2956
    string RowSubtitle { get; }

    // Token: 0x17000203 RID: 515
    // (get) Token: 0x06000B8D RID: 2957
    string ButtonTitle { get; }

    // Token: 0x17000204 RID: 516
    // (get) Token: 0x06000B8E RID: 2958
    bool ButtonIsPrimary { get; }

    // Token: 0x06000B8F RID: 2959
    void PerformRowAction(string surface);

    // Token: 0x06000B90 RID: 2960
    Task CheckNowAsync(string surface);

    // Token: 0x06000B91 RID: 2961
    void InstallAndRelaunch(string surface);
  }
}
