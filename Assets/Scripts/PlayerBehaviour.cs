using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;
using UnityEditor.Timeline.Actions;

public class PlayerController : MonoBehaviour
{
    private InputAction Moves;

    private PlayerInput playerInputInstances;
    private Rigidbody2D rb2d;
    private GameObject player;
    private InputAction Shoot;
    private GameObject tank;
    private GameObject Bullet;



    private InputAction PlayerAction;
    private InputAction PlayerAction2;

   private PlayerInput PlayerInputInstances;

    public bool Moving;
    public bool Moving2;

    public float MoveDirection;
    public float MoveDirection2;

    public InputAction Shoot1 { get => Shoot; set => Shoot = value; }

    public GameObject Bullet1 { get => Bullet; set => Bullet = value; }

    void Start()
    {
        playerInputInstances = GetComponent<PlayerInput>();

        playerInputInstances.currentActionMap.Enable();

        rb2d = player.GetComponent<Rigidbody2D>();

        PlayerAction = playerInputInstances.currentActionMap.FindAction("W/S");

        PlayerAction2 = playerInputInstances.currentActionMap.FindAction("Arrows");

        PlayerAction.started += PlayerAction_started;

        PlayerAction2.started += PlayerAction2_started;

        Shoot = PlayerInputInstances.currentActionMap.FindAction("Shoot");



        PlayerAction.canceled += PlayerAction_canceled;

        PlayerAction2.canceled += PlayerAction2_canceled;

        Shoot.started += Shoot_started;


    }

    private void PlayerAction2_canceled(InputAction.CallbackContext context)
    {
        Moving2 = false;
        rb2d.velocity = Vector2.zero;
    }

    private void PlayerAction2_started(InputAction.CallbackContext context)
    {
        Moving2 = true;
    }

    private void Shoot_started(InputAction.CallbackContext context)
    {
        Instantiate(Bullet, new Vector2(tank.transform.position.x, tank.transform.position.y), Quaternion.identity);
        StartCoroutine(HoldShoot());
    }





    private void PlayerAction_canceled(InputAction.CallbackContext context)
    {
        Moving = false;
        rb2d.velocity = Vector2.zero;
    }



    private void PlayerAction_started(InputAction.CallbackContext context)
    {
        Moving = true;
    }




    void Update()
    {
        if (Moving == true)
        {

            MoveDirection = PlayerAction.ReadValue<float>();


            rb2d.velocity = new Vector2(rb2d.velocity.x, 4 * MoveDirection);
            Debug.Log(MoveDirection + "up");

        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }




        if (Moving2 == true)
        {

            MoveDirection2 = PlayerAction2.ReadValue<float>();


            rb2d.velocity = new Vector2(rb2d.velocity.x, 4 * MoveDirection2);
            Debug.Log(MoveDirection2 + "up");

        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }


    }



    public void OnDestroy()
    {
        Shoot.started -= Shoot_started;
    }

    IEnumerator HoldShoot()
    {
        yield return new WaitForSeconds(1);
        Instantiate(Bullet, new Vector2(tank.transform.position.x, tank.transform.position.y), Quaternion.identity);
    }
}