using NavMeshPlus.Components;
using System.Collections;
using UnityEngine;

public class NavMeshSurfaceManagement : MonoBehaviour
{
    public static NavMeshSurfaceManagement Instance { get; private set; }

    private NavMeshSurface _navMeshSurface;
    private Coroutine _rebakeCoroutine;
    private float _rebakeDelay = 0.5f;

    private void Awake()
    {
        Instance = this;
        _navMeshSurface = GetComponent<NavMeshSurface>();
        _navMeshSurface.hideEditorLogs = true;
    }

    public void RebakeNavMeshSurface()
    {
        _rebakeCoroutine ??= StartCoroutine(RebakeRoutine());
    }
    
    private IEnumerator RebakeRoutine()
    {
        yield return new WaitForSeconds(_rebakeDelay);
        var op = _navMeshSurface.BuildNavMeshAsync();
        while (!op.isDone) yield return null;
        _rebakeCoroutine = null;
    }
}
