Friend Module Player
    Friend character As Character
    Friend Sub Generate()
        character = New Character(AllCreatureTypes(CreatureTypeIdentifier.Dude).Generate)
    End Sub
    Friend Function MOVE_PLAYER(d As Integer) As MoveResult
        Return character.Creature.Move(d)
    End Function
    Friend Function GET_PLAYER_ENEMY(D As Integer) As Integer
        Dim I = character.CreatureIndex
        Dim X = AllCreatures(I).RoomColumn
        Dim Y = AllCreatures(I).RoomRow
        Dim NX = STEP_X(D, X, Y)
        Dim NY = STEP_Y(D, X, Y)
        Dim MX = AllCreatures(I).MazeColumn
        Dim MY = AllCreatures(I).MazeRow
        Return FIND_CREATURE(MX, MY, NX, NY)
    End Function
    Friend Sub PLAYER_ADD_XP(XP As Integer)
        character.XP += XP
    End Sub
    Friend Function GET_PLAYER_PICKUP(D As Integer) As Integer
        Dim I = character.CreatureIndex
        Dim X = AllCreatures(I).RoomColumn
        Dim Y = AllCreatures(I).RoomRow
        Dim NX = STEP_X(D, X, Y)
        Dim NY = STEP_Y(D, X, Y)
        Dim MX = AllCreatures(I).MazeColumn
        Dim MY = AllCreatures(I).MazeRow
        Return FIND_ITEM(MX, MY, NX, NY)
    End Function
    'TODO: push down into character
    Friend Sub PLAYER_TAKE_ITEM(II As Integer)
        AllItems(II).ClearRoom()
        Dim IT = AllItems(II).ItemType
        Dim ic As Integer
        If If(IT?.Stacks, False) Then
            If Not character.ItemStacks.ContainsKey(IT.Identifier) Then
                ic = 1
            Else
                ic = character.ItemStacks(IT.Identifier) + 1
            End If
            character.ItemStacks(IT.Identifier) = ic
            AllItems(II).Destroy()
        Else
            If Not character.Inventory.Contains(II) Then
                character.Inventory.Add(II)
            End If
        End If
    End Sub
End Module
