using UnityEngine;

public class Trash : MonoBehaviour
{
    // RED, BLUE, GREEN, BLACK
    [SerializeField] string trashType;
    public string TrashType => trashType;
}
