using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationIndicator : MonoBehaviour
{
    #region Properties
    
    [SerializeField] private GameObject ObjectToFollow;
    [SerializeField] private GameObject IndicatorVisual;

    private RectTransform _transform;
    private bool _isVisible;
    private Collider _colliderForObjectToFollow;
    
    #endregion
    
    #region MonoBehaviour
    
    void Start()
    {
        _transform = GetComponent<RectTransform>();

        if (ObjectToFollow != null)
        {
            AddObjectToFollow(ObjectToFollow);
        }
    }
    
    void Update()
    {
        if (ObjectToFollow != null && Camera.main != null)
        {
            _isVisible = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), _colliderForObjectToFollow.bounds);
            IndicatorVisual.SetActive(!_isVisible);
            if (!_isVisible)
            {
                var newScreenPos = Camera.main.WorldToScreenPoint(ObjectToFollow.transform.position);
                newScreenPos.x = Mathf.Clamp(newScreenPos.x, 0 + _transform.rect.width, Screen.width - _transform.rect.width);
                newScreenPos.y = Mathf.Clamp(newScreenPos.y, 0 + _transform.rect.height, Screen.height - _transform.rect.height);
                transform.position = newScreenPos;
                var uiWorldPos = Camera.main.ScreenToWorldPoint(transform.position);
                var direction = ObjectToFollow.transform.position - uiWorldPos;
                var angle = -Vector2.SignedAngle(direction, transform.right);
                IndicatorVisual.transform.rotation = Quaternion.Euler(0,0,angle);
            }
        }
    }
    
    #endregion
    
    #region Class Methods

    public void AddObjectToFollow(GameObject objectToFollow)
    {
        ObjectToFollow = objectToFollow;
        _colliderForObjectToFollow = ObjectToFollow.transform.GetComponentInChildren<Collider>();
    }
    
    #endregion
}
