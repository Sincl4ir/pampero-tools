using System;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Pampero.TimeSystem
{
    public class TimeManager : MonoBehaviour
    {
        private const string SKYBOX_MATERIAL_PROPERTY = "_Blend";

        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private Light _sun;
        [SerializeField] private Light _moon;
        [SerializeField] private AnimationCurve _lightIntensityCurve;
        [SerializeField] private float _maxSunIntensity = 1;
        [SerializeField] private float _maxMoonIntensity = 0.5f;
        [SerializeField] private Color _dayAmbientLight;
        [SerializeField] private Color _nightAmbientLight;
        [SerializeField] private Volume _volume;
        [SerializeField] private Material _skyboxMaterial;
        [SerializeField] private RectTransform _dial;
        [SerializeField] private TimeSettings timeSettings;

        private float _initialDialRotation;
        private ColorAdjustments _colorAdjustments;
        private TimeService _service;

        public event Action OnSunrise
        {
            add => _service.OnSunrise += value;
            remove => _service.OnSunrise -= value;
        }

        public event Action OnSunset
        {
            add => _service.OnSunset += value;
            remove => _service.OnSunset -= value;
        }

        public event Action OnHourChange
        {
            add => _service.OnHourChange += value;
            remove => _service.OnHourChange -= value;
        }

        void Start()
        {
            _service = new TimeService(timeSettings);
            _volume.profile.TryGet(out _colorAdjustments);
            OnSunrise += () => Debug.Log("Sunrise");
            OnSunset += () => Debug.Log("Sunset");
            OnHourChange += () => Debug.Log("Hour change");

            _initialDialRotation = _dial.rotation.eulerAngles.z;
        }

        void Update()
        {
            UpdateTimeOfDay();
            RotateSun();
            UpdateLightSettings();
            UpdateSkyBlend();
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    timeSettings.timeMultiplier *= 2;
            //}
            //if (Input.GetKeyDown(KeyCode.LeftShift))
            //{
            //    timeSettings.timeMultiplier /= 2;
            //}
        }

        void UpdateSkyBlend()
        {
            float dotProduct = Vector3.Dot(_sun.transform.forward, Vector3.up);
            float blend = Mathf.Lerp(0, 1, _lightIntensityCurve.Evaluate(dotProduct));
            _skyboxMaterial.SetFloat(SKYBOX_MATERIAL_PROPERTY, blend);
        }

        void UpdateLightSettings()
        {
            float dotProduct = Vector3.Dot(_sun.transform.forward, Vector3.down);
            float lightIntensity = _lightIntensityCurve.Evaluate(dotProduct);

            _sun.intensity = Mathf.Lerp(0, _maxSunIntensity, lightIntensity);
            _moon.intensity = Mathf.Lerp(_maxMoonIntensity, 0, lightIntensity);

            if (_colorAdjustments == null) { return; }
            _colorAdjustments.colorFilter.value = Color.Lerp(_nightAmbientLight, _dayAmbientLight, lightIntensity);
        }

        void RotateSun()
        {
            float rotation = _service.CalculateSunAngle();
            _sun.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.right);
            //_dial.rotation = Quaternion.Euler(0, 0, rotation + _initialDialRotation);
        }

        void UpdateTimeOfDay()
        {
            _service.UpdateTime(Time.deltaTime);
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_timeText == null) { return; }
            _timeText.text = _service.CurrentTime.ToString("hh:mm");
        }
    }
}
//EOF.