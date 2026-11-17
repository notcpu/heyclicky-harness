using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200002C RID: 44
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AdjacentLinksRegex_42 : Regex
  {
    // Token: 0x06000055 RID: 85 RVA: 0x0033227C File Offset: 0x0033227C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AdjacentLinksRegex_42()
    {
      this.pattern = "(\\[[^\\]]+\\]\\(https?://[^)\\s]+\\))[ \\t]*(?=\\[[^\\]]+\\]\\(https?://)";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AdjacentLinksRegex_42.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x0400002B RID: 43
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AdjacentLinksRegex_42 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AdjacentLinksRegex_42();

    // Token: 0x0200025F RID: 607
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002148 RID: 8520 RVA: 0x003F6F40 File Offset: 0x003F6F40
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AdjacentLinksRegex_42.RunnerFactory.Runner();
      }

      // Token: 0x020006C9 RID: 1737
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003ACD RID: 15053 RVA: 0x0045FFAC File Offset: 0x0045FFAC
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

        // Token: 0x06003ACE RID: 15054 RVA: 0x0045FFEC File Offset: 0x0045FFEC
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

        // Token: 0x06003ACF RID: 15055 RVA: 0x0046003C File Offset: 0x0046003C
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 91)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num4 = readOnlySpan.Slice(1).IndexOf(']');
          if (num4 < 0)
          {
            num4 = readOnlySpan.Length - 1;
          }
          if (num4 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          if (!readOnlySpan.Slice(1).StartsWith<char>("](http"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          if (readOnlySpan.Length > 7 && *readOnlySpan[7] == 115)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          if (!readOnlySpan.Slice(7).StartsWith<char>("://"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 10;
          readOnlySpan = inputSpan.Slice(num);
          int num5 = 0;
          char c;
          while (num5 < readOnlySpan.Length && (((c = (char)(*readOnlySpan[num5])) < '\u0080') ? (((int)"쇿\uffff﷾\uffff\uffff\uffff\uffff\uffff"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) != 0) : RegexRunner.CharInClass(c, "\u0001\u0002\u0001)*d")))
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
          readOnlySpan = inputSpan.Slice(num);
          base.Capture(1, num3, num);
          int num6 = readOnlySpan.IndexOfAnyExcept('\t', ' ');
          if (num6 < 0)
          {
            num6 = readOnlySpan.Length;
          }
          readOnlySpan = readOnlySpan.Slice(num6);
          num += num6;
          int num7 = num;
          if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
          {
            base.CheckTimeout();
          }
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 91)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num8 = readOnlySpan.Slice(1).IndexOf(']');
          if (num8 < 0)
          {
            num8 = readOnlySpan.Length - 1;
          }
          if (num8 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num8);
          num += num8;
          if (!readOnlySpan.Slice(1).StartsWith<char>("](http"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          if (readOnlySpan.Length > 7 && *readOnlySpan[7] == 115)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          if (!readOnlySpan.Slice(7).StartsWith<char>("://"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num = num7;
          readOnlySpan = inputSpan.Slice(num);
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003AD1 RID: 15057 RVA: 0x00460304 File Offset: 0x00460304
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
