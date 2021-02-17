let generatePowersOfTwoList n m =
    if n < 0 then
        failwith $"Invalid value of n: {n}. Input value must be nonnegative."
    elif m < 0 then
        failwith $"Invalid value of m: {m}. Input value must be nonnegative."

    let rec powersOfTwoSub list value i =
        if i <= m + n then
            powersOfTwoSub ((value / 2) :: list) (value / 2) (i + 1)
        else
            list

    powersOfTwoSub [] (pown 2 (m + n + 1)) n

printfn "n = 0, m = 0: %A" (generatePowersOfTwoList 0 0)
printfn "n = 0, m = 2: %A" (generatePowersOfTwoList 0 2)
printfn "n = 5, m = 3: %A" (generatePowersOfTwoList 5 3)