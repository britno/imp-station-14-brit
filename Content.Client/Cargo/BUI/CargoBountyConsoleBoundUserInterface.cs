using Content.Client.Cargo.UI;
using Content.Shared.Cargo.Components;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client.Cargo.BUI;

[UsedImplicitly]
public sealed class CargoBountyConsoleBoundUserInterface : BoundUserInterface
{
    [ViewVariables]
    private CargoBountyMenu? _menu;

    public CargoBountyConsoleBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        _menu = this.CreateWindow<CargoBountyMenu>();

        _menu.OnLabelButtonPressed += id =>
        {
            SendMessage(new BountyPrintLabelMessage(id));
        };

        _menu.OnSkipButtonPressed += id =>
        {
            SendMessage(new BountySkipMessage(id));
        };

        //imp edit start - bounty claiming & statuses
        _menu.OnClaimButtonPressed += id =>
        {
            SendMessage(new BountyClaimedMessage(id));
        };

        _menu.OnStatusOptionSelected += (id, status) =>
        {
            SendMessage(new BountySetStatusMessage(id, status));
        };
        //imp edit end
    }

    protected override void UpdateState(BoundUserInterfaceState message)
    {
        base.UpdateState(message);

        if (message is not CargoBountyConsoleState state)
            return;

        _menu?.UpdateEntries(state.Bounties, state.History, state.UntilNextSkip);
    }
}
