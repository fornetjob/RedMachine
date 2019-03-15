using UnityEngine;

public static class ViewBaseHelper
{
    public static TView AddView<TView>(this GameObject obj)
        where TView : ViewBase
    {
        var view = obj.AddComponent<TView>();

        view.BeginView();

        return view;
    }

    public static TView GetView<TView>(this GameObject obj)
        where TView : ViewBase
    {
        var view = obj.GetComponent<TView>();

        view.BeginView();

        return view;
    }
}

public abstract class ViewBase : MonoBehaviour
{
    public void BeginView()
    {
        OnBegin();
    }

    protected virtual void OnBegin()
    {

    }
}