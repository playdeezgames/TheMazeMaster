Friend Class ItemType
    Friend Sub New(Optional stacks As Boolean = False)
        Me.Stacks = stacks
    End Sub
    Friend ReadOnly Property Stacks As Boolean
End Class
