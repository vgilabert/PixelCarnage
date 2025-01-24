using Extensions;
using StatSystem;
using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager>
{
    [SerializeField] private Player.Player playerReference;
    public Player.Player PlayerReference => playerReference;

    public Vector3 PlayerPosition => playerReference != null ? playerReference.transform.position : Vector3.zero;
}