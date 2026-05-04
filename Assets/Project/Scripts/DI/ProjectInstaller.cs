using Reflex.Core;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    [SerializeField]
    private ConfigurationProvider ConfigurationProvider;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterMessageBroker<string>();

        containerBuilder.RegisterValue(ConfigurationProvider);
        containerBuilder.RegisterType(
            typeof(ConfigurationService),
            Reflex.Enums.Lifetime.Singleton,
            Reflex.Enums.Resolution.Eager
        );
        containerBuilder.RegisterFactory(
            AudioServiceFactory,
            Reflex.Enums.Lifetime.Singleton,
            Reflex.Enums.Resolution.Eager
        );
        containerBuilder.RegisterFactory(
            InputServiceFactory,
            Reflex.Enums.Lifetime.Singleton,
            Reflex.Enums.Resolution.Eager
        );

        containerBuilder.RegisterType(
            typeof(EnemyFactory),
            Reflex.Enums.Lifetime.Singleton,
            Reflex.Enums.Resolution.Lazy
        );
        containerBuilder.RegisterType(
            typeof(EnemySpawner),
            Reflex.Enums.Lifetime.Singleton,
            Reflex.Enums.Resolution.Lazy
        );

        containerBuilder.RegisterType(
            typeof(GameDirector),
            Reflex.Enums.Lifetime.Singleton,
            Reflex.Enums.Resolution.Eager
        );
    }

    private AudioService AudioServiceFactory(Container container)
    {
        return new AudioService(container.Resolve<ConfigurationService>().GetAudioConfiguration());
    }

    private InputService InputServiceFactory(Container container)
    {
        return new InputService(container.Resolve<ConfigurationService>().GetInputConfiguration());
    }
}
