using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_AmuletOfFLCostReduction : MonoBehaviour
{
    private Animator animator;
    PlayerControl player;
    public Image imagemHUD;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerControl>();

        imagemHUD.enabled = false;
    }

    void Update()
    {
        if (player.AmuletOfVelocityEquipped)
        {
            imagemHUD.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("ChangeAmulet", true);
            }
        }
    }
}
