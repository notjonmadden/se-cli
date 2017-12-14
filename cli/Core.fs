module Core

type Ability = Str | Dex | Con | Cha | Wis | Int

type Skill = { name : string; ability : Ability }

let private makeSkills governingAbility =
    List.map (fun n -> { name = n; ability = governingAbility })

let skills =
    List.concat [
        makeSkills Str [
                "Climb";
                "Jump";
                "Swim"
            ];
        makeSkills Dex [
                "Acrobatics";
                "Initiative";
                "Pilot";
                "Ride";
                "Stealth"
            ];
        makeSkills Con [
                "Endurance"
            ];
        makeSkills Cha [
                "Deception";
                "Gather Information";
                "Persuasion";
                "Use the Force"
            ];
        makeSkills Wis [
                "Perception";
                "Survival";
                "Treat Injury"
            ];
        makeSkills Int [
                "Knowledge";
                "Mechanics";
                "Use Computer"
            ]
    ]