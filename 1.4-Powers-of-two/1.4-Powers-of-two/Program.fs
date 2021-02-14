let generatePowersOfTwoList n m =
    if n < 0 then
        failwith $"Invalid value of n: {n}. Input value must be nonnegative."
    elif m < 0 then
        failwith $"Invalid value of m: {m}. Input value must be nonnegative."

    [n..(n+m)] |> List.map (fun x -> pown 2 x)

printfn "n = 0, m = 0: %A" (generatePowersOfTwoList 0 0)
printfn "n = 0, m = 2: %A" (generatePowersOfTwoList 0 2)
printfn "n = 5, m = 3: %A" (generatePowersOfTwoList 5 3)