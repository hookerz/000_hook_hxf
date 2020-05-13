using System;
using System.Collections;
using System.Collections.Generic;
using Hook.NTC;
using UnityEngine;

namespace Hook.HXF
{
    public class FiniteStateMachine
    {
        #region Properties

        protected AbstractState<IAbstractStateController> currentState;

        #endregion
        
        #region Constructor

        public FiniteStateMachine()
        {
        }
        
        #endregion
        
        #region Class Methods

        public virtual void Transition<T>(T currentState) where T:AbstractState<IAbstractStateController>
        {
        }
        
        #endregion
    }
}