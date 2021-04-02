namespace Tests

open Test
open NUnit.Framework
open FsUnit

module Tests =
    [<Test>]
    let ``Test on n = 2`` () =
        Test.Square.print 2 |> should equal "**\n**"

    [<Test>]
    let ``Test on n = 3`` () =
        Test.Square.print 3 |> should equal "***\n* *\n***"

    [<Test>]
    let ``Test on n = 4`` () =
        Test.Square.print 4 |> should equal "****\n*  *\n*  *\n****"

    [<Test>]
    let ``Test on n = 1`` () =
        Test.Square.print 1 |> should equal "*"

    [<Test>]
    let ``Test on n = 0`` () =
        Test.Square.print 0 |> should equal ""
