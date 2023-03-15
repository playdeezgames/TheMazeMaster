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
            Dim MR = MOVE_PLAYER(d.Value)
            If MR = MoveResult.Fight Then
                FIGHT_CREATURE_INDEX = GET_PLAYER_ENEMY(d.Value)
                FIGHT_START()
                Return StateIdentifier.Fight
            ElseIf MR = MoveResult.PickUp Then
                PICKUP_ITEM_INDEX = GET_PLAYER_PICKUP(d.Value)
                Return StateIdentifier.PickUp
            End If
        End If
        If key = ConsoleKey.Escape Then
            Return StateIdentifier.GameMenu
        End If
        Return R
    End Function

    Private Sub DrawMap()
        Dim MX = AllCreatures(Player.character.CreatureIndex).MazeColumn
        Dim M_y = AllCreatures(Player.character.CreatureIndex).MazeRow
        Driver.DrawMap(GetRoomMap(MX, M_y))
    End Sub
End Module
