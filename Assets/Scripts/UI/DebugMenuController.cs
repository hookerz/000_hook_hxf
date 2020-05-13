using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hook.HXF
{
    public class DebugMenuController : MonoBehaviour
    {
        #region Constants
        
        private float kDebugMenuTime = 3f;
        
        #endregion
        
        #region Properties
        
        public static bool IsDebugEnabled { get { return _isDebugEnabled; } }
        private static bool _isDebugEnabled = true;
        
        [SerializeField] private GameObject MenuContent;
        
        private float _touchTimeDown;
        private float _debugMenuTime;
        
        #endregion
        
        #region MonoBehaviour

        private void Update()
        {
            // check for debug menu
            if (!MenuContent.activeSelf)
            {
                if (Input.touchCount == 3)
                {
                    _debugMenuTime += Time.deltaTime;
                    if (_debugMenuTime > kDebugMenuTime)
                    {
                        MenuContent.SetActive(true);
                        _debugMenuTime = 0f;
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.D))
                {
                    _debugMenuTime += Time.deltaTime;
                    if (_debugMenuTime > kDebugMenuTime)
                    {
                        MenuContent.SetActive(true);
                        _debugMenuTime = 0f;
                    }
                }
            }
            
            // update UI
        }

        #endregion
        
        #region Event Handlers

        public void OnCloseSelected()
        {
            _isDebugEnabled = false;
            MenuContent.SetActive(false);
        }
        
        #endregion
    }
}
