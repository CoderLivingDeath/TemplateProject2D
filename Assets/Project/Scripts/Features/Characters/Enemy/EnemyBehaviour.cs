using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private MovementBehaviour movementBehaviour;

    [SerializeField]
    private Transform target;

    public event Action OnDeath;

    public void Kill()
    {
        OnDeath?.Invoke();
    }

    void Start()
    {
        var go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
    }

    void FixedUpdate()
    {
        movementBehaviour.Move(target.position - transform.position);
    }

    void OnEnable()
    {
        UniTask
            .Action(async () =>
            {
                await UniTask.WaitForSeconds(UnityEngine.Random.Range(2f, 15));
                OnDeath?.Invoke();
            })
            .Invoke();
    }

    [ContextMenu("TestMethod")]
    public void TestMethod()
    {
        OnDeath?.Invoke();
    }
}
