using System;
using Reflex;
using Reflex.Core;
using Lifetime = Reflex.Enums.Lifetime;

/// <summary>
/// Методы расширения для регистрации MessageBroker в контейнере зависимостей Reflex.
/// </summary>
public static class MessageBrokerReflexExtensions
{
    /// <summary>
    /// Регистрирует MessageBroker как синглтон в контейнере Reflex.
    /// </summary>
    /// <typeparam name="T">Тип сообщения</typeparam>
    /// <param name="builder">ContainerBuilder для цепочки вызовов</param>
    /// <param name="lifetime">Время жизни (по умолчанию Singleton)</param>
    /// <returns>ContainerBuilder для цепочки вызовов</returns>
    public static ContainerBuilder RegisterMessageBroker<T>(
        this ContainerBuilder builder,
        Lifetime lifetime = Lifetime.Singleton
    )
    {
        var broker = new MessageBroker<T>();
        builder.RegisterValue(
            broker,
            new[]
            {
                typeof(IMessagePublisher<T>),
                typeof(IMessageSubscriber<T>),
                typeof(MessageBroker<T>),
            }
        );
        return builder;
    }

    /// <summary>
    /// Регистрирует BufferedMessageBroker как синглтон в контейнере Reflex.
    /// </summary>
    /// <typeparam name="T">Тип сообщения</typeparam>
    /// <param name="builder">ContainerBuilder для цепочки вызовов</param>
    /// <param name="bufferSize">Размер буфера</param>
    /// <param name="lifetime">Время жизни (по умолчанию Singleton)</param>
    /// <returns>ContainerBuilder для цепочки вызовов</returns>
    public static ContainerBuilder RegisterBufferedMessageBroker<T>(
        this ContainerBuilder builder,
        int bufferSize,
        Lifetime lifetime = Lifetime.Singleton
    )
    {
        var broker = new BufferedMessageBroker<T>(bufferSize);
        builder.RegisterValue(
            broker,
            new[] { typeof(IMessagePublisher<T>), typeof(IMessageSubscriber<T>), typeof(BufferedMessageBroker<T>) }
        );
        return builder;
    }

    /// <summary>
    /// Регистрирует несколько MessageBroker сразу по списку типов сообщений.
    /// </summary>
    /// <param name="builder">ContainerBuilder для цепочки вызовов</param>
    /// <param name="messageTypes">Массив типов сообщений</param>
    /// <param name="lifetime">Время жизни (по умолчанию Singleton)</param>
    /// <returns>ContainerBuilder для цепочки вызовов</returns>
    public static ContainerBuilder RegisterMessageBrokers(
        this ContainerBuilder builder,
        Type[] messageTypes,
        Lifetime lifetime = Lifetime.Singleton
    )
    {
        foreach (var type in messageTypes)
        {
            var brokerType = typeof(MessageBroker<>).MakeGenericType(type);
            var publisherType = typeof(IMessagePublisher<>).MakeGenericType(type);
            var subscriberType = typeof(IMessageSubscriber<>).MakeGenericType(type);

            var instance = Activator.CreateInstance(brokerType);
            builder.RegisterValue(instance, new[] { publisherType, subscriberType });
        }

        return builder;
    }
}
