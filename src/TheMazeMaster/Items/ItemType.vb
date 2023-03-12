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
End Class
