using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectionScript : MonoBehaviour
{
    private GameObject player;
    private Collider2D playerCollider;
	[SerializeField] LayerMask playerColLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerCollider = player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D playerCollider)
    {
		if (playerCollider.CompareTag("Player")) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
