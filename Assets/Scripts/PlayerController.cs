using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    Shooting
}

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]

    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private PlayerState currentState;
    private Animator animator;
    private bool facingRight = true;

    void Start()
    {
        currentState = PlayerState.Idle;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                HandleIdleState();
                break;

            case PlayerState.Moving:
                HandleMovingState();
                break;

            case PlayerState.Shooting:
                HandleShootingState();
                break;
        }
    }

    void HandleIdleState()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isShooting", false);

        if (Input.GetAxis("Horizontal") != 0)
        {
            currentState = PlayerState.Moving;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentState = PlayerState.Shooting;
        }
    }

    void HandleMovingState()
    {
        animator.SetBool("isMoving", true);
        animator.SetBool("isShooting", false);

        // Movimiento del jugador
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        transform.Translate(movement * speed * Time.deltaTime);

        // Voltear el personaje
        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }

        if (moveHorizontal == 0)
        {
            currentState = PlayerState.Idle;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentState = PlayerState.Shooting;
        }
    }

    void HandleShootingState()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isShooting", true);
        ShootProjectile();
        currentState = PlayerState.Idle;
    }

    void ShootProjectile()
    {
        Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    }

    void OnDrawGizmos()
    {
        if (projectileSpawnPoint != null)
        {
            // Dibujar una l�nea desde el punto de disparo en la direcci�n del disparo
            Gizmos.color = Color.red;
            Gizmos.DrawLine(projectileSpawnPoint.position, projectileSpawnPoint.position + projectileSpawnPoint.up * 2);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}