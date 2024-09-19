using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public GameObject Plane;
    //������public�Ɛ錾���邱�ƂŌ��Inspector�r���[���瑀��ł���
    float timer = 0;
    float spowntime = 2; //2�b���Ƃɐ���������

    void Update()
    {
        timer += Time.deltaTime; //timer�̒l��1�b��1�̃y�[�X�ő��₷
        if (timer > spowntime)
        {
            PlaneGenerate(); //PlaneGenerate�֐����Ăяo���B
            timer = 0; //timer��0�ɖ߂��B
        }
    }

    void PlaneGenerate()
    {
        Instantiate(Plane, new Vector3(1, 0, 0), Quaternion.identity);
        /*Plane��(1,0,0)�̏ꏊ�ɐ�������B
        Quaternion.identity�͉�]�����Ȃ����Ƃ��������t*/
    }
}
