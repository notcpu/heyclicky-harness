using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000019 RID: 25
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__EncryptReference_23 : Regex
  {
    // Token: 0x0600002F RID: 47 RVA: 0x00331BEC File Offset: 0x00331BEC
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__EncryptReference_23()
    {
      this.pattern = "/Encrypt\\s+(\\d+\\s+\\d+\\s+R|<<)";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__EncryptReference_23.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000018 RID: 24
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__EncryptReference_23 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__EncryptReference_23();

    // Token: 0x0200024C RID: 588
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002122 RID: 8482 RVA: 0x003F6E10 File Offset: 0x003F6E10
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__EncryptReference_23.RunnerFactory.Runner();
      }

      // Token: 0x020006B6 RID: 1718
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A72 RID: 14962 RVA: 0x0045D010 File Offset: 0x0045D010
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

        // Token: 0x06003A73 RID: 14963 RVA: 0x0045D050 File Offset: 0x0045D050
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 11)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_6EA455EE8B5DA4F1D7845D265904B991B56C71CC454D10190DEE48BAD102773D);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A74 RID: 14964 RVA: 0x0045D0A4 File Offset: 0x0045D0A4
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("/Encrypt"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 8;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = 0;
          while (num3 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num3])))
          {
            num3++;
          }
          if (num3 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          int num4 = num;
          int num5 = num;
          int num6 = 0;
          while (num6 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num6])))
          {
            num6++;
          }
          if (num6 != 0)
          {
            readOnlySpan = readOnlySpan.Slice(num6);
            num += num6;
            int num7 = 0;
            while (num7 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num7])))
            {
              num7++;
            }
            if (num7 != 0)
            {
              readOnlySpan = readOnlySpan.Slice(num7);
              num += num7;
              int num8 = 0;
              while (num8 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num8])))
              {
                num8++;
              }
              if (num8 != 0)
              {
                readOnlySpan = readOnlySpan.Slice(num8);
                num += num8;
                int num9 = 0;
                while (num9 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num9])))
                {
                  num9++;
                }
                if (num9 != 0)
                {
                  readOnlySpan = readOnlySpan.Slice(num9);
                  num += num9;
                  if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 82)
                  {
                    num++;
                    readOnlySpan = inputSpan.Slice(num);
                    goto IL_01CB;
                  }
                }
              }
            }
          }
          num = num5;
          readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("<<"))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          IL_01CB:
          base.Capture(1, num4, num);
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A76 RID: 14966 RVA: 0x0045D2A0 File Offset: 0x0045D2A0
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
