namespace OneTimePad

open System

module ActivePatterns =
    
    let (|Same|Different|) = function
        | first, second when first = second -> Same
        | _ -> Different

    let (|Empty|NotEmpty|) = function
        | str when String.IsNullOrEmpty(str) -> Empty
        | _ -> NotEmpty

module String =
    open ActivePatterns
    
    let isNotNullOrEmpty name str =
        match str with
        | NotEmpty -> Ok str
        | Empty -> Error $"%s{name} length must be greater than 0."