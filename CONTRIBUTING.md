# Panta Rhei Contributing Guidelines

###### _(Note that this has been largely borrowed from Delta-V. In the future, we will replace most of these examples with examples based on our native developments. For now, this is a placeholder. - M3739)_

Generally we follow [Wizden's PR guidelines](https://docs.spacestation14.com/en/general-development/codebase-info/pull-request-guidelines.html) for code quality and such.

Importantly do not make webedits, copied verbatim from above:
> Do not use GitHub's web editor to create PRs. PRs submitted through the web editor may be closed without review.

Upstream is the [DeltaV-Station/Delta-v](https://github.com/DeltaV-Station/Delta-v) repository that Delta-V runs on.

# Content specific to Panta Rhei

In general anything you create from scratch (not modifying something that exists from upstream) should go in the Floof Station subfolder, `_Floof`.

###### _(Remind me to redo this section to feature our native developments as examples. - M3739)_

Examples:
- `Content.Server/_DV/Chapel/SacrificialAltarSystem.cs`
- `Resources/Prototypes/_DV/ai_factions.yml`
- `Resources/Audio/_DV/Items/gavel.ogg`
- `Resources/Textures/_DV/Icons/cri.rsi`
- `Resources/Locale/en-US/_DV/shipyard/shipyard-console.ftl`
- `Resources/ServerInfo/Guidebook/_DV/AlertProcedure.xml`
  Note that guidebooks go in `ServerInfo/Guidebook/_Floof` and not `ServerInfo/_Floof`!

# Changes to upstream files

If you make a change to an upstream C# or YAML file **you must add comments on or around the changed lines**.
The comments should clarify what changed, to make conflict resolution simpler when a file is changed upstream.

For YAML specifically, if you add a new component to a prototype add the comment to the `type: ...` line.
If you just modify some fields of a component, comment the fields instead.

For C# files, if you are adding a lot of code try to put it in a partial class when it makes sense.

The exception to this is early merging commits that are going to be cherry picked in the future regardless, there's no harm in leaving them as-is.

As an aside, fluent (.ftl) files **do not support comments on the same line** as a locale value, so be careful when changing them.

## Examples of comments in upstream files

###### _(Remind me to redo this section to feature our native developments as examples. - M3739)_

A single line comment on a changed yml field:
```yml
- type: entity
  parent: BasePDA
  id: SciencePDA
  name: epistemics PDA # Panta Rhei - Epistemics Department replacing Science
```

A pair of comments enclosing a list of added items to starting gear:
```yml
  storage:
    back:
    - EmergencyRollerBedSpawnFolded
    # Begin Panta Rhei additions
    - BodyBagFolded
    - Portafib
    # End Panta Rhei additions
```

A comment on a new imported namespace:
```cs
using Content.Server.Psionics.Glimmer; // Panta Rhei
```

A pair of comments enclosing a block of added code:
```cs
private EntityUid Slice(...)
{
    ...

    _transform.SetLocalRotation(sliceUid, 0);

    // Panta Rhei - start of deep frier stuff
    var slicedEv = new FoodSlicedEvent(user, uid, sliceUid);
    RaiseLocalEvent(uid, ref slicedEv);
    // Panta Rhei - end of deep frier stuff

    ...
}
```

# Mapping

If you want to make changes to a map, get in touch with its maintainer to make sure you don't both make changes at the same time.

Conflicts with maps make PRs mutually exclusive so either your work or the maintainer's work will be lost, communicate to avoid this!

Please make a detailed list of **all** changes(even minor changes) with locations when submitting a PR. This helps reviewers hone in on them without having to search an entire map for differences. Ex: [Map Edits](https://github.com/DeltaV-Station/Delta-v/pull/3165)


**Submitting a map PR**

Please limit changelogs on map PRs to **significant** map alterations or additions. Minor map edits do not need changelogs.
Format for map PRs looks like:
```
:cl: Yourname
MAPS: Mapname
- add: Added fun!
- remove: Removed fun!
- tweak: Changed fun!
- fix: Fixed fun!
``` 

# Before you submit

Double-check your diff on GitHub before submitting: look for unintended commits or changes and remove accidental whitespace or line-ending changes.

Additionally for long-lasting PRs, if you see `RobustToolbox` in the changed files you have to revert it, use `git checkout upstream/master RobustToolbox` (replacing `upstream` with the name of your Floof-Station/Panta-Rhei remote)

# Changelogs
###### _(Update this section again once we finish the changelog stuff. - M3739)_

By default any changelogs goes in the Panta Rhei changelog, you can use the Panta Rhei admin changelog by putting `DELTAVADMIN:` in a line after `:cl:`.

Do not use `ADMIN:` as **it will mangle** the upstream admin changelog!

# Additional resources

If you are new to contributing to SS14 in general, have a look at the [SS14 docs](https://docs.spacestation14.io/) or ask for help in `#Development` on [Discord](https://discord.gg/pmxfNx8gRS)!

## AI-Generated Content
Code, sprites and any other AI-generated content is not allowed to be submitted to the repository.

Trying to PR AI-generated content may result in you being banned from contributing.
