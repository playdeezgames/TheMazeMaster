Friend Module PickUp
    Friend PICKUP_ITEM_INDEX As Integer = -1
    Friend Function Update() As StateIdentifier
        Dim II = PICKUP_ITEM_INDEX
        Dim IT = Worlds.world.GetItem(II).ItemType
        If AnsiConsole.Confirm($"[olive]Pick up {IT.Name}?[/]", True) Then
            Worlds.world.GetItem(II).Remove()
            PLAYER_TAKE_ITEM(II)
        End If
        Return StateIdentifier.InPlay
    End Function
End Module
