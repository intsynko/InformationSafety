using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JoysticsEnum { Fixed, Floating}

public class JoysticController : MonoBehaviour
{
    //ToDo сделать переключение джойтиков и событие по переключению
    public Joystick currentJoystick;
    public JoysticsEnum currentType = JoysticsEnum.Floating;
    public bool visible { get { return currentJoystick.gameObject.active;  } set { currentJoystick.gameObject.SetActive(value); } }

    public void SwithJoystic(JoysticsEnum joystics)
    {
        //visible = false;
        currentType = joystics;
        switch (joystics)
        {
            case JoysticsEnum.Fixed:
                currentJoystick = transform.Find("Fixed Joystick").GetComponent<Joystick>();
                break;
            case JoysticsEnum.Floating:
                currentJoystick = transform.Find("Floating Joystick").GetComponent<Joystick>();
                break;
        }
        //visible = true;
    }
}
