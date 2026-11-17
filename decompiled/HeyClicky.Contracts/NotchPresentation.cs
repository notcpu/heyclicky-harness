using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x0200002C RID: 44
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class NotchPresentation : IEquatable<NotchPresentation>
  {
    // Token: 0x17000090 RID: 144
    // (get) Token: 0x0600022C RID: 556 RVA: 0x00020314 File Offset: 0x00020314
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(NotchPresentation);
      }
    }

    // Token: 0x0600022D RID: 557 RVA: 0x00020320 File Offset: 0x00020320
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchPresentation");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600022E RID: 558 RVA: 0x0002036C File Offset: 0x0002036C
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x0600022F RID: 559 RVA: 0x00020370 File Offset: 0x00020370
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(NotchPresentation left, NotchPresentation right)
    {
      return !(left == right);
    }

    // Token: 0x06000230 RID: 560 RVA: 0x0002037C File Offset: 0x0002037C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(NotchPresentation left, NotchPresentation right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000231 RID: 561 RVA: 0x00020390 File Offset: 0x00020390
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x06000232 RID: 562 RVA: 0x000203A4 File Offset: 0x000203A4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchPresentation);
    }

    // Token: 0x06000233 RID: 563 RVA: 0x000203B4 File Offset: 0x000203B4
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(NotchPresentation other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x06000235 RID: 565 RVA: 0x000203D4 File Offset: 0x000203D4
    [CompilerGenerated]
    protected NotchPresentation(NotchPresentation original)
    {
    }

    // Token: 0x06000236 RID: 566 RVA: 0x000203DC File Offset: 0x000203DC
    protected NotchPresentation()
    {
    }

    // Token: 0x02000055 RID: 85
    [Nullable(0)]
    public sealed class Idle : NotchPresentation, IEquatable<NotchPresentation.Idle>
    {
      // Token: 0x170000F3 RID: 243
      // (get) Token: 0x0600048D RID: 1165 RVA: 0x00024FD4 File Offset: 0x00024FD4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchPresentation.Idle);
        }
      }

      // Token: 0x0600048E RID: 1166 RVA: 0x00024FE0 File Offset: 0x00024FE0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Idle");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600048F RID: 1167 RVA: 0x0002502C File Offset: 0x0002502C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06000490 RID: 1168 RVA: 0x00025038 File Offset: 0x00025038
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchPresentation.Idle left, NotchPresentation.Idle right)
      {
        return !(left == right);
      }

      // Token: 0x06000491 RID: 1169 RVA: 0x00025044 File Offset: 0x00025044
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchPresentation.Idle left, NotchPresentation.Idle right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000492 RID: 1170 RVA: 0x00025058 File Offset: 0x00025058
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06000493 RID: 1171 RVA: 0x00025060 File Offset: 0x00025060
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchPresentation.Idle);
      }

      // Token: 0x06000494 RID: 1172 RVA: 0x00025070 File Offset: 0x00025070
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchPresentation other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000495 RID: 1173 RVA: 0x0002507C File Offset: 0x0002507C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchPresentation.Idle other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06000497 RID: 1175 RVA: 0x00025094 File Offset: 0x00025094
      [CompilerGenerated]
      private Idle(NotchPresentation.Idle original)
        : base(original)
      {
      }

      // Token: 0x06000498 RID: 1176 RVA: 0x000250A0 File Offset: 0x000250A0
      public Idle()
      {
      }
    }

    // Token: 0x02000056 RID: 86
    [Nullable(0)]
    public sealed class LiveEvent : NotchPresentation, IEquatable<NotchPresentation.LiveEvent>
    {
      // Token: 0x06000499 RID: 1177 RVA: 0x000250A8 File Offset: 0x000250A8
      public LiveEvent(NotchLiveEvent Event)
      {
        this.Event = Event;
        base..ctor();
      }

      // Token: 0x170000F4 RID: 244
      // (get) Token: 0x0600049A RID: 1178 RVA: 0x000250B8 File Offset: 0x000250B8
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchPresentation.LiveEvent);
        }
      }

      // Token: 0x170000F5 RID: 245
      // (get) Token: 0x0600049B RID: 1179 RVA: 0x000250C4 File Offset: 0x000250C4
      // (set) Token: 0x0600049C RID: 1180 RVA: 0x000250CC File Offset: 0x000250CC
      public NotchLiveEvent Event { get; set; }

      // Token: 0x0600049D RID: 1181 RVA: 0x000250D8 File Offset: 0x000250D8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("LiveEvent");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600049E RID: 1182 RVA: 0x00025124 File Offset: 0x00025124
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Event = ");
        builder.Append(this.Event);
        return true;
      }

      // Token: 0x0600049F RID: 1183 RVA: 0x00025158 File Offset: 0x00025158
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchPresentation.LiveEvent left, NotchPresentation.LiveEvent right)
      {
        return !(left == right);
      }

      // Token: 0x060004A0 RID: 1184 RVA: 0x00025164 File Offset: 0x00025164
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchPresentation.LiveEvent left, NotchPresentation.LiveEvent right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060004A1 RID: 1185 RVA: 0x00025178 File Offset: 0x00025178
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<NotchLiveEvent>.Default.GetHashCode(this.<Event>k__BackingField);
      }

      // Token: 0x060004A2 RID: 1186 RVA: 0x00025198 File Offset: 0x00025198
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchPresentation.LiveEvent);
      }

      // Token: 0x060004A3 RID: 1187 RVA: 0x000251A8 File Offset: 0x000251A8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchPresentation other)
      {
        return this.Equals(other);
      }

      // Token: 0x060004A4 RID: 1188 RVA: 0x000251B4 File Offset: 0x000251B4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchPresentation.LiveEvent other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<NotchLiveEvent>.Default.Equals(this.<Event>k__BackingField, other.<Event>k__BackingField));
      }

      // Token: 0x060004A6 RID: 1190 RVA: 0x000251E8 File Offset: 0x000251E8
      [CompilerGenerated]
      private LiveEvent(NotchPresentation.LiveEvent original)
        : base(original)
      {
        this.Event = original.<Event>k__BackingField;
      }

      // Token: 0x060004A7 RID: 1191 RVA: 0x00025200 File Offset: 0x00025200
      [CompilerGenerated]
      public void Deconstruct(out NotchLiveEvent Event)
      {
        Event = this.Event;
      }
    }

    // Token: 0x02000057 RID: 87
    [Nullable(0)]
    public sealed class OnboardingPinned : NotchPresentation, IEquatable<NotchPresentation.OnboardingPinned>
    {
      // Token: 0x060004A8 RID: 1192 RVA: 0x0002520C File Offset: 0x0002520C
      public OnboardingPinned(int Step)
      {
        this.Step = Step;
        base..ctor();
      }

      // Token: 0x170000F6 RID: 246
      // (get) Token: 0x060004A9 RID: 1193 RVA: 0x0002521C File Offset: 0x0002521C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchPresentation.OnboardingPinned);
        }
      }

      // Token: 0x170000F7 RID: 247
      // (get) Token: 0x060004AA RID: 1194 RVA: 0x00025228 File Offset: 0x00025228
      // (set) Token: 0x060004AB RID: 1195 RVA: 0x00025230 File Offset: 0x00025230
      public int Step { get; set; }

      // Token: 0x060004AC RID: 1196 RVA: 0x0002523C File Offset: 0x0002523C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("OnboardingPinned");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060004AD RID: 1197 RVA: 0x00025288 File Offset: 0x00025288
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Step = ");
        builder.Append(this.Step.ToString());
        return true;
      }

      // Token: 0x060004AE RID: 1198 RVA: 0x000252D4 File Offset: 0x000252D4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchPresentation.OnboardingPinned left, NotchPresentation.OnboardingPinned right)
      {
        return !(left == right);
      }

      // Token: 0x060004AF RID: 1199 RVA: 0x000252E0 File Offset: 0x000252E0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchPresentation.OnboardingPinned left, NotchPresentation.OnboardingPinned right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060004B0 RID: 1200 RVA: 0x000252F4 File Offset: 0x000252F4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Step>k__BackingField);
      }

      // Token: 0x060004B1 RID: 1201 RVA: 0x00025314 File Offset: 0x00025314
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchPresentation.OnboardingPinned);
      }

      // Token: 0x060004B2 RID: 1202 RVA: 0x00025324 File Offset: 0x00025324
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchPresentation other)
      {
        return this.Equals(other);
      }

      // Token: 0x060004B3 RID: 1203 RVA: 0x00025330 File Offset: 0x00025330
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchPresentation.OnboardingPinned other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Step>k__BackingField, other.<Step>k__BackingField));
      }

      // Token: 0x060004B5 RID: 1205 RVA: 0x00025364 File Offset: 0x00025364
      [CompilerGenerated]
      private OnboardingPinned(NotchPresentation.OnboardingPinned original)
        : base(original)
      {
        this.Step = original.<Step>k__BackingField;
      }

      // Token: 0x060004B6 RID: 1206 RVA: 0x0002537C File Offset: 0x0002537C
      [CompilerGenerated]
      public void Deconstruct(out int Step)
      {
        Step = this.Step;
      }
    }

    // Token: 0x02000058 RID: 88
    [Nullable(0)]
    public sealed class TextInput : NotchPresentation, IEquatable<NotchPresentation.TextInput>
    {
      // Token: 0x060004B7 RID: 1207 RVA: 0x00025388 File Offset: 0x00025388
      public TextInput(string Draft, [Nullable(2)] NotchAgentFollowUp AgentFollowUp = null)
      {
        this.Draft = Draft;
        this.AgentFollowUp = AgentFollowUp;
        base..ctor();
      }

      // Token: 0x170000F8 RID: 248
      // (get) Token: 0x060004B8 RID: 1208 RVA: 0x000253A0 File Offset: 0x000253A0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchPresentation.TextInput);
        }
      }

      // Token: 0x170000F9 RID: 249
      // (get) Token: 0x060004B9 RID: 1209 RVA: 0x000253AC File Offset: 0x000253AC
      // (set) Token: 0x060004BA RID: 1210 RVA: 0x000253B4 File Offset: 0x000253B4
      public string Draft { get; set; }

      // Token: 0x170000FA RID: 250
      // (get) Token: 0x060004BB RID: 1211 RVA: 0x000253C0 File Offset: 0x000253C0
      // (set) Token: 0x060004BC RID: 1212 RVA: 0x000253C8 File Offset: 0x000253C8
      [Nullable(2)]
      public NotchAgentFollowUp AgentFollowUp
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x060004BD RID: 1213 RVA: 0x000253D4 File Offset: 0x000253D4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("TextInput");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060004BE RID: 1214 RVA: 0x00025420 File Offset: 0x00025420
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Draft = ");
        builder.Append(this.Draft);
        builder.Append(", AgentFollowUp = ");
        builder.Append(this.AgentFollowUp);
        return true;
      }

      // Token: 0x060004BF RID: 1215 RVA: 0x00025478 File Offset: 0x00025478
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchPresentation.TextInput left, NotchPresentation.TextInput right)
      {
        return !(left == right);
      }

      // Token: 0x060004C0 RID: 1216 RVA: 0x00025484 File Offset: 0x00025484
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchPresentation.TextInput left, NotchPresentation.TextInput right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060004C1 RID: 1217 RVA: 0x00025498 File Offset: 0x00025498
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Draft>k__BackingField)) * -1521134295 + EqualityComparer<NotchAgentFollowUp>.Default.GetHashCode(this.<AgentFollowUp>k__BackingField);
      }

      // Token: 0x060004C2 RID: 1218 RVA: 0x000254D0 File Offset: 0x000254D0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchPresentation.TextInput);
      }

      // Token: 0x060004C3 RID: 1219 RVA: 0x000254E0 File Offset: 0x000254E0
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchPresentation other)
      {
        return this.Equals(other);
      }

      // Token: 0x060004C4 RID: 1220 RVA: 0x000254EC File Offset: 0x000254EC
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchPresentation.TextInput other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Draft>k__BackingField, other.<Draft>k__BackingField) && EqualityComparer<NotchAgentFollowUp>.Default.Equals(this.<AgentFollowUp>k__BackingField, other.<AgentFollowUp>k__BackingField));
      }

      // Token: 0x060004C6 RID: 1222 RVA: 0x00025540 File Offset: 0x00025540
      [CompilerGenerated]
      private TextInput(NotchPresentation.TextInput original)
        : base(original)
      {
        this.Draft = original.<Draft>k__BackingField;
        this.AgentFollowUp = original.<AgentFollowUp>k__BackingField;
      }

      // Token: 0x060004C7 RID: 1223 RVA: 0x00025564 File Offset: 0x00025564
      [CompilerGenerated]
      public void Deconstruct(out string Draft, [Nullable(2)] out NotchAgentFollowUp AgentFollowUp)
      {
        Draft = this.Draft;
        AgentFollowUp = this.AgentFollowUp;
      }
    }

    // Token: 0x02000059 RID: 89
    [Nullable(0)]
    public sealed class TextResponse : NotchPresentation, IEquatable<NotchPresentation.TextResponse>
    {
      // Token: 0x060004C8 RID: 1224 RVA: 0x00025578 File Offset: 0x00025578
      public TextResponse(string Question, string Answer, bool IsThinking)
      {
        this.Question = Question;
        this.Answer = Answer;
        this.IsThinking = IsThinking;
        base..ctor();
      }

      // Token: 0x170000FB RID: 251
      // (get) Token: 0x060004C9 RID: 1225 RVA: 0x00025598 File Offset: 0x00025598
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchPresentation.TextResponse);
        }
      }

      // Token: 0x170000FC RID: 252
      // (get) Token: 0x060004CA RID: 1226 RVA: 0x000255A4 File Offset: 0x000255A4
      // (set) Token: 0x060004CB RID: 1227 RVA: 0x000255AC File Offset: 0x000255AC
      public string Question { get; set; }

      // Token: 0x170000FD RID: 253
      // (get) Token: 0x060004CC RID: 1228 RVA: 0x000255B8 File Offset: 0x000255B8
      // (set) Token: 0x060004CD RID: 1229 RVA: 0x000255C0 File Offset: 0x000255C0
      public string Answer { get; set; }

      // Token: 0x170000FE RID: 254
      // (get) Token: 0x060004CE RID: 1230 RVA: 0x000255CC File Offset: 0x000255CC
      // (set) Token: 0x060004CF RID: 1231 RVA: 0x000255D4 File Offset: 0x000255D4
      public bool IsThinking { get; set; }

      // Token: 0x060004D0 RID: 1232 RVA: 0x000255E0 File Offset: 0x000255E0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("TextResponse");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060004D1 RID: 1233 RVA: 0x0002562C File Offset: 0x0002562C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Question = ");
        builder.Append(this.Question);
        builder.Append(", Answer = ");
        builder.Append(this.Answer);
        builder.Append(", IsThinking = ");
        builder.Append(this.IsThinking.ToString());
        return true;
      }

      // Token: 0x060004D2 RID: 1234 RVA: 0x000256A8 File Offset: 0x000256A8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchPresentation.TextResponse left, NotchPresentation.TextResponse right)
      {
        return !(left == right);
      }

      // Token: 0x060004D3 RID: 1235 RVA: 0x000256B4 File Offset: 0x000256B4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchPresentation.TextResponse left, NotchPresentation.TextResponse right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060004D4 RID: 1236 RVA: 0x000256C8 File Offset: 0x000256C8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Question>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Answer>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsThinking>k__BackingField);
      }

      // Token: 0x060004D5 RID: 1237 RVA: 0x00025720 File Offset: 0x00025720
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchPresentation.TextResponse);
      }

      // Token: 0x060004D6 RID: 1238 RVA: 0x00025730 File Offset: 0x00025730
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchPresentation other)
      {
        return this.Equals(other);
      }

      // Token: 0x060004D7 RID: 1239 RVA: 0x0002573C File Offset: 0x0002573C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchPresentation.TextResponse other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Question>k__BackingField, other.<Question>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Answer>k__BackingField, other.<Answer>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsThinking>k__BackingField, other.<IsThinking>k__BackingField));
      }

      // Token: 0x060004D9 RID: 1241 RVA: 0x000257A8 File Offset: 0x000257A8
      [CompilerGenerated]
      private TextResponse(NotchPresentation.TextResponse original)
        : base(original)
      {
        this.Question = original.<Question>k__BackingField;
        this.Answer = original.<Answer>k__BackingField;
        this.IsThinking = original.<IsThinking>k__BackingField;
      }

      // Token: 0x060004DA RID: 1242 RVA: 0x000257D8 File Offset: 0x000257D8
      [CompilerGenerated]
      public void Deconstruct(out string Question, out string Answer, out bool IsThinking)
      {
        Question = this.Question;
        Answer = this.Answer;
        IsThinking = this.IsThinking;
      }
    }
  }
}
