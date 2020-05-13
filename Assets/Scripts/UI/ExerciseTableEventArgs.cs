using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hook.NTC
{
    public static class ExerciseTableEvents
    {
        public static EventHandler<ExerciseTableEventArgs> OnARViewSelected;
        public static EventHandler<ExerciseTableEventArgs> OnDetailsSelected;
    }
    
    public class ExerciseTableEventArgs : EventArgs
    {
        #region Properties
        
        public ExerciseCellController SelectedCell { get; private set; }
        
        #endregion
        
        #region Constructor

        public ExerciseTableEventArgs(ExerciseCellController selectedCell)
        {
            SelectedCell = selectedCell;
        }
        
        #endregion
    }
}