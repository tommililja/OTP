open System
open OTP

[<EntryPoint>]
let main argv = 

    let key =
        Console.Write "Key: "
        |> Console.ReadLine
    
    "1. Encrypt"
    |> Console.WriteLine
    
    "2. Decrypt"
    |> Console.WriteLine
    
    let selection =
        "Select: "
        |> Console.Write
        |> Console.ReadLine
        
    let result =
        let message =
            Console.Write "Message: "
            |> Console.ReadLine
            
        match selection with
        | "1" -> message |> Encryption.encrypt key
        | "2" -> message |> Encryption.decrypt key
        | _ -> Error "Bad selection."
            
    match result with
    | Ok message ->
        sprintf "Output: %s" message
        |> Console.WriteLine 
    | Error error ->
        sprintf "Error: %s" error
        |> Console.WriteLine
    
    0