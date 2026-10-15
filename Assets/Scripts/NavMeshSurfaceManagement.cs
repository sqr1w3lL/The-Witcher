using NavMeshPlus.Components;
using System.Collections;
using UnityEngine;

public class NavMeshSurfaceManagement : MonoBehaviour
{
    public static NavMeshSurfaceManagement Instance { get; private set; }

    private NavMeshSurface _navMeshSurface;
    private float _rebakeDelay = 0.3f;
    private float _localSize = 20f;
    private Vector3 _fullSize, _fullCenter;
    private Vector3? _pendingPos;
    private bool _running;

    private void Awake()
    {
        Instance = this;
        _navMeshSurface = GetComponent<NavMeshSurface>();
        _navMeshSurface.hideEditorLogs = true;
        _fullSize = _navMeshSurface.size;
        _fullCenter = _navMeshSurface.center;
    }

    public void RebakeNavMeshSurface(Vector3 worldPos)
    {
        _pendingPos = worldPos;
        if (!_running) StartCoroutine(RebakeRoutine());
    }
    
    private IEnumerator RebakeRoutine()
    {
        _running = true;
        yield return new WaitForSeconds(_rebakeDelay);

        _navMeshSurface.center = transform.InverseTransformPoint(_pendingPos.Value);
        _navMeshSurface.size = Vector3.one * _localSize;
        _pendingPos = null;

        var op = _navMeshSurface.BuildNavMeshAsync();
        while (!op.isDone) yield return null;

        _navMeshSurface.center = _fullCenter;
        _navMeshSurface.size = _fullSize;
        _running = false;
    }
}
