﻿Friend Module Player
    Friend character As Character
    Friend Sub GeneratePlayer(maze As Maze)
        character = New Character(AllCreatureTypes(CreatureTypeIdentifier.Dude).Generate(maze))
    End Sub
    Friend Function GET_PLAYER_ENEMY(D As DirectionIdentifier) As Integer
        Dim creature = character.Creature
        Dim X = creature.RoomColumn
        Dim Y = creature.RoomRow
        Dim NX = D.StepX(X)
        Dim NY = D.StepY(Y)
        Dim MX = creature.MazeColumn
        Dim MY = creature.MazeRow
        Return Worlds.world.GetRoom(MX, MY).Map.GetCell(NX, NY).Creature.Value
    End Function
    Friend Sub PLAYER_ADD_XP(XP As Integer)
        character.XP += XP
    End Sub
    Friend Function GET_PLAYER_PICKUP(D As DirectionIdentifier) As Integer
        Dim I = character.CreatureIndex
        Dim X = Worlds.world.GetCreature(I).RoomColumn
        Dim Y = Worlds.world.GetCreature(I).RoomRow
        Dim NX = D.StepX(X)
        Dim NY = D.StepY(Y)
        Dim MX = Worlds.world.GetCreature(I).MazeColumn
        Dim MY = Worlds.world.GetCreature(I).MazeRow
        Return Worlds.world.GetRoom(MX, MY).Map.GetCell(NX, NY).Item.Value
    End Function
    'TODO: push down into character
    Friend Sub PLAYER_TAKE_ITEM(II As Integer)
        Worlds.world.GetItem(II).ClearRoom()
        Dim IT = Worlds.world.GetItem(II).ItemType
        Dim ic As Integer
        If If(IT?.Stacks, False) Then
            If Not character.ItemStacks.ContainsKey(IT.Identifier) Then
                ic = 1
            Else
                ic = character.ItemStacks(IT.Identifier) + 1
            End If
            character.ItemStacks(IT.Identifier) = ic
            Worlds.world.GetItem(II).Destroy()
        Else
            If Not character.Inventory.Contains(II) Then
                character.Inventory.Add(II)
            End If
        End If
    End Sub
End Module
