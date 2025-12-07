using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 5f;
    private float inputMoveY;
    private float inputMoveX;
    private Vector2 move;



    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputMoveX * speed, inputMoveY * speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        inputMoveX = move.x;
        inputMoveY = move.y;

        anim.SetFloat("MoveY", inputMoveY);
        anim.SetFloat("MoveX", inputMoveX);
    }
    private void Update()
    {

    }
}
