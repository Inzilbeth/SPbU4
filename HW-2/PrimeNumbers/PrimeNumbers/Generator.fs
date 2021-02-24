namespace PrimeNumbers.Generator

module Generator =
    let rec generatePrimes =
        let cacheUpToSqrt (s: int seq) =
            let array = new ResizeArray<int>([|3|])
            let e = s.GetEnumerator()
            let cache nn =
                    let m = array.[array.Count-1]
                    if m*m <= nn then e.MoveNext() |> ignore; array.Add e.Current
                    array
            cache
        seq {
            yield 2; yield 3
            let cache = cacheUpToSqrt generatePrimes
            let isPrime n = cache n |> Seq.forall (fun i -> (n % i <> 0))
            yield! {5 .. 2 .. System.Int32.MaxValue} |> Seq.filter isPrime
        }
