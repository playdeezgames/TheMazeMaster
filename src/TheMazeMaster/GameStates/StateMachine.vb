Friend Module StateMachine
    Friend CURRENT_STATE As StateIdentifier = StateIdentifier.Title
    Private ReadOnly table As IReadOnlyDictionary(Of StateIdentifier, Func(Of StateIdentifier)) =
        New Dictionary(Of StateIdentifier, Func(Of StateIdentifier)) From
        {
            {StateIdentifier.Title, AddressOf Title.Update},
            {StateIdentifier.Fight, AddressOf Fight.Update},
            {StateIdentifier.InPlay, AddressOf InPlay.Update},
            {StateIdentifier.PickUp, AddressOf PickUp.Update}
        }
    Friend Sub Update()
        CURRENT_STATE = table(CURRENT_STATE)()
    End Sub
End Module
