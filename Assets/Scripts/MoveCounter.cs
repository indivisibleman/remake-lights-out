using UnityEngine;
using UnityEngine.UI;

namespace LightsOut
{
    public class MoveCounter : MonoBehaviour
    {
        private int moves = 0;
        private Text moveText;

        void Awake()
        {
            moveText = GetComponentInChildren<Text>();
        }

        public void IncrementCount()
        {
            moves++;
            moveText.text = moves.ToString();
        }

        public void ResetCount()
        {
            moves = 0;
            moveText.text = moves.ToString();
        }

        public int GetCount()
        {
            return moves;
        }
    }
}
