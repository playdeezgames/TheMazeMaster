Friend Class Creature
    Sub New(
           creatureTypeIdentifier As CreatureTypeIdentifier,
           mazeColumn As Integer,
           mazeRow As Integer,
           roomColumn As Integer,
           roomRow As Integer,
           Optional alive As Boolean = True)
        Me.Alive = alive
        Me.MazeColumn = mazeColumn
        Me.MazeRow = mazeRow
        Me.RoomColumn = roomColumn
        Me.RoomRow = roomRow
        Me.CreatureTypeIdentifier = creatureTypeIdentifier
        Me.HitPoints = CreatureType.HitPoints
        Me.Wounds = 0
        Me.Weapon = AllItemTypes(CreatureType.DefaultWeaponType).Create
    End Sub
    Private Property CreatureTypeIdentifier As CreatureTypeIdentifier
    ReadOnly Property CreatureType As CreatureType
        Get
            Return AllCreatureTypes(CreatureTypeIdentifier)
        End Get
    End Property
    Property Alive As Boolean
    Property MazeColumn As Integer
    Property MazeRow As Integer
    Property RoomColumn As Integer
    Property RoomRow As Integer
    Property HitPoints As Integer
    Property Wounds As Integer
    Property Weapon As Integer?
    Sub Place()
        Dim MX = MazeColumn
        Dim My = MazeRow
        Dim RM = GET_ROOM_MAP(MX, My)
        Dim TI = CreatureType.TileIndex
        MSET(RM, 2, RoomColumn, RoomRow, TI)
    End Sub
    ReadOnly Property Name As String
        Get
            Return CreatureType.Name
        End Get
    End Property
    ReadOnly Property Health As Integer
        Get
            Return HitPoints - Wounds
        End Get
    End Property
End Class
