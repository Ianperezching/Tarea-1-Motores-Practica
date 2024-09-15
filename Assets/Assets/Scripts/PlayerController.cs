using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private GameObject bulletPrefab; // Prefab del proyectil
    [SerializeField] private Transform firePoint; // Punto de origen del proyectil
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float rayDistance = 10f;

    private void Update()
    {
        // Lanzar un raycast desde el jugador en la dirección del puntero del ratón
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition - (Vector2)transform.position, rayDistance);

        // Si se detecta un clic izquierdo o derecho, disparar en la dirección del raycast
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet(mousePosition);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            FireBullet(mousePosition);
        }

        // Voltear el sprite del jugador según la posición del ratón
        CheckFlip(mousePosition.x);
    }

    public void Movimiento(InputAction.CallbackContext context)
    {
        Vector2 movementPlayer = context.ReadValue<Vector2>();
        myRBD2.velocity = movementPlayer * velocityModifier;

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }

    private void FireBullet(Vector2 targetPosition)
    {
        // Calcular la dirección hacia el punto de destino
        Vector2 direction = (targetPosition - (Vector2)firePoint.position).normalized;

        // Calcular el ángulo de rotación
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Crear una instancia del prefab de la bala en el punto de origen del proyectil con la rotación correcta
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
