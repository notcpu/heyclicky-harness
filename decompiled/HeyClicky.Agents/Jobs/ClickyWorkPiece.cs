using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000031 RID: 49
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ClickyWorkPiece : IEquatable<ClickyWorkPiece>
  {
    // Token: 0x17000143 RID: 323
    // (get) Token: 0x060004F1 RID: 1265 RVA: 0x000EEED8 File Offset: 0x000EEED8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyWorkPiece);
      }
    }

    // Token: 0x17000144 RID: 324
    // (get) Token: 0x060004F2 RID: 1266 RVA: 0x000EEEE4 File Offset: 0x000EEEE4
    // (set) Token: 0x060004F3 RID: 1267 RVA: 0x000EEEEC File Offset: 0x000EEEEC
    [RequiredMember]
    public string Id { get; set; }

    // Token: 0x17000145 RID: 325
    // (get) Token: 0x060004F4 RID: 1268 RVA: 0x000EEEF8 File Offset: 0x000EEEF8
    // (set) Token: 0x060004F5 RID: 1269 RVA: 0x000EEF00 File Offset: 0x000EEF00
    [RequiredMember]
    public string Slug { get; set; }

    // Token: 0x17000146 RID: 326
    // (get) Token: 0x060004F6 RID: 1270 RVA: 0x000EEF0C File Offset: 0x000EEF0C
    // (set) Token: 0x060004F7 RID: 1271 RVA: 0x000EEF14 File Offset: 0x000EEF14
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000147 RID: 327
    // (get) Token: 0x060004F8 RID: 1272 RVA: 0x000EEF20 File Offset: 0x000EEF20
    // (set) Token: 0x060004F9 RID: 1273 RVA: 0x000EEF28 File Offset: 0x000EEF28
    [Nullable(2)]
    public string ClientTurnKey
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000148 RID: 328
    // (get) Token: 0x060004FA RID: 1274 RVA: 0x000EEF34 File Offset: 0x000EEF34
    // (set) Token: 0x060004FB RID: 1275 RVA: 0x000EEF3C File Offset: 0x000EEF3C
    public string Ask { get; set; }

    // Token: 0x17000149 RID: 329
    // (get) Token: 0x060004FC RID: 1276 RVA: 0x000EEF48 File Offset: 0x000EEF48
    // (set) Token: 0x060004FD RID: 1277 RVA: 0x000EEF50 File Offset: 0x000EEF50
    public string Title { get; set; }

    // Token: 0x1700014A RID: 330
    // (get) Token: 0x060004FE RID: 1278 RVA: 0x000EEF5C File Offset: 0x000EEF5C
    // (set) Token: 0x060004FF RID: 1279 RVA: 0x000EEF64 File Offset: 0x000EEF64
    public string Summary { get; set; }

    // Token: 0x1700014B RID: 331
    // (get) Token: 0x06000500 RID: 1280 RVA: 0x000EEF70 File Offset: 0x000EEF70
    // (set) Token: 0x06000501 RID: 1281 RVA: 0x000EEF78 File Offset: 0x000EEF78
    public string Reply { get; set; }

    // Token: 0x1700014C RID: 332
    // (get) Token: 0x06000502 RID: 1282 RVA: 0x000EEF84 File Offset: 0x000EEF84
    // (set) Token: 0x06000503 RID: 1283 RVA: 0x000EEF8C File Offset: 0x000EEF8C
    public IReadOnlyList<string> ArtifactPaths { get; set; }

    // Token: 0x1700014D RID: 333
    // (get) Token: 0x06000504 RID: 1284 RVA: 0x000EEF98 File Offset: 0x000EEF98
    // (set) Token: 0x06000505 RID: 1285 RVA: 0x000EEFA0 File Offset: 0x000EEFA0
    public DateTimeOffset FinishedAt { get; set; }

    // Token: 0x1700014E RID: 334
    // (get) Token: 0x06000506 RID: 1286 RVA: 0x000EEFAC File Offset: 0x000EEFAC
    // (set) Token: 0x06000507 RID: 1287 RVA: 0x000EEFB4 File Offset: 0x000EEFB4
    public bool Failed { get; set; }

    // Token: 0x1700014F RID: 335
    // (get) Token: 0x06000508 RID: 1288 RVA: 0x000EEFC0 File Offset: 0x000EEFC0
    // (set) Token: 0x06000509 RID: 1289 RVA: 0x000EEFC8 File Offset: 0x000EEFC8
    public bool IsRoutineRun { get; set; }

    // Token: 0x0600050A RID: 1290 RVA: 0x000EEFD4 File Offset: 0x000EEFD4
    public ClickyWorkPiece Reidentified(string newId, DateTimeOffset newFinishedAt)
    {
      ClickyWorkPiece clickyWorkPiece = this.<Clone>$();
      clickyWorkPiece.Id = newId;
      clickyWorkPiece.FinishedAt = newFinishedAt;
      return clickyWorkPiece;
    }

    // Token: 0x17000150 RID: 336
    // (get) Token: 0x0600050B RID: 1291 RVA: 0x000EEFEC File Offset: 0x000EEFEC
    public string DisplayTitle
    {
      get
      {
        if (this.Title.Length > 0)
        {
          return this.Title;
        }
        if (this.Ask.Length != 0)
        {
          return this.Ask;
        }
        return "Work";
      }
    }

    // Token: 0x0600050C RID: 1292 RVA: 0x000EF01C File Offset: 0x000EF01C
    private static bool IsLink(string path)
    {
      return path.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || path.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x17000151 RID: 337
    // (get) Token: 0x0600050D RID: 1293 RVA: 0x000EF03C File Offset: 0x000EF03C
    public IReadOnlyList<string> ExistingFilePaths
    {
      get
      {
        return this.ArtifactPaths.Where<string>((string p) => !ClickyWorkPiece.IsLink(p) && (File.Exists(p) || Directory.Exists(p))).ToList<string>();
      }
    }

    // Token: 0x17000152 RID: 338
    // (get) Token: 0x0600050E RID: 1294 RVA: 0x000EF070 File Offset: 0x000EF070
    public IReadOnlyList<string> Links
    {
      get
      {
        IEnumerable<string> artifactPaths = this.ArtifactPaths;
        Func<string, bool> func;
        if ((func = ClickyWorkPiece.<>O.<0>__IsLink) == null)
        {
          func = (ClickyWorkPiece.<>O.<0>__IsLink = new Func<string, bool>(ClickyWorkPiece.IsLink));
        }
        return artifactPaths.Where<string>(func).ToList<string>();
      }
    }

    // Token: 0x0600050F RID: 1295 RVA: 0x000EF0A0 File Offset: 0x000EF0A0
    [NullableContext(2)]
    public bool Equals(ClickyWorkPiece other)
    {
      return other != null && this.Id == other.Id && this.Slug == other.Slug && this.ThreadId == other.ThreadId && this.ClientTurnKey == other.ClientTurnKey && this.Ask == other.Ask && this.Title == other.Title && this.Summary == other.Summary && this.Reply == other.Reply && this.ArtifactPaths.SequenceEqual<string>(other.ArtifactPaths) && this.FinishedAt == other.FinishedAt && this.Failed == other.Failed && this.IsRoutineRun == other.IsRoutineRun;
    }

    // Token: 0x06000510 RID: 1296 RVA: 0x000EF19C File Offset: 0x000EF19C
    public override int GetHashCode()
    {
      return HashCode.Combine<string, string, DateTimeOffset>(this.Id, this.Reply, this.FinishedAt);
    }

    // Token: 0x06000511 RID: 1297 RVA: 0x000EF1B8 File Offset: 0x000EF1B8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyWorkPiece");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000512 RID: 1298 RVA: 0x000EF204 File Offset: 0x000EF204
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Slug = ");
      builder.Append(this.Slug);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", ClientTurnKey = ");
      builder.Append(this.ClientTurnKey);
      builder.Append(", Ask = ");
      builder.Append(this.Ask);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", Reply = ");
      builder.Append(this.Reply);
      builder.Append(", ArtifactPaths = ");
      builder.Append(this.ArtifactPaths);
      builder.Append(", FinishedAt = ");
      builder.Append(this.FinishedAt.ToString());
      builder.Append(", Failed = ");
      builder.Append(this.Failed.ToString());
      builder.Append(", IsRoutineRun = ");
      builder.Append(this.IsRoutineRun.ToString());
      builder.Append(", DisplayTitle = ");
      builder.Append(this.DisplayTitle);
      builder.Append(", ExistingFilePaths = ");
      builder.Append(this.ExistingFilePaths);
      builder.Append(", Links = ");
      builder.Append(this.Links);
      return true;
    }

    // Token: 0x06000513 RID: 1299 RVA: 0x000EF3B8 File Offset: 0x000EF3B8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyWorkPiece left, ClickyWorkPiece right)
    {
      return !(left == right);
    }

    // Token: 0x06000514 RID: 1300 RVA: 0x000EF3C4 File Offset: 0x000EF3C4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyWorkPiece left, ClickyWorkPiece right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000515 RID: 1301 RVA: 0x000EF3D8 File Offset: 0x000EF3D8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyWorkPiece);
    }

    // Token: 0x06000517 RID: 1303 RVA: 0x000EF3F0 File Offset: 0x000EF3F0
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ClickyWorkPiece(ClickyWorkPiece original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Slug = original.<Slug>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.ClientTurnKey = original.<ClientTurnKey>k__BackingField;
      this.Ask = original.<Ask>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.Reply = original.<Reply>k__BackingField;
      this.ArtifactPaths = original.<ArtifactPaths>k__BackingField;
      this.FinishedAt = original.<FinishedAt>k__BackingField;
      this.Failed = original.<Failed>k__BackingField;
      this.IsRoutineRun = original.<IsRoutineRun>k__BackingField;
    }

    // Token: 0x06000518 RID: 1304 RVA: 0x000EF494 File Offset: 0x000EF494
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ClickyWorkPiece()
    {
      this.Ask = "";
      this.Title = "";
      this.Summary = "";
      this.Reply = "";
      this.ArtifactPaths = Array.Empty<string>();
      base..ctor();
    }

    // Token: 0x02000141 RID: 321
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040006C4 RID: 1732
      [Nullable(0)]
      public static Func<string, bool> <0>__IsLink;
    }
  }
}
