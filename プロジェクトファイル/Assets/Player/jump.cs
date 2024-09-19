using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    private new Rigidbody2D  rigidbody2D;
    public float Speed = 5.0f;
    private int JumpCount = 0;
    public int MaxJumpCount = 2;  // ジャンプの最大数
    public float JumpForce = 200f;  
    [SerializeField]
    float groundCheckRadius = 0.4f;
    [SerializeField]
    float groundCheckOffsetY = 0.45f;
    [SerializeField]
    float groundCheckDistance = 0.2f;
    [SerializeField]
    LayerMask groundLayers = 0;
    bool isGrounded = false;

    // スライディングをしているかのチェック
    //bool slidingFlag = false;
    public bool IsActive { get; set; } = true;
    private void Start()
    {
        // Rigidbody2Dコンポーネントの取得
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0); // 自動で右に進む
        if (!IsActive)
        {
            return;
        }
        isGrounded = CheckGroundStatus().collider != null;
        GetInput();
        UpdateAnimator();

        // スライディングの処理
        //if (Input.GetMouseButton(1) && slidingFlag == false)　// マウス右クリックが押されたら
        //{
        //    slidingFlag = true;
        //    animator.SetTrigger("SlidingTrigger");
        //}
    }

    //public void SlidingEnd()
    //{
    //    slidingFlag = false;
    //}
    void GetInput()
    {
        // ジャンプの処理
        // マウス左クリックを押したらジャンプさせる
        // MaxJumpCountの数だけジャンプできる
        if (Input.GetMouseButtonDown(0) && this.JumpCount < MaxJumpCount)
        {
            this.rigidbody2D.AddForce(transform.up * JumpForce);
            JumpCount++;
        }
    }
  
    // プレイヤーのデバック表示
    private void OnDrawGizmos()
    {
        var pos = transform.localPosition;
        pos.y += groundCheckOffsetY;
        Gizmos.DrawWireSphere(pos, groundCheckRadius);
    }


    // 接地判定
    RaycastHit2D CheckGroundStatus()
    {
        var pos = transform.localPosition;
        pos.y += groundCheckOffsetY;
        return Physics2D.CircleCast(pos, groundCheckRadius, Vector2.down, groundCheckDistance, groundLayers);
    }

    // 床に着地したら、jumpCountを0にする
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            JumpCount = 0;
        }
    }
    public void UpdateAnimator()
    {
        animator.SetBool("Grounded", isGrounded);
    }
}
