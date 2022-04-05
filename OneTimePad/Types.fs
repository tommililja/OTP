namespace OneTimePad

open System
open System.Text

type CipherKey =
    private CipherKey of byte []
    
type Ciphertext =
    private Ciphertext of byte []
    
type Plaintext =
    private Plaintext of byte []

type internal Message =
    | Encrypted of Ciphertext
    | Decrypted of Plaintext

module CipherKey =
    
    let create key =
        key
        |> String.isNotNullOrEmpty (nameof key)
        |> Result.map (
            Encoding.UTF32.GetBytes
            >> CipherKey
        )

    let internal asBytes (CipherKey c) = c
    
    let internal getLength = asBytes >> Array.length

module Ciphertext =
    
    let create ciphertext =
        ciphertext
        |> String.isNotNullOrEmpty (nameof ciphertext)
        |> Result.map (
            Convert.FromBase64String
            >> Ciphertext
        )
    
    let internal asBytes (Ciphertext c) = c
        
    let asString (Ciphertext ciphertext) =
        ciphertext
        |> Convert.ToBase64String 

module Plaintext =
    
    let create plaintext =
        plaintext
        |> String.isNotNullOrEmpty (nameof plaintext)
        |> Result.map (
            Encoding.UTF32.GetBytes
            >> Plaintext
        )
        
    let internal asBytes (Plaintext p) = p
        
    let asString (Plaintext plaintext) =
        plaintext
        |> Encoding.UTF32.GetString 
    
module internal Message =
    
    let asBytes = function
        | Encrypted message ->
            message
            |> Ciphertext.asBytes
        | Decrypted message ->
            message
            |> Plaintext.asBytes
    
    let getLength = asBytes >> Array.length