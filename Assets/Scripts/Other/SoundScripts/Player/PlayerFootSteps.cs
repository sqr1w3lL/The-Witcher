using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFootSteps : MonoBehaviour
{
    [System.Serializable]
    public class Surface
    {
        public string name;
        public Tilemap tilemap;     
        public TileBase tile;        
        public AudioClip footstepClip;
    }

    [SerializeField] private Surface[] surfaces;
    [SerializeField] private AudioSource audioSource;
    private Vector2 _feetOffset = new Vector2(0f, -0.5f);
    private Vector3 _lastPos;
    private float _minSpeed = 0.05f;
    private float _stopDelay = 0.1f;
    private float _timeSinceMoved;

    private void Start()
    {
        _lastPos = transform.position;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        float speed = Time.deltaTime > 0f ? (transform.position - _lastPos).magnitude / Time.deltaTime : 0f;
        _lastPos = transform.position;

        if (speed > _minSpeed) _timeSinceMoved = 0f;
        else _timeSinceMoved += Time.deltaTime;

        bool isMoving = _timeSinceMoved < _stopDelay;
        Surface current = isMoving ? GetSurfaceUnderFeet() : null;

        if (current == null)
        {
            if (audioSource.isPlaying) audioSource.Stop();
            return;
        }

        if (audioSource.clip != current.footstepClip)
        {
            audioSource.clip = current.footstepClip;
            audioSource.Play();
        }
        else if (!audioSource.isPlaying)
            audioSource.Play();
    }

    Surface GetSurfaceUnderFeet()
    {
        Vector3 feetPos = transform.position + (Vector3)_feetOffset;

        foreach (var s in surfaces)
        {
            Vector3Int cell = s.tilemap.WorldToCell(feetPos);
            if (s.tilemap.GetTile(cell) == s.tile)
                return s;
        }
        return null;
    }
}