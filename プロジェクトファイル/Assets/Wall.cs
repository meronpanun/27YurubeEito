using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
    // �T�C�Y�𒲐�
    public void SetWall(Vector2 size)
    {
        transform.localScale = new Vector3(size.x, size.y, 1);
    }
    // ��ʊO�ɏo����j���i���e�X�g�v���C���ɃV�[���r���[�ɉf���Ă���Ɣj������Ȃ��̂Œ��Ӂj
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
