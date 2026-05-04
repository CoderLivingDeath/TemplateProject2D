using System;
using R3;

/// <summary>
/// Интерфейс подписчика сообщений - позволяет получать Observable для подписки на сообщения типа T.
/// </summary>
/// <typeparam name="T">Тип получаемого сообщения</typeparam>
public interface IMessageSubscriber<T> : IDisposable
{
    /// <summary>
    /// Возвращает поток наблюдаемых сообщений.
    /// </summary>
    /// <returns>Observable для подписки</returns>
    Observable<T> Observe();
}