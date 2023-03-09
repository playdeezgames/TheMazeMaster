Friend Module Creatures
    Friend CREATURE_ALIVE As New List(Of Boolean)
    Friend CREATURE_MAZE_COLUMN As New List(Of Integer)
    Friend CREATURE_MAZE_ROW As New List(Of Integer)
    Friend CREATURE_ROOM_COLUMN As New List(Of Integer)
    Friend CREATURE_ROOM_ROW As New List(Of Integer)
    Friend CREATURE_TYPE As New List(Of String)
    Friend CREATURE_HITPOINTS As New List(Of Integer)
    Friend CREATURE_WOUNDS As New List(Of Integer)
    Friend CREATURE_WEAPONS As New Dictionary(Of Integer, Integer)
    Friend Sub Generate()
        CLEAR_CREATURES()
        For CTI = 0 To ALL_CREATURETYPES.Count - 1
            Dim CT = ALL_CREATURETYPES(CTI)
            Dim SC = CREATURETYPE_SPAWN_COUNTS(CT)
            While SC > 0
                GENERATE_CREATURE(CT)
                SC = SC - 1
            End While
        Next
    End Sub

    Friend Function GENERATE_CREATURE(cT As String) As Integer
        Dim L = CREATURETYPE_MINIMUM_EXIT_COUNTS(cT)
        Dim H = CREATURETYPE_MAXIMUM_EXIT_COUNTS(cT)
        Dim LX = CREATURETYPE_MINIMUM_X(cT)
        Dim LY = CREATURETYPE_MINIMUM_Y(cT)
        Dim HX = CREATURETYPE_MAXIMUM_X(cT)
        Dim HY = CREATURETYPE_MAXIMUM_Y(cT)
        Dim e As Integer
        Dim x As Integer
        Dim y As Integer
        Dim mx As Integer
        Dim m_y As Integer
        Do
            mx = Rnd(0, MAZE_COLUMNS - 1)
            m_y = Rnd(0, MAZE_ROWS - 1)
            e = GET_MAZE_CELL_EXITS(mx, m_y)
            Dim ti As Integer
            Dim cti As Integer
            Do
                x = Rnd(0, ROOM_COLUMNS - 1)
                y = Rnd(0, ROOM_ROWS - 1)
                Dim RM = GET_ROOM_MAP(mx, m_y)
                ti = MGET(RM, 1, x, y)
                cti = MGET(RM, 2, x, y)
            Loop Until ti = TILE_FLOOR And cti = TILE_EMPTY And x >= LX And x <= HX And y >= LY And y <= HY
        Loop Until e >= L And e <= H
        Return CREATE_CREATURE(cT, mx, m_y, x, y)
    End Function

    Private Function CREATE_CREATURE(cT As String, mX As Integer, m_y As Integer, x As Integer, y As Integer) As Integer
        'TODO: REUSE DEAD CREATURES WHEN POSSIBLE
        Dim I = CREATURE_ALIVE.Count
        CREATURE_ALIVE.Add(True)
        CREATURE_TYPE.Add(cT)
        CREATURE_MAZE_COLUMN.Add(mX)
        CREATURE_MAZE_ROW.Add(m_y)
        CREATURE_ROOM_COLUMN.Add(x)
        CREATURE_ROOM_ROW.Add(y)
        CREATURE_HITPOINTS.Add(CREATURETYPE_HITPOINTS(cT))
        CREATURE_WOUNDS.Add(0)
        Dim WT = CREATURETYPE_DEFAULT_WEAPONTYPES(cT)
        CREATURE_WEAPONS(I) = CREATE_ITEM(WT)
        PLACE_CREATURE(I)
        Return I
    End Function

    Private Sub PLACE_CREATURE(i As Integer)
        Dim MX = CREATURE_MAZE_COLUMN(i)
        Dim My = CREATURE_MAZE_ROW(i)
        Dim RM = GET_ROOM_MAP(MX, My)
        Dim TI = CREATURETYPE_TILE_INDICES(CREATURE_TYPE(i))
        MSET(RM, 2, CREATURE_ROOM_COLUMN(i), CREATURE_ROOM_ROW(i), TI)
    End Sub

    Private Sub REMOVE_CREATURE(i As Integer)
        Dim MX = CREATURE_MAZE_COLUMN(i)
        Dim My = CREATURE_MAZE_ROW(i)
        Dim RM = GET_ROOM_MAP(MX, My)
        Dim TI = TILE_EMPTY
        MSET(RM, 2, CREATURE_ROOM_COLUMN(i), CREATURE_ROOM_ROW(i), TI)
    End Sub

    Private Sub CLEAR_CREATURES()
        CREATURE_ALIVE.Clear()
        CREATURE_MAZE_COLUMN.Clear()
        CREATURE_MAZE_ROW.Clear()
        CREATURE_ROOM_COLUMN.Clear()
        CREATURE_ROOM_ROW.Clear()
        CREATURE_TYPE.Clear()
        CREATURE_HITPOINTS.Clear()
        CREATURE_WOUNDS.Clear()
        CREATURE_WEAPONS.Clear()
    End Sub
    Friend Const MOVE_SUCCESS = "SUCCESS"
    Friend Const MOVE_FIGHT = "FIGHT"
    Friend Const MOVE_PICKUP = "PICKUP"
    Friend Const MOVE_BLOCKED = "BLOCKED"
    Function MOVE_CREATURE(i As Integer, d As Integer) As String
        Dim R = MOVE_BLOCKED
        If CREATURE_ALIVE(i) Then
            REMOVE_CREATURE(i)
            Dim X = CREATURE_ROOM_COLUMN(i)
            Dim Y = CREATURE_ROOM_ROW(i)
            Dim NX = STEP_X(d, X, Y)
            Dim NY = STEP_Y(d, X, Y)
            Dim MX = CREATURE_MAZE_COLUMN(i)
            Dim M_Y = CREATURE_MAZE_ROW(i)
            If NX < 0 OrElse NY < 0 OrElse NX >= ROOM_COLUMNS OrElse NY >= ROOM_ROWS Then
                CREATURE_MAZE_COLUMN(i) = STEP_X(d, MX, M_Y)
                CREATURE_MAZE_ROW(i) = STEP_Y(d, MX, M_Y)
                If NX < 0 Then
                    CREATURE_ROOM_COLUMN(i) = NX + ROOM_COLUMNS
                ElseIf NX >= ROOM_COLUMNS Then
                    CREATURE_ROOM_COLUMN(i) = NX - ROOM_COLUMNS
                Else
                    CREATURE_ROOM_COLUMN(i) = NX
                End If
                If NY < 0 Then
                    CREATURE_ROOM_ROW(i) = NY + ROOM_ROWS
                ElseIf NY >= ROOM_ROWS Then
                    CREATURE_ROOM_ROW(i) = NY - ROOM_ROWS
                Else
                    CREATURE_ROOM_ROW(i) = NY
                End If
            Else
                Dim TL = GET_ROOM_TILE(MX, M_Y, NX, NY)
                If CAN_WALK_ON_TILE(TL) Then
                    TL = GET_ROOM_CREATURE_TILE(MX, M_Y, NX, NY)
                    If TL = TILE_EMPTY Then
                        CREATURE_ROOM_COLUMN(i) = NX
                        CREATURE_ROOM_ROW(i) = NY
                        R = MOVE_SUCCESS
                    Else
                        If IS_TILE_CREATURE(TL) Then
                            R = MOVE_FIGHT
                        Else
                            R = MOVE_PICKUP
                        End If
                    End If
                End If
            End If
            PLACE_CREATURE(i)
        End If
        Return R
    End Function
