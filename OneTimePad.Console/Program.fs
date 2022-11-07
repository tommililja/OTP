open System
open Microsoft.FSharp.Core
open OneTimePad.Console
open OneTimePad

let private encrypt key =
    Plaintext.create
    |>= Plaintext.encrypt key
    |>> Ciphertext.asString

let private decrypt key =
    Ciphertext.create
    |>= Ciphertext.decrypt key
    |>> Plaintext.asString

let run _args = result {
    
    "1. Encrypt"
    |> Console.WriteLine
    
    "2. Decrypt"
    |> Console.WriteLine
    
    let! selection =
        "Select: "
        |> Console.Write
        |> Console.ReadLine
        |> Selection.parse

    let! key =
        "Key: "
        |> Console.Write 
        |> Console.ReadLine
        |> Key.create
        
    let message =
        "Message: "
        |> Console.Write
        |> Console.ReadLine
        
    let oneTimePad =
        match selection with
        | Encrypt -> encrypt key message
        | Decrypt -> decrypt key message
        
    return! oneTimePad
}

[<EntryPoint>]
let main args =
    args
    |> run
    |> Result.id
    |> Console.WriteLine
    
    0