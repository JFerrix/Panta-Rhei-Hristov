namespace Content.Shared._DV.Silicon.IPC;

[RegisterComponent]
public sealed partial class SnoutHelmetComponent : Component
{
    [DataField]
    public bool EnableAlternateHelmet;

    //Floof Change: Removed Readonly and added default initialized value. Permits runtime changes (Remove default initializer when finished)
    [DataField]
    public string? ReplacementRace = "human";
}
