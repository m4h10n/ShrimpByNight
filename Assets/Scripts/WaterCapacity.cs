using UnityEngine;
using UnityEngine.InputSystem;

public class WaterCapacity : MonoBehaviour
{
	private float maxWater = 100f;
	private float currentWater;
	private float leak = 2f;
	private InputSystem_Actions playerAbsorb;
	private InputAction absorb;

	private Component playerWater;
	private SpriteRenderer waterTexture;
	private HydrationBar waterBar;

	private void Awake()
	{
		playerAbsorb = new InputSystem_Actions();		
		playerWater = GameObject.FindWithTag("Player").GetComponent<WaterCapacity>();
		waterTexture = GameObject.FindWithTag("WaterAura").GetComponent<SpriteRenderer>();
		waterBar = GameObject.FindWithTag("HydrationUI").GetComponent<HydrationBar>();

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
		
		if (absorb.IsInProgress()) waterTexture.color = new Color(1,1,1,1);
		else waterTexture.color = new Color(0,0,0,0);
		
	}

	public float GetCurrentWater
	{
		get { return currentWater; }
	}

	public void LosePlayerWater()
	{
		currentWater -= leak * Time.deltaTime;
		waterBar.SetHydration(currentWater);
		if (currentWater <= 0)
		{
			return;
		}
		//Debug.Log("CurrentWater: " +  currentWater);
	}
	
	public void PlayerGainWater(float amount)
	{
		if (currentWater <= 100f) {	
		currentWater += amount * Time.deltaTime;
		waterBar.SetHydration(currentWater);
		//Debug.Log("CurrentWater: " + currentWater);
		}
		else
		{
			currentWater = 100f;
		}
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		// Check if we hit an enemy, if we need water, and if we are absorbing.
		if (other.CompareTag("Enemy") && absorb.IsInProgress() && currentWater <= 99)
		{
			// Get the enemy's water and drain it.
			EnemyWater enemyWater = other.GetComponent<EnemyWater>();
			
			if (enemyWater != null)
			{
				float amount = enemyWater.LoseEnemyWater();
				PlayerGainWater(amount);
			}
			
		}
	}
}
