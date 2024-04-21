using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr
{
    int _order = 10; // UI���� ȭ�鿡 ����Ǵ� ���� (Canvas�� order)

    Stack<UI_PopUp> _PopUpStack = new Stack<UI_PopUp>();
    UI_Scene _sceneUI = null;

    public GameObject Root      // Ui�� �ϳ��� ����� �Ѹ� �θ�Ŭ����
    {
        get 
        {
            GameObject root = GameObject.Find("root");
            if(root == null)
                root = new GameObject { name = "root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;  // Canvas�� ��ø�Ǹ� �θ������Ʈ�� sortingOrder�� ���þ��� �ڱ��� sortingOrder�� �������� �ϴ� �ɼ�
        
        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;   // sort��û�� ���Ѱ� PopUp�� ���þ��� UI�̹Ƿ� sort���� ��ȭ x
        }
            
    }

    public T MakeSubItem<T>(Transform parent, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))      // �̸� ���� ���ϰ� �׳� ����ϸ� T �˾���Ŵ
            name = typeof(T).Name;

        GameObject go = Managers.resourceMgr.Instantiate($"UI/SubItem/{name}");

        if(parent != null)
            go.transform.SetParent(parent, false);

        return Utils.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))      // �̸� ���� ���ϰ� �׳� ����ϸ� T �˾���Ŵ
            name = typeof(T).Name;

        GameObject go = Managers.resourceMgr.Instantiate($"UI/Scene/{name}");
        T SceneUI = Utils.GetOrAddComponent<T>(go);
        _sceneUI = SceneUI;

        go.transform.SetParent(Root.transform);

        return SceneUI;
    }

    public T ShowPopUpUI<T>(string name = null) where T : UI_PopUp
    {
        if(string.IsNullOrEmpty(name))      // �̸� ���� ���ϰ� �׳� ����ϸ� T �˾���Ŵ
            name = typeof(T).Name;

        GameObject go = Managers.resourceMgr.Instantiate($"UI/PopUp/{name}");
        T PopUp = Utils.GetOrAddComponent<T>(go);
        _PopUpStack.Push(PopUp);

        go.transform.SetParent(Root.transform);

        return PopUp;
    }

    public void ClosePopUpUI(UI_PopUp popUp)
    {
        if (_PopUpStack.Count == 0)
            return;

        if(_PopUpStack.Peek() != popUp)         // Peek() : Stack�� ���� �����ִ� ��� Ȯ�ο�(Pop���� ���� �׳� ��������)
        {
            Debug.Log("Close PopUp failed");
            return;
        }
        ClosePopUpUI();
    }

    public void ClosePopUpUI()      // ���� �������� ���� �����Ͽ� ���� �������� �˾���(���� �����ִ�) UI �ݱ�
    {
        if (_PopUpStack.Count == 0)
            return;

        UI_PopUp popUp = _PopUpStack.Pop();
        Managers.resourceMgr.Destroy(popUp.gameObject);
        popUp = null;   // Ȥ�� �ٽ� ������ ������ ���ֱ� ���� popUp�� null�� �ʱ�ȭ
        _order--;
    }

    public void CloseAllPopUpUI()
    {
        while(_PopUpStack.Count > 0)
            ClosePopUpUI();
    }

    public void Clear()
    {
        CloseAllPopUpUI();
        _sceneUI = null;
    }
}
