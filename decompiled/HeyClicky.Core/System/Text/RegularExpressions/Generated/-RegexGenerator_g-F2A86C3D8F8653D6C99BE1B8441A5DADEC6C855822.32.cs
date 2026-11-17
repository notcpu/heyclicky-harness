using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000021 RID: 33
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeLine_31 : Regex
  {
    // Token: 0x0600003F RID: 63 RVA: 0x00331EB4 File Offset: 0x00331EB4
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeLine_31()
    {
      this.pattern = "<([0-9A-Fa-f]+)>\\s*<([0-9A-Fa-f]+)>\\s*(?:<([0-9A-Fa-f]*)>|\\[([^\\]]*)\\])";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeLine_31.RunnerFactory();
      this.capsize = 5;
    }

    // Token: 0x04000020 RID: 32
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeLine_31 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeLine_31();

    // Token: 0x02000254 RID: 596
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002132 RID: 8498 RVA: 0x003F6E90 File Offset: 0x003F6E90
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeLine_31.RunnerFactory.Runner();
      }

      // Token: 0x020006BE RID: 1726
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A99 RID: 15001 RVA: 0x0045E1B8 File Offset: 0x0045E1B8
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

        // Token: 0x06003A9A RID: 15002 RVA: 0x0045E1F8 File Offset: 0x0045E1F8
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 8)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            for (int i = 0; i < readOnlySpan.Length - 7; i++)
            {
              int num = readOnlySpan.Slice(i).IndexOf('<');
              if (num < 0)
              {
                break;
              }
              i += num;
              if (i + 1 >= readOnlySpan.Length)
              {
                break;
              }
              if (char.IsAsciiHexDigit((char)(*readOnlySpan[i + 1])))
              {
                this.runtextpos = runtextpos + i;
                return true;
              }
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A9B RID: 15003 RVA: 0x0045E284 File Offset: 0x0045E284
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
          if (num4 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          base.Capture(1, num3, num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 62)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num5 = 1;
          while (num5 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num5])))
          {
            num5++;
          }
          readOnlySpan = readOnlySpan.Slice(num5);
          num += num5;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 60)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num6 = num;
          int num7 = readOnlySpan.IndexOfAnyExcept<char>(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_asciiHexDigits);
          if (num7 < 0)
          {
            num7 = readOnlySpan.Length;
          }
          if (num7 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num7);
          num += num7;
          base.Capture(2, num6, num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 62)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num8 = 1;
          while (num8 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num8])))
          {
            num8++;
          }
          readOnlySpan = readOnlySpan.Slice(num8);
          num += num8;
          if (readOnlySpan.IsEmpty)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          char c = (char)(*readOnlySpan[0]);
          if (c != '<')
          {
            if (c != '[')
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
            int num9 = num;
            int num10 = readOnlySpan.IndexOf(']');
            if (num10 < 0)
            {
              num10 = readOnlySpan.Length;
            }
            readOnlySpan = readOnlySpan.Slice(num10);
            num += num10;
            base.Capture(4, num9, num);
            if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 93)
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
          }
          else
          {
            num++;
            readOnlySpan = inputSpan.Slice(num);
            int num11 = num;
            int num12 = readOnlySpan.IndexOfAnyExcept<char>(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_asciiHexDigits);
            if (num12 < 0)
            {
              num12 = readOnlySpan.Length;
            }
            readOnlySpan = readOnlySpan.Slice(num12);
            num += num12;
            base.Capture(3, num11, num);
            if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 62)
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
          }
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A9D RID: 15005 RVA: 0x0045E578 File Offset: 0x0045E578
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
