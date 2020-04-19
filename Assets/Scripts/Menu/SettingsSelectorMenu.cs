using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;

public class SettingsSelectorMenu : MonoBehaviour
{
    [Inject] private JoysticController _joysticController;
    [Inject] private SaveManager saveManager;
    [Inject] private Cinemachine.CinemachineVirtualCamera virtualCamera;
    [Inject] private MessageBox messageBox;

    public GameObject SettingsPanel;
    public Dropdown JoysticTypeDropDown;
    public Slider CameraSizeSlider;
    //public Toggle OffsetCameraToggle;
    public Toggle FloatingCameraToggle;

    private float cameraSize {
        get {
            return virtualCamera.m_Lens.OrthographicSize;
        }
        set {
            virtualCamera.m_Lens.OrthographicSize = value;
        }
    }
    private JoysticsEnum joystick {
        get {
            return _joysticController.currentType;
        }
        set {
            _joysticController.SwithJoystic(value);
        }
    }
    private bool floatingCamera {
        get {
            Cinemachine.CinemachineFramingTransposer settings = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>();
            return settings.m_SoftZoneWidth != 0f;
        }
        set {
            Cinemachine.CinemachineFramingTransposer settings = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>();
            if (!value)
            {
                settings.m_SoftZoneWidth = 0f;
                settings.m_SoftZoneHeight = 0f;
                settings.m_DeadZoneWidth = 0f;
                settings.m_DeadZoneHeight = 0f;
            }
            else
            {
                settings.m_SoftZoneWidth = 0.3f;
                settings.m_SoftZoneHeight = 0.3f;
                settings.m_DeadZoneWidth = 0.23f;
                settings.m_DeadZoneHeight = 0.15f;
            }
        }
    }
    //private bool offsetCamera {
    //    get {
    //        Cinemachine.CinemachineFramingTransposer settings = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>();
    //        return settings.m_DeadZoneWidth != 0f;
    //    }
    //    set {
    //        Cinemachine.CinemachineFramingTransposer settings = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>();
    //        if (!value)
    //        {
    //            settings.m_DeadZoneWidth = 0f;
    //            settings.m_DeadZoneHeight = 0f;
    //        }
    //        else
    //        {
    //            settings.m_DeadZoneWidth = 0.23f;
    //            settings.m_DeadZoneHeight = 0.15f;
    //        }
    //    }
    //}

    public List<JoysticsNames> JoysticsNames = new List<JoysticsNames>() {
        new JoysticsNames(){ JoysticType=JoysticsEnum.Floating, JoysticName="Плавающий"},
        new JoysticsNames(){ JoysticType=JoysticsEnum.Fixed, JoysticName="Фиксированный"},
    };

    
    private void Start()
    {
        saveManager.LoadSettings();
        LoadSettings();
        MyStart();
    }

    /// <summary>
    /// Устанавливаем значения элементов из сохранений
    /// </summary>
    private void LoadSettings()
    {
        cameraSize = saveManager.staticSettings.CameraSize; // поцизия камеры
        // тип управления
        joystick = (JoysticsEnum)Enum.Parse(typeof(JoysticsEnum), saveManager.staticSettings.ControlType);
        // плавающая ли камера
        //offsetCamera = saveManager.staticSettings.OffsetCamera;
        // Камера всегда в одной точке
        floatingCamera = saveManager.staticSettings.FloatingCamera;
    }

    /// <summary>
    /// Сохранить натсройки текущих управляемых элементов
    /// </summary>
    private void SaveSettings()
    {
        saveManager.staticSettings.CameraSize = cameraSize;
        saveManager.staticSettings.ControlType = joystick.ToString();
        saveManager.staticSettings.FloatingCamera = floatingCamera;
        saveManager.SaveSettings();
    }

    /// <summary>
    /// Устанавливает все ui элементы значениями связанных элементов
    /// </summary>
    private void MyStart()
    {
        Debug.Log("Set MyStart PreSettings");
        // заполняем выпадающий список имеющимися джойстиками
        JoysticTypeDropDown.ClearOptions();
        JoysticTypeDropDown.AddOptions(JoysticsNames.Select(x => x.JoysticName).ToList());
        // выбранным элеменом ставим текущий джостик
        JoysticTypeDropDown.value = JoysticsNames.IndexOf(
            JoysticsNames.Where(x => x.JoysticType == joystick).ToList()[0]
        );
        JoysticTypeDropDown.onValueChanged.RemoveAllListeners();
        // при изменении типа джостика - поставим новый джостик
        JoysticTypeDropDown.onValueChanged.AddListener(delegate {
            joystick = JoysticsNames[JoysticTypeDropDown.value].JoysticType;
        });


        // устанавливаем настойки для слайдера размера камеры
        CameraSizeSlider.value = cameraSize;
        CameraSizeSlider.onValueChanged.RemoveAllListeners();
        CameraSizeSlider.onValueChanged.AddListener(delegate {
            cameraSize = CameraSizeSlider.value;
            //SliderValueChanged(CameraSizeSlider);
        });


        // устанавливаем настройки для чекбокса "Плавающая камера"
        FloatingCameraToggle.isOn = floatingCamera;
        FloatingCameraToggle.onValueChanged.RemoveAllListeners();
        FloatingCameraToggle.onValueChanged.AddListener(delegate {
            floatingCamera = FloatingCameraToggle.isOn;
        });
    }
    
    /// <summary>
    /// ОТкрыть панель настроек
    /// </summary>
    public void Open()
    {
        SettingsPanel.SetActive(true);
    }

    /// <summary>
    /// Закрыть панель настроек
    /// </summary>
    public async void Close()
    {
        SaveSettings();
        SettingsPanel.SetActive(false);
        await messageBox.SaveAnim();
    }
}

/// <summary>
/// Вспомагательный класс для работы с выпадающим списком (объединяет человекочитаемую строку с перечислением JoysticsEnum)
/// </summary>
public class JoysticsNames
{
    public JoysticsEnum JoysticType;
    public string JoysticName;
}
