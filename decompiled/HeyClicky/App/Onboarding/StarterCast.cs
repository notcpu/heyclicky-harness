using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Home;
using HeyClicky.App.Portraits;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000126 RID: 294
  [NullableContext(1)]
  [Nullable(0)]
  public static class StarterCast
  {
    // Token: 0x06001018 RID: 4120 RVA: 0x00581B24 File Offset: 0x00581B24
    public static bool IsCast(string slug)
    {
      return StarterCast.Members.Any<StarterCast.Member>((StarterCast.Member m) => m.Slug == slug);
    }

    // Token: 0x06001019 RID: 4121 RVA: 0x00581B54 File Offset: 0x00581B54
    public static IReadOnlyList<string> PresentSlugs()
    {
      return (from m in StarterCast.Members
        select m.Slug into s
        where ClickyRoster.Shared.Clicky(s) != null
        select s).ToList<string>();
    }

    // Token: 0x0600101A RID: 4122 RVA: 0x00581BB4 File Offset: 0x00581BB4
    public static void Outfit()
    {
      ClickyRoster.Shared.OutfitStarterCast(StarterCast.Members.Select<StarterCast.Member, string>((StarterCast.Member m) => m.Slug));
    }

    // Token: 0x0600101B RID: 4123 RVA: 0x00581BEC File Offset: 0x00581BEC
    public static void Unlock()
    {
      ClickyRoster shared = ClickyRoster.Shared;
      foreach (StarterCast.Member member in StarterCast.Members)
      {
        IHomeSource source = shared.Source;
        if ((source == null || !source.KnowsClicky(member.Slug)) && shared.Clicky(member.Slug) == null)
        {
          RosterClicky rosterClicky = shared.CreateClicky(member.Name, member.Role, member.OneLiner, Array.Empty<string>(), member.SuggestedAsks, null, null, null);
          string text = "onboarding";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
          defaultInterpolatedStringHandler.AppendLiteral("starter cast: ");
          defaultInterpolatedStringHandler.AppendFormatted(rosterClicky.Name);
          defaultInterpolatedStringHandler.AppendLiteral(" (");
          defaultInterpolatedStringHandler.AppendFormatted(rosterClicky.Slug);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
    }

    // Token: 0x040006F1 RID: 1777
    public static readonly StarterCast.Member ResearchBuddy = new StarterCast.Member("research-buddy", "Research Buddy", "Researcher", "Digs into companies, markets, and competitors and hands you a tidy report.", new <>z__ReadOnlyArray<string>(new string[] { "Research my top three competitors and what they charge", "Find out who is winning in my space and why", "Put together a one page brief on this company" }));

    // Token: 0x040006F2 RID: 1778
    public static readonly StarterCast.Member Shippy = new StarterCast.Member("shippy", "Shippy", "Web builder", "Builds and ships websites and small web projects right on your PC.", new <>z__ReadOnlyArray<string>(new string[] { "Build me a simple landing page for my idea", "Make a one page site with a waitlist form", "Ship a little web app that does one thing well" }));

    // Token: 0x040006F3 RID: 1779
    public static readonly IReadOnlyList<StarterCast.Member> Members = new <>z__ReadOnlyArray<StarterCast.Member>(new StarterCast.Member[]
    {
      StarterCast.ResearchBuddy,
      StarterCast.Shippy
    });

    // Token: 0x0200065F RID: 1631
    [Nullable(0)]
    public sealed class Member : IEquatable<StarterCast.Member>
    {
      // Token: 0x060031CE RID: 12750 RVA: 0x006701BC File Offset: 0x006701BC
      public Member(string Slug, string Name, string Role, string OneLiner, IReadOnlyList<string> SuggestedAsks)
      {
        this.Slug = Slug;
        this.Name = Name;
        this.Role = Role;
        this.OneLiner = OneLiner;
        this.SuggestedAsks = SuggestedAsks;
        base..ctor();
      }

      // Token: 0x17000788 RID: 1928
      // (get) Token: 0x060031CF RID: 12751 RVA: 0x006701EC File Offset: 0x006701EC
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(StarterCast.Member);
        }
      }

      // Token: 0x17000789 RID: 1929
      // (get) Token: 0x060031D0 RID: 12752 RVA: 0x006701F8 File Offset: 0x006701F8
      // (set) Token: 0x060031D1 RID: 12753 RVA: 0x00670200 File Offset: 0x00670200
      public string Slug { get; set; }

      // Token: 0x1700078A RID: 1930
      // (get) Token: 0x060031D2 RID: 12754 RVA: 0x0067020C File Offset: 0x0067020C
      // (set) Token: 0x060031D3 RID: 12755 RVA: 0x00670214 File Offset: 0x00670214
      public string Name { get; set; }

      // Token: 0x1700078B RID: 1931
      // (get) Token: 0x060031D4 RID: 12756 RVA: 0x00670220 File Offset: 0x00670220
      // (set) Token: 0x060031D5 RID: 12757 RVA: 0x00670228 File Offset: 0x00670228
      public string Role { get; set; }

      // Token: 0x1700078C RID: 1932
      // (get) Token: 0x060031D6 RID: 12758 RVA: 0x00670234 File Offset: 0x00670234
      // (set) Token: 0x060031D7 RID: 12759 RVA: 0x0067023C File Offset: 0x0067023C
      public string OneLiner { get; set; }

      // Token: 0x1700078D RID: 1933
      // (get) Token: 0x060031D8 RID: 12760 RVA: 0x00670248 File Offset: 0x00670248
      // (set) Token: 0x060031D9 RID: 12761 RVA: 0x00670250 File Offset: 0x00670250
      public IReadOnlyList<string> SuggestedAsks { get; set; }

      // Token: 0x060031DA RID: 12762 RVA: 0x0067025C File Offset: 0x0067025C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Member");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060031DB RID: 12763 RVA: 0x006702A8 File Offset: 0x006702A8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Slug = ");
        builder.Append(this.Slug);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", Role = ");
        builder.Append(this.Role);
        builder.Append(", OneLiner = ");
        builder.Append(this.OneLiner);
        builder.Append(", SuggestedAsks = ");
        builder.Append(this.SuggestedAsks);
        return true;
      }

      // Token: 0x060031DC RID: 12764 RVA: 0x00670338 File Offset: 0x00670338
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(StarterCast.Member left, StarterCast.Member right)
      {
        return !(left == right);
      }

      // Token: 0x060031DD RID: 12765 RVA: 0x00670344 File Offset: 0x00670344
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(StarterCast.Member left, StarterCast.Member right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060031DE RID: 12766 RVA: 0x00670358 File Offset: 0x00670358
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Role>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OneLiner>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<SuggestedAsks>k__BackingField);
      }

      // Token: 0x060031DF RID: 12767 RVA: 0x006703E8 File Offset: 0x006703E8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as StarterCast.Member);
      }

      // Token: 0x060031E0 RID: 12768 RVA: 0x006703F8 File Offset: 0x006703F8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(StarterCast.Member other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Role>k__BackingField, other.<Role>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<OneLiner>k__BackingField, other.<OneLiner>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<SuggestedAsks>k__BackingField, other.<SuggestedAsks>k__BackingField));
      }

      // Token: 0x060031E2 RID: 12770 RVA: 0x006704A8 File Offset: 0x006704A8
      [CompilerGenerated]
      private Member(StarterCast.Member original)
      {
        this.Slug = original.<Slug>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.Role = original.<Role>k__BackingField;
        this.OneLiner = original.<OneLiner>k__BackingField;
        this.SuggestedAsks = original.<SuggestedAsks>k__BackingField;
      }

      // Token: 0x060031E3 RID: 12771 RVA: 0x006704F8 File Offset: 0x006704F8
      [CompilerGenerated]
      public void Deconstruct(out string Slug, out string Name, out string Role, out string OneLiner, out IReadOnlyList<string> SuggestedAsks)
      {
        Slug = this.Slug;
        Name = this.Name;
        Role = this.Role;
        OneLiner = this.OneLiner;
        SuggestedAsks = this.SuggestedAsks;
      }
    }
  }
}
