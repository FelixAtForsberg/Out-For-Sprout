using UnityEngine;

namespace _5_Scripts.Player
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(EdgeCollider2D))]
    public class CollisionTrail : MonoBehaviour
    {
        public float newPointDelta = 0.1f;

        private float _newPointDeltaSqr;

        private LineRenderer _lineRenderer;
        private EdgeCollider2D _edgeCollider;
        
        private Vector2[] _rootColliderPoints;
        private Vector3[] _rootRenderPoints;
        private int _curPointCount;
        
        private Vector3 _lastPointPos;

        // Start is called before the first frame update
        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _edgeCollider = GetComponent<EdgeCollider2D>();
            
            // use square because it's faster to compute
            _newPointDeltaSqr = Mathf.Pow(newPointDelta, _newPointDeltaSqr);
            
            _rootRenderPoints = new Vector3[8192];
            // _rootColliderPoints = new Vector2[8192];
            //
            
            
            _curPointCount = 0;
           
            AddRootPoint(transform.position);
        }

        // Faster than Unity's .distance()
        private float SqrDist(Vector3 a, Vector3 b)
        {
            return (a - b).sqrMagnitude;
        }
        
        private bool LastPointFurtherThanDelta => SqrDist(transform.position, _lastPointPos) > _newPointDeltaSqr;

        // Sets last point, updates _edgeCollider and _lineRenderer
        void AddRootPoint(Vector3 pointPos)
        {
            _lastPointPos = pointPos;
            _curPointCount++;

            _rootRenderPoints[_curPointCount] = pointPos;
            
            _lineRenderer.positionCount = _curPointCount;
            for (var i = 0; i < _curPointCount; i++)
            {
                _lineRenderer.SetPosition(i, _rootRenderPoints[i]);
                
            }
            
            // _edgeCollider.points = _rootColliderPoints;
            
            
            // _rootColliderPoints[_curPointCount] = new Vector2(pointPos.x, pointPos.z);

            // needs at least 2 points to work
            // https://docs.unity3d.com/ScriptReference/EdgeCollider2D.SetPoints.html
            
            
            
        }
        
        // Update is called once per frame
        void FixedUpdate()
        {
            if (LastPointFurtherThanDelta)
            {
                AddRootPoint(transform.position);
            }
            

        }
    }
}
