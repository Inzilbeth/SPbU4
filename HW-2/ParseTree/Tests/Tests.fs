namespace ParseTree.Tests

open FsCheck
open FsUnit
open NUnit.Framework
open ParseTree.Parser

[<TestFixture>]
module Tests =
    [<Test>]
    let ``Should work in simple case`` () =
        Parser.parse (Mult(Add(Number(8), Number(7)), Div(Number(42), Number(6)))) |> should equal 105

    [<Test>]
    let ``Shuold throw on division by zero`` () =
        (fun () -> Parser.parse (Div(Number(77), Number(0))) |> ignore)
        |> should (throwWithMessage "Division by zero occured!") typeof<System.InvalidOperationException>

    [<Test>]
    let ``Should return a number`` () =
        let isNumberReturned expression =
            try
                Parser.parse expression |> should be ofExactType<int>
            with
                | :? System.DivideByZeroException -> ()
                | :? System.InvalidOperationException -> ()
        Check.QuickThrowOnFailure isNumberReturned


