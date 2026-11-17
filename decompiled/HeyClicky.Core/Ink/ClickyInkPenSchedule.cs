using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000162 RID: 354
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkPenSchedule : IEquatable<ClickyInkPenSchedule>
  {
    // Token: 0x06001580 RID: 5504 RVA: 0x003B892C File Offset: 0x003B892C
    public ClickyInkPenSchedule(IReadOnlyList<InkRange> Busy, IReadOnlyList<double> Arrivals)
    {
      this.Busy = Busy;
      this.Arrivals = Arrivals;
      base..ctor();
    }

    // Token: 0x170004F6 RID: 1270
    // (get) Token: 0x06001581 RID: 5505 RVA: 0x003B8944 File Offset: 0x003B8944
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkPenSchedule);
      }
    }

    // Token: 0x170004F7 RID: 1271
    // (get) Token: 0x06001582 RID: 5506 RVA: 0x003B8950 File Offset: 0x003B8950
    // (set) Token: 0x06001583 RID: 5507 RVA: 0x003B8958 File Offset: 0x003B8958
    public IReadOnlyList<InkRange> Busy { get; set; }

    // Token: 0x170004F8 RID: 1272
    // (get) Token: 0x06001584 RID: 5508 RVA: 0x003B8964 File Offset: 0x003B8964
    // (set) Token: 0x06001585 RID: 5509 RVA: 0x003B896C File Offset: 0x003B896C
    public IReadOnlyList<double> Arrivals { get; set; }

    // Token: 0x06001586 RID: 5510 RVA: 0x003B8978 File Offset: 0x003B8978
    public static ClickyInkPenSchedule Of(ClickyInkLiveNote note)
    {
      object gate = ClickyInkPenSchedule.Gate;
      ClickyInkPenSchedule clickyInkPenSchedule2;
      lock (gate)
      {
        ValueTuple<ClickyInkLiveNote, ClickyInkPenSchedule>? valueTuple = ClickyInkPenSchedule.last;
        if (valueTuple != null)
        {
          ValueTuple<ClickyInkLiveNote, ClickyInkPenSchedule> valueOrDefault = valueTuple.GetValueOrDefault();
          if (valueOrDefault.Item1 == note)
          {
            return valueOrDefault.Item2;
          }
        }
        ClickyInkPenSchedule clickyInkPenSchedule = ClickyInkPenSchedule.Keyed(note);
        ClickyInkPenSchedule.last = new ValueTuple<ClickyInkLiveNote, ClickyInkPenSchedule>?(new ValueTuple<ClickyInkLiveNote, ClickyInkPenSchedule>(note, clickyInkPenSchedule));
        clickyInkPenSchedule2 = clickyInkPenSchedule;
      }
      return clickyInkPenSchedule2;
    }

    // Token: 0x06001587 RID: 5511 RVA: 0x003B89FC File Offset: 0x003B89FC
    private static ClickyInkPenSchedule Keyed(ClickyInkLiveNote note)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", note.Pages.Select<ClickyInkLiveNote.Page, string>((ClickyInkLiveNote.Page p) => p.Id)));
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<int>(note.VersionCount);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(note.PenFreeAt));
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      ClickyInkPenSchedule clickyInkPenSchedule;
      if (ClickyInkPenSchedule.Cache.TryGetValue(text, out clickyInkPenSchedule))
      {
        return clickyInkPenSchedule;
      }
      List<InkRange> list = new List<InkRange>();
      foreach (ClickyInkLiveNote.PenFocus penFocus in from f in note.PenFocusList()
        orderby f.At
        select f)
      {
        if (list.Count > 0)
        {
          double at = penFocus.At;
          List<InkRange> list2 = list;
          if (at <= list2[list2.Count - 1].UpperBound + 0.6)
          {
            List<InkRange> list3 = list;
            int num = list3.Count - 1;
            List<InkRange> list4 = list;
            double lowerBound = list4[list4.Count - 1].LowerBound;
            List<InkRange> list5 = list;
            list3[num] = new InkRange(lowerBound, Math.Max(list5[list5.Count - 1].UpperBound, penFocus.Until));
            continue;
          }
        }
        list.Add(new InkRange(penFocus.At, Math.Max(penFocus.At, penFocus.Until)));
      }
      ClickyInkPenSchedule clickyInkPenSchedule2 = new ClickyInkPenSchedule(list, note.VersionArrivals.ToList<double>());
      if (ClickyInkPenSchedule.Cache.Count > 60)
      {
        ClickyInkPenSchedule.Cache.Clear();
      }
      ClickyInkPenSchedule.Cache[text] = clickyInkPenSchedule2;
      return clickyInkPenSchedule2;
    }

    // Token: 0x06001588 RID: 5512 RVA: 0x003B8BEC File Offset: 0x003B8BEC
    public bool IsWriting(double clock)
    {
      int num = this.LastStartedBy(clock);
      return num > 0 && clock <= this.Busy[num - 1].UpperBound;
    }

    // Token: 0x06001589 RID: 5513 RVA: 0x003B8C24 File Offset: 0x003B8C24
    private int LastStartedBy(double clock)
    {
      int i = 0;
      int num = this.Busy.Count;
      while (i < num)
      {
        int num2 = (i + num) / 2;
        if (this.Busy[num2].LowerBound <= clock)
        {
          i = num2 + 1;
        }
        else
        {
          num = num2;
        }
      }
      return i;
    }

    // Token: 0x0600158A RID: 5514 RVA: 0x003B8C6C File Offset: 0x003B8C6C
    public double? IdleSince(double clock)
    {
      int num = this.LastStartedBy(clock);
      if (num == 0)
      {
        return null;
      }
      double num2 = this.Busy[num - 1].UpperBound + 0.6;
      if (clock < num2)
      {
        return null;
      }
      return new double?(num2);
    }

    // Token: 0x0600158B RID: 5515 RVA: 0x003B8CC4 File Offset: 0x003B8CC4
    public bool LooksWorking(double clock)
    {
      double? num = null;
      foreach (double num2 in this.Arrivals)
      {
        if (num2 <= clock)
        {
          num = new double?(num2);
        }
      }
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        return clock < valueOrDefault + 120.0;
      }
      return false;
    }

    // Token: 0x0600158C RID: 5516 RVA: 0x003B8D40 File Offset: 0x003B8D40
    public double? RestingSince(double clock, bool? isWorking)
    {
      double? num = this.IdleSince(clock);
      if (num == null)
      {
        return null;
      }
      double valueOrDefault = num.GetValueOrDefault();
      if (!(isWorking ?? this.LooksWorking(clock)))
      {
        return null;
      }
      return new double?(valueOrDefault);
    }

    // Token: 0x0600158D RID: 5517 RVA: 0x003B8DA0 File Offset: 0x003B8DA0
    [NullableContext(2)]
    public bool Equals(ClickyInkPenSchedule other)
    {
      return other != null && this.Busy.SequenceEqual<InkRange>(other.Busy) && this.Arrivals.SequenceEqual<double>(other.Arrivals);
    }

    // Token: 0x0600158E RID: 5518 RVA: 0x003B8DCC File Offset: 0x003B8DCC
    public override int GetHashCode()
    {
      return HashCode.Combine<int, int>(this.Busy.Count, this.Arrivals.Count);
    }

    // Token: 0x0600158F RID: 5519 RVA: 0x003B8DEC File Offset: 0x003B8DEC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkPenSchedule");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001590 RID: 5520 RVA: 0x003B8E38 File Offset: 0x003B8E38
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Busy = ");
      builder.Append(this.Busy);
      builder.Append(", Arrivals = ");
      builder.Append(this.Arrivals);
      return true;
    }

    // Token: 0x06001591 RID: 5521 RVA: 0x003B8E74 File Offset: 0x003B8E74
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkPenSchedule left, ClickyInkPenSchedule right)
    {
      return !(left == right);
    }

    // Token: 0x06001592 RID: 5522 RVA: 0x003B8E80 File Offset: 0x003B8E80
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkPenSchedule left, ClickyInkPenSchedule right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001593 RID: 5523 RVA: 0x003B8E94 File Offset: 0x003B8E94
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkPenSchedule);
    }

    // Token: 0x06001595 RID: 5525 RVA: 0x003B8EAC File Offset: 0x003B8EAC
    [CompilerGenerated]
    private ClickyInkPenSchedule(ClickyInkPenSchedule original)
    {
      this.Busy = original.<Busy>k__BackingField;
      this.Arrivals = original.<Arrivals>k__BackingField;
    }

    // Token: 0x06001596 RID: 5526 RVA: 0x003B8ECC File Offset: 0x003B8ECC
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<InkRange> Busy, out IReadOnlyList<double> Arrivals)
    {
      Busy = this.Busy;
      Arrivals = this.Arrivals;
    }

    // Token: 0x04000963 RID: 2403
    private static readonly Dictionary<string, ClickyInkPenSchedule> Cache = new Dictionary<string, ClickyInkPenSchedule>();

    // Token: 0x04000964 RID: 2404
    [TupleElementNames(new string[] { "Note", "Schedule" })]
    [Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<ClickyInkLiveNote, ClickyInkPenSchedule>? last;

    // Token: 0x04000965 RID: 2405
    private static readonly object Gate = new object();
  }
}
