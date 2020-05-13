using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Hook.HXF
{
    [RequireComponent(typeof(Collider))]
    public class ContentPlacement : MonoBehaviour
    {
        #region Constants

        private const float kDeltaScaleFactor = 0.005f;
        
        #endregion
        
        #region Properties

        [SerializeField] private GameObject GroundPlane;
        
        private bool _isPositionable;
        private Vector3 _previousPosition;

        #endregion
        
        #region MonoBehaviour

        private void Update()
        {
            CheckContentPositionable();
        }
        
        #endregion
        
        #region Class Methods

        private void CheckContentPositionable()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && string.Equals(hit.collider.name, name))
                    {
                        _isPositionable = true;
                        _previousPosition = Input.mousePosition;
                        
                        if (ContentPlacementEvent.OnContentPlacementStart != null)
                        {
                            ContentPlacementEvent.OnContentPlacementStart(this, new EventArgs());
                        }
                    }
                }
                else
                {
                    Debug.Log("No object hit");
                }
            }

            if (Input.GetMouseButton(0) && _isPositionable)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && string.Equals(hit.collider.name, name))
                    {
                        var newPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        transform.position = newPosition;
                    }
                }
                else
                {
                    Debug.Log("Did not hit collider on content.");
                }
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                _isPositionable = false;
                
                if (ContentPlacementEvent.OnContentPlacementEnd != null)
                {
                    ContentPlacementEvent.OnContentPlacementEnd(this, new EventArgs());
                }
            }
        }
        
        #endregion
    }
}