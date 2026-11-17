using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000166 RID: 358
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkSheetCamera
  {
    // Token: 0x060015B0 RID: 5552 RVA: 0x003B9B60 File Offset: 0x003B9B60
    public static List<ClickyInkLiveNote.PenFocus> FocusOf(ClickyInkLiveNote note)
    {
      object gate = ClickyInkSheetCamera.Gate;
      List<ClickyInkLiveNote.PenFocus> list2;
      lock (gate)
      {
        ValueTuple<ClickyInkLiveNote, List<ClickyInkLiveNote.PenFocus>>? valueTuple = ClickyInkSheetCamera.last;
        if (valueTuple != null)
        {
          ValueTuple<ClickyInkLiveNote, List<ClickyInkLiveNote.PenFocus>> valueOrDefault = valueTuple.GetValueOrDefault();
          if (valueOrDefault.Item1 == note)
          {
            return valueOrDefault.Item2;
          }
        }
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
        defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", note.Pages.Select<ClickyInkLiveNote.Page, string>((ClickyInkLiveNote.Page p) => p.Id)));
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<int>(note.VersionCount);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(note.PenFreeAt));
        defaultInterpolatedStringHandler.AppendLiteral("|");
        string text = defaultInterpolatedStringHandler.ToStringAndClear() + InkText.DoubleDescription(note.LastVersionAt.GetValueOrDefault());
        List<ClickyInkLiveNote.PenFocus> list;
        if (!ClickyInkSheetCamera.Cache.TryGetValue(text, out list))
        {
          list = note.PenFocusList();
          if (ClickyInkSheetCamera.Cache.Count > 60)
          {
            ClickyInkSheetCamera.Cache.Clear();
          }
          ClickyInkSheetCamera.Cache[text] = list;
        }
        ClickyInkSheetCamera.last = new ValueTuple<ClickyInkLiveNote, List<ClickyInkLiveNote.PenFocus>>?(new ValueTuple<ClickyInkLiveNote, List<ClickyInkLiveNote.PenFocus>>(note, list));
        list2 = list;
      }
      return list2;
    }

    // Token: 0x060015B1 RID: 5553 RVA: 0x003B9CD0 File Offset: 0x003B9CD0
    public static double Runway(double contentHeight, double viewportHeight, double full = 1.0)
    {
      double num = (contentHeight - 0.7 * viewportHeight) / (0.3 * viewportHeight);
      return 0.32 * viewportHeight * ClickyInkClock.EaseInOut(num) * full;
    }

    // Token: 0x060015B2 RID: 5554 RVA: 0x003B9D0C File Offset: 0x003B9D0C
    public static double? PenLine(IReadOnlyList<ClickyInkLiveNote.PenFocus> focus, IReadOnlyDictionary<string, ClickyInkSheetCamera.Place> places, double clock)
    {
      for (int i = focus.Count - 1; i >= 0; i--)
      {
        ClickyInkLiveNote.PenFocus penFocus = focus[i];
        ClickyInkSheetCamera.Place place;
        if (penFocus.At <= clock && places.TryGetValue(penFocus.PlaceID, out place))
        {
          return new double?(place.Top + place.Height * ClickyInkSheetCamera.Fraction(place, penFocus));
        }
      }
      return null;
    }

    // Token: 0x060015B3 RID: 5555 RVA: 0x003B9D74 File Offset: 0x003B9D74
    private static double Fraction(ClickyInkSheetCamera.Place place, ClickyInkLiveNote.PenFocus entry)
    {
      string itemID = entry.ItemID;
      double num;
      if (itemID == null || !place.ItemFractions.TryGetValue(itemID, out num))
      {
        return 1.0;
      }
      return num;
    }

    // Token: 0x060015B4 RID: 5556 RVA: 0x003B9DA8 File Offset: 0x003B9DA8
    public static double Offset(IReadOnlyList<ClickyInkLiveNote.PenFocus> focus, IReadOnlyDictionary<string, ClickyInkSheetCamera.Place> places, double clock, double viewportHeight, double maximumOffset)
    {
      ClickyInkSheetCamera.<>c__DisplayClass21_0 CS$<>8__locals1;
      CS$<>8__locals1.places = places;
      CS$<>8__locals1.viewportHeight = viewportHeight;
      CS$<>8__locals1.maximumOffset = maximumOffset;
      if (CS$<>8__locals1.maximumOffset <= 0.0)
      {
        return 0.0;
      }
      double num = 0.0;
      double num2 = 0.0;
      ValueTuple<double, double>? valueTuple = null;
      for (int i = 0; i < focus.Count; i++)
      {
        ClickyInkLiveNote.PenFocus penFocus = focus[i];
        if (penFocus.At - 0.7 > clock)
        {
          break;
        }
        double? num3 = ClickyInkSheetCamera.<Offset>g__Target|21_0(num, penFocus, ref CS$<>8__locals1);
        if (num3 != null)
        {
          double valueOrDefault = num3.GetValueOrDefault();
          num3 = ClickyInkSheetCamera.<Offset>g__Line|21_1(penFocus, ref CS$<>8__locals1);
          if (num3 != null)
          {
            double valueOrDefault2 = num3.GetValueOrDefault();
            double num4 = valueOrDefault;
            if (i + 1 < focus.Count && focus[i + 1].PlaceID != penFocus.PlaceID)
            {
              num3 = ClickyInkSheetCamera.<Offset>g__Target|21_0(num, focus[i + 1], ref CS$<>8__locals1);
              if (num3 != null)
              {
                double valueOrDefault3 = num3.GetValueOrDefault();
                double num5 = focus[i + 1].At - penFocus.At;
                double num6 = 1.0 - ClickyInkClock.Progress(num5, 0.3, 0.6);
                double num7 = (Math.Abs(valueOrDefault3 - num) - 0.15 * CS$<>8__locals1.viewportHeight) / (0.25 * CS$<>8__locals1.viewportHeight);
                double num8 = 1.0 - ClickyInkClock.EaseInOut(num7);
                num4 += (num - num4) * (num6 * num8);
              }
            }
            double num9;
            if (!penFocus.IsLanding && valueTuple != null)
            {
              ValueTuple<double, double> valueOrDefault4 = valueTuple.GetValueOrDefault();
              num9 = ClickyInkSheetCamera.Anticipation(valueOrDefault2, valueOrDefault4.Item1, num, num4, CS$<>8__locals1.viewportHeight, penFocus.At - valueOrDefault4.Item2);
            }
            else
            {
              num9 = 0.0;
            }
            double num10 = num9;
            double num11 = Math.Max(penFocus.At - num10, Math.Min(penFocus.At, penFocus.KnownSince));
            if (num11 > clock)
            {
              break;
            }
            double num12 = Math.Abs(num4 - num2);
            double num13 = Math.Min(1.6, Math.Max(0.8, 0.8 + num12 * 0.0008333333333333334));
            double num14 = ClickyInkClock.Progress(clock, num11, num11 + num13);
            num2 += (num4 - num2) * ClickyInkSheetCamera.SmootherStep(num14);
            num = num4;
            valueTuple = new ValueTuple<double, double>?(new ValueTuple<double, double>(valueOrDefault2, penFocus.At));
          }
        }
      }
      return Math.Min(CS$<>8__locals1.maximumOffset, Math.Max(0.0, num2));
    }

    // Token: 0x060015B5 RID: 5557 RVA: 0x003BA07C File Offset: 0x003BA07C
    public static double Anticipation(double newLine, double previousLine, double settled, double wanted, double viewportHeight, double room)
    {
      double num = wanted - settled;
      if (num <= 0.5 || viewportHeight <= 0.0 || room <= 0.0)
      {
        return 0.0;
      }
      double num2 = (newLine - settled) / viewportHeight;
      double num3 = ClickyInkClock.EaseInOut((num2 - 0.8) / 0.1499999999999999) * (1.0 - ClickyInkClock.EaseInOut((num2 - 1.5) / 0.25 + 1.0));
      if (num3 <= 0.0)
      {
        return 0.0;
      }
      double num4 = ((previousLine - settled) / viewportHeight - 0.12) * viewportHeight / num;
      if (num4 <= 0.0)
      {
        return 0.0;
      }
      double num5 = Math.Min(1.6, Math.Max(0.8, 0.8 + num * 0.0008333333333333334));
      double num6 = 0.7 * num3;
      if (num4 < 1.0)
      {
        num6 = Math.Min(num6, num5 * ClickyInkSheetCamera.InverseSmootherStep(num4));
      }
      return Math.Min(num6, room * 0.8);
    }

    // Token: 0x060015B6 RID: 5558 RVA: 0x003BA1C4 File Offset: 0x003BA1C4
    public static double InverseSmootherStep(double value)
    {
      double num = 0.0;
      double num2 = 1.0;
      for (int i = 0; i < 30; i++)
      {
        double num3 = (num + num2) / 2.0;
        if (ClickyInkSheetCamera.SmootherStep(num3) < value)
        {
          num = num3;
        }
        else
        {
          num2 = num3;
        }
      }
      return (num + num2) / 2.0;
    }

    // Token: 0x060015B7 RID: 5559 RVA: 0x003BA21C File Offset: 0x003BA21C
    public static double SmootherStep(double t)
    {
      double num = Math.Min(1.0, Math.Max(0.0, t));
      return num * num * num * (num * (6.0 * num - 15.0) + 10.0);
    }

    // Token: 0x060015B9 RID: 5561 RVA: 0x003BA288 File Offset: 0x003BA288
    [CompilerGenerated]
    internal static double? <Offset>g__Target|21_0(double settled, ClickyInkLiveNote.PenFocus entry, ref ClickyInkSheetCamera.<>c__DisplayClass21_0 A_2)
    {
      ClickyInkSheetCamera.Place place;
      if (!A_2.places.TryGetValue(entry.PlaceID, out place))
      {
        return null;
      }
      double num = place.Top + place.Height * ClickyInkSheetCamera.Fraction(place, entry) - 0.68 * A_2.viewportHeight;
      double num2 = place.Top - 0.1 * A_2.viewportHeight;
      double num3 = settled;
      if (num3 > num2)
      {
        num3 = num2;
      }
      if (num3 < num)
      {
        num3 = num;
      }
      return new double?(Math.Min(A_2.maximumOffset, Math.Max(0.0, num3)));
    }

    // Token: 0x060015BA RID: 5562 RVA: 0x003BA324 File Offset: 0x003BA324
    [CompilerGenerated]
    internal static double? <Offset>g__Line|21_1(ClickyInkLiveNote.PenFocus entry, ref ClickyInkSheetCamera.<>c__DisplayClass21_0 A_1)
    {
      ClickyInkSheetCamera.Place place;
      if (!A_1.places.TryGetValue(entry.PlaceID, out place))
      {
        return null;
      }
      return new double?(place.Top + place.Height * ClickyInkSheetCamera.Fraction(place, entry));
    }

    // Token: 0x04000974 RID: 2420
    public const double TopBand = 0.1;

    // Token: 0x04000975 RID: 2421
    public const double BottomBand = 0.68;

    // Token: 0x04000976 RID: 2422
    public const double RunwayFraction = 0.32;

    // Token: 0x04000977 RID: 2423
    public const double ShortestGlideSeconds = 0.8;

    // Token: 0x04000978 RID: 2424
    public const double LongestGlideSeconds = 1.6;

    // Token: 0x04000979 RID: 2425
    public const double GlideSecondsPerPoint = 0.0008333333333333334;

    // Token: 0x0400097A RID: 2426
    public const double BriefestVisitSeconds = 0.6;

    // Token: 0x0400097B RID: 2427
    public const double BriefVisitReturnFraction = 0.15;

    // Token: 0x0400097C RID: 2428
    public const double AnticipationSeconds = 0.7;

    // Token: 0x0400097D RID: 2429
    public const double AnticipateFromFraction = 0.8;

    // Token: 0x0400097E RID: 2430
    public const double AnticipateFullFraction = 0.95;

    // Token: 0x0400097F RID: 2431
    public const double AnticipateUntilFraction = 1.5;

    // Token: 0x04000980 RID: 2432
    public const double AnticipationKeepsLineBelowFraction = 0.12;

    // Token: 0x04000981 RID: 2433
    private static readonly Dictionary<string, List<ClickyInkLiveNote.PenFocus>> Cache = new Dictionary<string, List<ClickyInkLiveNote.PenFocus>>();

    // Token: 0x04000982 RID: 2434
    [TupleElementNames(new string[] { "Note", "Focus" })]
    [Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<ClickyInkLiveNote, List<ClickyInkLiveNote.PenFocus>>? last;

    // Token: 0x04000983 RID: 2435
    private static readonly object Gate = new object();

    // Token: 0x02000529 RID: 1321
    [Nullable(0)]
    public sealed class Place : IEquatable<ClickyInkSheetCamera.Place>
    {
      // Token: 0x060032F4 RID: 13044 RVA: 0x0043C99C File Offset: 0x0043C99C
      public Place(double Top, double Height, IReadOnlyDictionary<string, double> ItemFractions)
      {
        this.Top = Top;
        this.Height = Height;
        this.ItemFractions = ItemFractions;
        base..ctor();
      }

      // Token: 0x1700092A RID: 2346
      // (get) Token: 0x060032F5 RID: 13045 RVA: 0x0043C9BC File Offset: 0x0043C9BC
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetCamera.Place);
        }
      }

      // Token: 0x1700092B RID: 2347
      // (get) Token: 0x060032F6 RID: 13046 RVA: 0x0043C9C8 File Offset: 0x0043C9C8
      // (set) Token: 0x060032F7 RID: 13047 RVA: 0x0043C9D0 File Offset: 0x0043C9D0
      public double Top { get; set; }

      // Token: 0x1700092C RID: 2348
      // (get) Token: 0x060032F8 RID: 13048 RVA: 0x0043C9DC File Offset: 0x0043C9DC
      // (set) Token: 0x060032F9 RID: 13049 RVA: 0x0043C9E4 File Offset: 0x0043C9E4
      public double Height { get; set; }

      // Token: 0x1700092D RID: 2349
      // (get) Token: 0x060032FA RID: 13050 RVA: 0x0043C9F0 File Offset: 0x0043C9F0
      // (set) Token: 0x060032FB RID: 13051 RVA: 0x0043C9F8 File Offset: 0x0043C9F8
      public IReadOnlyDictionary<string, double> ItemFractions { get; set; }

      // Token: 0x060032FC RID: 13052 RVA: 0x0043CA04 File Offset: 0x0043CA04
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Place");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060032FD RID: 13053 RVA: 0x0043CA50 File Offset: 0x0043CA50
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Top = ");
        builder.Append(this.Top.ToString());
        builder.Append(", Height = ");
        builder.Append(this.Height.ToString());
        builder.Append(", ItemFractions = ");
        builder.Append(this.ItemFractions);
        return true;
      }

      // Token: 0x060032FE RID: 13054 RVA: 0x0043CACC File Offset: 0x0043CACC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetCamera.Place left, ClickyInkSheetCamera.Place right)
      {
        return !(left == right);
      }

      // Token: 0x060032FF RID: 13055 RVA: 0x0043CAD8 File Offset: 0x0043CAD8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetCamera.Place left, ClickyInkSheetCamera.Place right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003300 RID: 13056 RVA: 0x0043CAEC File Offset: 0x0043CAEC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Top>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Height>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, double>>.Default.GetHashCode(this.<ItemFractions>k__BackingField);
      }

      // Token: 0x06003301 RID: 13057 RVA: 0x0043CB50 File Offset: 0x0043CB50
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetCamera.Place);
      }

      // Token: 0x06003302 RID: 13058 RVA: 0x0043CB60 File Offset: 0x0043CB60
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetCamera.Place other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double>.Default.Equals(this.<Top>k__BackingField, other.<Top>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Height>k__BackingField, other.<Height>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, double>>.Default.Equals(this.<ItemFractions>k__BackingField, other.<ItemFractions>k__BackingField));
      }

      // Token: 0x06003304 RID: 13060 RVA: 0x0043CBDC File Offset: 0x0043CBDC
      [CompilerGenerated]
      private Place(ClickyInkSheetCamera.Place original)
      {
        this.Top = original.<Top>k__BackingField;
        this.Height = original.<Height>k__BackingField;
        this.ItemFractions = original.<ItemFractions>k__BackingField;
      }

      // Token: 0x06003305 RID: 13061 RVA: 0x0043CC08 File Offset: 0x0043CC08
      [CompilerGenerated]
      public void Deconstruct(out double Top, out double Height, out IReadOnlyDictionary<string, double> ItemFractions)
      {
        Top = this.Top;
        Height = this.Height;
        ItemFractions = this.ItemFractions;
      }
    }
  }
}
