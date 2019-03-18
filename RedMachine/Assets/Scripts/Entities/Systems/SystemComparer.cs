using System.Collections.Generic;

public class SystemComparer<T> : IComparer<T>
    where T : ISystem
{
    public int Compare(T sys1, T sys2)
    {
        return sys1.GetOrder().CompareTo(sys2.GetOrder());
    }
}