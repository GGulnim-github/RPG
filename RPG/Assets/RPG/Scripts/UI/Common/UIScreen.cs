using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class UIScreen : MonoBehaviour
{
    Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        gameObject.SetActive(false);
        UIManager.Instance.AddUI(this);
        transform.SetParent(UIManager.Instance.transform);
    }

    public int GetCanvasSortOrder()
    {
        return _canvas.sortingOrder;
    }

    public void SetCanvasSortOrder(int order)
    {
        _canvas.sortingOrder = order;
    }

    public virtual void OnClickBackButton()
    {
        UIManager.Instance.CloseUI(this);
    }
}
