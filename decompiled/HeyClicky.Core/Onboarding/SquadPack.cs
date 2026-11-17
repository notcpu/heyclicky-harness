using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000F0 RID: 240
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SquadPack : IEquatable<SquadPack>
  {
    // Token: 0x06000F50 RID: 3920 RVA: 0x0038E550 File Offset: 0x0038E550
    public SquadPack(string Id, string Title, bool IsCloud)
    {
      this.Id = Id;
      this.Title = Title;
      this.IsCloud = IsCloud;
      base..ctor();
    }

    // Token: 0x17000375 RID: 885
    // (get) Token: 0x06000F51 RID: 3921 RVA: 0x0038E570 File Offset: 0x0038E570
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(SquadPack);
      }
    }

    // Token: 0x17000376 RID: 886
    // (get) Token: 0x06000F52 RID: 3922 RVA: 0x0038E57C File Offset: 0x0038E57C
    // (set) Token: 0x06000F53 RID: 3923 RVA: 0x0038E584 File Offset: 0x0038E584
    public string Id { get; set; }

    // Token: 0x17000377 RID: 887
    // (get) Token: 0x06000F54 RID: 3924 RVA: 0x0038E590 File Offset: 0x0038E590
    // (set) Token: 0x06000F55 RID: 3925 RVA: 0x0038E598 File Offset: 0x0038E598
    public string Title { get; set; }

    // Token: 0x17000378 RID: 888
    // (get) Token: 0x06000F56 RID: 3926 RVA: 0x0038E5A4 File Offset: 0x0038E5A4
    // (set) Token: 0x06000F57 RID: 3927 RVA: 0x0038E5AC File Offset: 0x0038E5AC
    public bool IsCloud { get; set; }

    // Token: 0x17000379 RID: 889
    // (get) Token: 0x06000F58 RID: 3928 RVA: 0x0038E5B8 File Offset: 0x0038E5B8
    public string VoiceIdentifier
    {
      get
      {
        if (!this.IsCloud)
        {
          return "marin";
        }
        return "cedar";
      }
    }

    // Token: 0x06000F59 RID: 3929 RVA: 0x0038E5D0 File Offset: 0x0038E5D0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SquadPack");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000F5A RID: 3930 RVA: 0x0038E61C File Offset: 0x0038E61C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", IsCloud = ");
      builder.Append(this.IsCloud.ToString());
      builder.Append(", VoiceIdentifier = ");
      builder.Append(this.VoiceIdentifier);
      return true;
    }

    // Token: 0x06000F5B RID: 3931 RVA: 0x0038E6A4 File Offset: 0x0038E6A4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(SquadPack left, SquadPack right)
    {
      return !(left == right);
    }

    // Token: 0x06000F5C RID: 3932 RVA: 0x0038E6B0 File Offset: 0x0038E6B0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(SquadPack left, SquadPack right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000F5D RID: 3933 RVA: 0x0038E6C4 File Offset: 0x0038E6C4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsCloud>k__BackingField);
    }

    // Token: 0x06000F5E RID: 3934 RVA: 0x0038E728 File Offset: 0x0038E728
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as SquadPack);
    }

    // Token: 0x06000F5F RID: 3935 RVA: 0x0038E738 File Offset: 0x0038E738
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(SquadPack other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsCloud>k__BackingField, other.<IsCloud>k__BackingField));
    }

    // Token: 0x06000F61 RID: 3937 RVA: 0x0038E7B4 File Offset: 0x0038E7B4
    [CompilerGenerated]
    private SquadPack(SquadPack original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.IsCloud = original.<IsCloud>k__BackingField;
    }

    // Token: 0x06000F62 RID: 3938 RVA: 0x0038E7E0 File Offset: 0x0038E7E0
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string Title, out bool IsCloud)
    {
      Id = this.Id;
      Title = this.Title;
      IsCloud = this.IsCloud;
    }
  }
}
