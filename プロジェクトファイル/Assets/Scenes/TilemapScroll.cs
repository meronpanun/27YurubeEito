using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapScroll : MonoBehaviour
{
    [SerializeField]
    float scrollSpeed = 1;
    Vector3 cameraRectMin;
    void Start()
    {
        //�J�����͈̔͂��擾
        cameraRectMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);   //X�������ɃX�N���[��
       
    }
}
