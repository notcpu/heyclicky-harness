using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000310 RID: 784
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CardFooterRequest : IEquatable<CardFooterRequest>
  {
    // Token: 0x0600220B RID: 8715 RVA: 0x0061FC18 File Offset: 0x0061FC18
    public CardFooterRequest(CardFooterRequest.Kinds Kind, string Text = "", [Nullable(2)] CardReadyOutput Ready = null)
    {
      this.Kind = Kind;
      this.Text = Text;
      this.Ready = Ready;
      base..ctor();
    }

    // Token: 0x1700064B RID: 1611
    // (get) Token: 0x0600220C RID: 8716 RVA: 0x0061FC38 File Offset: 0x0061FC38
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CardFooterRequest);
      }
    }

    // Token: 0x1700064C RID: 1612
    // (get) Token: 0x0600220D RID: 8717 RVA: 0x0061FC44 File Offset: 0x0061FC44
    // (set) Token: 0x0600220E RID: 8718 RVA: 0x0061FC4C File Offset: 0x0061FC4C
    public CardFooterRequest.Kinds Kind { get; set; }

    // Token: 0x1700064D RID: 1613
    // (get) Token: 0x0600220F RID: 8719 RVA: 0x0061FC58 File Offset: 0x0061FC58
    // (set) Token: 0x06002210 RID: 8720 RVA: 0x0061FC60 File Offset: 0x0061FC60
    public string Text { get; set; }

    // Token: 0x1700064E RID: 1614
    // (get) Token: 0x06002211 RID: 8721 RVA: 0x0061FC6C File Offset: 0x0061FC6C
    // (set) Token: 0x06002212 RID: 8722 RVA: 0x0061FC74 File Offset: 0x0061FC74
    [Nullable(2)]
    public CardReadyOutput Ready
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700064F RID: 1615
    // (get) Token: 0x06002213 RID: 8723 RVA: 0x0061FC80 File Offset: 0x0061FC80
    // (set) Token: 0x06002214 RID: 8724 RVA: 0x0061FC88 File Offset: 0x0061FC88
    [TupleElementNames(new string[] { "BrowserName", "AddsAgain" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    public static Func<ValueTuple<string, bool>> ChromeSetup
    {
      [return: TupleElementNames(new string[] { "BrowserName", "AddsAgain" })]
      [return: Nullable(new byte[] { 1, 0, 1 })]
      get;
      [param: TupleElementNames(new string[] { "BrowserName", "AddsAgain" })]
      [param: Nullable(new byte[] { 1, 0, 1 })]
      set;
    } = () => new ValueTuple<string, bool>("Chrome", false);

    // Token: 0x06002215 RID: 8725 RVA: 0x0061FC90 File Offset: 0x0061FC90
    public static string ConnectChromeTitle(string browserName, bool addsAgain)
    {
      if (!addsAgain)
      {
        return "Needs the " + browserName + " extension";
      }
      return "Upgrade the " + browserName + " extension";
    }

    // Token: 0x06002216 RID: 8726 RVA: 0x0061FCB8 File Offset: 0x0061FCB8
    public static string ConnectChromeDetail(string browserName, bool addsAgain)
    {
      if (!addsAgain)
      {
        return "This runs in your " + browserName + ". HeyClicky's extension goes in once, a couple of clicks.";
      }
      return "The new version updates itself, so this is the last time you'll do this. Takes a few seconds.";
    }

    // Token: 0x06002217 RID: 8727 RVA: 0x0061FCD4 File Offset: 0x0061FCD4
    [NullableContext(2)]
    public static CardFooterRequest FromTask(bool hasTask, string pendingComputerUse, string pendingComputerUseSummary, [Nullable(1)] string speakableRequest, bool requiresExtraUsage, bool needsChrome, bool isCompleted, string userStep, bool canRetryAfterFailure, bool canRetryAfterInterruption, string failureSummary)
    {
      if (!hasTask)
      {
        return null;
      }
      string text = ((failureSummary != null) ? failureSummary.Trim() : null) ?? "";
      if (pendingComputerUse != null)
      {
        return new CardFooterRequest(CardFooterRequest.Kinds.ComputerUse, pendingComputerUseSummary ?? speakableRequest, null);
      }
      if (requiresExtraUsage)
      {
        return new CardFooterRequest(CardFooterRequest.Kinds.ExtraUsage, "", null);
      }
      if (needsChrome)
      {
        return new CardFooterRequest(CardFooterRequest.Kinds.ConnectChrome, "", null);
      }
      if (isCompleted && userStep != null)
      {
        return new CardFooterRequest(CardFooterRequest.Kinds.UserStep, userStep, null);
      }
      if (canRetryAfterFailure)
      {
        return new CardFooterRequest(CardFooterRequest.Kinds.Failed, text, null);
      }
      if (canRetryAfterInterruption)
      {
        return new CardFooterRequest(CardFooterRequest.Kinds.Stopped, text, null);
      }
      return null;
    }

    // Token: 0x17000650 RID: 1616
    // (get) Token: 0x06002218 RID: 8728 RVA: 0x0061FD60 File Offset: 0x0061FD60
    public string Title
    {
      get
      {
        string text;
        switch (this.Kind)
        {
        case CardFooterRequest.Kinds.ComputerUse:
          text = "One step on your PC";
          break;
        case CardFooterRequest.Kinds.ExtraUsage:
          text = "Needs more agent messages";
          break;
        case CardFooterRequest.Kinds.UserStep:
          text = "Your turn";
          break;
        case CardFooterRequest.Kinds.ConnectChrome:
          text = CardFooterRequest.ConnectChromeTitle(CardFooterRequest.ChromeSetup().Item1, CardFooterRequest.ChromeSetup().Item2);
          break;
        case CardFooterRequest.Kinds.Failed:
          text = "Didn't finish";
          break;
        case CardFooterRequest.Kinds.Stopped:
          text = "Interrupted";
          break;
        default:
        {
          CardReadyOutput ready = this.Ready;
          text = ((ready != null) ? ready.Title : null) ?? "";
          break;
        }
        }
        return text;
      }
    }

    // Token: 0x17000651 RID: 1617
    // (get) Token: 0x06002219 RID: 8729 RVA: 0x0061FE00 File Offset: 0x0061FE00
    public string Detail
    {
      get
      {
        string text2;
        switch (this.Kind)
        {
        case CardFooterRequest.Kinds.ComputerUse:
        {
          string text = this.Text.Trim();
          text2 = ((text != null && text.Length > 0) ? text : "HeyClicky can do this step on your screen while you watch.");
          break;
        }
        case CardFooterRequest.Kinds.ExtraUsage:
          text2 = "This one's big. Let it keep going?";
          break;
        case CardFooterRequest.Kinds.UserStep:
          text2 = this.Text;
          break;
        case CardFooterRequest.Kinds.ConnectChrome:
          text2 = CardFooterRequest.ConnectChromeDetail(CardFooterRequest.ChromeSetup().Item1, CardFooterRequest.ChromeSetup().Item2);
          break;
        case CardFooterRequest.Kinds.Failed:
          text2 = CardFooterRequest.FirstLine(this.Text) ?? "Something went wrong partway through.";
          break;
        case CardFooterRequest.Kinds.Stopped:
          text2 = ((this.Text.Trim() == "Stopped by you.") ? "You stopped it before it finished." : (CardFooterRequest.FirstLine(this.Text) ?? "It was cut off before it finished."));
          break;
        default:
        {
          CardReadyOutput ready = this.Ready;
          text2 = ((ready != null) ? ready.Detail : null) ?? "";
          break;
        }
        }
        return text2;
      }
    }

    // Token: 0x17000652 RID: 1618
    // (get) Token: 0x0600221A RID: 8730 RVA: 0x0061FF00 File Offset: 0x0061FF00
    public HandoffGlyph? Glyph
    {
      get
      {
        switch (this.Kind)
        {
        case CardFooterRequest.Kinds.ComputerUse:
          return new HandoffGlyph?(HandoffGlyph.MacStep);
        case CardFooterRequest.Kinds.UserStep:
          return new HandoffGlyph?(HandoffGlyph.YourTurn);
        case CardFooterRequest.Kinds.ConnectChrome:
          return new HandoffGlyph?(HandoffGlyph.ConnectChrome);
        }
        return null;
      }
    }

    // Token: 0x17000653 RID: 1619
    // (get) Token: 0x0600221B RID: 8731 RVA: 0x0061FF54 File Offset: 0x0061FF54
    public string Symbol
    {
      get
      {
        string text;
        switch (this.Kind)
        {
        case CardFooterRequest.Kinds.ComputerUse:
          text = "cursorarrow.click.2";
          break;
        case CardFooterRequest.Kinds.ExtraUsage:
          text = "bolt.fill";
          break;
        case CardFooterRequest.Kinds.UserStep:
          text = "hand.point.up.left.fill";
          break;
        case CardFooterRequest.Kinds.ConnectChrome:
          text = "globe";
          break;
        case CardFooterRequest.Kinds.Failed:
          text = "exclamationmark.triangle.fill";
          break;
        case CardFooterRequest.Kinds.Stopped:
          text = "stop.fill";
          break;
        default:
        {
          CardReadyOutput ready = this.Ready;
          text = ((ready != null) ? ready.Symbol : null) ?? "checkmark";
          break;
        }
        }
        return text;
      }
    }

    // Token: 0x17000654 RID: 1620
    // (get) Token: 0x0600221C RID: 8732 RVA: 0x0061FFD8 File Offset: 0x0061FFD8
    [TupleElementNames(new string[] { "Rgb", "Opacity" })]
    [Nullable(0)]
    public ValueTuple<uint, double> IconBackground
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "Rgb", "Opacity" })]
      get
      {
        switch (this.Kind)
        {
        case CardFooterRequest.Kinds.UserStep:
          return new ValueTuple<uint, double>(15241996U, 1.0);
        case CardFooterRequest.Kinds.ConnectChrome:
          return new ValueTuple<uint, double>(3375359U, 1.0);
        case CardFooterRequest.Kinds.Stopped:
          return new ValueTuple<uint, double>(1842724U, 0.45);
        case CardFooterRequest.Kinds.Ready:
        {
          CardReadyOutput ready = this.Ready;
          return new ValueTuple<uint, double>((ready != null) ? ready.IconBackgroundRgb : 2269291U, 1.0);
        }
        }
        return new ValueTuple<uint, double>(14242877U, 1.0);
      }
    }

    // Token: 0x0600221D RID: 8733 RVA: 0x00620088 File Offset: 0x00620088
    [return: Nullable(2)]
    private static string FirstLine(string text)
    {
      string text2 = text.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.FBB68C637180E81FFB4AF91596B53064C159DDF8F56E330BDD31C0DE6472348D2).FieldHandle)).FirstOrDefault<string>((string l) => l.Length > 0);
      string text3 = ((text2 != null) ? text2.Trim() : null) ?? "";
      if (text3.Length != 0)
      {
        return text3;
      }
      return null;
    }

    // Token: 0x0600221E RID: 8734 RVA: 0x006200EC File Offset: 0x006200EC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CardFooterRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600221F RID: 8735 RVA: 0x00620138 File Offset: 0x00620138
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Text = ");
      builder.Append(this.Text);
      builder.Append(", Ready = ");
      builder.Append(this.Ready);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Detail = ");
      builder.Append(this.Detail);
      builder.Append(", Glyph = ");
      builder.Append(this.Glyph.ToString());
      builder.Append(", Symbol = ");
      builder.Append(this.Symbol);
      builder.Append(", IconBackground = ");
      builder.Append(this.IconBackground.ToString());
      return true;
    }

    // Token: 0x06002220 RID: 8736 RVA: 0x00620240 File Offset: 0x00620240
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CardFooterRequest left, CardFooterRequest right)
    {
      return !(left == right);
    }

    // Token: 0x06002221 RID: 8737 RVA: 0x0062024C File Offset: 0x0062024C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CardFooterRequest left, CardFooterRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06002222 RID: 8738 RVA: 0x00620260 File Offset: 0x00620260
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<CardFooterRequest.Kinds>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<CardReadyOutput>.Default.GetHashCode(this.<Ready>k__BackingField);
    }

    // Token: 0x06002223 RID: 8739 RVA: 0x006202C4 File Offset: 0x006202C4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CardFooterRequest);
    }

    // Token: 0x06002224 RID: 8740 RVA: 0x006202D4 File Offset: 0x006202D4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CardFooterRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<CardFooterRequest.Kinds>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<CardReadyOutput>.Default.Equals(this.<Ready>k__BackingField, other.<Ready>k__BackingField));
    }

    // Token: 0x06002226 RID: 8742 RVA: 0x00620350 File Offset: 0x00620350
    [CompilerGenerated]
    private CardFooterRequest(CardFooterRequest original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Text = original.<Text>k__BackingField;
      this.Ready = original.<Ready>k__BackingField;
    }

    // Token: 0x06002227 RID: 8743 RVA: 0x0062037C File Offset: 0x0062037C
    [CompilerGenerated]
    public void Deconstruct(out CardFooterRequest.Kinds Kind, out string Text, [Nullable(2)] out CardReadyOutput Ready)
    {
      Kind = this.Kind;
      Text = this.Text;
      Ready = this.Ready;
    }

    // Token: 0x02000958 RID: 2392
    [NullableContext(0)]
    public enum Kinds
    {
      // Token: 0x04002747 RID: 10055
      ComputerUse,
      // Token: 0x04002748 RID: 10056
      ExtraUsage,
      // Token: 0x04002749 RID: 10057
      UserStep,
      // Token: 0x0400274A RID: 10058
      ConnectChrome,
      // Token: 0x0400274B RID: 10059
      Failed,
      // Token: 0x0400274C RID: 10060
      Stopped,
      // Token: 0x0400274D RID: 10061
      Ready
    }
  }
}
