using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200017C RID: 380
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class ClickyInkSheetRowKind : IEquatable<ClickyInkSheetRowKind>
  {
    // Token: 0x17000530 RID: 1328
    // (get) Token: 0x060016C1 RID: 5825 RVA: 0x003C0A3C File Offset: 0x003C0A3C
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkSheetRowKind);
      }
    }

    // Token: 0x060016C2 RID: 5826 RVA: 0x003C0A48 File Offset: 0x003C0A48
    public ClickyInkSheetRowKind Resolved(double asOf, double liveClock)
    {
      if (asOf != liveClock)
      {
        ClickyInkSheetRowKind.Group group = this as ClickyInkSheetRowKind.Group;
        if (group != null)
        {
          Func<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words> <>9__2;
          Func<ClickyInkLiveNote.Item, ClickyInkLiveNote.Item> <>9__1;
          List<ClickyInkLiveNote.Block> list = group.Value.Blocks.Select<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block>(delegate(ClickyInkLiveNote.Block original)
          {
            ClickyInkLiveNote.Block block = original.Clone();
            ClickyInkLiveNote.Words words = block.Words.<Clone>$();
            words.AsOf = new double?(asOf);
            block.Words = words;
            IEnumerable<ClickyInkLiveNote.Item> items = block.Items;
            Func<ClickyInkLiveNote.Item, ClickyInkLiveNote.Item> func;
            if ((func = <>9__1) == null)
            {
              func = (<>9__1 = delegate(ClickyInkLiveNote.Item originalItem)
              {
                ClickyInkLiveNote.Item item = originalItem.Clone();
                ClickyInkLiveNote.Words words2 = item.Words.<Clone>$();
                words2.AsOf = new double?(asOf);
                item.Words = words2;
                ClickyInkLiveNote.Words detail = item.Detail;
                object obj;
                if (detail == null)
                {
                  obj = null;
                }
                else
                {
                  (obj = detail.<Clone>$()).AsOf = new double?(asOf);
                }
                item.Detail = obj;
                IEnumerable<ClickyInkLiveNote.Words> cells = item.Cells;
                Func<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words> func2;
                if ((func2 = <>9__2) == null)
                {
                  func2 = (<>9__2 = delegate(ClickyInkLiveNote.Words cell)
                  {
                    ClickyInkLiveNote.Words words3 = cell.<Clone>$();
                    words3.AsOf = new double?(asOf);
                    return words3;
                  });
                }
                item.Cells = cells.Select<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words>(func2).ToList<ClickyInkLiveNote.Words>();
                return item;
              });
            }
            block.Items = items.Select<ClickyInkLiveNote.Item, ClickyInkLiveNote.Item>(func).ToList<ClickyInkLiveNote.Item>();
            return block;
          }).ToList<ClickyInkLiveNote.Block>();
          ClickyInkGroup clickyInkGroup = group.Value.<Clone>$();
          clickyInkGroup.Blocks = list;
          return new ClickyInkSheetRowKind.Group(clickyInkGroup, group.Resized);
        }
      }
      return this;
    }

    // Token: 0x060016C3 RID: 5827 RVA: 0x003C0AB8 File Offset: 0x003C0AB8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkSheetRowKind");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060016C4 RID: 5828 RVA: 0x003C0B04 File Offset: 0x003C0B04
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x060016C5 RID: 5829 RVA: 0x003C0B08 File Offset: 0x003C0B08
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkSheetRowKind left, ClickyInkSheetRowKind right)
    {
      return !(left == right);
    }

    // Token: 0x060016C6 RID: 5830 RVA: 0x003C0B14 File Offset: 0x003C0B14
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkSheetRowKind left, ClickyInkSheetRowKind right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060016C7 RID: 5831 RVA: 0x003C0B28 File Offset: 0x003C0B28
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x060016C8 RID: 5832 RVA: 0x003C0B3C File Offset: 0x003C0B3C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkSheetRowKind);
    }

    // Token: 0x060016C9 RID: 5833 RVA: 0x003C0B4C File Offset: 0x003C0B4C
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(ClickyInkSheetRowKind other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x060016CB RID: 5835 RVA: 0x003C0B6C File Offset: 0x003C0B6C
    [CompilerGenerated]
    protected ClickyInkSheetRowKind(ClickyInkSheetRowKind original)
    {
    }

    // Token: 0x060016CC RID: 5836 RVA: 0x003C0B74 File Offset: 0x003C0B74
    protected ClickyInkSheetRowKind()
    {
    }

    // Token: 0x02000552 RID: 1362
    [Nullable(0)]
    public sealed class Title : ClickyInkSheetRowKind, IEquatable<ClickyInkSheetRowKind.Title>
    {
      // Token: 0x060033E5 RID: 13285 RVA: 0x0043E48C File Offset: 0x0043E48C
      public Title(ClickyInkLiveNote.Words Words)
      {
        this.Words = Words;
        base..ctor();
      }

      // Token: 0x17000937 RID: 2359
      // (get) Token: 0x060033E6 RID: 13286 RVA: 0x0043E49C File Offset: 0x0043E49C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetRowKind.Title);
        }
      }

      // Token: 0x17000938 RID: 2360
      // (get) Token: 0x060033E7 RID: 13287 RVA: 0x0043E4A8 File Offset: 0x0043E4A8
      // (set) Token: 0x060033E8 RID: 13288 RVA: 0x0043E4B0 File Offset: 0x0043E4B0
      public ClickyInkLiveNote.Words Words { get; set; }

      // Token: 0x060033E9 RID: 13289 RVA: 0x0043E4BC File Offset: 0x0043E4BC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Title");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060033EA RID: 13290 RVA: 0x0043E508 File Offset: 0x0043E508
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Words = ");
        builder.Append(this.Words);
        return true;
      }

      // Token: 0x060033EB RID: 13291 RVA: 0x0043E53C File Offset: 0x0043E53C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetRowKind.Title left, ClickyInkSheetRowKind.Title right)
      {
        return !(left == right);
      }

      // Token: 0x060033EC RID: 13292 RVA: 0x0043E548 File Offset: 0x0043E548
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetRowKind.Title left, ClickyInkSheetRowKind.Title right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060033ED RID: 13293 RVA: 0x0043E55C File Offset: 0x0043E55C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<ClickyInkLiveNote.Words>.Default.GetHashCode(this.<Words>k__BackingField);
      }

      // Token: 0x060033EE RID: 13294 RVA: 0x0043E57C File Offset: 0x0043E57C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetRowKind.Title);
      }

      // Token: 0x060033EF RID: 13295 RVA: 0x0043E58C File Offset: 0x0043E58C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkSheetRowKind other)
      {
        return this.Equals(other);
      }

      // Token: 0x060033F0 RID: 13296 RVA: 0x0043E598 File Offset: 0x0043E598
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetRowKind.Title other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<ClickyInkLiveNote.Words>.Default.Equals(this.<Words>k__BackingField, other.<Words>k__BackingField));
      }

      // Token: 0x060033F2 RID: 13298 RVA: 0x0043E5CC File Offset: 0x0043E5CC
      [CompilerGenerated]
      private Title(ClickyInkSheetRowKind.Title original)
        : base(original)
      {
        this.Words = original.<Words>k__BackingField;
      }

      // Token: 0x060033F3 RID: 13299 RVA: 0x0043E5E4 File Offset: 0x0043E5E4
      [CompilerGenerated]
      public void Deconstruct(out ClickyInkLiveNote.Words Words)
      {
        Words = this.Words;
      }
    }

    // Token: 0x02000553 RID: 1363
    [Nullable(0)]
    public sealed class PageName : ClickyInkSheetRowKind, IEquatable<ClickyInkSheetRowKind.PageName>
    {
      // Token: 0x060033F4 RID: 13300 RVA: 0x0043E5F0 File Offset: 0x0043E5F0
      public PageName(int Index, string PageID, ClickyInkLiveNote.Words Name, double LandsAt, double? VanishesAt)
      {
        this.Index = Index;
        this.PageID = PageID;
        this.Name = Name;
        this.LandsAt = LandsAt;
        this.VanishesAt = VanishesAt;
        base..ctor();
      }

      // Token: 0x17000939 RID: 2361
      // (get) Token: 0x060033F5 RID: 13301 RVA: 0x0043E620 File Offset: 0x0043E620
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetRowKind.PageName);
        }
      }

      // Token: 0x1700093A RID: 2362
      // (get) Token: 0x060033F6 RID: 13302 RVA: 0x0043E62C File Offset: 0x0043E62C
      // (set) Token: 0x060033F7 RID: 13303 RVA: 0x0043E634 File Offset: 0x0043E634
      public int Index { get; set; }

      // Token: 0x1700093B RID: 2363
      // (get) Token: 0x060033F8 RID: 13304 RVA: 0x0043E640 File Offset: 0x0043E640
      // (set) Token: 0x060033F9 RID: 13305 RVA: 0x0043E648 File Offset: 0x0043E648
      public string PageID { get; set; }

      // Token: 0x1700093C RID: 2364
      // (get) Token: 0x060033FA RID: 13306 RVA: 0x0043E654 File Offset: 0x0043E654
      // (set) Token: 0x060033FB RID: 13307 RVA: 0x0043E65C File Offset: 0x0043E65C
      public ClickyInkLiveNote.Words Name { get; set; }

      // Token: 0x1700093D RID: 2365
      // (get) Token: 0x060033FC RID: 13308 RVA: 0x0043E668 File Offset: 0x0043E668
      // (set) Token: 0x060033FD RID: 13309 RVA: 0x0043E670 File Offset: 0x0043E670
      public double LandsAt { get; set; }

      // Token: 0x1700093E RID: 2366
      // (get) Token: 0x060033FE RID: 13310 RVA: 0x0043E67C File Offset: 0x0043E67C
      // (set) Token: 0x060033FF RID: 13311 RVA: 0x0043E684 File Offset: 0x0043E684
      public double? VanishesAt { get; set; }

      // Token: 0x06003400 RID: 13312 RVA: 0x0043E690 File Offset: 0x0043E690
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("PageName");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003401 RID: 13313 RVA: 0x0043E6DC File Offset: 0x0043E6DC
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Index = ");
        builder.Append(this.Index.ToString());
        builder.Append(", PageID = ");
        builder.Append(this.PageID);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", LandsAt = ");
        builder.Append(this.LandsAt.ToString());
        builder.Append(", VanishesAt = ");
        builder.Append(this.VanishesAt.ToString());
        return true;
      }

      // Token: 0x06003402 RID: 13314 RVA: 0x0043E7A8 File Offset: 0x0043E7A8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetRowKind.PageName left, ClickyInkSheetRowKind.PageName right)
      {
        return !(left == right);
      }

      // Token: 0x06003403 RID: 13315 RVA: 0x0043E7B4 File Offset: 0x0043E7B4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetRowKind.PageName left, ClickyInkSheetRowKind.PageName right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003404 RID: 13316 RVA: 0x0043E7C8 File Offset: 0x0043E7C8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Index>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PageID>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkLiveNote.Words>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<LandsAt>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<VanishesAt>k__BackingField);
      }

      // Token: 0x06003405 RID: 13317 RVA: 0x0043E850 File Offset: 0x0043E850
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetRowKind.PageName);
      }

      // Token: 0x06003406 RID: 13318 RVA: 0x0043E860 File Offset: 0x0043E860
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkSheetRowKind other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003407 RID: 13319 RVA: 0x0043E86C File Offset: 0x0043E86C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetRowKind.PageName other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Index>k__BackingField, other.<Index>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PageID>k__BackingField, other.<PageID>k__BackingField) && EqualityComparer<ClickyInkLiveNote.Words>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<LandsAt>k__BackingField, other.<LandsAt>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<VanishesAt>k__BackingField, other.<VanishesAt>k__BackingField));
      }

      // Token: 0x06003409 RID: 13321 RVA: 0x0043E90C File Offset: 0x0043E90C
      [CompilerGenerated]
      private PageName(ClickyInkSheetRowKind.PageName original)
        : base(original)
      {
        this.Index = original.<Index>k__BackingField;
        this.PageID = original.<PageID>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.LandsAt = original.<LandsAt>k__BackingField;
        this.VanishesAt = original.<VanishesAt>k__BackingField;
      }

      // Token: 0x0600340A RID: 13322 RVA: 0x0043E95C File Offset: 0x0043E95C
      [CompilerGenerated]
      public void Deconstruct(out int Index, out string PageID, out ClickyInkLiveNote.Words Name, out double LandsAt, out double? VanishesAt)
      {
        Index = this.Index;
        PageID = this.PageID;
        Name = this.Name;
        LandsAt = this.LandsAt;
        VanishesAt = this.VanishesAt;
      }
    }

    // Token: 0x02000554 RID: 1364
    [Nullable(0)]
    public sealed class Group : ClickyInkSheetRowKind, IEquatable<ClickyInkSheetRowKind.Group>
    {
      // Token: 0x0600340B RID: 13323 RVA: 0x0043E98C File Offset: 0x0043E98C
      public Group(ClickyInkGroup Value, [Nullable(2)] ClickyInkSheetRowKind.Resize Resized)
      {
        this.Value = Value;
        this.Resized = Resized;
        base..ctor();
      }

      // Token: 0x1700093F RID: 2367
      // (get) Token: 0x0600340C RID: 13324 RVA: 0x0043E9A4 File Offset: 0x0043E9A4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetRowKind.Group);
        }
      }

      // Token: 0x17000940 RID: 2368
      // (get) Token: 0x0600340D RID: 13325 RVA: 0x0043E9B0 File Offset: 0x0043E9B0
      // (set) Token: 0x0600340E RID: 13326 RVA: 0x0043E9B8 File Offset: 0x0043E9B8
      public ClickyInkGroup Value { get; set; }

      // Token: 0x17000941 RID: 2369
      // (get) Token: 0x0600340F RID: 13327 RVA: 0x0043E9C4 File Offset: 0x0043E9C4
      // (set) Token: 0x06003410 RID: 13328 RVA: 0x0043E9CC File Offset: 0x0043E9CC
      [Nullable(2)]
      public ClickyInkSheetRowKind.Resize Resized
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06003411 RID: 13329 RVA: 0x0043E9D8 File Offset: 0x0043E9D8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Group");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003412 RID: 13330 RVA: 0x0043EA24 File Offset: 0x0043EA24
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Value = ");
        builder.Append(this.Value);
        builder.Append(", Resized = ");
        builder.Append(this.Resized);
        return true;
      }

      // Token: 0x06003413 RID: 13331 RVA: 0x0043EA7C File Offset: 0x0043EA7C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetRowKind.Group left, ClickyInkSheetRowKind.Group right)
      {
        return !(left == right);
      }

      // Token: 0x06003414 RID: 13332 RVA: 0x0043EA88 File Offset: 0x0043EA88
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetRowKind.Group left, ClickyInkSheetRowKind.Group right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003415 RID: 13333 RVA: 0x0043EA9C File Offset: 0x0043EA9C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<ClickyInkGroup>.Default.GetHashCode(this.<Value>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkSheetRowKind.Resize>.Default.GetHashCode(this.<Resized>k__BackingField);
      }

      // Token: 0x06003416 RID: 13334 RVA: 0x0043EAD4 File Offset: 0x0043EAD4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetRowKind.Group);
      }

      // Token: 0x06003417 RID: 13335 RVA: 0x0043EAE4 File Offset: 0x0043EAE4
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkSheetRowKind other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003418 RID: 13336 RVA: 0x0043EAF0 File Offset: 0x0043EAF0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetRowKind.Group other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<ClickyInkGroup>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField) && EqualityComparer<ClickyInkSheetRowKind.Resize>.Default.Equals(this.<Resized>k__BackingField, other.<Resized>k__BackingField));
      }

      // Token: 0x0600341A RID: 13338 RVA: 0x0043EB44 File Offset: 0x0043EB44
      [CompilerGenerated]
      private Group(ClickyInkSheetRowKind.Group original)
        : base(original)
      {
        this.Value = original.<Value>k__BackingField;
        this.Resized = original.<Resized>k__BackingField;
      }

      // Token: 0x0600341B RID: 13339 RVA: 0x0043EB68 File Offset: 0x0043EB68
      [CompilerGenerated]
      public void Deconstruct(out ClickyInkGroup Value, [Nullable(2)] out ClickyInkSheetRowKind.Resize Resized)
      {
        Value = this.Value;
        Resized = this.Resized;
      }
    }

    // Token: 0x02000555 RID: 1365
    [Nullable(0)]
    public sealed class Resize : IEquatable<ClickyInkSheetRowKind.Resize>
    {
      // Token: 0x0600341C RID: 13340 RVA: 0x0043EB7C File Offset: 0x0043EB7C
      public Resize(double At, ClickyInkGroup Before)
      {
        this.At = At;
        this.Before = Before;
        base..ctor();
      }

      // Token: 0x17000942 RID: 2370
      // (get) Token: 0x0600341D RID: 13341 RVA: 0x0043EB94 File Offset: 0x0043EB94
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetRowKind.Resize);
        }
      }

      // Token: 0x17000943 RID: 2371
      // (get) Token: 0x0600341E RID: 13342 RVA: 0x0043EBA0 File Offset: 0x0043EBA0
      // (set) Token: 0x0600341F RID: 13343 RVA: 0x0043EBA8 File Offset: 0x0043EBA8
      public double At { get; set; }

      // Token: 0x17000944 RID: 2372
      // (get) Token: 0x06003420 RID: 13344 RVA: 0x0043EBB4 File Offset: 0x0043EBB4
      // (set) Token: 0x06003421 RID: 13345 RVA: 0x0043EBBC File Offset: 0x0043EBBC
      public ClickyInkGroup Before { get; set; }

      // Token: 0x06003422 RID: 13346 RVA: 0x0043EBC8 File Offset: 0x0043EBC8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Resize");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003423 RID: 13347 RVA: 0x0043EC14 File Offset: 0x0043EC14
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("At = ");
        builder.Append(this.At.ToString());
        builder.Append(", Before = ");
        builder.Append(this.Before);
        return true;
      }

      // Token: 0x06003424 RID: 13348 RVA: 0x0043EC68 File Offset: 0x0043EC68
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetRowKind.Resize left, ClickyInkSheetRowKind.Resize right)
      {
        return !(left == right);
      }

      // Token: 0x06003425 RID: 13349 RVA: 0x0043EC74 File Offset: 0x0043EC74
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetRowKind.Resize left, ClickyInkSheetRowKind.Resize right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003426 RID: 13350 RVA: 0x0043EC88 File Offset: 0x0043EC88
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<At>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkGroup>.Default.GetHashCode(this.<Before>k__BackingField);
      }

      // Token: 0x06003427 RID: 13351 RVA: 0x0043ECC8 File Offset: 0x0043ECC8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetRowKind.Resize);
      }

      // Token: 0x06003428 RID: 13352 RVA: 0x0043ECD8 File Offset: 0x0043ECD8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetRowKind.Resize other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField) && EqualityComparer<ClickyInkGroup>.Default.Equals(this.<Before>k__BackingField, other.<Before>k__BackingField));
      }

      // Token: 0x0600342A RID: 13354 RVA: 0x0043ED3C File Offset: 0x0043ED3C
      [CompilerGenerated]
      private Resize(ClickyInkSheetRowKind.Resize original)
      {
        this.At = original.<At>k__BackingField;
        this.Before = original.<Before>k__BackingField;
      }

      // Token: 0x0600342B RID: 13355 RVA: 0x0043ED5C File Offset: 0x0043ED5C
      [CompilerGenerated]
      public void Deconstruct(out double At, out ClickyInkGroup Before)
      {
        At = this.At;
        Before = this.Before;
      }
    }
  }
}
