namespace OneTimePad

open System
open System.Text

type Key = internal Key of byte[]

type Plaintext = internal Plaintext of byte[]

type Ciphertext = internal Ciphertext of byte[]

module Key =
    
    let create =
        String.isNotNullOrEmpty
        |>> Encoding.UTF32.GetBytes
        |>> Key

    let internal asBytes (Key c) = c
    
    let internal getLength = asBytes >> Array.length
    
    let asString = asBytes >> Convert.ToBase64String

module Ciphertext =
    
    let create =
        String.isNotNullOrEmpty
        |>> Convert.FromBase64String
        |>> Ciphertext
    
    let internal asBytes (Ciphertext c) = c
    
    let decrypt key ciphertext = result {
        let key = Key.asBytes key
        let ciphertext = asBytes ciphertext

        do!
            "Key and ciphertext must be of equal length."
            |> Array.compare key ciphertext 
        
        return
            ciphertext
            |> Array.xor key
            |> Plaintext
    }
        
    let asString = asBytes >> Convert.ToBase64String 

module Plaintext =
    
    let create =
        String.isNotNullOrEmpty
        |>> Encoding.UTF32.GetBytes
        |>> Plaintext
        
    let internal asBytes (Plaintext p) = p
        
    let encrypt key plaintext = result {
        let key = Key.asBytes key
        let plaintext = asBytes plaintext

        do!
            "Key and plaintext must be of equal length."
            |> Array.compare key plaintext 
        
        return
            plaintext
            |> Array.xor key
            |> Ciphertext
    }
        
    let asString = asBytes >> Encoding.UTF32.GetString