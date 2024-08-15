using UnityEngine;

public class DesktopInputService : IInputService
{
    public Vector3 MoveDirection => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
}