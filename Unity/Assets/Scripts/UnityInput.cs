using Programation_3_DnD_Core;
using UnityEngine;

public class UnityInput : MonoBehaviour, IInput
{
    private KeyCode _lastKey = KeyCode.None;

    void Update()
    {
        if (_lastKey != KeyCode.None)
        {
            Debug.Log(_lastKey);
        }
    }
    public void ProcessInput()
    {
        _lastKey = KeyCode.None;

        if (!Input.anyKeyDown)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) _lastKey = KeyCode.UpArrow;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) _lastKey = KeyCode.DownArrow;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) _lastKey = KeyCode.LeftArrow;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) _lastKey = KeyCode.RightArrow;

        else if (Input.GetKeyDown(KeyCode.Return)) _lastKey = KeyCode.Return;
        else if (Input.GetKeyDown(KeyCode.Escape)) _lastKey = KeyCode.Escape;

        else if (Input.GetKeyDown(KeyCode.Q)) _lastKey = KeyCode.Q;
        else if (Input.GetKeyDown(KeyCode.I)) _lastKey = KeyCode.I;
        else if (Input.GetKeyDown(KeyCode.P)) _lastKey = KeyCode.P;
        else if (Input.GetKeyDown(KeyCode.T)) _lastKey = KeyCode.T;
        else if (Input.GetKeyDown(KeyCode.W)) _lastKey = KeyCode.W;

        else if (Input.GetKeyDown(KeyCode.Alpha1)) _lastKey = KeyCode.Keypad1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) _lastKey = KeyCode.Keypad2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) _lastKey = KeyCode.Keypad3;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) _lastKey = KeyCode.Keypad4;
        else if (Input.GetKeyDown(KeyCode.Alpha5)) _lastKey = KeyCode.Keypad5;
        else if (Input.GetKeyDown(KeyCode.Alpha6)) _lastKey = KeyCode.Keypad6;
        else if (Input.GetKeyDown(KeyCode.Alpha7)) _lastKey = KeyCode.Keypad7;
        else if (Input.GetKeyDown(KeyCode.Alpha8)) _lastKey = KeyCode.Keypad8;
        else if (Input.GetKeyDown(KeyCode.Alpha9)) _lastKey = KeyCode.Keypad9;
    }


    public bool CheckKeyAvailable()
    {
        Debug.Log("Don't use CheckKeyAvailable() in unity !");
        throw new System.Exception();
    }

    public void ResetInput()
    {
        _lastKey = KeyCode.None;
    }

    public int GetNumberPressed()
    {
        if (_lastKey >= KeyCode.Keypad1 && _lastKey <= KeyCode.Keypad9)
        {
            return (int)(_lastKey - KeyCode.Keypad0);
        }

        return 0;
    }

    public bool IsKeyNone() => _lastKey == KeyCode.None;
    public bool IsKeyUp() => _lastKey == KeyCode.UpArrow;
    public bool IsKeyDown() => _lastKey == KeyCode.DownArrow;
    public bool IsKeyLeft() => _lastKey == KeyCode.LeftArrow;
    public bool IsKeyRight() => _lastKey == KeyCode.RightArrow;
    public bool IsKeyValidate() => _lastKey == KeyCode.Return;
    public bool IsKeyCancel() => _lastKey == KeyCode.Escape;
    public bool IsKeyQ() => _lastKey == KeyCode.Q;
    public bool IsKeyInventory() => _lastKey == KeyCode.I;
    public bool IsKeyPause() => _lastKey == KeyCode.P;
    public bool IsKeyTrade() => _lastKey == KeyCode.T;
    public bool IsKeyWork() => _lastKey == KeyCode.W;

    public bool IsKeyNumber1() => _lastKey == KeyCode.Keypad1;
    public bool IsKeyNumber2() => _lastKey == KeyCode.Keypad2;
    public bool IsKeyNumber3() => _lastKey == KeyCode.Keypad3;
    public bool IsKeyNumber4() => _lastKey == KeyCode.Keypad4;
    public bool IsKeyNumber5() => _lastKey == KeyCode.Keypad5;
    public bool IsKeyNumber6() => _lastKey == KeyCode.Keypad6;
    public bool IsKeyNumber7() => _lastKey == KeyCode.Keypad7;
    public bool IsKeyNumber8() => _lastKey == KeyCode.Keypad8;
    public bool IsKeyNumber9() => _lastKey == KeyCode.Keypad9;
}
