Friend Class ItemType
    Friend Sub New(
                  identifier As ItemTypeIdentifier,
                  name As String,
                  Optional tileIndex As Integer = TILE_EMPTY,
                  Optional stacks As Boolean = False,
                  Optional attackValue As Integer = 0,
                  Optional attackMaximum As Integer = 0)
        Me.Identifier = identifier
        Me.Stacks = stacks
        Me.Name = name
        Me.TileIndex = tileIndex
        Me.AttackValue = attackValue
        Me.AttackMaximum = attackMaximum
    End Sub
    Friend ReadOnly Property Identifier As ItemTypeIdentifier
    Friend ReadOnly Property Stacks As Boolean
    Public ReadOnly Property Name As String
    Public ReadOnly Property TileIndex As Integer
    Public ReadOnly Property AttackValue As Integer
    Public ReadOnly Property AttackMaximum As Integer
    Public Function RollAttack() As Integer
        Return ROLL_DICE(AttackValue, AttackMaximum)
    End Function
    Friend Function Create() As Integer
        'TODO: FIRST LOOK FOR EMPTY ITEM
        Dim I = AllItems.Count
        AllItems.Add(New Item(Identifier))
        Return I
    End Function
    Friend Function CreateInRoom(mx As Integer, m_y As Integer, x As Integer, y As Integer) As Integer
        Dim i = Create()
        AllItems(i).MazeColumn = mx
        AllItems(i).MazeRow = m_y
        AllItems(i).RoomColumn = x
        AllItems(i).RoomRow = y
        Return i
    End Function
End Class
