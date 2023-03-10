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
End Module
' DEF REMOVE_ITEM(I)
'     IF EXISTS(ITEM_MAZE_COLUMNS,I) THEN
'         IT = ITEM_TYPES(I)
'         MX=ITEM_MAZE_COLUMNS(I)
'         MY=ITEM_MAZE_ROWS(I)
'         X=ITEM_ROOM_COLUMNS(I)
'         Y=ITEM_ROOM_ROWS(I)
'         TI=TILE_EMPTY
'         RM=GET_ROOM_MAP(MX,MY)
'         MSET RM,2,X,Y,TI
'     ENDIF
' ENDDEF
' DEF FIND_ITEM(MX,MY,X,Y)
'     I=ITERATOR(ITEM_MAZE_COLUMNS)
'     WHILE MOVE_NEXT(I)
'         II=GET(I)
'         CMX=VAL(I)
'         CMY=ITEM_MAZE_ROWS(II)
'         CX=ITEM_ROOM_COLUMNS(II)
'         CY=ITEM_ROOM_ROWS(II)
'         IF MX=CMX AND MY=CMY AND X=CX AND Y=CY THEN
'             RETURN II
'         ENDIF
'     WEND
'     RETURN -1
' ENDDEF
' DEF ITEM_CLEAR_ROOM(II)
'     REMOVE(ITEM_MAZE_COLUMNS,II)
'     REMOVE(ITEM_MAZE_ROWS,II)
'     REMOVE(ITEM_ROOM_COLUMNS,II)
'     REMOVE(ITEM_ROOM_ROWS,II)
' ENDDEF
' DEF ITEM_DESTROY(II)
'     ITEM_TYPES(II)=ITEMTYPE_NONE
' ENDDEF