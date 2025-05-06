using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_AmuletOfHPIncrease : MonoBehaviour
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
        if (player.AmuletOfHPIncrease)
        {
            imagemHUD.enabled = true;
            if (player.AmuletOfHPIncreaseEquipped)
            {
                animator.SetBool("EquipAmulet", true);
            }
        }
        else if (!player.AmuletOfHPIncreaseEquipped)
        {
            imagemHUD.enabled = false;
        }
    }
}
