using Reflex.Attributes;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Inject]
    private InputService inputService;

    [SerializeField]
    private MovementBehaviour _movementBehaviour;

    [SerializeField]
    private GunBehaviour _gunBehaviour;

    [Inject]
    private GameDirector director;

    [SerializeField]
    private Camera camera;

    private void OnMove(Vector2 direction)
    {
        _movementBehaviour.Move(direction);
    }

    private void OnMousePosChanged(Vector2 value)
    {
        var posWS = camera.ScreenToWorldPoint(value);
        _gunBehaviour.Direction = (posWS - transform.position).normalized;
    }

    private void OnAttack(bool value)
    {
        _gunBehaviour.Fire();
    }

    void OnEnable()
    {
        inputService.MovementEvent += OnMove;
        inputService.MousePosChangedEvent += OnMousePosChanged;
        inputService.OnAttackEvent += OnAttack;
    }

    void OnDisable()
    {
        inputService.MovementEvent -= OnMove;
        inputService.MousePosChangedEvent -= OnMousePosChanged;
        inputService.OnAttackEvent -= OnAttack;
    }

    [ContextMenu("Test Method")]
    public void TestMethod()
    {
        director.GameThread().Forget();
    }
}
