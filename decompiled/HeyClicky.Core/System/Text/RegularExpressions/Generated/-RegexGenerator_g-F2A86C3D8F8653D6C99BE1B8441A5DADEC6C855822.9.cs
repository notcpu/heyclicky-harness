using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200000A RID: 10
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtBlockEnd_8 : Regex
  {
    // Token: 0x06000011 RID: 17 RVA: 0x003316B4 File Offset: 0x003316B4
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtBlockEnd_8()
    {
      this.pattern = "</text:(p|h)>|<text:line-break\\s*/>";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtBlockEnd_8.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000009 RID: 9
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtBlockEnd_8 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtBlockEnd_8();

    // Token: 0x0200023D RID: 573
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002104 RID: 8452 RVA: 0x003F6D20 File Offset: 0x003F6D20
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtBlockEnd_8.RunnerFactory.Runner();
      }

      // Token: 0x020006A7 RID: 1703
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A2D RID: 14893 RVA: 0x0045B1BC File Offset: 0x0045B1BC
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

        // Token: 0x06003A2E RID: 14894 RVA: 0x0045B1FC File Offset: 0x0045B1FC
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 9)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            for (int i = 0; i < readOnlySpan.Length - 8; i++)
            {
              int num = readOnlySpan.Slice(i).IndexOf('<');
              if (num < 0)
              {
                break;
              }
              i += num;
              if (i + 8 >= readOnlySpan.Length)
              {
                break;
              }
              char c;
              if ((((c = (char)(*readOnlySpan[i + 6])) == ':') | (c == 'l')) && (((c = (char)(*readOnlySpan[i + 8])) == '>') | (c == 'n')))
              {
                this.runtextpos = runtextpos + i;
                return true;
              }
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A2F RID: 14895 RVA: 0x0045B2B0 File Offset: 0x0045B2B0
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
          if (readOnlySpan.Length < 2)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          char c = (char)(*readOnlySpan[1]);
          if (c != '/')
          {
            if (c != 't')
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            if (!readOnlySpan.Slice(2).StartsWith<char>("ext:line-break"))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            int num3 = 16;
            while (num3 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num3])))
            {
              num3++;
            }
            readOnlySpan = readOnlySpan.Slice(num3);
            num += num3;
            if (!readOnlySpan.StartsWith<char>("/>"))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 2;
            readOnlySpan = inputSpan.Slice(num);
          }
          else
          {
            if (!readOnlySpan.Slice(2).StartsWith<char>("text:"))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num += 7;
            readOnlySpan = inputSpan.Slice(num);
            int num4 = num;
            char c2;
            if (readOnlySpan.IsEmpty || (((c2 = (char)(*readOnlySpan[0])) != 'h') & (c2 != 'p')))
            {
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
              return false;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
            base.Capture(1, num4, num);
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

        // Token: 0x06003A31 RID: 14897 RVA: 0x0045B478 File Offset: 0x0045B478
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
