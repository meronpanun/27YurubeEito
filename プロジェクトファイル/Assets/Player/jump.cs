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
    public int MaxJumpCount = 2;  // �W�����v�̍ő吔
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

    // �X���C�f�B���O�����Ă��邩�̃`�F�b�N
    //bool slidingFlag = false;
    public bool IsActive { get; set; } = true;
    private void Start()
    {
        // Rigidbody2D�R���|�[�l���g�̎擾
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0); // �����ŉE�ɐi��
        if (!IsActive)
        {
            return;
        }
        isGrounded = CheckGroundStatus().collider != null;
        GetInput();
        UpdateAnimator();

        // �X���C�f�B���O�̏���
        //if (Input.GetMouseButton(1) && slidingFlag == false)�@// �}�E�X�E�N���b�N�������ꂽ��
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
        // �W�����v�̏���
        // �}�E�X���N���b�N����������W�����v������
        // MaxJumpCount�̐������W�����v�ł���
        if (Input.GetMouseButtonDown(0) && this.JumpCount < MaxJumpCount)
        {
            this.rigidbody2D.AddForce(transform.up * JumpForce);
            JumpCount++;
        }
    }
  
    // �v���C���[�̃f�o�b�N�\��
    private void OnDrawGizmos()
    {
        var pos = transform.localPosition;
        pos.y += groundCheckOffsetY;
        Gizmos.DrawWireSphere(pos, groundCheckRadius);
    }


    // �ڒn����
    RaycastHit2D CheckGroundStatus()
    {
        var pos = transform.localPosition;
        pos.y += groundCheckOffsetY;
        return Physics2D.CircleCast(pos, groundCheckRadius, Vector2.down, groundCheckDistance, groundLayers);
    }

    // ���ɒ��n������AjumpCount��0�ɂ���
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
