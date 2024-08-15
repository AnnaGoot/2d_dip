using UnityEngine;

public class MobileInputService : IInputService
{
    private Joystick _joystick;

    public MobileInputService(Joystick joystickPrefab, Transform rootTransform)
    {
        _joystick = GameObject.Instantiate(joystickPrefab, rootTransform);
    }

    public Vector3 MoveDirection => _joystick.Direction;
}