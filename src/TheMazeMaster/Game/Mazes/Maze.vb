Friend Class Maze
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Private ReadOnly Cells As New List(Of MazeCell)
    Sub New(columns As Integer, rows As Integer)
        Me.Columns = columns
        Me.Rows = rows
        Cells.Clear()
        While Cells.Count < columns * rows
            Cells.Add(New MazeCell)
        End While
    End Sub
    Private Sub Clear()
        For Each cell In Cells
            cell.Clear()
        Next
    End Sub
    Function GetCell(column As Integer, row As Integer) As MazeCell
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Return Nothing
        End If
        Return Cells(column + row * Columns)
    End Function

    Friend Sub Generate()
        Clear()
        Dim COLUMN = Rnd(0, Columns - 1)
        Dim ROW = Rnd(0, Rows - 1)
        Dim NEXT_COL As Integer
        Dim NEXT_ROW As Integer
        Dim direction As DirectionIdentifier
        GetCell(COLUMN, ROW).State = MazeCellState.Inside
        For Each direction In AllDirections
            NEXT_COL = direction.StepX(COLUMN)
            NEXT_ROW = direction.StepY(ROW)
            If NEXT_COL >= 0 AndAlso NEXT_ROW >= 0 AndAlso NEXT_COL < Columns AndAlso NEXT_ROW < Rows Then
                GetCell(NEXT_COL, NEXT_ROW).State = MazeCellState.Frontier
            End If
        Next
        Dim CANDIDATES(DIRECTION_COUNT - 1) As Boolean
        Do
            Do
                COLUMN = Rnd(0, Columns - 1)
                ROW = Rnd(0, Rows - 1)
            Loop Until GetCell(COLUMN, ROW).State = MazeCellState.Frontier
            For Each direction In AllDirections
                CANDIDATES(direction) = False
                NEXT_COL = direction.StepX(COLUMN)
                NEXT_ROW = direction.StepY(ROW)
                If NEXT_COL >= 0 AndAlso NEXT_ROW >= 0 AndAlso NEXT_COL < Columns AndAlso NEXT_ROW < Rows Then
                    If GetCell(NEXT_COL, NEXT_ROW).State = MazeCellState.Inside Then
                        CANDIDATES(direction) = True
                    End If
                End If
            Next
            Do
                direction = Rnd(AllDirections)
            Loop Until CANDIDATES(direction) = True
            GetCell(COLUMN, ROW).State = MazeCellState.Inside
            GetCell(COLUMN, ROW).AddDoor(direction)
            NEXT_COL = direction.StepX(COLUMN)
            NEXT_ROW = direction.StepY(ROW)
            GetCell(NEXT_COL, NEXT_ROW).AddDoor(direction.Opposite)
            For Each direction In AllDirections
                NEXT_COL = direction.StepX(COLUMN)
                NEXT_ROW = direction.StepY(ROW)
                If NEXT_COL >= 0 AndAlso NEXT_ROW >= 0 AndAlso NEXT_COL < Columns AndAlso NEXT_ROW < Rows Then
                    If GetCell(NEXT_COL, NEXT_ROW).State = MazeCellState.Outside Then
                        GetCell(NEXT_COL, NEXT_ROW).State = MazeCellState.Frontier
                    End If
                End If
            Next
        Loop Until IsGenerated()
    End Sub

    Private Function IsGenerated() As Boolean
        Return Cells.All(Function(x) x.State = MazeCellState.Inside)
    End Function
End Class
