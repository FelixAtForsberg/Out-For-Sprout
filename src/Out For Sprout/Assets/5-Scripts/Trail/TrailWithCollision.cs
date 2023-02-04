using System.Collections.Generic;
using UnityEngine;

namespace _5_Scripts.Player
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(EdgeCollider2D))]
    public class TrailWithCollision : MonoBehaviour
    {
        public float newPointDelta = 0.1f;
        public float collisionRadius = .1f;
        public float trailRenderRadius = 0.1f;

        private float _newPointDeltaSqr;
        
        private LineRenderer _lineRenderer;

        // puts the EdgeCollider on this because otherwise it will follow the player
        private GameObject _worldRelativeEmpty;
        
        private EdgeCollider2D _edgeCollider;
        private List<Vector2> _trailColliderList;

        private Vector3 _lastPointPos;
        private Vector3[] _rootRenderPoints;
        private int _curPointCount;

        // Start is called before the first frame update
        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            
            // use square because it's faster to compute
            _newPointDeltaSqr = Mathf.Pow(newPointDelta, _newPointDeltaSqr);
            InitLineRenderer();
            InitEdgeCollider();

            AddTrailPoint(transform.position);
        }

        private void InitLineRenderer()
        {
            _curPointCount = 0;
            _rootRenderPoints = new Vector3[8192];
        }
        
        // initializes the point list and creates a world relative GameObject
        // since otherwise the EdgeCollider would follow the GameObject this script is 
        // attached to and we'd have to recalculate every point in world space 
        private void InitEdgeCollider()
        {
            _trailColliderList = new List<Vector2>();
            _worldRelativeEmpty = new GameObject("Trail Collider");
            _edgeCollider = _worldRelativeEmpty.AddComponent<EdgeCollider2D>();
            _worldRelativeEmpty.AddComponent<TrailCollisionHandler>();
        }
        
        // Faster than Unity's .distance()
        private float SqrDist(Vector3 a, Vector3 b)
        {
            return (a - b).sqrMagnitude;
        }
        
        private bool LastPointFurtherThanDelta => SqrDist(transform.position, _lastPointPos) > _newPointDeltaSqr;

        // Sets last point, updates _edgeCollider and _lineRenderer
        private void AddTrailPoint(Vector3 pointPos)
        {
            _lastPointPos = pointPos;
            _curPointCount++;

            _rootRenderPoints[_curPointCount] = pointPos;
            _trailColliderList.Add(new Vector2(pointPos.x, pointPos.y));
            // I'm afraid to use the entire _RootColliderPoints[] array here
            // since I don't know if Unity would be checking against the entire thing
            // while 95% of the points are at Vector2(0,0)
            // var collisionPointsBuffer = new Vector2[_curPointCount];
            
            // needs at least 2 points to work
            // https://docs.unity3d.com/ScriptReference/EdgeCollider2D.SetPoints.html
            if (_trailColliderList.Count >= 2)
                _edgeCollider.SetPoints(_trailColliderList);
            
            _lineRenderer.positionCount = _curPointCount;
            for (var i = 0; i < _curPointCount; i++)
            {
                _lineRenderer.SetPosition(i, _rootRenderPoints[i]);
            }


        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (LastPointFurtherThanDelta)
            {
                AddTrailPoint(transform.position);
            }

        }
    }
}
