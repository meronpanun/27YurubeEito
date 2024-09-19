using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;   // ���ړ��̑��x
    public float jumpP = 300.0f; // �W�����v��

    Rigidbody2D rbody; // ���W�b�h�{�f�B���g�����߂̐錾

    // Start is called before the first frame update
    void Start()
    {
        // ���W�b�h�{�f�B2D���R���|�[�l���g����擾���ĕϐ��ɓ����
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // �W�����v���邽�߂̃R�[�h(�������N���b�N��������āA������ɑ��x���Ȃ��Ƃ���)
        if (Input.GetMouseButton(0) && rbody.velocity.y == 0)
        {
            // ������ɃW�����v�͂�������
            rbody.AddForce(transform.up * jumpP);
        }
    }

    private void FixedUpdate()
    {
        //  ���W�b�h�{�f�B�Ɉ��̑��x������(���ړ��̑��x�A���W�b�h�{�f�B��y�̑��x)
        rbody.velocity = new Vector2(speed, rbody.velocity.y);
    }
}

