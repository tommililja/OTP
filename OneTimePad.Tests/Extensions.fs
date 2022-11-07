namespace OneTimePad.Tests

module Result =
    
    let failOnError = function
        | Ok v -> v
        | Error e -> failwith e