using Hook.HXF;
using Hook.PlayerInput;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hook.NTC
{
    public class ExerciseTableController : BaseController
    {
        #region Properties

        [SerializeField] private List<Transform> ExerciseCells;
        [SerializeField] private RectTransform CellContent;
        [SerializeField] private VideoContentController VideoPlayer;
        [SerializeField] private GameObject VideoScrim;
        [SerializeField] private GameObject Spacer;

        private ExerciseCellController _selectedCell;
        private CanvasGroup _canvasGroup;
        private RectTransform _viewPort;
        private float _startingCellContentHeight;
        private float _cellContentAdjustmentAmount;
        private float _videoPlayerHeight;
        
        protected CanvasGroup canvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = GetComponent<CanvasGroup>();
                }
                
                return _canvasGroup;
            }
        }

        #endregion
        
        #region MonoBehaviour
        
        void Start()
        {
            _viewPort = CellContent.parent.GetComponent<RectTransform>();
            _startingCellContentHeight = CellContent.rect.height;
            var exerciseCellController = ExerciseCells[0].GetComponent<ExerciseCellController>();
            var exereciseClip = exerciseCellController.ExerciseVideoClip;
            _videoPlayerHeight = _viewPort.rect.width / ((float)exereciseClip.width / exereciseClip.height);
            
            InputEvents.OnAndroidBackButtonDetected += OnAndroidBackButtonDetected;
            ExerciseTableEvents.OnARViewSelected += OnARSelectedFromRow;
            ExerciseTableEvents.OnDetailsSelected += OnDetailSelected;
        }

        private void OnDestroy()
        {
            InputEvents.OnAndroidBackButtonDetected -= OnAndroidBackButtonDetected;
            ExerciseTableEvents.OnARViewSelected -= OnARSelectedFromRow;
            ExerciseTableEvents.OnDetailsSelected -= OnDetailSelected;
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!DidHitARButton())
                {
                    // exit watch state
                    OnExerciseStateTransitionSelected?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    // enter setup/launch state (AR states)
                    OnARStateTransitionSelected?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        
        #endregion
        
        #region Class Methods

        private bool DidHitARButton()
        {
            var didHit = false;
            var hits = RaycastMouse();

            for (var i = 0; i < hits.Count; i++)
            {
                if (string.Equals(hits[i].gameObject.name, "ARButton"))
                {
                    didHit = true;
                    break;
                }
            }
            
            return didHit;
        }
        
        private List<RaycastResult> RaycastMouse()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1
            };

            pointerData.position = Input.mousePosition;
            
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            
            return results;
        }
        
        #endregion
        
        #region Event Handlers

        private void OnAndroidBackButtonDetected(object sender, EventArgs e)
        {
            OnExerciseStateTransitionSelected?.Invoke(this, EventArgs.Empty);
        }

        private void OnARSelectedFromRow(object sender, ExerciseTableEventArgs e)
        {
            OnARStateTransitionSelected?.Invoke(this, EventArgs.Empty);
        }

        private void OnDetailSelected(object sender, ExerciseTableEventArgs e)
        {
            _selectedCell = e.SelectedCell;
            
            OnWatchStateTransitionSelected?.Invoke(this, EventArgs.Empty);
        }
        
        #endregion
        
        #region INTCExerciseStateController

        public List<ExerciseCellController> GetExerciseCells()
        {
            var cells = new List<ExerciseCellController>();
            ExerciseCells.ForEach((cell) =>
            {
                cells.Add(cell.GetComponent<ExerciseCellController>());
            });

            return cells;
        }
        
        public CanvasGroup GetTableCanvasGroup()
        {
            return canvasGroup;
        }

        public void SetSelectedCell(ExerciseCellController value)
        {
            _selectedCell = value;
        }

        public event EventHandler OnWatchStateTransitionSelected;
        
        #endregion
        
        #region INTCWatchStateController

        public event EventHandler OnARStateTransitionSelected;
        public event EventHandler OnExerciseStateTransitionSelected;
        
        public VideoContentController GetVideoPlayer()
        {
            return VideoPlayer;
        }

        public GameObject GetSpacer()
        {
            return Spacer;
        }

        public GameObject GetVideoScrim()
        {
            return VideoScrim;
        }

        public ExerciseCellController GetSelectedCell()
        {
            return _selectedCell;
        }

        public RectTransform GetTableContent()
        {
            return CellContent;
        }

        public float GetStartingContentHeight()
        {
            return _startingCellContentHeight;
        }

        public float GetVideoPlayerHeight()
        {
            return _videoPlayerHeight;
        }

        #endregion
    }
}