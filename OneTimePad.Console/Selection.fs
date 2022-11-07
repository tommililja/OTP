namespace OneTimePad.Console

type Selection =
    | Encrypt
    | Decrypt

module Selection =
    
    let parse = function
        | "1" -> Ok Encrypt
        | "2" -> Ok Decrypt
        | _ -> Error "Invalid selection."