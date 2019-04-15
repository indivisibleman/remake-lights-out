using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightsOut
{
    public class GameManager : MonoBehaviour
    {
        public GameObject lightPrefab;
        public GameObject timerDisplay;
        public GameObject movesDisplay;
        public GameObject resetButton;

        private int edgeLength = 5;
        private int lightCount;
        private ButtonLight[] lights;
        private List<IEnumerator> startUpCoroutines = new List<IEnumerator>();
        private Timer timer;
        private MoveCounter moveCounter;

        void Start()
        {
            lightCount = edgeLength * edgeLength;
            lights = new ButtonLight[lightCount];

            for (int lightIndex = 0; lightIndex < lightCount; lightIndex++)
            {
                GameObject light = Instantiate(lightPrefab, Vector3.zero, Quaternion.identity);
                light.GetComponent<RectTransform>().SetParent(transform);
                light.GetComponent<ButtonLight>().SetId(lightIndex);
                lights[lightIndex] = light.GetComponent<ButtonLight>();
            }

            timer = timerDisplay.GetComponent<Timer>();
            moveCounter = movesDisplay.GetComponent<MoveCounter>();
        }

        public void CountMove()
        {
            moveCounter.IncrementCount();
        }

        public void ToggleLights(int lightClicked)
        {
            foreach (int offset in GetOffsets(lightClicked))
            {
                lights[lightClicked + offset].ChangeState();
            }
        }

        public int GetMoves()
        {
            return moveCounter.GetCount();
        }

        public float GetTime()
        {
            return timer.GetTime();
        }

        public void GameOverCheck()
        {
            if (LightsOnCount() == 0)
            {
                timer.SetActive(false);
                SetLightsActive(false);
                Debug.Log("Won in " + GetMoves() + " " + GetTime());
            }
        }

        List<int> GetOffsets(int lightClicked)
        {
            List<int> offsets = new List<int>();

            if (lightClicked - edgeLength >= 0)
            {
                offsets.Add(-edgeLength);
            }

            if (lightClicked % edgeLength != 0)
            {
                offsets.Add(-1);
            }

            offsets.Add(0);

            if (lightClicked % edgeLength != edgeLength - 1)
            {
                offsets.Add(1);
            }

            if (lightClicked + edgeLength < lightCount)
            {
                offsets.Add(edgeLength);
            }

            return offsets;
        }

        public int LightsOnCount()
        {
            int lightsOnCount = 0;

            for (int light = 0; light < lightCount; light++)
            {
                if (lights[light].IsLit())
                {
                    lightsOnCount++;
                }
            }

            return lightsOnCount;
        }

        private IEnumerator LightsOn()
        {
            SetLightsActive(false);
            timer.ZeroTimer();
            timer.SetActive(false);
            moveCounter.ResetCount();

            for (int index = 0; index < lightCount; index++)
            {
                lights[index].SetState(false);
            }

            IList<int> randomButtons = RandomButtonList();

            foreach (int button in randomButtons)
            {
                ToggleLights(button);
                yield return new WaitForSeconds(0.25f);
            }

            SetLightsActive(true);
            timer.ZeroTimer();
            timer.SetActive(true);
        }

        private IList<int> RandomButtonList()
        {
            IList<int> possibleButtons = new List<int>();

            for (int possibleButton = 0; possibleButton < lightCount; possibleButton++)
            {
                possibleButtons.Add(possibleButton);
            }

            IList<int> buttonList = new List<int>();

            for (int index = 0; index < 8; index++)
            {
                int randomButton = Random.Range(0, possibleButtons.Count);
                buttonList.Add(possibleButtons[randomButton]);
                possibleButtons.RemoveAt(randomButton);
            }

            return buttonList;
        }

        private IList<int> ButtonList()
        {
            return new List<int>() { 0, 4, 24, 20, 6, 8, 18, 16};
        }

        void SetLightsActive(bool active)
        {
            for (int index = 0; index < lightCount; index++)
            {
                lights[index].SetInteractable(active);

                if (!active)
                {
                    lights[index].SetState(false);
                }
            }
        }

        public void ClickLightsOn()
        {
            EventSystem.current.SetSelectedGameObject(null);

            foreach (IEnumerator oldCoroutine in startUpCoroutines)
            {
                StopCoroutine(oldCoroutine);
            }

            startUpCoroutines.Clear();

            IEnumerator lightsOnCoroutine = LightsOn();

            startUpCoroutines.Add(lightsOnCoroutine);

            StartCoroutine(lightsOnCoroutine);
        }
    }
}
