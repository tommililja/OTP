namespace OneTimePad.Tests

open OneTimePad
open Xunit
open Expecto

module Tests =
    let key =
        "testkey"
        |> CipherKey.create
        |> Result.orFailWith

    let message =
        "testmsg"
        |> Plaintext.create
        |> Result.orFailWith

    [<Fact>]
    let encrypt () =
        let ciphertext =
            message
            |> OneTimePad.encrypt key
            |> Result.orFailWith
        
        ciphertext
        |> Ciphertext.asString
        |> Expect.equal "AAAAAAAAAAAAAAAAAAAAAAYAAAAWAAAAHgAAAA=="  

    [<Fact>]
    let decrypt () =
        let ciphertext =
            message
            |> OneTimePad.encrypt key
            |> Result.orFailWith
            
        let plaintext =
            ciphertext
            |> OneTimePad.decrypt key
            |> Result.orFailWith

        message
        |> Expect.equal plaintext