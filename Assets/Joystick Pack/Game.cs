using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Joystick joystickPrefab;
    [SerializeField] private RectTransform inputRootUI;


    private IInputService inputService;

    void Awake()
    {
        inputService = GetInputService();
    }

    void Update()
    {
        Debug.Log(inputService.MoveDirection);

    }

    IInputService GetInputService()
    {
        Debug.Log(Application.platform);
        Dictionary<RuntimePlatform, Func<IInputService>> inputMap = new Dictionary<RuntimePlatform, Func<IInputService>>()
        {
            { RuntimePlatform.WindowsEditor, ()=>
            {
                Debug.Log("Hello world");
                return new DesktopInputService();
            }},
            { RuntimePlatform.Android, ()=>new MobileInputService(joystickPrefab, inputRootUI)}
        };

        if (inputMap.TryGetValue(RuntimePlatform.Android, out var inputService))
        {
            return inputService();
        }
        else
        {
            Debug.Log("Error");
            throw new System.Exception("Error");
        }
    }
}
