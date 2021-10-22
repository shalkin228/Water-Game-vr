using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class FaunWaterMovingBase : MonoBehaviour
{
    [Header("Faun Base")]
    [SerializeField] private float _movingSpeed, _goingMinTime, _goingMaxTime, _rotationSpeed;
    private float _groundHeightToEntity;
    private Vector3 _velocity;
    private bool _pathFinded, _canUpdatePos;
    private Path _currentPath;
    

    protected virtual void Start()
    {
        FindPath();

        _canUpdatePos = true;
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

        FindPath();
    }
    protected virtual void LateUpdate()
    {
        UpdatePos();
    }

    protected virtual void FindPath()
    {
        if (_pathFinded)
            return;

        var dir = Random.rotation;
        float goingTime = Random.Range(_goingMinTime, _goingMaxTime);
        _currentPath = new Path(dir, goingTime);

        StartCoroutine(RotateToPathPoint());

        _pathFinded = true;
    }

    protected virtual IEnumerator RotateToPathPoint()
    {
        _canUpdatePos = false;
        while(transform.rotation != _currentPath.direction)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _currentPath.direction, 
                Time.deltaTime * _rotationSpeed);

            yield return null;
        }
        _canUpdatePos = true;
    }

    protected virtual IEnumerator GoingTimer(float time)
    {
        _velocity = transform.forward * _movingSpeed;

        yield return new WaitForSeconds(time);

        _pathFinded = false;
    }

    protected virtual void UpdatePos()
    {
        if (!_canUpdatePos)
            return;

        var move = _velocity * Time.deltaTime * _movingSpeed;
        move = new Vector3(Mathf.Max(move.x * _movingSpeed, 0), Mathf.Max(move.y * _movingSpeed, 0),
            Mathf.Max(move.z * _movingSpeed, 0));

        transform.Translate(move);

        _velocity = new Vector3(Mathf.Max(_velocity.x - Time.deltaTime, 0), Mathf.Max(_velocity.y - Time.deltaTime, 0),
            Mathf.Max(_velocity.z - Time.deltaTime, 0));
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
