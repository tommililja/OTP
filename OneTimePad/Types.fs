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

    let asBytes (Key key) = key

    let asString = asBytes >> Convert.ToBase64String
        
    let getLength = asBytes >> Array.length

module Ciphertext =
    
    let create =
        String.isNotNullOrEmpty
        |>> Convert.FromBase64String
        |>> Ciphertext
       
    let asBytes (Ciphertext ciphertext) = ciphertext
       
    let asString = asBytes >> Convert.ToBase64String 
       
    let decrypt (Key key) =
        asBytes >> Pad.create key
        |>> Plaintext

module Plaintext =
    
    let create =
        String.isNotNullOrEmpty
        |>> Encoding.UTF32.GetBytes
        |>> Plaintext
        
    let asBytes (Plaintext plaintext) = plaintext
    
    let asString = asBytes >> Encoding.UTF32.GetString
    
    let encrypt (Key key) =
        asBytes >> Pad.create key
        |>> Ciphertext