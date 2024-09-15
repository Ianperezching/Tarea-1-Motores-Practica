using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float detectionRange = 5f; // Rango de detección del raycast
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private bool playerDetected = false;

    private void Start()
    {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update()
    {
        if (!playerDetected)
            Patrol();
        else
            ChasePlayer();

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
    }

    private void Patrol()
    {
        CheckNewPoint();

        // Dibuja el raycast en la escena para visualizarlo
        Debug.DrawRay(transform.position, currentPositionTarget.position - transform.position, Color.green);

        // Aquí puedes agregar el código para el raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentPositionTarget.position - transform.position, detectionRange);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void ChasePlayer()
    {
        // Mover más rápido hacia la dirección original
        myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier * 2f;
        CheckFlip(myRBD2.velocity.x);

        // Si pierde visión del jugador, regresar a estado normal
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentPositionTarget.position - transform.position, detectionRange);
        if (hit.collider == null || !hit.collider.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    private void CheckNewPoint()
    {
        if (Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25)
        {
            patrolPos = (patrolPos + 1) % checkpointsPatrol.Length;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}
