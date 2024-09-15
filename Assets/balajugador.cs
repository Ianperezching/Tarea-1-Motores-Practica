using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balajugador : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector2 direction;

    // M�todo para establecer la direcci�n de la bala
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    // M�todo para inicializar la direcci�n de la bala
    public void InitializeDirection(Vector2 initialDir)
    {
        direction = initialDir.normalized;
    }

    void Update()
    {
        // Movimiento de la bala en la direcci�n establecida
        transform.Translate(direction * speed * Time.deltaTime);

    }
}
