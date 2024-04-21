using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI_Btn : UI_PopUp   // Bind, Get~~ �� �⺻ UI���̽� ���
{ 
    enum enum_Button        // UI�� ����� ��ư/�ؽ�Ʈ/���ӿ�����Ʈ ���� �̸��� �����ϰ� �����صΰ� ����Ƽ ���� ���� �׸�� �ڵ����� �����ϰ� ����ϱ� ���� enum
    {
        PointBtn,
    }

    enum enum_Text
    {
        PointTxt,
        ScoreTxt,
    }

    enum enum_Image
    {
        ItemIcon,
    }

    enum enum_GameObject
    {
        TestObj,
    }

    private void Start()
    {
        init();
    }

    public override void init()      // UI_PopUp�� �����Լ��� �����ص� init()
    {                
        base.init();
        Managers.UIMgr.SetCanvas(gameObject, true);

        Bind<Button>(typeof(enum_Button));
        Bind<Text>(typeof(enum_Text));
        Bind<Image>(typeof(enum_Image));
        Bind<GameObject>(typeof(enum_GameObject));

        GetText((int)enum_Text.PointTxt).text = "Testing";
        GetText((int)enum_Text.ScoreTxt).text = "Binding test";
        Debug.Log(GetGameObject((int)enum_GameObject.TestObj).name);

        GetButton((int)enum_Button.PointBtn).gameObject.BindUIEvent(OnBtnClicked);   // ExtensionMethod�� ���� ���� ���

        GameObject go = GetImage((int)enum_Image.ItemIcon).gameObject;
        BindUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag); // ������������ BindUIEvent
    }

    int score = 0;
    public void OnBtnClicked(PointerEventData data)
    {
        score++;
        Debug.Log("Btn Clicked");
        GetText((int)enum_Text.ScoreTxt).text = $"���� : {score}";
    }
}
