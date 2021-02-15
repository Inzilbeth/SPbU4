let findIndex list value =
    let rec findIndexSub list i =
       match list with
       | head :: tail when head = value -> Some(i)
       | head :: tail -> findIndexSub tail (i + 1)
       | [] -> None
    findIndexSub list 0

printfn "Index of 4 in [1..20]: %A" (findIndex [1..20] 4)
printfn "Index of 777 in [1..20]: %A" (findIndex [1..20] 777)