using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hook.HXF
{
    public class PageControlController : MonoBehaviour
    {
        #region Properties

        public int ActiveIndicator
        {
            get { return _activeIndicator; }
            set
            {
                UpdateIndicators(value);
            }
        }
        [SerializeField] private GameObject PageIndicatorPrefab;
        [SerializeField] private int NumberOfPages;

        private PageIndicator[] _indicators;
        private int _activeIndicator;
        
        #endregion
        
        #region Class Methods

        /// <summary>
        /// Initialize the PageControl instance
        /// </summary>
        /// <param name="activeIndicator">Indicator that should be active (zero based)</param>
        private void Initialize(int activeIndicator)
        {
            // creating array to hold indicator gameobjects
            _indicators = new PageIndicator[NumberOfPages];
            
            // creating page indicators
            for (int i = 0; i < NumberOfPages; i++)
            {
                var indicator = Instantiate(PageIndicatorPrefab).GetComponent<PageIndicator>();
                indicator.UpdateState(i == activeIndicator);
                indicator.transform.SetParent(transform, true);

                _indicators[i] = indicator;
            }
        }

        private void UpdateIndicators(int activeIndicator)
        {
            // checking to make sure that indicators are initialized
            if (_indicators == null)
            {
                Initialize(activeIndicator);
            }
            
            _activeIndicator = activeIndicator;
            
            for (int i = 0; i < NumberOfPages; i++)
            {
                _indicators[i].UpdateState(i == _activeIndicator);
            }
        }
        
        #endregion
    }
}