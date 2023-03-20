Friend Module PickUp
    Friend PICKUP_ITEM_INDEX As Integer = -1
    Friend Function Update(world As World) As StateIdentifier
        Dim II = PICKUP_ITEM_INDEX
        Dim IT = world.GetItem(II).ItemType
        If AnsiConsole.Confirm($"[olive]Pick up {IT.Name}?[/]", True) Then
            world.GetItem(II).Remove(world)
            world.character.TakeItem(world, II)
        End If
        Return StateIdentifier.InPlay
    End Function
End Module
