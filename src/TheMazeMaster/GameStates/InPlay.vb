Friend Module InPlay
    Friend Function Update() As StateIdentifier
        AnsiConsole.Clear()
        DrawMap()
        AnsiConsole.MarkupLine("[olive]▲▼►◄[/] - Navigate | [olive]Esc[/] - Menu")
        Return MovePlayer()
    End Function

    Private Function MovePlayer() As StateIdentifier
        Dim key = Console.ReadKey(True).Key
        Dim d As DirectionIdentifier? =
            If(key = ConsoleKey.UpArrow, DirectionIdentifier.North,
            If(key = ConsoleKey.DownArrow, DirectionIdentifier.South,
            If(key = ConsoleKey.LeftArrow, DirectionIdentifier.West,
            If(key = ConsoleKey.RightArrow, DirectionIdentifier.East,
            Nothing))))
        Dim R = StateIdentifier.InPlay
        If d.HasValue Then
            Dim MR = Worlds.world.character.Move(d.Value)
            If MR = MoveResult.Fight Then
                FIGHT_CREATURE_INDEX = Worlds.world.character.GetEnemy(d.Value)
                FIGHT_START()
                Return StateIdentifier.Fight
            ElseIf MR = MoveResult.PickUp Then
                PICKUP_ITEM_INDEX = Worlds.world.character.GetPickUp(d.Value)
                Return StateIdentifier.PickUp
            End If
        End If
        If key = ConsoleKey.Escape Then
            Return StateIdentifier.GameMenu
        End If
        Return R
    End Function

    Private Sub DrawMap()
        Dim MX = Worlds.world.GetCreature(Worlds.world.character.CreatureIndex).MazeColumn
        Dim M_y = Worlds.world.GetCreature(Worlds.world.character.CreatureIndex).MazeRow
        Driver.DrawMap(Worlds.world.GetRoom(MX, M_y).Map)
    End Sub
End Module
