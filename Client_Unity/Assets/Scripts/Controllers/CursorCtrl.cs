using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCtrl : MonoBehaviour
{
    Texture2D AttackCursor;
    Texture2D HandCursor;

    enum CursorType
    {
        None,
        Hand,
        Attack,
    }
    CursorType _cursorType = CursorType.None;

    int GroundMonsterLayerMask = (1 << (int)Define.Layer.Monster) | (1 << (int)Define.Layer.Ground);

    void Start()
    {
        AttackCursor = Managers.resourceMgr.Load<Texture2D>("Textures/Cursors/Attack");
        HandCursor = Managers.resourceMgr.Load<Texture2D>("Textures/Cursors/Hand");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))    // ���콺 Ŭ���߿��� Ŀ�� ��ȭ x
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, GroundMonsterLayerMask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(AttackCursor, new Vector2(AttackCursor.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;    // SetCursor(texture, hotspot, cursorMode) hotspot : ���콺 �����Ͱ� Ŭ���Ǵ� ��ġ (0,0) == ������ ����
                }                                                               // cursorMode : auto == �ϵ��� ���� ����ȭ �� auto����
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(HandCursor, new Vector2(HandCursor.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }
}
