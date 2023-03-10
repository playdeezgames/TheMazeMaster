Friend Module Player
    Friend PLAYER_CREATURE_INDEX As Integer = 0
    Friend PLAYER_XP As Integer = 0
    Friend PLAYER_XP_GOAL As Integer = 10
    Friend PLAYER_INVENTORY As New List(Of Integer)
    Friend PLAYER_STACKS As New Dictionary(Of String, Integer)
    Friend Sub Generate()
        PLAYER_CREATURE_INDEX = GENERATE_CREATURE(CREATURETYPE_DUDE)
        PLAYER_XP = 0
        PLAYER_XP_GOAL = 10
        PLAYER_INVENTORY.Clear()
        PLAYER_STACKS.Clear()
    End Sub
    Friend Function MOVE_PLAYER(d As Integer) As String
        Dim I = PLAYER_CREATURE_INDEX
        Return MOVE_CREATURE(I, d)
    End Function
    Friend Function GET_PLAYER_ENEMY(D As Integer) As Integer
        Dim I = PLAYER_CREATURE_INDEX
        Dim X = CREATURE_ROOM_COLUMN(I)
        Dim Y = CREATURE_ROOM_ROW(I)
        Dim NX = STEP_X(D, X, Y)
        Dim NY = STEP_Y(D, X, Y)
        Dim MX = CREATURE_MAZE_COLUMN(I)
        Dim MY = CREATURE_MAZE_ROW(I)
        Return FIND_CREATURE(MX, MY, NX, NY)
    End Function
    Friend Sub PLAYER_ADD_XP(XP As Integer)
        PLAYER_XP = PLAYER_XP + XP
    End Sub
End Module
' DEF GET_PLAYER_PICKUP(D)
'     I=PLAYER_CREATURE_INDEX
'     X=CREATURE_ROOM_COLUMN(I)
'     Y=CREATURE_ROOM_ROW(I)
'     NX=STEP_X(D,X,Y)
'     NY=STEP_Y(D,X,Y)
'     MX=CREATURE_MAZE_COLUMN(I)
'     MY=CREATURE_MAZE_ROW(I)
'     RETURN FIND_ITEM(MX,MY,NX,NY)
' ENDDEF
' DEF HAS_PLAYER_LEVELED()
'     RETURN PLAYER_XP>=PLAYER_XP_GOAL
' ENDDEF
' DEF PLAYER_TAKE_ITEM(II)
'     ITEM_CLEAR_ROOM(II)
'     IT=ITEM_TYPES(II)
'     IF EXISTS(ITEMTYPE_STACKS,IT) THEN
'         IF NOT EXISTS(PLAYER_STACKS,IT) THEN
'             IC=1
'         ELSE
'             IC=PLAYER_STACKS(IT)+1
'         ENDIF
'         PLAYER_STACKS(IT)=IC
'         ITEM_DESTROY(II)
'     ELSE
'         IF NOT EXISTS(PLAYER_INVENTORY,II) THEN
'             PUSH(PLAYER_INVENTORY,II)
'         ENDIF
'     ENDIF
' ENDDEF