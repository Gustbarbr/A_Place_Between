using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletAmount : MonoBehaviour
{

    public TextMeshProUGUI ammoText;
    PlayerControl player;

    int ammunation = 0;

    void Start(){
        player = FindObjectOfType<PlayerControl>();
        ammunation = player.ammunation;
        DontDestroyOnLoad(gameObject);
        ammoText.text = ammunation.ToString();
    }

    public void AddAmmunation(){
        ammunation+=player.ammunation;
        ammoText.text = ammunation.ToString();
    }
}
