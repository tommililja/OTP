namespace OneTimePad

open System

[<AutoOpen>]
module ActivePatterns =
    
    let (|Same|Different|) = function
        | first, second when first = second -> Same
        | _ -> Different

    let (|Empty|Something|) = function
        | str when String.IsNullOrEmpty str -> Empty
        | str -> Something str

module String =
    
    let isNotNullOrEmpty = function
        | Something str -> Ok str
        | Empty -> Error "String cannot be empty."

module Array =
    
    let compare a b msg =
        match Array.length a, Array.length b with
        | Same -> Ok ()
        | Different -> Error msg
        
    let xor (bytes:byte[]) = Array.mapi (fun i -> (^^^) bytes[i])

[<AutoOpen>]
module Result =
    
    let inline (|>>) func1 func2 = func1 >> Result.map func2
    
    let inline (|>=) func1 func2 = func1 >> Result.bind func2
    
    let id = function
        | Ok v -> v
        | Error e -> e
        
    type ResultBuilder() =
            
        member this.Bind (x, f) = Result.bind f x
        
        member this.Return x = Ok x
        
        member this.ReturnFrom (x:Result<_,_>) = x
        
        member this.Zero () = Ok ()
            
    let result = ResultBuilder()