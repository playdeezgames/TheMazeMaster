﻿Friend Module Player
    Friend PLAYER_CREATURE_INDEX As Integer = 0
    Friend PLAYER_XP As Integer = 0
    Friend PLAYER_XP_GOAL As Integer = 10
    Friend PLAYER_INVENTORY As New List(Of Integer)
    Friend PLAYER_STACKS As New Dictionary(Of ItemTypeIdentifier, Integer)
    Friend Sub Generate()
        PLAYER_CREATURE_INDEX = AllCreatureTypes(CreatureTypeIdentifier.Dude).Generate
        PLAYER_XP = 0
        PLAYER_XP_GOAL = 10
        PLAYER_INVENTORY.Clear()
        PLAYER_STACKS.Clear()
    End Sub
    Friend Function MOVE_PLAYER(d As Integer) As MoveResult
        Return AllCreatures(PLAYER_CREATURE_INDEX).Move(d)
    End Function
    Friend Function GET_PLAYER_ENEMY(D As Integer) As Integer
        Dim I = PLAYER_CREATURE_INDEX
        Dim X = AllCreatures(I).RoomColumn
        Dim Y = AllCreatures(I).RoomRow
        Dim NX = STEP_X(D, X, Y)
        Dim NY = STEP_Y(D, X, Y)
        Dim MX = AllCreatures(I).MazeColumn
        Dim MY = AllCreatures(I).MazeRow
        Return FIND_CREATURE(MX, MY, NX, NY)
    End Function
    Friend Sub PLAYER_ADD_XP(XP As Integer)
        PLAYER_XP += XP
    End Sub
    Friend Function GET_PLAYER_PICKUP(D As Integer) As Integer
        Dim I = PLAYER_CREATURE_INDEX
        Dim X = AllCreatures(I).RoomColumn
        Dim Y = AllCreatures(I).RoomRow
        Dim NX = STEP_X(D, X, Y)
        Dim NY = STEP_Y(D, X, Y)
        Dim MX = AllCreatures(I).MazeColumn
        Dim MY = AllCreatures(I).MazeRow
        Return FIND_ITEM(MX, MY, NX, NY)
    End Function
    Friend Sub PLAYER_TAKE_ITEM(II As Integer)
        AllItems(II).ClearRoom()
        Dim IT = AllItems(II).ItemType
        Dim ic As Integer
        If If(IT?.Stacks, False) Then
            If Not PLAYER_STACKS.ContainsKey(IT.Identifier) Then
                ic = 1
            Else
                ic = PLAYER_STACKS(IT.Identifier) + 1
            End If
            PLAYER_STACKS(IT.Identifier) = ic
            AllItems(II).Destroy()
        Else
            If Not PLAYER_INVENTORY.Contains(II) Then
                PLAYER_INVENTORY.Add(II)
            End If
        End If
    End Sub
End Module
' DEF HAS_PLAYER_LEVELED()
'     RETURN PLAYER_XP>=PLAYER_XP_GOAL
' ENDDEF
