﻿using UnityEngine;
using UnityEngine.UI;

namespace MD.UI.MainMenu
{
    public class MenuTab : MonoBehaviour
    {
        #region SERIALIZE FIELDS
        [SerializeField]
        private MainMenuTabToggler toggler = null;

        [SerializeField]
        private MenuType type;

        [SerializeField]
        private Button activeButton, inactiveButton = null;
        #endregion

        public MenuType MenuType { get => type; }

        void Start()
        {
            inactiveButton.onClick.AddListener(Toggle);
        }
        
        private void Toggle() =>toggler.Toggle(type);

        public void Activate()
        {
            activeButton.gameObject.SetActive(true);
            inactiveButton.gameObject.SetActive(false);
        }

        public void Deactivate()
        {
            activeButton.gameObject.SetActive(false);
            inactiveButton.gameObject.SetActive(true);
        }
    }
}