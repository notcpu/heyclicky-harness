using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200014F RID: 335
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkGroup : IEquatable<ClickyInkGroup>
  {
    // Token: 0x170004DE RID: 1246
    // (get) Token: 0x060014DB RID: 5339 RVA: 0x003B1984 File Offset: 0x003B1984
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkGroup);
      }
    }

    // Token: 0x170004DF RID: 1247
    // (get) Token: 0x060014DC RID: 5340 RVA: 0x003B1990 File Offset: 0x003B1990
    // (set) Token: 0x060014DD RID: 5341 RVA: 0x003B1998 File Offset: 0x003B1998
    public ClickyInkDocument.Kind Kind { get; set; }

    // Token: 0x170004E0 RID: 1248
    // (get) Token: 0x060014DE RID: 5342 RVA: 0x003B19A4 File Offset: 0x003B19A4
    // (set) Token: 0x060014DF RID: 5343 RVA: 0x003B19AC File Offset: 0x003B19AC
    public List<ClickyInkLiveNote.Block> Blocks { get; set; }

    // Token: 0x060014E0 RID: 5344 RVA: 0x003B19B8 File Offset: 0x003B19B8
    public ClickyInkGroup(ClickyInkDocument.Kind kind, List<ClickyInkLiveNote.Block> blocks)
    {
      this.Kind = kind;
      this.Blocks = blocks;
    }

    // Token: 0x170004E1 RID: 1249
    // (get) Token: 0x060014E1 RID: 5345 RVA: 0x003B19D0 File Offset: 0x003B19D0
    public string Id
    {
      get
      {
        if (this.Blocks.Count <= 0)
        {
          return Guid.NewGuid().ToString();
        }
        return this.Blocks[0].Id;
      }
    }

    // Token: 0x170004E2 RID: 1250
    // (get) Token: 0x060014E2 RID: 5346 RVA: 0x003B1A10 File Offset: 0x003B1A10
    public bool IsMadeLink
    {
      get
      {
        return this.Kind == ClickyInkDocument.Kind.Link && this.Blocks.Count > 0 && this.Blocks[0].Source["_made"] != null;
      }
    }

    // Token: 0x170004E3 RID: 1251
    // (get) Token: 0x060014E3 RID: 5347 RVA: 0x003B1A50 File Offset: 0x003B1A50
    public bool IsSupportingFiles
    {
      get
      {
        return this.Kind == ClickyInkDocument.Kind.File && this.Blocks.Count > 0 && this.Blocks[0].Source["_supporting_files"] != null;
      }
    }

    // Token: 0x170004E4 RID: 1252
    // (get) Token: 0x060014E4 RID: 5348 RVA: 0x003B1A90 File Offset: 0x003B1A90
    public double LandsAt
    {
      get
      {
        if (this.Blocks.Count != 0)
        {
          return this.Blocks.Min<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.LandsAt);
        }
        return 0.0;
      }
    }

    // Token: 0x170004E5 RID: 1253
    // (get) Token: 0x060014E5 RID: 5349 RVA: 0x003B1AE0 File Offset: 0x003B1AE0
    public double? VanishesAt
    {
      get
      {
        if (!this.Blocks.Any<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.VanishesAt == null))
        {
          return new double?(this.Blocks.Max<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.VanishesAt.Value));
        }
        return null;
      }
    }

    // Token: 0x060014E6 RID: 5350 RVA: 0x003B1B54 File Offset: 0x003B1B54
    public unsafe static List<ClickyInkGroup> Groups(ClickyInkLiveNote.Page page, double clock)
    {
      List<ClickyInkLiveNote.Block> list = (from b in page.Blocks
        where b.IsPresent(clock)
        select b.DashboardResolved(clock)).ToList<ClickyInkLiveNote.Block>();
      HashSet<ClickyInkDocument.Kind> hashSet = new HashSet<ClickyInkDocument.Kind>
      {
        ClickyInkDocument.Kind.Figure,
        ClickyInkDocument.Kind.Ask,
        ClickyInkDocument.Kind.Link,
        ClickyInkDocument.Kind.Sticky,
        ClickyInkDocument.Kind.Photo,
        ClickyInkDocument.Kind.Status
      };
      List<ClickyInkGroup> list2 = new List<ClickyInkGroup>();
      HashSet<string> hashSet2 = ClickyInkMadeLink.Ids(page, list);
      foreach (ClickyInkLiveNote.Block block in list)
      {
        ClickyInkLiveNote.Block block2 = block;
        ClickyInkDocument.Kind kind = block2.Source.Kind;
        if (kind == ClickyInkDocument.Kind.Link && hashSet2.Contains(block2.Id))
        {
          block2 = ClickyInkGroup.WithAttribute(block2, "_made", "1", true);
          List<ClickyInkGroup> list3 = list2;
          ClickyInkDocument.Kind kind4 = kind;
          int num = 1;
          List<ClickyInkLiveNote.Block> list4 = new List<ClickyInkLiveNote.Block>(num);
          CollectionsMarshal.SetCount<ClickyInkLiveNote.Block>(list4, num);
          Span<ClickyInkLiveNote.Block> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Block>(list4);
          int num2 = 0;
          *span[num2] = block2;
          list3.Add(new ClickyInkGroup(kind4, list4));
        }
        else
        {
          string text = block2.Source["_tiles"];
          if (text != null)
          {
            if (list2.Count > 0)
            {
              List<ClickyInkGroup> list5 = list2;
              if (list5[list5.Count - 1].Kind == ClickyInkDocument.Kind.Tiles)
              {
                List<ClickyInkGroup> list6 = list2;
                if (list6[list6.Count - 1].Blocks.Count > 0)
                {
                  List<ClickyInkGroup> list7 = list2;
                  if (list7[list7.Count - 1].Blocks[0].Source["_tiles"] == text)
                  {
                    List<ClickyInkGroup> list8 = list2;
                    list8[list8.Count - 1].Blocks.Add(block2);
                    continue;
                  }
                }
              }
            }
            List<ClickyInkGroup> list9 = list2;
            ClickyInkDocument.Kind kind2 = ClickyInkDocument.Kind.Tiles;
            int num2 = 1;
            List<ClickyInkLiveNote.Block> list10 = new List<ClickyInkLiveNote.Block>(num2);
            CollectionsMarshal.SetCount<ClickyInkLiveNote.Block>(list10, num2);
            Span<ClickyInkLiveNote.Block> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Block>(list10);
            int num = 0;
            *span[num] = block2;
            list9.Add(new ClickyInkGroup(kind2, list10));
          }
          else
          {
            int num3 = list2.FindLastIndex((ClickyInkGroup g) => g.Kind == kind && !g.IsMadeLink);
            if (hashSet.Contains(kind) && num3 >= 0)
            {
              if (num3 != list2.Count - 1 && block2.GoneAt == null)
              {
                if (!list2.Skip<ClickyInkGroup>(num3 + 1).All<ClickyInkGroup>((ClickyInkGroup g) => g.Blocks.All<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.GoneAt != null)))
                {
                  goto IL_0295;
                }
              }
              list2[num3].Blocks.Add(block2);
              continue;
            }
            IL_0295:
            List<ClickyInkGroup> list11 = list2;
            ClickyInkDocument.Kind kind3 = kind;
            int num = 1;
            List<ClickyInkLiveNote.Block> list12 = new List<ClickyInkLiveNote.Block>(num);
            CollectionsMarshal.SetCount<ClickyInkLiveNote.Block>(list12, num);
            Span<ClickyInkLiveNote.Block> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Block>(list12);
            int num2 = 0;
            *span[num2] = block2;
            list11.Add(new ClickyInkGroup(kind3, list12));
          }
        }
      }
      list2 = ClickyInkGroup.MovingSupportingFilesToTheEnd(list2);
      for (int i = 0; i < list2.Count; i++)
      {
        if (list2[i].Kind == ClickyInkDocument.Kind.Link && !list2[i].IsMadeLink)
        {
          string text2 = ((i > 0 && list2[i - 1].Kind == ClickyInkDocument.Kind.Heading) ? list2[i - 1].Blocks[0].Words.Current : null);
          string text3 = ((i == 0) ? page.Name.Current : null);
          if ((text2 != null && ClickyInkGroup.NamesSources(text2)) || (text3 != null && ClickyInkGroup.NamesSources(text3)))
          {
            list2[i].Blocks[0] = ClickyInkGroup.WithAttribute(list2[i].Blocks[0], "_labelled", "1", false);
          }
        }
      }
      return list2;
    }

    // Token: 0x060014E7 RID: 5351 RVA: 0x003B1F50 File Offset: 0x003B1F50
    private static ClickyInkLiveNote.Block WithAttribute(ClickyInkLiveNote.Block block, string key, string value, bool lastStamp)
    {
      ClickyInkLiveNote.Block block2 = block.Clone();
      int num = (lastStamp ? (block2.Stamps.Count - 1) : 0);
      ClickyInkDocument.Block block3 = block2.Stamps[num].Value.Clone();
      block3.Attributes[key] = value;
      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> stamps = block2.Stamps;
      int num2 = num;
      ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> stamp = block2.Stamps[num];
      stamp.Value = block3;
      stamps[num2] = stamp;
      return block2;
    }

    // Token: 0x060014E8 RID: 5352 RVA: 0x003B1FC4 File Offset: 0x003B1FC4
    private static List<ClickyInkGroup> MovingSupportingFilesToTheEnd(List<ClickyInkGroup> groups)
    {
      ClickyInkGroup.<>c__DisplayClass24_0 CS$<>8__locals1 = new ClickyInkGroup.<>c__DisplayClass24_0();
      List<ClickyInkGroup> list = groups.Where<ClickyInkGroup>((ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.File).ToList<ClickyInkGroup>();
      if (list.Count <= 1)
      {
        return groups;
      }
      ClickyInkGroup.<>c__DisplayClass24_0 CS$<>8__locals2 = CS$<>8__locals1;
      Func<IReadOnlyList<string>, ISet<string>> supportingPaths = ClickyInkPaths.MainArtifacts.SupportingPaths;
      IEnumerable<ClickyInkGroup> enumerable = list;
      Func<ClickyInkGroup, string> func;
      if ((func = ClickyInkGroup.<>O.<0>__PathOf) == null)
      {
        func = (ClickyInkGroup.<>O.<0>__PathOf = new Func<ClickyInkGroup, string>(ClickyInkGroup.<MovingSupportingFilesToTheEnd>g__PathOf|24_1));
      }
      CS$<>8__locals2.supportingPaths = supportingPaths(enumerable.Select<ClickyInkGroup, string>(func).ToList<string>());
      CS$<>8__locals1.mainIDs = (from g in list.Where<ClickyInkGroup>(new Func<ClickyInkGroup, bool>(CS$<>8__locals1.<MovingSupportingFilesToTheEnd>g__IsMain|2))
        select g.Id).ToHashSet<string>();
      if (CS$<>8__locals1.mainIDs.Count == 0)
      {
        CS$<>8__locals1.mainIDs.Add(list[0].Id);
      }
      List<ClickyInkLiveNote.Block> list2 = list.Where<ClickyInkGroup>((ClickyInkGroup g) => !CS$<>8__locals1.mainIDs.Contains(g.Id)).SelectMany<ClickyInkGroup, ClickyInkLiveNote.Block>((ClickyInkGroup g) => g.Blocks).ToList<ClickyInkLiveNote.Block>();
      if (list2.Count == 0)
      {
        return groups;
      }
      ClickyInkLiveNote.Block block = ClickyInkGroup.WithAttribute(list2[0], "_supporting_files", "1", true);
      List<ClickyInkGroup> list3 = groups.Where<ClickyInkGroup>((ClickyInkGroup g) => g.Kind != ClickyInkDocument.Kind.File || CS$<>8__locals1.mainIDs.Contains(g.Id)).ToList<ClickyInkGroup>();
      ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.File;
      List<ClickyInkLiveNote.Block> list4 = new List<ClickyInkLiveNote.Block>();
      list4.Add(block);
      list4.AddRange(list2.Skip<ClickyInkLiveNote.Block>(1));
      list3.Add(new ClickyInkGroup(kind, list4));
      return list3;
    }

    // Token: 0x060014E9 RID: 5353 RVA: 0x003B214C File Offset: 0x003B214C
    public static bool NamesSources(string name)
    {
      string words = ClickyInkParser.Emphasis.Strip(name).ToLowerInvariant();
      return new string[] { "source", "link", "reference", "reading", "read more", "citation" }.Any<string>((string w) => words.Contains(w, StringComparison.Ordinal)) && words.CharacterCount() <= 32;
    }

    // Token: 0x060014EA RID: 5354 RVA: 0x003B21D0 File Offset: 0x003B21D0
    [NullableContext(2)]
    public bool Equals(ClickyInkGroup other)
    {
      return other != null && this.Kind == other.Kind && this.Blocks.SequenceEqual<ClickyInkLiveNote.Block>(other.Blocks);
    }

    // Token: 0x060014EB RID: 5355 RVA: 0x003B21F8 File Offset: 0x003B21F8
    public override int GetHashCode()
    {
      return HashCode.Combine<ClickyInkDocument.Kind, int>(this.Kind, this.Blocks.Count);
    }

    // Token: 0x060014EC RID: 5356 RVA: 0x003B2210 File Offset: 0x003B2210
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkGroup");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060014ED RID: 5357 RVA: 0x003B225C File Offset: 0x003B225C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Blocks = ");
      builder.Append(this.Blocks);
      builder.Append(", Id = ");
      builder.Append(this.Id);
      builder.Append(", IsMadeLink = ");
      builder.Append(this.IsMadeLink.ToString());
      builder.Append(", IsSupportingFiles = ");
      builder.Append(this.IsSupportingFiles.ToString());
      builder.Append(", LandsAt = ");
      builder.Append(this.LandsAt.ToString());
      builder.Append(", VanishesAt = ");
      builder.Append(this.VanishesAt.ToString());
      return true;
    }

    // Token: 0x060014EE RID: 5358 RVA: 0x003B2364 File Offset: 0x003B2364
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkGroup left, ClickyInkGroup right)
    {
      return !(left == right);
    }

    // Token: 0x060014EF RID: 5359 RVA: 0x003B2370 File Offset: 0x003B2370
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkGroup left, ClickyInkGroup right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060014F0 RID: 5360 RVA: 0x003B2384 File Offset: 0x003B2384
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkGroup);
    }

    // Token: 0x060014F2 RID: 5362 RVA: 0x003B239C File Offset: 0x003B239C
    [CompilerGenerated]
    private ClickyInkGroup(ClickyInkGroup original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Blocks = original.<Blocks>k__BackingField;
    }

    // Token: 0x060014F3 RID: 5363 RVA: 0x003B23BC File Offset: 0x003B23BC
    [CompilerGenerated]
    internal static string <MovingSupportingFilesToTheEnd>g__PathOf|24_1(ClickyInkGroup group)
    {
      return ClickyInkPaths.ExpandingTilde(group.Blocks[0].Source["path"] ?? "");
    }

    // Token: 0x04000920 RID: 2336
    public const string SupportingFilesAttribute = "_supporting_files";

    // Token: 0x020004EE RID: 1262
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001EC8 RID: 7880
      [Nullable(0)]
      public static Func<ClickyInkGroup, string> <0>__PathOf;
    }
  }
}
