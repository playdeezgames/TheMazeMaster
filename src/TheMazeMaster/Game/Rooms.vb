Friend Module Rooms
    Friend Const ROOM_ROWS = 15
    Friend Const ROOM_COLUMNS = 15
    Friend Const ROOM_CELL_WIDTH = 8
    Friend Const ROOM_CELL_HEIGHT = 8
    Private ROOM_ASSET_MAPS As New List(Of MapAssetData)
    Private RoomMaps As New List(Of Map)
    Private ROOM_CHAMBERS As New List(Of Boolean)
    Friend Sub Generate()
        ROOM_ASSET_MAPS.Clear()
        ROOM_CHAMBERS.Clear()
        RoomMaps.Clear()
        Dim TEMP As Integer = 0
        For ROW = 0 To MAZE_ROWS - 1
            For COLUMN = 0 To MAZE_COLUMNS - 1
                TEMP = 0
                For d = DIRECTION_FIRST To DIRECTION_LAST
                    If MAZE_CELL_DOORS(COLUMN, ROW, d) Then
                        TEMP = TEMP Or (1 << d)
                    End If
                Next
                Dim EXIT_COUNT As Integer = Maze.GET_MAZE_CELL_EXITS(COLUMN, ROW)
                Dim IS_CHAMBER As Boolean = False
                If EXIT_COUNT = 1 Then
                    IS_CHAMBER = True
                ElseIf EXIT_COUNT = 2 Then
                    If Rnd(0, 99) < 50 Then
                        IS_CHAMBER = True
                    End If
                ElseIf EXIT_COUNT = 3 Then
                    If Rnd(0, 99) < 75 Then
                        IS_CHAMBER = True
                    End If
                Else
                    If Rnd(0, 99) < 25 Then
                        IS_CHAMBER = True
                    End If
                End If
                Dim ROOM_MAP As MapAssetData
                If IS_CHAMBER Then
                    ROOM_MAP = CLONE(CHAMBER_MAPS(TEMP))
                Else
                    ROOM_MAP = CLONE(PASSAGEWAY_MAPS(TEMP))
                End If
                ROOM_CHAMBERS.Add(IS_CHAMBER)
                ROOM_ASSET_MAPS.Add(ROOM_MAP)
                RoomMaps.Add(ROOM_MAP.ToMap)
            Next
        Next
        PLACE_ROOM_DOORS()
    End Sub

    Private Sub PLACE_ROOM_DOORS()
        For MX = 0 To MAZE_COLUMNS - 1
            For M_y = 0 To MAZE_ROWS - 1
                Dim EXIT_COUNT = GET_MAZE_CELL_EXITS(MX, M_y)
                If EXIT_COUNT = 1 Then
                    Dim D = 0
                    While Not MAZE_CELL_DOORS(MX, M_y, D)
                        D = D + 1
                    End While
                    Dim FM As MapAssetData

                    If IS_ROOM_CHAMBER(MX, M_y) Then
                        FM = CHAMBERDOOR_MAPS(D)
                    Else
                        FM = PASSAGEWAYDOOR_MAPS(D)
                    End If
                    Dim TM = GET_ROOM_MAP_ASSET(MX, M_y)
                    BLIT_MAP_ASSET(FM, TM, 1, TILE_EMPTY)
                    Dim toMap = GetRoomMap(MX, M_y)
                    BlitTerrain(FM.ToMap, toMap, TerrainIdentifier.EMPTY)

                    Dim NX = STEP_X(D, MX, M_y)
                    Dim NY = STEP_Y(D, MX, M_y)
                    D = OPPOSITE_DIRECTION(D)
                    If IS_ROOM_CHAMBER(NX, NY) Then
                        FM = CHAMBERDOOR_MAPS(D)
                    Else
                        FM = PASSAGEWAYDOOR_MAPS(D)
                    End If
                    TM = GET_ROOM_MAP_ASSET(NX, NY)
                    BLIT_MAP_ASSET(FM, TM, 1, TILE_EMPTY)

                End If
            Next
        Next
    End Sub

    Private Sub BlitTerrain(fromMap As Map, toMap As Map, transparent As TerrainIdentifier)
        For column = 0 To toMap.Columns - 1
            For row = 0 To toMap.Rows - 1
                Dim terrain = fromMap.GetCell(column, row).Terrain
                If terrain = transparent Then
                    Continue For
                End If
                toMap.GetCell(column, row).Terrain = terrain
            Next
        Next
    End Sub

    Private Sub BLIT_MAP_ASSET(FROM_MAP As MapAssetData, TO_MAP As MapAssetData, L As Integer, TRANSPARENT_TILE As Integer)
        For X = 0 To ROOM_COLUMNS - 1
            For Y = 0 To ROOM_ROWS - 1
                Dim T = MGET(FROM_MAP, L, X, Y)
                If T <> TRANSPARENT_TILE Then
                    MSET(TO_MAP, L, X, Y, T)
                End If
            Next
        Next
    End Sub

    Friend Function GET_ROOM_MAP_ASSET(COLUMN As Integer, ROW As Integer) As MapAssetData
        Return ROOM_ASSET_MAPS(COLUMN + ROW * MAZE_COLUMNS)
    End Function
    Friend Function GetRoomMap(COLUMN As Integer, ROW As Integer) As Map
        Return RoomMaps(COLUMN + ROW * MAZE_COLUMNS)
    End Function

    Function IS_ROOM_CHAMBER(MX As Integer, M_Y As Integer) As Boolean
        Return ROOM_CHAMBERS(MX + M_Y * MAZE_COLUMNS)
    End Function
    Friend Function GET_ROOM_TILE(MX As Integer, M_Y As Integer, X As Integer, Y As Integer) As Integer
        Dim MP = GET_ROOM_MAP_ASSET(MX, M_Y)
        Return MGET(MP, 1, X, Y)
    End Function
    Friend Function GET_ROOM_CREATURE_TILE(MX As Integer, M_Y As Integer, X As Integer, Y As Integer) As Integer
        Dim MP = GET_ROOM_MAP_ASSET(MX, M_Y)
        Return MGET(MP, 2, X, Y)
    End Function
End Module
