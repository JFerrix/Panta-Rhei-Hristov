//Things this component should do:
//Get all markings in the genital category
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Markings;

namespace Content.Shared._Floof.Humanoid.Genital;

[RegisterComponent]
public sealed partial class GenitalComponent : Component
{
    /// <summary>
    ///     A list for the Genital Markings.
    /// </summary>
    [DataField] //Remove this later. Also it only shows genital, not the markings themselves
    public List<Marking> GenitalMarkings = new();

    //Find all owners genital markings
}