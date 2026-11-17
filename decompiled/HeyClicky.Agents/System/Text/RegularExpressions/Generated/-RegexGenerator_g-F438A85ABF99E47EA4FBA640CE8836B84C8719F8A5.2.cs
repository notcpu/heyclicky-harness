using System;
using System.Buffers;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000003 RID: 3
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  internal static class <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities
  {
    // Token: 0x06000003 RID: 3 RVA: 0x000D5CB4 File Offset: 0x000D5CB4
    // Note: this type is marked as 'beforefieldinit'.
    static <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities()
    {
      object data = AppContext.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
      TimeSpan timeSpan2;
      if (data is TimeSpan)
      {
        TimeSpan timeSpan = (TimeSpan)data;
        timeSpan2 = timeSpan;
      }
      else
      {
        timeSpan2 = Regex.InfiniteMatchTimeout;
      }
      <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities.s_defaultTimeout = timeSpan2;
      <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities.s_hasTimeout = <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities.s_defaultTimeout != Regex.InfiniteMatchTimeout;
      <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities.s_ascii_FF0300000000FEFFFF07 = SearchValues.Create("0123456789abcdefghijklmnopqrstuvwxyz");
    }

    // Token: 0x04000002 RID: 2
    internal static readonly TimeSpan s_defaultTimeout;

    // Token: 0x04000003 RID: 3
    internal static readonly bool s_hasTimeout;

    // Token: 0x04000004 RID: 4
    [Nullable(1)]
    internal static readonly SearchValues<char> s_ascii_FF0300000000FEFFFF07;
  }
}
