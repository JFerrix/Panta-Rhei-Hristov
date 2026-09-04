using Content.Shared._Common.Consent;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Markings;
using Content.Shared.Verbs;

namespace Content.Shared._Floof.Humanoid.Genital;

//Done:
//Get Consent data for GenitalVisibility - DONE
//Show/Hide Genitals based on consent switch - DONE
//TODO:
//Add verb functionality - Skipped since piggybacking off the Undies System. Well, guess it was inevitable
//Get events for filling and emptying the suit and oversuit slot and show/hide markings depending on if filled or not
//Implement a way to hide/show specific and multiple markings - Skipped since piggyback on undies system

public sealed class GenitalSystem : EntitySystem
{
    [Dependency] private readonly SharedConsentSystem _consent = default!;
    [Dependency] private readonly SharedHumanoidAppearanceSystem _humanoidSystem = default!;
    [Dependency] private readonly MarkingManager _markingManager = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<GenitalComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<GenitalComponent, EntityConsentToggleUpdatedEvent>(OnConsentToggle);
    }
    //Todo Problem: System only works on the user, everyone elses genitals are still visible. Not great
    private void OnStartup(EntityUid uid, GenitalComponent component, ComponentStartup args)
    {
        _humanoidSystem.SetLayerVisibility(uid, HumanoidVisualLayers.Genital, _consent.HasConsent(uid, "GenitalVisibility"));
    }

    private void OnConsentToggle(EntityUid uid, GenitalComponent component, EntityConsentToggleUpdatedEvent args)
    {
        //WHY THE FUCK DOES IT REQUIRE A BOOL INVERT BUT THE ONE ABOVE DOESNT?
        _humanoidSystem.SetLayerVisibility(uid, HumanoidVisualLayers.Genital, !_consent.HasConsent(uid, "GenitalVisibility"));
    }

    private void SetMarkingVisibility(Entity<HumanoidAppearanceComponent> ent, string markingId, bool visible)
    {
        if (visible)
            ent.Comp.HiddenMarkings.Remove(markingId);
        else
            ent.Comp.HiddenMarkings.Add(markingId);
    }
}