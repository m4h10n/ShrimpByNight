using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum GameState {Idle, Wander, Chase}
    public GameState EnemyBehavior { get; set; }

    [SerializeField] private Transform pfFieldOfView;
    private FieldOfView fieldOfView;

    private Vector3 aimDir;
    private float idleCounter = -1f; //Time it takes to look in new direction.

    private GameObject player;
    private Rigidbody2D rb;
    private float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyBehavior = GameState.Idle;
        player = GameObject.FindWithTag("Player");
        moveSpeed = 10f;

        fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentToTarget = player.transform.position - (Vector3) rb.position;
        fieldOfView.SetOrigin(transform.position);

        if (EnemyBehavior == GameState.Idle)
        {
            if (idleCounter < 0)
            {
                idleCounter = 5f;
                aimDir = (new Vector3(UnityEngine.Random.Range(-360,361), UnityEngine.Random.Range(-360,361), 0) - transform.position).normalized; // When enemy cone is too far to the left things break
                fieldOfView.SetAimDirection(aimDir);
                if (UnityEngine.Random.Range(1,5) == 1)
                {
                    EnemyBehavior = GameState.Wander;
                }
            }

            idleCounter -= 1 * Time.deltaTime;
        }

        if (EnemyBehavior == GameState.Chase)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < fieldOfView.GetViewDistance())
            {
                Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer, fieldOfView.GetViewDistance());
                if (Vector3.Angle(aimDir, dirToPlayer) < fieldOfView.GetFOV() / 2f)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            rb.MovePosition((Vector3) rb.position + currentToTarget.normalized * moveSpeed * Time.deltaTime);;
                            aimDir = dirToPlayer;
                            fieldOfView.SetAimDirection(aimDir);
                        }   
                        else
                        {
                            EnemyBehavior = GameState.Idle;
                        }
                    }
                }
                else
                {
                    EnemyBehavior = GameState.Idle;
                }
            }
            else
            {
                EnemyBehavior = GameState.Idle;
            }
        }


        if (EnemyBehavior == GameState.Wander)
        {
           if (Vector3.Distance(transform.position, player.transform.position) < fieldOfView.GetViewDistance())
            {
                Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer, fieldOfView.GetViewDistance());
                if (Vector3.Angle(aimDir, dirToPlayer) < fieldOfView.GetFOV() / 2f)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            EnemyBehavior = GameState.Chase;
                        }  
                    } 
                }
            } 
        }
        
    }

    public GameState getGameState() {
        return EnemyBehavior;
    }

    public void setIdleCounter(float count)
    {
        idleCounter = count;
    }

    public void setAimDir(Vector3 dir)
    {
        aimDir = dir;
        fieldOfView.SetAimDirection(dir);
    }
}
