using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    Animator animator;

    // Armazena todas as animações de idle do animator
    public string[] idleAnimations = { "PlayerIdleUp", "PlayerIdleUpRight", "PlayerIdleRight", "PlayerIdleDownRight", "PlayerIdleDown", "PlayerIdleDownLeft", "PlayerIdleLeft", "PlayerIdleUpLeft" };
    public string[] walkAnimations = { "PlayerWalkingUp", "PlayerWalkingUpRight", "PlayerWalkingRight", "PlayerWalkingDownRight", "PlayerWalkingDown", "PlayerWalkingDownLeft", "PlayerWalkingLeft", "PlayerWalkingUpLeft" };

    int lastDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 playerDirection)
    {
        string[] directionArray = null;
        // Verifica se o player está parado
        if(playerDirection.magnitude < 0.01)
        {
            directionArray = idleAnimations;
        }

        animator.Play(directionArray[lastDirection]);
    }
}
