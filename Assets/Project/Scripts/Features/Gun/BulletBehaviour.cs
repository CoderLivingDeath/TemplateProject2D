using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody;

    public void AddForce(Vector2 value)
    {
        rigidbody.AddForce(value, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyBehaviour>(out var component))
        {
            component.Kill();
            GameObject.Destroy(this);
        }
    }
}
