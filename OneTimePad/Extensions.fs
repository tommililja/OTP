namespace OneTimePad

open System

[<AutoOpen>]
module ActivePatterns =
    
    let (|Same|Different|) = function
        | first, second when first = second -> Same
        | _ -> Different

    let (|Empty|NotEmpty|) = function
        | str when String.IsNullOrEmpty(str) -> Empty
        | str -> NotEmpty str

module String =
    
    let isNotNullOrEmpty = function
        | NotEmpty str -> Ok str
        | Empty -> Error "String cannot be empty."

[<AutoOpen>]
module Result =
    
    let inline (|>>) func1 func2 = func1 >> Result.map func2
    
    let inline (|>=) func1 func2 = func1 >> Result.bind func2
    
    let id = function
        | Ok r -> r
        | Error r -> r
        
    type ResultBuilder() =
            
        member this.Bind (x, f) = Result.bind f x
        
        member this.Return x = Ok x
        
        member this.ReturnFrom (x:Result<_,_>) = x
        
        member this.Zero () = Ok ()
            
    let result = ResultBuilder()