using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    Animator animator;

    // Armazena todas as anima��es de idle do animator
    public string[] idleAnimations = { "PlayerIdleUp", "PlayerIdleUpLeft", "PlayerIdleLeft", "PlayerIdleDownLeft", "PlayerIdleDown", "PlayerIdleDownRight", "PlayerIdleRight", "PlayerIdleUpRight" };
    // Armazena todas as anima��es de andar do animator
    public string[] walkAnimations = { "PlayerWalkingUp", "PlayerWalkingUpLeft", "PlayerWalkingLeft", "PlayerWalkingDownLeft", "PlayerWalkingDown", "PlayerWalkingDownRight", "PlayerWalkingRight", "PlayerWalkingUpRight" };

    int lastDirection;

    void Start()
    {
        animator = GetComponent<Animator>();

        float result1 = Vector2.SignedAngle(Vector2.up, Vector2.right);
        float result2 = Vector2.SignedAngle(Vector2.up, Vector2.left);
        float result3 = Vector2.SignedAngle(Vector2.up, Vector2.down);
    }

    // Basicamente vai comparar cada direção com um elemento dentro de um array e escolher qual seria a animação correta com base nisso
    public void SetDirection(Vector2 playerDirection)
    {
        string[] directionArray = null;
        // Verifica se o player est� parado, ou seja, se está idle
        if(playerDirection.magnitude < 0.01)
        {
            directionArray = idleAnimations;
        }
        else{
            directionArray = walkAnimations;

            // Procura o indice da animação referente
            lastDirection = DirectionToIndex(playerDirection);
        }

        // Toca a animação que deu match no array
        animator.Play(directionArray[lastDirection]);
    }

    // Converte as informações do vector2 em um indice, e funciona no sentido anti-horário (cima, esquerda, baixo, direita)
    private int DirectionToIndex(Vector2 playerDirection){
        
        // Retorna a magnitude do vetor como 1 e o indice normalized
        // Magnitude = tamanho do vetor; normalize = transformar em vetor unitário que aponta para a mesma direção que o original
        Vector2 normalizeDirection = playerDirection.normalized;

        // Divide um circulo (referente as animações, pois elas podem olhar para todas as direções) por 8 (numero de animações)
        float step = 360 / 8;

        // Auxilia no calculo e garante que o indice do array esta correto
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normalizeDirection);
        
        angle += offset;

        // Impede que o angulo seja negativo (saia do circulo de animação)
        if(angle < 0){
            angle += 360;
        }

        float stepCount = angle/step;

        return Mathf.FloorToInt(stepCount);
    }
}
