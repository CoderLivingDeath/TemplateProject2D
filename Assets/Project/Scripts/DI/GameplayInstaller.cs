using UnityEngine;
using Reflex.Core;

public class GameplayInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterMessageBroker<string>();

        Debug.Log("Gameplay Scene Loaded");
    }
}
