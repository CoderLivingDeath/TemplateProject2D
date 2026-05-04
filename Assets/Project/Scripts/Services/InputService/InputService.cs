using System;
using UnityEngine;

public class InputService : IDisposable
{
    private readonly InputConfiguration inputConfiguration;

    public event Action<Vector2> MovementEvent;
    public event Action<bool> FireEvent;
    public event Action<bool> SprintEvent;

    public event Action<Vector2> MousePosChangedEvent;
    public event Action<bool> OnAttackEvent;

    public InputService(InputConfiguration inputConfiguration)
    {
        this.inputConfiguration = inputConfiguration;

        Subscribe();

        inputConfiguration.Enable();
    }

    private void Subscribe()
    {
        inputConfiguration.PlayerActions.Move.performed += ctx =>
            MovementEvent?.Invoke(ctx.ReadValue<Vector2>());
        inputConfiguration.PlayerActions.Move.canceled += ctx =>
            MovementEvent?.Invoke(ctx.ReadValue<Vector2>());

        inputConfiguration.PlayerActions.Attack.performed += ctx =>
            FireEvent?.Invoke(ctx.ReadValueAsButton());
        inputConfiguration.PlayerActions.Attack.canceled += ctx =>
            FireEvent?.Invoke(ctx.ReadValueAsButton());

        inputConfiguration.PlayerActions.Sprint.performed += ctx =>
            SprintEvent?.Invoke(ctx.ReadValueAsButton());
        inputConfiguration.PlayerActions.Sprint.canceled += ctx =>
            SprintEvent?.Invoke(ctx.ReadValueAsButton());

        inputConfiguration.UIActions.Point.performed += ctx =>
            MousePosChangedEvent?.Invoke(ctx.ReadValue<Vector2>());

        inputConfiguration.PlayerActions.Attack.performed += ctx =>
            OnAttackEvent?.Invoke(ctx.ReadValueAsButton());
    }

    public void Dispose()
    {
        inputConfiguration.Disable();
    }
}
