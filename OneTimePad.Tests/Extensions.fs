namespace OneTimePad.Tests

module Result =
    let orFailWith r =
        match r with
        | Ok v -> v
        | Error e -> failwith e