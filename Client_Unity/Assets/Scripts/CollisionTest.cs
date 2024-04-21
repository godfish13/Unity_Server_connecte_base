using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(Input.mousePosition);
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));  //viewPort ��ǥ

        /*if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 dir = (mousePos - Camera.main.transform.position).normalized;                                      // ī�޶� ClipPlane�� near ��

            Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
            {
                Debug.Log($"RayCast Camera : {hit.collider.gameObject.name}");
            }
        }*/     // �Ʒ����� Ǯ� ������ ���� ray = mousePos + dir

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //int mask = (1 << 6) | (1 << 8);      // Monster�� 6�����̾�� �س����Ƿ� 6��° ��Ʈ�÷��׷� ���ָ� ���͸� �νĵ�
                                                 //������ | �Ѱ��ΰ� ����! �ΰ����°� ������ ��/������ ��� �� ������ �������� ���� : ������ ������ ���� ����
                                                 //��Ʈ�÷��׿����� ���� �����Ϸ��� ���°��̹Ƿ� ������ ������ �ΰ����ƴ� �Ѱ��� ���°�
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");  // �ٵ� �׳� ��Ʈ�÷��� �Ⱦ��� LayerMask �ᵵ ��
            RaycastHit hit;                     
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"RayCast Camera : {hit.collider.gameObject.name}");
            }
        }
    }
}
