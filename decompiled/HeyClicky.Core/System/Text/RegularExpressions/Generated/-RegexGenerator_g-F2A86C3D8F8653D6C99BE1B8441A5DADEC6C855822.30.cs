using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200001F RID: 31
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeBlock_29 : Regex
  {
    // Token: 0x0600003B RID: 59 RVA: 0x00331E00 File Offset: 0x00331E00
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeBlock_29()
    {
      this.pattern = "beginbfrange(.*?)endbfrange";
      this.roptions = RegexOptions.Singleline;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeBlock_29.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x0400001E RID: 30
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeBlock_29 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeBlock_29();

    // Token: 0x02000252 RID: 594
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600212E RID: 8494 RVA: 0x003F6E70 File Offset: 0x003F6E70
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeBlock_29.RunnerFactory.Runner();
      }

      // Token: 0x020006BC RID: 1724
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A8F RID: 14991 RVA: 0x0045DD90 File Offset: 0x0045DD90
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          while (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan) && this.runtextpos != inputSpan.Length)
          {
            this.runtextpos++;
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
          }
        }

        // Token: 0x06003A90 RID: 14992 RVA: 0x0045DDD0 File Offset: 0x0045DDD0
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 22)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_beginbfrange_Ordinal);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A91 RID: 14993 RVA: 0x0045DE24 File Offset: 0x0045DE24
        private bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("beginbfrange"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 12;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = num;
          for (;;)
          {
            int num5 = base.Crawlpos();
            base.Capture(1, num3, num);
            if (readOnlySpan.StartsWith<char>("endbfrange"))
            {
              goto Block_5;
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num5);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            num = num4;
            if (inputSpan.Slice(num).IsEmpty)
            {
              break;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
            num4 = readOnlySpan.IndexOf<char>("endbfrange");
            if (num4 < 0)
            {
              goto Block_4;
            }
            num += num4;
            readOnlySpan = inputSpan.Slice(num);
            num4 = num;
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_4:
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_5:
          num += 10;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A93 RID: 14995 RVA: 0x0045DF38 File Offset: 0x0045DF38
        [CompilerGenerated]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void <TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(int capturePosition)
        {
          while (base.Crawlpos() > capturePosition)
          {
            base.Uncapture();
          }
        }
      }
    }
  }
}
