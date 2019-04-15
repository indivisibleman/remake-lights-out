using UnityEngine;
using UnityEngine.UI;

namespace LightsOut
{
    public class Timer : MonoBehaviour
    {
        private bool isActive = false;
        private float startTime;
        private Text timerText;

        void Awake()
        {
            startTime = Time.time;
            timerText = GetComponentInChildren<Text>();
        }

        void Update()
        {
            if (isActive)
            {
                UpdateTimerText();
            }
        }

        public void SetActive(bool active)
        {
            isActive = active;
        }

        public void ZeroTimer()
        {
            startTime = Time.time;
            UpdateTimerText();
        }

        public float GetTime()
        {
            return Time.time - startTime;
        }

        void UpdateTimerText()
        {
            timerText.text = FormatTime(Time.time - startTime);
        }

        string FormatTime(float time)
        {
            return Mathf.Floor(time / 60).ToString("00") + ":" + Mathf.Floor(time % 60).ToString("00");
        }
    }
}
