using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balajugador : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector2 direction;

    // Método para establecer la dirección de la bala
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    // Método para inicializar la dirección de la bala
    public void InitializeDirection(Vector2 initialDir)
    {
        direction = initialDir.normalized;
    }

    void Update()
    {
        // Movimiento de la bala en la dirección establecida
        transform.Translate(direction * speed * Time.deltaTime);

    }
}
