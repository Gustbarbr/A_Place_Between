using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_AmuletOfVelocity : MonoBehaviour
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
        if (player.AmuletOfVelocity && player.AmuletOfVelocityEquipped && equipped == false)
        {
            animator.SetTrigger("EquipAmuletOfVelocity");
            equipped = true;
        }

        else if (!player.AmuletOfVelocityEquipped && equipped == true)
        {
            animator.SetTrigger("UnequipAmuletOfVelocity");
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
