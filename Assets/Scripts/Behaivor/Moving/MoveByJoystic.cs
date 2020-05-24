using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveByJoystic : Move
{
    [Inject] private JoysticController _joysticController;

    public override Vector2 Direction {
        get {
            return _joysticController.currentJoystick.Direction;
        }
    }
}
