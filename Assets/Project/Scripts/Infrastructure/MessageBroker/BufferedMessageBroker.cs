using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

/// <summary>
/// Буферизированный брокер сообщений с ограниченным размером буфера.
/// Хранит последние bufferSize сообщений и позволяет новым подписчикам получить их сразу при подписке.
/// </summary>
/// <typeparam name="T">Тип передаваемого сообщения</typeparam>
public sealed class BufferedMessageBroker<T> : IMessagePublisher<T>, IMessageSubscriber<T>, IDisposable
{
    private readonly ReplaySubject<T> _subject;
    private bool _disposed;

    public ReplaySubject<T> Subject => _subject;

    /// <summary>
    /// Создает буферизированный брокер с указанным размером буфера.
    /// </summary>
    /// <param name="bufferSize">Количество хранимых сообщений</param>
    public BufferedMessageBroker(int bufferSize)
    {
        _subject = new ReplaySubject<T>(bufferSize);
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