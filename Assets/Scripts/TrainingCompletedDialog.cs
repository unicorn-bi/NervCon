using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gtec.UnityInterface
{
    public class TrainingCompletedDialog : MonoBehaviour
    {
        #region Event Handlers...

        public event EventHandler BtnRetrain_Click;
        public event EventHandler BtnContinueFlashing_Click;

        #endregion

        #region Private Members...

        [SerializeField]
        [Tooltip("Button to retrain the classifier.")]
        private Button _btnRetrain;
        
        [SerializeField]
        [Tooltip("Button to continue flashing.")]
        private Button _btnContinue;

        #endregion

        void Start()
        {
            _btnRetrain.onClick.AddListener(OnRetrainClick);
            _btnContinue.onClick.AddListener(OnContinueClick);
        }

        private void OnContinueClick()
        {
            BtnContinueFlashing_Click?.Invoke(this, null);
        }

        private void OnRetrainClick()
        {
            BtnRetrain_Click?.Invoke(this, null);
        }
    }
}