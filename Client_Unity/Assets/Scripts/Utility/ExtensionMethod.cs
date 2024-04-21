using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class ExtensionMethod    // GameObject �տ� this �߰��� ���� go.BindUIEvent�������� ��� ����
{
    public static T GetOrAddComponent<T>(this GameObject Target) where T : UnityEngine.Component
    {
        return Utils.GetOrAddComponent<T>(Target);
    }

    public static void BindUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindUIEvent(go, action, type);
    }

}
