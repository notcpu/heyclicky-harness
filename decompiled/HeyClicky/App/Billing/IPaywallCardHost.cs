using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.Core.Billing;

namespace HeyClicky.App.Billing
{
  // Token: 0x02000364 RID: 868
  [NullableContext(1)]
  public interface IPaywallCardHost
  {
    // Token: 0x060025F1 RID: 9713
    [return: Nullable(new byte[] { 1, 2 })]
    Task<string> StartCheckoutAsync(string plan, string interval, string surface, PaywallSource source);

    // Token: 0x060025F2 RID: 9714
    [return: Nullable(new byte[] { 1, 2 })]
    Task<string> OpenPortalAsync();

    // Token: 0x060025F3 RID: 9715
    void Close(PaywallCloseKind kind);

    // Token: 0x060025F4 RID: 9716
    void ContentResized();
  }
}
