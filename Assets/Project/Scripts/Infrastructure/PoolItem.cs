public struct PoolItem<T>
{
    public readonly T Value;

    public PoolItem(T value)
    {
        Value = value;
        IsBusy = false;
    }

    public bool IsBusy { get; internal set; }
}
