module Cli

open Level
open Character
open Persistence
open System.Net.NetworkInformation

let formatCharacter c =
    System.String.Format ("{0}, level {1} {2} with {3} experience.", c.name, c.level, c.cClass, c.experience)

let formatLevel l =
    ("Level " + l.number.ToString() + ":\n") +
    (getBonuses l |> List.map (fun b -> b + "!") |> String.concat "\n")

[<EntryPoint>]
let main argv =
    let rootPath = "./characters"
    let (load, save, delete) = repository rootPath    

    match argv with
    | [| "new"; className; name |] ->
        let char = classFromString className
                |> Option.map (fun c -> create c name)
        match char with
        | Some c -> 
            save c |> ignore
            printfn "%s" (formatCharacter c)
        | None -> printfn "bad entry"
    | [| "exp"; charName; amount |] ->
        load charName 
            |> Option.map (fun char ->
                let exp = int amount
                let modChar, maybeLevel = giveExperience exp char
                modChar |> save  |> formatCharacter |> (printfn "%A") |> ignore
                maybeLevel |> Option.map formatLevel |> Option.map (printfn "%s"))
            |> ignore            
                
    | [| "diag" |] ->
        let print = formatLevel >> printfn "%A"
        List.iter print levels
        
    | _ -> printfn "%A" argv    
         
    0 // return an integer exit code
