using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000022 RID: 34
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexString_32 : Regex
  {
    // Token: 0x06000041 RID: 65 RVA: 0x00331F0C File Offset: 0x00331F0C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexString_32()
    {
      this.pattern = "<([0-9A-Fa-f]*)>";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexString_32.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000021 RID: 33
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexString_32 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexString_32();

    // Token: 0x02000255 RID: 597
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002134 RID: 8500 RVA: 0x003F6EA0 File Offset: 0x003F6EA0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexString_32.RunnerFactory.Runner();
      }

      // Token: 0x020006BF RID: 1727
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A9E RID: 15006 RVA: 0x0045E58C File Offset: 0x0045E58C
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

        // Token: 0x06003A9F RID: 15007 RVA: 0x0045E5CC File Offset: 0x0045E5CC
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 2)
          {
            int num = inputSpan.Slice(runtextpos).IndexOf('<');
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AA0 RID: 15008 RVA: 0x0045E61C File Offset: 0x0045E61C
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 60)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = readOnlySpan.IndexOfAnyExcept<char>(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_asciiHexDigits);
          if (num4 < 0)
          {
            num4 = readOnlySpan.Length;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          base.Capture(1, num3, num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 62)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003AA2 RID: 15010 RVA: 0x0045E6DC File Offset: 0x0045E6DC
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
