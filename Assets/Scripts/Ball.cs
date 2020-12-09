﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    private Rigidbody2D rb;

    public PhysicsMaterial2D material;

    public bool sleeping;
    private float timerUntilCatchable = 0;

    public Vector2 speedWhenFreeFromDash = new Vector2(0, 10);

    public float gravityWhenThorwn = 0.2f;
    public float gravity = 2f;
    public float maxSpeedSound = 15;

    [HideInInspector]
    public Player player;

    [HideInInspector]
    public bool charged = false;
    [HideInInspector]
    public Player previousPlayer;

    [HideInInspector]
    public Player previousPlayertouched;
    [HideInInspector]
    public float previousPlayertouchedTimeStamp;
    [HideInInspector]
    public float timeBeforeballCanBeCatchBySamePlayer = 0.2f;

    public float collisionRadius = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        sleeping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            transform.position = player.transform.position;
        }
    }

    public bool Catch(Player player) {
        if (!sleeping && timerUntilCatchable + 0.2f < Time.time) {
            this.player = player;
            sleeping = true;
            rb.Sleep();
            return true;
        }else {
            return false;
        }
    }

    public void Throw(Vector2 dir, float force) {
            Charge();
            Free();
            rb.velocity = Vector2.zero;
            rb.velocity += dir.normalized * force;
    }

    public void Free() {
        player = null;
        sleeping = false;
        rb.WakeUp();
        timerUntilCatchable = Time.time;
    }

    public void Charge() {
        previousPlayer = player;
        rb.gravityScale = 0;
        charged = true;
    }

    public void Uncharge() {
        charged = false;
        rb.gravityScale = gravity;
    }

    public void MoveToPreviousPlayer() {

    }

    public void Hit() {
        Uncharge();
        MoveToPreviousPlayer();
    }

    public void SetSpeed(Vector2 speed) {
        rb.velocity = speed;
    }

    public void FakeCollision() {
        rb.velocity = -rb.velocity * material.bounciness;
    }

    public Vector2 GetSpeed() {
        return rb.velocity;
    }

    public float GetSpeedSound() {
        float speed = rb.velocity.magnitude;
        if (speed < 0)
            speed = 0;
        if (speed > 15)
            speed = 15;

        return speed / maxSpeedSound;
    }

    private void OnDrawGizmos() {
        //Gizmos.color = Color.green;


        //Gizmos.DrawWireSphere((Vector2)transform.position, collisionRadius);
    }

    public void SetSpeedWhenFreeFromDash() {
        SetSpeed(speedWhenFreeFromDash);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Vector3 normal = collision.contacts[0].normal;
        
        if (charged) {
            if (collision.gameObject.tag == "wall") {
                Hit();
            }
        }
    }

}
