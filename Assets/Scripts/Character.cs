using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float detectRadius = 4f;
    Rigidbody2D playerRB;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    Animator animator;
    float xInput;
    bool isHiding = false;
    bool canHide = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isWalking = xInput != 0;
        animator.SetBool("isRunning", isWalking);
        canHide = boxCollider.IsTouchingLayers(LayerMask.GetMask("Hiding Place"));        
        Debug.Log("isHiding: " + isHiding);
        Debug.Log("Can Hide: " + canHide);

    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(xInput * moveSpeed, playerRB.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(isHiding) return;
        xInput = context.ReadValue<float>();
        if(context.performed)
        {
            spriteRenderer.flipX = xInput < 0 ;
        }
    }


    public void OnHide(InputAction.CallbackContext context)
    {        
        if (context.performed)
        {
            float axisValue = context.ReadValue<float>();
            Hide(axisValue);
        }
    }

    void Hide(float axisValue)
    {
        if (!canHide) return;
        Debug.Log(canHide);
        if (!isHiding && axisValue > 0)
        {
            isHiding = true;
            gameObject.layer = 7;
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        }
        else if (isHiding && axisValue < 0)
        {
            isHiding = !isHiding;
            gameObject.layer = 3;
            spriteRenderer.color = Color.white;
        }

    }

    public void OnCheck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D Interactable = Physics2D.OverlapCircle(transform.position, detectRadius, LayerMask.GetMask("Interactable"));
            Debug.Log(Interactable is not null ? "isNotNull" : "isNull");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

}
