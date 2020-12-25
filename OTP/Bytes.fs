namespace OTP

open System
open System.Text

module Bytes = 

    let toString bytes =
        bytes
        |> Encoding.UTF32.GetString
        
    let toBase64String bytes =
        bytes
        |> Convert.ToBase64String
        
    let isSameLength a b =
        (a |> Array.length) = (b |> Array.length)
        
    let xor first (second:byte[]) =
        first
        |> Array.mapi (fun i _ ->
            first.[i] ^^^ second.[i])