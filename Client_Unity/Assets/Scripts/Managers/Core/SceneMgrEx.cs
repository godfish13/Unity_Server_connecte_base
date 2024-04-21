using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgrEx         // Ex : extended
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene sceneType)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(sceneType));
    }

    string GetSceneName(Define.Scene sceneType)
    {
        //return sceneType.ToString();    // �� �̰� ���� �𰡴ɰ�����? �ٸ� ��ҵ� ����(Ȯ�强)�� ���� ���÷������� �����ϳ�?
                                        // => enum.GetName()������ ���÷��� ���°� ToString���� ���ɻ� ������� ��!
        string name = System.Enum.GetName(typeof(Define.Scene), sceneType);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
