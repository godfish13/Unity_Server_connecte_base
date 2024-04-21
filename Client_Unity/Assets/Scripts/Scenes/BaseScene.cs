using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    Define.Scene _SceneType = Define.Scene.UnKnown;
    public Define.Scene SceneType { get; protected set; } = Define.Scene.UnKnown;

    void Awake()    //  @SceneMgr ������Ʈ�� ��Ȱ��ȭ �Ǿ����� ��츦 ����� Awake���� init()
    {
        init();
    }

    protected virtual void init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null) 
        {
            Managers.resourceMgr.Instantiate("UI/EventSystem").name = "@EventSystem"; 
        }
    }

    public abstract void Clear();
}
