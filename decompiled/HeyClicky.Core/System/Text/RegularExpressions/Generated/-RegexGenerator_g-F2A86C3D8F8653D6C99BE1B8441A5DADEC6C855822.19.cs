using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000014 RID: 20
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StandaloneImageRegex_18 : Regex
  {
    // Token: 0x06000025 RID: 37 RVA: 0x00331A34 File Offset: 0x00331A34
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StandaloneImageRegex_18()
    {
      this.pattern = "^!\\[([^\\]]*)\\]\\(\\s*(\\S+?)(?:\\s+\"[^\"]*\")?\\s*\\)$";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StandaloneImageRegex_18.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x04000013 RID: 19
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StandaloneImageRegex_18 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StandaloneImageRegex_18();

    // Token: 0x02000247 RID: 583
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002118 RID: 8472 RVA: 0x003F6DC0 File Offset: 0x003F6DC0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StandaloneImageRegex_18.RunnerFactory.Runner();
      }

      // Token: 0x020006B1 RID: 1713
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A59 RID: 14937 RVA: 0x0045C2E0 File Offset: 0x0045C2E0
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan))
          {
            this.runtextpos = inputSpan.Length;
          }
        }

        // Token: 0x06003A5A RID: 14938 RVA: 0x0045C304 File Offset: 0x0045C304
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 6 && runtextpos == 0)
          {
            return true;
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A5B RID: 14939 RVA: 0x0045C338 File Offset: 0x0045C338
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          int num3 = 0;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (num != 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          if (!readOnlySpan.StartsWith<char>("!["))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num4 = num;
          int num5 = readOnlySpan.IndexOf(']');
          if (num5 < 0)
          {
            num5 = readOnlySpan.Length;
          }
          readOnlySpan = readOnlySpan.Slice(num5);
          num += num5;
          base.Capture(1, num4, num);
          if (!readOnlySpan.StartsWith<char>("]("))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num6 = num;
          int num7 = 0;
          while (num7 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num7])))
          {
            num7++;
          }
          readOnlySpan = readOnlySpan.Slice(num7);
          num += num7;
          int num8 = num;
          for (;;)
          {
            int num9 = base.Crawlpos();
            int num10 = num;
            if (!readOnlySpan.IsEmpty && !char.IsWhiteSpace((char)(*readOnlySpan[0])))
            {
              num++;
              readOnlySpan = inputSpan.Slice(num);
              int num11 = num;
              for (;;)
              {
                int num12 = base.Crawlpos();
                base.Capture(2, num10, num);
                int num13 = 0;
                for (;;)
                {
                  <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.StackPush(ref this.runstack, ref num3, base.Crawlpos(), num);
                  num13++;
                  int num14 = 0;
                  while (num14 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num14])))
                  {
                    num14++;
                  }
                  if (num14 == 0)
                  {
                    break;
                  }
                  readOnlySpan = readOnlySpan.Slice(num14);
                  num += num14;
                  if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 34)
                  {
                    break;
                  }
                  int num15 = readOnlySpan.Slice(1).IndexOf('"');
                  if (num15 < 0)
                  {
                    num15 = readOnlySpan.Length - 1;
                  }
                  readOnlySpan = readOnlySpan.Slice(num15);
                  num += num15;
                  if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 34)
                  {
                    break;
                  }
                  num += 2;
                  readOnlySpan = inputSpan.Slice(num);
                  if (num13 != 0)
                  {
                    goto IL_02D8;
                  }
                }
                IL_029E:
                if (--num13 >= 0)
                {
                  num = this.runstack[--num3];
                  this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num3]);
                  readOnlySpan = inputSpan.Slice(num);
                }
                else
                {
                  this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num12);
                  if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
                  {
                    base.CheckTimeout();
                  }
                  num = num11;
                  readOnlySpan = inputSpan.Slice(num);
                  if (!readOnlySpan.IsEmpty && !char.IsWhiteSpace((char)(*readOnlySpan[0])))
                  {
                    num++;
                    readOnlySpan = inputSpan.Slice(num);
                    num11 = num;
                    continue;
                  }
                  break;
                }
                IL_02D8:
                int num16 = 0;
                while (num16 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num16])))
                {
                  num16++;
                }
                readOnlySpan = readOnlySpan.Slice(num16);
                num += num16;
                if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 41 && 2 >= readOnlySpan.Length && (1 >= readOnlySpan.Length || *readOnlySpan[1] == 10))
                {
                  goto IL_034F;
                }
                goto IL_029E;
              }
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num9);
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num6 >= num8)
            {
              break;
            }
            num8 = (num = num8 - 1);
            readOnlySpan = inputSpan.Slice(num);
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_034F:
          num++;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06003A5D RID: 14941 RVA: 0x0045C6B4 File Offset: 0x0045C6B4
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
