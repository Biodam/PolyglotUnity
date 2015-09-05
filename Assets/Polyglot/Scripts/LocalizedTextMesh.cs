﻿#if UNITY_5
using JetBrains.Annotations;
#endif
using UnityEngine;

namespace Polyglot
{
    [AddComponentMenu("Mesh/Localized TextMesh")]
    [RequireComponent(typeof(TextMesh))]
    public class LocalizedTextMesh : MonoBehaviour, ILocalize
    {

        [Tooltip("The TextMesh component to localize")]
        [SerializeField]
        private TextMesh text;

        [Tooltip("The key to localize with")]
        [SerializeField]
        private string key;

        public string Key { get { return key; } }

#if UNITY_5
        [UsedImplicitly]
#endif
        public void Reset()
        {
            text = GetComponent<TextMesh>();
        }

#if UNITY_5
        [UsedImplicitly]
#endif
        public void Start()
        {
            LocalizationManager.Instance.AddOnLocalizeEvent(this);
        }

        public void OnLocalize()
        {
            var flags = text.hideFlags;
            text.hideFlags = HideFlags.DontSave;
            text.text = LocalizationManager.Get(key);

            var direction = LocalizationManager.Instance.SelectedLanguageDirection;

            if (IsOppositeDirection(text.alignment, direction))
            {
                switch (text.alignment)
                {
                    case TextAlignment.Left:
                        text.alignment = TextAlignment.Right;
                        break;
                    case TextAlignment.Right:
                        text.alignment = TextAlignment.Left;
                        break;
                }
            }
            text.hideFlags = flags;
        }

        private bool IsOppositeDirection(TextAlignment alignment, LanguageDirection direction)
        {
            return (direction == LanguageDirection.LeftToRight && IsAlignmentRight(alignment)) || (direction == LanguageDirection.RightToLeft && IsAlignmentLeft(alignment));
        }

        private bool IsAlignmentRight(TextAlignment alignment)
        {
            return alignment == TextAlignment.Right;
        }
        private bool IsAlignmentLeft(TextAlignment alignment)
        {
            return alignment == TextAlignment.Left;
        }
    }
}