﻿using UnityEngine;
using UnityEngine.VFX;

public class Ball : MonoBehaviour
{
    
    private Rigidbody2D rb;

    public PhysicsMaterial2D material;

    public bool sleeping;
    private float timerUntilCatchable = 0;

    public Vector2 speedWhenFreeFromDash = new Vector2(0, 10);

    public float gravityWhenThorwn = 0.2f;
    public float gravity = 2f;

    private float maxSpeedSound = 15;
    private float speedSound = 0;

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
    public Color ballColor;
    public VisualEffect ballEffect;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Ball_Idle();
        AudioManager.Ball_Air(gameObject);
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        sleeping = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        GetSpeedSound();
        AudioManager.Ball_Velocity(speedSound);
        if (player != null) {
            transform.position = player.transform.position;
        }
    }

    public bool Catch(Player player) {
        if (!sleeping && timerUntilCatchable + 0.2f < Time.time) {
            ballEffect.SetVector4("Color", player.color);
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
        AudioManager.Ball_Charegd();
        previousPlayer = player;
        rb.gravityScale = 0;
        charged = true;
    }

    public void Uncharge()
    {
        if (charged)
            VFXManager.Spawn(VFXManager.Instance.BallImpact, transform.position, previousPlayer.color);
        ballEffect.SetVector4("Color", ballColor);
        AudioManager.Ball_Idle();
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

    public void GetSpeedSound() {
        if (!sleeping) {
            float speed = rb.velocity.magnitude;
            if (speed < 0)
                speed = 0;
            if (speed > 15)
                speed = 15;

            speedSound =((speed / maxSpeedSound) * 100);
        } else {
            speedSound = 0;
        }
    }

    public void StopSound() {
        AudioManager.Ball_Air_Stop(gameObject);
    }

    private void OnDrawGizmos() {
        //Gizmos.color = Color.green;


        //Gizmos.DrawWireSphere((Vector2)transform.position, collisionRadius);
    }

    public void SetSpeedWhenFreeFromDash() {
        SetSpeed(speedWhenFreeFromDash);
    }

    void OnCollisionEnter2D(Collision2D collision) {


        if (!sleeping) {

            Vector3 normal = collision.contacts[0].normal;

            if ((collision.gameObject.tag == "plateform") && collision.GetContact(0).normalImpulse != 0) {
                AudioManager.Ball_Bounce(gameObject);
                Hit();
            }
            if (collision.gameObject.tag == "wall") {
                if (charged) {
                    Hit();
                }
                AudioManager.Ball_Bounce(gameObject);
            }
            
        }
        
    }

}
