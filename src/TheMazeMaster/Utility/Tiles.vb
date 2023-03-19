Friend Module Tiles
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
    Friend Const TILE_GRASS = 35
    Friend Const TILE_FENCE_NORTH = 36
    Friend Const TILE_FENCE_EAST = 37
    Friend Const TILE_FENCE_SOUTH = 38
    Friend Const TILE_FENCE_WEST = 39
    Friend Const TILE_FENCE_CORNER_INSIDE_NORTHEAST = 40
    Friend Const TILE_FENCE_CORNER_INSIDE_NORTHWEST = 41
    Friend Const TILE_FENCE_CORNER_INSIDE_SOUTHEAST = 42
    Friend Const TILE_FENCE_CORNER_INSIDE_SOUTHWEST = 43

    Friend Const TILE_TERRAIN_START = TILE_WALL_NORTH
    Friend Const TILE_TERRAIN_END = TILE_FENCE_CORNER_INSIDE_SOUTHWEST

    Friend Const TILE_RATTAIL = 80
    Friend Const tile_köttbulle = 81
    Friend Const TILE_PENNY = 82
    Friend Const TILE_MACGUFFIN = 83

    Friend Const TILE_RAT = 237
    Friend Const TILE_DUDE = 238

    Friend Const TILE_EMPTY = 239

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
            {TILE_DUDE, "[white]☻[/]"},
            {tile_köttbulle, "[olive]∙[/]"},
            {TILE_PENNY, "[olive]¢[/]"},
            {TILE_MACGUFFIN, "[aqua]√[/]"}
        }
End Module

