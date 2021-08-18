open System
open OneTimePad

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
    
    key
    |> Result.bind (fun key ->
        let message =
            Console.Write "Message: "
            |> Console.ReadLine
        
        match selection with
        | "1" ->
            message
            |> Plaintext.create
            |> Result.bind (OneTimePad.encrypt key)
            |> Result.map Ciphertext.asString
        | "2" ->
            message
            |> Ciphertext.create
            |> Result.bind (OneTimePad.decrypt key)
            |> Result.map Plaintext.asString
        | _ -> Error "Bad selection."  
    )
    |> function
        | Ok message -> $"Ok: %s{message}"
        | Error error -> $"Error: %s{error}"
    |> Console.WriteLine
    
    0