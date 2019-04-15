using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LightsOut
{
    public class ScreenMonitor : MonoBehaviour
    {
        public static ScreenMonitor Instance { get { return instance; } }
        public static event Action OnScreenChanged;

        private static ScreenMonitor instance;
        private static DeviceOrientation lastOrientation;
        private static Resolution lastResolution;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                lastOrientation = Input.deviceOrientation;
                lastResolution = Screen.currentResolution;
            }
        }

        public static DeviceOrientation GetLastOrientation()
        {
            return lastOrientation;
        }

        public static Resolution GetResolution()
        {
            return lastResolution;
        }

        void Update()
        {
            bool orientationChanged = false;
            bool resolutionChanged = false;

            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Unknown:
                case DeviceOrientation.FaceUp:
                case DeviceOrientation.FaceDown:
                    break;
                default:
                    if (lastOrientation != Input.deviceOrientation)
                    {
                        lastOrientation = Input.deviceOrientation;
                        orientationChanged = true;
                    }

                    break;
            }

            if (lastResolution.height != Screen.currentResolution.height ||
                lastResolution.width != Screen.currentResolution.width)
            {
                lastResolution = Screen.currentResolution;
                resolutionChanged = true;
            }

            if (orientationChanged || resolutionChanged)
            {
                OnScreenChanged();
            }
        }
    }
}
