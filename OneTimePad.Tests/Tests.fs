namespace OneTimePad.Tests

open OneTimePad
open Xunit
open Expecto

module Tests =
    
    let private key =
        "testkey"
        |> Key.create
        |> Result.failOnError

    let private message =
        "testmsg"
        |> Plaintext.create
        |> Result.failOnError

    [<Fact>]
    let encrypt () =
        let ciphertext =
            message
            |> Plaintext.encrypt key
            |> Result.failOnError
        
        ciphertext
        |> Ciphertext.asString
        |> Expect.equal "AAAAAAAAAAAAAAAAAAAAAAYAAAAWAAAAHgAAAA=="

    [<Fact>]
    let decrypt () =
        let ciphertext =
            message
            |> Plaintext.encrypt key
            |> Result.failOnError
            
        let plaintext =
            ciphertext
            |> Ciphertext.decrypt key
            |> Result.failOnError
  
        Expect.equal plaintext message 