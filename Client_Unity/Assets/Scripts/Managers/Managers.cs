using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Managers : MonoBehaviour
{
    static Managers Mgr_Instance;     // �� ��ũ��Ʈ�� ������������ �� �ٸ������� ���� ȣ�� �� init()�ѹ� �����Ͽ� singleton������ instance �������
    public static Managers Instance { get { init(); return Mgr_Instance; } }  // �̹� ������ ��� init()������ ���� ��ŵ��

    #region Contents
    NetworkMgr _networkMgr = new NetworkMgr();

    public static NetworkMgr networkMgr { get { return Instance._networkMgr; } }
    #endregion

    DataMgr _dataMgr = new DataMgr();
    InputMgr _inputMgr = new InputMgr();
    PoolMgr _poolMgr = new PoolMgr();
    ResourceMgr _resourceMgr = new ResourceMgr();
    SceneMgrEx _sceneMgrEx = new SceneMgrEx();
    SoundMgr _soundMgr = new SoundMgr();
    UIMgr _UIMgr = new UIMgr();

    public static DataMgr dataMgr { get { return Instance._dataMgr; } }
    public static InputMgr inputMgr { get { return Instance._inputMgr; } }
    public static PoolMgr poolMgr { get { return Instance._poolMgr; } }
    public static ResourceMgr resourceMgr { get { return Instance._resourceMgr; } }
    public static SceneMgrEx sceneMgrEx { get { return Instance._sceneMgrEx; } }
    public static SoundMgr soundMgr { get { return Instance._soundMgr; } }
    public static UIMgr UIMgr { get { return Instance._UIMgr; } }

    void Start()
    {
        init();
        _networkMgr.Init();
    }

    void Update()
    {
        _inputMgr.UpdateWhenanyKey();   // anyKey�� ������ Update �۵�
        _networkMgr.Update();
    }

    static void init()          //singleton ����
    {
        if(Mgr_Instance == null)
        {
            GameObject MgrObject = GameObject.Find("@Managers");
            if(MgrObject == null)
            {
                MgrObject = new GameObject { name = "@Managers" };
                MgrObject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(MgrObject);
            Mgr_Instance = MgrObject.GetComponent<Managers>();

            Mgr_Instance._dataMgr.init();
            Mgr_Instance._poolMgr.init();
            Mgr_Instance._soundMgr.init();
        }      
    }

    public static void Clear()
    {
        // DataMgr�� ũ�⵵ �۰� �� �����ؾ��ϹǷ� Clear����
        inputMgr.Clear();
        sceneMgrEx.Clear();
        soundMgr.Clear();
        UIMgr.Clear();

        poolMgr.Clear();
    }
}
