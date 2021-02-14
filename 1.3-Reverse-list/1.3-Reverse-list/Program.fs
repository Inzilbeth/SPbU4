let reverseList list =
    let rec reverseListSub list accumulator =
        match list with
            | [] -> accumulator
            | [x] -> x::accumulator
            | head::tail -> reverseListSub tail (head::accumulator)
    reverseListSub list []

printfn "Original list: %A, reversed list: %A\n" [1..10] (reverseList [1..10])
printfn "Original list: %A, reversed list: %A" [0] (reverseList [0])