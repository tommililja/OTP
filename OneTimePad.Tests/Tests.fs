namespace OneTimePad.Tests

open OneTimePad
open Xunit
open Expecto

module Tests =
    
    let private key =
        "testkey"
        |> Key.create
        |> Result.orFailWith

    let private message =
        "testmsg"
        |> PlainText.create
        |> Result.orFailWith

    [<Fact>]
    let encrypt () =
        let ciphertext =
            message
            |> OneTimePad.encrypt key
            |> Result.orFailWith
        
        ciphertext
        |> CipherText.asString
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

        plaintext
        |> Expect.equal message