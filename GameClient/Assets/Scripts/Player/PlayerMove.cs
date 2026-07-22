using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerStatSO playerStat;
    private Rigidbody2D rb;
    private Vector2 inputVector;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        float inputX = 0.0f;
        float inputY = 0.0f;

        if(Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) inputY = 1.0f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) inputX = -1.0f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) inputY = -1.0f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) inputX = 1.0f;
        }

        // 캐릭터가 바라보는 방향 수정
        if (inputVector.x < 0.0f)
        {
            sprite.flipX = true;
        }
        else if (inputVector.x > 0.0f)
        {
            sprite.flipX = false;
        }

        inputVector = new Vector2(inputX, inputY).normalized;
    }

    private void FixedUpdate()
    {
        if (playerStat != null)
        {
            rb.linearVelocity = inputVector * playerStat.MoveSpeed;
        }
    }
}
