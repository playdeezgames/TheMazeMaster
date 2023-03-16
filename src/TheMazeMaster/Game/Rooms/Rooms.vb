Friend Module Rooms
    Friend Const ROOM_ROWS = 15
    Friend Const ROOM_COLUMNS = 15
    Friend Const ROOM_CELL_WIDTH = 8
    Friend Const ROOM_CELL_HEIGHT = 8
    Private AllRooms As New List(Of Room)
    Friend Sub Generate()
        AllRooms.Clear()
        Dim TEMP As Integer = 0
        For ROW = 0 To MAZE_ROWS - 1
            For COLUMN = 0 To MAZE_COLUMNS - 1
                TEMP = 0
                For Each d In AllDirections
                    If MAZE_CELL_DOORS(COLUMN, ROW, d) Then
                        TEMP = TEMP Or (1 << d)
                    End If
                Next
                Dim EXIT_COUNT As Integer = Mazes.GET_MAZE_CELL_EXITS(COLUMN, ROW)
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
                AllRooms.Add(New Room(ROOM_MAP.ToMap, IS_CHAMBER))
            Next
        Next
        PLACE_ROOM_DOORS()
    End Sub

    Private Sub PLACE_ROOM_DOORS()
        For MX = 0 To MAZE_COLUMNS - 1
            For M_y = 0 To MAZE_ROWS - 1
                Dim EXIT_COUNT = GET_MAZE_CELL_EXITS(MX, M_y)
                If EXIT_COUNT = 1 Then
                    Dim D As DirectionIdentifier = DirectionIdentifier.North
                    While Not MAZE_CELL_DOORS(MX, M_y, D)
                        D = D.NextDirection
                    End While
                    Dim FM As MapAssetData

                    If IS_ROOM_CHAMBER(MX, M_y) Then
                        FM = CHAMBERDOOR_MAPS(D)
                    Else
                        FM = PASSAGEWAYDOOR_MAPS(D)
                    End If
                    Dim toMap = GetRoomMap(MX, M_y)
                    BlitTerrain(FM.ToMap, toMap, TerrainIdentifier.EMPTY)

                    Dim NX = D.StepX(MX)
                    Dim NY = D.StepY(M_y)
                    D = D.Opposite
                    If IS_ROOM_CHAMBER(NX, NY) Then
                        FM = CHAMBERDOOR_MAPS(D)
                    Else
                        FM = PASSAGEWAYDOOR_MAPS(D)
                    End If
                    toMap = GetRoomMap(NX, NY)
                    BlitTerrain(FM.ToMap, toMap, TerrainIdentifier.EMPTY)
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
    Friend Function GetRoomMap(COLUMN As Integer, ROW As Integer) As Map
        Return GetRoom(COLUMN, ROW).Map
    End Function
    Friend Function GetRoom(COLUMN As Integer, ROW As Integer) As Room
        Return AllRooms(COLUMN + ROW * MAZE_COLUMNS)
    End Function

    Function IS_ROOM_CHAMBER(MX As Integer, M_Y As Integer) As Boolean
        Return GetRoom(MX, M_Y).IsChamber
    End Function
End Module
