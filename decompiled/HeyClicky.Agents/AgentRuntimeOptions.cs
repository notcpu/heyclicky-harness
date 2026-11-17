using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents
{
  // Token: 0x02000013 RID: 19
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentRuntimeOptions
  {
    // Token: 0x17000069 RID: 105
    // (get) Token: 0x0600013C RID: 316 RVA: 0x000D99B8 File Offset: 0x000D99B8
    // (set) Token: 0x0600013D RID: 317 RVA: 0x000D99C0 File Offset: 0x000D99C0
    public string CodexHome { get; set; } = AppPaths.Combine(new string[] { "codex-home" });

    // Token: 0x1700006A RID: 106
    // (get) Token: 0x0600013E RID: 318 RVA: 0x000D99CC File Offset: 0x000D99CC
    // (set) Token: 0x0600013F RID: 319 RVA: 0x000D99D4 File Offset: 0x000D99D4
    public string ProjectsRoot { get; set; } = AppPaths.DocumentsRoot;

    // Token: 0x1700006B RID: 107
    // (get) Token: 0x06000140 RID: 320 RVA: 0x000D99E0 File Offset: 0x000D99E0
    // (set) Token: 0x06000141 RID: 321 RVA: 0x000D99E8 File Offset: 0x000D99E8
    public string StateDirectory { get; set; } = AppPaths.Combine(new string[] { "agents" });

    // Token: 0x1700006C RID: 108
    // (get) Token: 0x06000142 RID: 322 RVA: 0x000D99F4 File Offset: 0x000D99F4
    // (set) Token: 0x06000143 RID: 323 RVA: 0x000D99FC File Offset: 0x000D99FC
    public string HarnessRuntimeDirectory { get; set; } = Path.Combine(AppContext.BaseDirectory, "HarnessRuntime");

    // Token: 0x1700006D RID: 109
    // (get) Token: 0x06000144 RID: 324 RVA: 0x000D9A08 File Offset: 0x000D9A08
    // (set) Token: 0x06000145 RID: 325 RVA: 0x000D9A10 File Offset: 0x000D9A10
    public string BundledResourcesDirectory { get; set; } = Path.Combine(AppContext.BaseDirectory, "BundledResources");

    // Token: 0x1700006E RID: 110
    // (get) Token: 0x06000146 RID: 326 RVA: 0x000D9A1C File Offset: 0x000D9A1C
    // (set) Token: 0x06000147 RID: 327 RVA: 0x000D9A24 File Offset: 0x000D9A24
    public string Model { get; set; } = AgentRuntimeOptions.EnvOr("CLICKY_LOCAL_CODEX_MODEL", "gpt-6-luna");

    // Token: 0x1700006F RID: 111
    // (get) Token: 0x06000148 RID: 328 RVA: 0x000D9A30 File Offset: 0x000D9A30
    // (set) Token: 0x06000149 RID: 329 RVA: 0x000D9A38 File Offset: 0x000D9A38
    public string ReasoningEffort { get; set; } = AgentRuntimeOptions.ValidEffort(Environment.GetEnvironmentVariable("CLICKY_LOCAL_CODEX_REASONING_EFFORT")) ?? "high";

    // Token: 0x17000070 RID: 112
    // (get) Token: 0x0600014A RID: 330 RVA: 0x000D9A44 File Offset: 0x000D9A44
    // (set) Token: 0x0600014B RID: 331 RVA: 0x000D9A4C File Offset: 0x000D9A4C
    public int MaxConcurrentTasks { get; set; } = 5;

    // Token: 0x17000071 RID: 113
    // (get) Token: 0x0600014C RID: 332 RVA: 0x000D9A58 File Offset: 0x000D9A58
    // (set) Token: 0x0600014D RID: 333 RVA: 0x000D9A60 File Offset: 0x000D9A60
    public TimeSpan SessionTokenRefreshMargin { get; set; } = TimeSpan.FromMinutes(30L);

    // Token: 0x17000072 RID: 114
    // (get) Token: 0x0600014E RID: 334 RVA: 0x000D9A6C File Offset: 0x000D9A6C
    // (set) Token: 0x0600014F RID: 335 RVA: 0x000D9A74 File Offset: 0x000D9A74
    public bool AutoApproveExtraUsage { get; set; }

    // Token: 0x17000073 RID: 115
    // (get) Token: 0x06000150 RID: 336 RVA: 0x000D9A80 File Offset: 0x000D9A80
    // (set) Token: 0x06000151 RID: 337 RVA: 0x000D9A88 File Offset: 0x000D9A88
    [Nullable(2)]
    public string ObsidianVaultPath
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000074 RID: 116
    // (get) Token: 0x06000152 RID: 338 RVA: 0x000D9A94 File Offset: 0x000D9A94
    // (set) Token: 0x06000153 RID: 339 RVA: 0x000D9A9C File Offset: 0x000D9A9C
    public bool EnableRoutines { get; set; } = true;

    // Token: 0x06000154 RID: 340 RVA: 0x000D9AA8 File Offset: 0x000D9AA8
    private static string EnvOr(string name, string fallback)
    {
      string environmentVariable = Environment.GetEnvironmentVariable(name);
      string text = ((environmentVariable != null) ? environmentVariable.Trim() : null);
      if (!string.IsNullOrEmpty(text))
      {
        return text;
      }
      return fallback;
    }

    // Token: 0x06000155 RID: 341 RVA: 0x000D9AD4 File Offset: 0x000D9AD4
    [NullableContext(2)]
    private static string ValidEffort(string v)
    {
      string text = ((v != null) ? v.Trim() : null);
      string text2;
      if (text == "low" || text == "medium" || text == "high" || text == "xhigh")
      {
        text2 = v.Trim();
      }
      else
      {
        text2 = null;
      }
      return text2;
    }
  }
}
