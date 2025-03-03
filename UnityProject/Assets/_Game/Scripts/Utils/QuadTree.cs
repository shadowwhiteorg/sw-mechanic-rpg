using System.Collections.Generic;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.Utils
{
    public class QuadTree<T> where T : IDamageable
    {
        private Rect _bounds;
        private int _capacity;
        private List<T> _objects;
        private QuadTree<T>[] _children;
        private bool _divided;
        private QuadTree<T> _northwest, _northeast, _southwest, _southeast;
    
        public QuadTree(Rect bounds, int capacity)
        {
            _bounds = bounds;
            _capacity = capacity;
            _objects = new List<T>();
            _divided = false;
        }
    
        public bool Insert(T obj)
        {
            if (!_bounds.Contains(new Vector2(obj.GetPosition().x, obj.GetPosition().z)))
                return false;
    
            if (_objects.Count < _capacity)
            {
                _objects.Add(obj);
                return true;
            }
    
            if (!_divided)
                Subdivide();
    
            return (_northwest.Insert(obj) || _northeast.Insert(obj) ||
                    _southwest.Insert(obj) || _southeast.Insert(obj));
        }
        
        public void Remove(T obj)
        {
            if (!_bounds.Contains(obj.GetPosition())) return;

            _objects.Remove(obj);

            if (_children != null)
            {
                foreach (var child in _children)
                    child.Remove(obj);
            }
        }
        
    
        private void Subdivide()
        {
            float x = _bounds.x;
            float y = _bounds.y;
            float w = _bounds.width / 2f;
            float h = _bounds.height / 2f;

            _children = new QuadTree<T>[4]
            {
                _northwest = new QuadTree<T>(new Rect(x, y, w, h), _capacity),
                _northeast = new QuadTree<T>(new Rect(x + w, y, w, h), _capacity),
                _southwest = new QuadTree<T>(new Rect(x, y + h, w, h), _capacity),
                _southeast = new QuadTree<T>(new Rect(x + w, y + h, w, h), _capacity)
            };
    
            _divided = true;
        }
    
        public T FindNearest(Vector3 position, float searchRadius, T exclude = default, List<T> excludeList = null)
        {
            float closestDist = searchRadius * searchRadius;
            T closestObject = default;
    
            foreach (var obj in _objects)
            {
                // if(exclude != null && obj.Equals(exclude)) continue;
                if(excludeList != null && excludeList.Contains(obj)) continue;
                if(exclude != null && obj.Equals(exclude))continue;
                float sqrDist = (obj.GetPosition() - position).sqrMagnitude;
                if (sqrDist < closestDist)
                {
                    closestDist = sqrDist;
                    closestObject = obj;
                }
            }
    
            if (_divided)
            {
                closestObject ??= _northwest.FindNearest(position, searchRadius);
                closestObject ??= _northeast.FindNearest(position, searchRadius);
                closestObject ??= _southwest.FindNearest(position, searchRadius);
                closestObject ??= _southeast.FindNearest(position, searchRadius);
            }
    
            return closestObject;
        }
    }
}