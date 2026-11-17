using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000028 RID: 40
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkRegex_38 : Regex
  {
    // Token: 0x0600004D RID: 77 RVA: 0x0033211C File Offset: 0x0033211C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkRegex_38()
    {
      this.pattern = "\\s*\\{\\s*(new|needs|done|pick|now|gone|out|ruled-out|ruledout|no|skip)\\s*\\}";
      this.roptions = RegexOptions.IgnoreCase;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkRegex_38.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000027 RID: 39
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkRegex_38 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkRegex_38();

    // Token: 0x0200025B RID: 603
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002140 RID: 8512 RVA: 0x003F6F00 File Offset: 0x003F6F00
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkRegex_38.RunnerFactory.Runner();
      }

      // Token: 0x020006C5 RID: 1733
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AB9 RID: 15033 RVA: 0x0045F2DC File Offset: 0x0045F2DC
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

        // Token: 0x06003ABA RID: 15034 RVA: 0x0045F31C File Offset: 0x0045F31C
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 4)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            int num = readOnlySpan.IndexOf('{');
            if (num >= 0)
            {
              int num2 = num - 1;
              while (num2 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num2])))
              {
                num2--;
              }
              this.runtextpos = runtextpos + num2 + 1;
              this.runtrackpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003ABB RID: 15035 RVA: 0x0045F39C File Offset: 0x0045F39C
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          num = this.runtrackpos;
          readOnlySpan = inputSpan.Slice(num);
          if (this.runtextpos < num)
          {
            this.runtextpos = num;
          }
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 123)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num6 = 1;
          while (num6 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num6])))
          {
            num6++;
          }
          readOnlySpan = readOnlySpan.Slice(num6);
          num += num6;
          int num7 = num;
          int num8 = num;
          int num9 = base.Crawlpos();
          if (readOnlySpan.IsEmpty || (*readOnlySpan[0] | 32) != 110)
          {
            goto IL_0176;
          }
          num5 = num;
          num4 = base.Crawlpos();
          if (readOnlySpan.Length >= 3 && readOnlySpan.Slice(1).StartsWith("ew", StringComparison.OrdinalIgnoreCase))
          {
            num3 = 0;
            num += 3;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_016F;
          }
          goto IL_010D;
          int num11;
          for (;;)
          {
            IL_04E6:
            base.Capture(1, num7, num);
            int num10 = 0;
            while (num10 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num10])))
            {
              num10++;
            }
            readOnlySpan = readOnlySpan.Slice(num10);
            num += num10;
            if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 125)
            {
              goto Block_42;
            }
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            switch (num11)
            {
            case 0:
              goto IL_0159;
            case 1:
              goto IL_01BE;
            case 2:
              goto IL_0229;
            case 3:
              goto IL_0271;
            case 4:
              goto IL_02B9;
            case 5:
              goto IL_0301;
            case 6:
              goto IL_03D5;
            case 7:
              goto IL_041D;
            case 8:
              goto IL_04DD;
            }
          }
          IL_0159:
          if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
          {
            base.CheckTimeout();
          }
          if (num3 == 0)
          {
            goto IL_010D;
          }
          if (num3 != 1)
          {
            goto IL_016F;
          }
          goto IL_0176;
          IL_04DD:
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_42:
          num++;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
          IL_010D:
          num = num5;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num4);
          if (readOnlySpan.Length < 5 || !readOnlySpan.Slice(1).StartsWith("eeds", StringComparison.OrdinalIgnoreCase))
          {
            goto IL_0176;
          }
          num3 = 1;
          num += 5;
          readOnlySpan = inputSpan.Slice(num);
          IL_016F:
          num11 = 0;
          goto IL_04E6;
          IL_0176:
          num = num8;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
          if (readOnlySpan.Length >= 4 && readOnlySpan.StartsWith("done", StringComparison.OrdinalIgnoreCase))
          {
            num11 = 1;
            num += 4;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_04E6;
          }
          IL_01BE:
          num = num8;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
          char c;
          if (readOnlySpan.Length >= 4 && readOnlySpan.StartsWith("pic", StringComparison.OrdinalIgnoreCase) && !((((c = (char)(*readOnlySpan[3])) | ' ') != 'k') & (c != 'K')))
          {
            num11 = 2;
            num += 4;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_04E6;
          }
          IL_0229:
          num = num8;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
          if (readOnlySpan.Length >= 3 && readOnlySpan.StartsWith("now", StringComparison.OrdinalIgnoreCase))
          {
            num11 = 3;
            num += 3;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_04E6;
          }
          IL_0271:
          num = num8;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
          if (readOnlySpan.Length >= 4 && readOnlySpan.StartsWith("gone", StringComparison.OrdinalIgnoreCase))
          {
            num11 = 4;
            num += 4;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_04E6;
          }
          IL_02B9:
          num = num8;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
          if (readOnlySpan.Length >= 3 && readOnlySpan.StartsWith("out", StringComparison.OrdinalIgnoreCase))
          {
            num11 = 5;
            num += 3;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_04E6;
          }
          IL_0301:
          num = num8;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
          if (readOnlySpan.Length >= 5 && readOnlySpan.StartsWith("ruled", StringComparison.OrdinalIgnoreCase) && readOnlySpan.Length >= 6)
          {
            char c2 = (char)(*readOnlySpan[5]);
            if (c2 != '-')
            {
              if (c2 != 'O' && c2 != 'o')
              {
                goto IL_03D5;
              }
              if (readOnlySpan.Length < 8 || !readOnlySpan.Slice(6).StartsWith("ut", StringComparison.OrdinalIgnoreCase))
              {
                goto IL_03D5;
              }
              num += 8;
              readOnlySpan = inputSpan.Slice(num);
            }
            else
            {
              if (readOnlySpan.Length < 9 || !readOnlySpan.Slice(6).StartsWith("out", StringComparison.OrdinalIgnoreCase))
              {
                goto IL_03D5;
              }
              num += 9;
              readOnlySpan = inputSpan.Slice(num);
            }
            num11 = 6;
            goto IL_04E6;
          }
          IL_03D5:
          num = num8;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
          if (readOnlySpan.Length >= 2 && readOnlySpan.StartsWith("no", StringComparison.OrdinalIgnoreCase))
          {
            num11 = 7;
            num += 2;
            readOnlySpan = inputSpan.Slice(num);
            goto IL_04E6;
          }
          IL_041D:
          num = num8;
          readOnlySpan = inputSpan.Slice(num);
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
          if (readOnlySpan.Length < 4 || (*readOnlySpan[0] | 32) != 115 || ((((c = (char)(*readOnlySpan[1])) | ' ') != 'k') & (c != 'K')) || !readOnlySpan.Slice(2).StartsWith("ip", StringComparison.OrdinalIgnoreCase))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num11 = 8;
          num += 4;
          readOnlySpan = inputSpan.Slice(num);
          goto IL_04E6;
        }

        // Token: 0x06003ABD RID: 15037 RVA: 0x0045F90C File Offset: 0x0045F90C
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
