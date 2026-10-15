using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Npc Talk State")]
public class NpcTalkStateSO : ScriptableObject
{
    [NonSerialized] private HashSet<string> _disabled = new();

    private void OnEnable()
    {
        hideFlags |= HideFlags.DontUnloadUnusedAsset;
        _disabled ??= new HashSet<string>();
    }

    public bool IsDisabled(string id) => _disabled.Contains(id);
    public void Disable(string id) => _disabled.Add(id);
    public void Enable(string id) => _disabled.Remove(id);
    public void ResetAll() => _disabled.Clear();
}