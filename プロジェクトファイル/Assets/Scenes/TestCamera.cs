using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    private Vector3 diff; //�J�����ƃv���C���[�̋���
    public GameObject target; //�Ǐ]����^�[�Q�b�g�I�u�W�F�N�g
    public float followSpeed; //�Ǐ]����X�s�[�h

    void Start()
    {
        target = GameObject.Find("Player"); //���O��Player�̃I�u�W�F�N�g���擾���ă^�[�Q�b�g�Ɏw��
        diff = target.transform.position - this.transform.position; //�J�����ƃv���C���[�̏����̋������w��
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(this.transform.position, target.transform.position - diff, Time.deltaTime * followSpeed); //���`��Ԋ֐��ɂ��J�����̈ړ�
    }
}
