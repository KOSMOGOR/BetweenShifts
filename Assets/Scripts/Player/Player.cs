public class Player : SingletonMonoBehaviour<Player>
{
    public PlayerState playerState = PlayerState.Free;
}

public enum PlayerState
{
    Free,
    Interacting,
    Cutscene,
    Inventory
}
