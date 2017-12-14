module Character

open Level

type Class = Jedi | Soldier | Scoundrel | Noble | Scout

type Character = 
    { name       : string
      cClass     : Class
      level      : int 
      experience : int }

let create cClass name =
    { name = name; cClass = cClass; level = 1; experience = 0 }

let giveExperience amount char =
    let newAmount = max 0 (char.experience + amount)
    let level = getLevelByTotalExperience newAmount
    let leveledUp = level.number <> char.level
    let result = { char with experience = newAmount; level = level.number }

    (result, if leveledUp then Some level else None)

let classFromString s =
    let classFromLowerCaseString =
        function
        | "jedi"      -> Some Jedi
        | "scout"     -> Some Scout
        | "soldier"   -> Some Soldier
        | "noble"     -> Some Noble
        | "scoundrel" -> Some Scoundrel
        | _           -> None

    s |> String.map System.Char.ToLower |> classFromLowerCaseString

let stringFromClass =
    function
    | Jedi -> "Jedi"
    | Scout -> "Scout"
    | Scoundrel -> "Scoundrel"
    | Noble -> "Noble"
    | Soldier -> "Soldier"