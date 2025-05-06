using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_AmuletOfFLCostReduction : MonoBehaviour
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
        if (player.AmuletOfFLCostReductionEquipped && equipped == false)
        {
            animator.SetTrigger("EquipAmulet");
            equipped = true;
        }

        else if (!player.AmuletOfFLCostReductionEquipped && equipped == true)
        {
            animator.SetTrigger("UnequipAmulet");
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
