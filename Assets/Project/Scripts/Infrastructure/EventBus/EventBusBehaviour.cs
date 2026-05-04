using System;
using R3;
using Reflex.Attributes;
using Unity.VisualScripting;
using UnityEngine;

public class EventBusBehaviour : MonoBehaviour
{
    private const string HOOK_NAME = "EventBusMessage";

    [Inject]
    private MessageBroker<string> _broker;

    private DisposableBag disposableBag;

    public void Publish(string message)
    {
        if (_broker == null)
            Debug.LogError("Broker is null");

        _broker.Publish(message);
    }

    public void Register(Action<string> handler)
    {
        if (_broker == null)
            Debug.LogError("Broker is null");

        _broker.Observe().Subscribe(handler).AddTo(this);
    }

    private void ProvideMessageToScriptGraphEventBus(string message)
    {
        Unity.VisualScripting.EventBus.Trigger(new EventHook(HOOK_NAME), message);
    }

    private void OnEnable()
    {
        disposableBag = new();
        disposableBag.AddTo(this);

        Register(ProvideMessageToScriptGraphEventBus);
    }

    private void OnDisable()
    {
        disposableBag.Dispose();
    }
}
