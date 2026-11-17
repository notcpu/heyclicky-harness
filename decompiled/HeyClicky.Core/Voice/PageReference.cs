using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200008A RID: 138
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PageReference : IEquatable<PageReference>
  {
    // Token: 0x06000857 RID: 2135 RVA: 0x003693F8 File Offset: 0x003693F8
    public PageReference(string Kind, string Reference, int ProcessId = 0, IntPtr WindowHandle = 0)
    {
      this.Kind = Kind;
      this.Reference = Reference;
      this.ProcessId = ProcessId;
      this.WindowHandle = WindowHandle;
      base..ctor();
    }

    // Token: 0x170001D4 RID: 468
    // (get) Token: 0x06000858 RID: 2136 RVA: 0x00369420 File Offset: 0x00369420
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PageReference);
      }
    }

    // Token: 0x170001D5 RID: 469
    // (get) Token: 0x06000859 RID: 2137 RVA: 0x0036942C File Offset: 0x0036942C
    // (set) Token: 0x0600085A RID: 2138 RVA: 0x00369434 File Offset: 0x00369434
    public string Kind { get; set; }

    // Token: 0x170001D6 RID: 470
    // (get) Token: 0x0600085B RID: 2139 RVA: 0x00369440 File Offset: 0x00369440
    // (set) Token: 0x0600085C RID: 2140 RVA: 0x00369448 File Offset: 0x00369448
    public string Reference { get; set; }

    // Token: 0x170001D7 RID: 471
    // (get) Token: 0x0600085D RID: 2141 RVA: 0x00369454 File Offset: 0x00369454
    // (set) Token: 0x0600085E RID: 2142 RVA: 0x0036945C File Offset: 0x0036945C
    public int ProcessId { get; set; }

    // Token: 0x170001D8 RID: 472
    // (get) Token: 0x0600085F RID: 2143 RVA: 0x00369468 File Offset: 0x00369468
    // (set) Token: 0x06000860 RID: 2144 RVA: 0x00369470 File Offset: 0x00369470
    public IntPtr WindowHandle { get; set; }

    // Token: 0x170001D9 RID: 473
    // (get) Token: 0x06000861 RID: 2145 RVA: 0x0036947C File Offset: 0x0036947C
    public bool IsWebPage
    {
      get
      {
        return this.Kind == "web_page";
      }
    }

    // Token: 0x170001DA RID: 474
    // (get) Token: 0x06000862 RID: 2146 RVA: 0x00369490 File Offset: 0x00369490
    public string AgentPromptContextLine
    {
      get
      {
        if (!this.IsWebPage)
        {
          return "Context: the user currently has this file open on screen: " + this.Reference + " — if the task relates to it, open and read the file directly instead of relying on the screenshot's visible portion.";
        }
        return "Context: the user currently has this page open in their browser: " + this.Reference + " — if the task relates to it, fetch and read that page directly instead of relying on the screenshot's visible portion.";
      }
    }

    // Token: 0x06000863 RID: 2147 RVA: 0x003694C8 File Offset: 0x003694C8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PageReference");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000864 RID: 2148 RVA: 0x00369514 File Offset: 0x00369514
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind);
      builder.Append(", Reference = ");
      builder.Append(this.Reference);
      builder.Append(", ProcessId = ");
      builder.Append(this.ProcessId.ToString());
      builder.Append(", WindowHandle = ");
      builder.Append(this.WindowHandle.ToString());
      builder.Append(", IsWebPage = ");
      builder.Append(this.IsWebPage.ToString());
      builder.Append(", AgentPromptContextLine = ");
      builder.Append(this.AgentPromptContextLine);
      return true;
    }

    // Token: 0x06000865 RID: 2149 RVA: 0x003695E8 File Offset: 0x003695E8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PageReference left, PageReference right)
    {
      return !(left == right);
    }

    // Token: 0x06000866 RID: 2150 RVA: 0x003695F4 File Offset: 0x003695F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PageReference left, PageReference right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000867 RID: 2151 RVA: 0x00369608 File Offset: 0x00369608
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Reference>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ProcessId>k__BackingField)) * -1521134295 + EqualityComparer<IntPtr>.Default.GetHashCode(this.<WindowHandle>k__BackingField);
    }

    // Token: 0x06000868 RID: 2152 RVA: 0x00369684 File Offset: 0x00369684
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PageReference);
    }

    // Token: 0x06000869 RID: 2153 RVA: 0x00369694 File Offset: 0x00369694
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PageReference other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Reference>k__BackingField, other.<Reference>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ProcessId>k__BackingField, other.<ProcessId>k__BackingField) && EqualityComparer<IntPtr>.Default.Equals(this.<WindowHandle>k__BackingField, other.<WindowHandle>k__BackingField));
    }

    // Token: 0x0600086B RID: 2155 RVA: 0x00369728 File Offset: 0x00369728
    [CompilerGenerated]
    private PageReference(PageReference original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Reference = original.<Reference>k__BackingField;
      this.ProcessId = original.<ProcessId>k__BackingField;
      this.WindowHandle = original.<WindowHandle>k__BackingField;
    }

    // Token: 0x0600086C RID: 2156 RVA: 0x00369760 File Offset: 0x00369760
    [CompilerGenerated]
    public void Deconstruct(out string Kind, out string Reference, out int ProcessId, out IntPtr WindowHandle)
    {
      Kind = this.Kind;
      Reference = this.Reference;
      ProcessId = this.ProcessId;
      WindowHandle = this.WindowHandle;
    }
  }
}
