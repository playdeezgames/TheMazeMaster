Friend Class Character
    Sub New(creatureIndex As Integer)
        Me.CreatureIndex = creatureIndex
    End Sub
    Friend Property XP As Integer = 0
    Friend Property XPGoal As Integer = 10
    Friend ReadOnly Property Inventory As New List(Of Integer)
    Friend ReadOnly Property ItemStacks As New Dictionary(Of ItemTypeIdentifier, Integer)
    Friend ReadOnly Property CreatureIndex As Integer
    Friend ReadOnly Property Creature As Creature
        Get
            Return AllCreatures(CreatureIndex)
        End Get
    End Property
End Class
