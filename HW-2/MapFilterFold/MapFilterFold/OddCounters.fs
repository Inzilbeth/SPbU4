namespace MapFilterFold.OddCounters

module OddCounters =
    let mapOddCounter list =
        List.map (fun x -> (((abs (x)) + 1) % 2)) list |> List.sum

    let filterOddCounter list =
        (List.filter (fun x -> (x % 2 = 0)) list).Length

    let foldOddCounter list =
        List.fold (fun acc x -> acc + (((abs (x)) + 1) % 2)) 0 list