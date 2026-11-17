using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000027 RID: 39
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MeridiemRegex_37 : Regex
  {
    // Token: 0x0600004B RID: 75 RVA: 0x003320C4 File Offset: 0x003320C4
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MeridiemRegex_37()
    {
      this.pattern = "(\\d)\\s*([ap])\\.?m\\.?\\b";
      this.roptions = RegexOptions.IgnoreCase;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MeridiemRegex_37.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x04000026 RID: 38
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MeridiemRegex_37 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MeridiemRegex_37();

    // Token: 0x0200025A RID: 602
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600213E RID: 8510 RVA: 0x003F6EF0 File Offset: 0x003F6EF0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MeridiemRegex_37.RunnerFactory.Runner();
      }

      // Token: 0x020006C4 RID: 1732
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AB4 RID: 15028 RVA: 0x0045F068 File Offset: 0x0045F068
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

        // Token: 0x06003AB5 RID: 15029 RVA: 0x0045F0A8 File Offset: 0x0045F0A8
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 3)
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

        // Token: 0x06003AB6 RID: 15030 RVA: 0x0045F0F4 File Offset: 0x0045F0F4
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          if (readOnlySpan.IsEmpty || !char.IsDigit((char)(*readOnlySpan[0])))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          base.Capture(1, num3, num);
          int num4 = 0;
          while (num4 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num4])))
          {
            num4++;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          int num5 = num;
          ulong num6;
          if (readOnlySpan.IsEmpty || ((9223653513979035648UL << (int)(num6 = (ulong)(*readOnlySpan[0] - 65))) & (num6 - 64UL)) >= 0UL)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          base.Capture(2, num5, num);
          if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 46)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          if (readOnlySpan.IsEmpty || (*readOnlySpan[0] | 32) != 109)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num7 = num;
          if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 46)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          int num8 = num;
          for (;;)
          {
            int num9 = base.Crawlpos();
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.IsBoundary(inputSpan, num))
            {
              goto Block_11;
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num7 >= num8)
            {
              break;
            }
            num8 = (num = num8 - 1);
            readOnlySpan = inputSpan.Slice(num);
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_11:
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003AB8 RID: 15032 RVA: 0x0045F2C8 File Offset: 0x0045F2C8
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
