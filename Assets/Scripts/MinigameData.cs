using UnityEngine;

[CreateAssetMenu(fileName = "MinigameData", menuName = "ScriptableObjects/MinigameData", order = 1)]
public class MinigameData : ScriptableObject
{
    public GameObject minigamePrefab;
    public float countdownTime;
    public string minigamePrompt;
}
