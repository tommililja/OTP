namespace OneTimePad

open System
open System.Text

type Key = internal Key of byte[]

module Key =
    
    let create =
        String.isNotNullOrEmpty
        |>> Encoding.UTF32.GetBytes
        |>> Key

    let internal asBytes (Key c) = c
    
    let internal getLength = asBytes >> Array.length
    
    let asString = asBytes >> Convert.ToBase64String

type Ciphertext = internal Ciphertext of byte[]

module CipherText =
    
    let create =
        String.isNotNullOrEmpty
        |>> Convert.FromBase64String
        |>> Ciphertext
    
    let internal asBytes (Ciphertext c) = c
        
    let asString = asBytes >> Convert.ToBase64String 

type Plaintext = internal Plaintext of byte[]

module PlainText =
    
    let create =
        String.isNotNullOrEmpty
        |>> Encoding.UTF32.GetBytes
        |>> Plaintext
        
    let internal asBytes (Plaintext p) = p
        
    let asString = asBytes >> Encoding.UTF32.GetString 
    
type internal Message =
    | Encrypted of Ciphertext
    | Decrypted of Plaintext 
    
module internal Message =
    
    let asBytes = function
        | Encrypted message ->
            CipherText.asBytes message
        | Decrypted message ->
            PlainText.asBytes message
    
    let getLength = asBytes >> Array.length