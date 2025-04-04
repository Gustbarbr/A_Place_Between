using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    // Movimentacao
    public float movementSpeed = 5f;
    public bool walk;

    // Variavel para capturar o objeto "Flashlight"
    public Transform flashlight;

    // Ataque
    public Rigidbody2D bulletPrefab;
    public float attackSpeed = 0.3f;
    float attackCooldown = 0.5f;
    public float attackTimer;
    float projectileSpeed = 50f;

    public Slider flashlightSlider;

    void Start()
    {
        // Guarda o componente RigidBody2D na variavel
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flashlight.gameObject.SetActive(false);
    }

    void Update()
    {
        // Cuida da posição do mouse e tudo relacionado a lanterna
        ViewAndLanternDirection();

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

    public void ViewAndLanternDirection()
    {
        // Captura a posicao do mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (new Vector2(mousePosition.x, mousePosition.y) - (Vector2)transform.position).normalized;

        // Atualiza a lanterna para apontar para a direcao do mouse
        flashlight.up = direction;

        // A lanterna só liga se tiver com a barr de FL (FlashLight) 10% preenchida e se ela estiver desligada
        if (Input.GetKeyDown(KeyCode.Q) && flashlightSlider.value >= 0.1f && flashlight.gameObject.activeSelf == false)
        {
            flashlight.gameObject.SetActive(true);
        }

        // Desliga a lanterna se ela estiver ligada e o "Q" for pressionado ou se a barra de FL esvaziar
        else if (Input.GetKeyDown(KeyCode.Q) && flashlight.gameObject.activeSelf == true || flashlightSlider.value <= 0)
        {
            flashlight.gameObject.SetActive(false);
        }

        // Consome FL caso a lanterna esteja ligada
        if (flashlight.gameObject.activeSelf == true)
        {
            flashlightSlider.value -= Time.deltaTime / 10;
        }

        // Recupera FL automaticamente se a lanterna estiver desligada
        else if (flashlight.gameObject.activeSelf == false)
        {
            flashlightSlider.value += Time.deltaTime * 0.1f;
        }

        // Define a direção da lanterna como ponto para onde o player irá olhar
        FindObjectOfType<PlayerAnimations>().SetDirection(direction);
    }

    public void MovePlayer()
    {
        // Guarda os valores de entrada horizontal e vertical
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        float verticalMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        // Define a velocidade do jogador
        rb.velocity = new Vector2(horizontalMovement, verticalMovement);

        if (horizontalMovement != 0 || verticalMovement != 0) {
            walk = true;
        }

        else
        {
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
