namespace OneTimePad

open System

module ActivePatterns =
    let (|Same|Different|) (val1, val2) =
        if val1 = val2
        then Same
        else Different

    let (|Empty|NotEmpty|) str =
        if String.IsNullOrEmpty(str)
        then Empty
        else NotEmpty

module String =
    open ActivePatterns
    
    let isNotNullOrEmpty name str =
        match str with
        | NotEmpty -> Ok str
        | Empty -> Error $"%s{name} length must be greater than 0."