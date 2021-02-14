let generatePowersOfTwoList n m =
    if n < 0 then
        failwith $"Invalid value of n: {n}. Input value must be nonnegative."
    elif m < 0 then
        failwith $"Invalid value of m: {m}. Input value must be nonnegative."

    [n..(n+m)] |> List.map (fun x -> pown 2 x)

printfn "%A" (generatePowersOfTwoList 0 0)
printfn "%A" (generatePowersOfTwoList 0 2)
printfn "%A" (generatePowersOfTwoList 5 3)