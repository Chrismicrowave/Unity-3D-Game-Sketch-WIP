using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float raycastDistance = 1000f;

    List<GameObject> _lastFrameHits;
    List<GameObject> _curFrameHits;

    Vector3 _playerPo = new Vector3(0f,0.5f,0f);

    float DefaultTrans = -0.2f;
    float CastTrans = -0.9f;

    // Start is called before the first frame update
    void Start()
    {
        _curFrameHits = new List<GameObject>();
        _lastFrameHits = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Reset curFrameHits
        _curFrameHits = new List<GameObject>();

        // Calculate the direction from the camera to the player
        Vector3 direction = Player.transform.position - transform.position + _playerPo ;

        // Perform the raycast from the camera towards the player's bounds or collider
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, raycastDistance);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.name == "leaf")
            {
                GameObject hitObject = hit.transform.gameObject;
                _curFrameHits.Add(hitObject);
            }
        }

        // Unhide
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject i in _lastFrameHits)
        {
            if (!_curFrameHits.Contains(i))
            {
                Material mat = i.GetComponent<Renderer>().material;
                mat.SetFloat("_Tweak_transparency", DefaultTrans);

                toRemove.Add(i);
            }
        }

        // Remove the elements outside of the foreach loop
        foreach (GameObject obj in toRemove)
        {
            _lastFrameHits.Remove(obj);
        }

        // Hide
        foreach (GameObject i in _curFrameHits)
        {
            Material mat = i.GetComponent<Renderer>().material;
            mat.SetFloat("_Tweak_transparency", CastTrans);
        }

        // Save curFrameHits
        _lastFrameHits = _curFrameHits;
    }
}
