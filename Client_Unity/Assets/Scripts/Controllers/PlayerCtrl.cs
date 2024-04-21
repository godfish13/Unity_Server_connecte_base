using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //GameObject (Player) ����

    [SerializeField] float M_speed = 10.0f;
    Vector3 MouseClickDestination;

    public enum PlayerStatue
    {
        Die,
        Moving,
        Idle,
    }
    [SerializeField] PlayerStatue _Statue = PlayerStatue.Idle;

    private void Start()
    {
        /*Managers.inputMgr.KeyAction -= Move; // �ٸ��κп��� Move�� ������������ �׼��̺�Ʈ�� ������ �߻��ϴ� ���׹����� ���� �ʱ�ȭ
        Managers.inputMgr.KeyAction += Move; // inputMgr�� KeyAction�� Move ����*/ // ���ۿ����� �ϴ� ���콺 ������ ����

        Managers.inputMgr.MouseAction -= OnMouseClicked;
        Managers.inputMgr.MouseAction += OnMouseClicked;
    }

    void Update()
    {
        switch (_Statue)
        {
            case PlayerStatue.Die:
                UpdateDie();
                break;
            case PlayerStatue.Idle:
                UpdateIdle();
                break;
            case PlayerStatue.Moving:
                UpdateMoving();
                break;
        }    
    }

    void UpdateDie()
    {
        // �ƹ��͵� ���ϴ� �������
    }

    void UpdateMoving()
    {
        Vector3 dir = MouseClickDestination - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _Statue = PlayerStatue.Idle;
        }
        else
        {
            float MoveDist = Mathf.Clamp(M_speed * Time.deltaTime, 0, dir.magnitude);
            // Clamp(float value, min, max) : value���� min�� max ���� �̿��� ���� ���� �ʵ��� �� -> ��Ȯ�� �̵��� �Ÿ� ���
            transform.position += dir.normalized * MoveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }

        // ���� �ִϸ��̼� ó��
        Animator Anim = GetComponent<Animator>();
        // ���� ���� ���¿� ���� ������ �Ѱ���
        Anim.SetFloat("Speed", M_speed);
    }

    /*void OnRunEvent()     // AnimationEvent ����
    {
        Debug.Log("�ѹ��ѹ�")
    }*/

    void UpdateIdle()
    {

        // ���� �ִϸ��̼� ó��
        Animator Anim = GetComponent<Animator>();
        Anim.SetFloat("Speed", 0);
    }

 /*   void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
            transform.position += Vector3.forward * Time.deltaTime * M_speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
            transform.position += Vector3.back * Time.deltaTime * M_speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.1f);
            transform.position += Vector3.left * Time.deltaTime * M_speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.1f);
            transform.position += Vector3.right * Time.deltaTime * M_speed;
        }
    }*/

    void OnMouseClicked(Define.MouseEvent evt)
    {
        /*if (evt != Define.MouseEvent.Click)     // ���콺 Ŭ���ϰ� �������� �۵��ϵ��� ��, inputMgr�� MouseŬ�� �̺�Ʈ �׼� ȣ�� ����
            return;*/

        if (_Statue == PlayerStatue.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            MouseClickDestination = hit.point;
            _Statue = PlayerStatue.Moving;
            //Debug.Log($"RayCast Camera : {hit.collider.gameObject.name}");
        }
    }
}
