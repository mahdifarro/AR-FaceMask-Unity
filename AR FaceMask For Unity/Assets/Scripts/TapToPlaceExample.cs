using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlaceExample : MonoBehaviour
{

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    ARRaycastManager _raycastManager;
    ARAnchorManager _anchorManager;
    List<ARAnchor> _anchorPoint;
    ARPlaneManager _planeManager;

    void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
        _anchorManager = GetComponent<ARAnchorManager>();
        _planeManager = GetComponent<ARPlaneManager>();
        _anchorPoint = new List<ARAnchor>();
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (_raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = hits[0].pose;
            TrackableId planeId = hits[0].trackableId; //get the ID of the plane hit by the raycast
            var anchor = _anchorManager.AttachAnchor(_planeManager.GetPlane(planeId), hitPose);
            if (anchor != null)
            {
                RemoveAllReferencePoints();
                _anchorPoint.Add(anchor);
            }
        }
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    //Remove all reference points created
    public void RemoveAllReferencePoints()
    {
        foreach (var anchorPoint in _anchorPoint)
        {
            Destroy(anchorPoint.gameObject);
        }
        _anchorPoint.Clear();
    }

}