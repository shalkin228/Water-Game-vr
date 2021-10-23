using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class FaunWaterMovingBase : MonoBehaviour
{
    [Header("Faun Base")]
    [SerializeField] private float _movingSpeed, _goingMinTime, _goingMaxTime, _rotationSpeed, 
        _minWaterDistance, _minGroundDistance;
    [SerializeField] private Quaternion _rightRotation, _leftRotation, _upRotation, _downRotation;
    private float _groundHeightToEntity;
    private bool _pathFinded;
    private Path _currentPath;
    private Rigidbody _rigidbody;
    

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        var hitsInfo = Physics.RaycastAll(new Ray(transform.position, Vector2.down));
        foreach(var hitInfo in hitsInfo)
        {
            if(hitInfo.collider.TryGetComponent<Terrain>(out Terrain component))
            {
                _groundHeightToEntity = hitInfo.distance;
                break;
            }
        }

        var oceanHeight = OceanRenderer.Instance.SeaLevel;

    }



    protected virtual IEnumerator RotateToPathPoint(Quaternion direction)
    {
        while(transform.rotation.x >= direction.x &&
            transform.rotation.y >= direction.y &&
            transform.rotation.z >= direction.z)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, 
                Time.deltaTime * _rotationSpeed);

            yield return null;
        }
    }
}
[System.Serializable]
public class Path
{
    public Quaternion direction;
    public float goingTime;

    public Path(Quaternion dir, float goingTime)
    {
        direction = dir;
        this.goingTime = goingTime;
    }
}
