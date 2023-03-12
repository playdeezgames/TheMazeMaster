Friend Module PickUp
    Friend PICKUP_ITEM_INDEX As Integer = -1
    Friend Function Update() As String
        Dim II = PICKUP_ITEM_INDEX
        Dim IT = AllItems(II).ItemType
        If AnsiConsole.Confirm($"[olive]Pick up {IT.Name}?[/]", False) Then
            REMOVE_ITEM(II)
            PLAYER_TAKE_ITEM(II)
        End If
        Return STATE_IN_PLAY
    End Function
End Module
