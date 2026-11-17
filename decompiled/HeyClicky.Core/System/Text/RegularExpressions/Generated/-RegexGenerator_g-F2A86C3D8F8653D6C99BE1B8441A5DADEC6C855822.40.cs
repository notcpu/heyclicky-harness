using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000029 RID: 41
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BoldRegex_39 : Regex
  {
    // Token: 0x0600004F RID: 79 RVA: 0x00332174 File Offset: 0x00332174
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BoldRegex_39()
    {
      this.pattern = "\\*\\*(.+?)\\*\\*";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BoldRegex_39.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000028 RID: 40
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BoldRegex_39 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BoldRegex_39();

    // Token: 0x0200025C RID: 604
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002142 RID: 8514 RVA: 0x003F6F10 File Offset: 0x003F6F10
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BoldRegex_39.RunnerFactory.Runner();
      }

      // Token: 0x020006C6 RID: 1734
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003ABE RID: 15038 RVA: 0x0045F920 File Offset: 0x0045F920
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

        // Token: 0x06003ABF RID: 15039 RVA: 0x0045F960 File Offset: 0x0045F960
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 5)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_4B938BB3612E6695167AB8A3A3AD73E3F6DB0CC76E502A73DB7C1EC79B5A2092);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AC0 RID: 15040 RVA: 0x0045F9B0 File Offset: 0x0045F9B0
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("**"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] == 10)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num4 = num;
          for (;;)
          {
            int num5 = base.Crawlpos();
            base.Capture(1, num3, num);
            if (readOnlySpan.StartsWith<char>("**"))
            {
              goto Block_6;
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num5);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            num = num4;
            readOnlySpan = inputSpan.Slice(num);
            if (readOnlySpan.IsEmpty || *readOnlySpan[0] == 10)
            {
              break;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
            num4 = readOnlySpan.IndexOfAny('\n', '*');
            if (num4 >= readOnlySpan.Length || *readOnlySpan[num4] == 10)
            {
              goto IL_00EF;
            }
            num += num4;
            readOnlySpan = inputSpan.Slice(num);
            num4 = num;
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_00EF:
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_6:
          num += 2;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003AC2 RID: 15042 RVA: 0x0045FB0C File Offset: 0x0045FB0C
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
