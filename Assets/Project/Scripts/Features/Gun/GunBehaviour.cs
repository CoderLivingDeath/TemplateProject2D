using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using Unity.Mathematics;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public Vector2 Direction;

    public GameObject BulletPrefab;

    public Transform FirePoint;

    public float Force = 1;

    public void Fire()
    {
        Vector2 spawnPoint = FirePoint.position;

        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var instance = GameObject.Instantiate(BulletPrefab, spawnPoint, rotation);

        if (instance.TryGetComponent<BulletBehaviour>(out var component))
        {
            component.AddForce(Direction * Force);
        }
    }
}
