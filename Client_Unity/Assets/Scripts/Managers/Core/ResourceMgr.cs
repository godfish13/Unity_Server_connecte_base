using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceMgr
{
    public T Load<T>(string path) where T : Object  // ������Ʈ ���� �޼ҵ���� ���������� �갳�Ͽ� �ۼ��ϸ� ��� ������ ��� ������ ã�� ���� �������
    {                                               // ���� �����ϱ� �����ϱ����� ResourceMgr�� ���ϵ��� Load, Destroy�� �� �޼ҵ�� Wrapping �ص�
        if(typeof(T) == typeof(GameObject))
        {                                           // T�� GameObject�� Pool���� ������ ���ɼ��� �����Ƿ� Pool������ �˻� �� ������ �װ� ���
            string name = path;
            int index = name.LastIndexOf('/');      // path�� ��� ��θ��� �����ϹǷ� ���� �������� '/' ���� ���ڸ� �̸����� Pool�� �����Ƿ� name ����
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.poolMgr.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null) // Ǯ�� ��, �⺻ 5�� �����ϰ� ������ �� create�� ���� �ڵ� �߰��ǹǷ� ���� Ǯ������ ���� x
    {
        GameObject Original = Load<GameObject>($"Prefabs/{path}");
        if (Original == null)
        {
            Debug.Log($"Failed to Load prefab : {path}");
        }

        if (Original.GetComponent<Poolable>() != null)              // Poolable��ü�� Pool���� ���� ��
        {
            Debug.Log($"Pooled {path}");
            return Managers.poolMgr.Pop(Original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(Original, parent);   // Poolable �ƴϸ� �׳� �ҷ��پ�
        go.name = Original.name;
        return go;
    }

    public void Destroy(GameObject go) 
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();    // Poolable�̸� pool�� �־��
        if(poolable != null)
        {
            Managers.poolMgr.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }

    public void Destroy(GameObject go, float time)
    {
        if (go == null)
            return;
        
        // ���� Pooling�� ��ü�� �� Ǯ�� �Ŵ����� ��Ź

        Object.Destroy(go, time);
    }
}
