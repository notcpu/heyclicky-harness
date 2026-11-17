using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000018 RID: 24
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__RootReference_22 : Regex
  {
    // Token: 0x0600002D RID: 45 RVA: 0x00331B94 File Offset: 0x00331B94
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__RootReference_22()
    {
      this.pattern = "/Root\\s+(\\d+)\\s+\\d+\\s+R";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__RootReference_22.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000017 RID: 23
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__RootReference_22 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__RootReference_22();

    // Token: 0x0200024B RID: 587
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002120 RID: 8480 RVA: 0x003F6E00 File Offset: 0x003F6E00
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__RootReference_22.RunnerFactory.Runner();
      }

      // Token: 0x020006B5 RID: 1717
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A6D RID: 14957 RVA: 0x0045CD88 File Offset: 0x0045CD88
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

        // Token: 0x06003A6E RID: 14958 RVA: 0x0045CDC8 File Offset: 0x0045CDC8
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 11)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_1A32F470B3399637EEACAA4C40413209A17D35821C9C2505754B2D11F229B63A);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A6F RID: 14959 RVA: 0x0045CE1C File Offset: 0x0045CE1C
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("/Root"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 5;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = 0;
          while (num3 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num3])))
          {
            num3++;
          }
          if (num3 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          int num4 = num;
          int num5 = 0;
          while (num5 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num5])))
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
          base.Capture(1, num4, num);
          int num6 = 0;
          while (num6 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num6])))
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
          while (num7 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num7])))
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
          int num8 = 0;
          while (num8 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num8])))
          {
            num8++;
          }
          if (num8 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num8);
          num += num8;
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

        // Token: 0x06003A71 RID: 14961 RVA: 0x0045CFFC File Offset: 0x0045CFFC
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
