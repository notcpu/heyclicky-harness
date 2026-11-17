using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200017D RID: 381
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class ClickyInkSheetRowInfo : IEquatable<ClickyInkSheetRowInfo>
  {
    // Token: 0x17000531 RID: 1329
    // (get) Token: 0x060016CD RID: 5837 RVA: 0x003C0B7C File Offset: 0x003C0B7C
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkSheetRowInfo);
      }
    }

    // Token: 0x060016CE RID: 5838 RVA: 0x003C0B88 File Offset: 0x003C0B88
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkSheetRowInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060016CF RID: 5839 RVA: 0x003C0BD4 File Offset: 0x003C0BD4
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x060016D0 RID: 5840 RVA: 0x003C0BD8 File Offset: 0x003C0BD8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkSheetRowInfo left, ClickyInkSheetRowInfo right)
    {
      return !(left == right);
    }

    // Token: 0x060016D1 RID: 5841 RVA: 0x003C0BE4 File Offset: 0x003C0BE4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkSheetRowInfo left, ClickyInkSheetRowInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060016D2 RID: 5842 RVA: 0x003C0BF8 File Offset: 0x003C0BF8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x060016D3 RID: 5843 RVA: 0x003C0C0C File Offset: 0x003C0C0C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkSheetRowInfo);
    }

    // Token: 0x060016D4 RID: 5844 RVA: 0x003C0C1C File Offset: 0x003C0C1C
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(ClickyInkSheetRowInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x060016D6 RID: 5846 RVA: 0x003C0C3C File Offset: 0x003C0C3C
    [CompilerGenerated]
    protected ClickyInkSheetRowInfo(ClickyInkSheetRowInfo original)
    {
    }

    // Token: 0x060016D7 RID: 5847 RVA: 0x003C0C44 File Offset: 0x003C0C44
    protected ClickyInkSheetRowInfo()
    {
    }

    // Token: 0x02000557 RID: 1367
    [Nullable(0)]
    public sealed class Background : ClickyInkSheetRowInfo, IEquatable<ClickyInkSheetRowInfo.Background>
    {
      // Token: 0x17000945 RID: 2373
      // (get) Token: 0x06003430 RID: 13360 RVA: 0x0043EE94 File Offset: 0x0043EE94
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetRowInfo.Background);
        }
      }

      // Token: 0x06003431 RID: 13361 RVA: 0x0043EEA0 File Offset: 0x0043EEA0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Background");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003432 RID: 13362 RVA: 0x0043EEEC File Offset: 0x0043EEEC
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003433 RID: 13363 RVA: 0x0043EEF8 File Offset: 0x0043EEF8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetRowInfo.Background left, ClickyInkSheetRowInfo.Background right)
      {
        return !(left == right);
      }

      // Token: 0x06003434 RID: 13364 RVA: 0x0043EF04 File Offset: 0x0043EF04
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetRowInfo.Background left, ClickyInkSheetRowInfo.Background right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003435 RID: 13365 RVA: 0x0043EF18 File Offset: 0x0043EF18
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003436 RID: 13366 RVA: 0x0043EF20 File Offset: 0x0043EF20
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetRowInfo.Background);
      }

      // Token: 0x06003437 RID: 13367 RVA: 0x0043EF30 File Offset: 0x0043EF30
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkSheetRowInfo other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003438 RID: 13368 RVA: 0x0043EF3C File Offset: 0x0043EF3C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetRowInfo.Background other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x0600343A RID: 13370 RVA: 0x0043EF54 File Offset: 0x0043EF54
      [CompilerGenerated]
      private Background(ClickyInkSheetRowInfo.Background original)
        : base(original)
      {
      }

      // Token: 0x0600343B RID: 13371 RVA: 0x0043EF60 File Offset: 0x0043EF60
      public Background()
      {
      }
    }

    // Token: 0x02000558 RID: 1368
    [Nullable(0)]
    public sealed class Row : ClickyInkSheetRowInfo, IEquatable<ClickyInkSheetRowInfo.Row>
    {
      // Token: 0x0600343C RID: 13372 RVA: 0x0043EF68 File Offset: 0x0043EF68
      public Row(IReadOnlyList<string> PlaceIDs, IReadOnlyDictionary<string, double> ItemFractions, IReadOnlyList<string> CollapsedAbove, IReadOnlyList<string> CollapsedBelow)
      {
        this.PlaceIDs = PlaceIDs;
        this.ItemFractions = ItemFractions;
        this.CollapsedAbove = CollapsedAbove;
        this.CollapsedBelow = CollapsedBelow;
        base..ctor();
      }

      // Token: 0x17000946 RID: 2374
      // (get) Token: 0x0600343D RID: 13373 RVA: 0x0043EF90 File Offset: 0x0043EF90
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetRowInfo.Row);
        }
      }

      // Token: 0x17000947 RID: 2375
      // (get) Token: 0x0600343E RID: 13374 RVA: 0x0043EF9C File Offset: 0x0043EF9C
      // (set) Token: 0x0600343F RID: 13375 RVA: 0x0043EFA4 File Offset: 0x0043EFA4
      public IReadOnlyList<string> PlaceIDs { get; set; }

      // Token: 0x17000948 RID: 2376
      // (get) Token: 0x06003440 RID: 13376 RVA: 0x0043EFB0 File Offset: 0x0043EFB0
      // (set) Token: 0x06003441 RID: 13377 RVA: 0x0043EFB8 File Offset: 0x0043EFB8
      public IReadOnlyDictionary<string, double> ItemFractions { get; set; }

      // Token: 0x17000949 RID: 2377
      // (get) Token: 0x06003442 RID: 13378 RVA: 0x0043EFC4 File Offset: 0x0043EFC4
      // (set) Token: 0x06003443 RID: 13379 RVA: 0x0043EFCC File Offset: 0x0043EFCC
      public IReadOnlyList<string> CollapsedAbove { get; set; }

      // Token: 0x1700094A RID: 2378
      // (get) Token: 0x06003444 RID: 13380 RVA: 0x0043EFD8 File Offset: 0x0043EFD8
      // (set) Token: 0x06003445 RID: 13381 RVA: 0x0043EFE0 File Offset: 0x0043EFE0
      public IReadOnlyList<string> CollapsedBelow { get; set; }

      // Token: 0x06003446 RID: 13382 RVA: 0x0043EFEC File Offset: 0x0043EFEC
      public Row(IReadOnlyList<string> placeIDs, IReadOnlyDictionary<string, double> itemFractions)
        : this(placeIDs, itemFractions, Array.Empty<string>(), Array.Empty<string>())
      {
      }

      // Token: 0x06003447 RID: 13383 RVA: 0x0043F000 File Offset: 0x0043F000
      [NullableContext(2)]
      public bool Equals(ClickyInkSheetRowInfo.Row other)
      {
        return other != null && this.PlaceIDs.SequenceEqual<string>(other.PlaceIDs) && this.ItemFractions.Count == other.ItemFractions.Count && this.ItemFractions.All<KeyValuePair<string, double>>(delegate(KeyValuePair<string, double> kv)
        {
          double num;
          return other.ItemFractions.TryGetValue(kv.Key, out num) && num == kv.Value;
        }) && this.CollapsedAbove.SequenceEqual<string>(other.CollapsedAbove) && this.CollapsedBelow.SequenceEqual<string>(other.CollapsedBelow);
      }

      // Token: 0x06003448 RID: 13384 RVA: 0x0043F0A0 File Offset: 0x0043F0A0
      public override int GetHashCode()
      {
        return HashCode.Combine<int, int, int, int>(this.PlaceIDs.Count, this.ItemFractions.Count, this.CollapsedAbove.Count, this.CollapsedBelow.Count);
      }

      // Token: 0x06003449 RID: 13385 RVA: 0x0043F0D4 File Offset: 0x0043F0D4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Row");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600344A RID: 13386 RVA: 0x0043F120 File Offset: 0x0043F120
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("PlaceIDs = ");
        builder.Append(this.PlaceIDs);
        builder.Append(", ItemFractions = ");
        builder.Append(this.ItemFractions);
        builder.Append(", CollapsedAbove = ");
        builder.Append(this.CollapsedAbove);
        builder.Append(", CollapsedBelow = ");
        builder.Append(this.CollapsedBelow);
        return true;
      }

      // Token: 0x0600344B RID: 13387 RVA: 0x0043F1A8 File Offset: 0x0043F1A8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetRowInfo.Row left, ClickyInkSheetRowInfo.Row right)
      {
        return !(left == right);
      }

      // Token: 0x0600344C RID: 13388 RVA: 0x0043F1B4 File Offset: 0x0043F1B4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetRowInfo.Row left, ClickyInkSheetRowInfo.Row right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600344D RID: 13389 RVA: 0x0043F1C8 File Offset: 0x0043F1C8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetRowInfo.Row);
      }

      // Token: 0x0600344E RID: 13390 RVA: 0x0043F1D8 File Offset: 0x0043F1D8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkSheetRowInfo other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003450 RID: 13392 RVA: 0x0043F1EC File Offset: 0x0043F1EC
      [CompilerGenerated]
      private Row(ClickyInkSheetRowInfo.Row original)
        : base(original)
      {
        this.PlaceIDs = original.<PlaceIDs>k__BackingField;
        this.ItemFractions = original.<ItemFractions>k__BackingField;
        this.CollapsedAbove = original.<CollapsedAbove>k__BackingField;
        this.CollapsedBelow = original.<CollapsedBelow>k__BackingField;
      }

      // Token: 0x06003451 RID: 13393 RVA: 0x0043F228 File Offset: 0x0043F228
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<string> PlaceIDs, out IReadOnlyDictionary<string, double> ItemFractions, out IReadOnlyList<string> CollapsedAbove, out IReadOnlyList<string> CollapsedBelow)
      {
        PlaceIDs = this.PlaceIDs;
        ItemFractions = this.ItemFractions;
        CollapsedAbove = this.CollapsedAbove;
        CollapsedBelow = this.CollapsedBelow;
      }
    }

    // Token: 0x02000559 RID: 1369
    [Nullable(0)]
    public sealed class Tail : ClickyInkSheetRowInfo, IEquatable<ClickyInkSheetRowInfo.Tail>
    {
      // Token: 0x1700094B RID: 2379
      // (get) Token: 0x06003452 RID: 13394 RVA: 0x0043F24C File Offset: 0x0043F24C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetRowInfo.Tail);
        }
      }

      // Token: 0x06003453 RID: 13395 RVA: 0x0043F258 File Offset: 0x0043F258
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Tail");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003454 RID: 13396 RVA: 0x0043F2A4 File Offset: 0x0043F2A4
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003455 RID: 13397 RVA: 0x0043F2B0 File Offset: 0x0043F2B0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetRowInfo.Tail left, ClickyInkSheetRowInfo.Tail right)
      {
        return !(left == right);
      }

      // Token: 0x06003456 RID: 13398 RVA: 0x0043F2BC File Offset: 0x0043F2BC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetRowInfo.Tail left, ClickyInkSheetRowInfo.Tail right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003457 RID: 13399 RVA: 0x0043F2D0 File Offset: 0x0043F2D0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003458 RID: 13400 RVA: 0x0043F2D8 File Offset: 0x0043F2D8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetRowInfo.Tail);
      }

      // Token: 0x06003459 RID: 13401 RVA: 0x0043F2E8 File Offset: 0x0043F2E8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkSheetRowInfo other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600345A RID: 13402 RVA: 0x0043F2F4 File Offset: 0x0043F2F4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetRowInfo.Tail other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x0600345C RID: 13404 RVA: 0x0043F30C File Offset: 0x0043F30C
      [CompilerGenerated]
      private Tail(ClickyInkSheetRowInfo.Tail original)
        : base(original)
      {
      }

      // Token: 0x0600345D RID: 13405 RVA: 0x0043F318 File Offset: 0x0043F318
      public Tail()
      {
      }
    }

    // Token: 0x0200055A RID: 1370
    [Nullable(0)]
    public sealed class PenMarker : ClickyInkSheetRowInfo, IEquatable<ClickyInkSheetRowInfo.PenMarker>
    {
      // Token: 0x1700094C RID: 2380
      // (get) Token: 0x0600345E RID: 13406 RVA: 0x0043F320 File Offset: 0x0043F320
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetRowInfo.PenMarker);
        }
      }

      // Token: 0x0600345F RID: 13407 RVA: 0x0043F32C File Offset: 0x0043F32C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("PenMarker");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003460 RID: 13408 RVA: 0x0043F378 File Offset: 0x0043F378
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003461 RID: 13409 RVA: 0x0043F384 File Offset: 0x0043F384
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetRowInfo.PenMarker left, ClickyInkSheetRowInfo.PenMarker right)
      {
        return !(left == right);
      }

      // Token: 0x06003462 RID: 13410 RVA: 0x0043F390 File Offset: 0x0043F390
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetRowInfo.PenMarker left, ClickyInkSheetRowInfo.PenMarker right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003463 RID: 13411 RVA: 0x0043F3A4 File Offset: 0x0043F3A4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003464 RID: 13412 RVA: 0x0043F3AC File Offset: 0x0043F3AC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetRowInfo.PenMarker);
      }

      // Token: 0x06003465 RID: 13413 RVA: 0x0043F3BC File Offset: 0x0043F3BC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkSheetRowInfo other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003466 RID: 13414 RVA: 0x0043F3C8 File Offset: 0x0043F3C8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetRowInfo.PenMarker other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06003468 RID: 13416 RVA: 0x0043F3E0 File Offset: 0x0043F3E0
      [CompilerGenerated]
      private PenMarker(ClickyInkSheetRowInfo.PenMarker original)
        : base(original)
      {
      }

      // Token: 0x06003469 RID: 13417 RVA: 0x0043F3EC File Offset: 0x0043F3EC
      public PenMarker()
      {
      }
    }

    // Token: 0x0200055B RID: 1371
    [Nullable(0)]
    public sealed class FollowMarker : ClickyInkSheetRowInfo, IEquatable<ClickyInkSheetRowInfo.FollowMarker>
    {
      // Token: 0x1700094D RID: 2381
      // (get) Token: 0x0600346A RID: 13418 RVA: 0x0043F3F4 File Offset: 0x0043F3F4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetRowInfo.FollowMarker);
        }
      }

      // Token: 0x0600346B RID: 13419 RVA: 0x0043F400 File Offset: 0x0043F400
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("FollowMarker");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600346C RID: 13420 RVA: 0x0043F44C File Offset: 0x0043F44C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x0600346D RID: 13421 RVA: 0x0043F458 File Offset: 0x0043F458
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetRowInfo.FollowMarker left, ClickyInkSheetRowInfo.FollowMarker right)
      {
        return !(left == right);
      }

      // Token: 0x0600346E RID: 13422 RVA: 0x0043F464 File Offset: 0x0043F464
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetRowInfo.FollowMarker left, ClickyInkSheetRowInfo.FollowMarker right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600346F RID: 13423 RVA: 0x0043F478 File Offset: 0x0043F478
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003470 RID: 13424 RVA: 0x0043F480 File Offset: 0x0043F480
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetRowInfo.FollowMarker);
      }

      // Token: 0x06003471 RID: 13425 RVA: 0x0043F490 File Offset: 0x0043F490
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkSheetRowInfo other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003472 RID: 13426 RVA: 0x0043F49C File Offset: 0x0043F49C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetRowInfo.FollowMarker other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06003474 RID: 13428 RVA: 0x0043F4B4 File Offset: 0x0043F4B4
      [CompilerGenerated]
      private FollowMarker(ClickyInkSheetRowInfo.FollowMarker original)
        : base(original)
      {
      }

      // Token: 0x06003475 RID: 13429 RVA: 0x0043F4C0 File Offset: 0x0043F4C0
      public FollowMarker()
      {
      }
    }
  }
}
