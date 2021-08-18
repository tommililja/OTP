namespace OneTimePad

open OneTimePad.ActivePatterns

module OneTimePad =
    let private validate key message =
        let length =
            (key |> CipherKey.getLength),
            (message |> Message.getLength)

        match length with
        | Same -> Ok (key, message)
        | Different -> Error "Key and plaintext/ciphertext must be the same length."
    
    let private convert (key, message) =
        (key |> CipherKey.asBytes),
        (message |> Message.asBytes)
    
    let private oneTimePad key message =
        validate key message
        |> Result.map (convert >> (fun (key, message) ->
            message
            |> Array.mapi (fun i _ ->
                key.[i] ^^^ message.[i]
            )
        ))
        
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