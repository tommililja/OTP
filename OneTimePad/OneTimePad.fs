namespace OneTimePad

open ActivePatterns

module OneTimePad =
    
    let private validate key message =
        let lengths =
            key |> CipherKey.getLength,               
            message |> Message.getLength

        match lengths with
        | Same -> Ok (key, message)
        | Different -> Error "Key and plaintext/ciphertext must be the same length."
    
    let private xor (key, message) =
        let key =
            key
            |> CipherKey.asBytes         
        
        message
        |> Message.asBytes
        |> Array.mapi (fun i m ->
            key.[i] ^^^ m
        )
        
    let private oneTimePad key message =
        validate key message
        |> Result.map xor
        
    let encrypt key plaintext =
        plaintext
        |> Decrypted
        |> oneTimePad key 
        |> Result.map Ciphertext

    let decrypt key ciphertext =
        ciphertext
        |> Encrypted
        |> oneTimePad key 
        |> Result.map Plaintext