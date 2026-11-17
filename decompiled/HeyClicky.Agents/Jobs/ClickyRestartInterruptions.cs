using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000050 RID: 80
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyRestartInterruptions
  {
    // Token: 0x0600070D RID: 1805 RVA: 0x000F9D38 File Offset: 0x000F9D38
    public ClickyRestartInterruptions()
    {
      DateTimeOffset now = DateTimeOffset.Now;
      foreach (ClickyRestartInterruptions.Turn turn in (AgentDefaults.Get<List<ClickyRestartInterruptions.Turn>>("clicky.clickys.runningTurns.v1") ?? new List<ClickyRestartInterruptions.Turn>()))
      {
        ClickyRestartInterruptions.Turn turn2;
        if (turn.InterruptedAt != null)
        {
          turn2 = turn;
        }
        else
        {
          (turn2 = turn.<Clone>$()).InterruptedAt = new DateTimeOffset?(now);
        }
        ClickyRestartInterruptions.Turn turn3 = turn2;
        if (!(now - turn3.InterruptedAt.GetValueOrDefault(now) >= ClickyRestartInterruptions.KeptFor))
        {
          this.turnsBySlug[turn3.Slug] = turn3;
        }
      }
      this.Save();
    }

    // Token: 0x0600070E RID: 1806 RVA: 0x000F9E14 File Offset: 0x000F9E14
    public void NoteRunning([TupleElementNames(new string[] { "Slug", "ThreadId", "StartedAt" })] [Nullable(new byte[] { 1, 0, 1, 2 })] IReadOnlyList<ValueTuple<string, string, DateTimeOffset>> running)
    {
      object obj = this.gate;
      lock (obj)
      {
        Dictionary<string, ClickyRestartInterruptions.Turn> dictionary = this.turnsBySlug.Where<KeyValuePair<string, ClickyRestartInterruptions.Turn>>((KeyValuePair<string, ClickyRestartInterruptions.Turn> kv) => kv.Value.InterruptedAt != null).ToDictionary<KeyValuePair<string, ClickyRestartInterruptions.Turn>, string, ClickyRestartInterruptions.Turn>((KeyValuePair<string, ClickyRestartInterruptions.Turn> kv) => kv.Key, (KeyValuePair<string, ClickyRestartInterruptions.Turn> kv) => kv.Value);
        foreach (ValueTuple<string, string, DateTimeOffset> valueTuple in running)
        {
          ClickyRestartInterruptions.Turn turn;
          if (this.turnsBySlug.TryGetValue(valueTuple.Item1, out turn) && turn.InterruptedAt == null && turn.ThreadId == valueTuple.Item2)
          {
            dictionary[valueTuple.Item1] = turn;
          }
          else
          {
            dictionary[valueTuple.Item1] = new ClickyRestartInterruptions.Turn
            {
              Slug = valueTuple.Item1,
              ThreadId = valueTuple.Item2,
              StartedAt = valueTuple.Item3
            };
          }
        }
        if (dictionary.Count != this.turnsBySlug.Count || !dictionary.All<KeyValuePair<string, ClickyRestartInterruptions.Turn>>(delegate(KeyValuePair<string, ClickyRestartInterruptions.Turn> kv)
        {
          ClickyRestartInterruptions.Turn turn2;
          return this.turnsBySlug.TryGetValue(kv.Key, out turn2) && turn2 == kv.Value;
        }))
        {
          this.turnsBySlug = dictionary;
          this.Save();
        }
      }
    }

    // Token: 0x0600070F RID: 1807 RVA: 0x000F9FC8 File Offset: 0x000F9FC8
    [return: Nullable(2)]
    public ClickyRestartInterruptions.Turn Interruption(string slug)
    {
      object obj = this.gate;
      ClickyRestartInterruptions.Turn turn;
      lock (obj)
      {
        ClickyRestartInterruptions.Turn turn2;
        turn = ((this.turnsBySlug.TryGetValue(slug, out turn2) && turn2.InterruptedAt != null) ? turn2 : null);
      }
      return turn;
    }

    // Token: 0x06000710 RID: 1808 RVA: 0x000FA02C File Offset: 0x000FA02C
    public void Clear(string slug)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.turnsBySlug.Remove(slug))
        {
          this.Save();
        }
      }
    }

    // Token: 0x06000711 RID: 1809 RVA: 0x000FA07C File Offset: 0x000FA07C
    public IReadOnlyList<ClickyRestartInterruptions.Turn> TakeInterruptionsForChat()
    {
      object obj = this.gate;
      IReadOnlyList<ClickyRestartInterruptions.Turn> readOnlyList;
      lock (obj)
      {
        List<ClickyRestartInterruptions.Turn> list = this.turnsBySlug.Values.Where<ClickyRestartInterruptions.Turn>((ClickyRestartInterruptions.Turn t) => t.InterruptedAt != null && !t.IsInChat.GetValueOrDefault()).ToList<ClickyRestartInterruptions.Turn>();
        if (list.Count == 0)
        {
          readOnlyList = Array.Empty<ClickyRestartInterruptions.Turn>();
        }
        else
        {
          foreach (ClickyRestartInterruptions.Turn turn in list)
          {
            Dictionary<string, ClickyRestartInterruptions.Turn> dictionary = this.turnsBySlug;
            string slug = turn.Slug;
            ClickyRestartInterruptions.Turn turn2 = turn.<Clone>$();
            turn2.IsInChat = new bool?(true);
            dictionary[slug] = turn2;
          }
          this.Save();
          readOnlyList = list.OrderBy<ClickyRestartInterruptions.Turn, DateTimeOffset>((ClickyRestartInterruptions.Turn t) => t.StartedAt).ToList<ClickyRestartInterruptions.Turn>();
        }
      }
      return readOnlyList;
    }

    // Token: 0x06000712 RID: 1810 RVA: 0x000FA190 File Offset: 0x000FA190
    public void Reset()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.turnsBySlug = new Dictionary<string, ClickyRestartInterruptions.Turn>();
      }
      AgentDefaults.Remove("clicky.clickys.runningTurns.v1");
    }

    // Token: 0x06000713 RID: 1811 RVA: 0x000FA1E0 File Offset: 0x000FA1E0
    private void Save()
    {
      object obj = this.gate;
      lock (obj)
      {
        AgentDefaults.Set<List<ClickyRestartInterruptions.Turn>>("clicky.clickys.runningTurns.v1", this.turnsBySlug.Values.ToList<ClickyRestartInterruptions.Turn>());
      }
    }

    // Token: 0x04000290 RID: 656
    private const string DefaultsKey = "clicky.clickys.runningTurns.v1";

    // Token: 0x04000291 RID: 657
    private static readonly TimeSpan KeptFor = TimeSpan.FromDays(7);

    // Token: 0x04000292 RID: 658
    private readonly object gate = new object();

    // Token: 0x04000293 RID: 659
    private Dictionary<string, ClickyRestartInterruptions.Turn> turnsBySlug = new Dictionary<string, ClickyRestartInterruptions.Turn>();

    // Token: 0x0200018A RID: 394
    [Nullable(0)]
    [RequiredMember]
    public sealed class Turn : IEquatable<ClickyRestartInterruptions.Turn>
    {
      // Token: 0x170002CB RID: 715
      // (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00115D14 File Offset: 0x00115D14
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyRestartInterruptions.Turn);
        }
      }

      // Token: 0x170002CC RID: 716
      // (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00115D20 File Offset: 0x00115D20
      // (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00115D28 File Offset: 0x00115D28
      [RequiredMember]
      public string Slug { get; set; }

      // Token: 0x170002CD RID: 717
      // (get) Token: 0x06000DB3 RID: 3507 RVA: 0x00115D34 File Offset: 0x00115D34
      // (set) Token: 0x06000DB4 RID: 3508 RVA: 0x00115D3C File Offset: 0x00115D3C
      [Nullable(2)]
      public string ThreadId
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170002CE RID: 718
      // (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00115D48 File Offset: 0x00115D48
      // (set) Token: 0x06000DB6 RID: 3510 RVA: 0x00115D50 File Offset: 0x00115D50
      public DateTimeOffset StartedAt { get; set; }

      // Token: 0x170002CF RID: 719
      // (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00115D5C File Offset: 0x00115D5C
      // (set) Token: 0x06000DB8 RID: 3512 RVA: 0x00115D64 File Offset: 0x00115D64
      public DateTimeOffset? InterruptedAt { get; set; }

      // Token: 0x170002D0 RID: 720
      // (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00115D70 File Offset: 0x00115D70
      // (set) Token: 0x06000DBA RID: 3514 RVA: 0x00115D78 File Offset: 0x00115D78
      public bool? IsInChat { get; set; }

      // Token: 0x06000DBB RID: 3515 RVA: 0x00115D84 File Offset: 0x00115D84
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Turn");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000DBC RID: 3516 RVA: 0x00115DD0 File Offset: 0x00115DD0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Slug = ");
        builder.Append(this.Slug);
        builder.Append(", ThreadId = ");
        builder.Append(this.ThreadId);
        builder.Append(", StartedAt = ");
        builder.Append(this.StartedAt.ToString());
        builder.Append(", InterruptedAt = ");
        builder.Append(this.InterruptedAt.ToString());
        builder.Append(", IsInChat = ");
        builder.Append(this.IsInChat.ToString());
        return true;
      }

      // Token: 0x06000DBD RID: 3517 RVA: 0x00115E8C File Offset: 0x00115E8C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyRestartInterruptions.Turn left, ClickyRestartInterruptions.Turn right)
      {
        return !(left == right);
      }

      // Token: 0x06000DBE RID: 3518 RVA: 0x00115E98 File Offset: 0x00115E98
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyRestartInterruptions.Turn left, ClickyRestartInterruptions.Turn right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000DBF RID: 3519 RVA: 0x00115EAC File Offset: 0x00115EAC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<InterruptedAt>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<IsInChat>k__BackingField);
      }

      // Token: 0x06000DC0 RID: 3520 RVA: 0x00115F3C File Offset: 0x00115F3C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyRestartInterruptions.Turn);
      }

      // Token: 0x06000DC1 RID: 3521 RVA: 0x00115F4C File Offset: 0x00115F4C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyRestartInterruptions.Turn other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<InterruptedAt>k__BackingField, other.<InterruptedAt>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<IsInChat>k__BackingField, other.<IsInChat>k__BackingField));
      }

      // Token: 0x06000DC3 RID: 3523 RVA: 0x00115FFC File Offset: 0x00115FFC
      [CompilerGenerated]
      [SetsRequiredMembers]
      private Turn(ClickyRestartInterruptions.Turn original)
      {
        this.Slug = original.<Slug>k__BackingField;
        this.ThreadId = original.<ThreadId>k__BackingField;
        this.StartedAt = original.<StartedAt>k__BackingField;
        this.InterruptedAt = original.<InterruptedAt>k__BackingField;
        this.IsInChat = original.<IsInChat>k__BackingField;
      }

      // Token: 0x06000DC4 RID: 3524 RVA: 0x0011604C File Offset: 0x0011604C
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public Turn()
      {
      }
    }
  }
}
