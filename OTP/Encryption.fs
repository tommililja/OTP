namespace OTP

module Encryption =

    let validate key message =
        let key =
            key
            |> String.toBytes
 
        let isSameLength =
            message
            |> Bytes.isSameLength key
        
        if isSameLength
        then Ok (key, message)
        else Error "Key and plaintext/ciphertext are not the same length."
        
    let encrypt key plaintext =
        plaintext
        |> String.toBytes
        |> validate key 
        |> Result.map (fun (key, ciphertext) ->
            ciphertext
            |> Bytes.xor key
            |> Bytes.toBase64String
        )

    let decrypt key ciphertext =
        ciphertext
        |> String.fromBase64String
        |> validate key
        |> Result.map (fun (key, plaintext) ->
            plaintext
            |> Bytes.xor key
            |> Bytes.toString
        )