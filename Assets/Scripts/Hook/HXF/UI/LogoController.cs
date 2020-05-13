using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hook.HXF
{
    public class LogoController : MonoBehaviour
    {
        #region Properties

        [SerializeField] private string RootSceneName;
        
        #endregion
        
        #region Event Handlers

        public void OnLogoAnimationComplete()
        {
            SceneManager.LoadScene(RootSceneName);
        }
        
        #endregion
    }
}