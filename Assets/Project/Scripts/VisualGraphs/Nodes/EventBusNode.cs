using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("On EventBus Message")]
[UnitCategory("Events/My Events")]
public class EventBusNode : EventUnit<string>
{
    [DoNotSerialize]
    public ValueOutput message { get; private set; }

    [DoNotSerialize]
    public ValueInput key;

    protected override bool register => true;

    public override EventHook GetHook(GraphReference reference)
    {
        return new EventHook("EventBusMessage");
    }

    protected override void Definition()
    {
        base.Definition();
        key = ValueInput<string>("key", string.Empty);
        message = ValueOutput<string>(nameof(message));
    }

    protected override bool ShouldTrigger(Flow flow, string data)
    {
        string key = flow.GetValue<string>(this.key);
        bool matches = string.Equals(data, key, System.StringComparison.Ordinal);
        return matches;
    }

    protected override void AssignArguments(Flow flow, string data)
    {
        // Вызывается ТОЛЬКО если ShouldTrigger=true
        flow.SetValue(message, data);
    }
}
