using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolMgr
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> poolStack = new Stack<Poolable>();

        public void init(GameObject original, int PoolCount = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";
            
            for (int i = 0; i < PoolCount; i++)
            {
                Push(Create());
            }
        }

        Poolable Create()   // pooling�Ϸ��� �����տ� Ȥ�� Poolable ������ �޾���(�⺻������ pooling�� ��ü�� Poolabel�� �޾��� prefab���� ���������
        {                   // inspecter������ ���۽� ���� �̷���찡 ������� �����Ƿ� GetOrAddComponent ���
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)     // Poolable ������Ʈ ��Ȱ��ȭ �� ���ÿ� ����
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;
            poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;
            if (poolStack.Count > 0)
                poolable = poolStack.Pop();
            else
                poolable = Create();      // Pool�� ������Ʈ �����ϸ� ���������

            poolable.gameObject.SetActive(true);

            if (parent == null)  // DontDestroyOnLoad ������ ���� ���� �׷� ����� �����Ƿ� Ÿ ������Ʈ�� parent�� ��� ����            
                poolable.transform.parent = Managers.sceneMgrEx.CurrentScene.transform;
            
            poolable.transform.parent = parent;
            poolable.isUsing = true;
            return poolable;
        }
    }
    #endregion


    Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();
    Transform _root;

    public void init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original, int PoolCount = 5)
    {
        Pool pool = new Pool();
        pool.init(original, PoolCount);
        pool.Root.parent = _root;

        poolDictionary.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (poolDictionary.ContainsKey(name) == false)   // Ȥ�� ������Ʈ Ǯ�� �̻����� ���¿��� ������Ʈ�� Ǯ�� �������ϸ� �������� ������Ʈ Destroy
        {
            Debug.Log("no Key contains");
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        poolDictionary[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null, int PoolCount = 5)
    {
        if (poolDictionary.ContainsKey(original.name) == false)
            CreatePool(original, PoolCount);
        return poolDictionary[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (poolDictionary.ContainsKey(name) == false)
            return null;

        return poolDictionary[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        poolDictionary.Clear();
    }
}