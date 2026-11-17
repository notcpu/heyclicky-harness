using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200002B RID: 43
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__LinkRegex_41 : Regex
  {
    // Token: 0x06000053 RID: 83 RVA: 0x00332224 File Offset: 0x00332224
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__LinkRegex_41()
    {
      this.pattern = "\\[([^\\]]+)\\]\\((https?://[^)\\s]+)\\)";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__LinkRegex_41.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x0400002A RID: 42
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__LinkRegex_41 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__LinkRegex_41();

    // Token: 0x0200025E RID: 606
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002146 RID: 8518 RVA: 0x003F6F30 File Offset: 0x003F6F30
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__LinkRegex_41.RunnerFactory.Runner();
      }

      // Token: 0x020006C8 RID: 1736
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AC8 RID: 15048 RVA: 0x0045FD20 File Offset: 0x0045FD20
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

        // Token: 0x06003AC9 RID: 15049 RVA: 0x0045FD60 File Offset: 0x0045FD60
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 13)
          {
            int num = inputSpan.Slice(runtextpos).IndexOf('[');
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003ACA RID: 15050 RVA: 0x0045FDB0 File Offset: 0x0045FDB0
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 91)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = readOnlySpan.IndexOf(']');
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
          if (!readOnlySpan.StartsWith<char>("]("))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num5 = num;
          if (!readOnlySpan.StartsWith<char>("http"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          if (readOnlySpan.Length > 4 && *readOnlySpan[4] == 115)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          if (!readOnlySpan.Slice(4).StartsWith<char>("://"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 7;
          readOnlySpan = inputSpan.Slice(num);
          int num6 = 0;
          char c;
          while (num6 < readOnlySpan.Length && (((c = (char)(*readOnlySpan[num6])) < '\u0080') ? (((int)"쇿\uffff﷾\uffff\uffff\uffff\uffff\uffff"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) != 0) : RegexRunner.CharInClass(c, "\u0001\u0002\u0001)*d")))
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
          base.Capture(2, num5, num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 41)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003ACC RID: 15052 RVA: 0x0045FF98 File Offset: 0x0045FF98
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
