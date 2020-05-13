using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hook.NASA
{
    public class PlanePointLocator : MonoBehaviour
    {
        #region Properties

        protected Vector3 center
        {
            get { return _collider.bounds.center; }
        }
        
        protected Vector3 max
        {
            get { return _collider.bounds.max; }
        }
        
        protected Vector3 min
        {
            get { return _collider.bounds.min; }
        }
        
        private MeshCollider _collider;
        private Vector3 _size;
        
        #endregion
        
        #region MonoBehaviour
        
        void Awake()
        {
            _collider = GetComponent<MeshCollider>();
            _size = _collider.bounds.size;
        }
        
        void Update()
        {
        }
        
        #endregion
        
        #region Class Methods

        public Vector3 GetRandomPosition()
        {
            Vector3 pos = new Vector3();
            pos.x = Random.Range(min.x, max.x);
            pos.y = Random.Range(min.y, max.y);
            pos.z = Random.Range(min.z, max.z);
            
            return pos;
        }
        
        #endregion
    }
}
