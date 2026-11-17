using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200001E RID: 30
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfCharBlock_28 : Regex
  {
    // Token: 0x06000039 RID: 57 RVA: 0x00331DA4 File Offset: 0x00331DA4
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfCharBlock_28()
    {
      this.pattern = "beginbfchar(.*?)endbfchar";
      this.roptions = RegexOptions.Singleline;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfCharBlock_28.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x0400001D RID: 29
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfCharBlock_28 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfCharBlock_28();

    // Token: 0x02000251 RID: 593
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600212C RID: 8492 RVA: 0x003F6E60 File Offset: 0x003F6E60
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfCharBlock_28.RunnerFactory.Runner();
      }

      // Token: 0x020006BB RID: 1723
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A8A RID: 14986 RVA: 0x0045DBD4 File Offset: 0x0045DBD4
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

        // Token: 0x06003A8B RID: 14987 RVA: 0x0045DC14 File Offset: 0x0045DC14
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 20)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_beginbfchar_Ordinal);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A8C RID: 14988 RVA: 0x0045DC68 File Offset: 0x0045DC68
        private bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("beginbfchar"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 11;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = num;
          for (;;)
          {
            int num5 = base.Crawlpos();
            base.Capture(1, num3, num);
            if (readOnlySpan.StartsWith<char>("endbfchar"))
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
            num4 = readOnlySpan.IndexOf<char>("endbfchar");
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
          num += 9;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A8E RID: 14990 RVA: 0x0045DD7C File Offset: 0x0045DD7C
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
