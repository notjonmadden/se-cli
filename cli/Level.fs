
module Level

type Level = 
    { number : int
      experienceThreshold : int
      grantsFeat : bool
      grantsAbilityIncrease : bool 
      grantsTalent : bool }

let rec private getExperienceThreshold =
    function 
    | 1 -> 0
    | l -> getExperienceThreshold (l - 1) + (1000 * (l - 1))

let private grantsFeat l =
    l % 2 = 0

let private grantsAbilityIncrease l =
    l % 4 = 0

let private grantsTalent l =
    (l % 2) = 1

let getBonuses level =
    [
        (if level.grantsAbilityIncrease then Some "Ability Increase" else None);
        (if level.grantsFeat then Some "New Feat" else None);
        (if level.grantsTalent then Some "New Talent" else None)
    ] |> List.filter Option.isSome
    |> List.map Option.get

let levels =
    [for i in 1 .. 20 -> 
        { number = i;
          experienceThreshold = getExperienceThreshold i;
          grantsFeat = grantsFeat i;
          grantsAbilityIncrease = grantsAbilityIncrease i;
          grantsTalent = grantsTalent i }]

let get =
    function
    | i when i < 1 -> None
    | i when i > 20 -> None
    | i -> Some levels.[i]

let getLevelByTotalExperience e =
    let exceedsThreshold l =
        e >= l.experienceThreshold
    levels |> List.find exceedsThreshold
