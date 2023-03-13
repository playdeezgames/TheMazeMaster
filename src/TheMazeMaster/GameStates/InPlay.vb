﻿Friend Module InPlay
    Friend Function Update() As StateIdentifier
        AnsiConsole.Clear()
        DrawMap()
        AnsiConsole.MarkupLine("▲▼►◄ - Navigate")
        Return MovePlayer()
    End Function

    Private Function MovePlayer() As StateIdentifier
        Dim key = Console.ReadKey(True).Key
        Dim d =
            If(key = ConsoleKey.UpArrow, DIRECTION_NORTH,
            If(key = ConsoleKey.DownArrow, DIRECTION_SOUTH,
            If(key = ConsoleKey.LeftArrow, DIRECTION_WEST,
            If(key = ConsoleKey.RightArrow, DIRECTION_EAST,
            DIRECTION_COUNT))))
        Dim R = StateIdentifier.InPlay
        If d < DIRECTION_COUNT Then
            Dim MR = MOVE_PLAYER(d)
            If MR = MoveResult.Fight Then
                FIGHT_CREATURE_INDEX = GET_PLAYER_ENEMY(d)
                FIGHT_START()
                Return StateIdentifier.Fight
            ElseIf MR = MoveResult.PickUp Then
                PICKUP_ITEM_INDEX = GET_PLAYER_PICKUP(d)
                Return StateIdentifier.PickUp
            End If
        End If
        If key = ConsoleKey.Escape Then

        End If
        Return R
    End Function

    Private Sub DrawMap()
        Dim MX = AllCreatures(PLAYER_CREATURE_INDEX).MazeColumn
        Dim M_y = AllCreatures(PLAYER_CREATURE_INDEX).MazeRow
        Dim ROOM_MAP = GET_ROOM_MAP(MX, M_y)
        MAP(ROOM_MAP, 0, 0)
    End Sub
End Module
