using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform foot;
    public LayerMask groundLayer;   
    private bool isGrounded;
    private int jumpCount = 0; 


    [Header("Dash Mechanic")]
    public TrailRenderer trailRenderer;
    public bool CanDash;
    private bool IsDashing;
    public float DashSpeed = 30f;
    public float DashingTime = 0.2f;
    public float DashingCooldown = 1f;

    
    [Header("Health System")]
    [SerializeField] public  GameObject healing;
    public HealthManager healManager;

    public CoinManager cm;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if(IsDashing){
            return;
        }
        // Check if player is grounded using a raycast
        isGrounded = Physics2D.Raycast(foot.position, Vector2.down, 1f, groundLayer);

        // Reset jump count if grounded
        if (isGrounded)
        {
            jumpCount = 0; // Reset jumps when player is on the ground
        }

        // Player movement (simple left/right movement)
        float moveX = Input.GetAxisRaw("Horizontal");
        if(moveX > 0.1f){
            transform.localScale = new Vector3(1,1,1);
            
        }else if(moveX < -0.1f){
            transform.localScale = new Vector3(-1,1,1);
        }
        
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y); // Keep existing vertical velocity (y-axis)
        
        // Double Jump mechanic
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // First jump
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount = 1;  // Player has performed 1 jump
        }
        else if (jumpCount < 2 && !isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Second jump (double jump)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount = 2;  // Player has performed 2 jumps (first and second jump)
        }

        // Dash mechanic
       if(Input.GetKeyDown(KeyCode.LeftShift)){
        StartCoroutine(Dash());
       }
    }

    private IEnumerator Dash(){
        CanDash = false;
        IsDashing = true;
        float OriginalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        trailRenderer.enabled = false; 
        rb.velocity = new Vector2(transform.localScale.x * DashSpeed,0f);
        yield return new WaitForSeconds(DashingTime);
        rb.gravityScale =  OriginalGravity;
        trailRenderer.enabled = true; 
        IsDashing = false;
        yield return new WaitForSeconds(DashingCooldown);
        CanDash = true;

    }

    
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Spike"){
            healManager.TakeDamage(20f);
        }

        if(other.gameObject.tag == "Health"){
            healing.SetActive(false);
            healManager.Heal(20f);
        }
        if(other.gameObject.tag == "Coin"){
            cm.CoinCounter++;
            Destroy(other.gameObject);
            
        }
    }

}
