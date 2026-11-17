using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000020 RID: 32
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexPair_30 : Regex
  {
    // Token: 0x0600003D RID: 61 RVA: 0x00331E5C File Offset: 0x00331E5C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexPair_30()
    {
      this.pattern = "<([0-9A-Fa-f]+)>\\s*<([0-9A-Fa-f]*)>";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexPair_30.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x0400001F RID: 31
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexPair_30 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexPair_30();

    // Token: 0x02000253 RID: 595
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002130 RID: 8496 RVA: 0x003F6E80 File Offset: 0x003F6E80
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexPair_30.RunnerFactory.Runner();
      }

      // Token: 0x020006BD RID: 1725
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A94 RID: 14996 RVA: 0x0045DF4C File Offset: 0x0045DF4C
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

        // Token: 0x06003A95 RID: 14997 RVA: 0x0045DF8C File Offset: 0x0045DF8C
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 5)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            for (int i = 0; i < readOnlySpan.Length - 4; i++)
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

        // Token: 0x06003A96 RID: 14998 RVA: 0x0045E018 File Offset: 0x0045E018
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
          readOnlySpan = readOnlySpan.Slice(num7);
          num += num7;
          base.Capture(2, num6, num);
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

        // Token: 0x06003A98 RID: 15000 RVA: 0x0045E1A4 File Offset: 0x0045E1A4
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
