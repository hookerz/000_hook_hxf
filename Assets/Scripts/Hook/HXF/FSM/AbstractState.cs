using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hook.HXF
{
    public class AbstractState<T> where T : IAbstractStateController
    {
        #region Properties

        public T StateController { get; private set; }
        
        #endregion

        #region Constructor

        public AbstractState(T stateController)
        {
            StateController = stateController;
        }
        
        #endregion
        
        #region Class Methods

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }
        
        #endregion
    }
}