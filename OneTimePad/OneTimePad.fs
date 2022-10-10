namespace OneTimePad

module OneTimePad =

    let private create key message = result {
        let key =
            key
            |> Key.asBytes
            
        let message =
            message
            |> Message.asBytes
        
        do!        
            match key.Length, message.Length with
            | Same -> Ok ()
            | Different -> Error "Key and message must be the same length."
        
        let xor i m = m ^^^ key[i]
       
        return Array.mapi xor message
    }
    
    let encrypt key =
        Decrypted
        >> create key
        |>> Ciphertext

    let decrypt key =
        Encrypted
        >> create key
        |>> Plaintext