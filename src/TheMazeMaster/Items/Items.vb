Friend Module Items
    Friend AllItems As New List(Of Item)
    Friend ITEM_MAZE_COLUMNS As New Dictionary(Of Integer, Integer)
    Friend ITEM_MAZE_ROWS As New Dictionary(Of Integer, Integer)
    Friend ITEM_ROOM_COLUMNS As New Dictionary(Of Integer, Integer)
    Friend ITEM_ROOM_ROWS As New Dictionary(Of Integer, Integer)
    Friend Sub Clear()
        AllItems.Clear()
    End Sub
    'TODO: move to item
    Friend Function ITEM_ROLL_ATTACK(I As Integer) As Integer
        Return AllItems(I).ItemType.RollAttack
    End Function
    'TODO: move to itemtype
    Friend Function CREATE_ROOM_ITEM(IT As ItemTypeIdentifier, MX As Integer, M_Y As Integer, X As Integer, Y As Integer) As Integer
        Dim I = AllItemTypes(IT).Create()
        ITEM_MAZE_COLUMNS(I) = MX
        ITEM_MAZE_ROWS(I) = M_Y
        ITEM_ROOM_COLUMNS(I) = X
        ITEM_ROOM_ROWS(I) = Y
        Return I
    End Function
    'TODO: move to item
    Friend Sub PLACE_ITEM(I As Integer)
        If ITEM_MAZE_COLUMNS.ContainsKey(I) Then
            Dim IT = AllItems(I).ItemType
            Dim MX = ITEM_MAZE_COLUMNS(I)
            Dim MY = ITEM_MAZE_ROWS(I)
            Dim X = ITEM_ROOM_COLUMNS(I)
            Dim Y = ITEM_ROOM_ROWS(I)
            Dim TI = IT.TileIndex
            Dim RM = GET_ROOM_MAP(MX, MY)
            MSET(RM, 2, X, Y, TI)
        End If
    End Sub
    Friend Function FIND_ITEM(MX As Integer, M_Y As Integer, X As Integer, Y As Integer) As Integer
        For Each entry In ITEM_MAZE_COLUMNS
            Dim II = entry.Key
            Dim CMX = entry.Value
            Dim CMY = ITEM_MAZE_ROWS(II)
            Dim CX = ITEM_ROOM_COLUMNS(II)
            Dim CY = ITEM_ROOM_ROWS(II)
            If MX = CMX AndAlso M_Y = CMY AndAlso X = CX AndAlso Y = CY Then
                Return II
            End If
        Next
        Return -1
    End Function
    'TODO: move to item
    Friend Sub REMOVE_ITEM(I As Integer)
        If ITEM_MAZE_COLUMNS.ContainsKey(I) Then
            Dim IT = AllItems(I).ItemType
            Dim MX = ITEM_MAZE_COLUMNS(I)
            Dim MY = ITEM_MAZE_ROWS(I)
            Dim X = ITEM_ROOM_COLUMNS(I)
            Dim Y = ITEM_ROOM_ROWS(I)
            Dim TI = TILE_EMPTY
            Dim RM = GET_ROOM_MAP(MX, MY)
            MSET(RM, 2, X, Y, TI)
        End If
    End Sub
    'TODO: move to item
    Friend Sub ITEM_CLEAR_ROOM(II As Integer)
        ITEM_MAZE_COLUMNS.Remove(II)
        ITEM_MAZE_ROWS.Remove(II)
        ITEM_ROOM_COLUMNS.Remove(II)
        ITEM_ROOM_ROWS.Remove(II)
    End Sub
    'TODO: move to item
    Friend Sub ITEM_DESTROY(II As Integer)
        AllItems(II).SetItemType(ItemTypeIdentifier.None)
    End Sub
End Module
