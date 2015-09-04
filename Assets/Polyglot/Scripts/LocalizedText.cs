﻿using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Polyglot
{
    [AddComponentMenu("UI/Localized Text", 11)]
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour, ILocalize
    {
        [Tooltip("The text component to localize")]
        [SerializeField]
        private Text text;

        [Tooltip("The key to localize with")]
        [SerializeField]
        private string key;

        public string Key { get { return key; } }

        [UsedImplicitly]
        public void Reset()
        {
            text = GetComponent<Text>();
        }

        [UsedImplicitly]
        public void Start()
        {
            LocalizationManager.Instance.AddOnLocalizeEvent(this);
        }

        public void OnLocalize()
        {
            var flags = text.hideFlags;
            text.hideFlags = HideFlags.DontSaveInEditor;
            text.text = LocalizationManager.Get(key);
            text.hideFlags = flags;
        }
    }
}