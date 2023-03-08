Friend Module StateMachine
    Friend Const STATE_TITLE = "TITLE"
    Friend Const STATE_FIGHT = "FIGHT"
    Friend Const STATE_IN_PLAY = "INPLAY"
    Friend Const STATE_PICKUP = "PICKUP"
    Friend CURRENT_STATE As String = STATE_TITLE
    Private ReadOnly table As IReadOnlyDictionary(Of String, Func(Of String)) =
        New Dictionary(Of String, Func(Of String)) From
        {
            {STATE_TITLE, AddressOf Title.Update},
            {STATE_FIGHT, AddressOf Fight.Update},
            {STATE_IN_PLAY, AddressOf InPlay.Update},
            {STATE_PICKUP, AddressOf PickUp.Update}
        }
    Friend Sub Update()
        CURRENT_STATE = table(CURRENT_STATE)()
    End Sub
End Module
