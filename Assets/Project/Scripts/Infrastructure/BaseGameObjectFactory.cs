using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

public abstract class BaseGameObjectFactory<T> where T: class
{
    protected GameObject go;
    private readonly Container _container;

    protected BaseGameObjectFactory(Container container)
    {
        _container = container;
    }

    public virtual T Create()
    {
        if (go.TryGetComponent<T>(out var component))
        {
            var instance = GameObject.Instantiate(go);
            GameObjectInjector.InjectObject(instance, _container);
            instance.SetActive(false);
            return instance.GetComponent<T>();
        }
        else
        {
            Debug.LogError(
                $"The created game object ({go.name}) does not contain a {nameof(T)} component."
            );
            return null;
        }
    }
}