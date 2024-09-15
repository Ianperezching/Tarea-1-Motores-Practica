using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("No se encontró al jugador.");
        }
    }

    void Update()
    {
        if (player != null)
        {
          
            Vector3 direction = (player.position - transform.position).normalized;

           
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
       
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
