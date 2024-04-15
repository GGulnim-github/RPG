// ProjectSettings/TagManager.asset
public static class Tags
{
    public const string Respawn = "Respawn";
    public const string Finish = "Finish";
    public const string EditorOnly = "EditorOnly";
    public const string MainCamera = "MainCamera";
    public const string Player = "Player";
    public const string GameController = "GameController";
    public const string Monster = "Monster";
}

public static class Layers
{
    public const int Default = 0;
    public const int TransparentFX = 1;
    public const int IgnoreRaycast = 2;
    public const int Player = 3;
    public const int Water = 4;
    public const int UI = 5;


    public static string GetName(int _layer)
    {
        string _retval = string.Empty;
        switch (_layer)
        {
            case Layers.Default: _retval = "Default"; break;
            case Layers.TransparentFX: _retval = "TransparentFX"; break;
            case Layers.IgnoreRaycast: _retval = "IgnoreRaycast"; break;
            case Layers.Player: _retval = "Player"; break;
            case Layers.Water: _retval = "Water"; break;
            case Layers.UI: _retval = "UI"; break;
        }
        return _retval;
    }
}
