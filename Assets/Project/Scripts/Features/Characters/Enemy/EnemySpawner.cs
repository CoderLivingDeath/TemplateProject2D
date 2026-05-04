using UnityEngine;

public class EnemySpawner
{
    private readonly EnemyFactory _factory;
    private readonly ObjectPool<EnemyBehaviour> objectPool;

    public EnemySpawner(EnemyFactory factory)
    {
        _factory = factory;
        objectPool = new(
            32,
            factory: factory.Create,
            onGetting: (enemy) => enemy.gameObject.SetActive(true),
            onReturn: (enemy) => enemy.gameObject.SetActive(false)
        );
    }

    public EnemyBehaviour Spawn(Vector2 position)
    {
        var enemy = objectPool.Get();

        enemy.transform.position = position;
        enemy.OnDeath += () => objectPool.Return(enemy);
        Debug.Log($"active: {objectPool.ActiveCount} free: {objectPool.FreeCount}, total: {objectPool.TotalCount}");

        return enemy;
    }
}
