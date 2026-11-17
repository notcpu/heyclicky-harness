using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200004B RID: 75
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ClickyProgramBatch : IEquatable<ClickyProgramBatch>
  {
    // Token: 0x170001B3 RID: 435
    // (get) Token: 0x0600069E RID: 1694 RVA: 0x000F7310 File Offset: 0x000F7310
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyProgramBatch);
      }
    }

    // Token: 0x170001B4 RID: 436
    // (get) Token: 0x0600069F RID: 1695 RVA: 0x000F731C File Offset: 0x000F731C
    // (set) Token: 0x060006A0 RID: 1696 RVA: 0x000F7324 File Offset: 0x000F7324
    [RequiredMember]
    public string Id { get; set; }

    // Token: 0x170001B5 RID: 437
    // (get) Token: 0x060006A1 RID: 1697 RVA: 0x000F7330 File Offset: 0x000F7330
    // (set) Token: 0x060006A2 RID: 1698 RVA: 0x000F7338 File Offset: 0x000F7338
    public int SearchCount { get; set; }

    // Token: 0x170001B6 RID: 438
    // (get) Token: 0x060006A3 RID: 1699 RVA: 0x000F7344 File Offset: 0x000F7344
    // (set) Token: 0x060006A4 RID: 1700 RVA: 0x000F734C File Offset: 0x000F734C
    public int SearchesDone { get; set; }

    // Token: 0x170001B7 RID: 439
    // (get) Token: 0x060006A5 RID: 1701 RVA: 0x000F7358 File Offset: 0x000F7358
    // (set) Token: 0x060006A6 RID: 1702 RVA: 0x000F7360 File Offset: 0x000F7360
    [Nullable(2)]
    public string FirstQuery
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001B8 RID: 440
    // (get) Token: 0x060006A7 RID: 1703 RVA: 0x000F736C File Offset: 0x000F736C
    // (set) Token: 0x060006A8 RID: 1704 RVA: 0x000F7374 File Offset: 0x000F7374
    public IReadOnlyList<ClickyProgramBatch.SiteEntry> Sites { get; set; }

    // Token: 0x170001B9 RID: 441
    // (get) Token: 0x060006A9 RID: 1705 RVA: 0x000F7380 File Offset: 0x000F7380
    // (set) Token: 0x060006AA RID: 1706 RVA: 0x000F7388 File Offset: 0x000F7388
    public int SiteCount { get; set; }

    // Token: 0x170001BA RID: 442
    // (get) Token: 0x060006AB RID: 1707 RVA: 0x000F7394 File Offset: 0x000F7394
    // (set) Token: 0x060006AC RID: 1708 RVA: 0x000F739C File Offset: 0x000F739C
    public int SitesDone { get; set; }

    // Token: 0x170001BB RID: 443
    // (get) Token: 0x060006AD RID: 1709 RVA: 0x000F73A8 File Offset: 0x000F73A8
    // (set) Token: 0x060006AE RID: 1710 RVA: 0x000F73B0 File Offset: 0x000F73B0
    public IReadOnlyList<ClickyProgramBatch.AppEntry> Apps { get; set; }

    // Token: 0x060006AF RID: 1711 RVA: 0x000F73BC File Offset: 0x000F73BC
    [return: Nullable(2)]
    public static ClickyProgramBatch FromEvent(JsonObject e)
    {
      string text = ClickyProgramBatch.<FromEvent>g__Str|37_0(e["id"]);
      if (text == null || text.Length <= 0)
      {
        return null;
      }
      JsonObject jsonObject = e["searches"] as JsonObject;
      JsonArray jsonArray;
      if ((jsonArray = e["sites"] as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      List<ClickyProgramBatch.SiteEntry> list = jsonArray.OfType<JsonObject>().Select<JsonObject, ClickyProgramBatch.SiteEntry>(delegate(JsonObject s)
      {
        string text2 = ClickyProgramBatch.<FromEvent>g__Str|37_0(s["host"]);
        if (text2 == null || text2.Length <= 0)
        {
          return null;
        }
        return new ClickyProgramBatch.SiteEntry(text2, ClickyProgramBatch.<FromEvent>g__Bool|37_2(s["done"]).GetValueOrDefault());
      }).OfType<ClickyProgramBatch.SiteEntry>()
        .ToList<ClickyProgramBatch.SiteEntry>();
      JsonArray jsonArray2;
      if ((jsonArray2 = e["apps"] as JsonArray) == null)
      {
        jsonArray2 = new JsonArray(null);
      }
      List<ClickyProgramBatch.AppEntry> list2 = jsonArray2.OfType<JsonObject>().Select<JsonObject, ClickyProgramBatch.AppEntry>(delegate(JsonObject a)
      {
        string text3 = ClickyProgramBatch.<FromEvent>g__Str|37_0(a["toolkit"]);
        if (text3 == null || text3.Length <= 0)
        {
          return null;
        }
        return new ClickyProgramBatch.AppEntry
        {
          Toolkit = text3,
          Slug = ClickyProgramBatch.<FromEvent>g__Str|37_0(a["slug"]),
          Name = text3,
          Kind = (ClickyProgramBatch.<FromEvent>g__Str|37_0(a["kind"]) ?? "read"),
          Count = ClickyProgramBatch.<FromEvent>g__Int|37_1(a["count"]).GetValueOrDefault(),
          Done = ClickyProgramBatch.<FromEvent>g__Int|37_1(a["done"]).GetValueOrDefault()
        };
      }).OfType<ClickyProgramBatch.AppEntry>()
        .ToList<ClickyProgramBatch.AppEntry>();
      ClickyProgramBatch clickyProgramBatch = new ClickyProgramBatch();
      clickyProgramBatch.Id = text;
      clickyProgramBatch.SearchCount = ClickyProgramBatch.<FromEvent>g__Int|37_1((jsonObject != null) ? jsonObject["count"] : null).GetValueOrDefault();
      clickyProgramBatch.SearchesDone = ClickyProgramBatch.<FromEvent>g__Int|37_1((jsonObject != null) ? jsonObject["done"] : null).GetValueOrDefault();
      clickyProgramBatch.FirstQuery = ClickyProgramBatch.<FromEvent>g__Str|37_0((jsonObject != null) ? jsonObject["query"] : null);
      clickyProgramBatch.Sites = list;
      clickyProgramBatch.SiteCount = Math.Max(list.Count, ClickyProgramBatch.<FromEvent>g__Int|37_1(e["siteCount"]).GetValueOrDefault());
      int? num = ClickyProgramBatch.<FromEvent>g__Int|37_1(e["sitesDone"]);
      int num2;
      if (num == null)
      {
        num2 = list.Count<ClickyProgramBatch.SiteEntry>((ClickyProgramBatch.SiteEntry s) => s.Done);
      }
      else
      {
        num2 = num.GetValueOrDefault();
      }
      clickyProgramBatch.SitesDone = num2;
      clickyProgramBatch.Apps = list2;
      return clickyProgramBatch;
    }

    // Token: 0x170001BC RID: 444
    // (get) Token: 0x060006B0 RID: 1712 RVA: 0x000F75A0 File Offset: 0x000F75A0
    [TupleElementNames(new string[] { "Text", "Glyph" })]
    [Nullable(new byte[] { 0, 1 })]
    public ValueTuple<string, ClickyInkWorkStep.StepKind>? Summary
    {
      [return: TupleElementNames(new string[] { "Text", "Glyph" })]
      [return: Nullable(new byte[] { 0, 1 })]
      get
      {
        List<ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>> list = new List<ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>>();
        List<ClickyProgramBatch.AppEntry> list2 = this.Apps.Where<ClickyProgramBatch.AppEntry>((ClickyProgramBatch.AppEntry a) => a.Kind != "read").ToList<ClickyProgramBatch.AppEntry>();
        HashSet<string> writingApps = list2.Select<ClickyProgramBatch.AppEntry, string>((ClickyProgramBatch.AppEntry a) => a.Toolkit).ToHashSet<string>();
        List<string> list3 = ClickyProgramBatch.UniqueNames(this.Apps.Where<ClickyProgramBatch.AppEntry>((ClickyProgramBatch.AppEntry a) => a.Kind == "read" && !writingApps.Contains(a.Toolkit)));
        bool flag = new bool[]
        {
          list2.Count == 0,
          this.SiteCount == 0,
          this.SearchCount == 0,
          list3.Count == 0
        }.Count<bool>((bool empty) => !empty) == 1;
        foreach (ClickyProgramBatch.AppEntry appEntry in list2)
        {
          list.Add(new ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>(0, 2, ClickyProgramBatch.WriteText(appEntry), ClickyInkWorkStep.StepKind.Apps));
        }
        if (this.SiteCount > 0)
        {
          string text;
          if (!flag || this.SiteCount != 1)
          {
            if (this.SitesDone >= this.SiteCount || this.SiteCount <= 1)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
              defaultInterpolatedStringHandler.AppendFormatted<int>(this.SiteCount);
              defaultInterpolatedStringHandler.AppendLiteral(" site");
              defaultInterpolatedStringHandler.AppendFormatted((this.SiteCount == 1) ? "" : "s");
              text = defaultInterpolatedStringHandler.ToStringAndClear();
            }
            else
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 2);
              defaultInterpolatedStringHandler.AppendFormatted<int>(this.SitesDone);
              defaultInterpolatedStringHandler.AppendLiteral(" of ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(this.SiteCount);
              defaultInterpolatedStringHandler.AppendLiteral(" sites");
              text = defaultInterpolatedStringHandler.ToStringAndClear();
            }
          }
          else
          {
            ClickyProgramBatch.SiteEntry siteEntry = this.Sites.FirstOrDefault<ClickyProgramBatch.SiteEntry>();
            text = ((siteEntry != null) ? siteEntry.Host : null) ?? "a page";
          }
          string text2 = text;
          list.Add(new ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>(1, 1, text2, ClickyInkWorkStep.StepKind.Read));
        }
        if (this.SearchCount > 0)
        {
          string item = ClickyLiveSteps.ReadableSearch(this.FirstQuery ?? "").Item2;
          string text3;
          if (!flag || this.SearchCount != 1)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
            defaultInterpolatedStringHandler.AppendFormatted<int>(this.SearchCount);
            defaultInterpolatedStringHandler.AppendLiteral(" search");
            defaultInterpolatedStringHandler.AppendFormatted((this.SearchCount == 1) ? "" : "es");
            text3 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text3 = ((item.Length == 0) ? "the web" : ClickyLiveSteps.Shortened(item, 34).Replace("…", ""));
          }
          string text4 = text3;
          list.Add(new ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>(2, 0, text4, ClickyInkWorkStep.StepKind.Search));
        }
        if (list3.Count > 0)
        {
          list.Add(new ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>(3, 3, (flag ? "" : "checking ") + ClickyProgramBatch.Joined(list3), ClickyInkWorkStep.StepKind.Apps));
        }
        List<ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>> list4 = list.OrderBy<ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>, int>(([TupleElementNames(new string[] { "Priority", "Display", "Text", "Glyph" })] ValueTuple<int, int, string, ClickyInkWorkStep.StepKind> p) => p.Item1).Take<ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>>(2).ToList<ValueTuple<int, int, string, ClickyInkWorkStep.StepKind>>();
        if (list4.Count == 0)
        {
          return null;
        }
        return new ValueTuple<string, ClickyInkWorkStep.StepKind>?(new ValueTuple<string, ClickyInkWorkStep.StepKind>(string.Join(" · ", from p in list4
          orderby p.Item2
          select p.Item3), list4[0].Item4));
      }
    }

    // Token: 0x170001BD RID: 445
    // (get) Token: 0x060006B1 RID: 1713 RVA: 0x000F7970 File Offset: 0x000F7970
    public IReadOnlyList<ClickyLiveLineIcon> Icons
    {
      get
      {
        HashSet<string> seen = new HashSet<string>();
        return (from a in this.Apps
          where seen.Add(a.Toolkit)
          select ClickyLiveLineIcon.App(a.Toolkit, a.Name, a.LogoUrl)).Concat<ClickyLiveLineIcon>(this.Sites.Select<ClickyProgramBatch.SiteEntry, ClickyLiveLineIcon>((ClickyProgramBatch.SiteEntry s) => ClickyLiveLineIcon.Site(s.Host))).ToList<ClickyLiveLineIcon>();
      }
    }

    // Token: 0x060006B2 RID: 1714 RVA: 0x000F7A00 File Offset: 0x000F7A00
    [NullableContext(2)]
    public ClickyInkWorkStep WorkStep(DateTimeOffset at)
    {
      if (this.SiteCount > 1)
      {
        ClickyInkWorkStep.StepKind stepKind = ClickyInkWorkStep.StepKind.Read;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.SiteCount);
        defaultInterpolatedStringHandler.AppendLiteral(" sites");
        return new ClickyInkWorkStep(stepKind, defaultInterpolatedStringHandler.ToStringAndClear(), at);
      }
      ClickyProgramBatch.SiteEntry siteEntry = this.Sites.FirstOrDefault<ClickyProgramBatch.SiteEntry>();
      if (siteEntry != null && this.SiteCount == 1)
      {
        return new ClickyInkWorkStep(ClickyInkWorkStep.StepKind.Read, siteEntry.Host, at);
      }
      if (this.SearchCount > 0)
      {
        return new ClickyInkWorkStep(ClickyInkWorkStep.StepKind.Search, null, at);
      }
      ClickyProgramBatch.AppEntry appEntry = this.Apps.FirstOrDefault<ClickyProgramBatch.AppEntry>();
      if (appEntry != null)
      {
        return new ClickyInkWorkStep(ClickyInkWorkStep.StepKind.Apps, appEntry.Name, at);
      }
      return null;
    }

    // Token: 0x170001BE RID: 446
    // (get) Token: 0x060006B3 RID: 1715 RVA: 0x000F7AA0 File Offset: 0x000F7AA0
    public IReadOnlyList<string> SitesRead
    {
      get
      {
        return (from s in this.Sites
          where s.Done
          select s.Host).ToList<string>();
      }
    }

    // Token: 0x060006B4 RID: 1716 RVA: 0x000F7B00 File Offset: 0x000F7B00
    private static string WriteText(ClickyProgramBatch.AppEntry app)
    {
      bool flag = app.Done < app.Count;
      string kind = app.Kind;
      string text;
      if (!(kind == "send"))
      {
        if (!(kind == "create"))
        {
          if (!(kind == "update"))
          {
            if (!(kind == "delete"))
            {
              text = (flag ? "working" : "worked");
            }
            else
            {
              text = (flag ? "deleting" : "deleted");
            }
          }
          else
          {
            text = (flag ? "updating" : "updated");
          }
        }
        else
        {
          text = (flag ? "creating" : "created");
        }
      }
      else
      {
        text = (flag ? "sending" : "sent");
      }
      string text2 = text;
      if (app.Count <= 1)
      {
        return text2 + " in " + app.Name;
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(app.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" in ");
      defaultInterpolatedStringHandler.AppendFormatted(app.Name);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060006B5 RID: 1717 RVA: 0x000F7C18 File Offset: 0x000F7C18
    private static List<string> UniqueNames(IEnumerable<ClickyProgramBatch.AppEntry> apps)
    {
      HashSet<string> hashSet = new HashSet<string>();
      return apps.Select<ClickyProgramBatch.AppEntry, string>((ClickyProgramBatch.AppEntry a) => a.Name).Where<string>(new Func<string, bool>(hashSet.Add)).ToList<string>();
    }

    // Token: 0x060006B6 RID: 1718 RVA: 0x000F7C68 File Offset: 0x000F7C68
    private static string Joined(IReadOnlyList<string> names)
    {
      if (names.Count <= 1)
      {
        return names.FirstOrDefault<string>() ?? "";
      }
      if (names.Count > 3)
      {
        string text = string.Join(", ", names.Take<string>(2));
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
        defaultInterpolatedStringHandler.AppendLiteral(" and ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(names.Count - 2);
        defaultInterpolatedStringHandler.AppendLiteral(" more");
        return text + defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return string.Join(", ", names.Take<string>(names.Count - 1)) + " and " + names[names.Count - 1];
    }

    // Token: 0x060006B7 RID: 1719 RVA: 0x000F7D18 File Offset: 0x000F7D18
    public ClickyProgramBatch NamingApps([TupleElementNames(new string[] { "DisplayName", "LogoUrl", "Toolkits", "Slug", "Logo" })] [Nullable(new byte[] { 1, 0, 1, 2, 1, 0, 1, 2 })] IReadOnlyList<ValueTuple<string, string, IReadOnlyList<ValueTuple<string, string>>>> integrations)
    {
      ClickyProgramBatch clickyProgramBatch = this.<Clone>$();
      clickyProgramBatch.Apps = this.Apps.Select<ClickyProgramBatch.AppEntry, ClickyProgramBatch.AppEntry>(delegate(ClickyProgramBatch.AppEntry app)
      {
        string text = app.Toolkit.ToLowerInvariant();
        string slug = app.Slug;
        string text2 = ((slug != null) ? slug.ToLowerInvariant() : null) ?? text;
        ValueTuple<string, string, string, string>? valueTuple = null;
        foreach (ValueTuple<string, string, IReadOnlyList<ValueTuple<string, string>>> valueTuple2 in integrations)
        {
          foreach (ValueTuple<string, string> valueTuple3 in valueTuple2.Item3)
          {
            string text3 = valueTuple3.Item1.ToLowerInvariant();
            if ((!(text3 != text) || text2.StartsWith(text3 + "_", StringComparison.Ordinal)) && text3.Length > ((valueTuple != null) ? valueTuple.GetValueOrDefault().Item3.Length : 0))
            {
              valueTuple = new ValueTuple<string, string, string, string>?(new ValueTuple<string, string, string, string>(valueTuple2.Item1, valueTuple2.Item2, text3, valueTuple3.Item2));
            }
          }
        }
        if (valueTuple != null)
        {
          ValueTuple<string, string, string, string> valueOrDefault = valueTuple.GetValueOrDefault();
          ClickyProgramBatch.AppEntry appEntry = app.<Clone>$();
          appEntry.Toolkit = valueOrDefault.Item3;
          appEntry.Name = valueOrDefault.Item1;
          string text4 = valueOrDefault.Item4;
          appEntry.LogoUrl = ((text4 != null && text4.Length > 0) ? valueOrDefault.Item4 : valueOrDefault.Item2);
          return appEntry;
        }
        ClickyProgramBatch.AppEntry appEntry2 = app.<Clone>$();
        string text5;
        if (text.Length != 0)
        {
          char c = char.ToUpperInvariant(text[0]);
          ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
          string text4 = text;
          text5 = readOnlySpan + text4.Substring(1, text4.Length - 1);
        }
        else
        {
          text5 = text;
        }
        appEntry2.Name = text5;
        return appEntry2;
      }).ToList<ClickyProgramBatch.AppEntry>();
      return clickyProgramBatch;
    }

    // Token: 0x060006B8 RID: 1720 RVA: 0x000F7D5C File Offset: 0x000F7D5C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyProgramBatch");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060006B9 RID: 1721 RVA: 0x000F7DA8 File Offset: 0x000F7DA8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", SearchCount = ");
      builder.Append(this.SearchCount.ToString());
      builder.Append(", SearchesDone = ");
      builder.Append(this.SearchesDone.ToString());
      builder.Append(", FirstQuery = ");
      builder.Append(this.FirstQuery);
      builder.Append(", Sites = ");
      builder.Append(this.Sites);
      builder.Append(", SiteCount = ");
      builder.Append(this.SiteCount.ToString());
      builder.Append(", SitesDone = ");
      builder.Append(this.SitesDone.ToString());
      builder.Append(", Apps = ");
      builder.Append(this.Apps);
      builder.Append(", Summary = ");
      builder.Append(this.Summary.ToString());
      builder.Append(", Icons = ");
      builder.Append(this.Icons);
      builder.Append(", SitesRead = ");
      builder.Append(this.SitesRead);
      return true;
    }

    // Token: 0x060006BA RID: 1722 RVA: 0x000F7F14 File Offset: 0x000F7F14
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyProgramBatch left, ClickyProgramBatch right)
    {
      return !(left == right);
    }

    // Token: 0x060006BB RID: 1723 RVA: 0x000F7F20 File Offset: 0x000F7F20
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyProgramBatch left, ClickyProgramBatch right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060006BC RID: 1724 RVA: 0x000F7F34 File Offset: 0x000F7F34
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<SearchCount>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<SearchesDone>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FirstQuery>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ClickyProgramBatch.SiteEntry>>.Default.GetHashCode(this.<Sites>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<SiteCount>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<SitesDone>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ClickyProgramBatch.AppEntry>>.Default.GetHashCode(this.<Apps>k__BackingField);
    }

    // Token: 0x060006BD RID: 1725 RVA: 0x000F800C File Offset: 0x000F800C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyProgramBatch);
    }

    // Token: 0x060006BE RID: 1726 RVA: 0x000F801C File Offset: 0x000F801C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyProgramBatch other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<SearchCount>k__BackingField, other.<SearchCount>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<SearchesDone>k__BackingField, other.<SearchesDone>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FirstQuery>k__BackingField, other.<FirstQuery>k__BackingField) && EqualityComparer<IReadOnlyList<ClickyProgramBatch.SiteEntry>>.Default.Equals(this.<Sites>k__BackingField, other.<Sites>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<SiteCount>k__BackingField, other.<SiteCount>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<SitesDone>k__BackingField, other.<SitesDone>k__BackingField) && EqualityComparer<IReadOnlyList<ClickyProgramBatch.AppEntry>>.Default.Equals(this.<Apps>k__BackingField, other.<Apps>k__BackingField));
    }

    // Token: 0x060006C0 RID: 1728 RVA: 0x000F811C File Offset: 0x000F811C
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ClickyProgramBatch(ClickyProgramBatch original)
    {
      this.Id = original.<Id>k__BackingField;
      this.SearchCount = original.<SearchCount>k__BackingField;
      this.SearchesDone = original.<SearchesDone>k__BackingField;
      this.FirstQuery = original.<FirstQuery>k__BackingField;
      this.Sites = original.<Sites>k__BackingField;
      this.SiteCount = original.<SiteCount>k__BackingField;
      this.SitesDone = original.<SitesDone>k__BackingField;
      this.Apps = original.<Apps>k__BackingField;
    }

    // Token: 0x060006C1 RID: 1729 RVA: 0x000F8190 File Offset: 0x000F8190
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ClickyProgramBatch()
    {
      this.Sites = Array.Empty<ClickyProgramBatch.SiteEntry>();
      this.Apps = Array.Empty<ClickyProgramBatch.AppEntry>();
      base..ctor();
    }

    // Token: 0x060006C2 RID: 1730 RVA: 0x000F81B0 File Offset: 0x000F81B0
    [NullableContext(2)]
    [CompilerGenerated]
    internal static string <FromEvent>g__Str|37_0(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x060006C3 RID: 1731 RVA: 0x000F81D4 File Offset: 0x000F81D4
    [NullableContext(2)]
    [CompilerGenerated]
    internal static int? <FromEvent>g__Int|37_1(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      int num;
      if (jsonValue == null || !jsonValue.TryGetValue<int>(out num))
      {
        return null;
      }
      return new int?(num);
    }

    // Token: 0x060006C4 RID: 1732 RVA: 0x000F8208 File Offset: 0x000F8208
    [NullableContext(2)]
    [CompilerGenerated]
    internal static bool? <FromEvent>g__Bool|37_2(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      bool flag;
      if (jsonValue == null || !jsonValue.TryGetValue<bool>(out flag))
      {
        return null;
      }
      return new bool?(flag);
    }

    // Token: 0x04000275 RID: 629
    public const int IconLimit = 4;

    // Token: 0x02000177 RID: 375
    [Nullable(0)]
    public sealed class SiteEntry : IEquatable<ClickyProgramBatch.SiteEntry>
    {
      // Token: 0x06000D44 RID: 3396 RVA: 0x00114F1C File Offset: 0x00114F1C
      public SiteEntry(string Host, bool Done)
      {
        this.Host = Host;
        this.Done = Done;
        base..ctor();
      }

      // Token: 0x170002BC RID: 700
      // (get) Token: 0x06000D45 RID: 3397 RVA: 0x00114F34 File Offset: 0x00114F34
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyProgramBatch.SiteEntry);
        }
      }

      // Token: 0x170002BD RID: 701
      // (get) Token: 0x06000D46 RID: 3398 RVA: 0x00114F40 File Offset: 0x00114F40
      // (set) Token: 0x06000D47 RID: 3399 RVA: 0x00114F48 File Offset: 0x00114F48
      public string Host { get; set; }

      // Token: 0x170002BE RID: 702
      // (get) Token: 0x06000D48 RID: 3400 RVA: 0x00114F54 File Offset: 0x00114F54
      // (set) Token: 0x06000D49 RID: 3401 RVA: 0x00114F5C File Offset: 0x00114F5C
      public bool Done { get; set; }

      // Token: 0x06000D4A RID: 3402 RVA: 0x00114F68 File Offset: 0x00114F68
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SiteEntry");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000D4B RID: 3403 RVA: 0x00114FB4 File Offset: 0x00114FB4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Host = ");
        builder.Append(this.Host);
        builder.Append(", Done = ");
        builder.Append(this.Done.ToString());
        return true;
      }

      // Token: 0x06000D4C RID: 3404 RVA: 0x00115008 File Offset: 0x00115008
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyProgramBatch.SiteEntry left, ClickyProgramBatch.SiteEntry right)
      {
        return !(left == right);
      }

      // Token: 0x06000D4D RID: 3405 RVA: 0x00115014 File Offset: 0x00115014
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyProgramBatch.SiteEntry left, ClickyProgramBatch.SiteEntry right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000D4E RID: 3406 RVA: 0x00115028 File Offset: 0x00115028
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Host>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Done>k__BackingField);
      }

      // Token: 0x06000D4F RID: 3407 RVA: 0x00115068 File Offset: 0x00115068
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyProgramBatch.SiteEntry);
      }

      // Token: 0x06000D50 RID: 3408 RVA: 0x00115078 File Offset: 0x00115078
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyProgramBatch.SiteEntry other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Host>k__BackingField, other.<Host>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Done>k__BackingField, other.<Done>k__BackingField));
      }

      // Token: 0x06000D52 RID: 3410 RVA: 0x001150DC File Offset: 0x001150DC
      [CompilerGenerated]
      private SiteEntry(ClickyProgramBatch.SiteEntry original)
      {
        this.Host = original.<Host>k__BackingField;
        this.Done = original.<Done>k__BackingField;
      }

      // Token: 0x06000D53 RID: 3411 RVA: 0x001150FC File Offset: 0x001150FC
      [CompilerGenerated]
      public void Deconstruct(out string Host, out bool Done)
      {
        Host = this.Host;
        Done = this.Done;
      }
    }

    // Token: 0x02000178 RID: 376
    [Nullable(0)]
    [RequiredMember]
    public sealed class AppEntry : IEquatable<ClickyProgramBatch.AppEntry>
    {
      // Token: 0x170002BF RID: 703
      // (get) Token: 0x06000D54 RID: 3412 RVA: 0x00115110 File Offset: 0x00115110
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyProgramBatch.AppEntry);
        }
      }

      // Token: 0x170002C0 RID: 704
      // (get) Token: 0x06000D55 RID: 3413 RVA: 0x0011511C File Offset: 0x0011511C
      // (set) Token: 0x06000D56 RID: 3414 RVA: 0x00115124 File Offset: 0x00115124
      [RequiredMember]
      public string Toolkit { get; set; }

      // Token: 0x170002C1 RID: 705
      // (get) Token: 0x06000D57 RID: 3415 RVA: 0x00115130 File Offset: 0x00115130
      // (set) Token: 0x06000D58 RID: 3416 RVA: 0x00115138 File Offset: 0x00115138
      [Nullable(2)]
      public string Slug
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170002C2 RID: 706
      // (get) Token: 0x06000D59 RID: 3417 RVA: 0x00115144 File Offset: 0x00115144
      // (set) Token: 0x06000D5A RID: 3418 RVA: 0x0011514C File Offset: 0x0011514C
      [RequiredMember]
      public string Name { get; set; }

      // Token: 0x170002C3 RID: 707
      // (get) Token: 0x06000D5B RID: 3419 RVA: 0x00115158 File Offset: 0x00115158
      // (set) Token: 0x06000D5C RID: 3420 RVA: 0x00115160 File Offset: 0x00115160
      [Nullable(2)]
      public string LogoUrl
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170002C4 RID: 708
      // (get) Token: 0x06000D5D RID: 3421 RVA: 0x0011516C File Offset: 0x0011516C
      // (set) Token: 0x06000D5E RID: 3422 RVA: 0x00115174 File Offset: 0x00115174
      public string Kind { get; set; }

      // Token: 0x170002C5 RID: 709
      // (get) Token: 0x06000D5F RID: 3423 RVA: 0x00115180 File Offset: 0x00115180
      // (set) Token: 0x06000D60 RID: 3424 RVA: 0x00115188 File Offset: 0x00115188
      public int Count { get; set; }

      // Token: 0x170002C6 RID: 710
      // (get) Token: 0x06000D61 RID: 3425 RVA: 0x00115194 File Offset: 0x00115194
      // (set) Token: 0x06000D62 RID: 3426 RVA: 0x0011519C File Offset: 0x0011519C
      public int Done { get; set; }

      // Token: 0x06000D63 RID: 3427 RVA: 0x001151A8 File Offset: 0x001151A8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("AppEntry");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000D64 RID: 3428 RVA: 0x001151F4 File Offset: 0x001151F4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Toolkit = ");
        builder.Append(this.Toolkit);
        builder.Append(", Slug = ");
        builder.Append(this.Slug);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", LogoUrl = ");
        builder.Append(this.LogoUrl);
        builder.Append(", Kind = ");
        builder.Append(this.Kind);
        builder.Append(", Count = ");
        builder.Append(this.Count.ToString());
        builder.Append(", Done = ");
        builder.Append(this.Done.ToString());
        return true;
      }

      // Token: 0x06000D65 RID: 3429 RVA: 0x001152D4 File Offset: 0x001152D4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyProgramBatch.AppEntry left, ClickyProgramBatch.AppEntry right)
      {
        return !(left == right);
      }

      // Token: 0x06000D66 RID: 3430 RVA: 0x001152E0 File Offset: 0x001152E0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyProgramBatch.AppEntry left, ClickyProgramBatch.AppEntry right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000D67 RID: 3431 RVA: 0x001152F4 File Offset: 0x001152F4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Toolkit>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LogoUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Count>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Done>k__BackingField);
      }

      // Token: 0x06000D68 RID: 3432 RVA: 0x001153B4 File Offset: 0x001153B4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyProgramBatch.AppEntry);
      }

      // Token: 0x06000D69 RID: 3433 RVA: 0x001153C4 File Offset: 0x001153C4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyProgramBatch.AppEntry other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Toolkit>k__BackingField, other.<Toolkit>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LogoUrl>k__BackingField, other.<LogoUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Count>k__BackingField, other.<Count>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Done>k__BackingField, other.<Done>k__BackingField));
      }

      // Token: 0x06000D6B RID: 3435 RVA: 0x001154AC File Offset: 0x001154AC
      [CompilerGenerated]
      [SetsRequiredMembers]
      private AppEntry(ClickyProgramBatch.AppEntry original)
      {
        this.Toolkit = original.<Toolkit>k__BackingField;
        this.Slug = original.<Slug>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.LogoUrl = original.<LogoUrl>k__BackingField;
        this.Kind = original.<Kind>k__BackingField;
        this.Count = original.<Count>k__BackingField;
        this.Done = original.<Done>k__BackingField;
      }

      // Token: 0x06000D6C RID: 3436 RVA: 0x00115514 File Offset: 0x00115514
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public AppEntry()
      {
        this.Kind = "read";
        base..ctor();
      }
    }
  }
}
