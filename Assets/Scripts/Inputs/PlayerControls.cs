// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""0e7e74c2-1d84-4278-af95-845ba823e19b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""3e3cf0d0-35ef-413e-be56-c7534f29b033"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""8af20fb7-608b-4186-ad13-9c4a2ce61697"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ketchup"",
                    ""type"": ""Button"",
                    ""id"": ""a52db074-f1fa-4322-8307-aa5d91201f54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mustard"",
                    ""type"": ""Button"",
                    ""id"": ""baabb57e-7604-4d9a-a6ee-ccc6ca27c6e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mayo"",
                    ""type"": ""Button"",
                    ""id"": ""2f339341-1fd7-4e0c-9f50-4294652f92ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""9e3da638-a497-4077-a4f3-35b8f43c62b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Moving (PC)"",
                    ""id"": ""93972f68-7bf2-4347-aa16-037a153be809"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dd551100-c534-4e11-9166-8591e05e3ce6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""031dc7bc-6a43-4b0e-ba96-1f99ad98e91a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bd526314-bc4c-4f9f-ae87-79e5ff3880f0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""21939522-1405-4050-ad19-e0234ab48231"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""TouchInput"",
                    ""id"": ""0e795d86-8efe-4548-b658-2f582252b5d6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""616e502f-ce46-42f7-9479-dddd1dc45914"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""f399f118-5ad8-4ef8-bbbe-dcf98c61bf9c"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b1ed5d3f-81ad-4ac3-9e56-8764849b21cb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05e8acda-84e6-4626-9ced-562b6c8e2f57"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ketchup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32be33d8-77a0-45ec-b2af-4480a9aed9c7"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mustard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be257311-c0e8-417e-83e1-45d77858791a"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mayo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75220f4e-0496-4e7d-b5f9-55ff9ab616e4"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""008d5f37-8d83-4d1b-84eb-15e2ddee702f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player"",
            ""bindingGroup"": ""Player"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
        m_Player_Ketchup = m_Player.FindAction("Ketchup", throwIfNotFound: true);
        m_Player_Mustard = m_Player.FindAction("Mustard", throwIfNotFound: true);
        m_Player_Mayo = m_Player.FindAction("Mayo", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Fire;
    private readonly InputAction m_Player_Ketchup;
    private readonly InputAction m_Player_Mustard;
    private readonly InputAction m_Player_Mayo;
    private readonly InputAction m_Player_Pause;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Fire => m_Wrapper.m_Player_Fire;
        public InputAction @Ketchup => m_Wrapper.m_Player_Ketchup;
        public InputAction @Mustard => m_Wrapper.m_Player_Mustard;
        public InputAction @Mayo => m_Wrapper.m_Player_Mayo;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Ketchup.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKetchup;
                @Ketchup.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKetchup;
                @Ketchup.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKetchup;
                @Mustard.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMustard;
                @Mustard.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMustard;
                @Mustard.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMustard;
                @Mayo.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMayo;
                @Mayo.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMayo;
                @Mayo.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMayo;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Ketchup.started += instance.OnKetchup;
                @Ketchup.performed += instance.OnKetchup;
                @Ketchup.canceled += instance.OnKetchup;
                @Mustard.started += instance.OnMustard;
                @Mustard.performed += instance.OnMustard;
                @Mustard.canceled += instance.OnMustard;
                @Mayo.started += instance.OnMayo;
                @Mayo.performed += instance.OnMayo;
                @Mayo.canceled += instance.OnMayo;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_PlayerSchemeIndex = -1;
    public InputControlScheme PlayerScheme
    {
        get
        {
            if (m_PlayerSchemeIndex == -1) m_PlayerSchemeIndex = asset.FindControlSchemeIndex("Player");
            return asset.controlSchemes[m_PlayerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnKetchup(InputAction.CallbackContext context);
        void OnMustard(InputAction.CallbackContext context);
        void OnMayo(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
