using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Stat

[Serializable]      // �޸𸮿� ����ִ� ������ ���Ϸ� ��ȯ��Ű�� ���� �ʿ��� ���� // �׳� ���...
public class Stat
{
    public int level;       // public or [SerializeField] �����ؾ��� JSON���� ������ �޾ƿ� �� ����
    public int HP;          // �� �׸��� �̸��̶� JSON ���� �� �׸��� �̸��� �� ���ƾ� ������ �޾ƿ� �� ����
    public int attack;      // �ڷ��� ���� ����!
}

[Serializable]
public class StatData : ILoader<int, Stat>
{
    public List<Stat> Stats = new List<Stat>();     // !!!!!!�߿�!!!!!! JSON���Ͽ��� �޾ƿ����� list�� �̸��� ��!!! ���ƾ���

    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in Stats)
            dict.Add(stat.level, stat);
        return dict;
    }
}

#endregion