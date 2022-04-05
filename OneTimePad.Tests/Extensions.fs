namespace OneTimePad.Tests

module Result =
    
    let orFailWith = function
        | Ok v -> v
        | Error e -> failwith e