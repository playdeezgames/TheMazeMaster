Friend Module Items
    Friend ITEM_TYPES As New List(Of String)
    Friend ITEM_MAZE_COLUMNS As New Dictionary(Of Integer, Integer)
    Friend ITEM_MAZE_ROWS As New Dictionary(Of Integer, Integer)
    Friend ITEM_ROOM_COLUMNS As New Dictionary(Of Integer, Integer)
    Friend ITEM_ROOM_ROWS As New Dictionary(Of Integer, Integer)
    Function CREATE_ITEM(IT As String) As Integer
        'TODO: FIRST LOOK FOR EMPTY ITEM
        Dim I = ITEM_TYPES.Count
        ITEM_TYPES.Add(IT)
        Return I
    End Function
    Friend Function ITEM_ROLL_ATTACK(I As Integer) As Integer
        Dim IT = ITEM_TYPES(I)
        Return ITEMTYPE_ROLL_ATTACK(IT)
    End Function
    Friend Function CREATE_ROOM_ITEM(IT As String, MX As Integer, M_Y As Integer, X As Integer, Y As Integer) As Integer
        Dim I = CREATE_ITEM(IT)
        ITEM_MAZE_COLUMNS(I) = MX
        ITEM_MAZE_ROWS(I) = M_Y
        ITEM_ROOM_COLUMNS(I) = X
        ITEM_ROOM_ROWS(I) = Y
        Return I
    End Function
    Friend Sub PLACE_ITEM(I As Integer)
        If ITEM_MAZE_COLUMNS.ContainsKey(I) Then
            Dim IT = ITEM_TYPES(I)
            Dim MX = ITEM_MAZE_COLUMNS(I)
            Dim MY = ITEM_MAZE_ROWS(I)
            Dim X = ITEM_ROOM_COLUMNS(I)
            Dim Y = ITEM_ROOM_ROWS(I)
            Dim TI = ITEMTYPE_TILE_INDICES(IT)
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
    Friend Sub REMOVE_ITEM(I As Integer)
        If ITEM_MAZE_COLUMNS.ContainsKey(I) Then
            Dim IT = ITEM_TYPES(I)
            Dim MX = ITEM_MAZE_COLUMNS(I)
            Dim MY = ITEM_MAZE_ROWS(I)
            Dim X = ITEM_ROOM_COLUMNS(I)
            Dim Y = ITEM_ROOM_ROWS(I)
            Dim TI = TILE_EMPTY
            Dim RM = GET_ROOM_MAP(MX, MY)
            MSET(RM, 2, X, Y, TI)
        End If
    End Sub
    Friend Sub ITEM_CLEAR_ROOM(II As Integer)
        ITEM_MAZE_COLUMNS.Remove(II)
        ITEM_MAZE_ROWS.Remove(II)
        ITEM_ROOM_COLUMNS.Remove(II)
        ITEM_ROOM_ROWS.Remove(II)
    End Sub
    Friend Sub ITEM_DESTROY(II As Integer)
        ITEM_TYPES(II) = ITEMTYPE_NONE
    End Sub
End Module
