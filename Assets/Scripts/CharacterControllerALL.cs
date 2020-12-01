using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class CharacterControllerALL : MonoBehaviour
{
    [SerializeField]
    private Transform projectPosition;

    [SerializeField]
    private GameObject projectliePrefab;

    [SerializeField]
    private LayerMask platformLayerMask;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    private int speed = 5;

    [SerializeField]
    private int jumpHeight = 0;


    private bool IsAttacking;
    private bool IsAttacking2;

    [SerializeField]
    private float fallForce;

    [SerializeField]
    protected Rigidbody2D rigidbody2D;

    [SerializeField]
    private CircleCollider2D circleCollider2d;

    public bool isFacingLeft = true;


    public Action Attack1;
    public Action Attack2;
    public Action Attack3;
    public Action JumpUp;
    public Action FallDown;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2d = transform.GetComponent<CircleCollider2D>();
        
    }


    private bool isPlayer;



    private bool IsGrounded()
    {
        float extraHeightText = 0.25f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider2d.bounds.center, circleCollider2d.bounds.size,0f,Vector2.down,extraHeightText, platformLayerMask);
        Color rayColor;

        if(raycastHit.collider!=null)
        {
            rayColor = Color.green;
            animator.SetBool("isJumping", false);
            animator.SetBool("isLanding", false);
            Debug.DrawRay(circleCollider2d.bounds.center + new Vector3(circleCollider2d.bounds.extents.x, 0), Vector2.down * (circleCollider2d.bounds.extents.y + extraHeightText), rayColor);
            Debug.DrawRay(circleCollider2d.bounds.center - new Vector3(circleCollider2d.bounds.extents.x, 0), Vector2.down * (circleCollider2d.bounds.extents.y + extraHeightText), rayColor);
            Debug.DrawRay(circleCollider2d.bounds.center - new Vector3(circleCollider2d.bounds.extents.x, circleCollider2d.bounds.extents.y + extraHeightText), Vector2.right * 2 * (circleCollider2d.bounds.extents.x), rayColor);
            Debug.Log(raycastHit.collider);
            Debug.Log(rayColor);
            return true;
        }
        
        else
        {
            rayColor = Color.red;
            animator.SetBool("isJumping", true);
            Debug.DrawRay(circleCollider2d.bounds.center + new Vector3(circleCollider2d.bounds.extents.x, 0), Vector2.down * (circleCollider2d.bounds.extents.y + extraHeightText), rayColor);
            Debug.DrawRay(circleCollider2d.bounds.center - new Vector3(circleCollider2d.bounds.extents.x, 0), Vector2.down * (circleCollider2d.bounds.extents.y + extraHeightText), rayColor);
            Debug.DrawRay(circleCollider2d.bounds.center - new Vector3(circleCollider2d.bounds.extents.x, circleCollider2d.bounds.extents.y + extraHeightText), Vector2.right * 2 * (circleCollider2d.bounds.extents.x), rayColor);
            Debug.Log(raycastHit.collider);
            Debug.Log(rayColor);
            return false;
        }


    }

    private void ResetValues()
    {
        animator.SetBool("isMoving", false);
        IsAttacking = false;
        IsAttacking2 = false;
    }
    public bool canIMove = true;

    public virtual void Update()
    {
        ResetValues();
        isPlayer = gameObject.tag == "Player";
        HandleInput();
        HandleAttacks();
        HandleLayers();
    }


    private void HandleAttacks()
    {
        if (AttackAnimationIsOff() && IsAttacking)
        {
            animator.SetTrigger("isAttacking");
        }
        if (AttackAnimationIsOff() && IsAttacking2)
        {
            animator.SetTrigger("isAttacking2");
        }
 

    }

    private bool AttackAnimationIsOff()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsTag("IblisAttack") || this.animator.GetCurrentAnimatorStateInfo(0).IsTag("IblisAttack2"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void HandleInput()
    {
        if (AttackAnimationIsOff() && isPlayer && canIMove)
        {
            if (rigidbody2D.velocity.y < 0)
            {
                animator.SetBool("isLanding", true);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }

            if (IsGrounded() && Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                FastFall();
            }

            if (IsGrounded() && Input.GetKeyDown(KeyCode.Z))
            {
                IsAttacking = true;
            }

            else if (IsGrounded() && Input.GetKeyDown(KeyCode.X))
            {
                IsAttacking2 = true;

            }

            else if (Input.GetKey(KeyCode.C))
            {

            }
        }
    }


    public void AttackWith1()
    {
        IsAttacking = true;
        Attack1?.Invoke();
    }

    public void AttackWith2()
    {
        IsAttacking2 = true;
        Attack2?.Invoke();
    }

    public void ThrowProjectile(int value)
    {

        if(!IsGrounded() && value == 1 || IsGrounded() && value == 0)
        {
            if (isFacingLeft)
            {
                GameObject tmp = (GameObject)Instantiate(projectliePrefab,projectPosition.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                tmp.GetComponent<Projectille>().Initialize(Vector2.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(projectliePrefab,projectPosition.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                tmp.GetComponent<Projectille>().Initialize(Vector2.left);
            }
        }


    }

    private void HandleLayers()
    {
        if (!IsGrounded())
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    void MoveRight()
    {
        if(!isFacingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingLeft = true;
        }

        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        animator.SetBool("isMoving", true);
    }

    void MoveLeft()
    {
        if(isFacingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isFacingLeft = false;
        }

        transform.position += new Vector3(speed * -1 * Time.deltaTime, 0, 0);
        animator.SetBool("isMoving", true);
    }

    public void Jump()
    {
        if(IsGrounded())
        {
            JumpUp?.Invoke();
            rigidbody2D.AddForce(new Vector3(0, jumpHeight, 0), ForceMode2D.Impulse);
        }
    }
    
    public void FastFall()
    {
        if(!IsGrounded())
        {
            FallDown?.Invoke();
            rigidbody2D.AddForce(new Vector3(0, -fallForce, 0), ForceMode2D.Impulse);
        }
    }


    public void ResetAfterFrozen(CharacterControllerALL playerController, BoxCollider boxCollider, SpriteRenderer renderer, AICharacterController aiController = null)
    {
        StartCoroutine(playerController.ResetCharacter(playerController, boxCollider, renderer, aiController));
    }

    public IEnumerator ResetCharacter(CharacterControllerALL playerController, BoxCollider boxCollider, SpriteRenderer renderer, AICharacterController aiController = null)
    {
        yield return new WaitForSeconds(1f);

        if (aiController != null)
        {
            aiController.enabled = true;
        }

        if (playerController != null)
        {
            playerController.canIMove = true;
        }

        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }

        if (rigidbody2D != null)
        {
            rigidbody2D.isKinematic = false;
        }

        if (renderer != null)
        {
            renderer.color = Color.white;
        }
    }
}
