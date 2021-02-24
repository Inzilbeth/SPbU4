namespace PrimeNumbers.Tests

open FsCheck
open FsUnit
open NUnit.Framework
open PrimeNumbers.Generator

[<TestFixture>]
module Tests =
    [<Test>]
    let ``Calculates correctly for simple cases`` () =
        Generator.generatePrimes |> Seq.item 0 |> should equal 2
        Generator.generatePrimes |> Seq.item 1 |> should equal 3
        Generator.generatePrimes |> Seq.item 2 |> should equal 5
        Generator.generatePrimes |> Seq.item 1000 |> should equal 7927
        Generator.generatePrimes |> Seq.item 10000 |> should equal 104743

    [<Test>]
    let ``Generates prime numbers`` () =
        let isPrime number =
            let upper = int (sqrt(float number))
            seq {2..upper}
            |> Seq.exists (fun x -> number % x = 0)
            |> not
        let ithNumberIsPrime (i : uint) =
            Generator.generatePrimes |> Seq.item (int(i)) |> isPrime
        Check.QuickThrowOnFailure ithNumberIsPrime