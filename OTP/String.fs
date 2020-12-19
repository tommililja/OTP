namespace OTP

open System
open System.Text

module String =
    
    let toBytes (string:string) =
        string
        |> Encoding.UTF32.GetBytes

    let fromBase64String string =
      string
      |> Convert.FromBase64String