module Persistence

open Character
open System.IO

let private getFilePath rootPath characterName =
    Path.Combine (rootPath, characterName + ".txt")

let private saver rootPath =
    fun character ->
        let filePath = getFilePath rootPath character.name
        let lines = [
            "Name=" + character.name;
            "Class=" + stringFromClass character.cClass;
            "Level=" + character.level.ToString();
            "Experience=" + character.experience.ToString()
        ]

        File.WriteAllLines (filePath, lines) |> ignore

        character
        
let private loader rootPath =
    let read (line : string) =
        Array.get (line.Split ('=')) 1
    fun characterName ->
        let filePath = getFilePath rootPath characterName
        let lines = File.ReadAllLines (filePath)

        match lines with
        | [|name; cClass; level; exp|] ->
            read cClass 
            |> classFromString
            |> Option.map (fun c -> 
                {  name = read name;
                   cClass = c;
                   level = read level |> int; 
                   experience = read exp |> int })
        | _ -> None           

let private deleter rootPath =
    fun characterName ->
        let filePath = getFilePath rootPath characterName

        File.Delete (filePath)


let repository rootPath =
    Directory.CreateDirectory rootPath |> ignore
    (loader rootPath, saver rootPath, deleter rootPath)

        
