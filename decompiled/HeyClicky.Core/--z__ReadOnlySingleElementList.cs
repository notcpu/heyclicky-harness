using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000234 RID: 564
[CompilerGenerated]
internal sealed class <>z__ReadOnlySingleElementList<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
  // Token: 0x060020D4 RID: 8404 RVA: 0x003F6B28 File Offset: 0x003F6B28
  public <>z__ReadOnlySingleElementList(T item)
  {
    this._item = item;
  }

  // Token: 0x060020D5 RID: 8405 RVA: 0x003F6B38 File Offset: 0x003F6B38
  [return: Nullable(1)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
  }

  // Token: 0x17000766 RID: 1894
  // (get) Token: 0x060020D6 RID: 8406 RVA: 0x003F6B48 File Offset: 0x003F6B48
  int ICollection.Count
  {
    get
    {
      return 1;
    }
  }

  // Token: 0x17000767 RID: 1895
  // (get) Token: 0x060020D7 RID: 8407 RVA: 0x003F6B4C File Offset: 0x003F6B4C
  bool ICollection.IsSynchronized
  {
    get
    {
      return false;
    }
  }

  // Token: 0x17000768 RID: 1896
  // (get) Token: 0x060020D8 RID: 8408 RVA: 0x003F6B50 File Offset: 0x003F6B50
  object ICollection.SyncRoot
  {
    [return: Nullable(1)]
    get
    {
      return this;
    }
  }

  // Token: 0x060020D9 RID: 8409 RVA: 0x003F6B54 File Offset: 0x003F6B54
  void ICollection.CopyTo([Nullable(1)] Array array, int index)
  {
    array.SetValue(this._item, index);
  }

  // Token: 0x17000769 RID: 1897
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

  // Token: 0x1700076A RID: 1898
  // (get) Token: 0x060020DC RID: 8412 RVA: 0x003F6B88 File Offset: 0x003F6B88
  bool IList.IsFixedSize
  {
    get
    {
      return true;
    }
  }

  // Token: 0x1700076B RID: 1899
  // (get) Token: 0x060020DD RID: 8413 RVA: 0x003F6B8C File Offset: 0x003F6B8C
  bool IList.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x060020DE RID: 8414 RVA: 0x003F6B90 File Offset: 0x003F6B90
  int IList.Add([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020DF RID: 8415 RVA: 0x003F6B98 File Offset: 0x003F6B98
  void IList.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020E0 RID: 8416 RVA: 0x003F6BA0 File Offset: 0x003F6BA0
  bool IList.Contains([Nullable(2)] object value)
  {
    return EqualityComparer<T>.Default.Equals(this._item, (T)((object)value));
  }

  // Token: 0x060020E1 RID: 8417 RVA: 0x003F6BB8 File Offset: 0x003F6BB8
  int IList.IndexOf([Nullable(2)] object value)
  {
    if (!EqualityComparer<T>.Default.Equals(this._item, (T)((object)value)))
    {
      return -1;
    }
    return 0;
  }

  // Token: 0x060020E2 RID: 8418 RVA: 0x003F6BD8 File Offset: 0x003F6BD8
  void IList.Insert(int index, [Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020E3 RID: 8419 RVA: 0x003F6BE0 File Offset: 0x003F6BE0
  void IList.Remove([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020E4 RID: 8420 RVA: 0x003F6BE8 File Offset: 0x003F6BE8
  void IList.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020E5 RID: 8421 RVA: 0x003F6BF0 File Offset: 0x003F6BF0
  [return: Nullable(new byte[] { 1, 0 })]
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
  }

  // Token: 0x1700076C RID: 1900
  // (get) Token: 0x060020E6 RID: 8422 RVA: 0x003F6C00 File Offset: 0x003F6C00
  int IReadOnlyCollection<T>.Count
  {
    get
    {
      return 1;
    }
  }

  // Token: 0x1700076D RID: 1901
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

  // Token: 0x1700076E RID: 1902
  // (get) Token: 0x060020E8 RID: 8424 RVA: 0x003F6C18 File Offset: 0x003F6C18
  int ICollection<T>.Count
  {
    get
    {
      return 1;
    }
  }

  // Token: 0x1700076F RID: 1903
  // (get) Token: 0x060020E9 RID: 8425 RVA: 0x003F6C1C File Offset: 0x003F6C1C
  bool ICollection<T>.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x060020EA RID: 8426 RVA: 0x003F6C20 File Offset: 0x003F6C20
  void ICollection<T>.Add(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020EB RID: 8427 RVA: 0x003F6C28 File Offset: 0x003F6C28
  void ICollection<T>.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020EC RID: 8428 RVA: 0x003F6C30 File Offset: 0x003F6C30
  bool ICollection<T>.Contains(T item)
  {
    return EqualityComparer<T>.Default.Equals(this._item, item);
  }

  // Token: 0x060020ED RID: 8429 RVA: 0x003F6C44 File Offset: 0x003F6C44
  void ICollection<T>.CopyTo([Nullable(new byte[] { 1, 0 })] T[] array, int arrayIndex)
  {
    array[arrayIndex] = this._item;
  }

  // Token: 0x060020EE RID: 8430 RVA: 0x003F6C54 File Offset: 0x003F6C54
  bool ICollection<T>.Remove(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x17000770 RID: 1904
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

  // Token: 0x060020F1 RID: 8433 RVA: 0x003F6C78 File Offset: 0x003F6C78
  int IList<T>.IndexOf(T item)
  {
    if (!EqualityComparer<T>.Default.Equals(this._item, item))
    {
      return -1;
    }
    return 0;
  }

  // Token: 0x060020F2 RID: 8434 RVA: 0x003F6C90 File Offset: 0x003F6C90
  void IList<T>.Insert(int index, T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020F3 RID: 8435 RVA: 0x003F6C98 File Offset: 0x003F6C98
  void IList<T>.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x04000DF8 RID: 3576
  [CompilerGenerated]
  private readonly T _item;

  // Token: 0x0200069E RID: 1694
  private sealed class Enumerator : IDisposable, IEnumerator, IEnumerator<T>
  {
    // Token: 0x06003A04 RID: 14852 RVA: 0x0045A0AC File Offset: 0x0045A0AC
    public Enumerator(T item)
    {
      this.System.Collections.Generic.IEnumerator<T>.Current = item;
    }

    // Token: 0x170009F5 RID: 2549
    // (get) Token: 0x06003A05 RID: 14853 RVA: 0x0045A0BC File Offset: 0x0045A0BC
    object IEnumerator.Current
    {
      get
      {
        return this._item;
      }
    }

    // Token: 0x170009F6 RID: 2550
    // (get) Token: 0x06003A06 RID: 14854 RVA: 0x0045A0CC File Offset: 0x0045A0CC
    T IEnumerator<T>.Current
    {
      get
      {
        return this._item;
      }
    }

    // Token: 0x06003A07 RID: 14855 RVA: 0x0045A0D4 File Offset: 0x0045A0D4
    bool IEnumerator.MoveNext()
    {
      return !this._moveNextCalled && (this._moveNextCalled = true);
    }

    // Token: 0x06003A08 RID: 14856 RVA: 0x0045A0F8 File Offset: 0x0045A0F8
    void IEnumerator.Reset()
    {
      this._moveNextCalled = false;
    }

    // Token: 0x06003A09 RID: 14857 RVA: 0x0045A104 File Offset: 0x0045A104
    void IDisposable.Dispose()
    {
    }

    // Token: 0x040025B0 RID: 9648
    [CompilerGenerated]
    private readonly T _item;

    // Token: 0x040025B1 RID: 9649
    [CompilerGenerated]
    private bool _moveNextCalled;
  }
}
