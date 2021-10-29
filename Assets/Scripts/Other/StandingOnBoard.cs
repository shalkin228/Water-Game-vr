using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StandingOnBoard : MonoBehaviour
{
    [SerializeField] private ActionBasedContinuousMoveProvider _moveProvider;
    [SerializeField] private CharacterController _char;
    public bool isOnBoard;
    private Transform _board;
    private Vector3 _lastBoardPos;
    private WaterMovement _wateMovement;

    private void Start()
    {
        _wateMovement = GetComponentInParent<WaterMovement>();
    }

    private void FixedUpdate()
    {
        if (isOnBoard)
        {
            var difference = _lastBoardPos - _board.position;
            transform.position = new Vector3(transform.position.x + difference.x,
            transform.position.y + difference.y,
            transform.position.z + difference.z);
            

            _lastBoardPos = _board.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Board"))
        {
            _board = other.transform.parent.parent;

            var difference = (transform.parent.position - other.gameObject.transform.position).normalized;

            if (difference.y > 0.2f)
            {
                isOnBoard = true;

                _lastBoardPos = _board.position;

                _wateMovement.canChangeGravity = false;
                _moveProvider.useGravity = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _wateMovement.canChangeGravity = true;
        isOnBoard = false;
    }
}
