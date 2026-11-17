using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000005 RID: 5
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ChallengeParameter_3 : Regex
  {
    // Token: 0x06000007 RID: 7 RVA: 0x003314FC File Offset: 0x003314FC
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ChallengeParameter_3()
    {
      this.pattern = "([A-Za-z_]+)\\s*=\\s*\"([^\"]*)\"";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ChallengeParameter_3.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x04000004 RID: 4
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ChallengeParameter_3 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ChallengeParameter_3();

    // Token: 0x02000238 RID: 568
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060020FA RID: 8442 RVA: 0x003F6CD0 File Offset: 0x003F6CD0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ChallengeParameter_3.RunnerFactory.Runner();
      }

      // Token: 0x020006A2 RID: 1698
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A17 RID: 14871 RVA: 0x0045A824 File Offset: 0x0045A824
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

        // Token: 0x06003A18 RID: 14872 RVA: 0x0045A864 File Offset: 0x0045A864
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 4)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny<char>(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_ascii_FEFFFF87FEFFFF07);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A19 RID: 14873 RVA: 0x0045A8B4 File Offset: 0x0045A8B4
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = readOnlySpan.IndexOfAnyExcept<char>(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_ascii_FEFFFF87FEFFFF07);
          if (num4 < 0)
          {
            num4 = readOnlySpan.Length;
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
          readOnlySpan = readOnlySpan.Slice(num5);
          num += num5;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 61)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num6 = 1;
          while (num6 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num6])))
          {
            num6++;
          }
          readOnlySpan = readOnlySpan.Slice(num6);
          num += num6;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 34)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num7 = num;
          int num8 = readOnlySpan.IndexOf('"');
          if (num8 < 0)
          {
            num8 = readOnlySpan.Length;
          }
          readOnlySpan = readOnlySpan.Slice(num8);
          num += num8;
          base.Capture(2, num7, num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 34)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A1B RID: 14875 RVA: 0x0045AA48 File Offset: 0x0045AA48
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
