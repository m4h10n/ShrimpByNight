using UnityEngine;
using UnityEngine.InputSystem;

public class WaterCapacity : MonoBehaviour
{
    private float maxWater = 100f;
    private float currentWater;
    private float leak = 5f;
    private float fill = 5f;
    private InputSystem_Actions playerAbsorb;
    private InputAction absorb;


    private void Awake()
    {
        playerAbsorb = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        absorb = playerAbsorb.Player.Attack;
        absorb.Enable();
    }

    private void OnDisable()
    {
        absorb.Disable();
    }

    private void Start()
    {
        currentWater = maxWater;
    }

    private void Update()
    {
        LosePlayerWater();
    }

    public float GetCurrentWater
    {
        get { return currentWater; }
    }

    public void LosePlayerWater()
    {
        currentWater -= leak * Time.deltaTime;
        if (currentWater <= 0)
        {
            return;
        }
        //Debug.Log("CurrentWater: " +  currentWater);
    }
    
    public void PlayerGainWater()
    {
        currentWater += fill * Time.deltaTime;
        //Debug.Log("CurrentWater: " + currentWater);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        // Check if we hit an enemy
        if (other.CompareTag("Enemy"))
        {
            if (absorb.IsInProgress())
            {
                EnemyWater enemyWater = other.GetComponent<EnemyWater>();
                if (enemyWater != null)
                {
                    enemyWater.LoseEnemyWater();

                    PlayerGainWater();
                }
            }
        }
    }
}
