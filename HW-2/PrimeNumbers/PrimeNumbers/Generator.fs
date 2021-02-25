namespace PrimeNumbers.Generator

module Generator =
    let isPrime n =
        let upper = float n |> sqrt |> int
        seq{2..upper}
        |> Seq.exists (fun x -> n % x = 0)
        |> not

    let rec nextPrime n =
        if isPrime (n + 1) then n + 1
        else nextPrime (n + 1)

    let rec generatePrimes () =
        Seq.unfold (fun n -> Some(n, nextPrime n)) 1
