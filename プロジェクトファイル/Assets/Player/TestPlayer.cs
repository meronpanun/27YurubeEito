using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidBody2D;
    [SerializeField]
    Animator animator;
    [SerializeField]
    float jumpPower = 20;
    [SerializeField]
    float groundCheckRadius = 0.4f;
    [SerializeField]
    float groundCheckOffsetY = 0.45f;
    [SerializeField]
    float groundCheckDistance = 0.2f;
    [SerializeField]
    LayerMask groundLayers = 0;
    bool isGrounded = false;
    public bool IsActive { get; set; } = true;
    void Update()
    {
        if (!IsActive)
        {
            return;
        }
        isGrounded = CheckGroundStatus().collider != null;
        GetInput();
        UpdateAnimator();
    }
    // 入力受付
    void GetInput()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    // 接地判定
    RaycastHit2D CheckGroundStatus()
    {
        return Physics2D.CircleCast((Vector2)transform.position + groundCheckOffsetY * Vector2.up, groundCheckRadius, Vector2.down, groundCheckDistance, groundLayers);
    }
    // ジャンプ
    void Jump()
    {
        rigidBody2D.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
    }
    // アニメーターの更新
    void UpdateAnimator()
    {
        animator.SetBool("Grounded", isGrounded);
    }
}
