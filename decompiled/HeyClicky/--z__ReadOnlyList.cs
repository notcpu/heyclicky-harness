using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000373 RID: 883
[CompilerGenerated]
internal sealed class <>z__ReadOnlyList<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
  // Token: 0x06002669 RID: 9833 RVA: 0x006407CC File Offset: 0x006407CC
  public <>z__ReadOnlyList(List<T> items)
  {
    this._items = items;
  }

  // Token: 0x0600266A RID: 9834 RVA: 0x006407DC File Offset: 0x006407DC
  [return: Nullable(1)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return ((IEnumerable)this._items).GetEnumerator();
  }

  // Token: 0x170006DC RID: 1756
  // (get) Token: 0x0600266B RID: 9835 RVA: 0x006407EC File Offset: 0x006407EC
  int ICollection.Count
  {
    get
    {
      return this._items.Count;
    }
  }

  // Token: 0x170006DD RID: 1757
  // (get) Token: 0x0600266C RID: 9836 RVA: 0x006407FC File Offset: 0x006407FC
  bool ICollection.IsSynchronized
  {
    get
    {
      return false;
    }
  }

  // Token: 0x170006DE RID: 1758
  // (get) Token: 0x0600266D RID: 9837 RVA: 0x00640800 File Offset: 0x00640800
  object ICollection.SyncRoot
  {
    [return: Nullable(1)]
    get
    {
      return this;
    }
  }

  // Token: 0x0600266E RID: 9838 RVA: 0x00640804 File Offset: 0x00640804
  void ICollection.CopyTo([Nullable(1)] Array array, int index)
  {
    ((ICollection)this._items).CopyTo(array, index);
  }

  // Token: 0x170006DF RID: 1759
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

  // Token: 0x170006E0 RID: 1760
  // (get) Token: 0x06002671 RID: 9841 RVA: 0x00640830 File Offset: 0x00640830
  bool IList.IsFixedSize
  {
    get
    {
      return true;
    }
  }

  // Token: 0x170006E1 RID: 1761
  // (get) Token: 0x06002672 RID: 9842 RVA: 0x00640834 File Offset: 0x00640834
  bool IList.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x06002673 RID: 9843 RVA: 0x00640838 File Offset: 0x00640838
  int IList.Add([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002674 RID: 9844 RVA: 0x00640840 File Offset: 0x00640840
  void IList.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002675 RID: 9845 RVA: 0x00640848 File Offset: 0x00640848
  bool IList.Contains([Nullable(2)] object value)
  {
    return ((IList)this._items).Contains(value);
  }

  // Token: 0x06002676 RID: 9846 RVA: 0x00640858 File Offset: 0x00640858
  int IList.IndexOf([Nullable(2)] object value)
  {
    return ((IList)this._items).IndexOf(value);
  }

  // Token: 0x06002677 RID: 9847 RVA: 0x00640868 File Offset: 0x00640868
  void IList.Insert(int index, [Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002678 RID: 9848 RVA: 0x00640870 File Offset: 0x00640870
  void IList.Remove([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002679 RID: 9849 RVA: 0x00640878 File Offset: 0x00640878
  void IList.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600267A RID: 9850 RVA: 0x00640880 File Offset: 0x00640880
  [return: Nullable(new byte[] { 1, 0 })]
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    return ((IEnumerable<T>)this._items).GetEnumerator();
  }

  // Token: 0x170006E2 RID: 1762
  // (get) Token: 0x0600267B RID: 9851 RVA: 0x00640890 File Offset: 0x00640890
  int IReadOnlyCollection<T>.Count
  {
    get
    {
      return this._items.Count;
    }
  }

  // Token: 0x170006E3 RID: 1763
  T IReadOnlyList<T>.this[int index]
  {
    get
    {
      return this._items[index];
    }
  }

  // Token: 0x170006E4 RID: 1764
  // (get) Token: 0x0600267D RID: 9853 RVA: 0x006408B0 File Offset: 0x006408B0
  int ICollection<T>.Count
  {
    get
    {
      return this._items.Count;
    }
  }

  // Token: 0x170006E5 RID: 1765
  // (get) Token: 0x0600267E RID: 9854 RVA: 0x006408C0 File Offset: 0x006408C0
  bool ICollection<T>.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x0600267F RID: 9855 RVA: 0x006408C4 File Offset: 0x006408C4
  void ICollection<T>.Add(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002680 RID: 9856 RVA: 0x006408CC File Offset: 0x006408CC
  void ICollection<T>.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002681 RID: 9857 RVA: 0x006408D4 File Offset: 0x006408D4
  bool ICollection<T>.Contains(T item)
  {
    return this._items.Contains(item);
  }

  // Token: 0x06002682 RID: 9858 RVA: 0x006408E4 File Offset: 0x006408E4
  void ICollection<T>.CopyTo([Nullable(new byte[] { 1, 0 })] T[] array, int arrayIndex)
  {
    this._items.CopyTo(array, arrayIndex);
  }

  // Token: 0x06002683 RID: 9859 RVA: 0x006408F4 File Offset: 0x006408F4
  bool ICollection<T>.Remove(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x170006E6 RID: 1766
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

  // Token: 0x06002686 RID: 9862 RVA: 0x00640914 File Offset: 0x00640914
  int IList<T>.IndexOf(T item)
  {
    return this._items.IndexOf(item);
  }

  // Token: 0x06002687 RID: 9863 RVA: 0x00640924 File Offset: 0x00640924
  void IList<T>.Insert(int index, T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002688 RID: 9864 RVA: 0x0064092C File Offset: 0x0064092C
  void IList<T>.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x04001273 RID: 4723
  [CompilerGenerated]
  private readonly List<T> _items;
}
