using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000052 RID: 82
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ClickyWorkEntry : IEquatable<ClickyWorkEntry>
  {
    // Token: 0x170001D3 RID: 467
    // (get) Token: 0x0600072E RID: 1838 RVA: 0x000FAD38 File Offset: 0x000FAD38
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyWorkEntry);
      }
    }

    // Token: 0x170001D4 RID: 468
    // (get) Token: 0x0600072F RID: 1839 RVA: 0x000FAD44 File Offset: 0x000FAD44
    // (set) Token: 0x06000730 RID: 1840 RVA: 0x000FAD4C File Offset: 0x000FAD4C
    [RequiredMember]
    public string Id { get; set; }

    // Token: 0x170001D5 RID: 469
    // (get) Token: 0x06000731 RID: 1841 RVA: 0x000FAD58 File Offset: 0x000FAD58
    // (set) Token: 0x06000732 RID: 1842 RVA: 0x000FAD60 File Offset: 0x000FAD60
    [RequiredMember]
    public string Title { get; set; }

    // Token: 0x170001D6 RID: 470
    // (get) Token: 0x06000733 RID: 1843 RVA: 0x000FAD6C File Offset: 0x000FAD6C
    // (set) Token: 0x06000734 RID: 1844 RVA: 0x000FAD74 File Offset: 0x000FAD74
    public DateTimeOffset LastActive { get; set; }

    // Token: 0x170001D7 RID: 471
    // (get) Token: 0x06000735 RID: 1845 RVA: 0x000FAD80 File Offset: 0x000FAD80
    // (set) Token: 0x06000736 RID: 1846 RVA: 0x000FAD88 File Offset: 0x000FAD88
    public IReadOnlyList<string> Asks { get; set; }

    // Token: 0x170001D8 RID: 472
    // (get) Token: 0x06000737 RID: 1847 RVA: 0x000FAD94 File Offset: 0x000FAD94
    // (set) Token: 0x06000738 RID: 1848 RVA: 0x000FAD9C File Offset: 0x000FAD9C
    [Nullable(2)]
    public string Result
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001D9 RID: 473
    // (get) Token: 0x06000739 RID: 1849 RVA: 0x000FADA8 File Offset: 0x000FADA8
    // (set) Token: 0x0600073A RID: 1850 RVA: 0x000FADB0 File Offset: 0x000FADB0
    [RequiredMember]
    public string State { get; set; }

    // Token: 0x170001DA RID: 474
    // (get) Token: 0x0600073B RID: 1851 RVA: 0x000FADBC File Offset: 0x000FADBC
    // (set) Token: 0x0600073C RID: 1852 RVA: 0x000FADC4 File Offset: 0x000FADC4
    [Nullable(2)]
    public string FolderPath
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001DB RID: 475
    // (get) Token: 0x0600073D RID: 1853 RVA: 0x000FADD0 File Offset: 0x000FADD0
    // (set) Token: 0x0600073E RID: 1854 RVA: 0x000FADD8 File Offset: 0x000FADD8
    public bool IsEarlierClicky { get; set; }

    // Token: 0x170001DC RID: 476
    // (get) Token: 0x0600073F RID: 1855 RVA: 0x000FADE4 File Offset: 0x000FADE4
    // (set) Token: 0x06000740 RID: 1856 RVA: 0x000FADEC File Offset: 0x000FADEC
    [Nullable(2)]
    public string ReportText
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000741 RID: 1857 RVA: 0x000FADF8 File Offset: 0x000FADF8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyWorkEntry");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000742 RID: 1858 RVA: 0x000FAE44 File Offset: 0x000FAE44
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", LastActive = ");
      builder.Append(this.LastActive.ToString());
      builder.Append(", Asks = ");
      builder.Append(this.Asks);
      builder.Append(", Result = ");
      builder.Append(this.Result);
      builder.Append(", State = ");
      builder.Append(this.State);
      builder.Append(", FolderPath = ");
      builder.Append(this.FolderPath);
      builder.Append(", IsEarlierClicky = ");
      builder.Append(this.IsEarlierClicky.ToString());
      builder.Append(", ReportText = ");
      builder.Append(this.ReportText);
      return true;
    }

    // Token: 0x06000743 RID: 1859 RVA: 0x000FAF54 File Offset: 0x000FAF54
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyWorkEntry left, ClickyWorkEntry right)
    {
      return !(left == right);
    }

    // Token: 0x06000744 RID: 1860 RVA: 0x000FAF60 File Offset: 0x000FAF60
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyWorkEntry left, ClickyWorkEntry right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000745 RID: 1861 RVA: 0x000FAF74 File Offset: 0x000FAF74
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<LastActive>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Asks>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Result>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<State>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FolderPath>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsEarlierClicky>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ReportText>k__BackingField);
    }

    // Token: 0x06000746 RID: 1862 RVA: 0x000FB060 File Offset: 0x000FB060
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyWorkEntry);
    }

    // Token: 0x06000747 RID: 1863 RVA: 0x000FB070 File Offset: 0x000FB070
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyWorkEntry other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<LastActive>k__BackingField, other.<LastActive>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Asks>k__BackingField, other.<Asks>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Result>k__BackingField, other.<Result>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<State>k__BackingField, other.<State>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FolderPath>k__BackingField, other.<FolderPath>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsEarlierClicky>k__BackingField, other.<IsEarlierClicky>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ReportText>k__BackingField, other.<ReportText>k__BackingField));
    }

    // Token: 0x06000749 RID: 1865 RVA: 0x000FB18C File Offset: 0x000FB18C
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ClickyWorkEntry(ClickyWorkEntry original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.LastActive = original.<LastActive>k__BackingField;
      this.Asks = original.<Asks>k__BackingField;
      this.Result = original.<Result>k__BackingField;
      this.State = original.<State>k__BackingField;
      this.FolderPath = original.<FolderPath>k__BackingField;
      this.IsEarlierClicky = original.<IsEarlierClicky>k__BackingField;
      this.ReportText = original.<ReportText>k__BackingField;
    }

    // Token: 0x0600074A RID: 1866 RVA: 0x000FB20C File Offset: 0x000FB20C
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ClickyWorkEntry()
    {
      this.Asks = Array.Empty<string>();
      base..ctor();
    }
  }
}
