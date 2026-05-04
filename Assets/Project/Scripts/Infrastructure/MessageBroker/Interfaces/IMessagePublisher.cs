using System;

/// <summary>
/// Интерфейс издателя сообщений - позволяет публиковать сообщения типа T.
/// </summary>
/// <typeparam name="T">Тип публикуемого сообщения</typeparam>
public interface IMessagePublisher<T>
{
    /// <summary>
    /// Публикует сообщение для всех подписчиков.
    /// </summary>
    /// <param name="message">Сообщение для публикации</param>
    void Publish(T message);
}