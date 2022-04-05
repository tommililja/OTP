open System
open OneTimePad

let encrypt key message =
    message
    |> Plaintext.create
    |> Result.bind (OneTimePad.encrypt key)
    |> Result.map Ciphertext.asString

let decrypt key message =
    message
    |> Ciphertext.create
    |> Result.bind (OneTimePad.decrypt key)
    |> Result.map Plaintext.asString

[<EntryPoint>]
let main _ =
    "1. Encrypt"
    |> Console.WriteLine
    
    "2. Decrypt"
    |> Console.WriteLine
    
    let selection =
        "Select: "
        |> Console.Write
        |> Console.ReadLine
    
    let key =
        Console.Write "Key: "
        |> Console.ReadLine
        |> CipherKey.create
    
    let result =
        key
        |> Result.bind (fun key ->
            let message =
                Console.Write "Message: "
                |> Console.ReadLine
                
            match selection with
            | "1" -> encrypt key message
            | "2" -> decrypt key message
            | _ -> Error "Invalid selection."  
        )
    
    match result with
        | Ok message -> $"Ok: %s{message}"
        | Error error -> $"Error: %s{error}"
        |> Console.WriteLine
    
    0