using UnityEngine;
using UnityEngine.UI;

namespace LightsOut
{
    public class LayoutChange : MonoBehaviour
    {
        public GameObject lights;
        public GameObject controls;

        private AspectRatioFitter containerAspectRatioFitter;
        private RectTransform lightsRectTransform;
        private GridLayoutGroup lightsGridLayoutGroup;
        private RectTransform controlsRectTransform;
        private GridLayoutGroup controlsGridLayoutGroup;
        private AspectRatioFitter controlsAspectRatioFitter;
        private const float CONTAINER_LANDSCAPE_ASPECT_RATIO = 1.5f;
        private const float CONTAINER_PORTRAIT_ASPECT_RATIO = 1 / 1.5f;
        private readonly Vector2 LIGHTS_LANDSCAPE_PIVOT = new Vector2(0, 0.5f);
        private readonly Vector2 LIGHTS_PORTRAIT_PIVOT = new Vector2(0.5f, 1);
        private const float LIGHTS_SPACING_FACTOR = 0.015f;
        private const float LIGHTS_CELL_SIZE_FACTOR = 0.18f;
        private const float CONTROLS_LANDSCAPE_ASPECT_RATIO = 0.5f;
        private const float CONTROLS_PORTRAIT_ASPECT_RATIO = 2f;
        private readonly Vector2 CONTROLS_LANDSCAPE_PIVOT = new Vector2(1, 0.5f);
        private readonly Vector2 CONTROLS_PORTRAIT_PIVOT = new Vector2(0.5f, 0);
        private const float CONTROLS_SPACING_FACTOR = 0.015f;
        private readonly Vector2 CONTROLS_LANDSCAPE_CELL_SIZE_FACTOR = new Vector2(0.54f, 0.18f);
        private readonly Vector2 CONTROLS_PORTRAIT_CELL_SIZE_FACTOR = new Vector2(0.27f, 0.36f);

        void Start()
        {
            ScreenMonitor.OnScreenChanged += UpdateLayout;

            containerAspectRatioFitter = GetComponent<AspectRatioFitter>();

            lightsRectTransform = lights.GetComponent<RectTransform>();
            lightsGridLayoutGroup = lights.GetComponent<GridLayoutGroup>();

            controlsRectTransform = controls.GetComponent<RectTransform>();
            controlsGridLayoutGroup = controls.GetComponent<GridLayoutGroup>();
            controlsAspectRatioFitter = controls.GetComponent<AspectRatioFitter>();

            UpdateLayout();
        }

        void UpdateLayout()
        {
            if (DeviceOrientation.LandscapeLeft == ScreenMonitor.GetLastOrientation() ||
                DeviceOrientation.LandscapeRight == ScreenMonitor.GetLastOrientation())
            {
                UpdateLayoutToLandscape();
            }
            else
            {
                UpdateLayoutToPortrait();
            }
        }

        void UpdateLayoutToLandscape()
        {
            containerAspectRatioFitter.aspectRatio = CONTAINER_LANDSCAPE_ASPECT_RATIO;

            lightsRectTransform.pivot = LIGHTS_LANDSCAPE_PIVOT;

            Vector2 lightBoardWidthByHeight = new Vector2(lightsRectTransform.rect.width, lightsRectTransform.rect.height);
            lightsGridLayoutGroup.spacing = lightBoardWidthByHeight * LIGHTS_SPACING_FACTOR;
            lightsGridLayoutGroup.cellSize = lightBoardWidthByHeight * LIGHTS_CELL_SIZE_FACTOR;

            controlsAspectRatioFitter.aspectRatio = CONTROLS_LANDSCAPE_ASPECT_RATIO;
            controlsRectTransform.pivot = CONTROLS_LANDSCAPE_PIVOT;
            controlsGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;

            Vector2 controlBoardWidthByHeight = new Vector2(controlsRectTransform.rect.width, controlsRectTransform.rect.height);
            controlsGridLayoutGroup.spacing = controlBoardWidthByHeight * CONTROLS_SPACING_FACTOR;
            controlsGridLayoutGroup.cellSize = controlBoardWidthByHeight * CONTROLS_LANDSCAPE_CELL_SIZE_FACTOR;
        }

        void UpdateLayoutToPortrait()
        {
            containerAspectRatioFitter.aspectRatio = CONTAINER_PORTRAIT_ASPECT_RATIO;

            lightsRectTransform.pivot = LIGHTS_PORTRAIT_PIVOT;

            Vector2 lightBoardWidthByHeight = new Vector2(lightsRectTransform.rect.width, lightsRectTransform.rect.height);
            lightsGridLayoutGroup.spacing = lightBoardWidthByHeight * LIGHTS_SPACING_FACTOR;
            lightsGridLayoutGroup.cellSize = lightBoardWidthByHeight * LIGHTS_CELL_SIZE_FACTOR;

            controlsAspectRatioFitter.aspectRatio = CONTROLS_PORTRAIT_ASPECT_RATIO;
            controlsRectTransform.pivot = CONTROLS_PORTRAIT_PIVOT;
            controlsGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;

            Vector2 controlBoardWidthByHeight = new Vector2(controlsRectTransform.rect.width, controlsRectTransform.rect.height);
            controlsGridLayoutGroup.spacing = controlBoardWidthByHeight * CONTROLS_SPACING_FACTOR;
            controlsGridLayoutGroup.cellSize = controlBoardWidthByHeight * CONTROLS_PORTRAIT_CELL_SIZE_FACTOR;
        }
    }
}
