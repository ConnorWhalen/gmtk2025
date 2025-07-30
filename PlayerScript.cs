using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] LandInputSub GetInput;
    public Rigidbody2D rb;
    
    Vector2 PlayerMovement;
    float Speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        PlayerMovement = new Vector2(GetInput.MovementInput.x, GetInput.MovementInput.y);
        rb.linearVelocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * Speed;
    }

}
