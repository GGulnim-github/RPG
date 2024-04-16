using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : Manager<UIManager>
{
    [SerializeField] SerializedDictionary<string, UIScreen> _uiDictionary = new();
    [SerializeField] List<UIScreen> _uiList = new();

    public override void Initialize()
    {

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnClickBackButton();
        }
    }

    public void OnClickBackButton()
    {
        if (_uiList.Count > 0)
        {
            UIScreen ui = _uiList[_uiList.Count - 1];
            ui.OnClickBackButton();
        }
    }

    public T OpenUI<T>() where T : UIScreen
    {
        T screen = GetUI<T>();
        
        if (screen == null)
        {
            screen = CreateUI(typeof(T).Name).GetComponent<T>();
            AddUI(screen);
        }
        
        if (_uiList.Contains(screen) == false)
        {
            _uiList.Add(screen);
            screen.SetCanvasSortOrder(_uiList.Count);
        }
      
        screen.gameObject.SetActive(true);
        return screen;
    }

    public void CloseUI<T>() where T : UIScreen
    {
        CloseUI(GetUI<T>());
    }

    public void CloseUI(UIScreen screen)
    {
        if (screen == null)
        {
            return;
        }

        int index = screen.GetCanvasSortOrder();
        _uiList.Remove(screen);
        for (int i = index - 1; i < _uiList.Count; i++)
        {
            _uiList[i].SetCanvasSortOrder(i + 1);
        }

        screen.gameObject.SetActive(false);
    }

    public T GetUI<T>() where T : UIScreen
    {
        if (_uiDictionary.TryGetValue(typeof(T).Name, out UIScreen screen))
        {
            return screen.GetComponent<T>();
        }

        return null;
    }

    public void AddUI(UIScreen screen)
    {
        string screenName = screen.GetType().Name;
        if (_uiDictionary.ContainsKey(screenName) == false)
        {
            _uiDictionary.Add(screenName, screen);
        }
    }

    public void Clear()
    {
        _uiDictionary.Clear();
        foreach (var screen in  _uiList)
        {
            Destroy(screen.gameObject);
        }
        _uiList.Clear();
    }

    public GameObject CreateUI(string name)
    {
        GameObject prefab = Resources.Load<GameObject>($"UI/Screen/{name}");
        GameObject ui = Instantiate(prefab);
        ui.name = name;
        return ui;
    }
}
