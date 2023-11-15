using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boost : MonoBehaviour
{
    private PlayerMover Player;
    private bool MouseDown;

    public float PlayerMoverMaxSpeed;
    public float PlayerMoverAceel;
    public float NewMaxSpeed = 15f;
    public float NewMaxAccel = 10f;
    public float SpeedBoost = 1f;
    public float AccelBoost = 1f;

    private void Start()
    {
        Player = GetComponent<PlayerMover>();
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
         MouseDown = Input.GetMouseButton(0);

        if (MouseDown == true)
        {
            if(Player.acceleration < NewMaxAccel)
            {
                Player.acceleration += AccelBoost;
            }
            
            if (Player.maxSpeed < NewMaxSpeed)
            {
                Player.maxSpeed += SpeedBoost;
            }
            //Debug.Log("Pressed");
        }
        else
        {
            MouseDown = false;
            Player.acceleration = PlayerMoverAceel;
            Player.maxSpeed = PlayerMoverMaxSpeed;
        }
    }
}
