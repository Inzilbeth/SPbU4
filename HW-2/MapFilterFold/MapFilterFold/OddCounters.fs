namespace MapFilterFold.EvenCounters

module EvenCounters =
    let mapEvenCounter list =
        List.map (fun x -> (((abs (x)) + 1) % 2)) list |> List.sum

    let filterEvenCounter list =
        (List.filter (fun x -> (x % 2 = 0)) list).Length

    let foldEvenCounter list =
        List.fold (fun acc x -> acc + (((abs (x)) + 1) % 2)) 0 list