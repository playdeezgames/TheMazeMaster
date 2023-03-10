Friend Module Tiles
    Friend Const TILE_MAZE_0 = 0
    Friend Const TILE_MAZE_1 = 1
    Friend Const TILE_MAZE_2 = 2
    Friend Const TILE_MAZE_3 = 3
    Friend Const TILE_MAZE_4 = 4
    Friend Const TILE_MAZE_5 = 5
    Friend Const TILE_MAZE_6 = 6
    Friend Const TILE_MAZE_7 = 7
    Friend Const TILE_MAZE_8 = 8
    Friend Const TILE_MAZE_9 = 9
    Friend Const TILE_MAZE_10 = 10
    Friend Const TILE_MAZE_11 = 11
    Friend Const TILE_MAZE_12 = 12
    Friend Const TILE_MAZE_13 = 13
    Friend Const TILE_MAZE_14 = 14
    Friend Const TILE_MAZE_15 = 15
    Friend Const TILE_WALL_NORTH = 16
    Friend Const TILE_WALL_EAST = 17
    Friend Const TILE_WALL_SOUTH = 18
    Friend Const TILE_WALL_WEST = 19
    Friend Const TILE_CORNER_INSIDE_NORTHEAST = 20
    Friend Const TILE_CORNER_INSIDE_SOUTHEAST = 21
    Friend Const TILE_CORNER_INSIDE_SOUTHWEST = 22
    Friend Const TILE_CORNER_INSIDE_NORTHWEST = 23
    Friend Const TILE_CORNER_OUTSIDE_NORTHEAST = 24
    Friend Const TILE_CORNER_OUTSIDE_SOUTHEAST = 25
    Friend Const TILE_CORNER_OUTSIDE_SOUTHWEST = 26
    Friend Const TILE_CORNER_OUTSIDE_NORTHWEST = 27
    Friend Const TILE_DOOR_NORTH = 28
    Friend Const TILE_DOOR_EAST = 29
    Friend Const TILE_DOOR_SOUTH = 30
    Friend Const TILE_DOOR_WEST = 31
    Friend Const TILE_SOLID = 32
    Friend Const TILE_FLOOR = 33
    Friend Const TILE_BLOOD = 34
    Friend Const TILE_TERRAIN_START = TILE_WALL_NORTH
    Friend Const TILE_TERRAIN_END = TILE_BLOOD

    Friend Const TILE_RATTAIL = 80
    Friend Const TILE_ITEM_START = TILE_RATTAIL
    Friend Const TILE_ITEM_END = TILE_RATTAIL

    Friend Const TILE_RAT = 237
    Friend Const TILE_DUDE = 238
    Friend Const TILE_CREATURE_START = TILE_RAT
    Friend Const TILE_CREATURE_END = TILE_DUDE

    Friend Const TILE_EMPTY = 239

    '╔╗╚╝
    Friend ReadOnly TILE_TABLE As IReadOnlyDictionary(Of Integer, String) =
        New Dictionary(Of Integer, String) From
        {
            {TILE_WALL_NORTH, "[blue]═[/]"},
            {TILE_WALL_EAST, "[blue]║[/]"},
            {TILE_WALL_SOUTH, "[blue]═[/]"},
            {TILE_WALL_WEST, "[blue]║[/]"},
            {TILE_CORNER_INSIDE_NORTHEAST, "[blue]╗[/]"},
            {TILE_CORNER_INSIDE_SOUTHEAST, "[blue]╝[/]"},
            {TILE_CORNER_INSIDE_SOUTHWEST, "[blue]╚[/]"},
            {TILE_CORNER_INSIDE_NORTHWEST, "[blue]╔[/]"},
            {TILE_CORNER_OUTSIDE_NORTHEAST, "[blue]╗[/]"},
            {TILE_CORNER_OUTSIDE_SOUTHEAST, "[blue]╝[/]"},
            {TILE_CORNER_OUTSIDE_SOUTHWEST, "[blue]╚[/]"},
            {TILE_CORNER_OUTSIDE_NORTHWEST, "[blue]╔[/]"},
            {TILE_DOOR_NORTH, "[grey].[/]"},
            {TILE_DOOR_EAST, "[grey].[/]"},
            {TILE_DOOR_SOUTH, "[grey].[/]"},
            {TILE_DOOR_WEST, "[grey].[/]"},
            {TILE_SOLID, "[grey]█[/]"},
            {TILE_FLOOR, "[grey].[/]"},
            {TILE_BLOOD, "[red],[/]"},
            {TILE_RATTAIL, "[fuchsia])[/]"},
            {TILE_RAT, "[fuchsia]r[/]"},
            {TILE_DUDE, "[white]☻[/]"}
        }
    Friend Function CAN_WALK_ON_TILE(T As Integer) As Boolean
        If T = TILE_FLOOR OrElse T = TILE_DOOR_EAST OrElse T = TILE_DOOR_WEST OrElse T = TILE_DOOR_NORTH OrElse T = TILE_DOOR_SOUTH OrElse T = TILE_BLOOD Then
            Return True
        End If
        Return False
    End Function
    Friend Function IS_TILE_CREATURE(T As Integer) As Boolean
        If T >= TILE_CREATURE_START AndAlso T <= TILE_CREATURE_END Then
            Return True
        End If
        Return False
    End Function
    Friend Function IS_TILE_ITEM(T As Integer) As Boolean
        If T >= TILE_ITEM_START AndAlso T <= TILE_ITEM_END Then
            Return True
        End If
        Return False
    End Function
End Module

