using Content.Shared._Common.Consent;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Markings;
using Content.Shared.Verbs;
using Content.Shared.GameTicking;

namespace Content.Shared._Floof.Humanoid.Genital;

//Done:
//Get Consent data for GenitalVisibility - DONE
//Show/Hide Genitals based on consent switch - DONE
//Skipped:
//Add verb functionality - Skipped since piggybacking off the Undies System. Well, guess it was inevitable
//Implement a way to hide/show specific and multiple markings - Skipped since piggyback on undies system
//TODO:
//Get events for filling and emptying the suit and oversuit slot and show/hide markings depending on if filled or not
//Only works on own character. Find a way to get every object with that component and run the consent toggle one on every entity with this component
//Check if SetLayerVisibility is clientside or serverside

//Problems:
//Respawning forces the visibility to off but doesnt update consent system, requiring manually resetting the switch

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
    //Main methods
    private void OnStartup(EntityUid uid, GenitalComponent component, ComponentStartup args)
    {
        component.ConsentState = false;
        UpdateAll(component);
    }

    private void OnConsentToggle(EntityUid uid, GenitalComponent component, EntityConsentToggleUpdatedEvent args)
    {
        component.ConsentState = !_consent.HasConsent(uid, "GenitalVisibility");
        UpdateAll(component);
    }
    

    //Secondary Methods
    private void UpdateAll(GenitalComponent MainComponent)
    {
        var query = AllEntityQuery<GenitalComponent>();
        while (query.MoveNext(out var uid, out var component))
        {
            _humanoidSystem.SetLayerVisibility(uid, HumanoidVisualLayers.Genital, MainComponent.ConsentState);
        }
    }

    private void SetMarkingVisibility(Entity<HumanoidAppearanceComponent> ent, string markingId, bool visible)
    {
        if (visible)
            ent.Comp.HiddenMarkings.Remove(markingId);
        else
            ent.Comp.HiddenMarkings.Add(markingId);
    }
}