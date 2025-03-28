using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    // Movimentacao
    public float movementSpeed = 5f;
    public bool walk;

    // Variavel para capturar o objeto "Lantern"
    public Transform lantern;

    // Ataque
    public Rigidbody2D bulletPrefab;
    public float attackSpeed = 0.3f;
    float attackCooldown = 0.5f;
    public float attackTimer;
    float projectileSpeed = 50f;

    void Start()
    {
        // Guarda o componente RigidBody2D na variavel
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Captura a posicao do mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (new Vector2(mousePosition.x, mousePosition.y) - (Vector2)transform.position).normalized;

        // Atualiza a lanterna para apontar para a direcao do mouse
        lantern.up = direction;

        Debug.Log(direction);
        // Movimenta o player
        MovePlayer();

        // Define o botao de ataque
        bool fire = Input.GetKey(KeyCode.Mouse0);

        // Atualiza o temporizador de ataque
        attackTimer += Time.deltaTime;

        // Se o botao for pressionado e o tempo de recarga ja passou, chama a funcao de disparo
        if (fire && attackTimer >= attackCooldown)
        {
            Fire();
        }
    }

    /*void Direction()
    {

        // Muda a animação com base na direção do mouse (olha para cima)
        if (direction.x > -0.5 && direction.x < 0.5 && direction.y >= 0 && walk == true)
        {
            animator.SetFloat("VelocityUp", 1);
        }
        else if (direction.x > -0.5 && direction.x < 0.5 && direction.y >= 0 && walk == false)
        {
            animator.SetFloat("VelocityUp", 0);
            animator.SetBool("IdleUp", true);
        }
    }*/

    void MovePlayer()
    {
        // Guarda os valores de entrada horizontal e vertical
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        float verticalMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        // Define a velocidade do jogador
        rb.velocity = new Vector2(horizontalMovement, verticalMovement);

        if(horizontalMovement != 0 || verticalMovement != 0){
            walk = true;
        }

        else{
            walk = false;
        }
    }

    void Fire()
    {
        // Instancia a bala na posicao do jogador (transform.position)
        Rigidbody2D bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as Rigidbody2D;

        // Obtem a posicao do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calcula a direcao entre o jogador e o mouse
        Vector2 fireDirection = (new Vector2(mousePosition.x, mousePosition.y) - (Vector2)transform.position).normalized;

        // Ajusta a velocidade diretamente na direcao do mouse
        bullet.velocity = fireDirection * projectileSpeed;  // Movimenta a bala com a velocidade m�xima na dire��o do mouse

        // Reseta o tempo de recarga do ataque
        attackTimer = 0;
    }

}
