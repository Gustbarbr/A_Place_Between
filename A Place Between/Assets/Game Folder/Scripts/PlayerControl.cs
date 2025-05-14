using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    DamageOnEnemy damageEnemy;
    Light2D light2d;

    public bool gunInInventory = false;
    public bool lanternInInventory = false;

    AudioSource audioSource;

    public AudioClip tiroSound;
    public AudioClip lanternaOnSound;
    public AudioClip lanternaOffSound;

    public float healthRegen;
    public float healthRegenCooldown = 10;

    // Movimentacao
    float movementSpeed = 3;
    [HideInInspector] public bool walk;
    bool moveSpeedIncreased = false;

    public Slider hpSlider;
    [HideInInspector] public bool increaseHP = false;

    // Variavel para capturar o objeto "Flashlight"
    public Transform flashlight;

    // Ataque
    public Rigidbody2D bulletPrefab;
    float attackSpeed = 0.3f;
    float attackCooldown = 0.5f;
    float attackTimer;
    float projectileSpeed = 50f;
    public int ammunation = 10;
    public Slider flashlightSlider;

    // Flags do amuleto de velocidade
    [HideInInspector] public bool AmuletOfVelocity = false;
    [HideInInspector] public bool AmuletOfVelocityEquipped = false;
    // Flags do amuleto de aumento de dano do player
    [HideInInspector] public bool AmuletOfDamageIncrease = false;
    [HideInInspector] public bool AmuletOfDamageIncreaseEquipped = false;
    // Flags do amuleto de redução de custo de FL da lanterna
    [HideInInspector] public bool AmuletOfFLCostReduction = false;
    [HideInInspector] public bool AmuletOfFLCostReductionEquipped = false;
    bool reduceCost = false;
    // Flags do amuleto de incremento de HP
    [HideInInspector] public bool AmuletOfHPIncrease = false;
    [HideInInspector] public bool AmuletOfHPIncreaseEquipped = false;

    public bool SacredAmulet1 = false;
    public bool SacredAmulet2 = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageEnemy = FindObjectOfType<DamageOnEnemy>();
        audioSource = GetComponent<AudioSource>();
        light2d = flashlight.GetComponent<Light2D>();
        flashlight.gameObject.SetActive(false);
    }

    void Update()
    {
                //<ALAN> CODIGO ADICIONADO PARA FINS DE TESTE
        //TODO APAGAR 
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Outside");
        }

        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Floresta");
        }

        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("Boss2");
        }
        //</ALAN>


        // Cuida da posição do mouse e tudo relacionado a lanterna
        ViewAndLanternDirection();

        // Movimenta o player
        MovePlayer();

        HealthRegeneration();

        Die();

        // Equipar o amuleto
        EquipAmulet();

        // Define o botao de ataque
        bool fire = Input.GetKey(KeyCode.Mouse0);

        // Atualiza o temporizador de ataque
        attackTimer += Time.deltaTime;

        // Se o botao for pressionado e o tempo de recarga ja passou, chama a funcao de disparo
        if (fire && attackTimer >= attackCooldown && ammunation >= 1 && gunInInventory)
        {
            Fire();
            ammunation -= 1;
            FindObjectOfType<BulletAmount>().UpdateAmmoText();
        }

        // Se o amuleto de velocidade não estiver equipado, a velocidade volta ao normal
        if (!AmuletOfVelocityEquipped)
        {
            movementSpeed = 3;
            moveSpeedIncreased = false;
        }

        // Se o amuleto de redução de custo de bateria não estiver equipado, a bateria é gasta normalmente
        if (!AmuletOfFLCostReductionEquipped)
        {
            reduceCost = false;
        }

        // Se o amuleto de aumento de dano não estiver equipado, o dano volta ao normal
        if (!AmuletOfDamageIncreaseEquipped)
        {
            damageEnemy.damage = 1;
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
        if (Input.GetKeyDown(KeyCode.Q) && lanternInInventory && flashlightSlider.value >= 0.1f && flashlight.gameObject.activeSelf == false)
        {
            flashlight.gameObject.SetActive(true);
            
            audioSource.clip = lanternaOnSound;
            audioSource.Play();
        }

        // Desliga a lanterna se ela estiver ligada e o "Q" for pressionado ou se a barra de FL esvaziar
        else if (Input.GetKeyDown(KeyCode.Q) && lanternInInventory && flashlight.gameObject.activeSelf == true || flashlightSlider.value <= 0)
        {
            flashlight.gameObject.SetActive(false);
            audioSource.clip = lanternaOffSound;
            audioSource.Play();
        }

        // Consome FL caso a lanterna esteja ligada
        if (flashlight.gameObject.activeSelf && !reduceCost)
        {
            light2d.intensity -= Time.deltaTime * 0.1f;
            flashlightSlider.value -= Time.deltaTime / 10;
        }

        // Consome FL caso a lanterna esteja ligada
        else if (flashlight.gameObject.activeSelf && reduceCost)
        {
            light2d.intensity -= Time.deltaTime * 0.05f;
            flashlightSlider.value -= Time.deltaTime / 20;
        }

        // Recupera FL automaticamente se a lanterna estiver desligada
        if (!flashlight.gameObject.activeSelf)
        {
            if(light2d.intensity <= 1)
            {
                light2d.intensity += Time.deltaTime * 0.1f;
            }
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

        audioSource.clip = tiroSound;
        audioSource.Play();

        // Reseta o tempo de recarga do ataque
        attackTimer = 0;
    }

    void HealthRegeneration()
    {
        healthRegen += Time.deltaTime;
        if (healthRegen >= healthRegenCooldown)
        {
            hpSlider.value += Time.deltaTime * 0.1f;
        }
    }

    void Die()
    {
        if(hpSlider.value < 0.2f || Input.GetKeyDown("k")) { 
            if (SceneManager.GetActiveScene().name == "Floresta"){
                SceneManager.LoadScene("Death");
            }
            else{
                SceneManager.LoadScene("Opening");
            }

        }
    }

    void EquipAmulet()
    {
        // Equipar amuleto de aumento de velocidade
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            AmuletOfVelocityEquipped = true;
            AmuletOfDamageIncreaseEquipped = false;
            AmuletOfFLCostReductionEquipped = false;
            AmuletOfHPIncreaseEquipped = false;
            if (AmuletOfVelocity && AmuletOfVelocityEquipped && !moveSpeedIncreased){
                movementSpeed += 2.5f;
                moveSpeedIncreased = true;
            }
        }

        // Equipar amuleto de aumento de dano
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            AmuletOfVelocityEquipped = false;
            AmuletOfDamageIncreaseEquipped = true;
            AmuletOfFLCostReductionEquipped = false;
            AmuletOfHPIncreaseEquipped = false;
            if (AmuletOfDamageIncrease && AmuletOfDamageIncreaseEquipped){
                damageEnemy.damage *= 2;
            }
        }

        // Equipar amuleto de redução do custo de FL
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AmuletOfVelocityEquipped = false;
            AmuletOfDamageIncreaseEquipped = false;
            AmuletOfFLCostReductionEquipped = true;
            AmuletOfHPIncreaseEquipped = false;
            if (AmuletOfFLCostReduction && AmuletOfFLCostReductionEquipped)
            {
                reduceCost = true;
            }
        }

        // Equipar amuleto de aumento de HP
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AmuletOfVelocityEquipped = false;
            AmuletOfDamageIncreaseEquipped = false;
            AmuletOfFLCostReductionEquipped = false;
            AmuletOfHPIncreaseEquipped = true;
            if (AmuletOfHPIncrease && AmuletOfHPIncreaseEquipped)
            {
                increaseHP = true;
            }
        }
    }

}
