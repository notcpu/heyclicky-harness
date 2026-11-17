using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000372 RID: 882
[CompilerGenerated]
internal sealed class <>z__ReadOnlyArray<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
  // Token: 0x06002649 RID: 9801 RVA: 0x00640670 File Offset: 0x00640670
  public <>z__ReadOnlyArray(T[] items)
  {
    this._items = items;
  }

  // Token: 0x0600264A RID: 9802 RVA: 0x00640680 File Offset: 0x00640680
  [return: Nullable(1)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return this._items.GetEnumerator();
  }

  // Token: 0x170006D1 RID: 1745
  // (get) Token: 0x0600264B RID: 9803 RVA: 0x00640690 File Offset: 0x00640690
  int ICollection.Count
  {
    get
    {
      return this._items.Length;
    }
  }

  // Token: 0x170006D2 RID: 1746
  // (get) Token: 0x0600264C RID: 9804 RVA: 0x0064069C File Offset: 0x0064069C
  bool ICollection.IsSynchronized
  {
    get
    {
      return false;
    }
  }

  // Token: 0x170006D3 RID: 1747
  // (get) Token: 0x0600264D RID: 9805 RVA: 0x006406A0 File Offset: 0x006406A0
  object ICollection.SyncRoot
  {
    [return: Nullable(1)]
    get
    {
      return this;
    }
  }

  // Token: 0x0600264E RID: 9806 RVA: 0x006406A4 File Offset: 0x006406A4
  void ICollection.CopyTo([Nullable(1)] Array array, int index)
  {
    this._items.CopyTo(array, index);
  }

  // Token: 0x170006D4 RID: 1748
  object IList.this[int index]
  {
    [return: Nullable(2)]
    get
    {
      return this._items[index];
    }
    [param: Nullable(2)]
    set
    {
      throw new NotSupportedException();
    }
  }

  // Token: 0x170006D5 RID: 1749
  // (get) Token: 0x06002651 RID: 9809 RVA: 0x006406D0 File Offset: 0x006406D0
  bool IList.IsFixedSize
  {
    get
    {
      return true;
    }
  }

  // Token: 0x170006D6 RID: 1750
  // (get) Token: 0x06002652 RID: 9810 RVA: 0x006406D4 File Offset: 0x006406D4
  bool IList.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x06002653 RID: 9811 RVA: 0x006406D8 File Offset: 0x006406D8
  int IList.Add([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002654 RID: 9812 RVA: 0x006406E0 File Offset: 0x006406E0
  void IList.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002655 RID: 9813 RVA: 0x006406E8 File Offset: 0x006406E8
  bool IList.Contains([Nullable(2)] object value)
  {
    return this._items.Contains(value);
  }

  // Token: 0x06002656 RID: 9814 RVA: 0x006406F8 File Offset: 0x006406F8
  int IList.IndexOf([Nullable(2)] object value)
  {
    return this._items.IndexOf(value);
  }

  // Token: 0x06002657 RID: 9815 RVA: 0x00640708 File Offset: 0x00640708
  void IList.Insert(int index, [Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002658 RID: 9816 RVA: 0x00640710 File Offset: 0x00640710
  void IList.Remove([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002659 RID: 9817 RVA: 0x00640718 File Offset: 0x00640718
  void IList.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600265A RID: 9818 RVA: 0x00640720 File Offset: 0x00640720
  [return: Nullable(new byte[] { 1, 0 })]
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    return this._items.GetEnumerator();
  }

  // Token: 0x170006D7 RID: 1751
  // (get) Token: 0x0600265B RID: 9819 RVA: 0x00640730 File Offset: 0x00640730
  int IReadOnlyCollection<T>.Count
  {
    get
    {
      return this._items.Length;
    }
  }

  // Token: 0x170006D8 RID: 1752
  T IReadOnlyList<T>.this[int index]
  {
    get
    {
      return this._items[index];
    }
  }

  // Token: 0x170006D9 RID: 1753
  // (get) Token: 0x0600265D RID: 9821 RVA: 0x0064074C File Offset: 0x0064074C
  int ICollection<T>.Count
  {
    get
    {
      return this._items.Length;
    }
  }

  // Token: 0x170006DA RID: 1754
  // (get) Token: 0x0600265E RID: 9822 RVA: 0x00640758 File Offset: 0x00640758
  bool ICollection<T>.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x0600265F RID: 9823 RVA: 0x0064075C File Offset: 0x0064075C
  void ICollection<T>.Add(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002660 RID: 9824 RVA: 0x00640764 File Offset: 0x00640764
  void ICollection<T>.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002661 RID: 9825 RVA: 0x0064076C File Offset: 0x0064076C
  bool ICollection<T>.Contains(T item)
  {
    return this._items.Contains(item);
  }

  // Token: 0x06002662 RID: 9826 RVA: 0x0064077C File Offset: 0x0064077C
  void ICollection<T>.CopyTo([Nullable(new byte[] { 1, 0 })] T[] array, int arrayIndex)
  {
    this._items.CopyTo(array, arrayIndex);
  }

  // Token: 0x06002663 RID: 9827 RVA: 0x0064078C File Offset: 0x0064078C
  bool ICollection<T>.Remove(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x170006DB RID: 1755
  T IList<T>.this[int index]
  {
    get
    {
      return this._items[index];
    }
    set
    {
      throw new NotSupportedException();
    }
  }

  // Token: 0x06002666 RID: 9830 RVA: 0x006407AC File Offset: 0x006407AC
  int IList<T>.IndexOf(T item)
  {
    return this._items.IndexOf(item);
  }

  // Token: 0x06002667 RID: 9831 RVA: 0x006407BC File Offset: 0x006407BC
  void IList<T>.Insert(int index, T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002668 RID: 9832 RVA: 0x006407C4 File Offset: 0x006407C4
  void IList<T>.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x04001272 RID: 4722
  [CompilerGenerated]
  private readonly T[] _items;
}
