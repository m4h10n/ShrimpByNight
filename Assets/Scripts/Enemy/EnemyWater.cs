using UnityEngine;

public class EnemyWater : MonoBehaviour
{
    private float enemyMax = 30f;
    private float enemyCurrent;
    private float leak = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCurrent = enemyMax;
    }

    public float LoseEnemyWater()
    {
        enemyCurrent -= leak * Time.deltaTime;
        if (enemyCurrent <= 0)
        {
			enemyCurrent = 0;
            return 0;
        }
		return leak;
        //Debug.Log("EnemyWater: " + enemyCurrent);
    }

}
