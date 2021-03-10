namespace PrimeNumbers.Tests

open FsCheck
open FsUnit
open NUnit.Framework
open PrimeNumbers.Generator

[<TestFixture>]
module Tests =
    [<Test>]
    let ``Calculates correctly for simple cases`` () =
        Generator.generatePrimes() |> Seq.item 0 |> should equal 1
        Generator.generatePrimes() |> Seq.item 2 |> should equal 3
        Generator.generatePrimes() |> Seq.item 3 |> should equal 5
        Generator.generatePrimes() |> Seq.item 1001 |> should equal 7927
        Generator.generatePrimes() |> Seq.item 10001 |> should equal 104743

    [<Test>]
    let ``Generates prime numbers`` () =
        let ithNumberIsPrime (i : uint) =
            Generator.generatePrimes() |> Seq.item (int(i)) |> Generator.isPrime
        Check.QuickThrowOnFailure ithNumberIsPrime