End Module
' DEF GET_CREATURE_XP(I)
'     CT=CREATURE_TYPE(I)
'     IF EXISTS(CREATURETYPE_XP,CT) THEN
'         RETURN CREATURETYPE_XP(CT)
'     END IF
'     RETURN 0
' ENDDEF
' DEF CREATURE_DROP_ITEM(I)
'     CT=CREATURE_TYPE(I)
'     'TODO: CHANCE OF NOT DROPPING ITEM?
'     'TODO: WEIGHTED GENERATOR FOR WHAT ITEM GETS DROPPED?
'     IT=CREATURETYPE_ITEMTYPE_DROP(CT)
'     MX=CREATURE_MAZE_COLUMN(I)
'     M_Y=CREATURE_MAZE_ROW(I)
'     X=CREATURE_ROOM_COLUMN(I)
'     Y=CREATURE_ROOM_ROW(I)
'     II=CREATE_ROOM_ITEM(IT,MX,M_Y,X,Y)
'     PRINT II;
'     PLACE_ITEM(II)
' ENDDEF
' DEF CREATURE_ROLL_ATTACK(I)
'     IF EXISTS(CREATURE_WEAPONS,I) THEN
'         W=CREATURE_WEAPONS(I)
'         RETURN ITEM_ROLL_ATTACK(W)
'     END IF
'     RETURN 0
' ENDDEF
' DEF CREATURE_ROLL_DEFEND(I)
'     'ARMOR
'     RETURN 0
' ENDDEF
' DEF GET_CREATURE_NAME(I)
'     CT=CREATURE_TYPE(I)
'     RETURN CREATURETYPE_NAME(CT)
' ENDDEF
' DEF GET_CREATURE_HEALTH(I)
'     RETURN CREATURE_HITPOINTS(I)-CREATURE_WOUNDS(I)
' ENDDEF
' DEF FIND_CREATURE(MX,M_Y,X,Y)
'     FOR I=0 TO LEN(CREATURE_ALIVE)-1
'         IF CREATURE_ALIVE(I) THEN
'             IF MX=CREATURE_MAZE_COLUMN(I) AND M_Y=CREATURE_MAZE_ROW(I) AND X=CREATURE_ROOM_COLUMN(I) AND Y=CREATURE_ROOM_ROW(I) THEN
'                 RETURN I
'             END IF
'         END IF
'     NEXT I
'     RETURN -1
' ENDDEF
' DEF WOUND_CREATURE(I,D)
'     IF CREATURE_ALIVE(I) THEN
'         CREATURE_WOUNDS(I)=CREATURE_WOUNDS(I)+D
'         IF CREATURE_WOUNDS(I)>=CREATURE_HITPOINTS(I) THEN
'             CREATURE_ALIVE(I)=FALSE
'             CREATURE_WOUNDS(I)=CREATURE_HITPOINTS(I)
'         END IF
'     END IF
' ENDDEF
