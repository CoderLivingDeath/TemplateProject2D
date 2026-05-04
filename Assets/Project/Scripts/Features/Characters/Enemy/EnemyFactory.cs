using Reflex.Core;

public class EnemyFactory : BaseGameObjectFactory<EnemyBehaviour>
{
    public EnemyFactory(ConfigurationProvider provider, Container container)
        : base(container)
    {
        go = provider.DraftObject;
    }
}
