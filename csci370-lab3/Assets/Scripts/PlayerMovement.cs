using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public ParticleSystem dust;

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    float horizontal;

    public float runSpeed = 5f;
    private bool m_Grounded;

    public int dir = 1;
    public int newDir = 1;

    public UnityEvent OnLandEvent;
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip carrotSound;
    AudioSource audio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        if (OnLandEvent == null) {
		    OnLandEvent = new UnityEvent();
        }
        OnLandEvent.AddListener(Landed);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (horizontal < 0) {
            spriteRenderer.flipX = false;
            newDir = 1;
        } 
        if (horizontal > 0) {
            spriteRenderer.flipX = true;
            newDir = 0;
        }

        if (dir != newDir) { // Determines if direction changes, plays dust
            dir = newDir;
            createDust();
        }

        if (Input.GetKeyDown("space") && m_Grounded)
        {
            createDust();
            rigidbody2D.AddForce(Vector2.up * 300);
            animator.SetBool("IsJumping", true);
            audio.PlayOneShot(jumpSound, 1);
            Debug.Log("space key was pressed");
        }

    }

    void FixedUpdate()
    {
        rigidbody2D.linearVelocity = new Vector2(horizontal * runSpeed, rigidbody2D.linearVelocity.y);
    
		bool wasGrounded = m_Grounded;
;
        m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
                m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

    public void Landed() {
        createDust();
        animator.SetBool("IsJumping", false);
    }

    void createDust() {
        dust.Play();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Carrot")) {
            GameManager.Instance.IncScore(1);
            audio.PlayOneShot(carrotSound, 1);
            Destroy(collision.gameObject);
        }
    }

}