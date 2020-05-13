using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hook.HXF
{
    public class PageIndicator : MonoBehaviour
    {
        #region Properties

        [SerializeField] private GameObject OffIndicator;
        [SerializeField] private GameObject OnIndicator;

        private bool _state;
        
        #endregion
        
        #region Class Methods

        public void UpdateState(bool state)
        {
            // saving state
            _state = state;
            
            // setting indicator
            OnIndicator.SetActive(_state);
            OffIndicator.SetActive(!_state);
        }
        
        #endregion
    }
}