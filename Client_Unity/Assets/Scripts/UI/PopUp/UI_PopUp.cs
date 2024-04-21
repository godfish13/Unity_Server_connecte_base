using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopUp : UI_Base
{
    public override void init()      // Start�� �θ��� ������ �� ��ũ��Ʈ�� ���� ���°� �ƴ� UI_Btn���� UI_PopUpŬ������ ��ӹ޾� ����ϹǷ�
    {                                                   // UI_Base���� �����Լ��� ����� init() override �� UI_Btn�� Start���� �������
        Managers.UIMgr.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopUpUI()
    {
        Managers.UIMgr.ClosePopUpUI(this);
    }
}
