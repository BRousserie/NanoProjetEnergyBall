﻿using UnityEngine;

public class AudioManager : MonoBehaviour

    
{
    public static bool exist = false;
    public static AudioManager instance;

    [Space(10)]
    [Header("Events")]

    

    //Wwise Events
    public AK.Wwise.Event dash_green;
    public AK.Wwise.Event dash_blue;
    public AK.Wwise.Event jump;
    public AK.Wwise.Event battle_Scene;
    public AK.Wwise.Event music_menu;
    public AK.Wwise.Event ball_get;
    public AK.Wwise.Event throw_ball;
    public AK.Wwise.Event dash_hit;
    public AK.Wwise.Event ball_hit;
    public AK.Wwise.Event ball_bounce;
    public AK.Wwise.Event ball_air;
    public AK.Wwise.Event start_horn;
    public AK.Wwise.Event ambiance;
    public AK.Wwise.Event navigate;
    public AK.Wwise.Event validate;
    public AK.Wwise.Event begin;


    [Space(10)]
    [Header("Game Parameters")]
    
    //Wwise Game Parameters
    public AK.Wwise.RTPC ball_velocity;
    public AK.Wwise.State ball_idle;
    public AK.Wwise.State ball_charged;


    // Start is called before the first frame update
    void Awake()
    {
        exist = true;
        instance = this;

        Battle_Scene(gameObject);
        Ambiance(gameObject);

    }


    public static void Dash_Green(GameObject gameObject) {
        if (exist) instance.dash_green.Post(gameObject);
    }
    public static void Dash_Blue(GameObject gameObject)
    {
        if (exist) instance.dash_blue.Post(gameObject);
    }
    public static void Jump(GameObject gameObject) {
        if (exist) instance.jump.Post(gameObject);
    }
    public static void Battle_Scene(GameObject gameObject) {
        if (exist) instance.battle_Scene.Post(gameObject);
    }
    public static void Music_Menu(GameObject gameObject)
    {
        if (exist) instance.music_menu.Post(gameObject);
    }
    public static void Ball_Get(GameObject gameObject) {
        if (exist) instance.ball_get.Post(gameObject);
    }
    public static void Throw_Ball(GameObject gameObject) {
        if (exist) instance.throw_ball.Post(gameObject);
    }
    public static void Dash_Hit(GameObject gameObject) {
        if (exist) instance.dash_hit.Post(gameObject);
    }
    public static void Ball_Hit(GameObject gameObject) {
        if (exist) instance.ball_hit.Post(gameObject);
    }
    public static void Ball_Bounce(GameObject gameObject) {
        if (exist) instance.ball_bounce.Post(gameObject);
    }
    public static void Ball_Air(GameObject gameObject) {
        if (exist) instance.ball_air.Post(gameObject);
    }
    public static void Start_Horn(GameObject gameObject)
    { 
        if (exist) instance.start_horn.Post(gameObject);
    }
    public static void Ambiance(GameObject gameObject)
    {
        if (exist) instance.ambiance.Post(gameObject);
    }
    public static void Navigate(GameObject gameObject)
    {
        if (exist) instance.navigate.Post(gameObject);
    }
    public static void Validate(GameObject gameObject)
    {
        if (exist) instance.validate.Post(gameObject);
    }
    public static void Begin(GameObject gameObject)
    {
        if (exist) instance.begin.Post(gameObject);
    }


    public static void Ball_Velocity(float value)
    {
        //Debug.Log(value);
        if (exist) instance.ball_velocity.SetGlobalValue(value);
    }

    public static void Ball_Idle()
    {
        //Debug.Log(value);
        if (exist) instance.ball_idle.SetValue();
    }

    public static void Ball_Charegd()
    {
        //Debug.Log(value);
        if (exist) instance.ball_charged.SetValue();
    }



    void update()
    {
        
    }
}

