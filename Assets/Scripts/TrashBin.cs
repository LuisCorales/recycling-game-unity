using UnityEngine;

public class TrashBin : MonoBehaviour
{
    [SerializeField] GameObject game;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = game.GetComponent<GameController>();
    }

    public void BinClicked()
    {
        if(!gameController.FirstTry)
            gameController.FirstTry = true;
        
        gameController.SpawnTrashItem(this.gameObject);
    }
}