using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;   // 横移動の速度
    public float jumpP = 300.0f; // ジャンプ力

    Rigidbody2D rbody; // リジッドボディを使うための宣言

    // Start is called before the first frame update
    void Start()
    {
        // リジッドボディ2Dをコンポーネントから取得して変数に入れる
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ジャンプするためのコード(もし左クリックが押されて、上方向に速度がないときに)
        if (Input.GetMouseButton(0) && rbody.velocity.y == 0)
        {
            // 上方向にジャンプ力を加える
            rbody.AddForce(transform.up * jumpP);
        }
    }

    private void FixedUpdate()
    {
        //  リジッドボディに一定の速度を入れる(横移動の速度、リジッドボディのyの速度)
        rbody.velocity = new Vector2(speed, rbody.velocity.y);
    }
}

