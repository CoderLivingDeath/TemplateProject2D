using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector2 _inputMoveVector;

    [SerializeField]
    private float _deltaScale = 2f;

    [SerializeField]
    private Rigidbody2D rigidbody;

    void Start()
    {
        if (rigidbody == null) Debug.LogError("rigidbody is null");
        
    }

    private void FixedUpdate()
    {
        Vector2 delta = _inputMoveVector * Time.deltaTime * _deltaScale;
        rigidbody.MovePosition(rigidbody.position + delta);
    }

    public void Move(Vector2 direction)
    {
        _inputMoveVector = direction.normalized;
    }
}
