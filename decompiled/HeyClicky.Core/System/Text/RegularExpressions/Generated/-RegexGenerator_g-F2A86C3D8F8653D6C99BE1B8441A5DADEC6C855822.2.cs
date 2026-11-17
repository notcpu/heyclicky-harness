using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000003 RID: 3
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkdownLinkPattern_1 : Regex
  {
    // Token: 0x06000003 RID: 3 RVA: 0x0033144C File Offset: 0x0033144C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkdownLinkPattern_1()
    {
      this.pattern = "\\[([^\\]\\n]{1,80})\\]\\(https?://[^)\\s\"]+\\)";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkdownLinkPattern_1.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000002 RID: 2
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkdownLinkPattern_1 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkdownLinkPattern_1();

    // Token: 0x02000236 RID: 566
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060020F6 RID: 8438 RVA: 0x003F6CB0 File Offset: 0x003F6CB0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkdownLinkPattern_1.RunnerFactory.Runner();
      }

      // Token: 0x020006A0 RID: 1696
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A0E RID: 14862 RVA: 0x0045A378 File Offset: 0x0045A378
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

        // Token: 0x06003A0F RID: 14863 RVA: 0x0045A3B8 File Offset: 0x0045A3B8
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 13)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            for (int i = 0; i < readOnlySpan.Length - 12; i++)
            {
              int num = readOnlySpan.Slice(i).IndexOf('[');
              if (num < 0)
              {
                break;
              }
              i += num;
              if (i + 1 >= readOnlySpan.Length)
              {
                break;
              }
              char c;
              if (((c = (char)(*readOnlySpan[i + 1])) != '\n') & (c != ']'))
              {
                this.runtextpos = runtextpos + i;
                return true;
              }
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A10 RID: 14864 RVA: 0x0045A458 File Offset: 0x0045A458
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
          int num4 = 0;
          char c;
          while (num4 < 80 && num4 < readOnlySpan.Length && (((c = (char)(*readOnlySpan[num4])) != '\n') & (c != ']')))
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
          if (!readOnlySpan.StartsWith<char>("](http"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          if (readOnlySpan.Length > 6 && *readOnlySpan[6] == 115)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          if (!readOnlySpan.Slice(6).StartsWith<char>("://"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 9;
          readOnlySpan = inputSpan.Slice(num);
          int num5 = 0;
          while (num5 < readOnlySpan.Length && (((c = (char)(*readOnlySpan[num5])) < '\u0080') ? (((int)"쇿\uffffﷺ\uffff\uffff\uffff\uffff\uffff"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) != 0) : RegexRunner.CharInClass(c, "\u0001\u0004\u0001\"#)*d")))
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

        // Token: 0x06003A12 RID: 14866 RVA: 0x0045A628 File Offset: 0x0045A628
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
