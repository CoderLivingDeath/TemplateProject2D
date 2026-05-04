using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

/// <summary>
/// Универсальный брокер сообщений, реализующий паттерн Pub-Sub на базе R3.
/// Позволяет издателям публиковать сообщения типа T, а подписчикам - наблюдать за ними.
/// </summary>
/// <typeparam name="T">Тип передаваемого сообщения</typeparam>
public sealed class MessageBroker<T> : IMessagePublisher<T>, IMessageSubscriber<T>, IDisposable
{
    private readonly Subject<T> _subject;
    private bool _disposed;

    public Subject<T> Subject => _subject;

    public MessageBroker()
    {
        _subject = new Subject<T>();
    }

    /// <summary>
    /// Публикует сообщение всем подписчикам.
    /// </summary>
    /// <param name="message">Сообщение для публикации</param>
    public void Publish(T message)
    {
        if (_disposed)
            return;

        _subject.OnNext(message);
    }

    /// <summary>
    /// Возвращает Observable для подписки на сообщения.
    /// </summary>
    public Observable<T> Observe()
    {
        if (_disposed)
            return Observable.Empty<T>();

        return _subject.AsObservable();
    }

    /// <summary>
    /// Автоматически освобождает ресурсы при уничтожении компонента.
    /// </summary>
    /// <param name="component">Компонент, при уничтожении которого нужно освободить брокер</param>
    /// <returns>Ссылка на себя для цепочки вызовов</returns>
    public IDisposable AddTo(Component component)
    {
        component.GetCancellationTokenOnDestroy().Register(Dispose);
        return this;
    }

    /// <summary>
    /// Освобождает ресурсы брокера.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        _subject.Dispose();
    }
}
