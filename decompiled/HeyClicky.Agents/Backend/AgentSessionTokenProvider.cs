using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x02000070 RID: 112
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class AgentSessionTokenProvider
  {
    // Token: 0x060008DE RID: 2270 RVA: 0x00102734 File Offset: 0x00102734
    public AgentSessionTokenProvider(AgentWorkerApi api, [Nullable(new byte[] { 1, 1, 2 })] Func<CancellationToken, Task<string>> supabaseAccessToken, TimeSpan refreshMargin)
    {
    }

    // Token: 0x1700023D RID: 573
    // (get) Token: 0x060008DF RID: 2271 RVA: 0x00102760 File Offset: 0x00102760
    public DateTimeOffset? ExpiresAt
    {
      get
      {
        if (this.token != null)
        {
          return new DateTimeOffset?(this.expiresAt);
        }
        return null;
      }
    }

    // Token: 0x1700023E RID: 574
    // (get) Token: 0x060008E0 RID: 2272 RVA: 0x0010278C File Offset: 0x0010278C
    // (set) Token: 0x060008E1 RID: 2273 RVA: 0x00102794 File Offset: 0x00102794
    public bool AllowsDeveloperModelSelection { get; private set; }

    // Token: 0x1700023F RID: 575
    // (get) Token: 0x060008E2 RID: 2274 RVA: 0x001027A0 File Offset: 0x001027A0
    public bool NeedsRefresh
    {
      get
      {
        return this.token == null || this.expiresAt - DateTimeOffset.UtcNow <= this.<refreshMargin>P;
      }
    }

    // Token: 0x060008E3 RID: 2275 RVA: 0x001027C8 File Offset: 0x001027C8
    public void Invalidate()
    {
      Interlocked.Increment(ref this.generation);
      this.token = null;
      this.expiresAt = default(DateTimeOffset);
    }

    // Token: 0x060008E4 RID: 2276 RVA: 0x001027EC File Offset: 0x001027EC
    public async Task<string> GetAsync(CancellationToken ct, bool forceRefresh = false)
    {
      string text;
      if (!forceRefresh && !this.NeedsRefresh)
      {
        text = this.token;
      }
      else
      {
        await this.gate.WaitAsync(ct).ConfigureAwait(false);
        try
        {
          if (!forceRefresh && !this.NeedsRefresh)
          {
            text = this.token;
          }
          else
          {
            int mintGeneration = Volatile.Read(ref this.generation);
            object obj = await this.<supabaseAccessToken>P(ct).ConfigureAwait(false);
            if (obj == null)
            {
              throw new InvalidOperationException("Sign in to HeyClicky to use agents (no Supabase access token).");
            }
            string supabase = obj;
            int num = 0;
            ValueTuple<string, DateTimeOffset, bool> minted;
            try
            {
              minted = await this.<api>P.MintSessionTokenAsync(supabase, ct).ConfigureAwait(false);
            }
            catch (Exception obj3) when (AgentWorkerApi.IsUnauthorized((Exception)obj3))
            {
              num = 1;
            }
            object obj3;
            if (num == 1)
            {
              string text2 = await this.<supabaseAccessToken>P(ct).ConfigureAwait(false);
              if (text2 == null || text2 == supabase)
              {
                Exception ex = obj3 as Exception;
                if (ex == null)
                {
                  throw obj3;
                }
                ExceptionDispatchInfo.Capture(ex).Throw();
              }
              minted = await this.<api>P.MintSessionTokenAsync(text2, ct).ConfigureAwait(false);
            }
            obj3 = null;
            if (mintGeneration != Volatile.Read(ref this.generation))
            {
              throw new InvalidOperationException("Signed out while the agent session was being made.");
            }
            this.token = minted.Item1;
            this.expiresAt = minted.Item2;
            this.AllowsDeveloperModelSelection = minted.Item3;
            string text3 = "Agents";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 1);
            defaultInterpolatedStringHandler.AppendLiteral("agent session token minted, expires ");
            defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(this.expiresAt, "u");
            Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
            text = this.token;
          }
        }
        finally
        {
          this.gate.Release();
        }
      }
      return text;
    }

    // Token: 0x04000321 RID: 801
    [CompilerGenerated]
    private AgentWorkerApi <api>P = api;

    // Token: 0x04000322 RID: 802
    [Nullable(new byte[] { 1, 1, 2 })]
    [CompilerGenerated]
    private Func<CancellationToken, Task<string>> <supabaseAccessToken>P = supabaseAccessToken;

    // Token: 0x04000323 RID: 803
    [CompilerGenerated]
    private TimeSpan <refreshMargin>P = refreshMargin;

    // Token: 0x04000324 RID: 804
    private readonly SemaphoreSlim gate = new SemaphoreSlim(1, 1);

    // Token: 0x04000325 RID: 805
    [Nullable(2)]
    private string token;

    // Token: 0x04000326 RID: 806
    private DateTimeOffset expiresAt;

    // Token: 0x04000327 RID: 807
    private int generation;
  }
}
