using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000374 RID: 884
[CompilerGenerated]
internal sealed class <>z__ReadOnlySingleElementList<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
  // Token: 0x06002689 RID: 9865 RVA: 0x00640934 File Offset: 0x00640934
  public <>z__ReadOnlySingleElementList(T item)
  {
    this._item = item;
  }

  // Token: 0x0600268A RID: 9866 RVA: 0x00640944 File Offset: 0x00640944
  [return: Nullable(1)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
  }

  // Token: 0x170006E7 RID: 1767
  // (get) Token: 0x0600268B RID: 9867 RVA: 0x00640954 File Offset: 0x00640954
  int ICollection.Count
  {
    get
    {
      return 1;
    }
  }

  // Token: 0x170006E8 RID: 1768
  // (get) Token: 0x0600268C RID: 9868 RVA: 0x00640958 File Offset: 0x00640958
  bool ICollection.IsSynchronized
  {
    get
    {
      return false;
    }
  }

  // Token: 0x170006E9 RID: 1769
  // (get) Token: 0x0600268D RID: 9869 RVA: 0x0064095C File Offset: 0x0064095C
  object ICollection.SyncRoot
  {
    [return: Nullable(1)]
    get
    {
      return this;
    }
  }

  // Token: 0x0600268E RID: 9870 RVA: 0x00640960 File Offset: 0x00640960
  void ICollection.CopyTo([Nullable(1)] Array array, int index)
  {
    array.SetValue(this._item, index);
  }

  // Token: 0x170006EA RID: 1770
  object IList.this[int index]
  {
    [return: Nullable(2)]
    get
    {
      if (index != 0)
      {
        throw new IndexOutOfRangeException();
      }
      return this._item;
    }
    [param: Nullable(2)]
    set
    {
      throw new NotSupportedException();
    }
  }

  // Token: 0x170006EB RID: 1771
  // (get) Token: 0x06002691 RID: 9873 RVA: 0x00640994 File Offset: 0x00640994
  bool IList.IsFixedSize
  {
    get
    {
      return true;
    }
  }

  // Token: 0x170006EC RID: 1772
  // (get) Token: 0x06002692 RID: 9874 RVA: 0x00640998 File Offset: 0x00640998
  bool IList.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x06002693 RID: 9875 RVA: 0x0064099C File Offset: 0x0064099C
  int IList.Add([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002694 RID: 9876 RVA: 0x006409A4 File Offset: 0x006409A4
  void IList.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002695 RID: 9877 RVA: 0x006409AC File Offset: 0x006409AC
  bool IList.Contains([Nullable(2)] object value)
  {
    return EqualityComparer<T>.Default.Equals(this._item, (T)((object)value));
  }

  // Token: 0x06002696 RID: 9878 RVA: 0x006409C4 File Offset: 0x006409C4
  int IList.IndexOf([Nullable(2)] object value)
  {
    if (!EqualityComparer<T>.Default.Equals(this._item, (T)((object)value)))
    {
      return -1;
    }
    return 0;
  }

  // Token: 0x06002697 RID: 9879 RVA: 0x006409E4 File Offset: 0x006409E4
  void IList.Insert(int index, [Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002698 RID: 9880 RVA: 0x006409EC File Offset: 0x006409EC
  void IList.Remove([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06002699 RID: 9881 RVA: 0x006409F4 File Offset: 0x006409F4
  void IList.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600269A RID: 9882 RVA: 0x006409FC File Offset: 0x006409FC
  [return: Nullable(new byte[] { 1, 0 })]
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
  }

  // Token: 0x170006ED RID: 1773
  // (get) Token: 0x0600269B RID: 9883 RVA: 0x00640A0C File Offset: 0x00640A0C
  int IReadOnlyCollection<T>.Count
  {
    get
    {
      return 1;
    }
  }

  // Token: 0x170006EE RID: 1774
  T IReadOnlyList<T>.this[int index]
  {
    get
    {
      if (index != 0)
      {
        throw new IndexOutOfRangeException();
      }
      return this._item;
    }
  }

  // Token: 0x170006EF RID: 1775
  // (get) Token: 0x0600269D RID: 9885 RVA: 0x00640A24 File Offset: 0x00640A24
  int ICollection<T>.Count
  {
    get
    {
      return 1;
    }
  }

  // Token: 0x170006F0 RID: 1776
  // (get) Token: 0x0600269E RID: 9886 RVA: 0x00640A28 File Offset: 0x00640A28
  bool ICollection<T>.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x0600269F RID: 9887 RVA: 0x00640A2C File Offset: 0x00640A2C
  void ICollection<T>.Add(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060026A0 RID: 9888 RVA: 0x00640A34 File Offset: 0x00640A34
  void ICollection<T>.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x060026A1 RID: 9889 RVA: 0x00640A3C File Offset: 0x00640A3C
  bool ICollection<T>.Contains(T item)
  {
    return EqualityComparer<T>.Default.Equals(this._item, item);
  }

  // Token: 0x060026A2 RID: 9890 RVA: 0x00640A50 File Offset: 0x00640A50
  void ICollection<T>.CopyTo([Nullable(new byte[] { 1, 0 })] T[] array, int arrayIndex)
  {
    array[arrayIndex] = this._item;
  }

  // Token: 0x060026A3 RID: 9891 RVA: 0x00640A60 File Offset: 0x00640A60
  bool ICollection<T>.Remove(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x170006F1 RID: 1777
  T IList<T>.this[int index]
  {
    get
    {
      if (index != 0)
      {
        throw new IndexOutOfRangeException();
      }
      return this._item;
    }
    set
    {
      throw new NotSupportedException();
    }
  }

  // Token: 0x060026A6 RID: 9894 RVA: 0x00640A84 File Offset: 0x00640A84
  int IList<T>.IndexOf(T item)
  {
    if (!EqualityComparer<T>.Default.Equals(this._item, item))
    {
      return -1;
    }
    return 0;
  }

  // Token: 0x060026A7 RID: 9895 RVA: 0x00640A9C File Offset: 0x00640A9C
  void IList<T>.Insert(int index, T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060026A8 RID: 9896 RVA: 0x00640AA4 File Offset: 0x00640AA4
  void IList<T>.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x04001274 RID: 4724
  [CompilerGenerated]
  private readonly T _item;

  // Token: 0x02000AA8 RID: 2728
  private sealed class Enumerator : IDisposable, IEnumerator, IEnumerator<T>
  {
    // Token: 0x06004183 RID: 16771 RVA: 0x006C7E40 File Offset: 0x006C7E40
    public Enumerator(T item)
    {
      this.System.Collections.Generic.IEnumerator<T>.Current = item;
    }

    // Token: 0x1700087F RID: 2175
    // (get) Token: 0x06004184 RID: 16772 RVA: 0x006C7E50 File Offset: 0x006C7E50
    object IEnumerator.Current
    {
      get
      {
        return this._item;
      }
    }

    // Token: 0x17000880 RID: 2176
    // (get) Token: 0x06004185 RID: 16773 RVA: 0x006C7E60 File Offset: 0x006C7E60
    T IEnumerator<T>.Current
    {
      get
      {
        return this._item;
      }
    }

    // Token: 0x06004186 RID: 16774 RVA: 0x006C7E68 File Offset: 0x006C7E68
    bool IEnumerator.MoveNext()
    {
      return !this._moveNextCalled && (this._moveNextCalled = true);
    }

    // Token: 0x06004187 RID: 16775 RVA: 0x006C7E8C File Offset: 0x006C7E8C
    void IEnumerator.Reset()
    {
      this._moveNextCalled = false;
    }

    // Token: 0x06004188 RID: 16776 RVA: 0x006C7E98 File Offset: 0x006C7E98
    void IDisposable.Dispose()
    {
    }

    // Token: 0x04002DBC RID: 11708
    [CompilerGenerated]
    private readonly T _item;

    // Token: 0x04002DBD RID: 11709
    [CompilerGenerated]
    private bool _moveNextCalled;
  }
}
