using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private Transform _loadingObjectsParent;
    [SerializeField] private float _maxRenderDistance;
    private List<ChunkLoadingObject> _loadingObjects = new List<ChunkLoadingObject>();
    private Transform _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main.transform;
        foreach (Transform child in _loadingObjectsParent)
        {
            if(child.TryGetComponent(out ChunkLoadingObject loadingObject))
            {
                _loadingObjects.Add(loadingObject);
                
                if(Vector3.Distance(loadingObject.transform.position, _mainCamera.position) < _maxRenderDistance)
                {
                    loadingObject.gameObject.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        foreach(ChunkLoadingObject loadingObject in _loadingObjects)
        {
            if(Vector3.Distance(loadingObject.transform.position, _mainCamera.position) > _maxRenderDistance)
            {
                loadingObject.gameObject.SetActive(false);
            }
            else
            {
                loadingObject.gameObject.SetActive(true);
            }
        }
    }
}
