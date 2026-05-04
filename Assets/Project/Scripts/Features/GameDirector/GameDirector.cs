using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [Inject]
    public EnemySpawner enemySpawner;

    public float GameTime;

    public float x_min = -30;
    public float y_min = -30;

    public float x_max = 30;
    public float y_max = 30;

    public async UniTaskVoid GameThread(CancellationToken token = default)
    {
        while (!token.IsCancellationRequested)
        {
            await UniTask.WaitForSeconds(1);

            for (int i = 0; i < 10; i++)
            {
                Vector2 pos = new(Random.Range(x_min, x_max), Random.Range(y_min, y_max));
                enemySpawner.Spawn(pos);
            }
            await UniTask.Yield();
        }
    }
}
