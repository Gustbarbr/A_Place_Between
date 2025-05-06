using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletAmount : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    PlayerControl player;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        DontDestroyOnLoad(gameObject);
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
        ammoText.text = player.ammunation.ToString();
    }
}

