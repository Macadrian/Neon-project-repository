using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Inicialización de Datos

    [Header("Variables de movimiento")]
    [SerializeField] private float velocidadMovimiento = 20f;
    [SerializeField] private float velocidadCaidaSlide = 5f;
    [SerializeField] [Range(0f, .3f)] private float smoothMovimiento = 0.15f;
    [Space(10)]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float radiusGroundCheck = 0.05f;
    [Space(10)]
    [SerializeField] private LayerMask whatIsGround;


    [Header("Elementos")]
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform wallChecker;

    private Rigidbody2D rgbd;
    private Animator animator;

    private Vector3 m_Velocity = Vector3.zero;

    private bool isGrounded = true;
    private bool isWalled = false;
    private bool isSliding = false;
    private bool mirandoIzquierda = true;

    private const sbyte not = -1;
    private const byte zero = 0;

    #endregion

    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || isSliding))
        {
            Saltar();
            //Actualizar animacion salto
        }
        animator.SetBool("isJumping", !isGrounded);
    }

    private void FixedUpdate()
    {
        checkPlayerOverlaps();
        Mover();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = (isGrounded)? Color.cyan : Color.blue;
        Gizmos.DrawWireSphere(groundChecker.position, radiusGroundCheck);

        Gizmos.color = (isWalled) ? Color.cyan : Color.blue;
        Gizmos.DrawWireSphere(wallChecker.position, radiusGroundCheck);

    }

    #region Funciones Propias

    private void Saltar()
    {
        var saltoFinal = Vector2.up * jumpForce;
        rgbd.velocity += saltoFinal;
        isGrounded = false;
    }

    private void checkPlayerOverlaps()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, radiusGroundCheck, whatIsGround);
        isWalled = Physics2D.OverlapCircle(wallChecker.position, radiusGroundCheck, whatIsGround);
    }

    private void Mover()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        horizontalMovement *= Time.fixedDeltaTime;

        var targetVelocity = new Vector2(horizontalMovement * velocidadMovimiento, rgbd.velocity.y);

        //Comprobrar si se está produciendo Slide Wall y modificar velocidad de caída en consecuencia
        targetVelocity = ComprobarSlideWall(horizontalMovement, targetVelocity);

        rgbd.velocity = Vector3.SmoothDamp(rgbd.velocity, targetVelocity, ref m_Velocity, smoothMovimiento);

        //Actualizar animaciones
        if (horizontalMovement > zero && !mirandoIzquierda) { FlipCharacter(); }
        else if (horizontalMovement < zero && mirandoIzquierda) { FlipCharacter(); }

        animator.SetFloat("velocidadX", Mathf.Abs(horizontalMovement));
        animator.SetFloat("velocidadY", rgbd.velocity.y);
    }

    private Vector2 ComprobarSlideWall(float horizontalMovement, Vector2 targetVelocity)
    {
        if (!isGrounded & isWalled && Mathf.Abs(horizontalMovement) > zero && targetVelocity.y < zero)
        {
            targetVelocity = new Vector2(targetVelocity.x, -velocidadCaidaSlide);
            isSliding = true;
        }
        else { isSliding = false; }

        return targetVelocity;
    }

    private void FlipCharacter()
    {
        mirandoIzquierda = !mirandoIzquierda;

        var escala = transform.localScale;
        escala.x *= not;
        transform.localScale = escala;
    }
    #endregion
}