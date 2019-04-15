using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightsOut
{
    public class ButtonLight : MonoBehaviour
    {
        public Color32 onColor = new Color32(140, 117, 197, 255);
        public Color32 offColor = new Color32(255, 138, 247, 255);

        private GameManager gameManager;
        private int id;
        private bool isLit = false;
        private ColorBlock colors;
        private Button button;
        private bool isInteractable;

        void Start()
        {
            gameManager = transform.parent.gameObject.GetComponent<GameManager>();
            button = GetComponent<Button>();
            colors = button.colors;
            colors.pressedColor = offColor;
            colors.normalColor = onColor;
            button.colors = colors;
            isInteractable = false;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public void OnClick()
        {
            if (isInteractable)
            {
                gameManager.ToggleLights(id);
                gameManager.CountMove();
                gameManager.GameOverCheck();
            }

            EventSystem.current.SetSelectedGameObject(null);
        }

        public void ChangeState()
        {
            SetState(!isLit);
        }

        public void SetState(bool lit)
        {
            isLit = lit;

            if (isLit)
            {
                colors.pressedColor = onColor;
                colors.normalColor = offColor;
            }
            else
            {
                colors.pressedColor = offColor;
                colors.normalColor = onColor;
            }

            button.colors = colors;
        }

        public bool IsLit()
        {
            return isLit;
        }

        public void SetInteractable(bool interactable)
        {
            isInteractable = interactable;
        }
    }
}
