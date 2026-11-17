using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000015 RID: 21
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__InlineImageRegex_19 : Regex
  {
    // Token: 0x06000027 RID: 39 RVA: 0x00331A8C File Offset: 0x00331A8C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__InlineImageRegex_19()
    {
      this.pattern = "!\\[([^\\]]*)\\]\\(";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__InlineImageRegex_19.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000014 RID: 20
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__InlineImageRegex_19 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__InlineImageRegex_19();

    // Token: 0x02000248 RID: 584
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600211A RID: 8474 RVA: 0x003F6DD0 File Offset: 0x003F6DD0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__InlineImageRegex_19.RunnerFactory.Runner();
      }

      // Token: 0x020006B2 RID: 1714
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A5E RID: 14942 RVA: 0x0045C6C8 File Offset: 0x0045C6C8
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

        // Token: 0x06003A5F RID: 14943 RVA: 0x0045C708 File Offset: 0x0045C708
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 4)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_A35D34A63ABD1CA9D7860076AA22A8E02AF70FE7A4FEF864363EC52527771C59);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A60 RID: 14944 RVA: 0x0045C758 File Offset: 0x0045C758
        private bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("!["))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = readOnlySpan.IndexOf(']');
          if (num4 < 0)
          {
            num4 = readOnlySpan.Length;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          base.Capture(1, num3, num);
          if (!readOnlySpan.StartsWith<char>("]("))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A62 RID: 14946 RVA: 0x0045C80C File Offset: 0x0045C80C
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
