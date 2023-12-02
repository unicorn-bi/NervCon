using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gtec.UnityInterface
{
    public class ConnectionDialog : MonoBehaviour
    {
        #region Event Handlers...

        public event EventHandler BtnConnect_Click;

        #endregion

        #region Properties...

        public string Serial
        {
            get { return _serial; }
        }

        #endregion

        #region Private Members...

        private string _serial;

        [SerializeField]
        [Tooltip("Button to connect to the selected device.")]
        private Button _btnConnect;
    
        [SerializeField]
        [Tooltip("Dropdown to select the device to connect to.")]
        private TMP_Dropdown _ddDevices;

        #endregion

        void Start()
        {
            _ddDevices.options.Clear();
            List<string> serials = ERPBCIManager.Instance.GetAvailableDevices();
            foreach (string serial in serials)
                _ddDevices.options.Add(new TMP_Dropdown.OptionData(serial));
            _ddDevices.RefreshShownValue();
            _btnConnect.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            _ddDevices.RefreshShownValue();
            _serial = _ddDevices.options[_ddDevices.value].text;
            BtnConnect_Click?.Invoke(this, null);
        }
    }
}