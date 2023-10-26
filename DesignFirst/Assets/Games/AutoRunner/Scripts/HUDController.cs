using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HUDController : MonoBehaviour
{
    private TextMeshProUGUI textmesh;
    public CharacterController playerCC_;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player");
        playerCC_ = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        textmesh.text = playerCC_.coins_.ToString("0");
    }
}
