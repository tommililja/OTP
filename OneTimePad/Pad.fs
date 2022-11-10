namespace OneTimePad

module Pad =
    
    [<Literal>]
    let KeyMessageLengthError = "Key and message must be of equal length."
     
    let create (message:byte[]) key = result {
        
        do!
            message
            |> Array.compare key 
            |> Bool.asResult KeyMessageLengthError
            
        let pad =
            message
            |> Array.mapi (fun i -> (^^^) key[i])

        return pad
    }