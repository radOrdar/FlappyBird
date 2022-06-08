using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    [SerializeField] private Transform visual;
    [SerializeField] private float rotationSpeedDeg;
    [SerializeField] private float force;

    private Rigidbody2D rb;
    private Animator animator;

    private float angle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Jump();
    }

    private void Update()
    {
        if (Game.Instance.IsGameOver)
        {
            animator.enabled = false;
        } else
        {
            bool jumpInput;
#if UNITY_EDITOR
            jumpInput = Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame;
#elif UNITY_ANDROID
            jumpInput = Touchscreen.current.primaryTouch.press.wasPressedThisFrame;
#endif
            if (jumpInput)
            {
                Jump();
            }
        }

        if (rb.velocity.y < 0.2)
        {
            angle = Mathf.Clamp(angle - rotationSpeedDeg * Time.deltaTime, -90, 45);
            visual.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * force;
        angle = 25;
        visual.eulerAngles = new Vector3(0, 0, angle);
        AudioManager.Instance.PlaySound(AudioManager.ClipName.Wing);
    }

    private void OnTriggerEnter2D()
    {
        if (Game.Instance.IsGameOver) { return; }

        Game.Instance.GG();
        AudioManager.Instance.PlaySound(AudioManager.ClipName.Hit);
        if (transform.position.y > -3)
        {
            AudioManager.Instance.PlaySound(AudioManager.ClipName.Die);
        }
    }

    private void OnCollisionEnter2D()
    {
        if (Game.Instance.IsGameOver) { return; }

        Game.Instance.GG();
        AudioManager.Instance.PlaySound(AudioManager.ClipName.Hit);
    }
}