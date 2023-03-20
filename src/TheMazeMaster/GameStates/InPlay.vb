Friend Module InPlay
    Friend Function Update(world As World) As StateIdentifier
        AnsiConsole.Clear()
        DrawMap(world)
        AnsiConsole.MarkupLine("[olive]▲▼►◄[/] - Navigate | [olive]Esc[/] - Menu")
        Return MovePlayer(world)
    End Function

    Private Function MovePlayer(world As World) As StateIdentifier
        Dim key = Console.ReadKey(True).Key
        Dim d As DirectionIdentifier? =
            If(key = ConsoleKey.UpArrow, DirectionIdentifier.North,
            If(key = ConsoleKey.DownArrow, DirectionIdentifier.South,
            If(key = ConsoleKey.LeftArrow, DirectionIdentifier.West,
            If(key = ConsoleKey.RightArrow, DirectionIdentifier.East,
            Nothing))))
        Dim R = StateIdentifier.InPlay
        If d.HasValue Then
            Dim MR = world.character.Move(world, d.Value)
            If MR = MoveResult.Shoppe Then
                Shoppe.ShoppeTypeIdentifier = world.character.GetShopType(world, d.Value)
                Return StateIdentifier.Shoppe
            ElseIf MR = MoveResult.Fight Then
                FIGHT_CREATURE_INDEX = world.character.GetEnemy(world, d.Value)
                FIGHT_START(world)
                Return StateIdentifier.Fight
            ElseIf MR = MoveResult.PickUp Then
                PICKUP_ITEM_INDEX = world.character.GetPickUp(world, d.Value)
                Return StateIdentifier.PickUp
            End If
        End If
        If key = ConsoleKey.Escape Then
            Return StateIdentifier.GameMenu
        End If
        Return R
    End Function

    Private Sub DrawMap(world As World)
        Dim MX = world.GetCreature(world.character.CreatureIndex).MazeColumn
        Dim M_y = world.GetCreature(world.character.CreatureIndex).MazeRow
        Driver.DrawMap(world, world.GetRoom(MX, M_y).Map)
    End Sub
End Module
