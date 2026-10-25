using UnityEngine;
using UnityEngine.InputSystem;

public class StateSwitch : MonoBehaviour, ISwitch
{ 
    public enum ColorState
    {
        Red,
        Green,
        Blue
    }

    public ColorState CurrentBulletType = ColorState.Red;

    public void Switch()
    {
        switch (CurrentBulletType)
        {
            case ColorState.Red:
                CurrentBulletType = ColorState.Green;
                Debug.Log("Switched State to: Green");
                break;

            case ColorState.Green:
                CurrentBulletType = ColorState.Blue;
                Debug.Log("Switched State to: Blue");
                break;

            case ColorState.Blue:
                CurrentBulletType = ColorState.Red;
                Debug.Log("Switched State to: Red");
                break;
        }
    }
}
