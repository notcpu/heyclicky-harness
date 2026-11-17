using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.App.Interop
{
  // Token: 0x02000198 RID: 408
  [NullableContext(1)]
  [Nullable(0)]
  public static class FolderDialog
  {
    // Token: 0x06001606 RID: 5638 RVA: 0x005B9E98 File Offset: 0x005B9E98
    [return: Nullable(2)]
    public static string Pick(IntPtr owner, string title, string okLabel, [Nullable(2)] string startFolder, bool folders = true)
    {
      FolderDialog.IFileOpenDialog fileOpenDialog = null;
      string text;
      try
      {
        fileOpenDialog = (FolderDialog.IFileOpenDialog)new FolderDialog.FileOpenDialogCoClass();
        uint num;
        fileOpenDialog.GetOptions(out num);
        fileOpenDialog.SetOptions(num | (folders ? 32U : 4096U) | 64U | 2048U | 8U);
        fileOpenDialog.SetTitle(title);
        fileOpenDialog.SetOkButtonLabel(okLabel);
        FolderDialog.IShellItem shellItem;
        if (startFolder != null && Directory.Exists(startFolder) && FolderDialog.SHCreateItemFromParsingName(startFolder, (IntPtr)0, typeof(FolderDialog.IShellItem).GUID, out shellItem) == 0 && shellItem != null)
        {
          fileOpenDialog.SetFolder(shellItem);
          Marshal.ReleaseComObject(shellItem);
        }
        if (fileOpenDialog.Show(owner) != 0)
        {
          text = null;
        }
        else
        {
          FolderDialog.IShellItem shellItem2;
          fileOpenDialog.GetResult(out shellItem2);
          IntPtr intPtr;
          shellItem2.GetDisplayName(2147844096U, out intPtr);
          try
          {
            text = Marshal.PtrToStringUni(intPtr);
          }
          finally
          {
            Marshal.FreeCoTaskMem(intPtr);
            Marshal.ReleaseComObject(shellItem2);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("folder-dialog", ex.Message);
        text = null;
      }
      finally
      {
        if (fileOpenDialog != null)
        {
          Marshal.ReleaseComObject(fileOpenDialog);
        }
      }
      return text;
    }

    // Token: 0x06001607 RID: 5639
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern int SHCreateItemFromParsingName(string path, IntPtr bindContext, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, [Nullable(2)] out FolderDialog.IShellItem item);

    // Token: 0x04000AB1 RID: 2737
    private const uint FOS_NOCHANGEDIR = 8U;

    // Token: 0x04000AB2 RID: 2738
    private const uint FOS_PICKFOLDERS = 32U;

    // Token: 0x04000AB3 RID: 2739
    private const uint FOS_FORCEFILESYSTEM = 64U;

    // Token: 0x04000AB4 RID: 2740
    private const uint FOS_PATHMUSTEXIST = 2048U;

    // Token: 0x04000AB5 RID: 2741
    private const uint FOS_FILEMUSTEXIST = 4096U;

    // Token: 0x04000AB6 RID: 2742
    private const uint SIGDN_FILESYSPATH = 2147844096U;

    // Token: 0x0200073E RID: 1854
    [NullableContext(0)]
    [Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComImport]
    private class FileOpenDialogCoClass
    {
      // Token: 0x060034A2 RID: 13474
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
      public extern FileOpenDialogCoClass();
    }

    // Token: 0x0200073F RID: 1855
    [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    private interface IShellItem
    {
      // Token: 0x060034A3 RID: 13475
      void BindToHandler(IntPtr pbc, [MarshalAs(UnmanagedType.LPStruct)] Guid bhid, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr ppv);

      // Token: 0x060034A4 RID: 13476
      void GetParent(out FolderDialog.IShellItem ppsi);

      // Token: 0x060034A5 RID: 13477
      void GetDisplayName(uint sigdnName, out IntPtr ppszName);

      // Token: 0x060034A6 RID: 13478
      void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);

      // Token: 0x060034A7 RID: 13479
      void Compare(FolderDialog.IShellItem psi, uint hint, out int piOrder);
    }

    // Token: 0x02000740 RID: 1856
    [Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    private interface IFileOpenDialog
    {
      // Token: 0x060034A8 RID: 13480
      [PreserveSig]
      int Show(IntPtr parent);

      // Token: 0x060034A9 RID: 13481
      void SetFileTypes(uint cFileTypes, IntPtr rgFilterSpec);

      // Token: 0x060034AA RID: 13482
      void SetFileTypeIndex(uint iFileType);

      // Token: 0x060034AB RID: 13483
      void GetFileTypeIndex(out uint piFileType);

      // Token: 0x060034AC RID: 13484
      void Advise(IntPtr pfde, out uint pdwCookie);

      // Token: 0x060034AD RID: 13485
      void Unadvise(uint dwCookie);

      // Token: 0x060034AE RID: 13486
      void SetOptions(uint fos);

      // Token: 0x060034AF RID: 13487
      void GetOptions(out uint pfos);

      // Token: 0x060034B0 RID: 13488
      void SetDefaultFolder(FolderDialog.IShellItem psi);

      // Token: 0x060034B1 RID: 13489
      void SetFolder(FolderDialog.IShellItem psi);

      // Token: 0x060034B2 RID: 13490
      void GetFolder(out FolderDialog.IShellItem ppsi);

      // Token: 0x060034B3 RID: 13491
      void GetCurrentSelection(out FolderDialog.IShellItem ppsi);

      // Token: 0x060034B4 RID: 13492
      void SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);

      // Token: 0x060034B5 RID: 13493
      void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

      // Token: 0x060034B6 RID: 13494
      void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

      // Token: 0x060034B7 RID: 13495
      void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);

      // Token: 0x060034B8 RID: 13496
      void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

      // Token: 0x060034B9 RID: 13497
      void GetResult(out FolderDialog.IShellItem ppsi);

      // Token: 0x060034BA RID: 13498
      void AddPlace(FolderDialog.IShellItem psi, int fdap);

      // Token: 0x060034BB RID: 13499
      void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

      // Token: 0x060034BC RID: 13500
      void Close(int hr);

      // Token: 0x060034BD RID: 13501
      void SetClientGuid([MarshalAs(UnmanagedType.LPStruct)] Guid guid);

      // Token: 0x060034BE RID: 13502
      void ClearClientData();

      // Token: 0x060034BF RID: 13503
      void SetFilter(IntPtr pFilter);

      // Token: 0x060034C0 RID: 13504
      void GetResults(out IntPtr ppenum);

      // Token: 0x060034C1 RID: 13505
      void GetSelectedItems(out IntPtr ppsai);
    }
  }
}
