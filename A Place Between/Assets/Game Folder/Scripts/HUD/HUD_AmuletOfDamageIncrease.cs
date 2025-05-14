using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_AmuletOfDamageIncrease : MonoBehaviour
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
        if (player.AmuletOfDamageIncrease && player.AmuletOfDamageIncreaseEquipped && equipped == false)
        {
            animator.SetTrigger("EquipAmuletOfDamageIncrease");
            equipped = true;
        }

        else if (!player.AmuletOfDamageIncreaseEquipped && equipped == true)
        {
            animator.SetTrigger("UnequipAmuletOfDamageIncrease");
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
