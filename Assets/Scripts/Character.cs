using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float travelTime;
    [SerializeField] float detectRadius = 4f;
    Vector3 mouseClickPosition;
    Rigidbody2D playerRB;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    Animator animator;
    float xInput;
    bool isHiding = false;
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
        animator.SetBool("isWalking", isWalking);
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(xInput * moveSpeed, playerRB.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext value)
    {
        xInput = value.ReadValue<float>();
    }


    public void OnHide(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            float axisValue = callbackContext.ReadValue<float>();
            Hide(axisValue);
        }
    }

    void Hide(float axisValue)
    {
        bool canHide = boxCollider.IsTouchingLayers(LayerMask.GetMask("Hiding"));
        if (!canHide) return;
        isHiding = !isHiding;
        if(isHiding && axisValue > 0)
        {
            gameObject.layer = LayerMask.GetMask("Hiding Char");
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        }
        else
        {
            gameObject.layer = LayerMask.GetMask("Player");
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

    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse Clicked");
        Vector2 mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToViewportPoint(mousePos);
        Vector3.MoveTowards(transform.position, new Vector2(mouseClickPosition.x, transform.position.y), travelTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

}
