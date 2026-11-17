using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Drive
{
  // Token: 0x02000194 RID: 404
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class ClickyDriveBilling : IEquatable<ClickyDriveBilling>
  {
    // Token: 0x17000559 RID: 1369
    // (get) Token: 0x060017EB RID: 6123 RVA: 0x003C6A5C File Offset: 0x003C6A5C
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyDriveBilling);
      }
    }

    // Token: 0x060017EC RID: 6124 RVA: 0x003C6A68 File Offset: 0x003C6A68
    public static JsonObject RequestFields(ClickyDriveBilling billing, string driveId, IReadOnlyList<string> askingTurnTokens)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["drive_id"] = driveId;
      JsonObject jsonObject2 = jsonObject;
      if (!(billing is ClickyDriveBilling.User))
      {
        ClickyDriveBilling.Job job = billing as ClickyDriveBilling.Job;
        if (job == null)
        {
          if (billing is ClickyDriveBilling.Setup)
          {
            jsonObject2["drive_billing"] = "setup";
          }
        }
        else
        {
          jsonObject2["drive_billing"] = "job";
          string leaseId = job.LeaseId;
          if (leaseId != null)
          {
            jsonObject2["drive_job_lease_id"] = leaseId;
          }
        }
      }
      else
      {
        jsonObject2["drive_billing"] = "agent";
        if (askingTurnTokens.Count > 0)
        {
          jsonObject2["asking_turn_tokens"] = new JsonArray(askingTurnTokens.Select<string, JsonNode>((string t) => JsonValue.Create(t, null)).ToArray<JsonNode>());
        }
      }
      return jsonObject2;
    }

    // Token: 0x060017ED RID: 6125 RVA: 0x003C6B5C File Offset: 0x003C6B5C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyDriveBilling");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060017EE RID: 6126 RVA: 0x003C6BA8 File Offset: 0x003C6BA8
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x060017EF RID: 6127 RVA: 0x003C6BAC File Offset: 0x003C6BAC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyDriveBilling left, ClickyDriveBilling right)
    {
      return !(left == right);
    }

    // Token: 0x060017F0 RID: 6128 RVA: 0x003C6BB8 File Offset: 0x003C6BB8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyDriveBilling left, ClickyDriveBilling right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060017F1 RID: 6129 RVA: 0x003C6BCC File Offset: 0x003C6BCC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x060017F2 RID: 6130 RVA: 0x003C6BE0 File Offset: 0x003C6BE0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyDriveBilling);
    }

    // Token: 0x060017F3 RID: 6131 RVA: 0x003C6BF0 File Offset: 0x003C6BF0
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(ClickyDriveBilling other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x060017F5 RID: 6133 RVA: 0x003C6C10 File Offset: 0x003C6C10
    [CompilerGenerated]
    protected ClickyDriveBilling(ClickyDriveBilling original)
    {
    }

    // Token: 0x060017F6 RID: 6134 RVA: 0x003C6C18 File Offset: 0x003C6C18
    protected ClickyDriveBilling()
    {
    }

    // Token: 0x04000A4A RID: 2634
    public static readonly ClickyDriveBilling UserDrive = new ClickyDriveBilling.User();

    // Token: 0x04000A4B RID: 2635
    public static readonly ClickyDriveBilling SetupDrive = new ClickyDriveBilling.Setup();

    // Token: 0x0200057B RID: 1403
    [Nullable(0)]
    public sealed class User : ClickyDriveBilling, IEquatable<ClickyDriveBilling.User>
    {
      // Token: 0x17000963 RID: 2403
      // (get) Token: 0x06003535 RID: 13621 RVA: 0x004414C4 File Offset: 0x004414C4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyDriveBilling.User);
        }
      }

      // Token: 0x06003536 RID: 13622 RVA: 0x004414D0 File Offset: 0x004414D0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("User");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003537 RID: 13623 RVA: 0x0044151C File Offset: 0x0044151C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003538 RID: 13624 RVA: 0x00441528 File Offset: 0x00441528
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyDriveBilling.User left, ClickyDriveBilling.User right)
      {
        return !(left == right);
      }

      // Token: 0x06003539 RID: 13625 RVA: 0x00441534 File Offset: 0x00441534
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyDriveBilling.User left, ClickyDriveBilling.User right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600353A RID: 13626 RVA: 0x00441548 File Offset: 0x00441548
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x0600353B RID: 13627 RVA: 0x00441550 File Offset: 0x00441550
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyDriveBilling.User);
      }

      // Token: 0x0600353C RID: 13628 RVA: 0x00441560 File Offset: 0x00441560
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyDriveBilling other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600353D RID: 13629 RVA: 0x0044156C File Offset: 0x0044156C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyDriveBilling.User other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x0600353F RID: 13631 RVA: 0x00441584 File Offset: 0x00441584
      [CompilerGenerated]
      private User(ClickyDriveBilling.User original)
        : base(original)
      {
      }

      // Token: 0x06003540 RID: 13632 RVA: 0x00441590 File Offset: 0x00441590
      public User()
      {
      }
    }

    // Token: 0x0200057C RID: 1404
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class Job : ClickyDriveBilling, IEquatable<ClickyDriveBilling.Job>
    {
      // Token: 0x06003541 RID: 13633 RVA: 0x00441598 File Offset: 0x00441598
      public Job(string LeaseId)
      {
        this.LeaseId = LeaseId;
        base..ctor();
      }

      // Token: 0x17000964 RID: 2404
      // (get) Token: 0x06003542 RID: 13634 RVA: 0x004415A8 File Offset: 0x004415A8
      [Nullable(1)]
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ClickyDriveBilling.Job);
        }
      }

      // Token: 0x17000965 RID: 2405
      // (get) Token: 0x06003543 RID: 13635 RVA: 0x004415B4 File Offset: 0x004415B4
      // (set) Token: 0x06003544 RID: 13636 RVA: 0x004415BC File Offset: 0x004415BC
      public string LeaseId { get; set; }

      // Token: 0x06003545 RID: 13637 RVA: 0x004415C8 File Offset: 0x004415C8
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Job");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003546 RID: 13638 RVA: 0x00441614 File Offset: 0x00441614
      [NullableContext(1)]
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("LeaseId = ");
        builder.Append(this.LeaseId);
        return true;
      }

      // Token: 0x06003547 RID: 13639 RVA: 0x00441648 File Offset: 0x00441648
      [CompilerGenerated]
      public static bool operator !=(ClickyDriveBilling.Job left, ClickyDriveBilling.Job right)
      {
        return !(left == right);
      }

      // Token: 0x06003548 RID: 13640 RVA: 0x00441654 File Offset: 0x00441654
      [CompilerGenerated]
      public static bool operator ==(ClickyDriveBilling.Job left, ClickyDriveBilling.Job right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003549 RID: 13641 RVA: 0x00441668 File Offset: 0x00441668
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LeaseId>k__BackingField);
      }

      // Token: 0x0600354A RID: 13642 RVA: 0x00441688 File Offset: 0x00441688
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyDriveBilling.Job);
      }

      // Token: 0x0600354B RID: 13643 RVA: 0x00441698 File Offset: 0x00441698
      [CompilerGenerated]
      public sealed override bool Equals(ClickyDriveBilling other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600354C RID: 13644 RVA: 0x004416A4 File Offset: 0x004416A4
      [CompilerGenerated]
      public bool Equals(ClickyDriveBilling.Job other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<LeaseId>k__BackingField, other.<LeaseId>k__BackingField));
      }

      // Token: 0x0600354E RID: 13646 RVA: 0x004416D8 File Offset: 0x004416D8
      [CompilerGenerated]
      private Job([Nullable(1)] ClickyDriveBilling.Job original)
        : base(original)
      {
        this.LeaseId = original.<LeaseId>k__BackingField;
      }

      // Token: 0x0600354F RID: 13647 RVA: 0x004416F0 File Offset: 0x004416F0
      [CompilerGenerated]
      public void Deconstruct(out string LeaseId)
      {
        LeaseId = this.LeaseId;
      }
    }

    // Token: 0x0200057D RID: 1405
    [Nullable(0)]
    public sealed class Setup : ClickyDriveBilling, IEquatable<ClickyDriveBilling.Setup>
    {
      // Token: 0x17000966 RID: 2406
      // (get) Token: 0x06003550 RID: 13648 RVA: 0x004416FC File Offset: 0x004416FC
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyDriveBilling.Setup);
        }
      }

      // Token: 0x06003551 RID: 13649 RVA: 0x00441708 File Offset: 0x00441708
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Setup");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003552 RID: 13650 RVA: 0x00441754 File Offset: 0x00441754
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003553 RID: 13651 RVA: 0x00441760 File Offset: 0x00441760
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyDriveBilling.Setup left, ClickyDriveBilling.Setup right)
      {
        return !(left == right);
      }

      // Token: 0x06003554 RID: 13652 RVA: 0x0044176C File Offset: 0x0044176C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyDriveBilling.Setup left, ClickyDriveBilling.Setup right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003555 RID: 13653 RVA: 0x00441780 File Offset: 0x00441780
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003556 RID: 13654 RVA: 0x00441788 File Offset: 0x00441788
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyDriveBilling.Setup);
      }

      // Token: 0x06003557 RID: 13655 RVA: 0x00441798 File Offset: 0x00441798
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyDriveBilling other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003558 RID: 13656 RVA: 0x004417A4 File Offset: 0x004417A4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyDriveBilling.Setup other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x0600355A RID: 13658 RVA: 0x004417BC File Offset: 0x004417BC
      [CompilerGenerated]
      private Setup(ClickyDriveBilling.Setup original)
        : base(original)
      {
      }

      // Token: 0x0600355B RID: 13659 RVA: 0x004417C8 File Offset: 0x004417C8
      public Setup()
      {
      }
    }
  }
}
