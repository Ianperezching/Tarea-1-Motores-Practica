using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    [SerializeField] private Transform initialPosition;
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;

    private bool playerInRange = false;
    private bool isReturning = false;
    private float nextFireTime = 0f;

    private void Update()
    {
        if (playerInRange)
        {
            MoveTowardsPlayer();
            if (Time.time > nextFireTime)
            {
                FireProjectile();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else if (isReturning)
        {
            ReturnToInitialPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            isReturning = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            isReturning = true;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void ReturnToInitialPosition()
    {
        Vector2 direction = (initialPosition.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, initialPosition.position) < 0.1f)
        {
            isReturning = false;
        }
    }

    private void FireProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    }
}
