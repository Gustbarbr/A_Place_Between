using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_AmuletOfHPIncrease : MonoBehaviour
{
    private Animator animator;
    PlayerControl player;
    public Image imagemHUD;
    bool equipped = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerControl>();

        imagemHUD.enabled = false;
    }

    void Update()
    {
        if (player.AmuletOfHPIncrease && player.AmuletOfHPIncreaseEquipped && equipped == false)
        {
            animator.SetTrigger("EquipAmuletOfHPIncrease");
            equipped = true;
        }

        else if (!player.AmuletOfHPIncreaseEquipped && equipped == true)
        {
            animator.SetTrigger("UnequipAmuletOfHPIncrease");
            equipped = false;
        }
    }

    public void ShowImage()
    {
        imagemHUD.enabled = true;
    }

    public void HideImage()
    {
        imagemHUD.enabled = false;
    }
}
