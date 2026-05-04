using UnityEngine;

[CreateAssetMenu(fileName = "InputConfiguaration", menuName = "configs/InputConfiguaration")]
public class InputConfiguration : ScriptableObject
{
    private InputSystem_Actions _inputActions;

    public InputSystem_Actions InputActions
    {
        get
        {
            if (_inputActions == null)
                _inputActions = new();

            return _inputActions;
        }
    }

    public InputSystem_Actions.PlayerActions PlayerActions => InputActions.Player;
    public InputSystem_Actions.UIActions UIActions => InputActions.UI;

    public void Enable() => InputActions.Enable();

    public void Disable() => InputActions.Disable();
}
