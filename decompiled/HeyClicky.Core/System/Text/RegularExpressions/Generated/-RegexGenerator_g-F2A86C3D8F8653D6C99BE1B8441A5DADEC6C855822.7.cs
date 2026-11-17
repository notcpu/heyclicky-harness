using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000008 RID: 8
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordBreak_6 : Regex
  {
    // Token: 0x0600000D RID: 13 RVA: 0x00331604 File Offset: 0x00331604
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordBreak_6()
    {
      this.pattern = "<w:(br|cr)\\b[^>]*/>";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordBreak_6.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000007 RID: 7
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordBreak_6 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordBreak_6();

    // Token: 0x0200023B RID: 571
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002100 RID: 8448 RVA: 0x003F6D00 File Offset: 0x003F6D00
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordBreak_6.RunnerFactory.Runner();
      }

      // Token: 0x020006A5 RID: 1701
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A23 RID: 14883 RVA: 0x0045AC0C File Offset: 0x0045AC0C
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

        // Token: 0x06003A24 RID: 14884 RVA: 0x0045AC4C File Offset: 0x0045AC4C
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 7)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_E74F74D0ED66D8490D5B0B1D9BB2497AE7C17B70A136C471D98114C581F89718);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A25 RID: 14885 RVA: 0x0045AC9C File Offset: 0x0045AC9C
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("<w:"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 3;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          if (readOnlySpan.IsEmpty)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          char c = (char)(*readOnlySpan[0]);
          if (c != 'b')
          {
            if (c != 'c')
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 114)
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 2;
            readOnlySpan = inputSpan.Slice(num);
          }
          else
          {
            if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 114)
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 2;
            readOnlySpan = inputSpan.Slice(num);
          }
          base.Capture(1, num3, num);
          if (!<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.IsBoundary(inputSpan, num))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num4 = num;
          int num5 = readOnlySpan.IndexOf('>');
          if (num5 < 0)
          {
            num5 = readOnlySpan.Length;
          }
          readOnlySpan = readOnlySpan.Slice(num5);
          num += num5;
          int num6 = num;
          for (;;)
          {
            int num7 = base.Crawlpos();
            if (readOnlySpan.StartsWith<char>("/>"))
            {
              goto Block_11;
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num7);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num4 >= num6 || (num6 = inputSpan.Slice(num4, Math.Min(inputSpan.Length, num6 + 1) - num4).LastIndexOf<char>("/>")) < 0)
            {
              break;
            }
            num6 += num4;
            num = num6;
            readOnlySpan = inputSpan.Slice(num);
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_11:
          num += 2;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A27 RID: 14887 RVA: 0x0045AE74 File Offset: 0x0045AE74
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
