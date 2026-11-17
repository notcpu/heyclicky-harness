using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000039 RID: 57
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ClickyJob : IEquatable<ClickyJob>
  {
    // Token: 0x1700015F RID: 351
    // (get) Token: 0x06000557 RID: 1367 RVA: 0x000F09D0 File Offset: 0x000F09D0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyJob);
      }
    }

    // Token: 0x17000160 RID: 352
    // (get) Token: 0x06000558 RID: 1368 RVA: 0x000F09DC File Offset: 0x000F09DC
    // (set) Token: 0x06000559 RID: 1369 RVA: 0x000F09E4 File Offset: 0x000F09E4
    [RequiredMember]
    public string Id { get; set; }

    // Token: 0x17000161 RID: 353
    // (get) Token: 0x0600055A RID: 1370 RVA: 0x000F09F0 File Offset: 0x000F09F0
    // (set) Token: 0x0600055B RID: 1371 RVA: 0x000F09F8 File Offset: 0x000F09F8
    [RequiredMember]
    public string Title { get; set; }

    // Token: 0x17000162 RID: 354
    // (get) Token: 0x0600055C RID: 1372 RVA: 0x000F0A04 File Offset: 0x000F0A04
    // (set) Token: 0x0600055D RID: 1373 RVA: 0x000F0A0C File Offset: 0x000F0A0C
    [RequiredMember]
    public string Prompt { get; set; }

    // Token: 0x17000163 RID: 355
    // (get) Token: 0x0600055E RID: 1374 RVA: 0x000F0A18 File Offset: 0x000F0A18
    // (set) Token: 0x0600055F RID: 1375 RVA: 0x000F0A20 File Offset: 0x000F0A20
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000164 RID: 356
    // (get) Token: 0x06000560 RID: 1376 RVA: 0x000F0A2C File Offset: 0x000F0A2C
    // (set) Token: 0x06000561 RID: 1377 RVA: 0x000F0A34 File Offset: 0x000F0A34
    [RequiredMember]
    public string FolderPath { get; set; }

    // Token: 0x17000165 RID: 357
    // (get) Token: 0x06000562 RID: 1378 RVA: 0x000F0A40 File Offset: 0x000F0A40
    // (set) Token: 0x06000563 RID: 1379 RVA: 0x000F0A48 File Offset: 0x000F0A48
    public DateTimeOffset CreatedAt { get; set; }

    // Token: 0x17000166 RID: 358
    // (get) Token: 0x06000564 RID: 1380 RVA: 0x000F0A54 File Offset: 0x000F0A54
    // (set) Token: 0x06000565 RID: 1381 RVA: 0x000F0A5C File Offset: 0x000F0A5C
    public DateTimeOffset? FinishedAt { get; set; }

    // Token: 0x17000167 RID: 359
    // (get) Token: 0x06000566 RID: 1382 RVA: 0x000F0A68 File Offset: 0x000F0A68
    // (set) Token: 0x06000567 RID: 1383 RVA: 0x000F0A70 File Offset: 0x000F0A70
    public ClickyJobOutcome Outcome { get; set; }

    // Token: 0x17000168 RID: 360
    // (get) Token: 0x06000568 RID: 1384 RVA: 0x000F0A7C File Offset: 0x000F0A7C
    // (set) Token: 0x06000569 RID: 1385 RVA: 0x000F0A84 File Offset: 0x000F0A84
    [Nullable(2)]
    public string Summary
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000169 RID: 361
    // (get) Token: 0x0600056A RID: 1386 RVA: 0x000F0A90 File Offset: 0x000F0A90
    // (set) Token: 0x0600056B RID: 1387 RVA: 0x000F0A98 File Offset: 0x000F0A98
    [Nullable(2)]
    public string ReplyText
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700016A RID: 362
    // (get) Token: 0x0600056C RID: 1388 RVA: 0x000F0AA4 File Offset: 0x000F0AA4
    // (set) Token: 0x0600056D RID: 1389 RVA: 0x000F0AAC File Offset: 0x000F0AAC
    public IReadOnlyList<string> ArtifactEntries { get; set; }

    // Token: 0x1700016B RID: 363
    // (get) Token: 0x0600056E RID: 1390 RVA: 0x000F0AB8 File Offset: 0x000F0AB8
    // (set) Token: 0x0600056F RID: 1391 RVA: 0x000F0AC0 File Offset: 0x000F0AC0
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> LatestReplyArtifactEntries
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700016C RID: 364
    // (get) Token: 0x06000570 RID: 1392 RVA: 0x000F0ACC File Offset: 0x000F0ACC
    // (set) Token: 0x06000571 RID: 1393 RVA: 0x000F0AD4 File Offset: 0x000F0AD4
    public bool IsSeen { get; set; }

    // Token: 0x1700016D RID: 365
    // (get) Token: 0x06000572 RID: 1394 RVA: 0x000F0AE0 File Offset: 0x000F0AE0
    // (set) Token: 0x06000573 RID: 1395 RVA: 0x000F0AE8 File Offset: 0x000F0AE8
    public bool IsDismissed { get; set; }

    // Token: 0x1700016E RID: 366
    // (get) Token: 0x06000574 RID: 1396 RVA: 0x000F0AF4 File Offset: 0x000F0AF4
    // (set) Token: 0x06000575 RID: 1397 RVA: 0x000F0AFC File Offset: 0x000F0AFC
    public bool? FollowedUpOnLaterDay { get; set; }

    // Token: 0x1700016F RID: 367
    // (get) Token: 0x06000576 RID: 1398 RVA: 0x000F0B08 File Offset: 0x000F0B08
    // (set) Token: 0x06000577 RID: 1399 RVA: 0x000F0B10 File Offset: 0x000F0B10
    public bool? IsArchived { get; set; }

    // Token: 0x17000170 RID: 368
    // (get) Token: 0x06000578 RID: 1400 RVA: 0x000F0B1C File Offset: 0x000F0B1C
    // (set) Token: 0x06000579 RID: 1401 RVA: 0x000F0B24 File Offset: 0x000F0B24
    [Nullable(2)]
    public string CloudColorName
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000171 RID: 369
    // (get) Token: 0x0600057A RID: 1402 RVA: 0x000F0B30 File Offset: 0x000F0B30
    // (set) Token: 0x0600057B RID: 1403 RVA: 0x000F0B38 File Offset: 0x000F0B38
    public Guid? SuggestionCardId { get; set; }

    // Token: 0x17000172 RID: 370
    // (get) Token: 0x0600057C RID: 1404 RVA: 0x000F0B44 File Offset: 0x000F0B44
    // (set) Token: 0x0600057D RID: 1405 RVA: 0x000F0B4C File Offset: 0x000F0B4C
    public bool? WaitsForChrome { get; set; }

    // Token: 0x17000173 RID: 371
    // (get) Token: 0x0600057E RID: 1406 RVA: 0x000F0B58 File Offset: 0x000F0B58
    // (set) Token: 0x0600057F RID: 1407 RVA: 0x000F0B60 File Offset: 0x000F0B60
    public bool? SkipsChrome { get; set; }

    // Token: 0x17000174 RID: 372
    // (get) Token: 0x06000580 RID: 1408 RVA: 0x000F0B6C File Offset: 0x000F0B6C
    // (set) Token: 0x06000581 RID: 1409 RVA: 0x000F0B74 File Offset: 0x000F0B74
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<ClickyJobRun> Runs
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000175 RID: 373
    // (get) Token: 0x06000582 RID: 1410 RVA: 0x000F0B80 File Offset: 0x000F0B80
    // (set) Token: 0x06000583 RID: 1411 RVA: 0x000F0B88 File Offset: 0x000F0B88
    [Nullable(2)]
    public ClickyJobUserStep WaitingStep
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000176 RID: 374
    // (get) Token: 0x06000584 RID: 1412 RVA: 0x000F0B94 File Offset: 0x000F0B94
    // (set) Token: 0x06000585 RID: 1413 RVA: 0x000F0B9C File Offset: 0x000F0B9C
    [Nullable(2)]
    public ClickyAnswer ResultCard
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000177 RID: 375
    // (get) Token: 0x06000586 RID: 1414 RVA: 0x000F0BA8 File Offset: 0x000F0BA8
    [JsonIgnore]
    public bool IsWaitingToStart
    {
      get
      {
        return this.SuggestionCardId != null && this.ThreadId == null;
      }
    }

    // Token: 0x17000178 RID: 376
    // (get) Token: 0x06000587 RID: 1415 RVA: 0x000F0BD0 File Offset: 0x000F0BD0
    [JsonIgnore]
    public bool IsHeldForChrome
    {
      get
      {
        return this.WaitsForChrome.GetValueOrDefault() && this.ThreadId == null;
      }
    }

    // Token: 0x17000179 RID: 377
    // (get) Token: 0x06000588 RID: 1416 RVA: 0x000F0BF8 File Offset: 0x000F0BF8
    [JsonIgnore]
    public IReadOnlyList<string> AllThreadIds
    {
      get
      {
        List<string> list = (this.Runs ?? Array.Empty<ClickyJobRun>()).Select<ClickyJobRun, string>((ClickyJobRun r) => r.ThreadId).OfType<string>().Distinct<string>()
          .ToList<string>();
        string threadId = this.ThreadId;
        if (threadId != null && !list.Contains(threadId))
        {
          list.Add(threadId);
        }
        return list;
      }
    }

    // Token: 0x1700017A RID: 378
    // (get) Token: 0x06000589 RID: 1417 RVA: 0x000F0C64 File Offset: 0x000F0C64
    [JsonIgnore]
    public string RailKey
    {
      get
      {
        return "job:" + this.Id;
      }
    }

    // Token: 0x1700017B RID: 379
    // (get) Token: 0x0600058A RID: 1418 RVA: 0x000F0C78 File Offset: 0x000F0C78
    // (set) Token: 0x0600058B RID: 1419 RVA: 0x000F0C80 File Offset: 0x000F0C80
    [Nullable(2)]
    public string Nickname
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700017C RID: 380
    // (get) Token: 0x0600058C RID: 1420 RVA: 0x000F0C8C File Offset: 0x000F0C8C
    [JsonIgnore]
    public string DisplayName
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(this.Nickname))
        {
          return this.Nickname.Trim();
        }
        return this.Title;
      }
    }

    // Token: 0x0600058D RID: 1421 RVA: 0x000F0CB0 File Offset: 0x000F0CB0
    [return: Nullable(2)]
    public static string JobIdFromRailKey(string railKey)
    {
      if (!railKey.StartsWith("job:", StringComparison.Ordinal))
      {
        return null;
      }
      int length = "job:".Length;
      return railKey.Substring(length, railKey.Length - length);
    }

    // Token: 0x1700017D RID: 381
    // (get) Token: 0x0600058E RID: 1422 RVA: 0x000F0CEC File Offset: 0x000F0CEC
    [JsonIgnore]
    public bool IsFinished
    {
      get
      {
        return this.Outcome > ClickyJobOutcome.Working;
      }
    }

    // Token: 0x1700017E RID: 382
    // (get) Token: 0x0600058F RID: 1423 RVA: 0x000F0CF8 File Offset: 0x000F0CF8
    [JsonIgnore]
    public bool IsQuickActionWithoutOutput
    {
      get
      {
        if (this.Outcome == ClickyJobOutcome.Done && this.ArtifactEntries.Count == 0)
        {
          string replyText = this.ReplyText;
          return ((replyText != null) ? replyText.Trim().Length : 0) <= 160;
        }
        return false;
      }
    }

    // Token: 0x1700017F RID: 383
    // (get) Token: 0x06000590 RID: 1424 RVA: 0x000F0D34 File Offset: 0x000F0D34
    [JsonIgnore]
    public ClickyJobKind Kind
    {
      get
      {
        return ClickyJobKinds.Infer(this.ArtifactEntries, this.Title, this.Prompt);
      }
    }

    // Token: 0x17000180 RID: 384
    // (get) Token: 0x06000591 RID: 1425 RVA: 0x000F0D50 File Offset: 0x000F0D50
    [JsonIgnore]
    public IReadOnlyList<string> FilesFromLatestReply
    {
      get
      {
        return this.LatestReplyArtifactEntries ?? this.ArtifactEntries;
      }
    }

    // Token: 0x06000592 RID: 1426 RVA: 0x000F0D64 File Offset: 0x000F0D64
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyJob");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000593 RID: 1427 RVA: 0x000F0DB0 File Offset: 0x000F0DB0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Prompt = ");
      builder.Append(this.Prompt);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", FolderPath = ");
      builder.Append(this.FolderPath);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt.ToString());
      builder.Append(", FinishedAt = ");
      builder.Append(this.FinishedAt.ToString());
      builder.Append(", Outcome = ");
      builder.Append(this.Outcome.ToString());
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", ReplyText = ");
      builder.Append(this.ReplyText);
      builder.Append(", ArtifactEntries = ");
      builder.Append(this.ArtifactEntries);
      builder.Append(", LatestReplyArtifactEntries = ");
      builder.Append(this.LatestReplyArtifactEntries);
      builder.Append(", IsSeen = ");
      builder.Append(this.IsSeen.ToString());
      builder.Append(", IsDismissed = ");
      builder.Append(this.IsDismissed.ToString());
      builder.Append(", FollowedUpOnLaterDay = ");
      builder.Append(this.FollowedUpOnLaterDay.ToString());
      builder.Append(", IsArchived = ");
      builder.Append(this.IsArchived.ToString());
      builder.Append(", CloudColorName = ");
      builder.Append(this.CloudColorName);
      builder.Append(", SuggestionCardId = ");
      builder.Append(this.SuggestionCardId.ToString());
      builder.Append(", WaitsForChrome = ");
      builder.Append(this.WaitsForChrome.ToString());
      builder.Append(", SkipsChrome = ");
      builder.Append(this.SkipsChrome.ToString());
      builder.Append(", Runs = ");
      builder.Append(this.Runs);
      builder.Append(", WaitingStep = ");
      builder.Append(this.WaitingStep);
      builder.Append(", ResultCard = ");
      builder.Append(this.ResultCard);
      builder.Append(", IsWaitingToStart = ");
      builder.Append(this.IsWaitingToStart.ToString());
      builder.Append(", IsHeldForChrome = ");
      builder.Append(this.IsHeldForChrome.ToString());
      builder.Append(", AllThreadIds = ");
      builder.Append(this.AllThreadIds);
      builder.Append(", RailKey = ");
      builder.Append(this.RailKey);
      builder.Append(", Nickname = ");
      builder.Append(this.Nickname);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", IsFinished = ");
      builder.Append(this.IsFinished.ToString());
      builder.Append(", IsQuickActionWithoutOutput = ");
      builder.Append(this.IsQuickActionWithoutOutput.ToString());
      builder.Append(", Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", FilesFromLatestReply = ");
      builder.Append(this.FilesFromLatestReply);
      return true;
    }

    // Token: 0x06000594 RID: 1428 RVA: 0x000F11D4 File Offset: 0x000F11D4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyJob left, ClickyJob right)
    {
      return !(left == right);
    }

    // Token: 0x06000595 RID: 1429 RVA: 0x000F11E0 File Offset: 0x000F11E0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyJob left, ClickyJob right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000596 RID: 1430 RVA: 0x000F11F4 File Offset: 0x000F11F4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Prompt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FolderPath>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<CreatedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<FinishedAt>k__BackingField)) * -1521134295 + EqualityComparer<ClickyJobOutcome>.Default.GetHashCode(this.<Outcome>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ReplyText>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ArtifactEntries>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<LatestReplyArtifactEntries>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsSeen>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDismissed>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<FollowedUpOnLaterDay>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<IsArchived>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CloudColorName>k__BackingField)) * -1521134295 + EqualityComparer<Guid?>.Default.GetHashCode(this.<SuggestionCardId>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<WaitsForChrome>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<SkipsChrome>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ClickyJobRun>>.Default.GetHashCode(this.<Runs>k__BackingField)) * -1521134295 + EqualityComparer<ClickyJobUserStep>.Default.GetHashCode(this.<WaitingStep>k__BackingField)) * -1521134295 + EqualityComparer<ClickyAnswer>.Default.GetHashCode(this.<ResultCard>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Nickname>k__BackingField);
    }

    // Token: 0x06000597 RID: 1431 RVA: 0x000F143C File Offset: 0x000F143C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyJob);
    }

    // Token: 0x06000598 RID: 1432 RVA: 0x000F144C File Offset: 0x000F144C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyJob other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Prompt>k__BackingField, other.<Prompt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FolderPath>k__BackingField, other.<FolderPath>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<FinishedAt>k__BackingField, other.<FinishedAt>k__BackingField) && EqualityComparer<ClickyJobOutcome>.Default.Equals(this.<Outcome>k__BackingField, other.<Outcome>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ReplyText>k__BackingField, other.<ReplyText>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ArtifactEntries>k__BackingField, other.<ArtifactEntries>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<LatestReplyArtifactEntries>k__BackingField, other.<LatestReplyArtifactEntries>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsSeen>k__BackingField, other.<IsSeen>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDismissed>k__BackingField, other.<IsDismissed>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<FollowedUpOnLaterDay>k__BackingField, other.<FollowedUpOnLaterDay>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<IsArchived>k__BackingField, other.<IsArchived>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CloudColorName>k__BackingField, other.<CloudColorName>k__BackingField) && EqualityComparer<Guid?>.Default.Equals(this.<SuggestionCardId>k__BackingField, other.<SuggestionCardId>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<WaitsForChrome>k__BackingField, other.<WaitsForChrome>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<SkipsChrome>k__BackingField, other.<SkipsChrome>k__BackingField) && EqualityComparer<IReadOnlyList<ClickyJobRun>>.Default.Equals(this.<Runs>k__BackingField, other.<Runs>k__BackingField) && EqualityComparer<ClickyJobUserStep>.Default.Equals(this.<WaitingStep>k__BackingField, other.<WaitingStep>k__BackingField) && EqualityComparer<ClickyAnswer>.Default.Equals(this.<ResultCard>k__BackingField, other.<ResultCard>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Nickname>k__BackingField, other.<Nickname>k__BackingField));
    }

    // Token: 0x0600059A RID: 1434 RVA: 0x000F16FC File Offset: 0x000F16FC
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ClickyJob(ClickyJob original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Prompt = original.<Prompt>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.FolderPath = original.<FolderPath>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
      this.FinishedAt = original.<FinishedAt>k__BackingField;
      this.Outcome = original.<Outcome>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.ReplyText = original.<ReplyText>k__BackingField;
      this.ArtifactEntries = original.<ArtifactEntries>k__BackingField;
      this.LatestReplyArtifactEntries = original.<LatestReplyArtifactEntries>k__BackingField;
      this.IsSeen = original.<IsSeen>k__BackingField;
      this.IsDismissed = original.<IsDismissed>k__BackingField;
      this.FollowedUpOnLaterDay = original.<FollowedUpOnLaterDay>k__BackingField;
      this.IsArchived = original.<IsArchived>k__BackingField;
      this.CloudColorName = original.<CloudColorName>k__BackingField;
      this.SuggestionCardId = original.<SuggestionCardId>k__BackingField;
      this.WaitsForChrome = original.<WaitsForChrome>k__BackingField;
      this.SkipsChrome = original.<SkipsChrome>k__BackingField;
      this.Runs = original.<Runs>k__BackingField;
      this.WaitingStep = original.<WaitingStep>k__BackingField;
      this.ResultCard = original.<ResultCard>k__BackingField;
      this.Nickname = original.<Nickname>k__BackingField;
    }

    // Token: 0x0600059B RID: 1435 RVA: 0x000F1830 File Offset: 0x000F1830
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ClickyJob()
    {
      this.ArtifactEntries = Array.Empty<string>();
      base..ctor();
    }

    // Token: 0x04000210 RID: 528
    public const string RailKeyPrefix = "job:";
  }
}
