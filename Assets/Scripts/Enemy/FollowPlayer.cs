using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public enum GameState { Chill, Search, Alert }
    public GameState State { get; private set; }

    private Transform target;
    private float timer = 15f;
    [SerializeField] private float timerMax = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            target = player.transform;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (State == GameState.Alert)
        {
            Vector2 direction = target.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
        if(State == GameState.Chill)
        {
            timer -= 1 * Time.deltaTime;
            if (timer <= 0)
            {
                timer = timerMax;

            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        State = GameState.Alert;
    }
}
