using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200001B RID: 27
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__IndirectReference_25 : Regex
  {
    // Token: 0x06000033 RID: 51 RVA: 0x00331C9C File Offset: 0x00331C9C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__IndirectReference_25()
    {
      this.pattern = "(\\d+)\\s+\\d+\\s+R";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__IndirectReference_25.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x0400001A RID: 26
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__IndirectReference_25 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__IndirectReference_25();

    // Token: 0x0200024E RID: 590
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002126 RID: 8486 RVA: 0x003F6E30 File Offset: 0x003F6E30
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__IndirectReference_25.RunnerFactory.Runner();
      }

      // Token: 0x020006B8 RID: 1720
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A7B RID: 14971 RVA: 0x0045D448 File Offset: 0x0045D448
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

        // Token: 0x06003A7C RID: 14972 RVA: 0x0045D488 File Offset: 0x0045D488
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 5)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAnyDigit();
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A7D RID: 14973 RVA: 0x0045D4D4 File Offset: 0x0045D4D4
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = 0;
          while (num4 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num4])))
          {
            num4++;
          }
          if (num4 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          base.Capture(1, num3, num);
          int num5 = 0;
          while (num5 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num5])))
          {
            num5++;
          }
          if (num5 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num5);
          num += num5;
          int num6 = 0;
          while (num6 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num6])))
          {
            num6++;
          }
          if (num6 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num6);
          num += num6;
          int num7 = 0;
          while (num7 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num7])))
          {
            num7++;
          }
          if (num7 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num7);
          num += num7;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 82)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A7F RID: 14975 RVA: 0x0045D648 File Offset: 0x0045D648
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
