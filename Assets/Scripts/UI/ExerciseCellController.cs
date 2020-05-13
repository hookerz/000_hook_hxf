using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Hook.NTC
{
    public enum ExerciseCellState
    {
        Closed,
        Open
    }
    
    public class ExerciseCellController : MonoBehaviour
    {
        #region Properties

        [SerializeField] private GameObject DetailsContent;
        [SerializeField] private Transform DetailButtonContainer;
        [SerializeField] private VideoContentController VideoController;
        [SerializeField] private string ExerciseName;
        [SerializeField] private string ExerciseDisplayName;
        [SerializeField] private VideoClip ExerciseVideo;
        [SerializeField] private Button DetailsButton;

        private ExerciseCellState _state = ExerciseCellState.Closed;
        private float _homePosition;
        private float _openPosition;
        private CanvasGroup _detailsCanvasGroup;

        public float HomePosition
        {
            get { return _homePosition; }
        }

        public float OpenPosition
        {
            get { return _openPosition; }
        }

        public ExerciseCellState State
        {
            get { return _state; }
        }

        public string ExerciseId
        {
            get { return ExerciseName; }
        }

        public string DisplayName
        {
            get { return ExerciseDisplayName; }
        }
        
        public GameObject DetailsContentArea
        {
            get { return DetailsContent; }
        }

        public VideoClip ExerciseVideoClip
        {
            get { return ExerciseVideo; }
        }
        
        #endregion
    
        #region MonoBehaviour

        private void Start()
        {
            _homePosition = transform.localPosition.y;
            _openPosition = transform.localPosition.y - DetailsContent.GetComponent<RectTransform>().rect.height;
            _detailsCanvasGroup = DetailsContent.GetComponent<CanvasGroup>();
        }
        
        #endregion
    
        #region Class Methods

        public string GetExerciseDisplayName()
        {
            return ExerciseDisplayName;
        }
        
        public string GetExerciseName()
        {
            return ExerciseName;
        }
        
        public void UpdateState(ExerciseCellState newState)
        {
            // saving state
            _state = newState;
            
            switch (_state)
            {
                case ExerciseCellState.Closed:
                    // rotating detail dropdown button
                    DetailButtonContainer.DORotate(new Vector3(0, 0, 0), 0.25f);
                    
                    // hiding details content area
                    DetailsContent.SetActive(false);
                    
                    // enabling details button now that cell is not selected
                    DetailsButton.enabled = true;
                    break;
                case ExerciseCellState.Open:
                    // rotating detail dropdown button
                    DetailButtonContainer.DORotate(new Vector3(0, 0, 180f), 0.25f);
                    
                    // displaying details content area
                    DetailsContent.SetActive(true);
                    
                    // disabling details button since it is selected
                    DetailsButton.enabled = false;
                    break;
            }
        }
        
        #endregion
        
        #region Event Handlers

        public void OnARButtonSelected()
        {
            if (_state == ExerciseCellState.Open)
            {
                VideoController.Stop();
            }
            
            if (ExerciseTableEvents.OnARViewSelected != null)
            {
                ExerciseTableEvents.OnARViewSelected(this, new ExerciseTableEventArgs(this));
            }
        }
        
        public void OnDetailsSelected()
        {
            if (ExerciseTableEvents.OnDetailsSelected != null)
            {
                ExerciseTableEvents.OnDetailsSelected(this, new ExerciseTableEventArgs(this));
            }
        }
    
        #endregion
    }
}
