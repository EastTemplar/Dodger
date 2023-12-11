using UnityEngine;

namespace Dodger.Domain.Inputs
{
     public class PlayerInput : MonoBehaviour
     {
          public float HorizontalMovement { get; private set; }
          private InputActions _actions;

          private void Awake()
          {
               _actions = new InputActions();
          }

          private void Update()
          {
               HorizontalMovement = _actions.Player.HorizontalMovement.ReadValue<float>();
          }

          private void OnEnable()
          {
               _actions.Enable();
          }

          private void OnDisable()
          {
               _actions.Disable();
          }
     }
}