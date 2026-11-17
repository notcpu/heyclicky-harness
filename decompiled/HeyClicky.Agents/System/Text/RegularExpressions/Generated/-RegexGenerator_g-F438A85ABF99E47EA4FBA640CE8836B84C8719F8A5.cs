using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000002 RID: 2
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__NonSlug_0 : Regex
  {
    // Token: 0x06000001 RID: 1 RVA: 0x000D5C5C File Offset: 0x000D5C5C
    private <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__NonSlug_0()
    {
      this.pattern = "[^a-z0-9]+";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__NonSlug_0.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000001 RID: 1
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__NonSlug_0 Instance = new <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__NonSlug_0();

    // Token: 0x0200007B RID: 123
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06000973 RID: 2419 RVA: 0x001042C8 File Offset: 0x001042C8
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__NonSlug_0.RunnerFactory.Runner();
      }

      // Token: 0x020001F0 RID: 496
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06000FBF RID: 4031 RVA: 0x0011D540 File Offset: 0x0011D540
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          while (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan) && this.runtextpos != inputSpan.Length)
          {
            this.runtextpos++;
            if (<RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
          }
        }

        // Token: 0x06000FC0 RID: 4032 RVA: 0x0011D580 File Offset: 0x0011D580
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos < inputSpan.Length)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAnyExcept<char>(<RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities.s_ascii_FF0300000000FEFFFF07);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06000FC1 RID: 4033 RVA: 0x0011D5D0 File Offset: 0x0011D5D0
        private bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          int num3 = readOnlySpan.IndexOfAny<char>(<RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__Utilities.s_ascii_FF0300000000FEFFFF07);
          if (num3 < 0)
          {
            num3 = readOnlySpan.Length;
          }
          if (num3 == 0)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
