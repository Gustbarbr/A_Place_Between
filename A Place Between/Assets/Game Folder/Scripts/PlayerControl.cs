using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    DamageOnEnemy damageEnemy;

    // Movimentacao
    private float movementSpeed = 3;
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

    // Flags do chapéu de velocidade
    public bool hatOfAviator = false;
    public bool hatOfAviatorEquipped = false;
    // Flags do chapéu de aumento de dano do player
    public bool hatOfWarlord = false;
    public bool hatOfWarlordEquipped = false;
    // Flags do chapéu de redução de custo de FL da lanterna
    public bool hatOfMiner = false;
    public bool hatOfMinerEquipped = false;
    public bool reduceCost = false;

    void Start()
    {
        // Guarda o componente RigidBody2D na variavel
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageEnemy = FindObjectOfType<DamageOnEnemy>();
        flashlight.gameObject.SetActive(false);
    }

    void Update()
    {
        // Cuida da posição do mouse e tudo relacionado a lanterna
        ViewAndLanternDirection();

        // Movimenta o player
        MovePlayer();

        // Equipar o chapéu
        EquipHat();

        // Checa a todo instante se o chapéu ainda está equipado ou não
        if (!hatOfMinerEquipped)
        {
            reduceCost = false;
        }

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
        if (flashlight.gameObject.activeSelf && !reduceCost)
        {
            flashlightSlider.value -= Time.deltaTime / 10;
        }

        // Consome FL caso a lanterna esteja ligada
        else if (flashlight.gameObject.activeSelf && reduceCost)
        {
            flashlightSlider.value -= Time.deltaTime / 20;
        }

        // Recupera FL automaticamente se a lanterna estiver desligada
        if (!flashlight.gameObject.activeSelf)
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

    void EquipHat()
    {
        // Equipar chapéu de aviador (velocidade)
        if(Input.GetKeyDown(KeyCode.Alpha8)){
            hatOfAviatorEquipped = true;
            hatOfWarlordEquipped = false;
            hatOfMinerEquipped = false;
            if (hatOfAviator && hatOfAviatorEquipped){
                movementSpeed += 2.5f;
            }
        }

        // Equipar chapéu de chefe de guerra (aumento de dano)
        if(Input.GetKeyDown(KeyCode.Alpha9)){
            hatOfAviatorEquipped = false;
            hatOfWarlordEquipped = true;
            hatOfMinerEquipped = false;
            if (hatOfWarlord && hatOfWarlordEquipped){
                damageEnemy.damage *= 2;
            }
        }

        // Equipar chapéu de mineiro (redução do custo de FL)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            hatOfWarlordEquipped = false;
            hatOfAviatorEquipped = false;
            hatOfMinerEquipped = true;
            if (hatOfMiner && hatOfMinerEquipped)
            {
                reduceCost = true;
            }
        }
    }

}
