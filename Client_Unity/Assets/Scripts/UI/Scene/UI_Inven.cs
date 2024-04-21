using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel,
    }

    void Start()
    {
        init();
    }

    public override void init()
    {
        base.init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)     // �ϼ� ���¸� ������ �̸� �����ص� �����ܵ� ����(����)
            Managers.resourceMgr.Destroy(child.gameObject);

        for(int i = 0; i < 8; i++)
        { 
            GameObject item = Managers.UIMgr.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform).gameObject;    //  ~~~: ~~~��� �Ű�������� ǥ��
            UI_Inven_Item InvenItem = item.GetOrAddComponent<UI_Inven_Item>();
            InvenItem.Setname($"�׽�Ʈ {i}");
        }
    }
}
