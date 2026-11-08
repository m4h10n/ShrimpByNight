using UnityEngine;

public class EnemyWater : MonoBehaviour
{
    private float enemyMax = 30f;
    private float enemyCurrent;
    private float leak = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCurrent = enemyMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseEnemyWater()
    {
        enemyCurrent -= leak * Time.deltaTime;
        if (enemyCurrent <= 0)
        {
            return;
        }
        Debug.Log("EnemyWater: " + enemyCurrent);
    }

}
