using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Component that is responsible for all player inputs
/// </summary>
/// 
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(WeaponsComponent))]
public class PlayerInputComponent : MonoBehaviour
{

    // Player Control Inputs
    private PlayerInput playerInput;
    private PlayerControls playerControls;

    private MovementComponent playerMovement;
    private WeaponsComponent playerWeapons;



    // Start is called before the first frame update
    void Awake()
    {
        GettingComponents();
        ControlSetup();
        MovementSetup();
        WeaponSetup();
        UIControls();

    }

    /// <summary>
    /// Finding the interaction components
    /// </summary>
    private void GettingComponents()
    {
        playerMovement = GetComponent<MovementComponent>();
        playerWeapons = GetComponent<WeaponsComponent>();   
    }

    /// <summary>
    /// Enabling the Player Controls
    /// </summary>

    private void ControlSetup()
    {
        playerInput = new PlayerInput();
        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    /// <summary>
    /// Setting up the movement controls for the player
    /// </summary>
    private void MovementSetup()
    {
        playerMovement.PlayerControls = playerControls;
        playerControls.Player.Movement.performed += playerMovement.OnMovementAction;
        playerControls.Player.Movement.canceled += playerMovement.OnMovementAction;
    }

    /// <summary>
    /// Seetting up the weapon controls for the player
    /// </summary>
    private void WeaponSetup()
    {
        playerControls.Player.Fire.performed += playerWeapons.WeaponFire;
        playerControls.Player.Ketchup.performed += playerWeapons.SwitchWeapon;
        playerControls.Player.Mustard.performed += playerWeapons.SwitchWeapon;
        playerControls.Player.Mayo.performed += playerWeapons.SwitchWeapon;
    }

    private void UIControls()
    {
        // Pausing Game
        playerControls.Player.Pause.performed += playerWeapons.PausingGame;
    }

    private void OnDisable()
    {
        // Movement
        playerControls.Player.Movement.performed -= playerMovement.OnMovementAction;
        playerControls.Player.Movement.canceled -= playerMovement.OnMovementAction;

        // Weapons
        playerControls.Player.Fire.performed -= playerWeapons.WeaponFire;
        playerControls.Player.Ketchup.performed -= playerWeapons.SwitchWeapon;
        playerControls.Player.Mustard.performed -= playerWeapons.SwitchWeapon;
        playerControls.Player.Mayo.performed -= playerWeapons.SwitchWeapon;


        // Pausing Game
        playerControls.Player.Pause.performed -= playerWeapons.PausingGame;
    }


}
