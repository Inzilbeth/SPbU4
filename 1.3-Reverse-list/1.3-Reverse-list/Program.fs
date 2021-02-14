let reverseList list =
    let rec reverseListSub list accumulator =
        match list with
            | [] -> accumulator
            | [x] -> x::accumulator
            | head::tail -> reverseListSub tail (head::accumulator)
    reverseListSub list []

reverseList [1..10] |> List.iter (printf "%d ")
reverseList [0] |> List.iter (printf "\n%d ")
