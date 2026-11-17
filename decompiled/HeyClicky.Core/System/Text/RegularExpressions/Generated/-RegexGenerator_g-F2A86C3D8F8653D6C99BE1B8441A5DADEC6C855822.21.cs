using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000016 RID: 22
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ObjectHeader_20 : Regex
  {
    // Token: 0x06000029 RID: 41 RVA: 0x00331AE4 File Offset: 0x00331AE4
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ObjectHeader_20()
    {
      this.pattern = "(?<![\\d])(\\d+)\\s+(\\d+)\\s+obj\\b";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ObjectHeader_20.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x04000015 RID: 21
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ObjectHeader_20 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ObjectHeader_20();

    // Token: 0x02000249 RID: 585
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600211C RID: 8476 RVA: 0x003F6DE0 File Offset: 0x003F6DE0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ObjectHeader_20.RunnerFactory.Runner();
      }

      // Token: 0x020006B3 RID: 1715
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A63 RID: 14947 RVA: 0x0045C820 File Offset: 0x0045C820
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

        // Token: 0x06003A64 RID: 14948 RVA: 0x0045C860 File Offset: 0x0045C860
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 7)
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

        // Token: 0x06003A65 RID: 14949 RVA: 0x0045C8AC File Offset: 0x0045C8AC
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
          {
            base.CheckTimeout();
          }
          if ((ulong)(num - 1) < (ulong)((long)inputSpan.Length) && char.IsDigit((char)(*inputSpan[num - 1])))
          {
            num--;
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num = num3;
          readOnlySpan = inputSpan.Slice(num);
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
          int num7 = num;
          int num8 = 0;
          while (num8 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num8])))
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
          base.Capture(2, num7, num);
          int num9 = 0;
          while (num9 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num9])))
          {
            num9++;
          }
          if (num9 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num9);
          num += num9;
          if (!readOnlySpan.StartsWith<char>("obj"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          if (!<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.IsBoundary(inputSpan, num + 3))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 3;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A67 RID: 14951 RVA: 0x0045CA98 File Offset: 0x0045CA98
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
