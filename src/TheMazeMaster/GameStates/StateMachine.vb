Friend Module StateMachine
    Friend CURRENT_STATE As StateIdentifier = StateIdentifier.Title
    Private ReadOnly table As IReadOnlyDictionary(Of StateIdentifier, Func(Of World, StateIdentifier)) =
        New Dictionary(Of StateIdentifier, Func(Of World, StateIdentifier)) From
        {
            {StateIdentifier.Title, AddressOf Title.Update},
            {StateIdentifier.Fight, AddressOf Fight.Update},
            {StateIdentifier.InPlay, AddressOf InPlay.Update},
            {StateIdentifier.PickUp, AddressOf PickUp.Update},
            {StateIdentifier.GameMenu, AddressOf GameMenu.Update},
            {StateIdentifier.Inventory, AddressOf Inventory.Update},
            {StateIdentifier.Status, AddressOf Status.Update},
            {StateIdentifier.Shoppe, AddressOf Shoppe.Update}
        }
    Friend Sub Update(world As World)
        CURRENT_STATE = table(CURRENT_STATE)(world)
    End Sub
End Module
