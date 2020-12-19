namespace OTP

module Encryption =
    
    let private validate key plaintext =
        if (String.length key) = (String.length plaintext)
        then Ok (key, plaintext)
        else Error "Key must be equal to the length of the message."
    
    let encrypt key plaintext =
        plaintext
        |> validate key
        |> Result.bind (fun (key, plaintext) ->
            plaintext
            |> String.toBytes
            |> Bytes.xor (key |> String.toBytes)
            |> Result.map (fun ciphertext ->
                ciphertext
                |> Bytes.toBase64String
            )
        )
  
    let decrypt key ciphertext =
        ciphertext
        |> String.fromBase64String
        |> Bytes.xor (key |> String.toBytes)
        |> Result.map (fun plaintext ->
            plaintext
            |> Bytes.toString
        )