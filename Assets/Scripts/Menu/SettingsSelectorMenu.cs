using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsSelectorMenu : MonoBehaviour
{
    [Inject] private JoysticController _joysticController;
    [Inject] private CameraFollow2D _cameraFollow2D;
    public Dropdown JoysticTypeDropDown;
    public Slider CameraSizeSlider;
    public Toggle OffsetCameraToggle;
    public Toggle FloatingCameraToggle;

    public List<JoysticsNames> JoysticsNames = new List<JoysticsNames>() {
        new JoysticsNames(){ JoysticType=Joystics.Floating, JoysticName="Плавающий"},
        new JoysticsNames(){ JoysticType=Joystics.Fixed, JoysticName="Фиксированный"},
    };

    private void Start()
    {
        JoysticTypeDropDown.ClearOptions();
        JoysticTypeDropDown.AddOptions(JoysticsNames.Select(x => x.JoysticName).ToList());
        JoysticTypeDropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(JoysticTypeDropDown);
        });

        CameraSizeSlider.onValueChanged.AddListener(delegate {
            SliderValueChanged(CameraSizeSlider);
        });

        
        OffsetCameraToggle.onValueChanged.AddListener(delegate {
            OffsetCameraToggleChanged(OffsetCameraToggle);
        });

        
        FloatingCameraToggle.onValueChanged.AddListener(delegate {
            FloatingCameraToggleChanged(FloatingCameraToggle);
        });

        Dependensies();
    }

    private void OffsetCameraToggleChanged(Toggle toggle)
    {
        _cameraFollow2D.isOffsetCamera = toggle.isOn;
        Dependensies();
    }

    private void FloatingCameraToggleChanged(Toggle toggle)
    {
        _cameraFollow2D.isFloatingCamera = toggle.isOn;
        Dependensies();
    }

    public void Dependensies()
    {
        JoysticTypeDropDown.value = JoysticsNames.IndexOf(
            JoysticsNames.Where(x => x.JoysticType == _joysticController.currentType).ToList()[0]
        );
        CameraSizeSlider.value = Camera.main.orthographicSize;
        FloatingCameraToggle.isOn = _cameraFollow2D.isFloatingCamera;
        OffsetCameraToggle.isOn = _cameraFollow2D.isOffsetCamera;

        if (!_cameraFollow2D.isFloatingCamera)
        {
            OffsetCameraToggle.isOn = false;
            _cameraFollow2D.isOffsetCamera = false;
        }
    }

    public void DropdownValueChanged(Dropdown dropdown) {
        _joysticController.SwithJoystic(JoysticsNames[dropdown.value].JoysticType);
    }

    public void SliderValueChanged(Slider slider)
    {
        Camera.main.orthographicSize = slider.value;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}

public class JoysticsNames
{
    public Joystics JoysticType;
    public string JoysticName;
}
